using System;
using System.Collections.Generic;
using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;
using F1Solutions.InfrastructureStatistics.Services;
using F1Solutions.InfrastructureStatistics.Services.Models;
using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator
{
    public class ApiOrchestrator
    {
        private readonly AirCallApiTask _airCallApiTask;
        private readonly FreshServiceApiTask _freshServiceApiTask;
        private readonly FreshServiceTimeEntriesTask _freshServiceTimeEntriesTask;
        private readonly FreshServiceAgentGroupApiTask _freshServiceAgentGroupApiTask;
        private readonly StatisticsService _statisticsService;
        private readonly ServiceCaller _serviceCaller;
        private readonly CacheHelper _cacheHelper;
        private readonly ServiceHelper _serviceExecutionHelper;

        private StatisticsDataModel _statisticsModel;
        private MonthlyStatisticsDataModel _monthlyStatisticsModel;
        private string _levelOneGroupIdentifierId = null;

        public ApiOrchestrator()
        {
            _airCallApiTask = new AirCallApiTask();
            _freshServiceApiTask = new FreshServiceApiTask();
            _freshServiceAgentGroupApiTask = new FreshServiceAgentGroupApiTask();
            _freshServiceTimeEntriesTask = new FreshServiceTimeEntriesTask();
            _statisticsService = new StatisticsService();
            _statisticsModel = new StatisticsDataModel();
            _monthlyStatisticsModel = new MonthlyStatisticsDataModel();
            _serviceCaller = new ServiceCaller();
            _cacheHelper = new CacheHelper();
            _serviceExecutionHelper = new ServiceHelper();
        }

        public void ExecuteMonthlyStatisticsServiceCalls()
        {
            var listOfTickets = string.Empty;

            if (_cacheHelper.GetCacheExpiryValue() == DateTime.MinValue || (DateTime.Now > _cacheHelper.GetCacheExpiryValue()))
            {
                listOfTickets = _serviceCaller.CallFreshServiceApi(_freshServiceApiTask);
                var deserializedTicketList = JsonConvert.DeserializeObject<FreshServiceTicketModel[]>(listOfTickets);
                _cacheHelper.SaveToCache(Constants.CacheKey, deserializedTicketList);
            }

            var cachedTicketList = _cacheHelper.GetFromCache<FreshServiceTicketModel[]>(Constants.CacheKey);

            var listOfGroups = _serviceCaller.CallFreshServiceGroupApi(_freshServiceAgentGroupApiTask);
            _levelOneGroupIdentifierId = TransformationHelper.FindLevelOneGroupIdentifier(listOfGroups);

            _monthlyStatisticsModel = FreshServiceMonthlyStatistics(cachedTicketList);
            var ticketIdList = TransformationHelper.GetListOfTickets(listOfTickets);
            var timeEntries = _serviceCaller.CallFreshServiceTimeEntriesApi(ticketIdList, _freshServiceTimeEntriesTask);
            
            _monthlyStatisticsModel = FreshServiceTicketHandleTimeStatistics(timeEntries);
            SaveMonthlyStatisticsData(_monthlyStatisticsModel);
        }

        public void ExecuteHourlyStatisticsServiceCalls()
        {
            var airCallTaskResult = _serviceExecutionHelper.ExecutePaginatedAirCallService(_airCallApiTask);
            var freshServiceResult = _freshServiceApiTask.Start();
            var listOfTickets = JsonConvert.DeserializeObject<FreshServiceTicketModel[]>(freshServiceResult);
            var listOfCalls = JsonConvert.DeserializeObject<AirCallModel[]>(airCallTaskResult);

            _statisticsModel = FreshServiceFilterTicketsData(listOfTickets);
            _statisticsModel = AirCallFilterCallsData(listOfCalls);

            SaveStatisticsData(_statisticsModel);
        }

        private StatisticsDataModel AirCallFilterCallsData(AirCallModel[] airCallData)
        {
            _statisticsModel = _statisticsModel.PopulateTotalMspMissedCalls(airCallData);
            _statisticsModel = _statisticsModel.PopulateTotalRegisMissedCalls(airCallData);

            return _statisticsModel;
        }

        private StatisticsDataModel FreshServiceFilterTicketsData(FreshServiceTicketModel[] ticketData)
        {
            _statisticsModel = _statisticsModel.PopulateTotalTicketsMoreThanSevenDays(ticketData);
            _statisticsModel = _statisticsModel.PopulateTotalTicketsMoreThanThirtyDays(ticketData);
            _statisticsModel = _statisticsModel.PopulateTwoDayPercentageForTickets(ticketData);

            return _statisticsModel;
        }

        private MonthlyStatisticsDataModel FreshServiceMonthlyStatistics(FreshServiceTicketModel[] ticketData)
        {
            _monthlyStatisticsModel = _monthlyStatisticsModel.PopulateTicketCountForTheMonth(ticketData);
            _monthlyStatisticsModel = _monthlyStatisticsModel.PopulateTicketsResolvedAtLevelOne(ticketData, _levelOneGroupIdentifierId);

            return _monthlyStatisticsModel;
        }

        private MonthlyStatisticsDataModel FreshServiceTicketHandleTimeStatistics(
            FreshServiceTimeEntriesModel[] timeEntryData)
        {
            _monthlyStatisticsModel = _monthlyStatisticsModel.PopulateAverageTicketHandleTimeInMinutes(timeEntryData);
            return _monthlyStatisticsModel;
        }

        private void SaveStatisticsData(StatisticsDataModel model)
        {
            _statisticsService.SaveStatisticsValues(model);
        }

        private void SaveMonthlyStatisticsData(MonthlyStatisticsDataModel monthlyStatisticsModel)
        {
            _statisticsService.SaveMonthlyStatisticsValues(monthlyStatisticsModel);
        }
    }
}

﻿using System;
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

        private StatisticsDataModel _statisticsModel;
        private MonthlyStatisticsDataModel _monthlyStatisticsModel;
        private StatisticsAgentDataModel _statisticsAgentDataModel;
        private StatisticsOrganisationDataModel _statisticsOrganisationDataModel;
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
            _statisticsAgentDataModel = new StatisticsAgentDataModel();
            _statisticsOrganisationDataModel = new StatisticsOrganisationDataModel();
        }

        public void ExecuteMonthlyStatisticsServiceCalls()
        {
            var listOfTickets = string.Empty;

            if (CacheHelper.GetCacheExpiryValue() == DateTime.MinValue || (DateTime.Now > CacheHelper.GetCacheExpiryValue()))
            {
                listOfTickets = ServiceCaller.CallFreshServiceApi(_freshServiceApiTask);
                var deserializedTicketList = JsonConvert.DeserializeObject<FreshServiceTicketModel[]>(listOfTickets);
                CacheHelper.SaveToCache(Constants.CacheKey, deserializedTicketList, DateTime.Now.AddHours(4));
            }

            var cachedTicketList = CacheHelper.GetFromCache<FreshServiceTicketModel[]>(Constants.CacheKey);

            var listOfGroups = ServiceCaller.CallFreshServiceGroupApi(_freshServiceAgentGroupApiTask);
            _levelOneGroupIdentifierId = TransformationHelper.FindLevelOneGroupIdentifier(listOfGroups);

            _monthlyStatisticsModel = FreshServiceMonthlyStatistics(cachedTicketList);
            var ticketIdList = TransformationHelper.GetListOfTickets(cachedTicketList);
            var timeEntries = ServiceCaller.CallFreshServiceTimeEntriesApi(ticketIdList, _freshServiceTimeEntriesTask);
            
            _monthlyStatisticsModel = FreshServiceTicketHandleTimeStatistics(timeEntries);
            SaveMonthlyStatisticsData(_monthlyStatisticsModel);
        }

        public void ExecuteHourlyStatisticsServiceCalls()
        {
            var airCallTaskResult = ServiceHelper.ExecutePaginatedAirCallService(_airCallApiTask);
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


        private StatisticsAgentDataModel FreshServiceAgentStatistics(FreshServiceTicketModel[] ticketData)
        {
            _statisticsAgentDataModel = _statisticsAgentDataModel.TotalTicketsOpen(ticketData);

            return _statisticsAgentDataModel;
        }


        private StatisticsOrganisationDataModel FreshServiceOrganisationStatistics(FreshServiceTicketModel[] ticketData)
        {
            _statisticsOrganisationDataModel = _statisticsOrganisationDataModel.TotalTicketsOpen(ticketData);

            return _statisticsOrganisationDataModel;
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

        private void SaveAgentStatisticsData(StatisticsAgentDataModel agentDataModel)
        {
            _statisticsService.SaveAgentStatisticsData(agentDataModel);
        }

        private void SaveOrganisationStatisticsData(StatisticsOrganisationDataModel organistionDataModel)
        {
            _statisticsService.SaveOrganisationStatisticsData(organistionDataModel);
        }
    }
}

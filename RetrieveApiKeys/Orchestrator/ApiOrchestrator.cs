using System.Collections.Generic;
using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;
using F1Solutions.InfrastructureStatistics.Services;
using F1Solutions.InfrastructureStatistics.Services.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator
{
    public class ApiOrchestrator
    {
        private readonly AirCallApiTask _airCallApiTask;
        private readonly FreshServiceApiTask _freshServiceApiTask;
        private readonly FreshServiceAgentGroupApiTask _freshServiceAgentGroupApiTask;
        private readonly StatisticsService _statisticsService;
        private StatisticsDataModel _statisticsModel;
        private MonthlyStatisticsDataModel _monthlyStatisticsModel;

        public ApiOrchestrator()
        {
            _airCallApiTask = new AirCallApiTask();
            _freshServiceApiTask = new FreshServiceApiTask();
            _freshServiceAgentGroupApiTask = new FreshServiceAgentGroupApiTask();
            _statisticsService = new StatisticsService();
            _statisticsModel = new StatisticsDataModel();
            _monthlyStatisticsModel = new MonthlyStatisticsDataModel();
        }

        public void Start()
        {
            var airCallResult = _airCallApiTask.Start();
            var freshServiceResult = _freshServiceApiTask.Start();
            var freshServiceAgentGroupApiTaskResult = _freshServiceAgentGroupApiTask.Start();

            var listOfCalls = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
            var listOfGroups = JsonConvert.DeserializeObject<FreshServiceAgentGroupModel>(freshServiceAgentGroupApiTaskResult);
            var listOfTickets = JsonConvert.DeserializeObject<FreshServiceTicketModel[]>(freshServiceResult);

            //listOfTickets.LevelOneGroup = TransformationHelper.LevelOneGroupIdentifier(listOfGroups);
            _statisticsModel = AirCallFilterCallsData(listOfCalls);
            _statisticsModel = FreshServiceFilterTicketsData(listOfTickets);
            //_monthlyStatisticsModel = FreshServiceMonthlyStatistics(listOfTickets);

            Save(_statisticsModel, _monthlyStatisticsModel);
        }

        public void Stop()
        {
        }

        private StatisticsDataModel AirCallFilterCallsData(AirCallModel data)
        {
            _statisticsModel = _statisticsModel.PopulateTotalMspMissedCalls(data);
            _statisticsModel = _statisticsModel.PopulateTotalRegisMissedCalls(data);

            return _statisticsModel;
        }

        private StatisticsDataModel FreshServiceFilterTicketsData(FreshServiceTicketModel[] data)
        {
            _statisticsModel = _statisticsModel.PopulateTotalTicketsMoreThanSevenDays(data);
            _statisticsModel = _statisticsModel.PopulateTotalTicketsMoreThanThirtyDays(data);

            return _statisticsModel;
        }

        private MonthlyStatisticsDataModel FreshServiceMonthlyStatistics(FreshServiceTicketModel[] data)
        {
            _monthlyStatisticsModel = _monthlyStatisticsModel.PopulateTicketCountForTheMonth(data);
            _monthlyStatisticsModel = _monthlyStatisticsModel.PopulateAverageTicketHandleTimeInMinutes(data);
            _monthlyStatisticsModel = _monthlyStatisticsModel.PopulateTicketsResolvedAtLevelOne(data);

            return _monthlyStatisticsModel;
        }

        private void Save(StatisticsDataModel model, MonthlyStatisticsDataModel monthlyStatisticsModel)
        {
            _statisticsService.SaveStatisticsValues(model);
            _statisticsService.SaveMonthlyStatisticsValues(monthlyStatisticsModel);
        }
    }
}

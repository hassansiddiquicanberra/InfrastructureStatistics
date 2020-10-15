using System.Collections.Generic;
using System.Text;
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
        private readonly FreshServiceTimeEntriesTask _freshServiceTimeEntriesTask;
        private readonly FreshServiceAgentGroupApiTask _freshServiceAgentGroupApiTask;
        private readonly StatisticsService _statisticsService;
        private StatisticsDataModel _statisticsModel;
        private MonthlyStatisticsDataModel _monthlyStatisticsModel;

        public ApiOrchestrator()
        {
            _airCallApiTask = new AirCallApiTask();
            _freshServiceApiTask = new FreshServiceApiTask();
            _freshServiceAgentGroupApiTask = new FreshServiceAgentGroupApiTask();
            _freshServiceTimeEntriesTask = new FreshServiceTimeEntriesTask();
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

            var freshServiceTimeEntriesList = ExecuteFreshServiceTimeEntriesTask(listOfTickets);
            var timeEntries = JsonConvert.DeserializeObject<FreshServiceTimeEntriesModel[]>(freshServiceTimeEntriesList);

            var levelOneGroupIdentifier = TransformationHelper.LevelOneGroupIdentifier(listOfGroups);
            _statisticsModel = AirCallFilterCallsData(listOfCalls);
            _statisticsModel = FreshServiceFilterTicketsData(listOfTickets);
            _monthlyStatisticsModel = FreshServiceMonthlyStatistics(listOfTickets);

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

        private string ExecuteFreshServiceTimeEntriesTask(FreshServiceTicketModel[] tickets)
        {
            var responseBodyList = new List<string>();
            var ticketStringBuilder = new StringBuilder();

            foreach (var ticket in tickets)
            {
                foreach (var individualTicket in ticket.Tickets)
                {
                    var ticketId = individualTicket.Id;
                    if (!string.IsNullOrEmpty(ticketId))
                    {
                        var url = ConfigHelper.FreshServiceForTicketsUri + "/" + ticketId + "/time_entries";
                        var freshServiceAgentGroupApiTaskResult = _freshServiceTimeEntriesTask.Start(ticketId);
                        responseBodyList.Add(freshServiceAgentGroupApiTaskResult);
                    }
                }
            }

            foreach (var value in responseBodyList)
            {
                ticketStringBuilder.Append(value);
            }


            var mergedJsonValues = ConfigHelper.MergeJsonString(responseBodyList);

            return mergedJsonValues;
        }
    }
}

using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
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
        private readonly FreshServiceAgentGroupApiTask _freshServiceAgentGroupApiTask;
        private readonly StatisticsService _statisticsService;
        private DataModel _model;

        public ApiOrchestrator()
        {
            _airCallApiTask = new AirCallApiTask();
            _freshServiceApiTask = new FreshServiceApiTask();
            _freshServiceAgentGroupApiTask = new FreshServiceAgentGroupApiTask();
            _statisticsService = new StatisticsService();
            _model = new DataModel();
        }

        public void Start()
        {
            var airCallResult = _airCallApiTask.Start();
            var freshServiceResult = _freshServiceApiTask.Start();
            var freshServiceAgentGroupApiTaskResult = _freshServiceAgentGroupApiTask.Start();

            var listOfCalls = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
            var listOfGroups = JsonConvert.DeserializeObject<FreshServiceAgentGroupModel>(freshServiceAgentGroupApiTaskResult);
            var listOfTickets = JsonConvert.DeserializeObject<FreshServiceTicketModel>(freshServiceResult);

            listOfTickets.LevelOneGroup = TransformationHelper.LevelOneGroupIdentifier(listOfGroups);
            _model = AirCallFilterCallsData(listOfCalls);
            _model = FreshServiceFilterTicketsData(listOfTickets);
            _model = FreshServiceMonthlyStatistics(listOfTickets);

            Save(_model);
        }

        public void Stop()
        {
        }

        private DataModel AirCallFilterCallsData(AirCallModel data)
        {
            _model = _model.PopulateTotalMspMissedCalls(data);
            _model = _model.PopulateTotalRegisMissedCalls(data);

            return _model;
        }

        private DataModel FreshServiceFilterTicketsData(FreshServiceTicketModel data)
        {
            _model = _model.PopulateTotalTicketsMoreThanSevenDays(data);
            _model = _model.PopulateTotalTicketsMoreThanThirtyDays(data);

            return _model;
        }

        private DataModel FreshServiceMonthlyStatistics(FreshServiceTicketModel data)
        {
            _model = _model.PopulateTicketCountForTheMonth(data);
            _model = _model.PopulateAverageTicketHandleTimeInMinutes(data);
            _model = _model.PopulateTicketsResolvedAtLevelOne(data);

            return _model;
        }

        private void Save(DataModel model)
        {
            _statisticsService.SaveStatisticsValues(model);
        }
    }
}

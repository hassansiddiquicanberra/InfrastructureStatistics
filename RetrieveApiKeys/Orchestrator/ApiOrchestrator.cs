using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.Services;
using F1Solutions.InfrastructureStatistics.Services.Models;
using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator
{
    public class ApiOrchestrator
    {
        private readonly AirCallApiTask _airCallApiTask;
        private readonly FreshServiceApiTask _freshServiceApiTask;
        private readonly StatisticsService _statisticsService;
        private DataModel _model;

        public ApiOrchestrator()
        {
            _airCallApiTask = new AirCallApiTask();
            _freshServiceApiTask = new FreshServiceApiTask();
            _statisticsService = new StatisticsService();
            _model = new DataModel();
        }

        public void Start()
        {
            var airCallResult = _airCallApiTask.Start();
            var freshServiceResult = _freshServiceApiTask.Start();
            var listOfCalls = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
            var listOfTickets = JsonConvert.DeserializeObject<FreshServiceTicketModel[]>(freshServiceResult);

            _model = AirCallFilterCallsData(listOfCalls);
            _model = FreshServiceFilterTicketsData(listOfTickets);

            //Save(model);
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

        private DataModel FreshServiceFilterTicketsData(FreshServiceTicketModel[] data)
        {
            _model = _model.PopulateTotalTicketsMoreThanSevenDays(data);
            _model = _model.PopulateTotalTicketsMoreThanThirtyDays(data);

            return _model;
        }

        private void Save(DataModel model)
        {
            _statisticsService.SaveStatisticsValues(model);
        }
    }
}

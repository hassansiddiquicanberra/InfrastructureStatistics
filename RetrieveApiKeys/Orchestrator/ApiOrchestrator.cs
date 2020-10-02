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

        public ApiOrchestrator()
        {
            _airCallApiTask = new AirCallApiTask();
            _freshServiceApiTask = new FreshServiceApiTask();
            _statisticsService = new StatisticsService();
        }

        public void Start()
        {
            var airCallResult = _airCallApiTask.Start();
            var freshServiceResult = _freshServiceApiTask.Start();
            var listOfCalls = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
            var listOfTickets = JsonConvert.DeserializeObject<FreshServiceTicketModel[]>(freshServiceResult);

            var model = AirCallFilterCallsData(listOfCalls);
            model = FreshServiceFilterTicketsData(listOfTickets);

            //Save(model);
        }

        public void Stop()
        {
        }

        private DataModel AirCallFilterCallsData(AirCallModel data)
        {
            var model = new DataModel();
            model = model.PopulateTotalMspMissedCalls(data);
            model = model.PopulateTotalRegisMissedCalls(data);

            return model;
        }

        private DataModel FreshServiceFilterTicketsData(FreshServiceTicketModel[] data)
        {
            var model = new DataModel();
            model = model.PopulateTotalTicketsMoreThanSevenDays(data);
            model = model.PopulateTotalTicketsMoreThanThirtyDays(data);

            return model;
        }

        private void Save(DataModel model)
        {
            _statisticsService.SaveStatisticsValues(model);
        }
    }
}

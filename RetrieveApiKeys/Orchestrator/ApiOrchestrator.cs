using System.Collections.Generic;
using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel;
using F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions;
using F1Solutions.InfrastructureStatistics.Services;
using F1Solutions.InfrastructureStatistics.Services.Models;
using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator
{
    public class ApiOrchestrator
    {
        private readonly AirCallApiTask _airCallApiTask;
        private readonly FreshServiceApiTask _freshServiceApiTask;
        private readonly FreshServiceDepartmentTask _freshServiceDepartmentTask;
        private readonly FreshServiceRequesterTask _freshServiceRequesterTask;
        private readonly StatisticsService _statisticsService;

        public ApiOrchestrator()
        {
            _airCallApiTask = new AirCallApiTask();
            _freshServiceApiTask = new FreshServiceApiTask();
            _statisticsService = new StatisticsService();
            _freshServiceDepartmentTask = new FreshServiceDepartmentTask();
            _freshServiceRequesterTask = new FreshServiceRequesterTask();
        }

        public void ExecuteServiceForCalls()
        {
            var stringListOfCalls = ServiceHelper.ExecutePaginatedAirCallService(_airCallApiTask);
            var jsonDeserializedCalls = JsonConvert.DeserializeObject<AirCallModel[]>(stringListOfCalls);
            var callDomainObjects = PopulateModelWithCallObjects(jsonDeserializedCalls);

            SaveCalls(callDomainObjects);
        }

        //Call the department and requester service api and retrieve the values...only the necessary ones

        public void ExecuteApiServiceCallForRequesters()
        {
            var stringListOfRequesters = ServiceCaller.CallFreshServiceRequester(_freshServiceRequesterTask);
            var jsonDeserialisedRequesters = JsonConvert.DeserializeObject<FreshServiceRequesterModel[]>(stringListOfRequesters);
            var a = jsonDeserialisedRequesters;
            //var ticketDomainObjects = PopulateModelWithTicketObjects(jsonDeserializedTickets);
        }

        public void ExecuteApiServiceCallForDepartments()
        {
            var stringListOfDepartments = ServiceCaller.CallFreshServiceDepartment(_freshServiceDepartmentTask);
            var jsonDeserialisedDepartments = JsonConvert.DeserializeObject<FreshServiceDepartmentModel[]>(stringListOfDepartments);
            var a = jsonDeserialisedDepartments;
            //var ticketDomainObjects = PopulateModelWithTicketObjects(jsonDeserializedTickets);
        }

        public void ExecuteApiServiceCallForTickets()
        {
            var stringListOfTickets = ServiceCaller.CallFreshServiceApi(_freshServiceApiTask);
            var jsonDeserializedTickets = JsonConvert.DeserializeObject<FreshServiceTicketModel[]>(stringListOfTickets);
            var ticketDomainObjects = PopulateModelWithTicketObjects(jsonDeserializedTickets);

            SaveTickets(ticketDomainObjects);
        }

        private List<CallModel> PopulateModelWithCallObjects(AirCallModel[] callData)
        {
            return AirCallModelExtensions.PopulateCallData(callData);
        }

        private List<TicketModel> PopulateModelWithTicketObjects(FreshServiceTicketModel[] callData)
        {
            return TicketModelExtensions.PopulateTicketData(callData);
        }

        private void SaveTickets(List<TicketModel> tickets)
        {
            foreach (var ticket in tickets)
            {
                _statisticsService.SaveTicket(ticket);
            }
        }

        private void SaveCalls(List<CallModel> calls)
        {
            foreach (var call in calls)
            {
                _statisticsService.SaveCall(call);
            }
        }
    }
}

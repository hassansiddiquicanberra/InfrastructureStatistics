﻿using System.Collections.Generic;
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
        private readonly StatisticsService _statisticsService;

        private MonthlyStatisticsDataModel _monthlyStatisticsModel;
        private TicketModel _ticketModel;
        private CallModel _callModel;

        public ApiOrchestrator()
        {
            _airCallApiTask = new AirCallApiTask();
            _freshServiceApiTask = new FreshServiceApiTask();
            _statisticsService = new StatisticsService();
            _monthlyStatisticsModel = new MonthlyStatisticsDataModel();
            _ticketModel = new TicketModel();
            _callModel = new CallModel();
        }

        public void ExecuteServiceForCalls()
        {
            var stringListOfCalls = ServiceHelper.ExecutePaginatedAirCallService(_airCallApiTask);
            var jsonDeserializedCalls = JsonConvert.DeserializeObject<AirCallModel[]>(stringListOfCalls);
            var callDomainObjects = PopulateModelWithCallObjects(jsonDeserializedCalls);

            SaveCalls(callDomainObjects);
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


        //private MonthlyStatisticsDataModel FreshServiceTicketHandleTimeStatistics(FreshServiceTimeEntriesModel[] timeEntryData)
        //{
        //    _monthlyStatisticsModel = _monthlyStatisticsModel.PopulateAverageTicketHandleTimeInMinutes(timeEntryData);
        //    return _monthlyStatisticsModel;
        //}


        private void SaveTickets(List<TicketModel> tickets)
        {
            foreach (var ticket in tickets)
            {
                _statisticsService.SaveTicket(ticket);
            }
        }

        private void SaveCalls(List<CallModel> calls)
        { 
            //check for inserts first, then updates as well
            foreach (var call in calls)
            {
                _statisticsService.SaveCall(call);
            }
        }

    }
}

﻿using System.Collections.Generic;
using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class ServiceExecutionHelper
    {
        public static string ExecutePaginatedAirCallService(AirCallApiTask _airCallApiTask)
        {
            var airCallModelList = new List<string>();
            var airCallResult = _airCallApiTask.Start();
            airCallModelList.Add(airCallResult);

            var listOfCalls = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
            var airCallNextPageUrl = listOfCalls.Meta.NextPageLink;

            do
            {
                if (!string.IsNullOrEmpty(airCallNextPageUrl))
                {
                    airCallResult = _airCallApiTask.Start(null, airCallNextPageUrl);
                    airCallModelList.Add(airCallResult);
                    var deserializedObject = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
                    airCallNextPageUrl = deserializedObject.Meta.NextPageLink;
                }

                //} while (!string.IsNullOrEmpty(airCallNextPageUrl));
            } while (airCallNextPageUrl == "https://api.aircall.io/v1/calls?order=asc&page=15&per_page=20");

            return JsonHelper.MergeJsonStringValues(airCallModelList);
        }

        public static string ExecuteFreshServiceTimeEntriesForEachTicket(FreshServiceTicketModel[] tickets, FreshServiceTimeEntriesTask _freshServiceTimeEntriesTask)
        {
            var responseBodyList = new List<string>();

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

            return JsonHelper.MergeJsonStringValues(responseBodyList);
        }
    }
}

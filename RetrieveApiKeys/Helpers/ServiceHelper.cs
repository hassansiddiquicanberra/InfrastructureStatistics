﻿using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class ServiceHelper
    {
        public static string ExecutePaginatedAirCallService(AirCallApiTask airCallApiTask)
        {
            var airCallModelList = new List<string>();
            var airCallResult = airCallApiTask.Start();
            airCallModelList.Add(airCallResult);

            var listOfCalls = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
            var airCallNextPageUrl = listOfCalls.Meta.NextPageLink;
            
            do
            {
                if (!string.IsNullOrEmpty(airCallNextPageUrl))
                {
                    airCallResult = airCallApiTask.Start(null, airCallNextPageUrl);
                    airCallModelList.Add(airCallResult);
                    var deserializedObject = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
                    airCallNextPageUrl = deserializedObject.Meta.NextPageLink;
                }

            } while (!string.IsNullOrEmpty(airCallNextPageUrl));

            return JsonHelper.MergeJsonStringValues(airCallModelList);
        }

        public static string ExecuteFreshServiceTimeEntriesForEachTicket(List<string> ticketIds, FreshServiceTimeEntriesTask freshServiceTimeEntriesTask)
        {
            var responseBodyList = new List<string>();

            foreach (var ticketId in ticketIds)
            {
                if (!string.IsNullOrEmpty(ticketId))
                {
                    var freshServiceTimeEntriesServiceResult = ServiceCaller.ExecuteFreshServiceTimeEntriesApiService(freshServiceTimeEntriesTask,ticketId);
                    if (!string.IsNullOrEmpty(freshServiceTimeEntriesServiceResult) && freshServiceTimeEntriesServiceResult.Any())
                    {
                        var deserializedTimeEntries = JsonConvert.DeserializeObject<FreshServiceTimeEntriesModel>(freshServiceTimeEntriesServiceResult);

                        if (deserializedTimeEntries != null && deserializedTimeEntries.Time_Entries.Any())
                        {
                           responseBodyList.Add(freshServiceTimeEntriesServiceResult);
                        }
                    }
                }
            }

            return JsonHelper.MergeJsonStringValues(responseBodyList);
        }
    }
}

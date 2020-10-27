using System;
using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;
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

            } while (!string.IsNullOrEmpty(airCallNextPageUrl));

            return JsonHelper.MergeJsonStringValues(airCallModelList);
        }

        public static string ExecuteFreshServiceTimeEntriesForEachTicket(List<string> ticketIds, FreshServiceTimeEntriesTask freshServiceTimeEntriesTask)
        {
            var ticketEntryId = 0;
            FreshServiceTimeEntriesModel deserialisedTimeEntries = null;
            foreach (var ticketId in ticketIds)
            {
                if (!string.IsNullOrEmpty(ticketId))
                {
                    var url = ConfigHelper.FreshServiceForTicketsUri + "/" + ticketId + "/time_entries";
                    var freshServiceTimeEntriesServiceResult = freshServiceTimeEntriesTask.Start(ticketId);
                    if (!string.IsNullOrEmpty(freshServiceTimeEntriesServiceResult) && freshServiceTimeEntriesServiceResult.Any())
                    {
                        deserialisedTimeEntries = JsonConvert.DeserializeObject<FreshServiceTimeEntriesModel>(freshServiceTimeEntriesServiceResult);

                        if (deserialisedTimeEntries != null && deserialisedTimeEntries.Time_Entries.Any())
                        {
                            List<TimeEntry> cachedTimeEntries = deserialisedTimeEntries.Time_Entries.Select(x =>
                                new TimeEntry()
                                {
                                    CreatedAt = x.CreatedAt,
                                    Billable = x.billable,
                                    OwnerId = x.AgentId,
                                    TimeSpent = x.TimeSpent,
                                    UpdatedAt = x.UpdatedAt
                                }).ToList();

                            foreach (var entry in cachedTimeEntries)
                            {
                                CacheHelper.ModifyTimeEntriesInCache(Constants.CacheKey, entry,
                                    DateTime.Now.AddHours(Constants.CacheExpirationTimeInHours), ticketEntryId, true);
                                ticketEntryId++;
                            }
                        }
                    }
                }
            }

            return "";
        }
    }
}

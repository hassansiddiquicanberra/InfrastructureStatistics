using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
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
            var timeEntriesList = new List<CachedTimeEntries>();
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

                            var cachedTimeEntries = deserialisedTimeEntries.Time_Entries.Select(x =>
                                new TimeEntry()
                                {
                                    CreatedAt = x.CreatedAt,
                                    Billable = x.billable,
                                    OwnerId = x.AgentId,
                                    TimeSpent = x.TimeSpent,
                                    UpdatedAt = x.UpdatedAt
                                }).SingleOrDefault();

                            //get the existing cache and modify the root object which is cachemodel and add this new time entry object there
                            CacheHelper.SaveTimeEntriesToCache(Constants.CacheKey, cachedTimeEntries,DateTime.Now.AddHours(Constants.CacheExpirationTimeInHours), true);
                        }
                    }
                }
            }


            var a = CacheHelper.GetFromCache<List<CachedTimeEntries>>("ListOfCachedTimeEntries");

            return "";
            //return JsonHelper.MergeJsonStringValues(freshServiceTimeEntriesServiceResult);
        }
    }
}

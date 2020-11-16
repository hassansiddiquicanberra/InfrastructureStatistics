using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
using F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel;
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
                    if (airCallResult != null)
                    {
                        airCallModelList.Add(airCallResult);
                        var deserializedObject = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
                        airCallNextPageUrl = deserializedObject.Meta.NextPageLink;
                    }
                }

            } while (airCallNextPageUrl != "https://api.aircall.io/v1/calls?order=asc&page=23&per_page=20");
            //} while (!string.IsNullOrEmpty(airCallNextPageUrl));

            return JsonHelper.MergeJsonStringValues(airCallModelList);
        }
    }
}

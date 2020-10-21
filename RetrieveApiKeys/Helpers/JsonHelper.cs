using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class JsonHelper
    {
        public static string IndentJsonValues(List<string> stringList)
        {
            var mergeSettings = new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            };

            var jArrayItems = new JArray();

            foreach (var item in stringList)
            {
                jArrayItems.Add((JObject)JsonConvert.DeserializeObject(item));
            }

            return jArrayItems.ToString();
        }

        public static string MergeJsonStringValues(List<string> listOfValues)
        {
            var mergedTimeEntriesJsonValues = IndentJsonValues(listOfValues);

            return mergedTimeEntriesJsonValues;
        }
    }
}

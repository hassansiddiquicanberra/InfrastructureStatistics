﻿using System;
using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class ConfigHelper
    {
        public static int ServiceTimer => GetSetting<int>("ServiceTimer");
        public static string AirCallForCallUri => GetSetting<string>("AirCallForCallUri");
        public static string FreshServiceForTicketsUri => GetSetting<string>("FreshServiceForTicketsUri");

        public static string FreshServiceForAgentGroupsUri => GetSetting<string>("FreshServiceForAgentGroupsUri");
        public static string FreshServiceApiKey => GetSetting<string>("FreshServiceApiKey");
        public static string AirCallApiId => GetSetting<string>("AirCallApiId");
        public static string AirCallApiToken => GetSetting<string>("AirCallApiToken");
        public static string MspNumber => GetSetting<string>("MspNumber");
        public static string RegisNumber => GetSetting<string>("RegisNumber");

        public static string FirstLevelHelpDesk => GetSetting<string>("FirstLevelHelpDesk");

        public static T GetSetting<T>(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public static string MergeJsonString(List<string> stringList)
        {
            var mergeSettings = new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            };

            //find the length of 

            var length = stringList.Count;

            var j1 = (JObject)JsonConvert.DeserializeObject(stringList[0]);
            var j2 = (JObject)JsonConvert.DeserializeObject(stringList[1]);
            var j3 = (JObject)JsonConvert.DeserializeObject(stringList[2]);


            var jArray = new JArray(j1, j2, j3);
            return jArray.ToString();
        }
    }
}

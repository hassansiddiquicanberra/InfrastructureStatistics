using System;
using System.Configuration;

namespace RetrieveApiKeys.Helpers
{
    public static class ConfigHelper
    {
        public static int ServiceTimer => GetSetting<int>("ServiceTimer");
        public static string AirCallForCallUri => GetSetting<string>("AirCallForCallUri");
        public static string FreshServiceForTicketsUri => GetSetting<string>("FreshServiceForTicketsUri");
        public static string FreshServiceApiKey => GetSetting<string>("FreshServiceApiKey");
        public static string AirCallApiId => GetSetting<string>("AirCallApiId");
        public static string AirCallApiToken => GetSetting<string>("AirCallApiToken");
        public static string MspNumber => GetSetting<string>("MspNumber");
        public static string RegisNumber => GetSetting<string>("RegisNumber");

        public static T GetSetting<T>( string key, bool useDefault = true)
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
    }
}

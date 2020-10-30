using System;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class TransformationHelper
    {

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch<- what this means :*)
            var dateTimeValue = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dateTimeValue = dateTimeValue.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTimeValue;
        }
    }
}





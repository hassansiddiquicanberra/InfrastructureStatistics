using System.Net.Http;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask
{
    public class AirCallApiTask : BaseApiTask
    {
        public AirCallApiTask()
        {
            Id = ConfigHelper.AirCallApiId;
            Token = ConfigHelper.AirCallApiToken;
        }

        public virtual string Start(string url)
        {
            var airCallResponse = SendRequest(ConfigHelper.AirCallForCallUri, ConfigHelper.AirCallForCallUri, HttpMethod.Get);
            return airCallResponse.Result;
        }
    }
}
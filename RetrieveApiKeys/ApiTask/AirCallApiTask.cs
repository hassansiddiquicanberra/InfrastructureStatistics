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
        
        public override string Start()
        {
            var airCallResponse = SendRequest(ConfigHelper.AirCallForCallUri, HttpMethod.Get);
            return airCallResponse.Result;
        }
    }
}

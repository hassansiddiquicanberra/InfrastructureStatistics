using System.Configuration;
using System.Net.Http;
using F1Solutions.InfrastructureStatictics.ApiCalls.Helpers;

namespace F1Solutions.InfrastructureStatictics.ApiCalls.ApiTask
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
            var airCallResponse = SendRequest(ConfigurationManager.AppSettings["AirCallForCallUri"], HttpMethod.Get);
            return airCallResponse.Result;
        }
    }
}

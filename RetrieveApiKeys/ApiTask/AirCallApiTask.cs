using System.Net.Http;
using System.Configuration;
using RetrieveApiKeys.Helpers;

namespace RetrieveApiKeys.ApiTask
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

using System.Configuration;
using System.Net.Http;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask
{
    public class FreshServiceApiTask : BaseApiTask
    {
        public FreshServiceApiTask()
        {
            Id = ConfigHelper.FreshServiceApiKey;
        }
        
        public override string Start()
        {
            var freshServiceResponse = SendRequest(ConfigurationManager.AppSettings["FreshServiceForTicketsUri"], HttpMethod.Get);
            return freshServiceResponse.Result;
        }
    }
}

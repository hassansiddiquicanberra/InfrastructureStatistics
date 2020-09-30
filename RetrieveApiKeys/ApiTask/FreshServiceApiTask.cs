using System.Configuration;
using System.Net.Http;
using RetrieveApiKeys.Helpers;

namespace RetrieveApiKeys.ApiTask
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

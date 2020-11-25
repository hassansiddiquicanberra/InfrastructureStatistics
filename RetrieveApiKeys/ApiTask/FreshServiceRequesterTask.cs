using System.Net.Http;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask
{
    public class FreshServiceRequesterTask : BaseApiTask
    {
        public FreshServiceRequesterTask()
        {
            Id = ConfigHelper.FreshServiceApiKey;
        }

        public override string Start(string ticketId = null, string url = null)
        {
            var freshServiceResponse = GetAllAsync(ConfigHelper.FreshServiceForRequesterUri, HttpMethod.Get);
            
            return freshServiceResponse.Result;
        }
    }
}

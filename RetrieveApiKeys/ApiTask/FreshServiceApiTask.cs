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

        public override string Start(string ticketId = null, string url = null)
        {
            var freshServiceResponse = GetAllTicketsAsync(ConfigHelper.FreshServiceForTicketsUri, HttpMethod.Get);

            return freshServiceResponse.Result;
        }
    }
}
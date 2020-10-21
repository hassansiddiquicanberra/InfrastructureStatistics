using System.Net.Http;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask
{
    public class FreshServiceTimeEntriesTask : BaseApiTask
    {
        public FreshServiceTimeEntriesTask()
        {
            Id = ConfigHelper.FreshServiceApiKey;
        }

        public override string Start(string ticketId = null, string url = null)
        {
            var freshServiceResponse = GetAllTimeEntriesAsync(ticketId, ConfigHelper.FreshServiceForTicketsUri, HttpMethod.Get);

            return freshServiceResponse.Result;
        }
    }
}
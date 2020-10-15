using System.Collections.Generic;
using System.Net.Http;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask
{
    public class FreshServiceAgentGroupApiTask : BaseApiTask
    {
        public FreshServiceAgentGroupApiTask()
        {
            Id = ConfigHelper.FreshServiceApiKey;
        }

        public override string Start(string ticketId = null, string url = null)
        {
            var freshServiceResponse = SendRequest(string.Empty,ConfigHelper.FreshServiceForAgentGroupsUri, HttpMethod.Get);
            return freshServiceResponse.Result;
        }
    }
}

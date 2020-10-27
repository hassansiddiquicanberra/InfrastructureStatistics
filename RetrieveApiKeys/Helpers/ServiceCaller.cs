using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public class ServiceCaller
    {
        private readonly ServiceExecutionHelper _serviceExecutionHelper;
        public ServiceCaller()
        {
            _serviceExecutionHelper = new ServiceExecutionHelper();
        }
        public string CallFreshServiceApi(FreshServiceApiTask freshServiceApiTask)
        {

            return freshServiceApiTask.Start();
        }

        public FreshServiceAgentGroupModel CallFreshServiceGroupApi(FreshServiceAgentGroupApiTask freshServiceAgentGroupApiTask)
        {

            var freshServiceAgentGroupApiTaskResult = freshServiceAgentGroupApiTask.Start();
            return  JsonConvert.DeserializeObject<FreshServiceAgentGroupModel>(freshServiceAgentGroupApiTaskResult);
        }

        public FreshServiceTimeEntriesModel[] CallFreshServiceTimeEntriesApi(List<string> ticketIdList, FreshServiceTimeEntriesTask freshServiceTimeEntriesTask)
        {
            var freshServiceTimeEntriesList = _serviceExecutionHelper.ExecuteFreshServiceTimeEntriesForEachTicket(ticketIdList, freshServiceTimeEntriesTask);
            return JsonConvert.DeserializeObject<FreshServiceTimeEntriesModel[]>(freshServiceTimeEntriesList);
        }
    }
}

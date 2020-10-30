using System.Collections.Generic;
using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
using F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class ServiceCaller
    {
        public static string CallFreshServiceApi(FreshServiceApiTask freshServiceApiTask)
        {

            return freshServiceApiTask.Start();
        }

        public static FreshServiceAgentGroupModel CallFreshServiceGroupApi(FreshServiceAgentGroupApiTask freshServiceAgentGroupApiTask)
        {

            var freshServiceAgentGroupApiTaskResult = freshServiceAgentGroupApiTask.Start();
            return  JsonConvert.DeserializeObject<FreshServiceAgentGroupModel>(freshServiceAgentGroupApiTaskResult);
        }

        public static FreshServiceTimeEntriesModel[] CallFreshServiceTimeEntriesApi(List<string> ticketIdList, FreshServiceTimeEntriesTask freshServiceTimeEntriesTask)
        {
            var freshServiceTimeEntriesList = ServiceHelper.ExecuteFreshServiceTimeEntriesForEachTicket(ticketIdList, freshServiceTimeEntriesTask);
            return JsonConvert.DeserializeObject<FreshServiceTimeEntriesModel[]>(freshServiceTimeEntriesList);
        }

        public static string ExecuteFreshServiceTimeEntriesApiService(FreshServiceTimeEntriesTask freshServiceTimeEntriesTask, string ticketId)
        {
            return freshServiceTimeEntriesTask.Start(ticketId);
        }
    }
}

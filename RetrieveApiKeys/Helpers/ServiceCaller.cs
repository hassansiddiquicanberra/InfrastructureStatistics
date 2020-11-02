﻿using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class ServiceCaller
    {
        public static string CallFreshServiceApi(FreshServiceApiTask freshServiceApiTask)
        {

            return freshServiceApiTask.Start();
        }

        public static string ExecuteFreshServiceTimeEntriesApiService(FreshServiceTimeEntriesTask freshServiceTimeEntriesTask, string ticketId)
        {
            return freshServiceTimeEntriesTask.Start(ticketId);
        }
    }
}

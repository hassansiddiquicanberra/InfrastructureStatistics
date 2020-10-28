using System;
using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions
{
    public static class FreshServiceAgentModelExtensions
    {
        public static StatisticsAgentDataModel TotalTicketsOpen(this StatisticsAgentDataModel model, FreshServiceTicketModel[] data)
        {
            //foreach (var freshServiceTicketModel in data)
            //{
            //    freshServiceTicketModel.Tickets.Where(x=>x.)
            //}

            //return model;

            return new StatisticsAgentDataModel();
        }


    }
}

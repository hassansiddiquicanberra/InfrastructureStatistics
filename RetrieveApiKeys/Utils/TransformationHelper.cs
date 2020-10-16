using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Utils
{
    public static class TransformationHelper
    {
        public static string FindLevelOneGroupIdentifier(FreshServiceAgentGroupModel model)
        {
            var levelOneGroupId = string.Empty;

            var levelOneGroupRecord = model.Groups.FirstOrDefault(x => x.Name.Contains(ConfigHelper.FirstLevelHelpDesk));
            if (levelOneGroupRecord != null)
            {
                levelOneGroupId = levelOneGroupRecord.Id;
            }

            return levelOneGroupId;
        }

        public static string ExecutePaginatedAirCallService(AirCallApiTask _airCallApiTask)
        {
            var airCallModelList = new List<string>();
            var airCallResult = _airCallApiTask.Start();
            airCallModelList.Add(airCallResult);

            var listOfCalls = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
            var airCallNextPageUrl = listOfCalls.Meta.NextPageLink;

            do
            {
                if (!string.IsNullOrEmpty(airCallNextPageUrl))
                {
                    airCallResult = _airCallApiTask.Start(null, airCallNextPageUrl);
                    airCallModelList.Add(airCallResult);
                    var deserializedObject = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
                    airCallNextPageUrl = deserializedObject.Meta.NextPageLink;
                }
            } while (!string.IsNullOrEmpty(airCallNextPageUrl));

            return JsonHelper.MergeJsonStringValues(airCallModelList);
        }

        public static string ExecuteFreshServiceTimeEntriesForEachTicket(FreshServiceTicketModel[] tickets, FreshServiceTimeEntriesTask _freshServiceTimeEntriesTask)
        {
            var responseBodyList = new List<string>(); 

            foreach (var ticket in tickets)
            {
                foreach (var individualTicket in ticket.Tickets)
                {
                    var ticketId = individualTicket.Id;
                    if (!string.IsNullOrEmpty(ticketId))
                    {
                        var url = ConfigHelper.FreshServiceForTicketsUri + "/" + ticketId + "/time_entries";
                        var freshServiceAgentGroupApiTaskResult = _freshServiceTimeEntriesTask.Start(ticketId);
                        responseBodyList.Add(freshServiceAgentGroupApiTaskResult);
                    }
                }
            }

            return JsonHelper.MergeJsonStringValues(responseBodyList);
        }

       
    }
}




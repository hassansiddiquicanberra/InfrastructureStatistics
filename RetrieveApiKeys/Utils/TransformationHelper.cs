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
        public static string LevelOneGroupIdentifier(FreshServiceAgentGroupModel model)
        {
            var levelOneGroupId = "";

            var levelOneGroupRecord = model.Groups.FirstOrDefault(x => x.Name.Contains(ConfigHelper.FirstLevelHelpDesk));
            if (levelOneGroupRecord != null)
            {
                levelOneGroupId = levelOneGroupRecord.Id;
            }

            return levelOneGroupId;
        }

        public static string ExecuteSubSequentAirCallService(AirCallApiTask _airCallApiTask)
        {
            var airCallModelList = new List<string>();
            var callsStringBuilder = new StringBuilder();
            var airCallResult = _airCallApiTask.Start();
            airCallModelList.Add(airCallResult);
            var listOfCalls = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
            var nextPageUrl = listOfCalls.Meta.NextPageLink;

            do
            {
                if (!string.IsNullOrEmpty(nextPageUrl))
                {
                    airCallResult = _airCallApiTask.Start(null, nextPageUrl);
                    airCallModelList.Add(airCallResult);
                    var deserializedObject = JsonConvert.DeserializeObject<AirCallModel>(airCallResult);
                    nextPageUrl = deserializedObject.Meta.NextPageLink;
                }
            } while (!string.IsNullOrEmpty(nextPageUrl));

            foreach (var value in airCallModelList)
            {
                callsStringBuilder.Append(value);
            }

            var mergedAirCallJsonValues = ConfigHelper.MergeJsonString(airCallModelList);

            return mergedAirCallJsonValues;
        }

        public static string ExecuteFreshServiceTimeEntriesTask(FreshServiceTicketModel[] tickets, FreshServiceTimeEntriesTask _freshServiceTimeEntriesTask)
        {
            var responseBodyList = new List<string>();
            var ticketStringBuilder = new StringBuilder();

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

            foreach (var value in responseBodyList)
            {
                ticketStringBuilder.Append(value);
            }

            var mergedJsonValues = ConfigHelper.MergeJsonString(responseBodyList);

            return mergedJsonValues;
        }
    }
}




using System;
using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class TransformationHelper
    {
        public static string FindLevelOneGroupIdentifier(FreshServiceAgentGroupModel model)
        {
            var levelOneGroupId = string.Empty;

            var levelOneGroupRecord =
                model.Groups.FirstOrDefault(x => x.Name.Contains(ConfigHelper.FirstLevelHelpDesk));
            if (levelOneGroupRecord != null)
            {
                levelOneGroupId = levelOneGroupRecord.Id;
            }

            return levelOneGroupId;
        }

        public static List<CachedModel> TransformTicketsToCachedEntity(FreshServiceTicketModel[] listOfTickets)
        {
            var cachedTicketModel = new List<CachedModel>();
            if (listOfTickets != null)
            {
                foreach (var tickets in listOfTickets)
                {
                    if (tickets?.Tickets == null)
                    {
                        continue;
                    }

                    foreach (var individualTicket in tickets.Tickets)
                    {
                        if (!string.IsNullOrEmpty(individualTicket.CreatedAt) &&
                            (DateTime.Parse(individualTicket.CreatedAt.Substring(0, 10))).Month ==
                            DateTime.Now.Month)
                        {
                            cachedTicketModel.Add(new CachedModel(
                                new Agent()
                                {
                                    TicketId = individualTicket.Id,
                                    CreatedAt = individualTicket.CreatedAt,
                                    DepartmentId = individualTicket.DepartmentId,
                                    DepartmentName = individualTicket.DepartmentName,
                                    UpdatedAt = individualTicket.UpdatedAt
                                },
                                new Organisation()
                                {
                                    TicketId = individualTicket.Id,
                                    CreatedAt = individualTicket.CreatedAt,
                                    DepartmentId = individualTicket.DepartmentId,
                                    DepartmentName = individualTicket.DepartmentName,
                                    UpdatedAt = individualTicket.UpdatedAt
                                },
                                
                                new TimeEntry(){})
                            );
                        }
                    }
                }
            }

            return cachedTicketModel;
        }


        public static List<string> ListOfTickets(string freshServiceResult)
        {
            var listOfTickets = JsonConvert.DeserializeObject<FreshServiceTicketModel[]>(freshServiceResult);

            var ticketIdList = new List<string>();
            if (listOfTickets != null)
            {
                foreach (var tickets in listOfTickets)
                {
                    if (tickets?.Tickets == null)
                    {
                        continue;
                    }

                    foreach (var individualTicket in tickets.Tickets)
                    {
                        if (!string.IsNullOrEmpty(individualTicket.CreatedAt) &&
                            (DateTime.Parse(individualTicket.CreatedAt.Substring(0, 10))).Month ==
                            DateTime.Now.Month)
                        {
                            ticketIdList.Add(individualTicket.Id);
                        }
                    }
                }
            }

            return ticketIdList;
        }
    }
}




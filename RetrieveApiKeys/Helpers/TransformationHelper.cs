﻿using System;
using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;

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

        public static List<string> GetListOfTickets(FreshServiceTicketModel[] listOfTickets)
        {
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




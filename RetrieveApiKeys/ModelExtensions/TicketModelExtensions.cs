using System;
using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions
{
    public static class TicketModelExtensions
    {
        public static List<TicketModel> PopulateTicketData(FreshServiceTicketModel[] ticketData, 
            FreshServiceRequesterModel[] cachedRequesterData, FreshServiceDepartmentModel[] cachedDepartmentData, FreshServiceAgentsModel[] cachedAgentsData)
        {
            var listOfTickets = new List<TicketModel>();

            if (ticketData != null && ticketData.Any())
            {
                foreach (var allTickets in ticketData)
                {
                    if (allTickets?.Tickets != null && allTickets.Tickets.Any())
                    {
                        foreach (var individualTicket in allTickets.Tickets)
                        {
                            var model = new TicketModel
                            {
                                TicketId = individualTicket.Id,
                                CreatedAt = DateTime.Parse(individualTicket.CreatedAt.Substring(0, 20)),
                                Status = RetrieveDataHelper.RetrieveStatusName(individualTicket.Status),
                                UpdatedAt = DateTime.Parse(individualTicket.UpdatedAt.Substring(0, 20)),
                                DueBy = DateTime.Parse(individualTicket.DueBy.Substring(0, 20)),
                                TicketType = individualTicket.TicketType,
                                Description = individualTicket.Description,
                                Requester = RetrieveDataHelper.RetrieveRequesterPrimaryEmail(cachedRequesterData, individualTicket.RequesterId),
                                DepartmentName = RetrieveDataHelper.RetrieveDepartmentName(cachedDepartmentData, individualTicket.DepartmentId),
                                AssignedTo = RetrieveDataHelper.RetrieveAgentEmail(cachedAgentsData, individualTicket.ResponderId)
                            };
                            listOfTickets.Add(model);
                        }
                    }
                }
            }

            return listOfTickets;
        }

    }
}

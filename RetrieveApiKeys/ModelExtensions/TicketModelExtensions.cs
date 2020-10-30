using System;
using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions
{
    public static class TicketModelExtensions
    {

        public static List<TicketModel> PopulateTicketData(FreshServiceTicketModel[] ticketData)
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
                                TicketId = Convert.ToInt32(individualTicket.Id),
                                CreatedAt = DateTime.Parse(individualTicket.CreatedAt.Substring(0, 10)),
                                Status = individualTicket.Status,
                                UpdatedAt = DateTime.Parse(individualTicket.UpdatedAt.Substring(0, 10)),
                                DueBy = DateTime.Parse(individualTicket.DueBy.Substring(0, 10)),
                                TicketType = individualTicket.TicketType,
                                Description = individualTicket.Description,
                                OwnerId = Convert.ToInt32(individualTicket.OwnerId),
                                DepartmentName = individualTicket.DepartmentName,
                                AssignedTo = individualTicket.OwnerId
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

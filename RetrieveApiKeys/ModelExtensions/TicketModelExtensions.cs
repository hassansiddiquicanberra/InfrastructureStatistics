using System;
using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions
{
    public static class TicketModelExtensions
    {
        public static List<TicketModel> PopulateTicketData(FreshServiceTicketModel[] ticketData, FreshServiceRequesterModel[] cachedRequesterData, FreshServiceDepartmentModel[] cachedDepartmentData)
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
                                CreatedAt = DateTime.Parse(individualTicket.CreatedAt.Substring(0, 10)),
                                Status = RetrieveStatusName(individualTicket.Status),
                                UpdatedAt = DateTime.Parse(individualTicket.UpdatedAt.Substring(0, 10)),
                                DueBy = DateTime.Parse(individualTicket.DueBy.Substring(0, 10)),
                                TicketType = individualTicket.TicketType,
                                Description = individualTicket.Description,
                                Requester = RetrieveRequesterPrimaryEmail(cachedRequesterData, individualTicket.RequesterId),
                                DepartmentName = RetrieveDepartmentName(cachedDepartmentData, individualTicket.DepartmentId)
                            };
                            listOfTickets.Add(model);
                        }
                    }
                }
            }

            return listOfTickets;
        }

        private static string RetrieveDepartmentName(FreshServiceDepartmentModel[] cachedDepartmentData, string departmentId)
        {
            var departmentName = string.Empty;

            foreach (var allDepartments in cachedDepartmentData)
            {
                if (allDepartments?.Departments != null && allDepartments.Departments.Any())
                {
                    foreach (var individualDepartment in allDepartments.Departments)
                    {
                        if (individualDepartment.Id == departmentId)
                        {
                            departmentName = individualDepartment.Name;
                        }
                    }
                }
            }

            return departmentName;
        }

        private static string RetrieveRequesterPrimaryEmail(FreshServiceRequesterModel[] cachedRequesterData, string requesterId)
        {
            var requesterName = string.Empty;

            foreach (var allRequesters in cachedRequesterData)
            {
                if (allRequesters?.Requesters != null && allRequesters.Requesters.Any())
                {
                    foreach (var individualRequester in allRequesters.Requesters)
                    {
                        if (individualRequester.Id == requesterId)
                        {
                            requesterName = individualRequester.PrimaryEmail;
                        }
                    }
                }
            }

            return requesterName;
        }

        private static string RetrieveStatusName(string statusId)
        {
            var statusValue = string.Empty;   
            switch (statusId)
            {
                case Constants.TicketWithOpenStatus:
                    statusValue = Constants.OpenTicket;
                    break;
                case Constants.TicketWithPendingStatus:
                    statusValue = Constants.PendingTicket;
                    break;
                case Constants.TicketWithResolvedStatus:
                    statusValue = Constants.ResolvedTicket;
                    break;
                case Constants.TicketWithClosedStatus:
                    statusValue = Constants.ClosedTicket;
                    break;
                default:
                    statusValue = string.Empty;
                    break;
            }

            return statusValue;
        }

    }
}

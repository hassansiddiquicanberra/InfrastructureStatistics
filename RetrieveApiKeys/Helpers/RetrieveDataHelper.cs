using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class RetrieveDataHelper
    {
        public static string RetrieveDepartmentName(FreshServiceDepartmentModel[] cachedDepartmentData, string departmentId)
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

        public static string RetrieveRequesterPrimaryEmail(FreshServiceRequesterModel[] cachedRequesterData, string requesterId)
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

        public static string RetrieveAgentEmail(FreshServiceAgentsModel[] cachedAgentsModels, string responderId)
        {
            var agentEmail = string.Empty;

            foreach (var allAgents in cachedAgentsModels)
            {
                if (allAgents?.Agents != null && allAgents.Agents.Any())
                {
                    foreach (var individualAgent in allAgents.Agents)
                    {
                        if (individualAgent.Id == responderId)
                        {
                            agentEmail = individualAgent.Email;
                        }
                    }
                }
            }

            return agentEmail;
        }

        public static string RetrieveStatusName(string statusId)
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

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Utils
{
    public static class Constants
    {
        public const string TicketWithOpenStatus = "2";
        public const string TicketWithPendingStatus = "3";
        public const string TicketWithResolvedStatus = "4";
        public const string TicketWithClosedStatus = "5";
        public const string LinkInResponseHeader = "link";
        public const double CacheExpirationTimeInHours = 5.0;
        public const string DepartmentsCacheKey = "ListOfDepartments";
        public const string RequestersCacheKey = "ListOfRequesters";
        public const string AgentsCacheKey = "ListOfAgents";
        public const string OpenTicket = "Open";
        public const string PendingTicket = "Pending";
        public const string ResolvedTicket = "Resolved";
        public const string ClosedTicket = "Closed";
    }
}

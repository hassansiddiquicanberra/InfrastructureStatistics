namespace F1Solutions.InfrastructureStatistics.ApiCalls.Models
{
    public class CachedModel
    {
        public CachedModel(Agent agent, Organisation organisation, TimeEntry timeEntry = null)
        {
            CachedAgent = agent;
            CachedOrganisation = organisation;
            CachedTimeEntry = timeEntry;
        }
        public Agent CachedAgent { get; set; }
        public Organisation CachedOrganisation { get; set; }
        public TimeEntry CachedTimeEntry { get; set; }
    }

    public class Agent
    {
        public string TicketId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

    public class Organisation //TODO: check what fields are required here ...
    {
        public string TicketId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

    public class TimeEntry
    {
        public string OwnerId { get; set; }
        public string UpdatedAt { get; set; }
        public string Urgency { get; set; }
        public string CreatedAt { get; set; }
        public string Status { get; set; }
        public string TimeSpent { get; set; }
        public string Billable { get; set; }
    }
}

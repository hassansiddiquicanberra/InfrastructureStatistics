using System;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Models
{
    public class FreshServiceAgentModel
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public DateTime DateRecorded { get; set; }
        public int TotalTicketsOpen { get; set; }
        public int TotalResolvedToday { get; set; }
        public int TotalResolvedLastSevenDays { get; set; }
        public int TotalTickets { get; set; }
        public int TotalResolvedLastThirtyDays { get; set; }
        public int TotalNotRespondedToInTwoDays { get; set; }
        public decimal AverageTicketAgeLastSevenDays { get; set; }
        public decimal AverageTicketResolutionTimeLastSevenDays { get; set; }
    }
}

using System;

namespace F1Solutions.InfrastructureStatistics.Services.Models
{
    public class CallModel
    {
        public int CallId { get; set; }
        public string Direction { get; set; }
        public string Status { get; set; }
        public string MissedCallReason { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime AnsweredAt { get; set; }
        public DateTime EndedAt { get; set; }
        public string Duration { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedToEmail { get; set; }
        public string UserName { get; set; }
    }
}

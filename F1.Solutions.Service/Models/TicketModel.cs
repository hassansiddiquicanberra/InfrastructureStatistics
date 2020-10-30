using System;

namespace F1Solutions.InfrastructureStatistics.Services.Models
{
    public class TicketModel
    {
        public int TicketId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DueBy { get; set; }
        public string TicketType { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public string DepartmentName { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedToEmail { get; set; }
    }
}

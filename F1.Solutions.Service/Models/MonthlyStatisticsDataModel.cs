namespace F1Solutions.InfrastructureStatistics.Services.Models
{
    public class MonthlyStatisticsDataModel
    {
        public int Id { get; set; }
        public int TicketCountForTheMonth { get; set; }
        public decimal? AverageTicketHandleTimeInMinutes { get; set; }
        public int TicketsResolvedByLevelOne { get; set; }
    }
}

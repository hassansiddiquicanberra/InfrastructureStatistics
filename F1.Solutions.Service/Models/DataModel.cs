using System;

namespace F1.Solutions.Service.Models
{
    public class DataModel
    {
        public int Id { get; set; }
        public decimal TwoDayPercentage { get; set; }
        public int OpenMoreThanSevenDays { get; set; }
        public int OpenMoreThanThirtyDays { get; set; }
        public int TotalPositive { get; set; }
        public int TotalNeutral { get; set; }
        public int TotalNegative { get; set; }
        public int TotalMspMissedCalls { get; set; }
        public int TotalRegisMissedCalls { get; set; }
        public DateTime? TotalMspHoldTime { get; set; }
        public DateTime? TotalRegisHoldTime { get; set; }
    }
}

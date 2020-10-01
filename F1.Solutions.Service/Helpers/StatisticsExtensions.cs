using System;
using DataAccess;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.Services.Helpers
{
    public static class StatisticsExtensions
    {
        public static Statistic ModelToDomain(this DataModel model)
        {
            return new Statistic()
            {
                TwoDayPercentage = model.TwoDayPercentage,
                OpenMoreThanSevenDays = model.OpenMoreThanSevenDays,
                OpenMoreThanThirtyDays = model.OpenMoreThanThirtyDays,
                TotalPositive = model.TotalPositive,
                TotalNeutral = model.TotalNeutral,
                TotalNegative = model.TotalNegative,
                TotalMspMissedCalls = model.TotalMspMissedCalls,
                TotalRegisMissedCalls = model.TotalRegisMissedCalls,
                TotalRegisHoldTime = model.TotalRegisHoldTime,
                TotalMspHoldTime = model.TotalMspHoldTime,
                EntryDateTime = DateTime.Now
            };
        }

    }
}

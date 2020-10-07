using System;
using F1Solutions.InfrastructureStatistics.DataAccess;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.Services.Helpers
{
    public static class StatisticsExtensions
    {
        public static Statistic StatisticModelToDomain(this StatisticsDataModel model)
        {
            return new Statistic()
            {
                Id = model.Id,
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

        public static MonthlyStatistic MonthlyStatisticModelToDomain(this MonthlyStatisticsDataModel model)
        {
            return new MonthlyStatistic()
            {
                Id = model.Id,
                MonthlyTicketCount = model.TicketCountForTheMonth,
                MonthlyAverageTicketHandlingTime = model.AverageTicketHandleTimeInMinutes,
                MonthlyLevelOneResolvedTickets = model.TicketsResolvedByLevelOne,
                EntryDateTime = DateTime.Now
            };
        }

    }
}

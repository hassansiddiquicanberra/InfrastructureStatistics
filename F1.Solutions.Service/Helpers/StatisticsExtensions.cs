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

        public static Agent StatisticsAgentModelToDomain(this StatisticsAgentDataModel model)
        {
            return new Agent()
            {
                AgentId = model.AgentId,
                AgentName =  model.AgentName,
                DateRecorded = model.DateRecorded,
                TotalTicketsOpen = model.TotalTicketsOpen,
                AverageTicketAgeLastSevenDays = model.AverageTicketAgeLastSevenDays,
                AverageTicketResolutionTimeLastSevenDays = model.AverageTicketResolutionTimeLastSevenDays,
                TotalNotRespondedToInTwoDays = model.TotalNotRespondedToInTwoDays,
                TotalResolvedLastSevenDays = model.TotalResolvedLastSevenDays,
                TotalResolvedLastThirtyDays = model.TotalResolvedLastThirtyDays,
                TotalResolvedToday = model.TotalResolvedToday,
                TotalTickets = model.TotalTickets
            };
        }

        public static Organisation StatisticsOrganisationModelToDomain(this StatisticsOrganisationDataModel model)
        {
            return new Organisation()
            {
                OrganisationId = model.OrganisationId,
                OrganisationName = model.OrganistionName,
                DateRecorded = model.DateRecorded,
                TotalOlderThanSevenDays = model.TotalOlderThanSevenDays,
                TotalOlderThanThirtyDays = model.TotalOlderThanThirtyDays,
                TotalOpen = model.TotalOpen
            };
        }
    }
}

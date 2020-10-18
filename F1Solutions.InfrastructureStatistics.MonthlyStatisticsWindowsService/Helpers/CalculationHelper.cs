using System;

namespace F1Solutions.InfrastructureStatistics.MonthlyStatisticsWindowsService.Helpers
{
    public static class CalculationHelper
    {
        public static bool IsTodayFirstDayOfTheMonth()
        {
            var today = DateTime.Now;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

            return firstDayOfMonth.Day == today.Day;
        }
    }
}

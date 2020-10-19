using System;

namespace F1Solutions.InfrastructureStatistics.MonthlyStatisticsWindowsService.Helpers
{
    public static class CalculationHelper
    {
        public static bool IsFirstDayOfTheMonthAndTimeMatches()
        {
            var today = DateTime.Now;
            var isTodayFirstDayOfTheMonth = false;
            var isTimeOneAm = false;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 19);
            var startDate = new DateTime(today.Year, today.Month, today.Day, today.Hour, today.Minute, today.Second);
            var endDate = new DateTime(today.Year, today.Month, today.Day, 01, 0, 0);

            if (firstDayOfMonth.Day == today.Day)
            {
                isTodayFirstDayOfTheMonth = true;
            }

            if ((today > startDate) && (today < endDate))
            {
                isTimeOneAm = true;
            }

            return isTodayFirstDayOfTheMonth && isTimeOneAm;
        }
    }
}

using System;

namespace F1Solutions.InfrastructureStatistics.MonthlyStatisticsWindowsService.Helpers
{
    public static class CalculationHelper
    {
        public static bool IsFirstDayOfTheMonthAndTimeMatches()
        {
            var today = DateTime.Now;
            var isTimeMatching = false;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 19);
            var start = new DateTime(today.Year, today.Month, today.Day, today.Hour, today.Minute, today.Second); 
            var end = new DateTime(today.Year, today.Month, today.Day, 13, 30, 0); //1:30 pm today 19 October
            DateTime now = DateTime.Now;

            if ((now > start) && (now < end))
            {
                isTimeMatching = true;
            }


            return firstDayOfMonth.Day == today.Day && isTimeMatching;
        }
    }
}

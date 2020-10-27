using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using F1Solutions.InfrastructureStatistics.DataAccess;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class CalculationHelper<T> where T:DbSet<T>
    {
        public static bool IsFirstDayOfTheMonthAndTimeMatches()
        {
            var hourClockAsOneAm = 1;
            var today = DateTime.Now;
            var isTodayFirstDayOfTheMonth = false;
            var isTimeOneAm = false;

            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var currentDateWithTime =
                new DateTime(today.Year, today.Month, today.Day, today.Hour, today.Minute, today.Second);
            var currentDateWithTimeAsOneAm = new DateTime(today.Year, today.Month, today.Day, hourClockAsOneAm, 0, 0);

            if (firstDayOfMonth.Day == today.Day)
            {
                isTodayFirstDayOfTheMonth = true;
            }

            if (today > currentDateWithTime && today < currentDateWithTimeAsOneAm)
            {
                isTimeOneAm = true;
            }

            return isTodayFirstDayOfTheMonth && isTimeOneAm;
        }
    }
}
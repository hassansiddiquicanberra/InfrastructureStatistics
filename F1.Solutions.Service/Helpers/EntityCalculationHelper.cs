using System;
using System.Data.Entity;
using System.Linq;
using F1Solutions.InfrastructureStatistics.DataAccess;

namespace F1Solutions.InfrastructureStatistics.Services.Helpers
{
    public static class EntityCalculationHelper
    {
        public static bool DoesValueExistForTodaysDate(DbSet<MonthlyStatistic> dbObject)
        {
            var currentDateTime = DateTime.Now;

            return dbObject.Any(x => x.EntryDateTime.Year == currentDateTime.Year
                                        && x.EntryDateTime.Month == currentDateTime.Month
                                        && x.EntryDateTime.Day == currentDateTime.Day);

        }
    }
}
using System;
using System.Linq;
using F1Solutions.InfrastructureStatistics.DataAccess;
using F1Solutions.InfrastructureStatistics.Services.Helpers;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.Services
{
    public class StatisticsService
    {
        static readonly StatisticsEntity DataAccessStatistics = new StatisticsEntity();

        public void SaveStatisticsValues(StatisticsDataModel model)
        {
            var statisticsValues = model.StatisticModelToDomain();

            DataAccessStatistics.Statistics.Add(statisticsValues);
            DataAccessStatistics.SaveChanges();
        }

        public void SaveMonthlyStatisticsValues(MonthlyStatisticsDataModel model)
        {
            var monthlyStatisticValues = model.MonthlyStatisticModelToDomain();

            DataAccessStatistics.MonthlyStatistics.Add(monthlyStatisticValues);
            DataAccessStatistics.SaveChanges();
        }

        public bool DoesAnyRecordExistForToday()
        {
            var currentDateTime = DateTime.Now;
            bool recordExistsForToday = DataAccessStatistics.MonthlyStatistics.Any(x => x.EntryDateTime.Year == currentDateTime.Year &&
                                                                                         x.EntryDateTime.Month == currentDateTime.Month &&
                                                                                         x.EntryDateTime.Day == currentDateTime.Day);

            return recordExistsForToday;
        }
    }
}

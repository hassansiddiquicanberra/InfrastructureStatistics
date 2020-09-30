using DataAccess;
using F1.Solutions.Service.Helpers;
using F1.Solutions.Service.Models;

namespace F1.Solutions.Service
{
    public class StatisticsService
    {
        static readonly StatisticsEntity DataAccessStatistics = new StatisticsEntity();

        public void SaveStatisticsValues(DataModel model)
        {
            var statisticsValues = model.ModelToDomain();

            DataAccessStatistics.Statistics.Add(statisticsValues);
            DataAccessStatistics.SaveChanges();
        }
    }
}

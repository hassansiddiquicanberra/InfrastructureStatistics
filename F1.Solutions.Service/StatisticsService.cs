﻿using DataAccess;
using F1.Solutions.Service.Models;
using F1Solutions.InfrastructureStatistics.Services.Helpers;

namespace F1Solutions.InfrastructureStatistics.Services
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

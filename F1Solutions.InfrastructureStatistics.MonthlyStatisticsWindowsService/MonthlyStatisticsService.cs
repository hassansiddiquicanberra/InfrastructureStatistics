﻿using System;
using System.ServiceProcess;
using F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator;
using F1Solutions.InfrastructureStatistics.MonthlyStatisticsWindowsService.Helpers;

namespace F1Solutions.InfrastructureStatistics.MonthlyStatisticsWindowsService
{
    public partial class MonthlyStatisticsService : ServiceBase
    {
        private readonly ApiOrchestrator _apiOrchestrator;
        public MonthlyStatisticsService()
        {
            InitializeComponent();
            _apiOrchestrator = new ApiOrchestrator();
        }
        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            if(CalculationHelper.IsTodayFirstDayOfTheMonth())
            {
                _apiOrchestrator.ExecuteMonthlyStatisticsServiceCalls();
            }
        }

        protected override void OnStop()
        {
        }
    }
}

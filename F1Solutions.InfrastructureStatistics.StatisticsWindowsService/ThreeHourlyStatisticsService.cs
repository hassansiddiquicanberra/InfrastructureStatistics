﻿using System.ServiceProcess;
using System.Timers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator;

namespace F1Solutions.InfrastructureStatistics.StatisticsWindowsService
{
    public partial class ThreeHourlyStatisticsService : ServiceBase
    {
        private readonly double ServiceToRunEveryThreeHoursInMilliseconds = 10800000;
        readonly Timer _timer = new Timer();

        private readonly ApiOrchestrator _apiOrchestrator;
        public ThreeHourlyStatisticsService()
        {
            InitializeComponent();
            _apiOrchestrator = new ApiOrchestrator();
        }

        protected override void OnStart(string[] args)
        {
            _timer.Elapsed += OnElapsedTime;
            _timer.Interval = ServiceToRunEveryThreeHoursInMilliseconds;
            _timer.Enabled = true;
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            _apiOrchestrator.ExecuteHourlyStatisticsServiceCalls();
        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator;

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

        protected override void OnStart(string[] args)
        {
            _apiOrchestrator.ExecuteMonthlyStatisticsServiceCalls();
        }

        protected override void OnStop()
        {
        }
    }
}

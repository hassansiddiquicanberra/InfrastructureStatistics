using System.ServiceProcess;
using F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator;

namespace F1Solutions.InfrastructureStatistics.StatisticsWindowsService
{
    public partial class StatisticsService : ServiceBase
    {
        private readonly ApiOrchestrator _apiOrchestrator;
        public StatisticsService()
        {
            InitializeComponent();
            _apiOrchestrator = new ApiOrchestrator();
        }

        protected override void OnStart(string[] args)
        {
            _apiOrchestrator.ExecuteNonMonthlyStatisticsServiceCalls();
        }

        protected override void OnStop()
        {
        }
    }
}

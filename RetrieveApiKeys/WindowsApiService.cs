using System.ServiceProcess;
using System.Timers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator;

namespace F1Solutions.InfrastructureStatistics.ApiCalls
{
    partial class WindowsApiService : ServiceBase
    {
        private readonly ApiOrchestrator _apiOrchestrator;
        private readonly double ServiceToRunEveryThreeHoursInMilliseconds = 10800000;
        readonly Timer _timer = new Timer();
        public WindowsApiService()
        {
            InitializeComponent();
            _apiOrchestrator = new ApiOrchestrator();
        }

        public void Start()
        {
            if (CalculationHelper.IsFirstDayOfTheMonthAndTimeMatches())
            {
                _apiOrchestrator.ExecuteMonthlyStatisticsServiceCalls();
            }

            _timer.Elapsed += OnElapsedTime;
            _timer.Interval = ServiceToRunEveryThreeHoursInMilliseconds;
            _timer.Enabled = true;
        }

        public new void Stop()
        {
            _timer.Enabled = false;
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            _apiOrchestrator.ExecuteHourlyStatisticsServiceCalls();
        }

        protected override void OnStart(string[] args){}

        protected override void OnStop(){}
    }
}

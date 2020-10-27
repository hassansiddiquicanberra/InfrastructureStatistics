using System.ServiceProcess;
using System.Timers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator;
using F1Solutions.InfrastructureStatistics.Services;

namespace F1Solutions.InfrastructureStatistics.ApiCalls
{
    partial class WindowsApiService : ServiceBase
    {
        private readonly ApiOrchestrator _apiOrchestrator;
        private readonly double ServiceToRunEverySixMinutesInMilliseconds = 360000;
        private readonly StatisticsService _statisticsService;
        readonly Timer _timer = new Timer();

        public WindowsApiService()
        {
            InitializeComponent();
            _apiOrchestrator = new ApiOrchestrator();
            _statisticsService = new StatisticsService();
        }

        public void Start()
        {
            //_apiOrchestrator.ExecuteMonthlyStatisticsServiceCalls();
            ///*
            // * TODO:**CHECK WITH ZAR FOR Whether the Below call to monthly will change ??  will it not be every 1st day of the month at 1am?
            //*/
            //if (CalculationHelper.IsFirstDayOfTheMonthAndTimeMatches())
            //{
            //    if (!_statisticsService.DoesAnyRecordExistForToday())
            //    {
            //        _apiOrchestrator.ExecuteMonthlyStatisticsServiceCalls();
            //    }
            //}

            _timer.Elapsed += OnElapsedTime;
            _timer.Interval = ServiceToRunEverySixMinutesInMilliseconds;
            _timer.Enabled = true;
        }

        public new void Stop()
        {
            _timer.Enabled = false;
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            _apiOrchestrator.ExecuteMonthlyStatisticsServiceCalls();
        }

        protected override void OnStart(string[] args) { }

        protected override void OnStop() { }
    }
}

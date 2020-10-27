using System.ServiceProcess;
using System.Timers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator;
using F1Solutions.InfrastructureStatistics.Services;

namespace F1Solutions.InfrastructureStatistics.ApiCalls
{
    partial class WindowsApiService : ServiceBase
    {
        private readonly ApiOrchestrator _apiOrchestrator;
        private const string CacheKey = "CachedListOfTickets";
        //private readonly double ServiceToRunEveryThreeHoursInMilliseconds = 10800000;
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

            //only perform below if the cache has expired ?

            _apiOrchestrator.ExecuteMonthlyStatisticsServiceCalls();

            //if (CalculationHelper.IsFirstDayOfTheMonthAndTimeMatches())
            //{
            //    if(!_statisticsService.DoesAnyRecordExistForToday())
            //    {
            //        _apiOrchestrator.ExecuteMonthlyStatisticsServiceCalls();
            //    }
            //}

            //_timer.Elapsed += OnElapsedTime;
            //_timer.Interval = ServiceToRunEveryThreeHoursInMilliseconds;
            //_timer.Enabled = true;
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

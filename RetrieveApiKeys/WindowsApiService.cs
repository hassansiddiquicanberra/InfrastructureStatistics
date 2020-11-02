using System.ServiceProcess;
using System.Timers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator;

namespace F1Solutions.InfrastructureStatistics.ApiCalls
{
    partial class WindowsApiService : ServiceBase
    {
        private readonly ApiOrchestrator _apiOrchestrator;
        private readonly double ServiceToRunEveryFiveHoursInMilliseconds = 18000000;
        readonly Timer _timer = new Timer();
        public WindowsApiService()
        {
            InitializeComponent();
            _apiOrchestrator = new ApiOrchestrator();
        }
        public void Start()
        {
            _timer.Elapsed += OnElapsedTime;
            _timer.Interval = ServiceToRunEveryFiveHoursInMilliseconds;
            _timer.Enabled = true;
        }
        public new void Stop()
        {
            _timer.Enabled = false;
        }
        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            _apiOrchestrator.ExecuteServiceForCalls();
            _apiOrchestrator.ExecuteApiServiceCallForTickets();
        }

        protected override void OnStart(string[] args) { }
        protected override void OnStop() { }
    }
}

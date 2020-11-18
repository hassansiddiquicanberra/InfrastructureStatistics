using System.ServiceProcess;
using System.Threading;
using System.Timers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator;
using log4net;
using Timer = System.Timers.Timer;

namespace F1Solutions.InfrastructureStatistics.ApiCalls
{
    partial class WindowsApiService : ServiceBase
    {
        readonly ILog _log = LogManager.GetLogger(typeof(WindowsApiService));
        private readonly ApiOrchestrator _apiOrchestrator;
        private readonly double ServiceToRunEveryFiveHoursInMilliseconds = 18000000;
        readonly Timer _timer = new System.Timers.Timer();

        public WindowsApiService()
        {
            InitializeComponent();
            _apiOrchestrator = new ApiOrchestrator();
        }
        public void Start()
        {
            OnStart(new[] {""});
        }
        public new void Stop()
        {
            OnStop();
        }
        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            _apiOrchestrator.ExecuteServiceForCalls();
            _apiOrchestrator.ExecuteApiServiceCallForTickets();
        }

        protected override void OnStart(string[] args)
        {
           Thread thread = new Thread(new ThreadStart(Listener));
           thread.Start();
        }

        private void Listener()
        {
            _log.Info("Service Initialized.");

            _apiOrchestrator.ExecuteServiceForCalls();
            _apiOrchestrator.ExecuteApiServiceCallForTickets();

            _timer.Elapsed += OnElapsedTime;
            _timer.Interval = ServiceToRunEveryFiveHoursInMilliseconds;
            _timer.Enabled = true;
            _timer.Start();
        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
        }
    }
}

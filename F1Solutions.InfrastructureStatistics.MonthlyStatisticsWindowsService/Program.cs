using System.ServiceProcess;

namespace F1Solutions.InfrastructureStatistics.MonthlyStatisticsWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            #if DEBUG
            var myService = new MonthlyStatisticsService();
            myService.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            #else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MonthlyStatisticsService()
            };
            ServiceBase.Run(ServicesToRun);
            #endif
        }
    }
}

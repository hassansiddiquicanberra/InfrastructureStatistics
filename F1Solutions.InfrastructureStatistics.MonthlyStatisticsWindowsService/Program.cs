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
            MonthlyStatisticsService service = new MonthlyStatisticsService();
            service.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            #else
            var servicesToRun = new ServiceBase[]
            {
                new MonthlyStatisticsService()
            };

            ServiceBase.Run(servicesToRun);
            #endif
        }
    }
}

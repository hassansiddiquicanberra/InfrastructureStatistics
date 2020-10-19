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
            var servicesToRun = new ServiceBase[]
            {
                new MonthlyStatisticsService()
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}

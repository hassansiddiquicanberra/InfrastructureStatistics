using System.ServiceProcess;

namespace F1Solutions.InfrastructureStatistics.StatisticsWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new HourlyStatisticsService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}

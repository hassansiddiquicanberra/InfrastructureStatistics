using System.ServiceProcess;

namespace F1Solutions.InfrastructureStatistics.ApiCalls
{
    public class Program
    {
        
        static void Main(string[] args)
        {
#if DEBUG

            WindowsApiService myService = new WindowsApiService();
            myService.Start();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

#endif
            ServiceBase[] servicesToRun;
            servicesToRun = new ServiceBase[]
            {
                new WindowsApiService(),
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}

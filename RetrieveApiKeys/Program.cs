using System.ServiceProcess;
using Topshelf;

namespace F1Solutions.InfrastructureStatistics.ApiCalls
{
    public class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(hostConfig =>
            {
                hostConfig.Service<WindowsApiService>(serviceConfig =>
                    {
                        if (serviceConfig != null)
                        {
                            serviceConfig.ConstructUsing(() => new WindowsApiService());
                            serviceConfig.WhenStarted(s => s.Start());
                            serviceConfig.WhenStopped(s => s.Stop());
                        }
                    })
                    .RunAsLocalService()
                    .StartManually()
                    .SetServiceName("Windows Api Service");
            });
        }
    }
}

using System;
using F1Solutions.InfrastructureStatictics.ApiCalls.Orchestrator;
using Topshelf;

namespace F1Solutions.InfrastructureStatistics.ApiCalls
{
    public class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(hostConfig =>
            {
                hostConfig.Service<ApiOrchestrator>(serviceConfig =>
                    {
                        serviceConfig.ConstructUsing(() => new ApiOrchestrator());
                        serviceConfig.WhenStarted(s => s.Start());
                        serviceConfig.WhenStopped(s => s.Stop());
                    })
                    .RunAsLocalSystem()
                    .EnableServiceRecovery(r => r.RestartService(TimeSpan.FromSeconds(10)))
                    .StartAutomatically()
                    .SetServiceName("Infrastructure Statistics");
            });
        }
    }
}

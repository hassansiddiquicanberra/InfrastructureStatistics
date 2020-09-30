using System;
using RetrieveApiKeys.Orchestrator;
using Topshelf;

namespace RetrieveApiKeys
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

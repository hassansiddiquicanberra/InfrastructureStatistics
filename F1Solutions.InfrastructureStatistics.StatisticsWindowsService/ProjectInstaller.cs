using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace F1Solutions.InfrastructureStatistics.StatisticsWindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            using (var serviceController = new ServiceController(HourlyServiceInstaller.ServiceName))
            {
                if (serviceController.Status != ServiceControllerStatus.Running)
                {
                    serviceController.Start();
                }
            }
        }
    }
}

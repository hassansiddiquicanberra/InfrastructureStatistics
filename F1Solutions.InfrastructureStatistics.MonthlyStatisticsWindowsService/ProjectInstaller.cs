using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace F1Solutions.InfrastructureStatistics.MonthlyStatisticsWindowsService
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
            using (var serviceController = new ServiceController(MonthlyServiceInstaller.ServiceName))
            {
                if (serviceController.Status != ServiceControllerStatus.Running)
                {
                    serviceController.Start();
                }
            }
        }
    }
}

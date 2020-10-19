namespace F1Solutions.InfrastructureStatistics.StatisticsWindowsService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ThreeHourlyServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ThreeHourlyServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ThreeHourlyServiceProcessInstaller
            // 
            this.ThreeHourlyServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ThreeHourlyServiceProcessInstaller.Password = null;
            this.ThreeHourlyServiceProcessInstaller.Username = null;
            // 
            // ThreeHourlyServiceInstaller
            // 
            this.ThreeHourlyServiceInstaller.Description = "This service runs every 3 hours to save statistical data";
            this.ThreeHourlyServiceInstaller.ServiceName = "ThreeHourlyStatisticsService";
            this.ThreeHourlyServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ThreeHourlyServiceProcessInstaller,
            this.ThreeHourlyServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ThreeHourlyServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ThreeHourlyServiceInstaller;
    }
}
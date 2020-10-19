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
            this.HourlyServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.HourlyServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // HourlyServiceProcessInstaller
            // 
            this.HourlyServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.HourlyServiceProcessInstaller.Password = null;
            this.HourlyServiceProcessInstaller.Username = null;
            // 
            // HourlyServiceInstaller
            // 
            this.HourlyServiceInstaller.Description = "This service runs every hour to save statistical data";
            this.HourlyServiceInstaller.ServiceName = "HourlyStatisticsService";
            this.HourlyServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.HourlyServiceProcessInstaller,
            this.HourlyServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller HourlyServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller HourlyServiceInstaller;
    }
}
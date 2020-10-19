namespace F1Solutions.InfrastructureStatistics.MonthlyStatisticsWindowsService
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
            this.MonthlyServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.MonthlyServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // MonthlyServiceProcessInstaller
            // 
            this.MonthlyServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.MonthlyServiceProcessInstaller.Password = null;
            this.MonthlyServiceProcessInstaller.Username = null;
            // 
            // MonthlyServiceInstaller
            // 
            this.MonthlyServiceInstaller.Description = "This service runs on 1st of every month to save monthly statistics";
            this.MonthlyServiceInstaller.ServiceName = "MonthlyStatisticsService";
            this.MonthlyServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.MonthlyServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.MonthlyServiceProcessInstaller,
            this.MonthlyServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller MonthlyServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller MonthlyServiceInstaller;
    }
}
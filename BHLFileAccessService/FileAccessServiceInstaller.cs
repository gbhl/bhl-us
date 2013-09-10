using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace MOBOT.FileAccessService
{
    [RunInstaller(true)]
    public partial class FileAccessServiceInstaller : Installer
    {
        private ServiceInstaller _serviceInstaller;
        private ServiceProcessInstaller _processInstaller;

        public FileAccessServiceInstaller()
        {
            InitializeComponent();

            this._processInstaller = new ServiceProcessInstaller();
            this._serviceInstaller = new ServiceInstaller();

            this._processInstaller.Account = ServiceAccount.LocalSystem;
            this._serviceInstaller.StartType = ServiceStartMode.Automatic;
            this._serviceInstaller.ServiceName = "MOBOT FileAccess Service";
            this._serviceInstaller.Description = "MOBOT FileAccess Service";

            Installers.Add(this._serviceInstaller);
            Installers.Add(this._processInstaller);
        }
    }
}
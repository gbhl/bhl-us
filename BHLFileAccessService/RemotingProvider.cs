using System;
using System.Runtime.Remoting;
using System.ServiceProcess;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace MOBOT.FileAccessService
{
    partial class RemotingProvider : ServiceBase
    {
        public RemotingProvider()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            log.Debug("Exception caught...");
            log.Debug("Message = " + ex.Message);
            log.Debug("Stack Trace = " + ex.StackTrace);
        }

        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        protected override void OnStart(string[] args)
        {
            log.Debug("MOBOT FileAccess OnStart invoked");

            log.Debug("Attempting Remoting configuration");
            RemotingConfiguration.Configure(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false);

            try
            {
                WellKnownServiceTypeEntry[] entries = RemotingConfiguration.GetRegisteredWellKnownServiceTypes();
                foreach (WellKnownServiceTypeEntry ent in entries)
                {
                    log.Debug("Object '" + ent.TypeName + "' assembly name: '" + ent.AssemblyName + "' registered bound to uri '" +
                        ent.ObjectUri + "'.  Mode: '" + ent.Mode.ToString() + "'.");
                }
                log.Debug("MOBOT FileAccess Service successfully started at " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                log.Error("Exception in MOBOT FileAccess Service OnStart", ex);
            }
        }

        protected override void OnStop()
        {

            try
            {
                log.Debug("MOBOT FileAccess Service successfully stopped ");
            }
            catch (Exception ex)
            {
                log.Error("Exception in MOBOT FileAccess Service OnStop", ex);
            }
        }
    }
}

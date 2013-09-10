using System.Diagnostics;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace MOBOT.BHL.BHLMETSUpload
{
    class Program
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        static void Main(string[] args)
        {
            if (isAlreadyRunning())
            {
                log.Error("BHLMETSUpload is already running");
            }
            else
            {
                METSUpload generator = new METSUpload();
                generator.Process();
            }
        }

        private static bool isAlreadyRunning()
        {
            Process thisProcess = Process.GetCurrentProcess();
            Process[] allProcesses = Process.GetProcessesByName(thisProcess.ProcessName);
            if (allProcesses.Length > 1)
            {
                return true;
            }
            return false;
        }
    }
}

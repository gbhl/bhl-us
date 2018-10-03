using System.Diagnostics;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BHL.TextImportProcessor
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
                log.Error("BHLTextImportProcessor is already running");
            }
            else
            {
                TextImportProcessor processor = new TextImportProcessor();
                processor.Process();
            }
        }

        private static bool isAlreadyRunning()
        {
            Process thisProcess = Process.GetCurrentProcess();
            Process[] allProcesses = Process.GetProcessesByName(thisProcess.ProcessName);
            // Allow up to ten instances of this process to run simultaneously
            if (allProcesses.Length > 10)
            {
                return true;
            }
            return false;
        }
    }
}

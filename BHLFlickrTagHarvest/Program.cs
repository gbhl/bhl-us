using System.Diagnostics;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BHLFlickrTagHarvest
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main()
        {
            if (IsAlreadyRunning())
            {
                log.Error("BHLFlickrTagHarvest is already running");
            }
            else
            {
                FlickrTagProcessor processor = new();
                processor.Process();
            }
        }

        private static bool IsAlreadyRunning()
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

using System.Diagnostics;

namespace BHLFlickrTagHarvest
{
    class Program
    {
        static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            if (isAlreadyRunning())
            {
                log.Error("BHLFlickrTagHarvest is already running");
            }
            else
            {
                FlickrTagProcessor processor = new FlickrTagProcessor();
                processor.Process();
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

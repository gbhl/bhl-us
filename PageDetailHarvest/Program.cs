using System;
using System.Diagnostics;

namespace PageDetailHarvest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (isAlreadyRunning())
            {
                Console.WriteLine("PageDetailHarvest is already running");
            }
            else
            {
                HarvestProcessor processor = new HarvestProcessor();
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

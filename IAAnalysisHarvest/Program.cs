﻿using System.Diagnostics;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace IAAnalysisHarvest
{
    class Program
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        static void Main()
        {
            if (IsAlreadyRunning())
            {
                log.Error("IAAnalysisHarvest is already running");
            }
            else
            {
                HarvestProcessor processor = new();
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

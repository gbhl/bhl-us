using System;

namespace MOBOT.BHL.BHLAuditImport
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ImportData newImport = new ImportData();
            newImport.mainThread();
        }
    }
}

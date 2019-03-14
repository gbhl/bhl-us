using System;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BHL.Export
{
    public class ExportLogger
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without needing to edit the code.

        bool _toFile = true;
        bool _toConsole = true;

        public ExportLogger(bool toFile = true, bool toConsole = true)
        {
            _toFile = toFile;
            _toConsole = toConsole;
        }

        public void Info(string message)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled)
            {
                if (_toFile) log.Info(message);
                if (_toConsole) Console.WriteLine(message);
            }
        }

        public void Error(string message, Exception ex = null)
        {
            if (ex == null)
            {
                if (_toFile) log.Error(message);
                if (_toConsole) Console.WriteLine(message);
            }
            else
            {
                if (_toFile) log.Error(message, ex);
                if (_toConsole)
                {
                    Console.WriteLine(message);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

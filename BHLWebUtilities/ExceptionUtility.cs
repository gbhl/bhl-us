using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;

namespace MOBOT.BHL.Web.Utilities
{
    public class ExceptionUtility
    {
        // Log an Exception with default log level (Detailed)
        public static void LogException(Exception exc, string source)
        {
            LogException(exc, source, LogLevel.Detailed);
        }

        public static class LogLevel
        {
            public const string Detailed = "DETAILED";
            public const string Short = "SHORT";
            public const string None = "NONE";
        }

        // Log an Exception with a specified log level
        public static void LogException(Exception exc, string source, string logLevel)
        {
            // If a global log level is set in a config file, then it determines the highest possible level of logging.
            // Individual log requests can only lower the level of logging.
            string globalLogLevel = ConfigurationManager.AppSettings["ExceptionLogLevel"];
            if (string.IsNullOrWhiteSpace(globalLogLevel)) globalLogLevel = LogLevel.Detailed;
            if (globalLogLevel == LogLevel.None) logLevel = LogLevel.None;
            if (globalLogLevel == LogLevel.Short && logLevel == LogLevel.Detailed) logLevel = LogLevel.Short;

            StreamWriter sw = null;

            if (logLevel != LogLevel.None)
            {
                try
                {
                    // Build the message to log
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(string.Format("*** {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    if (exc.InnerException != null)
                    {
                        sb.AppendLine(string.Format("Inner Exception: ({0}) {1}", exc.InnerException.GetType().ToString(), exc.InnerException.Message));
                        sb.AppendLine(string.Format("Inner Source: {0}", exc.InnerException.Source));
                        if (exc.InnerException.StackTrace != null && logLevel == LogLevel.Detailed)
                        {
                            sb.AppendLine("Inner Stack Trace: ");
                            sb.AppendLine(exc.InnerException.StackTrace);
                        }
                    }
                    sb.AppendLine(string.Format("Exception: ({0}) {1}", exc.GetType().ToString(), exc.Message));
                    sb.AppendLine(string.Format("Source: {0}", source));
                    if (exc.StackTrace != null && logLevel == LogLevel.Detailed)
                    {
                        sb.AppendLine("Stack Trace: ");
                        sb.AppendLine(exc.StackTrace);
                    }
                    sb.AppendLine();

                    // Get the absolute path to the log file
                    string logFile = string.Format("/logs/{0}-ExceptionLog-{1}.log", HttpContext.Current.Server.MachineName,
                        DateTime.Now.ToString("yyyyMMdd"));
                    logFile = HttpContext.Current.Server.MapPath(logFile);

                    // Open the log file for append and write the log entry
                    sw = new StreamWriter(logFile, true);
                    sw.Write(sb.ToString());
                }
                catch
                {
                    // Ignore exceptions writing to the exception log
                }
                finally
                {
                    if (sw != null) sw.Close();
                }
            }
        }
    }
}

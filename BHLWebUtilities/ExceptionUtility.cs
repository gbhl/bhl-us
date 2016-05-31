using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MOBOT.BHL.Web.Utilities
{
    public class ExceptionUtility
    {
        // Log an Exception
        public static void LogException(Exception exc, string source)
        {
            StreamWriter sw = null;

            try
            {
                // Build the message to log
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("********** {0} **********", DateTime.Now);
                if (exc.InnerException != null)
                {
                    sb.Append("Inner Exception Type: ");
                    sb.AppendLine(exc.InnerException.GetType().ToString());
                    sb.Append("Inner Exception: ");
                    sb.AppendLine(exc.InnerException.Message);
                    sb.Append("Inner Source: ");
                    sb.AppendLine(exc.InnerException.Source);
                    if (exc.InnerException.StackTrace != null)
                    {
                        sb.AppendLine("Inner Stack Trace: ");
                        sb.AppendLine(exc.InnerException.StackTrace);
                    }
                }
                sb.Append("Exception Type: ");
                sb.AppendLine(exc.GetType().ToString());
                sb.AppendLine("Exception: " + exc.Message);
                sb.AppendLine("Source: " + source);
                sb.AppendLine("Stack Trace: ");
                if (exc.StackTrace != null)
                {
                    sb.AppendLine(exc.StackTrace);
                    sb.AppendLine();
                }

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

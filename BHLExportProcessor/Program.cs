using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BHL.Export
{
    class Program
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private static ConfigParms configParms = new ConfigParms();

        static void Main(string[] args)
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!ReadCommandLineArguments()) return;

            // validate config values
            if (!ValidateConfiguration()) return;

            // Process the export
            ExportProcessorFactory factory = new ExportProcessorFactory(configParms.ProcessorToRun, configParms.Processors);
            IBHLExport processor = factory.New();
            processor.Process();

            // Report the results of item/page processing
            ProcessResults(processor.Stats(), processor.Errors());

            LogMessage(string.Format("{0} Processing Complete", configParms.ProcessorToRun));
        }

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        static private bool ReadCommandLineArguments()
        {
            bool returnValue = true;

            string[] args = System.Environment.GetCommandLineArgs();

            // First argument is the EXE name; ignore it
            if (args.Length > 2)
            {
                LogMessage("Too many command line arguments.  Format is 'BHLExportProcessor.exe [processortorun]', where processortorun matches one of the Processors listed in the app.config file.");
                returnValue = false;
            }
            else if (args.Length == 2)
            {
                configParms.ProcessorToRun = args[1];
            }

            return returnValue;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        static private bool ValidateConfiguration()
        {
            bool isValid = true;

            if (configParms.Processors.Count == 0)
            {
                LogMessage("Processor list is missing.  Check configuration file.");
                isValid = false;
            }

            if (!configParms.Processors.ContainsKey(configParms.ProcessorToRun))
            {
                LogMessage(string.Format("ProcessorToRun '{0}' does not match one of the configured Processors.  Check configuration file.", configParms.ProcessorToRun));
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Examine the results of the item/page processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        static private void ProcessResults(Dictionary<string,int> stats, List<string> errors)
        {
            try
            {
                // send email with process results to Exchange group
                if (stats.Count > 0 || errors.Count > 0)
                {
                    LogMessage("Sending Email....");
                    string message = GetEmailBody(stats, errors);
                    LogMessage(message);
                    SendEmail(message, errors.Count > 0);
                }
                else
                {
                    LogMessage("Nothing processed.  Email not sent.");
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception sending email.", ex);
                return;
            }
        }

        /// <summary>
        /// Constructs the body of an email message to be sent
        /// </summary>
        /// <returns>Body of email message to be sent</returns>
        static private string GetEmailBody(Dictionary<string, int> stats, List<string> errors)
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            sb.Append(string.Format("BHLExportProcessor: Processing {0} export on {1} complete.{2}", configParms.ProcessorToRun, thisComputer, endOfLine));
            foreach (KeyValuePair<string, int> kvp in stats)
            {
                sb.Append(string.Format("{0}Processed {1} {2}{0}", endOfLine, kvp.Value.ToString(), kvp.Key));
            }
            if (errors.Count > 0)
            {
                sb.Append(string.Format("{0}{1} Errors Occurred{0}See the log file for details", endOfLine, errors.Count.ToString()));
                foreach (string message in errors)
                {
                    sb.Append(message + endOfLine);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Send the specified email message 
        /// </summary>
        /// <param name="message">Body of the message to be sent</param>
        static private void SendEmail(String message, bool withErrors)
        {
            try
            {
                string thisComputer = Environment.MachineName;

                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress(configParms.EmailFromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(configParms.EmailToAddress);
                mailMessage.Body = message;

                if (!withErrors)
                {
                    mailMessage.Subject = string.Format("{0} export: Processing on {1} completed successfully.", configParms.ProcessorToRun, thisComputer);
                }
                else
                {
                    mailMessage.Subject = string.Format("{0} export: Processing on {1} completed with errors.", configParms.ProcessorToRun, thisComputer);
                }

                SmtpClient smtpClient = new SmtpClient(configParms.SMTPHost);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                log.Error("Email Exception: ", ex);
            }
        }

        static private void LogMessage(string message)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(string.Format("{0}\r\n", message));
        }
    }
}

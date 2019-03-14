using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace BHL.Export
{
    internal class ExportEngine
    {
        private ExportLogger _log = null;
        private ConfigParms _configParms = new ConfigParms();

        public void DoExport()
        {
            // Load app settings from the configuration file and set up the logger
            _configParms.LoadAppConfig();
            _log = new ExportLogger(_configParms.LogToFile, _configParms.LogToConsole);

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!ReadCommandLineArguments()) return;

            // validate config values
            if (!ValidateConfiguration()) return;

            _log.Info(string.Format("{0} Processing Starting", _configParms.ProcessorToRun));

            // Process the export
            ExportProcessorFactory factory = new ExportProcessorFactory(_configParms.Processors);
            IBHLExport processor = factory.New(_configParms.ProcessorToRun);
            processor.Process();

            // Report the results of item/page processing
            ProcessResults(processor.Stats(), processor.Errors());

            _log.Info(string.Format("{0} Processing Complete", _configParms.ProcessorToRun));
        }

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        private bool ReadCommandLineArguments()
        {
            bool returnValue = true;

            string[] args = System.Environment.GetCommandLineArgs();

            // First argument is the EXE name; ignore it
            if (args.Length > 2)
            {
                _log.Info("Too many command line arguments.  Format is 'BHLExportProcessor.exe [processortorun]', where processortorun matches one of the Processors listed in the app.config file.");
                returnValue = false;
            }
            else if (args.Length == 2)
            {
                _configParms.ProcessorToRun = args[1];
            }

            return returnValue;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            bool isValid = true;

            if (_configParms.Processors.Count == 0)
            {
                _log.Info("Processor list is missing.  Check configuration file.");
                isValid = false;
            }

            if (!_configParms.Processors.ContainsKey(_configParms.ProcessorToRun))
            {
                _log.Info(string.Format("ProcessorToRun '{0}' does not match one of the configured Processors.  Check configuration file.", _configParms.ProcessorToRun));
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Examine the results of the item/page processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults(Dictionary<string, int> stats, List<string> errors)
        {
            try
            {
                // send email with process results to Exchange group
                if (stats.Count > 0 || errors.Count > 0)
                {
                    _log.Info("Sending Email....");
                    string message = GetEmailBody(stats, errors);
                    _log.Info(message);
                    SendEmail(message, errors.Count > 0);
                }
                else
                {
                    _log.Info("Nothing processed.  Email not sent.");
                }
            }
            catch (Exception ex)
            {
                _log.Error("Exception sending email.", ex);
                return;
            }
        }

        /// <summary>
        /// Constructs the body of an email message to be sent
        /// </summary>
        /// <returns>Body of email message to be sent</returns>
        private string GetEmailBody(Dictionary<string, int> stats, List<string> errors)
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            sb.Append(string.Format("BHLExportProcessor: Processing {0} export on {1} complete.{2}", _configParms.ProcessorToRun, thisComputer, endOfLine));
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
        private void SendEmail(String message, bool withErrors)
        {
            try
            {
                string thisComputer = Environment.MachineName;

                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress(_configParms.EmailFromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(_configParms.EmailToAddress);
                mailMessage.Body = message;

                if (!withErrors)
                {
                    mailMessage.Subject = string.Format("{0} export: Processing on {1} completed successfully.", _configParms.ProcessorToRun, thisComputer);
                }
                else
                {
                    mailMessage.Subject = string.Format("{0} export: Processing on {1} completed with errors.", _configParms.ProcessorToRun, thisComputer);
                }

                SmtpClient smtpClient = new SmtpClient(_configParms.SMTPHost);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                _log.Error("Email Exception: ", ex);
            }
        }
    }
}

using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Security.Authentication.ExtendedProtection;
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
                _configParms.ProcessorToRun = args[1].ToUpper();
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
                string message;
                string serviceName = "BHLExportProcessor";
                if (stats.Count > 0 || errors.Count > 0)
                {
                    _log.Info("Sending Email....");
                    message = GetEmailBody(stats, errors);
                    _log.Info(message);
                    SendServiceLog(serviceName, message, errors);
                    SendEmail(serviceName, message, errors);
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
        private void SendEmail(string serviceName, string message, List<string> errors)
        {
            try
            {
                if (errors.Count > 0 && _configParms.EmailOnError)
                {
                    MailRequestModel mailRequest = new MailRequestModel();
                    mailRequest.Subject = string.Format("{0}-{1} export: Processing on {2} completed {3}.",
                        serviceName,
                        _configParms.ProcessorToRun,
                        Environment.MachineName,
                        errors.Count > 0 ? "with errors" : "successfully"); ;
                    mailRequest.Body = message;
                    mailRequest.From = _configParms.EmailFromAddress;

                    List<string> recipients = new List<string>();
                    foreach (string recipient in _configParms.EmailToAddress.Split(',')) recipients.Add(recipient);
                    mailRequest.To = recipients;

                    EmailClient restClient = new EmailClient(_configParms.BHLWSEndpoint);
                    restClient.SendEmail(mailRequest);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Email Exception: ", ex);
            }
        }

        /// <summary>
        /// Send the specified message to the log table in the database
        /// </summary>
        /// <param name="serviceName">Name of the service being logged</param>
        /// <param name="message">Body of the message to be sent</param>
        private void SendServiceLog(string serviceName, string message, List<string> errors)
        {
            try
            {
                ServiceLogModel serviceLog = new ServiceLogModel();
                serviceLog.Servicename = serviceName;
                serviceLog.Serviceparam = _configParms.ProcessorToRun;
                serviceLog.Logdate = DateTime.Now;
                serviceLog.Severityname = (errors.Count > 0 ? "Error" : "Information");
                serviceLog.Message = string.Format("Processing on {0} completed.\n\r{1}", Environment.MachineName, message);

                ServiceLogsClient restClient = new ServiceLogsClient(_configParms.BHLWSEndpoint);
                restClient.InsertServiceLog(serviceLog);
            }
            catch (Exception ex)
            {
                _log.Error("Service Log Exception: ", ex);
            }
        }
    }
}

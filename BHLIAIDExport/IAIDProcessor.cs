using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace MOBOT.BHL.BHLIAIDExport
{
    public class IAIDProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> processedIdentifiers = new List<string>();
        private List<string> errorMessages = new List<string>();

        /// <summary>
        /// Read and validate configuration parameters, and initiate the appropriate
        /// processor.
        /// </summary>
        public void Process()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Generate the list of identifiers
            this.ProcessIdentifiers();

            // Report the results of processing
            this.ProcessResults();

            this.LogMessage("BHLIAIDExport Processing Complete");
        }

        /// <summary>
        /// Get the list of identifiers and write it to a file.
        /// </summary>
        private void ProcessIdentifiers()
        {
            try
            {
                this.LogMessage("Exporting identifiers...");

                // Get the list of identifiers
                BHLWS.BHLWSSoapClient client = new BHLWS.BHLWSSoapClient();
                string[] identifiers = client.ExportIAIdentifiers();

                // Build the content of the file
                StringBuilder sb = new StringBuilder();
                foreach(string identifier in identifiers)
                {
                    sb.AppendLine(identifier);
                    this.processedIdentifiers.Add(identifier);
                }

                // Write the content to the file
                if (!Directory.Exists(configParms.IAIDFolder)) Directory.CreateDirectory(configParms.IAIDFolder);
                File.WriteAllText(string.Format("{0}\\{1}", configParms.IAIDFolder, configParms.IAIDFile), sb.ToString());

                this.LogMessage(string.Format("{0} identifier(s) exported", this.processedIdentifiers.Count()));
            }
            catch (Exception ex)
            {
                log.Error("Exception processing identifiers.", ex);
                errorMessages.Add("Exception processing identifiers:  " + ex.Message);
            }
        }

        /// <summary>
        /// Examine the results of the item/page processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // send email with process results to Exchange group
                if (errorMessages.Count > 0)
                {
                    this.LogMessage("Sending Email....");
                    string message = this.GetEmailBody();
                    this.LogMessage(message);
                    this.SendEmail(message);
                }
                else
                {
                    this.LogMessage("No errors.  Email not sent.");
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
        private string GetEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            sb.Append("BHLBibTeXExport: Processing  on " + thisComputer + " complete." + endOfLine);
            if (this.errorMessages.Count > 0)
            {
                sb.Append(endOfLine + this.errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details" + endOfLine);
                foreach (string message in errorMessages)
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
        private void SendEmail(string message)
        {
            try
            {
                string thisComputer = Environment.MachineName;
                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress(configParms.EmailFromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(configParms.EmailToAddress);
                if (this.errorMessages.Count > 0)
                {
                    mailMessage.Subject = "BHLIAIDExport: Processing on " + thisComputer + " completed with errors.";
                    mailMessage.Body = message;

                    SmtpClient smtpClient = new SmtpClient(configParms.SMTPHost);
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                log.Error("Email Exception: ", ex);
            }
        }

        private void LogMessage(string message)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");
        }
    }
}

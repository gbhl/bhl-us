using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;
using CustomDataAccess;
using System.Net;
using System.IO;
using System.Xml;
using MOBOT.BHL.OAI2;

namespace BHLOAIHarvester
{
    public class HarvestProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> itemsHarvested = new List<string>();
        private List<string> itemsPublished = new List<string>();
        private List<string> errorMessages = new List<string>();

        public void Harvest()
        {
            this.LogMessage("BHLOAIHarvester Processing Start");

            try
            {
                // Load app settings from the configuration file
                configParms.LoadAppConfig();
            }
            catch (Exception e)
            {
                this.LogMessage("LoadAppConfig Error: " + e.Message, true);
            }

            if (errorMessages.Count == 0)
            {
                // Read additional app settings from the command line
                // Note: Command line arguments override configuration file settings
                if (!this.ReadCommandLineArguments()) return;

                // Validate config values
                if (!this.ValidateConfiguration()) return;

                // Process each OAI set
                CustomGenericList<vwOAIHarvestSet> sets = new BHLImportProvider().OAIHarvestSetSelectAll();

                Dictionary<string, string> formats = new Dictionary<string, string>();
                foreach (vwOAIHarvestSet set in sets) formats.Add(set.Prefix, set.AssemblyName);
                foreach(vwOAIHarvestSet set in sets)
                {
                    HarvestSet(set, formats);
                }
            }

            // Report the results
            this.ProcessResults();

            this.LogMessage("BHLOAIHarvester Processing Complete");
        }

        private void HarvestSet(vwOAIHarvestSet set, Dictionary<string, string> formats)
        {
            this.LogMessage(string.Format("Begin harvesting of {0} ({1})", set.RepositoryName, set.SetName));

            try
            {
                OAI2Harvester harvester = new OAI2Harvester(set.BaseUrl, 
                    "BHL OAI Harvester", "biodiversitylibrary@gmail.com", formats);

                DateTime responseDate;
                string resumptionToken = string.Empty;
                do
                {
                    // 0. Get the from and until dates for this harvest set

                    


                    // 1. Make an OAI GetRecords request
                    OAIHarvestResult oaiResults = harvester.ListRecords(set.Prefix, set.SetSpec, "", "");

                    // 2. Save the records returned from the OAI service to the database
                    if (oaiResults.ResponseMessage == "ok")
                    {
                        foreach (OAIRecord oaiRecord in (List<OAIRecord>)oaiResults.Content)
                        {


                            // TODO: Save the information obtained from the OAI service


                        }
                    }


                    // 3. Continue until we have no more resumption tokens
                    responseDate = oaiResults.ResponseDate;
                    resumptionToken = oaiResults.ResumptionToken;

                } while (!string.IsNullOrWhiteSpace(resumptionToken));


                // 4. Log the OAI results





            }
            catch (Exception ex)
            {
                LogMessage(string.Format("Error harvesting {0} ({1})", set.RepositoryName, set.SetName), ex);

                // TODO: Clear out just-harvested records if failure due to OAI service error
            }

            this.LogMessage(string.Format("Finished harvesting of {0} ({1})", set.RepositoryName, set.SetName));
        }

        #region Utility methods

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        private bool ReadCommandLineArguments()
        {
            return true;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            return true;
        }

        #endregion Utility methods

        #region Logging

        private void LogMessage(string message)
        {
            this.LogMessage(message, false);
        }

        private void LogMessage(string message, bool isError)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");

            // If this is an error message, add it to the in-memory list of error messages
            if (isError) errorMessages.Add(message);
        }

        private void LogMessage(string message, Exception ex)
        {
            // Get the innermost exception
            while (ex.InnerException != null) ex = ex.InnerException;
            LogMessage(message + " - " + ex.Message, true);
        }

        #endregion Logging

        #region Process results

        /// <summary>
        /// Examine the results of the item/page processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // Send email if PDFS were deleted, or if an error occurred.
                // Don't send an email each time a PDF is generated.
                if (itemsHarvested.Count > 0 || itemsPublished.Count > 0 || errorMessages.Count > 0)
                {
                    String subject = String.Empty;
                    String thisComputer = Environment.MachineName;
                    if (this.errorMessages.Count == 0)
                    {
                        subject = "BHLOAIHarvester: OAI harvesting on " + thisComputer + " completed successfully.";
                    }
                    else
                    {
                        subject = "BHLOAIHarvester: OAI harvesting on " + thisComputer + " completed with errors.";
                    }

                    this.LogMessage("Sending Email....");
                    String message = this.GetCompletionEmailBody();
                    this.LogMessage(message);
                    this.SendEmail(subject, message, configParms.EmailFromAddress, configParms.EmailToAddress, "");
                }
                else
                {
                    this.LogMessage("Nothing to do.  Email not sent.");
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
        private String GetCompletionEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            sb.Append("BHLOAIHarvester: OAI harvesting on " + thisComputer + " complete." + endOfLine);
            if (this.itemsHarvested.Count > 0)
            {
                sb.Append(endOfLine + this.itemsHarvested.Count.ToString() + " Items were Harvested" + endOfLine);
            }
            if (this.itemsPublished.Count > 0)
            {
                sb.Append(endOfLine + this.itemsPublished.Count.ToString() + " Items were Published to Production" + endOfLine);
            }
            if (this.errorMessages.Count > 0)
            {
                sb.Append(endOfLine + this.errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details" + endOfLine + endOfLine);
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
        private void SendEmail(String subject, String message, String fromAddress,
            String toAddress, String ccAddresses)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress(fromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(toAddress);
                if (ccAddresses != String.Empty) mailMessage.CC.Add(ccAddresses);
                mailMessage.Subject = subject;
                mailMessage.Body = message;

                SmtpClient smtpClient = new SmtpClient(configParms.SMTPHost);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                LogMessage("Email Exception.", ex);
            }
        }

        #endregion Process results
    }
}

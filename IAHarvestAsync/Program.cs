using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Xml;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace IAHarvestAsync
{
    internal class Program
    {
        // Create a logger
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static ConfigParms configParms = new ConfigParms();
        private static List<string> retrievedIds = new List<string>();
        private static List<string> processedItems = new List<string>();
        private static List<string> errorMessages = new List<string>();

        // Create an IAHarvestProvider for use in this class
        static BHLImportProvider provider = new BHLImportProvider();

        static void Main(string[] args)
        {
            configParms.LoadAppConfig();
            if (configParms.DownloadAll)
            {
                GetSetItems();
                GetExtraSetItems();
            }
            HarvestData();
            ProcessResults();
            LogMessage("IAHarvestAsync Processing Complete");
        }

        /// <summary>
        /// Get all of the items for the Internet Archive sets that are marked as "DownloadAll"
        /// in the Set table.
        /// </summary>
        private static void GetSetItems()
        {
            try
            {
                LogMessage("Getting new and updated items from Internet Archive");

                List<IASet> sets = provider.IASetSelectForDownload();
                foreach (IASet set in sets)
                {
                    GetItem(set.SetSpecification, set.SetID, set.LastDownloadDate);
                    if (retrievedIds.Count > 0) provider.IASetUpdateLastDownloadDate(set.SetID, true);
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception getting new and updated items.", ex);
                errorMessages.Add("Exception getting new and updated items:  " + ex.Message);
            }

        }

        /// <summary>
        /// Get the items from other sets that have been identified as BHL-worthy
        /// </summary>
        private static void GetExtraSetItems()
        {
            try
            {
                LogMessage("Getting IAAnalysis items");
                provider.IAItemInsertFromIAAnalysis(configParms.LocalFileFolder);
            }
            catch (Exception ex)
            {
                log.Error("Exception getting IAAnalysis items.", ex);
                errorMessages.Add("Exception getting IAAnalysis items:  " + ex.Message);
            }
        }

        /// <summary>
        /// Get the item associated with the specified Internet Archive identifier.
        /// </summary>
        private static void GetItem(string itemIdentifier, int? setID, DateTime? startDate)
        {
            try
            {
                LogMessage("Getting item: " + itemIdentifier);

                string startDateString = (startDate == null) ? "1900-01-01" : ((DateTime)startDate).ToString("yyyy-MM-dd");
                string endDateString = DateTime.Now.ToString("yyyy-MM-dd");
                string url = String.Format(configParms.SearchListIdentifiersUrl, itemIdentifier, startDateString, endDateString);

                // Get the OAI headers for this item or set
                XmlDocument xml = provider.GetIAXmlData(url);

                XmlNodeList identifiers = xml.SelectNodes("//doc");

                foreach (XmlNode identifier in identifiers)
                {
                    XmlNode id = identifier.SelectSingleNode("str[@name = 'identifier']");
                    XmlNode updateDates = identifier.SelectSingleNode("arr[@name = 'oai_updatedate']");
                    XmlNode updateDate = updateDates.LastChild;

                    // Save the item identifier (and associate it with a set if necessary)
                    IAItem item = provider.SaveIAItemID(id.InnerText, configParms.LocalFileFolder, Convert.ToDateTime(updateDate.InnerText));
                    if (setID != null) provider.SaveIAItemSet(item.ItemID, (int)setID);
                    retrievedIds.Add(identifier.InnerText);
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception getting item: " + itemIdentifier, ex);
                errorMessages.Add("Exception getting item: " + itemIdentifier + "  " + ex.Message);
            }
        }

        private static void HarvestData()
        {
            try
            {
                List<IAItem> items = provider.IAItemSelectForXMLDownload("");

                LogMessage(string.Format("Harvesting data for {0} items", items.Count.ToString()));

                while (items.Count > 0)
                {
                    int runningProcesses = Process.GetProcessesByName(configParms.IAHarvestProcessName).Length;
                    if (runningProcesses < configParms.IAHarvestMaxInstances)
                    {
                        IAItem item = items[0];
                        LogMessage(string.Format("Harvesting data for {0}", item.IAIdentifier));

                        string arguments = string.Format("/ITEM:{0} /DOWNLOAD:{1} /UPLOAD:{2} /QUIET:{3}", 
                            item.IAIdentifier, configParms.Download, configParms.Upload, configParms.Quiet);
                        ProcessStartInfo startInfo = new ProcessStartInfo { FileName = configParms.IAHarvestExecutable, Arguments = arguments };
                        Process.Start(startInfo);

                        processedItems.Add(item.IAIdentifier);
                        items.RemoveAt(0);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception harvesting data", ex);
                errorMessages.Add("Exception harvesting data " + ex.Message);
            }
        }

        /// <summary>
        /// Examine the results of the process and take the appropriate actions (log, send email, do nothing).
        /// </summary>
        private static void ProcessResults()
        {
            try
            {
                // send email with process results
                if (retrievedIds.Count > 0 || processedItems.Count > 0 || errorMessages.Count > 0)
                {
                    LogMessage("Sending Email....");
                    string message = GetEmailBody();
                    LogMessage(message);
                    SendEmail(message);
                }
                else
                {
                    LogMessage("No items processed.  Email not sent.");
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
        private static string GetEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            sb.Append("IAHarvestAsync: IA Harvesting on " + Environment.MachineName + " complete." + endOfLine);
            if (retrievedIds.Count > 0) sb.Append(endOfLine + "Retrieved " + retrievedIds.Count.ToString() + " Identifiers" + endOfLine);
            if (processedItems.Count > 0) sb.Append(endOfLine + "Processed " + processedItems.Count.ToString() + " Identifiers" + endOfLine);
            if (errorMessages.Count > 0)
            {
                sb.Append(endOfLine + errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details" + endOfLine);
                foreach (string message in errorMessages) sb.Append(endOfLine + message + endOfLine);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Send the specified email message 
        /// </summary>
        /// <param name="message">Body of the message to be sent</param>
        private static void SendEmail(string message)
        {
            try
            {
                MailRequestModel mailRequest = new MailRequestModel();
                mailRequest.Subject = string.Format("IAHarvestAsync: IA Harvesting on {0} completed with {1} errors.", Environment.MachineName, errorMessages.Count.ToString());
                mailRequest.Body = message;
                mailRequest.From = configParms.EmailFromAddress;

                List<string> recipients = new List<string>();
                foreach (string recipient in configParms.EmailToAddress.Split(',')) recipients.Add(recipient);
                mailRequest.To = recipients;

                EmailClient restClient = new EmailClient(configParms.BHLWSEndpoint);
                restClient.SendEmail(mailRequest);
            }
            catch (Exception ex)
            {
                log.Error("Email Exception: ", ex);
            }
        }

        private static void LogMessage(string message)
        {
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");
        }
    }
}

﻿using MOBOT.IAAnalysis.DataObjects;
using MOBOT.IAAnalysis.Server;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Xml;

namespace IAAnalysisHarvest
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
        private List<string> retrievedIds = new List<string>();
        private List<string> harvestedXml = new List<string>();
        private List<string> errorMessages = new List<string>();

        // Create an HarvestProvider for use in this class
        IAAnalysisProvider provider = new IAAnalysisProvider();

        public void Process()
        {
            // Load the app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            // Do processing
            if (configParms.DownloadIDs) this.GetItems();
            if (configParms.GetXml) this.HarvestXMLInformation();

            // Report the results of processing
            this.ProcessResults();

            this.LogMessage("IAAnalysisHarvest Processing Complete");
        }

        #region Get basic Item information

        private void GetItems()
        {
            try
            {
                this.LogMessage("Downloading Items");

                // Get the most recent identifiers from IA, starting 30 days prior to today
                DateTime monthPrior = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
                DateTime startDate = new DateTime(monthPrior.Year, monthPrior.Month, 1);
                DateTime endDate = startDate.AddMonths(1);

                while(startDate.CompareTo(DateTime.Now) < 0)
                {
                    this.LogMessage("Downloading items modified between " + startDate.ToString("MM/dd/yyyy") + " and " + endDate.ToString("MM/dd/yyyy") + ".");
                    String url = String.Format(configParms.SearchListIdentifiersUrl, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
                    XmlDocument xml = provider.GetIAXmlData(url);

                    XmlNodeList identifiers = xml.SelectNodes("//doc");
                    foreach (XmlNode identifier in identifiers)
                    {
                        XmlNode id = identifier.SelectSingleNode("str[@name = 'identifier']");
                        Item item = provider.SaveItemIdentifier(id.InnerText);
                        retrievedIds.Add(id.InnerText);
                    }

                    startDate = endDate;
                    endDate = endDate.AddMonths(1);
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception downloading items.", ex);
                errorMessages.Add("Exception downloading items:  " + ex.Message);
            }
        }

        #endregion Get basic Item information

        #region Harvest XML

        /// <summary>
        /// Get the XML information for the new or updated items.  This method
        /// will download the XML files from Internet Archive, store it locally,
        /// and then parse the data from the files.
        /// </summary>
        private void HarvestXMLInformation()
        {
            try
            {
                this.LogMessage("Harvesting XML information");
                this.LogMessage("Harvesting Items");

                // Download the XML files for each item and parse the data into the database
                List<Item> items = provider.ItemSelectForXMLDownload();

                foreach (Item item in items)
                {
                    bool gotMeta = false;
                    bool gotMarc = false;

                    try
                    {
                        if (item.MetaGetStatus != "OK")
                        {
                            string metaGetStatus = this.HarvestMetadata(item.ItemID, item.Identifier);
                            provider.ItemUpdateMetaGetStatus(item.ItemID, metaGetStatus);
                        }
                        gotMeta = true;

                        if (item.MarcGetStatus != "OK")
                        {
                            string marcGetStatus = this.HarvestMarcData(item.ItemID, item.Identifier);
                            provider.ItemUpdateMarcGetStatus(item.ItemID, marcGetStatus);
                        }
                        gotMarc = true;

                        provider.ItemUpdateItemStatusIDAfterDataHarvest(item.ItemID);
                        this.harvestedXml.Add(item.Identifier);
                    }
                    catch (Exception ex)
                    {
                        if (!gotMeta) provider.ItemUpdateMetaGetStatus(item.ItemID, "Error");
                        else if (!gotMarc) provider.ItemUpdateMarcGetStatus(item.ItemID, "Error");

                        log.Error("Exception harvesting XML information for " + item.Identifier, ex);
                        errorMessages.Add("Exception harvesting XML information for " + item.Identifier + "  " + ex.Message);
                        // don't rethrow; we want to continue processing
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception harvesting XML information", ex);
                errorMessages.Add("Exception harvesting XML information  " + ex.Message);
            }
        }

        private string HarvestMetadata(int itemID, string identifier)
        {
            String returnCode = "OK";

            this.LogMessage("Harvesting metadata for " + identifier);

            // Read the XML file from IA
            XmlDocument xml = null;
            try
            {
                xml = provider.GetIAXmlData("https://www.archive.org/download/" + identifier + "/" + identifier + configParms.MetadataExtension);
            }
            catch (System.Net.WebException wex)
            {
                // Capture the Http error code
                System.Net.HttpWebResponse errResp = wex.Response as System.Net.HttpWebResponse;
                if (errResp == null)
                {
                    returnCode = (wex.Status == null) ? "ERR:" + wex.Message : wex.Status.ToString();
                }
                else
                {
                    returnCode = errResp.StatusCode.ToString();
                }
            }

            if (xml != null)
            {
                // Read additional elements
                String sponsor = String.Empty;
                String contributor = String.Empty;
                String scanningCenter = String.Empty;
                String collectionLibrary = String.Empty;
                String callNumber = String.Empty;
                int imageCount = 0;
                String curation;
                String curationState = String.Empty;
                String possibleCopyrightStatus = String.Empty;
                String volume = String.Empty;
                String scanDate = String.Empty;
                DateTime? addedDate = null;
                DateTime? publicDate = null;
                DateTime? updateDate = null;
                String sponsorDate = String.Empty;

                XmlNode element = xml.SelectSingleNode("metadata/sponsor");
                if (element != null) sponsor = element.InnerText;
                element = xml.SelectSingleNode("metadata/contributor");
                if (element != null) contributor = element.InnerText;
                element = xml.SelectSingleNode("metadata/scanningcenter");
                if (element != null) scanningCenter = element.InnerText;
                element = xml.SelectSingleNode("metadata/collection-library");
                if (element != null) collectionLibrary = element.InnerText;
                element = xml.SelectSingleNode("metadata/call_number");
                if (element != null) callNumber = element.InnerText;
                element = xml.SelectSingleNode("metadata/imagecount");
                if (element != null) Int32.TryParse(element.InnerText, out imageCount);

                element = xml.SelectSingleNode("metadata/curation");
                if (element != null)
                {
                    curation = element.InnerText;
                    if (curation.ToLower().Contains("[state]approved[/state]")) curationState = "approved";
                    int startStateTag = curation.ToLower().IndexOf("[state]");
                    int endStateTag = curation.ToLower().IndexOf("[/state]");
                    if ((startStateTag > -1) && ((startStateTag + 7) < endStateTag))
                    {
                        curationState = curation.Substring(startStateTag + 7, endStateTag - (startStateTag + 7)).ToLower();
                    }
                }

                element = xml.SelectSingleNode("metadata/possible-copyright-status");
                if (element != null) possibleCopyrightStatus = element.InnerText;
                element = xml.SelectSingleNode("metadata/volume");
                if (element != null) volume = element.InnerText;
                element = xml.SelectSingleNode("metadata/scandate");
                if (element != null) scanDate = element.InnerText;
                element = xml.SelectSingleNode("metadata/addeddate");
                if (element != null) addedDate = DateParse(element.InnerText);
                element = xml.SelectSingleNode("metadata/publicdate");
                if (element != null) publicDate = DateParse(element.InnerText);
                element = xml.SelectSingleNode("metadata/updatedate");
                if (element != null) updateDate = DateParse(element.InnerText);
                element = xml.SelectSingleNode("metadata/sponsordate");
                if (element != null) sponsorDate = element.InnerText;

                provider.ItemUpdateMetadata(itemID, sponsor, contributor, scanningCenter,
                    collectionLibrary, callNumber, imageCount, curationState, possibleCopyrightStatus,
                    volume, scanDate, addedDate, publicDate, updateDate, sponsorDate);

                // Read the set information
                XmlNodeList collections = xml.SelectNodes("metadata/collection");
                foreach (XmlNode collectionNode in collections)
                {
                    String collectionValue = collectionNode.Name + ":" + collectionNode.InnerText;
                    Collection collection = provider.SaveCollection(collectionValue);
                    provider.SaveItemCollection(itemID, collection.CollectionID);
                }
            }

            return returnCode;
        }

        private DateTime? DateParse(String dateString)
        {
            DateTime parsedDate;
            DateTime? returnDate = null;
            if (DateTime.TryParse(dateString, out parsedDate))
            {
                returnDate = parsedDate;
            }
            return returnDate;
        }

        private string HarvestMarcData(int itemID, string identifier)
        {
            String returnCode = "OK";

            this.LogMessage("Harvesting MARC data for " + identifier);

            // Read the XML file from IA
            XmlDocument xml = null;
            try
            {
                xml = provider.GetIAXmlData("https://www.archive.org/download/" + identifier + "/" + identifier + configParms.MarcExtension);
            }
            catch (System.Net.WebException wex)
            {
                // Capture the Http error code
                System.Net.HttpWebResponse errResp = wex.Response as System.Net.HttpWebResponse;
                if (errResp == null)
                {
                    returnCode = (wex.Status == null) ? "ERR:" + wex.Message : wex.Status.ToString();
                }
                else
                {
                    returnCode = errResp.StatusCode.ToString();
                }
            }

            if (xml != null)
            {
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("ns", "http://www.loc.gov/MARC21/slim");

                // Insert or update the root Marc information
                String leader = String.Empty;
                XmlNode marcNode = xml.SelectSingleNode("//ns:record/ns:leader", nsmgr);
                if (marcNode != null) leader = marcNode.InnerText;

                // Save MARC Leader on the Item record
                provider.ItemUpdateMARCLeader(itemID, leader);

                // Insert the new Marc control information
                XmlNodeList controlFields = xml.SelectNodes("//ns:record/ns:controlfield", nsmgr);
                foreach (XmlNode controlField in controlFields)
                {
                    String tag = (controlField.Attributes["tag"] == null) ? String.Empty : controlField.Attributes["tag"].Value;
                    String value = controlField.InnerText;
                    provider.MarcControlInsertAuto(itemID, tag, value);
                }

                // Insert the new Marc data field and subfield information
                XmlNodeList dataFields = xml.SelectNodes("//ns:record/ns:datafield", nsmgr);
                foreach (XmlNode dataField in dataFields)
                {
                    String tag = (dataField.Attributes["tag"] == null) ? String.Empty : dataField.Attributes["tag"].Value;
                    String indicator1 = (dataField.Attributes["ind1"] == null) ? String.Empty : dataField.Attributes["ind1"].Value;
                    String indicator2 = (dataField.Attributes["ind2"] == null) ? String.Empty : dataField.Attributes["ind2"].Value;
                    MarcDataField marcDataField = provider.MarcDataFieldInsertAuto(itemID, tag, indicator1, indicator2);

                    XmlNodeList subFields = dataField.SelectNodes("ns:subfield", nsmgr);
                    foreach (XmlNode subField in subFields)
                    {
                        String code = (subField.Attributes["code"] == null) ? String.Empty : subField.Attributes["code"].Value;
                        String value = subField.InnerText;
                        provider.MarcSubFieldInsertAuto(marcDataField.MarcDataFieldID, code, value);
                    }
                }
            }

            return returnCode;
        }

        #endregion Harvest XML

        #region Process Results

        /// <summary>
        /// Examine the results of the process and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // send email with process results to Exchange group
                if (retrievedIds.Count > 0 || harvestedXml.Count > 0 ||
                    errorMessages.Count > 0)
                {
                    this.LogMessage("Sending Email....");
                    string message = this.GetEmailBody();
                    this.LogMessage(message);
                    this.SendEmail(message);
                }
                else
                {
                    this.LogMessage("No items or pages processed.  Email not sent.");
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception sending email.", ex);
                return;
            }
        }

        #endregion Process Results

        #region Utility methods

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        private bool ReadCommandLineArguments()
        {
            string[] args = System.Environment.GetCommandLineArgs();

            if (args.Length == 1) return true;

            for (int x = 1; x < args.Length; x++)
            {
                string[] split = args[x].Split(':');
                if (split.Length != 2)
                {
                    this.LogMessage("Invalid command line format.  Format is IAAnalysisHarvest.exe [/DOWNLOADID:truefalse] [/GETXML:truefalse]");
                    return false;
                }

                if (String.Compare(split[0], "/DOWNLOADID", true) == 0) configParms.DownloadIDs = Convert.ToBoolean(split[1]);
                if (String.Compare(split[0], "/GETXML", true) == 0) configParms.GetXml = Convert.ToBoolean(split[1]);
            }

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

        /// <summary>
        /// Constructs the body of an email message to be sent
        /// </summary>
        /// <returns>Body of email message to be sent</returns>
        private string GetEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            sb.Append("IAAnalysisHarvest: IA Analysis Harvesting on " + thisComputer + " complete." + endOfLine);
            if (this.retrievedIds.Count > 0)
            {
                sb.Append(endOfLine + "Retrieved " + this.retrievedIds.Count.ToString() + " Identifiers" + endOfLine);
            }
            if (this.harvestedXml.Count > 0)
            {
                sb.Append(endOfLine + "Harvested data for " + this.harvestedXml.Count.ToString() + " Identifiers" + endOfLine);
            }
            if (this.errorMessages.Count > 0)
            {
                sb.Append(endOfLine + this.errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details" + endOfLine);
                foreach (string message in errorMessages)
                {
                    sb.Append(endOfLine + message + endOfLine);
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
                if (this.errorMessages.Count == 0)
                {
                    mailMessage.Subject = "IAAnalysisHarvest: IA Analysis Harvesting on " + thisComputer + " completed successfully.";
                }
                else
                {
                    mailMessage.Subject = "IAAnalysisHarvest: IA Analysis Harvesting on " + thisComputer + " completed with errors.";
                }
                mailMessage.Body = message;

                SmtpClient smtpClient = new SmtpClient(configParms.SMTPHost);
                smtpClient.Send(mailMessage);
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

        #endregion Utility methods

    }
}

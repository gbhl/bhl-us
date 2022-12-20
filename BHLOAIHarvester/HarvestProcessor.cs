using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using MOBOT.BHL.OAI2;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

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
        private Dictionary<string, List<string>> itemsHarvested = new Dictionary<string, List<string>>();
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

                // Get the sets to be processed.  If a particular harvest set was specified for processing,
                // return all sets (both active and inactive).  This allows us (in the next step) to select
                // the specified set from the list, whether it is active or not.  If no set was specified,
                // only select the active sets.
                List<vwOAIHarvestSet> sets = new BHLImportProvider().OAIHarvestSetSelectAll(string.IsNullOrWhiteSpace(configParms.HarvestSetID));

                Dictionary<string, string> formats = new Dictionary<string, string>();
                foreach (vwOAIHarvestSet set in sets)
                {
                    // If a particular set was specified for harvesting, only process that set.  Otherwise do them all.
                    if (set.HarvestSetID == Convert.ToInt32(configParms.HarvestSetID) ||
                        string.IsNullOrWhiteSpace(configParms.HarvestSetID))
                    {
                        if (!formats.ContainsKey(set.Prefix)) formats.Add(set.Prefix, set.AssemblyName);
                        HarvestSet(set, formats);
                    }
                }
            }

            // Report the results
            this.ProcessResults();

            this.LogMessage("BHLOAIHarvester Processing Complete");
        }

        private void HarvestSet(vwOAIHarvestSet set, Dictionary<string, string> formats)
        {
            this.LogMessage(string.Format("Begin harvesting of \"{0}\"", set.HarvestSetName));

            DateTime harvestStartTime = DateTime.Now;
            DateTime? responseDate = null;
            string responseMessage = string.Empty;
            string fromDate = string.Empty;
            string untilDate = string.Empty;
            int harvestLogID = 0;

            try
            {
                OAI2Harvester harvester = new OAI2Harvester(set.BaseUrl,
                    "BHL OAI Harvester", "biodiversitylibrary@gmail.com", formats);

                string resumptionToken = string.Empty;
                DateTime resumptionExpiration = DateTime.Now.ToUniversalTime().AddHours(1);

                // Get the from and until dates for this harvest set
                GetHarvestDates(set.HarvestSetID, set.Granularity, out fromDate, out untilDate);

                // Add a log record to indicate that the harvest has started
                OAIHarvestLog log = new BHLImportProvider().OAIHarvestLogInsert(set.HarvestSetID, harvestStartTime,
                    Convert.ToDateTime(fromDate), Convert.ToDateTime(untilDate), responseDate, "processing", 0);
                harvestLogID = log.HarvestLogID;

                do
                {
                    // Make an OAI GetRecords request
                    OAIHarvestResult oaiResults = null;
                    if (!string.IsNullOrWhiteSpace(resumptionToken))
                    {
                        // Check resumption expiration date
                        if (DateTime.Now.ToUniversalTime().CompareTo(resumptionExpiration) > 0)
                            throw new Exception(string.Format("resumptionToken \"{0}\" has expired", resumptionToken));

                        oaiResults = harvester.ListRecords(set.Prefix, resumptionToken);
                    }
                    else
                    {
                        oaiResults = harvester.ListRecords(set.Prefix, set.SetSpec, fromDate, untilDate);
                        responseDate = oaiResults.ResponseDate;
                    }

                    // Save the records returned from the OAI service to the database
                    responseMessage = oaiResults.ResponseMessage;
                    if (responseMessage == "ok")
                    {
                        BHLImportProvider provider = new BHLImportProvider();
                        foreach (MOBOT.BHL.OAI2.OAIRecord oaiRecord in (List<MOBOT.BHL.OAI2.OAIRecord>)oaiResults.Content)
                        {
                            // Convert the OAI metadata to a DAL data object
                            MOBOT.BHLImport.DataObjects.OAIRecord oaiDataRecord = ConvertToOAIDataObject(oaiRecord);
                            oaiDataRecord.HarvestLogID = harvestLogID;

                            // Save the OAI metadata
                            provider.Save(oaiDataRecord);

                            UpdateHarvestCount(set.HarvestSetName, oaiRecord.Type.ToString());
                        }
                    }
                    else
                    {
                        throw new Exception(responseMessage);
                    }

                    // Continue until we have no more resumption tokens
                    resumptionToken = oaiResults.ResumptionToken;
                    resumptionExpiration = oaiResults.ResumptionExpiration;

                } while (!string.IsNullOrWhiteSpace(resumptionToken));
            }
            catch (Exception ex)
            {
                LogMessage(string.Format("Error harvesting \"{0}\"", set.HarvestSetName), ex);
                responseMessage = ex.Message;

                // Clear out just-harvested records
                try
                {
                    BHLImportProvider provider = new BHLImportProvider();
                    if (harvestLogID > 0)
                    {
                        provider.OAIRecordDeleteForHarvestLogID(harvestLogID);
                        if (itemsHarvested.ContainsKey(set.HarvestSetName)) itemsHarvested[set.HarvestSetName].Clear();
                    }
                }
                catch (Exception ex2)
                {
                    LogMessage(string.Format("Error clearing just-harvested records and updating the log for \"{0}\"", set.HarvestSetName), ex2);
                }
            }
            finally
            {
                if (harvestLogID > 0)   // Make sure there is a log record to update
                {
                    try
                    {
                        // Log the OAI results
                        var numberHarvested = itemsHarvested.ContainsKey(set.HarvestSetName) ? itemsHarvested[set.HarvestSetName].Count : 0;
                        //var numberHarvested = (from r in itemsHarvested select r.Value.Count).Sum();

                        new BHLImportProvider().OAIHarvestLogUpdate(harvestLogID, set.HarvestSetID,
                            harvestStartTime,
                            (string.IsNullOrWhiteSpace(fromDate) ? Convert.ToDateTime(null) : Convert.ToDateTime(fromDate)),
                            (string.IsNullOrWhiteSpace(untilDate) ? Convert.ToDateTime(null) : Convert.ToDateTime(untilDate)),
                            (responseDate == null ? responseDate : ((DateTime)responseDate).ToLocalTime()),
                            responseMessage, numberHarvested);
                    }
                    catch (Exception ex)
                    {
                        LogMessage(string.Format("Error logging harvesting results for \"{0}\"", set.HarvestSetName), ex);
                    }
                }
            }

            this.LogMessage(string.Format("Finished harvesting of \"{0}\"", set.HarvestSetName));
        }

        /// <summary>
        /// Convert the OAI2 OAIRecord object to a BHL DataObjects OAIRecord object.
        /// </summary>
        /// <param name="oaiRecord"></param>
        /// <returns></returns>
        public MOBOT.BHLImport.DataObjects.OAIRecord ConvertToOAIDataObject(MOBOT.BHL.OAI2.OAIRecord oaiRecord)
        {
            MOBOT.BHLImport.DataObjects.OAIRecord oaiDataRecord = new MOBOT.BHLImport.DataObjects.OAIRecord();

            oaiDataRecord.OAIIdentifier = oaiRecord.OaiIdentifier;
            oaiDataRecord.OAIDateStamp = oaiRecord.OaiDateStamp;
            oaiDataRecord.OAIStatus = oaiRecord.OaiStatus;
            oaiDataRecord.RecordType = oaiRecord.Type.ToString();
            oaiDataRecord.Title = oaiRecord.Title;
            oaiDataRecord.ContainerTitle = oaiRecord.JournalTitle;
            oaiDataRecord.Volume = oaiRecord.JournalVolume;
            oaiDataRecord.Issue = oaiRecord.JournalIssue;
            oaiDataRecord.Edition = oaiRecord.Edition;
            oaiDataRecord.StartPage = oaiRecord.ArticleStartPage;
            oaiDataRecord.EndPage = oaiRecord.ArticleEndPage;
            oaiDataRecord.Date = oaiRecord.Date;
            oaiDataRecord.Language = oaiRecord.Languages.Count > 0 ? oaiRecord.Languages[0].Name : string.Empty;
            oaiDataRecord.Publisher = string.IsNullOrWhiteSpace(oaiRecord.Publisher) && 
                                        !string.IsNullOrWhiteSpace(oaiRecord.PublicationDetails) ? 
                                        oaiRecord.PublicationDetails : oaiRecord.Publisher;
            oaiDataRecord.PublicationPlace = oaiRecord.PublicationPlace;
            oaiDataRecord.PublicationDate = oaiRecord.PublicationDates;
            oaiDataRecord.CallNumber = oaiRecord.CallNumber;
            oaiDataRecord.Issn = oaiRecord.Issn;
            oaiDataRecord.Isbn = oaiRecord.Isbn;
            oaiDataRecord.Lccn = oaiRecord.Llc;
            oaiDataRecord.Doi = oaiRecord.Doi;
            oaiDataRecord.Url = oaiRecord.Url;
            if (oaiRecord.Contributors.Count > 0) oaiDataRecord.Contributor = oaiRecord.Contributors[0];

            foreach (KeyValuePair<string, MOBOT.BHL.OAI2.OAIRecord> relatedTitle in oaiRecord.RelatedTitles)
            {
                OAIRecordRelatedTitle oaiRecordRelatedTitle = new OAIRecordRelatedTitle();
                oaiRecordRelatedTitle.TitleType = relatedTitle.Key;
                oaiRecordRelatedTitle.Title = relatedTitle.Value.Title ?? string.Empty;
                oaiDataRecord.RelatedTitles.Add(oaiRecordRelatedTitle);
            }

            foreach (KeyValuePair<string, MOBOT.BHL.OAI2.OAIRecord.Creator> creator in oaiRecord.Creators)
            {
                OAIRecordCreator oaiRecordCreator = new OAIRecordCreator();
                oaiRecordCreator.CreatorType = creator.Key;
                oaiRecordCreator.FullName = creator.Value.FullName ?? string.Empty;
                oaiRecordCreator.Dates = creator.Value.Dates ?? string.Empty;

                // Parse the start and end dates
                if (oaiRecordCreator.Dates != string.Empty)
                {
                    Regex regex = new Regex("[0-9]{4}", RegexOptions.IgnoreCase);
                    MatchCollection matches = regex.Matches(creator.Value.Dates);
                    if (matches.Count > 0 && matches.Count <= 2) oaiRecordCreator.StartDate = matches[0].Value;
                    if (matches.Count == 2) oaiRecordCreator.EndDate = matches[1].Value;
                }

                foreach (MOBOT.BHL.OAI2.OAIRecord.Identifier identifier in creator.Value.Identifiers)
                {
                    OAIRecordCreatorIdentifier oaiRecordCreatorIdentifier = new OAIRecordCreatorIdentifier();
                    oaiRecordCreatorIdentifier.IdentifierType = identifier.IdentifierType;
                    oaiRecordCreatorIdentifier.IdentifierValue = identifier.IdentifierValue;
                    oaiRecordCreator.Identifiers.Add(oaiRecordCreatorIdentifier);
                }

                oaiDataRecord.Creators.Add(oaiRecordCreator);
            }

            foreach (KeyValuePair<string, string> subject in oaiRecord.Subjects)
            {
                OAIRecordSubject oaiRecordSubject = new OAIRecordSubject();
                oaiRecordSubject.Keyword = subject.Value ?? string.Empty;
                oaiDataRecord.Subjects.Add(oaiRecordSubject);
            }

            foreach (string dcType in oaiRecord.Types)
            {
                OAIRecordDCType oaiRecordDCType = new OAIRecordDCType();
                oaiRecordDCType.DCType = dcType ?? string.Empty;
                oaiDataRecord.DcTypes.Add(oaiRecordDCType);
            }

            foreach (string right in oaiRecord.Rights)
            {
                OAIRecordRight oaiRecordRight = new OAIRecordRight();
                oaiRecordRight.Right = right ?? string.Empty;
                oaiDataRecord.Rights.Add(oaiRecordRight);
            }

            return oaiDataRecord;
        }

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
                    this.LogMessage("Invalid command line format.  Format is BHLOAIHarvester.exe [/HARVESTSET:N [/FROMDATE:YYYY-MM-DD] [/UNTILDATE:YYYY-MM-DD]]");
                    return false;
                }

                if (String.Compare(split[0], "/HARVESTSET", true) == 0) configParms.HarvestSetID = split[1];
                if (String.Compare(split[0], "/FROMDATE", true) == 0) configParms.FromDate = split[1];
                if (String.Compare(split[0], "/UNTILDATE", true) == 0) configParms.UntilDate = split[1];
            }

            return true;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            DateTime tempDate;

            if (string.IsNullOrWhiteSpace(configParms.HarvestSetID) &&
                (!string.IsNullOrWhiteSpace(configParms.FromDate) ||
                 !string.IsNullOrWhiteSpace(configParms.UntilDate)))
            {
                this.LogMessage("No Harvest Set specified. Use /HARVESTSET to identify the set to be harvested.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(configParms.HarvestSetID))
            {
                int tempHarvestSetID;
                if (!(Int32.TryParse(configParms.HarvestSetID, out tempHarvestSetID)))
                {
                    this.LogMessage("Invalid HarvestSet.  Specify the integer identifier for the HarvestSet (from the OAIHarvestSet database table).");
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(configParms.FromDate))
            {
                if (!(DateTime.TryParse(configParms.FromDate, out tempDate)))
                {
                    this.LogMessage("Invalid From Date format.  Use YYYY-MM-DD.");
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(configParms.UntilDate))
            {
                if (!(DateTime.TryParse(configParms.UntilDate, out tempDate)))
                {
                    this.LogMessage("Invalid Until Date format.  Use YYYY-MM-DD.");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get the FromDate and UntilDate string for the specified harvest set.  The date format and precision 
        /// will match the specified granularity.
        /// </summary>
        /// <param name="harvestSetID"></param>
        /// <param name="granularity"></param>
        /// <param name="fromDate"></param>
        /// <param name="untilDate"></param>
        private void GetHarvestDates(int harvestSetID, string granularity, out string fromDate, out string untilDate)
        {
            DateTime from;
            DateTime until;

            if (string.IsNullOrWhiteSpace(configParms.FromDate))
            {
                // Get the latest harvested end date for this harvest set, and use it as the "from" date for 
                // this harvest.  If for some reason it is later than the current date, then use the current 
                // date instead.  
                from = new BHLImportProvider().OAIHarvestLogSelectLastDateForHarvestSet(harvestSetID);
                if (from.CompareTo(DateTime.Now) > 0) from = DateTime.Now;

                // The OAI specs recommend overlapping harvests to ensure that nothing is missed, so subtract
                // one day from the "from" date. 
                from = from.Subtract(new TimeSpan(1, 0, 0, 0));
            }
            else
            {
                // Use FromDate specified on the command line
                from = Convert.ToDateTime(configParms.FromDate);
            }

            if (string.IsNullOrWhiteSpace(configParms.UntilDate))
            {
                until = DateTime.Now;
            }
            else
            {
                // Use UntilDate specified on the command line.  If it is later than the current date, use
                // the current date instead.
                until = Convert.ToDateTime(configParms.UntilDate);
                if (until.CompareTo(DateTime.Now) > 0) until = DateTime.Now;
            }

            // Convert to UTC dates of the specified granularity.
            fromDate = from.ToUniversalTime().ToString(granularity);
            untilDate = until.ToUniversalTime().ToString(granularity);
        }

        /// <summary>
        /// Updates the count of harvested records for the specified harvest set.
        /// </summary>
        /// <param name="harvestSetName"></param>
        /// <param name="addedRecordInfo"></param>
        private void UpdateHarvestCount(string harvestSetName, string addedRecordInfo)
        {
            if (itemsHarvested.ContainsKey(harvestSetName))
            {
                itemsHarvested[harvestSetName].Add(addedRecordInfo);
            }
            else
            {
                List<string> insertedList = new List<string>();
                insertedList.Add(addedRecordInfo);
                itemsHarvested.Add(harvestSetName, insertedList);
            }
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
                if (itemsHarvested.Count > 0 || errorMessages.Count > 0)
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

            sb.Append(string.Format("BHLOAIHarvester: OAI harvesting on {0} complete.{1}{2}", thisComputer, endOfLine, endOfLine));
            if (this.itemsHarvested.Count > 0)
            {
                foreach (KeyValuePair<string, List<string>> kvp in itemsHarvested)
                {
                    sb.Append(string.Format("{0} Items were Harvested from \"{1}\"{2}",
                        kvp.Value.Count.ToString(), kvp.Key, endOfLine));
                }
                sb.Append(endOfLine);
            }
            if (this.errorMessages.Count > 0)
            {
                sb.Append(string.Format("{0} Errors Occurred {1}See the log file for details{2}{3}",
                    this.errorMessages.Count.ToString(), endOfLine, endOfLine, endOfLine));
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
                MailRequestModel mailRequest = new MailRequestModel();
                mailRequest.Subject = subject;
                mailRequest.Body = message;
                mailRequest.From = fromAddress;

                List<string> recipients = new List<string>();
                foreach (string recipient in toAddress.Split(',')) recipients.Add(recipient);
                mailRequest.To = recipients;

                if (ccAddresses != String.Empty)
                {
                    List<string> ccs = new List<string>();
                    foreach (string cc in ccAddresses.Split(',')) ccs.Add(cc);
                    mailRequest.Cc = ccs;
                }

                EmailClient restClient = new EmailClient(configParms.BHLWSEndpoint);
                restClient.SendEmail(mailRequest);
            }
            catch (Exception ex)
            {
                LogMessage("Email Exception.", ex);
            }
        }

        #endregion Process results
    }
}

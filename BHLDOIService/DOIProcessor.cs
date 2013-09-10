using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;
using MOBOT.BHL.BHLDOIService.BHLWS;
using MOBOT.BHL.DOIDeposit;
using RestSharp;

namespace MOBOT.BHL.BHLDOIService
{
    public class DOIProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> submittedDOIs = new List<string>();
        private List<string> approvedDOIs = new List<string>();
        private List<string> warningDOIs = new List<string>();
        private List<string> rejectedDOIs = new List<string>();
        private List<string> unverifiedDOIs = new List<string>();
        private List<string> errorMessages = new List<string>();

        public void Process()
        {
            this.LogMessage("BHLDOIService Processing Starting");

            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            // Generate and submit DOIs for any titles without DOIs
            if (configParms.SubmitTitles) this.ProcessTitlesWithoutDOIs();

            // Verify all previously submitted DOIs
            if (configParms.ValidateSubmissions) this.VerifySubmittedDOIs();

            // Report the results of pdf generation
            this.ProcessResults();

            this.LogMessage("BHLDOIService Processing Complete");
        }

        #region Process Entities Without DOIs

        private void ProcessTitlesWithoutDOIs()
        {
            this.LogMessage("Processing Titles Without DOIs");

            BHLWSSoapClient wsClient = null;

            try
            {
                wsClient = new BHLWSSoapClient();

                // Get IDs of monographs without a DOI
                DOI[] doisToSubmit = wsClient.TitleSelectWithoutSubmittedDOI(configParms.NumberToSubmit);
                this.LogMessage("Found " + doisToSubmit.Count().ToString() + " Titles To Process");
                foreach (DOI doi in doisToSubmit)
                {
                    // If no DOI record exists, create one (set status to "None" or "No DOI")
                    if (doi.DOIID == 0)
                    {
                        DOI newDoi = wsClient.DOIInsertAuto(configParms.DoiEntityTypeTitle, doi.EntityID, configParms.DoiStatusNone,
                            string.Empty, string.Empty, string.Empty, 0);
                        doi.DOIID = newDoi.DOIID;
                        doi.DOIStatusID = newDoi.DOIStatusID;
                        doi.DOIEntityTypeID = newDoi.DOIEntityTypeID;
                    }

                    if (doi.DOIStatusID == configParms.DoiStatusNone)
                    {
                        // Generate a DOI and assign it to the title
                        string doiName = string.Empty;
                        doiName = this.GenerateDOIName(configParms.DoiPrefix, configParms.DoiEntityTypeTitle, doi.EntityID);
                        wsClient.DOIUpdateDOIName(doi.DOIID, configParms.DoiStatusAssigned, doiName);
                        doi.DOIName = doiName;
                    }

                    // Generate a batch identifier for this DOI
                    string doiBatchID = this.GenerateDOIBatchID(doi.DOIID);
                    wsClient.DOIUpdateBatchID(doi.DOIID, configParms.DoiStatusBatchAssigned, doiBatchID);
                    doi.DOIBatchID = doiBatchID;

                    // Create a CrossRef deposit record for this DOI
                    DOIDepositData depositData = this.GetDepositData(doi.DOIEntityTypeID, doi.EntityID, doi.DOIBatchID, doi.DOIName);
                    string depositTemplate = this.GetDepositTemplate(depositData);
                    string doiDeposit = this.GenerateDOIDepositRecord(depositData, depositTemplate);
                    File.WriteAllText(configParms.DepositFolder + string.Format(configParms.DepositFileFormat, doiBatchID), doiDeposit);

                    try
                    {
                        // Submit the new DOI to CrossRef and update the DOI status to "Submitted"
                        this.SubmitDOI(doiDeposit, string.Format(configParms.DepositFileFormat, doiBatchID));
                        wsClient.DOIUpdateStatus(doi.DOIID, configParms.DoiStatusSubmitted, string.Empty, null);
                        submittedDOIs.Add(doi.DOIName);
                    }
                    catch (Exception ex)
                    {
                        // Set DOI error status and record the error
                        log.Error("Exception submitting DOI for title " + doi.EntityID.ToString(), ex);
                        errorMessages.Add("Exception submitting DOI for title " + doi.EntityID.ToString() + ": " + ex.Message);
                        wsClient.DOIUpdateStatus(doi.DOIID, configParms.DoiStatusSubmitError, ex.Message, null);
                    }

                    this.LogMessage("DOI " + doi.DOIID.ToString() + " for Title " + doi.EntityID.ToString() + " Processed");
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception processing titles", ex);
                errorMessages.Add("Exception processing titles: " + ex.Message);
            }
            finally
            {
                // Clean up the web service connection
                if (wsClient != null)
                {
                    if (wsClient.State != System.ServiceModel.CommunicationState.Closed) wsClient.Close();
                }
            }

            this.LogMessage("Done Processing Titles Without DOIs");
        }

        /// <summary>
        /// Generate a DOI
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        private string GenerateDOIName(string prefix, int entityType, int entityID)
        {
            string doiName = string.Empty;
            string entityTypeName = string.Empty;

            if (entityType == configParms.DoiEntityTypeTitle) entityTypeName = "title";
            if (entityType == configParms.DoiEntityTypeItem) entityTypeName = "item";
            if (entityType == configParms.DoiEntityTypePage) entityTypeName = "page";

            doiName = string.Format(configParms.DoiFormat, prefix, entityTypeName, entityID.ToString());
            return doiName;
        }

        /// <summary>
        /// Generate a batch ID to include in the deposit record sent to CrossRef for the specified DOI
        /// </summary>
        /// <param name="doiID"></param>
        /// <returns></returns>
        private string GenerateDOIBatchID(int doiID)
        {
            return String.Format("{0}.bhl.{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), doiID.ToString());
        }

        /// <summary>
        /// Populate a DOIDepositData object with data about the specified entity.
        /// </summary>
        /// <param name="doiEntityType"></param>
        /// <param name="entityID"></param>
        /// <param name="doiBatchID"></param>
        /// <param name="doiName"></param>
        /// <returns></returns>
        private DOIDepositData GetDepositData(int doiEntityType, int entityID, string doiBatchID, string doiName)
        {
            DOIDepositData data = new DOIDepositData();
            BHLWSSoapClient wsClient = null;

            try
            {
                wsClient = new BHLWSSoapClient();

                if (doiEntityType == configParms.DoiEntityTypeTitle)
                {
                    Title title = wsClient.TitleSelectDetailByTitleID(entityID);

                    if (title.BibliographicLevelID == configParms.BibLevelMonographComponent ||
                        title.BibliographicLevelID == configParms.BibLevelMonograph)
                    {
                        data.BookType = DOIDepositData.BookTypeValue.Monograph;
                    }
                    else
                    {
                        data.BookType = DOIDepositData.BookTypeValue.EditedBook;
                    }

                    data.Title = title.FullTitle;
                    data.PublisherName = title.Datafield_260_b;
                    data.PublisherPlace = title.Datafield_260_a;
                    data.PublicationDate = (title.StartYear == null ? "" : title.StartYear.ToString());
                    // data.Language = title.LanguageCode;      // Need to translate to ISO 639 language codes
                    // data.Edition = title.EditionStatement;   // Should only contain a number; our edition data is too messy
                    data.DoiName = doiName;

                    if (doiEntityType == configParms.DoiEntityTypeTitle)
                    {
                        data.DoiResource = string.Format(configParms.BhlTitleUrlFormat, entityID.ToString());
                    }
                    else if (doiEntityType == configParms.DoiEntityTypeItem)
                    {
                        data.DoiResource = string.Format(configParms.BhlItemUrlFormat, entityID.ToString());
                    }
                    else if (doiEntityType == configParms.DoiEntityTypePage)
                    {
                        data.DoiResource = string.Format(configParms.BhlPageUrlFormat, entityID.ToString());
                    }

                    foreach (TitleVariant titleVariant in title.TitleVariants)
                    {
                        if (titleVariant.TitleVariantTypeID == configParms.TitleVariantAbbreviated) data.AbbreviatedTitle = titleVariant.Title;
                    }

                    foreach (Title_Identifier titleIdentifier in title.TitleIdentifiers)
                    {
                        if (titleIdentifier.TitleIdentifierID == configParms.TitleIdentifierISBN) data.Isbn = titleIdentifier.IdentifierValue;
                        if (titleIdentifier.TitleIdentifierID == configParms.TitleIdentifierISSN) data.Issn = titleIdentifier.IdentifierValue;
                        if (titleIdentifier.TitleIdentifierID == configParms.TitleIdentifierCODEN) data.Coden = titleIdentifier.IdentifierValue;
                        if (titleIdentifier.TitleIdentifierID == configParms.TitleIdentifierAbbreviation &&
                            data.AbbreviatedTitle == string.Empty) data.AbbreviatedTitle = titleIdentifier.IdentifierValue;
                    }

                    foreach (TitleAuthor titleAuthor in title.TitleAuthors)
                    {
                        DOIDepositData.Contributor contributor = new DOIDepositData.Contributor();

                        if (titleAuthor.AuthorRoleID == configParms.AuthorRole100 ||
                            titleAuthor.AuthorRoleID == configParms.AuthorRole700)
                        {
                            contributor.PersonName = titleAuthor.FullName;
                            contributor.Role = DOIDepositData.ContributorRole.Author;
                            contributor.Sequence = (titleAuthor.AuthorRoleID == configParms.AuthorRole100 ?
                                                    DOIDepositData.PersonNameSequence.First :
                                                    DOIDepositData.PersonNameSequence.Additional);
                        }
                        else
                        {
                            contributor.OrganizationName = titleAuthor.FullName;
                            contributor.Role = DOIDepositData.ContributorRole.Author;
                            contributor.Sequence = (titleAuthor.AuthorRoleID == configParms.AuthorRole110 ||
                                                    titleAuthor.AuthorRoleID == configParms.AuthorRole111 ?
                                                    DOIDepositData.PersonNameSequence.First :
                                                    DOIDepositData.PersonNameSequence.Additional);
                        }

                        data.Contributors.Add(contributor);
                    }

                    data.BatchID = doiBatchID;
                    data.DepositorEmail = configParms.CrossrefDepositorEmail;
                    data.DepositorName = configParms.CrossrefDepositorName;
                    data.Registrant = configParms.CrossrefRegistrantName;
                }
                else
                {
                    // Only titles have been implemented
                    throw new NotImplementedException();
                }

                return data;
            }
            finally
            {
                if (wsClient != null)
                {
                    if (wsClient.State != System.ServiceModel.CommunicationState.Closed) wsClient.Close();
                }
            }
        }

        /// <summary>
        /// Examine the deposit data and return the appropriate template
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetDepositTemplate(DOIDepositData data)
        {
            string depositTemplate = string.Empty;
            if (data.BookType == DOIDepositData.BookTypeValue.Monograph)
                depositTemplate = File.ReadAllText(configParms.MonographDepositTemplateFile);
            else
                depositTemplate = File.ReadAllText(configParms.JournalDepositTemplateFile);

            return depositTemplate;
        }

        /// <summary>
        /// Generate a deposit record from the specified deposit data and template
        /// </summary>
        /// <param name="data"></param>
        /// <param name="depositTemplate"></param>
        /// <returns></returns>
        private string GenerateDOIDepositRecord(DOIDepositData data, string depositTemplate)
        {
            bool isMonograph = (data.BookType == DOIDepositData.BookTypeValue.Monograph);

            DOIDepositFactory depositFactory = new DOIDepositFactory(data);
            DOIDeposit.DOIDeposit deposit;

            if (isMonograph)
                deposit = depositFactory.GetDOIDeposit(DOIDepositFactory.DOIDepositType.Monograph);
            else
                deposit = depositFactory.GetDOIDeposit(DOIDepositFactory.DOIDepositType.Journal);

            return deposit.ToString(depositTemplate);
        }

        /// <summary>
        /// Use the RESTSharp library to submit the CrossRef deposit file
        /// </summary>
        /// <remarks>
        /// See http://dkdevelopment.net/2010/05/25/dropbox-api-restsharp-and-c-part-2-the-revenge/
        /// for an example of using RestSharp to perform a file upload.
        /// </remarks>
        /// <param name="deposit"></param>
        private void SubmitDOI(string deposit, string filename)
        {
            // Set up the REST client
            // NOTE:  The Url Base and Url Query are separated here because RestSharp puts a
            // trailing slash at the end of the base URL of POST operations.  Thrrefore, if we 
            // included the Url Query in the Url used to set up the RestClient object, the slash
            // would be placed at the end of the querystring.  By splitting the Base and Query, 
            // we "trick" RestSharp into putting the slash between the Base and Query (which is
            // where a slash should appear).
            RestClient restClient = new RestClient(configParms.CrossrefDepositUrlBase);
            RestRequest request = new RestRequest(
                String.Format(configParms.CrossrefDepositUrlQueryFormat,
                configParms.CrossrefLogin, configParms.CrossrefPassword, configParms.CrossrefDepositArea), 
                Method.POST);

            // Convert the deposit into a byte array and add it to the request
            byte[] bytes = new UTF8Encoding().GetBytes(deposit);
            request.AddFile("fname", bytes, filename, "text/xml");

            // Perform the POST operation
            RestResponse response = restClient.Execute(request);

            // Check the result of the POST operation.
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error posting deposit for " + filename + ": " + response.ErrorMessage);
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Error posting deposit for " + filename + ": " + response.StatusDescription);
            }
            if (response.Content.Contains("FAILURE"))
            {
                throw new Exception("Error posting deposit for " + filename + ": " + response.Content);
            }
        }

        #endregion Process Entities Without DOIs

        #region Verify Submitted DOIs

        private void VerifySubmittedDOIs()
        {
            this.LogMessage("Verifying Submitted DOIs");

            BHLWSSoapClient wsClient = new BHLWSSoapClient();

            try
            {
                // Check the CrossRef status of submitted DOIs that have not yet been verified
                DOI[] submittedDOIs = wsClient.DOISelectSubmitted(configParms.MinMinutesSinceSubmit);
                this.LogMessage("Found " + submittedDOIs.Count().ToString() + " Submitted DOIs To Verify");
                foreach (DOI doi in submittedDOIs)
                {
                    XDocument submitLog = null;

                    // Get the submission log from CrossRef
                    bool gotSubmissionLog = false;
                    try
                    {
                        submitLog = this.GetSubmissionLog(doi.DOIBatchID, string.Format(configParms.DepositFileFormat, doi.DOIBatchID));

                        // Write the submission log to the file system
                        File.WriteAllText(
                            configParms.SubmitLogFolder + string.Format(configParms.SubmitLogFileFormat, doi.DOIBatchID),
                            submitLog.ToString());

                        gotSubmissionLog = true;
                    }
                    catch (Exception ex)
                    {
                        // Set DOI error status and record the error
                        log.Error("Exception getting submission log for batch " + doi.DOIBatchID, ex);
                        errorMessages.Add("Exception getting submission log for batch " + doi.DOIBatchID + ":" + ex.Message);
                        wsClient.DOIUpdateStatus(doi.DOIID, configParms.DoiStatusGetLogError, ex.Message, null);
                    }

                    // Parse the submission log to determine current status of DOI
                    if (gotSubmissionLog)
                    {
                        try
                        {
                            XAttribute batchStatusAttrib = submitLog.Root.Attribute("status");

                            // Make sure the batch processing is complete
                            if (batchStatusAttrib.Value == "completed")
                            {
                                XElement recordDiagnostic = submitLog.Root.Element("record_diagnostic");

                                switch (recordDiagnostic.Attribute("status").Value)
                                {
                                    case  "Success":
                                        {
                                            // DOI accepted by CrossRef
                                            wsClient.DOIUpdateStatus(doi.DOIID, configParms.DoiStatusApproved, string.Empty, 1);
                                            this.LogMessage("DOI " + doi.DOIName + " Verified");
                                            approvedDOIs.Add(doi.DOIName);
                                            break;
                                        }
                                    case "Warning":
                                        {
                                            // Warning; DOI deposited, but has a metadata conflict with another DOI
                                            XElement diagnosticMessage = recordDiagnostic.Element("msg");
                                            wsClient.DOIUpdateStatus(doi.DOIID, configParms.DoiStatusCrossRefWarning,
                                                diagnosticMessage.Value, null);
                                            this.LogMessage("DOI " + doi.DOIName + " Verification WARNING: " + diagnosticMessage.Value);
                                            warningDOIs.Add(doi.DOIName);
                                            break;
                                        }
                                    case "Failure":
                                        {
                                            // Error reported by CrossRef; set status and record the error message
                                            XElement diagnosticMessage = recordDiagnostic.Element("msg");
                                            wsClient.DOIUpdateStatus(doi.DOIID, configParms.DoiStatusCrossRefError,
                                                diagnosticMessage.Value, null);
                                            this.LogMessage("DOI " + doi.DOIName + " Verification FAILURE: " + diagnosticMessage.Value);
                                            rejectedDOIs.Add(doi.DOIName);
                                            break;
                                        }
                                }
                            }
                            else if (batchStatusAttrib.Value == "unknown_submission")
                            {
                                // Something went wrong; set the DOI status and log the message from CrossRef
                                wsClient.DOIUpdateStatus(doi.DOIID, configParms.DoiStatusCrossRefError, batchStatusAttrib.Value, null);
                                this.LogMessage("DOI " + doi.DOIName + " NOT Verified: " + batchStatusAttrib.Value);
                                unverifiedDOIs.Add(doi.DOIName);
                            }
                            else
                            {
                                // Keep the current local DOI Status, and record the current CrossRef status
                                wsClient.DOIUpdateStatus(doi.DOIID, doi.DOIStatusID, batchStatusAttrib.Value, null);
                                this.LogMessage("DOI " + doi.DOIName + " NOT Verified: " + batchStatusAttrib.Value);
                                unverifiedDOIs.Add(doi.DOIName);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Set DOI error status and record the error
                            log.Error("Exception parsing submission log for batch " + doi.DOIBatchID, ex);
                            errorMessages.Add("Exception parsing submission log for batch " + doi.DOIBatchID + ":" + ex.Message);
                            wsClient.DOIUpdateStatus(doi.DOIID, configParms.DoiStatusCrossRefError, ex.Message, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception verifying submitted DOIs", ex);
                errorMessages.Add("Exception verifying submitted DOIs: " + ex.Message);
            }
            finally
            {
                // Clean up the web service connection
                if (wsClient != null)
                {
                    if (wsClient.State != System.ServiceModel.CommunicationState.Closed) wsClient.Close();
                }
            }

            this.LogMessage("Done Verifying Submitted DOIs");
        }

        /// <summary>
        /// Load the submission log for the specified DOI Batch
        /// </summary>
        /// <param name="doiBatchID"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        private XDocument GetSubmissionLog(string doiBatchID, string filename)
        {
            //var xml = XDocument.Load(string.Format(configParms.CrossrefCheckSubmissionUrlFormat,
            //    configParms.CrossrefLogin, configParms.CrossrefPassword, doiBatchID, filename));
            var xml = XDocument.Load(string.Format(configParms.CrossrefCheckSubmissionUrlFormat,
                configParms.CrossrefLogin, configParms.CrossrefPassword, string.Empty, filename));

            return xml;
        }

        #endregion Verify Submitted DOIs

        #region Get and validate parameters

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        private bool ReadCommandLineArguments()
        {
            bool returnValue = true;
            string[] keyValue;

            string[] args = System.Environment.GetCommandLineArgs();

            // If command line arguments have been specified, first turn off all functions of this service.
            // Parsing of the arguments will re-enable only the specified functions.
            if (args.Length > 1)
            {
                configParms.ValidateSubmissions = false;
                configParms.SubmitTitles = false;
            }

            for (int x = 0; x < args.Length; x++)
            {
                if (x > 0)  // first argument is the EXE name; skip it
                {
                    string arg = args[x];

                    switch (arg.ToUpper())
                    {
                        case "/VALIDATE":
                            {
                                configParms.ValidateSubmissions = true;
                                break;
                            }
                        default:
                            {
                                keyValue = arg.Split(':');

                                if (keyValue.Length != 2)
                                {
                                    returnValue = false;
                                }
                                else
                                {
                                    if (keyValue[0].ToUpper() == "/SUBMIT")
                                    {
                                        switch (keyValue[1].ToUpper())
                                        {
                                            case "TITLE":
                                                configParms.SubmitTitles = true;
                                                break;
                                            default:
                                                returnValue = false;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        returnValue = false;
                                    }
                                }

                                if (!returnValue) this.LogMessage("Invalid command line argument " + keyValue[0] + ".  Format is 'BHLDOIService.exe [/VALIDATE] [/SUBMIT:type]', where type is 'TITLE'.");
                                break;
                            }
                    }
                }

                if (!returnValue) break;
            }

            return returnValue;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(configParms.DoiPrefix))
            {
                this.LogMessage("DOIPrefix not set correctly.  Check configuration file.");
                return false;
            }

            if (string.IsNullOrEmpty(configParms.DoiFormat))
            {
                this.LogMessage("DOIFormat not set correctly.  Check configuration file.");
                return false;
            }

            if (string.IsNullOrEmpty(configParms.CrossrefCheckSubmissionUrlFormat) ||
                string.IsNullOrEmpty(configParms.CrossrefDepositArea) ||
                string.IsNullOrEmpty(configParms.CrossrefDepositorEmail) ||
                string.IsNullOrEmpty(configParms.CrossrefDepositorName) ||
                string.IsNullOrEmpty(configParms.CrossrefDepositUrlBase) ||
                string.IsNullOrEmpty(configParms.CrossrefDepositUrlQueryFormat) ||
                string.IsNullOrEmpty(configParms.CrossrefLogin) ||
                string.IsNullOrEmpty(configParms.CrossrefPassword) ||
                string.IsNullOrEmpty(configParms.CrossrefRegistrantName))
            {
                this.LogMessage("CrossRef information not set correctly.  Check all CrossRef settings in the configuration file.");
                return false;
            }

            if (configParms.NumberToSubmit <= 0)
            {
                this.LogMessage("NumberToSubmit must be greater than zero.  Check configuration file.");
                return false;
            }

            if (configParms.MinMinutesSinceSubmit <= 0)
            {
                this.LogMessage("MinimumMinutesSinceSubmit must be greater than zero.  Check configuration file.");
                return false;
            }

            return true;
        }

        #endregion Get and validate parameters

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
                if (approvedDOIs.Count > 0 || submittedDOIs.Count > 0 || 
                    unverifiedDOIs.Count > 0 || warningDOIs.Count > 0 ||
                    rejectedDOIs.Count > 0 || errorMessages.Count > 0)
                {
                    String subject = String.Empty;
                    String thisComputer = Environment.MachineName;
                    if (this.errorMessages.Count == 0)
                    {
                        subject = "BHLDOIService: DOI processing on " + thisComputer + " completed successfully.";
                    }
                    else
                    {
                        subject = "BHLDOIService: DOI processing on " + thisComputer + " completed with errors.";
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

            sb.Append("BHLDOIService: DOI processing on " + thisComputer + " complete." + endOfLine);
            if (this.submittedDOIs.Count > 0)
            {
                sb.Append(endOfLine + this.submittedDOIs.Count.ToString() + " new DOIs were Submitted" + endOfLine);
            }
            if (this.approvedDOIs.Count > 0)
            {
                sb.Append(endOfLine + this.approvedDOIs.Count.ToString() + " DOIs were Approved by CrossRef" + endOfLine);
            }
            if (this.unverifiedDOIs.Count > 0)
            {
                sb.Append(endOfLine + this.unverifiedDOIs.Count.ToString() + " DOIs are being Processed by CrossRef" + endOfLine);
            }
            if (this.warningDOIs.Count > 0)
            {
                sb.Append(endOfLine + this.warningDOIs.Count.ToString() + " DOIs have a Metadata Conflict at CrossRef" + endOfLine);
            }
            if (this.rejectedDOIs.Count > 0)
            {
                sb.Append(endOfLine + this.rejectedDOIs.Count.ToString() + " DOIs were Rejected by CrossRef" + endOfLine);
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
                log.Error("Email Exception: ", ex);
            }
        }

        #endregion Process results

        #region Logging

        private void LogMessage(string message)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");
        }

        #endregion Logging
    }
}

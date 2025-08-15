using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using MOBOT.BHL.Utility;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;
using System.Text;

public class WDHarvestProcessor
{
    // Create a logger for use in this class
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
    // is equivalent to typeof(LoggingExample) but is more portable
    // i.e. you can copy the code directly into another class without
    // needing to edit the code.

    private ConfigParms configParms = new();
    private List<EntityIdentifierPair> retrievedTitleIds = new();
    private List<EntityIdentifierPair> retrievedAuthorIds = new();
    private List<WDEntityIdentifier> publishedAuthorIds = new();
    private List<WDEntityIdentifier> publishedTitleIds = new();
    private List<string> errorMessages = new();
    private List<EntityIdentifierPair> invalidIds = new();
    private List<WDEntityIdentifier> idsToReview = new();

    // Create an BHLImportProvider for use in this class
    BHLImportProvider provider = new();

    public void Process()
    {
        LogMessage("WDHarvest Processing Started");

        // Load the app settings from the configuration file
        configParms.LoadAppConfig();

        // Read additional app settings from the command line
        // Note: Command line arguments override configuration file settings
        if (!this.ReadCommandLineArguments()) return;

        // validate config values
        if (!this.ValidateConfiguration()) return;

        DateTime harvestDateTime = DateTime.Now;
        string bhlEntityType;

        // Prepare output folder and filenames
        string outputFolder = "data";
        if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);
        string fileSuffix = harvestDateTime.ToString("yyyyMMdd.HHmm");
        string addedFilename = $"WDHarvestAdded.{fileSuffix}.csv";
        string reviewFilename = $"WDHarvestReview.{fileSuffix}.csv";

        // NOTE: Some data has been "split" into "regular" and "scholarly" types.  Most, but not all, BHL material
        // is in the "regular" data set.  Because of this split, federated queries must be submitted to Wikidata to
        // return all BHL data.

        // Harvest the author identifiers
        if (configParms.HarvestAuthorIDs)
        {
            bhlEntityType = "Author";
            HarvestIdentifiers(bhlEntityType, "authorQuery.rq", harvestDateTime, retrievedAuthorIds);
        }

        // Harvest the title identifiers
        if (configParms.HarvestTitleIDs)
        {
            bhlEntityType = "Title";
            HarvestIdentifiers(bhlEntityType, "titleQuery.rq", harvestDateTime, retrievedTitleIds);
        }

        // Push new author identifiers into the production database
        if (configParms.PublishAuthorIDs)
        {
            bhlEntityType = "Author";
            LogMessage($"{bhlEntityType} identifier publish started");
            publishedAuthorIds = provider.WDEntityIdentifierPublishAuthorIDs();
            LogMessage($"{bhlEntityType} identifier publish complete");
        }

        // Push new title identifiers into the production database
        if (configParms.PublishTitleIDs)
        {
            bhlEntityType = "Title";
            LogMessage($"{bhlEntityType} identifier publish started");
            publishedTitleIds = provider.WDEntityIdentifierPublishTitleIDs();
            LogMessage($"{bhlEntityType} identifier publish complete");
        }

        // If any identifiers were published, write them to a file
        if (publishedAuthorIds.Count > 0 || publishedTitleIds.Count > 0)
        {
            LogMessage("Output of published identifiers started");

            // Output a list of published identifiers
            var data = GetPublishedIdentifierList();
            File.WriteAllBytes($"{outputFolder}\\{addedFilename}", new CSV().FormatCSVData(data));

            LogMessage("Output of published identifiers complete");
        }

        // Pull report of identifiers needing investigation
        if (configParms.DoAnalysis)
        {
            LogMessage("Analysis started");

            // Get and output a list of potential problems
            idsToReview = provider.WDEntityIdentifierSelectNeedReview();
            var data = GetIdentifiersToReview();
            File.WriteAllBytes($"{outputFolder}\\{reviewFilename}", new CSV().FormatCSVData(data));

            LogMessage("Analysis complete");
        }

        // Report the results
        this.ProcessResults(outputFolder, addedFilename, reviewFilename);

        LogMessage("WDHarvest Processing Complete");
    }

    /// <summary>
    /// Use the specified queryFile to retrieve a list of identifiers for the specified bhlEntityType
    /// </summary>
    /// <param name="bhlEntityType"></param>
    /// <param name="queryFile"></param>
    /// <param name="harvestDateTime"></param>
    /// <param name="identifierList"></param>
    private void HarvestIdentifiers(string bhlEntityType, string queryFile, DateTime harvestDateTime, List<EntityIdentifierPair> identifierList)
    {
        LogMessage($"{bhlEntityType} identifier harvest started");

        try
        {
            // Reset outputs
            provider.WDEntityIdentifierDeleteByEntityType(bhlEntityType);

            // Get author identifiers from Wikidata
            string sparqlQuery = File.ReadAllText(queryFile);
            string cleanedTsv = WikidataSparqlClient.SubmitSparqlQueryAndSaveTsvAsync(WikidataSparqlClient.Endpoint.Primary, sparqlQuery).GetAwaiter().GetResult();

            // Convert the TSV files to a one-id-pair-per-line format
            var idEntries = TSVConverter.ConvertTSVToObjects(bhlEntityType, cleanedTsv);

            // Save the identifiers
            foreach (var entry in idEntries)
            {
                if (Int32.TryParse(entry.EntityID, out int entityId))
                {
                    provider.WDEntityIdentifierInsert(entry.EntityType, entityId, entry.Type, entry.Value, harvestDateTime);
                    identifierList.Add(entry);
                }
                else
                {
                    entry.EntityDescription = "Unknown";
                    entry.Message = "Malformed BHL Entity Identifier";
                    invalidIds.Add(entry);
                }
            }
        }
        catch (Exception ex)
        {
            log.Error($"Exception harvesting {bhlEntityType} identifiers.", ex);
            errorMessages.Add($"Exception harvesting {bhlEntityType} identifiers: {ex.Message}");
        }
        finally
        {
            LogMessage($"{bhlEntityType} identifier harvest complete");
        }
    }

    /// <summary>
    /// Combine all published identifiers into a single list
    /// </summary>
    /// <returns></returns>
    private List<dynamic> GetPublishedIdentifierList()
    {
        // Produce a list of all added identifiers
        var data = new List<dynamic>();
        foreach (var authorid in publishedAuthorIds)
        {
            dynamic record = new System.Dynamic.ExpandoObject();
            record.BHLEntityType = authorid.BHLEntityType;
            record.BHLEntityID = authorid.BHLEntityID;
            record.IdentifierType = authorid.IdentifierType;
            record.IdentifierValue = authorid.IdentifierValue;
            data.Add(record);
        }
        foreach (var titleid in publishedTitleIds)
        {
            dynamic record = new System.Dynamic.ExpandoObject();
            record.BHLEntityType = titleid.BHLEntityType;
            record.BHLEntityID = titleid.BHLEntityID;
            record.IdentifierType = titleid.IdentifierType;
            record.IdentifierValue = titleid.IdentifierValue;
            data.Add(record);
        }
        return data;
    }

    /// <summary>
    /// Combine all identifiers needing a review into a single list
    /// </summary>
    /// <returns></returns>
    private List<dynamic> GetIdentifiersToReview()
    {
        var data = new List<dynamic>();
        foreach (var id in invalidIds)
        {
            dynamic record = new System.Dynamic.ExpandoObject();
            record.BHLEntityType = id.EntityType;
            record.BHLEntityID = id.EntityID;
            record.EntityDescription = id.EntityDescription;
            record.IdentifierType = id.Type;
            record.IdentifierValue = id.Value;
            record.Message = id.Message;
            data.Add(record);
        }
        foreach (var id in idsToReview)
        {
            dynamic record = new System.Dynamic.ExpandoObject();
            record.BHLEntityType = id.BHLEntityType;
            record.BHLEntityID = id.BHLEntityID;
            record.EntityDescription = id.EntityDescription;
            record.IdentifierType = id.IdentifierType;
            record.IdentifierValue = id.IdentifierValue;
            record.Message = id.Message;
            data.Add(record);
        }
        return data;
    }

    /// <summary>
    /// Reads the arguments supplied on the command line and stores them 
    /// in an instance of the ConfigParms class.
    /// </summary>
    /// <returns>True if the arguments were in a valid format, false otherwise</returns>
    private bool ReadCommandLineArguments()
    {
        string[] args = Environment.GetCommandLineArgs();

        if (args.Length == 1) return true;

        for (int x = 1; x < args.Length; x++)
        {
            string[] split = args[x].Split(':');
            if (split.Length != 2)
            {
                LogMessage("Invalid command line format.  Format is WDHarvest.exe [/TITLEIDS:truefalse] [/AUTHORIDs:truefalse] [/ANALYSIS:truefalse]");
                return false;
            }

            if (string.Compare(split[0], "/TITLEIDS", true) == 0) configParms.HarvestTitleIDs = Convert.ToBoolean(split[1]);
            if (string.Compare(split[0], "/AUTHORIDS", true) == 0) configParms.HarvestAuthorIDs = Convert.ToBoolean(split[1]);
            if (string.Compare(split[0], "/ANALYSIS", true) == 0) configParms.DoAnalysis = Convert.ToBoolean(split[1]); 
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
    /// Examine the results of the process and take the appropriate actions (log, send email, do nothing).
    /// </summary>
    private void ProcessResults(string outputFolder, string addedFilename, string reviewFilename)
    {
        try
        {
            // Report the process results
            string subject;
            string message;
            string serviceName = "WDHarvest";
            if (retrievedTitleIds.Count > 0 || retrievedAuthorIds.Count > 0 ||
                publishedTitleIds.Count > 0 || publishedAuthorIds.Count > 0 ||
                invalidIds.Count > 0 || idsToReview.Count > 0 || errorMessages.Count > 0)
            {
                LogMessage("Sending Emails");

                // Email links to output files to specified recipients
                if (publishedAuthorIds.Count > 0 || publishedTitleIds.Count > 0 || invalidIds.Count > 0 || idsToReview.Count > 0)
                {
                    subject = "BHL Wikidata Harvesting is complete";
                    message = GetStaffEmailBody(outputFolder, addedFilename, reviewFilename);
                    LogMessage(message);
                    SendEmail(subject, message, configParms.StaffEmailToAddress);
                }

                // Email processing summary to admins
                string successOrFailure = (errorMessages.Count == 0 ? "successfully" : "with errors");
                subject = $"{serviceName}: Harvesting on {Environment.MachineName} completed {successOrFailure}.";
                message = GetAdminEmailBody();
                LogMessage(message);
                SendServiceLog(serviceName, message);
                if (errorMessages.Count > 0 && configParms.EmailOnError)
                {
                    SendEmail(subject, message, configParms.AdminEmailToAddress);
                }
            }
            else
            {
                message = "No data harvested";
                LogMessage(message);
                SendServiceLog(serviceName, message);
            }
        }
        catch (Exception ex)
        {
            log.Error("Exception processing results.", ex);
            return;
        }
    }

    private string GetStaffEmailBody(string outputFolder, string addedFilename, string reviewFilename)
    {
        string emailBody = File.ReadAllText(@"StaffEmailTemplate.txt");
        emailBody = emailBody.Replace("<PublishedTitleCount>", publishedTitleIds.Count.ToString());
        emailBody = emailBody.Replace("<PublishedAuthorCount>", publishedAuthorIds.Count.ToString());
        emailBody = emailBody.Replace("<ReviewCount>", (invalidIds.Count + idsToReview.Count).ToString());
        emailBody = emailBody.Replace("<Folder>", outputFolder);
        emailBody = emailBody.Replace("<AddFile>", addedFilename);
        emailBody = emailBody.Replace("<ReviewFile>", reviewFilename);
        return emailBody;
    }

    /// <summary>
    /// Constructs the body of an email message to be sent
    /// </summary>
    /// <returns>Body of email message to be sent</returns>
    private string GetAdminEmailBody()
    {
        StringBuilder sb = new();
        const string endOfLine = "\r\n";

        if (retrievedTitleIds.Count > 0) sb.Append($"Retrieved {retrievedTitleIds.Count.ToString()} Title Identifiers{endOfLine}");
        if (retrievedAuthorIds.Count > 0) sb.Append($"Retrieved {retrievedAuthorIds.Count.ToString()} Author Identifiers{endOfLine}");
        if (publishedTitleIds.Count > 0) sb.Append($"Published {publishedTitleIds.Count.ToString()} Title Identifiers{endOfLine}");
        if (publishedAuthorIds.Count > 0) sb.Append($"Published {publishedAuthorIds.Count.ToString()} Author Identifiers{endOfLine}");
        if (invalidIds.Count > 0 || idsToReview.Count > 0) sb.Append($"{(invalidIds.Count + idsToReview.Count).ToString()} Identifiers Require Manual Review{endOfLine}");
        if (errorMessages.Count > 0)
        {
            sb.Append($"{endOfLine}{errorMessages.Count.ToString()} Errors Occurred{endOfLine}See the log file for details{endOfLine}{endOfLine}");
            foreach (string message in errorMessages) sb.Append($"{message}{endOfLine}");
        }
        sb.Append($"{endOfLine}Lists of published identifiers and identifiers needing review can be downloaded from the https://admin.biodiversitylibrary.org/data folder.");

        return sb.ToString();
    }

    /// <summary>
    /// Send the specified email message 
    /// </summary>
    /// <param name="message">Body of the message to be sent</param>
    private void SendEmail(string subject, string message, string adminRecipients)
    {
        try
        {
            MailRequestModel mailRequest = new()
            {
                Subject = subject,
                Body = message,
                From = configParms.EmailFromAddress
            };

            List<string> recipients = new();
            foreach (string recipient in adminRecipients.Split(',')) recipients.Add(recipient);
            mailRequest.To = recipients;

            EmailClient restClient = new(configParms.BHLWSEndpoint);
            restClient.SendEmail(mailRequest);
        }
        catch (Exception ex)
        {
            log.Error("Email Exception: ", ex);
        }
    }

    /// <summary>
    /// Send the specified message to the log table in the database
    /// </summary>
    /// <param name="serviceName">Name of the service being logged</param>
    /// <param name="message">Body of the message to be sent</param>
    private void SendServiceLog(string serviceName, string message)
    {
        try
        {
            ServiceLogModel serviceLog = new ServiceLogModel();
            serviceLog.Servicename = serviceName;
            serviceLog.Logdate = DateTime.Now;
            serviceLog.Severityname = (errorMessages.Count > 0 ? "Error" : "Information");
            serviceLog.Message = string.Format("Processing on {0} completed.\n\r{1}", Environment.MachineName, message);

            ServiceLogsClient restClient = new ServiceLogsClient(configParms.BHLWSEndpoint);
            restClient.InsertServiceLog(serviceLog);
        }
        catch (Exception ex)
        {
            log.Error("Service Log Exception: ", ex);
        }
    }

    private static void LogMessage(string message)
    {
        // logger automatically adds date/time
        if (log.IsInfoEnabled) log.Info(message);
        Console.Write(message + "\r\n");
    }
}

using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
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
    private List<string> retrievedTitleIds = new();
    private List<string> retrievedAuthorIds = new();
    private List<string> errorMessages = new();
    private List<string> invalidIds = new();

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

        // TODO
        // Push new author identifiers into the production database
        if (configParms.HarvestAuthorIDs)
        {
            bhlEntityType = "Author";

            LogMessage($"{bhlEntityType} identifier publish started");





            LogMessage($"{bhlEntityType} identifier publish complete");
        }

        // TODO
        // Push new title identifiers into the production database
        if (configParms.HarvestTitleIDs)
        {
            bhlEntityType = "Title";

            LogMessage($"{bhlEntityType} identifier publish started");





            LogMessage($"{bhlEntityType} identifier publish complete");
        }

        // TODO
        // Pull reports of identifiers needing investigation and deliver to the appropriate recipients
        if (configParms.EmailAnalysis)
        {
            LogMessage("Report generation started");





            LogMessage("Report generation complete");
        }

        // Report the results of item/page processing
        this.ProcessResults();

        LogMessage("WDHarvest Processing Complete");
    }

    /// <summary>
    /// Use the specified queryFile to retrieve a list of identifiers for the specified bhlEntityType
    /// </summary>
    /// <param name="bhlEntityType"></param>
    /// <param name="queryFile"></param>
    /// <param name="harvestDateTime"></param>
    /// <param name="identifierList"></param>
    private void HarvestIdentifiers(string bhlEntityType, string queryFile, DateTime harvestDateTime, List<string> identifierList)
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

            // Save the identifiers from both Wikidata query endpoints
            foreach (var entry in idEntries)
            {
                if (Int32.TryParse(entry.EntityID, out int entityId))
                {
                    provider.WDEntityIdentifierInsert(entry.EntityType, entityId, entry.Type, entry.Value, harvestDateTime);
                    identifierList.Add($"{entry.EntityID}|{entry.Type}|{entry.Value}");
                }
                else
                {
                    invalidIds.Add($"{entry.EntityType}|{entry.EntityID}|{entry.Type}|{entry.Value}");
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
            if (string.Compare(split[0], "/ANALYSIS", true) == 0) configParms.EmailAnalysis = Convert.ToBoolean(split[1]); 
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
    /// Examine the results of the process and take the appropriate 
    /// actions (log, send email, do nothing).
    /// </summary>
    private void ProcessResults()
    {
        try
        {
            // Report the process results
            string message;
            string serviceName = "WDHarvest";
            if (retrievedTitleIds.Count > 0 || retrievedAuthorIds.Count > 0 || invalidIds.Count > 0 || errorMessages.Count > 0)
            {
                LogMessage("Sending Email....");
                message = this.GetEmailBody();
                LogMessage(message);
                this.SendEmail(serviceName, message);
            }
            else
            {
                message = "No items or pages processed";
                LogMessage(message);
            }
        }
        catch (Exception ex)
        {
            log.Error("Exception processing results.", ex);
            return;
        }
    }

    /// <summary>
    /// Constructs the body of an email message to be sent
    /// </summary>
    /// <returns>Body of email message to be sent</returns>
    private string GetEmailBody()
    {
        StringBuilder sb = new();
        const string endOfLine = "\r\n";

        if (this.retrievedTitleIds.Count > 0)
        {
            sb.Append($"Retrieved {this.retrievedTitleIds.Count.ToString()} Title Identifiers{endOfLine}");
        }
        if (this.retrievedAuthorIds.Count > 0)
        {
            sb.Append($"Retrieved {this.retrievedAuthorIds.Count.ToString()} Author Identifiers{endOfLine}");
        }
        if (this.invalidIds.Count > 0)
        {
            sb.Append($"{endOfLine}{this.invalidIds.Count.ToString()} Invalid Identifiers Found{endOfLine}");
            foreach(var id in invalidIds)
            {
                var invalid = id.Split('|');
                sb.Append($"{invalid[0]} {invalid[1]} - {invalid[2]}:{invalid[3]}{endOfLine}");
            }
        }
        if (this.errorMessages.Count > 0)
        {
            sb.Append($"{endOfLine}{this.errorMessages.Count.ToString()} Errors Occurred{endOfLine}See the log file for details{endOfLine}{endOfLine}");
            foreach (string message in errorMessages)
            {
                sb.Append($"{message}{endOfLine}");
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Send the specified email message 
    /// </summary>
    /// <param name="message">Body of the message to be sent</param>
    private void SendEmail(string serviceName, string message)
    {
        try
        {
            if (errorMessages.Count > 0 && configParms.EmailOnError)
            {
                MailRequestModel mailRequest = new()
                {
                    Subject = string.Format(
                        "{0}: Harvesting on {1} completed {2}.",
                        serviceName,
                        Environment.MachineName,
                        (errorMessages.Count == 0 ? "successfully" : "with errors")),
                    Body = message,
                    From = configParms.EmailFromAddress
                };

                List<string> recipients = new();
                foreach (string recipient in configParms.EmailToAddress.Split(',')) recipients.Add(recipient);
                mailRequest.To = recipients;

                EmailClient restClient = new(configParms.BHLWSEndpoint);
                restClient.SendEmail(mailRequest);
            }
        }
        catch (Exception ex)
        {
            log.Error("Email Exception: ", ex);
        }
    }

    private static void LogMessage(string message)
    {
        // logger automatically adds date/time
        if (log.IsInfoEnabled) log.Info(message);
        Console.Write(message + "\r\n");
    }
}

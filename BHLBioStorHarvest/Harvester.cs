using MOBOT.BHL.SegmentClusterer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;
using EFModel = MOBOT.BHLImport.BHLImportEFDataModel;
using EFService = MOBOT.BHLImport.BHLImportEFDataService;

namespace MOBOT.BHL.BHLBioStorHarvest
{
    public class Harvester
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> itemsDownloaded = new List<string>();
        private List<string> itemsUnavailable = new List<string>();
        private List<string> itemsHarvested = new List<string>();
        private List<string> articlesHarvested = new List<string>();
        private List<string> itemsPreprocessed = new List<string>();
        private List<string> articlesPreprocessed = new List<string>();
        private List<string> itemsPublished = new List<string>();
        private List<string> articlesPublished = new List<string>();
        private List<string> errorMessages = new List<string>();

        private const string MODE_SINCEDATE = "SINCE";
        private const string MODE_ITEM = "ITEM";
        private const string MODE_FILE = "FILE";

        private EFService.DataService provider = new EFService.DataService();
        
        public void Harvest()
        {
            this.LogMessage("BHLBioStorHarvest Processing Start");

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

                bool noErrors = true;
                switch (configParms.Mode)
                {
                    case MODE_SINCEDATE:
                        if (!configParms.NoDownload)
                        {
                            noErrors = GetChangedItems();
                            // If successful, update the config file with the new SinceDate
                            if (noErrors) configParms.UpdateAppSetting("SinceDate", string.Format("{0:yyyy-MM-dd}", DateTime.Now));
                        }
                        if (noErrors) noErrors = CheckItemAvailability();  // Make sure items not inactive in BHL
                        if (noErrors) noErrors = HarvestItems();  // Harvest the data from BioStor
                        if (noErrors) noErrors = PreprocessData();    // Prepare the data for production
                        if (noErrors && !configParms.NoPublish) noErrors = PublishToProduction();   // Publish data to production
                        if (noErrors && !configParms.NoCluster) noErrors = ClusterSegments(); // Cluster the new segments
                        break;
                    case MODE_ITEM:
                        try
                        {
                            int itemID = this.InsertItem(configParms.BHLItemID);
                            noErrors = CheckItemAvailability(configParms.BHLItemID);  // Make sure item not inactive in BHL
                            if (noErrors) noErrors = HarvestItems(itemID);  // Harvest the item from BioStor
                            if (noErrors) noErrors = PreprocessData(configParms.BHLItemID);  // Prepare the data for production
                            if (noErrors && !configParms.NoPublish) noErrors = PublishToProduction(configParms.BHLItemID);  // Publish data to production
                            if (noErrors && !configParms.NoCluster) noErrors = ClusterSegments(configParms.BHLItemID);    // Cluster the new segments
                        }
                        catch (Exception ex)
                        {
                            // This should catch errors in the InsertItem and SetItem methods.
                            this.LogMessage("Error harvesting item.", ex);
                        }
                        break;
                    case MODE_FILE:
                        break;
                }
            }

            // Report the results
            this.ProcessResults();

            this.LogMessage("BHLBioStorHarvest Processing Complete");
        }

        #region Harvest

        private bool HarvestItems(int? itemID = null)
        {
            bool harvestComplete = true;
            try
            {
                List<EFModel.BSItem> items = provider.SelectItemsForDownload(itemID);

                this.LogMessage(string.Format("Selected {0} items for harvesting.", items.Count()));

                foreach (EFModel.BSItem item in items)
                {
                    // Stop if harvesting fails.  Consider continuing to the next item (instead of stopping).
                    if (!HarvestItem(item.ItemID, (int)item.BHLItemID))
                    {
                        harvestComplete = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogMessage("Error harvesting items.", ex);
                harvestComplete = false;
            }

            return harvestComplete;
        }

        private bool HarvestItem(int itemID, int bhlItemID)
        {
            bool isHarvested = false;

            WebClient webClient = new WebClient();
            string itemArticlesUrl = string.Format(configParms.BioStorItemArticlesUrl, bhlItemID.ToString());

            try
            {
                // Get the articles (JSON-encoded) from BioStor).
                string jsonString = webClient.DownloadString(itemArticlesUrl);

                // Persist the json string
                this.SaveArticleJson(bhlItemID, jsonString);

                // Deserialize the JSON using LINQ-To-JSON.
                JObject jsonObject = JObject.Parse(jsonString);

                JArray articles = (JArray)jsonObject["articles"];
                if (articles != null)
                {
                    // Create segments for each article.
                    short sequenceOrder = 0;
                    foreach (JObject article in articles)
                    {
                        sequenceOrder++;
                        EFModel.BSSegment segment = GetSegment(bhlItemID, sequenceOrder, article);
                        List<EFModel.BSSegmentAuthor> segmentAuthors = GetAuthors(article);
                        segment.ItemID = itemID;
                        provider.InsertSegment(segment, segmentAuthors);
                        articlesHarvested.Add(segment.Title);
                    }

                    // Log the articles inserted/updated
                    this.LogMessage(string.Format("Harvested {0} articles for item {1}.", sequenceOrder.ToString(), bhlItemID.ToString()));
                }

                // Mark the item as harvested
                SetItemHarvested(itemID);

                itemsHarvested.Add(bhlItemID.ToString());
                isHarvested = true;
            }
            catch (WebException wex)
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)wex.Response;
                string details = string.Empty;
                string statusCode = string.Empty;
                if (httpWebResponse != null)
                {
                    details = httpWebResponse.StatusDescription;
                    statusCode = httpWebResponse.StatusCode.ToString();
                }
                else
                {
                    details = wex.Message;
                }
                this.LogMessage(string.Format("Error harvesting item {0}.  Description: {1} {2}.",
                    bhlItemID.ToString(), statusCode, details), true);
            }
            catch (Exception ex)
            {
                this.LogMessage( string.Format("Error harvesting item {0}.", bhlItemID.ToString()), ex);
            }
            finally
            {
                webClient.Dispose();
            }

            return isHarvested;
        }

        private EFModel.BSSegment GetSegment(int itemID, short sequenceOrder, JObject article)
        {
            // Create a new segment.
            EFModel.BSSegment segment = new EFModel.BSSegment();
            segment.ItemID = itemID;
            segment.SequenceOrder = sequenceOrder;

            segment.Genre = ((string)article["genre"]) ?? string.Empty;
            segment.BioStorReferenceID = ((string)article["reference_id"]) ?? string.Empty;
            segment.Title = ((string)article["title"]) ?? string.Empty;
            segment.ContainerTitle = ((string)article["secondary_title"]) ?? string.Empty;
            segment.PublisherName = ((string)article["publisher"]) ?? string.Empty;
            segment.PublisherPlace = ((string)article["publoc"]) ?? string.Empty;
            segment.Volume = ((string)article["volume"]) ?? string.Empty;
            segment.Issue = ((string)article["issue"]) ?? string.Empty;
            segment.Series = ((string)article["series"]) ?? string.Empty;
            segment.Year = ((string)article["year"]) ?? string.Empty;
            segment.Date = ((string)article["date"]) ?? string.Empty;
            segment.StartPageNumber = ((string)article["spage"]) ?? string.Empty;
            segment.EndPageNumber = ((string)article["epage"]) ?? string.Empty;
            segment.ISSN = ((string)article["issn"]) ?? string.Empty;
            segment.DOI = this.GetDOI(article);
            segment.OCLC = ((string)article["oclc"]) ?? string.Empty;
            segment.JSTOR = ((string)article["jstor"]) ?? string.Empty;
            segment.ContributorName = ((string)article["contributor"]) ?? string.Empty;

            // Strip tabs, newlines, and carriage returns from title strings
            segment.Title = segment.Title.Replace("\t", " ").Replace("\n\r", " ").Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
            segment.ContainerTitle = segment.ContainerTitle.Replace("\t", " ").Replace("\n\r", " ").Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");

            string startPageIDString = ((string)article["PageID"]) ?? string.Empty;
            int startPageID;
            if (Int32.TryParse(startPageIDString, out startPageID)) segment.StartPageID = startPageID;

            string created = ((string)article["created"]) ?? string.Empty;
            DateTime contributorCreationDate;
            segment.ContributorCreationDate = DateTime.TryParse(created, out contributorCreationDate) ? (DateTime?)contributorCreationDate : null;

            string updated = ((string)article["update"]) ?? string.Empty;
            DateTime contributorLastModifiedDate;
            segment.ContributorLastModifiedDate = DateTime.TryParse(updated, out contributorLastModifiedDate) ? (DateTime?)contributorLastModifiedDate : null;

            // Get the pages for the article
            JArray pages = (JArray)article["bhl_pages"];
            if (pages != null)
            {
                short pageSequenceOrder = 1;
                foreach (int page in pages)
                {
                    EFModel.BSSegmentPage segmentPage = new EFModel.BSSegmentPage();
                    segmentPage.BHLPageID = Convert.ToInt32(page);
                    segmentPage.SequenceOrder = pageSequenceOrder;
                    pageSequenceOrder++;
                    segment.BSSegmentPages.Add(segmentPage);
                }
            }

            return segment;
        }

        private List<EFModel.BSSegmentAuthor> GetAuthors(JObject article)
        {
            List<EFModel.BSSegmentAuthor> segmentAuthors = new List<EFModel.BSSegmentAuthor>();

            // Get the authors for the article
            JArray authors = (JArray)article["authors"];
            foreach (JObject author in authors)
            {
                string lastName = ((string)author["lastname"]) ?? string.Empty;
                string firstName = ((string)author["forename"]) ?? string.Empty;

                // Filter out 'empty' author names
                if (lastName != string.Empty || firstName != string.Empty)
                {
                    string bioStorId = ((string)author["id"]) ?? string.Empty; ;

                    // Make sure this isn't a duplicate author
                    var duplicate = segmentAuthors.Find(a => 
                        a.BioStorID == bioStorId && a.LastName == lastName.Trim() && a.FirstName == firstName.Trim());
                    if (duplicate == default(EFModel.BSSegmentAuthor))
                    {
                        // Get the author info
                        EFModel.BSSegmentAuthor segmentAuthor = new EFModel.BSSegmentAuthor();
                        segmentAuthor.ImportSourceID = configParms.ImportSourceID;
                        segmentAuthor.BioStorID = ((string)author["id"]) ?? string.Empty;
                        segmentAuthor.LastName = lastName.Trim();
                        segmentAuthor.FirstName = firstName.Trim();
                        segmentAuthors.Add(segmentAuthor);
                    }
                }
            }

            return segmentAuthors;
        }

        private string GetDOI(JObject article)
        {
            string doi = ((string)article["doi"]) ?? string.Empty;
            if (doi.Length > 50) doi = doi.Substring(0, 50);
            if (doi != string.Empty)
            {
                // Make sure this DOI applies to the article, and not the the container object (journal)
                if (!IsArticleDOI(doi)) doi = string.Empty;
            }
            return doi;
        }

        #endregion Harvest

        #region Harvest supporting methods

        /// <summary>
        /// Save the json representation of the articles for an item.
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="jsonString"></param>
        private void SaveArticleJson(int itemID, string jsonString)
        {
            // Save the jsonString to the file system (consider a NoSQL storage option at some point).
            System.IO.File.WriteAllText(configParms.JsonFolder + string.Format(configParms.JsonFileFormat, itemID.ToString()), jsonString);
        }

        /// <summary>
        /// Get the list of items with updated article information from BioStor, and create
        /// database records for each.
        /// </summary>
        private bool GetChangedItems()
        {
            bool itemsRetreived = false;

            WebClient client = new WebClient();

            try
            {
                // Get the list of changed items from BioStor
                string jsonString = client.DownloadString(string.Format(configParms.BioStorItemsChangedSinceUrl, string.Format("{0:yyyy-MM-dd}", configParms.DateSince)));

                // Deserialize the JSON using LINQ-To-JSON.
                JObject jsonObject = JObject.Parse(jsonString);

                // Add a database record for each changed item
                JArray items = (JArray)jsonObject["items"];
                if (items != null)
                {
                    foreach (string itemId in items)
                    {
                        int idInt;
                        if (Int32.TryParse(itemId, out idInt))
                        {
                            InsertItem(idInt);
                            itemsDownloaded.Add(itemId);
                        }
                    }
                }

                LogMessage(string.Format("Retrieved {0} item identifiers from BioStor.", (items == null ? 0 : items.Count())));

                itemsRetreived = true;
            }
            catch (WebException wex)
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)wex.Response;
                string details = string.Empty;
                string statusCode = string.Empty;
                if (httpWebResponse != null)
                {
                    details = httpWebResponse.StatusDescription;
                    statusCode = httpWebResponse.StatusCode.ToString();
                }
                else
                {
                    details = wex.Message;
                }
                this.LogMessage(string.Format("Error getting changed items.  Description: {0} {1}.",
                    statusCode, details), true);
            }
            catch (Exception ex)
            {
                this.LogMessage("Error getting changed items.", ex);
            }
            finally
            {
                client.Dispose();
            }

            return itemsRetreived;
        }

        private int InsertItem(int itemID)
        {
            EFModel.BSItem item = new EFModel.BSItem();
            item.BHLItemID = itemID;
            return provider.AddItem(item);
        }

        private void SetItemHarvested(int itemID)
        {
            provider.SetItemHarvested(itemID);
        }

        /// <summary>
        /// Query CrossRef to see if the specified DOI is for an article or not.
        /// Article example: http://www.crossref.org/openurl?pid=sbhl:sbhl1018&id=doi:10.1080/00222938309459090&noredirect=true
        /// Non-article example: http://www.crossref.org/openurl?pid=sbhl:sbhl1018&id=doi:10.5962/bhl.title.47829&noredirect=true
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        private bool IsArticleDOI(string doi)
        {
            bool isArticle = false;

            try
            {
                string doiQuery = string.Format(configParms.CrossRefOpenUrlDOIGet, doi);
                XDocument xDoc = XDocument.Load(doiQuery);

                XElement queryElement = null;
                XNamespace ns = "http://www.crossref.org/qrschema/2.0";
                XElement crossrefResult = xDoc.Element(ns + "crossref_result");
                if (crossrefResult != null) crossrefResult = crossrefResult.Element(ns + "query_result");
                if (crossrefResult != null) crossrefResult = crossrefResult.Element(ns + "body");
                if (crossrefResult != null) queryElement = crossrefResult.Element(ns + "query");

                if (queryElement != null)
                {
                    XAttribute queryStatus = queryElement.Attribute("status");
                    if (queryStatus.Value == "resolved")
                    {
                        XElement doiElement = queryElement.Element(ns + "doi");
                        XAttribute doiType = doiElement.Attribute("type");

                        if (doiType.Value == "journal_article") isArticle = true;
                    }
                }
            }
            catch
            {
                // Do nothing, DOI will be discarded
            }

            return isArticle;
        }

        #endregion Harvest supporting methods

        #region Preprocessing

        /// <summary>
        /// Analyze the data just harvested, and prepare it for ingest into the production database.
        /// </summary>
        public bool PreprocessData(int? bhlItemID = null)
        {
            bool processSuccess = true;
            int itemID = 0;

            try
            {
                List<EFModel.BSItem> items = new List<EFModel.BSItem>();
                items = provider.SelectItemsForAuthorResolution(bhlItemID);
                foreach (EFModel.BSItem item in items)
                {
                    itemID = item.ItemID;

                    // Get the VIAF identifiers for the authors
                    this.GetVIAFIdentifiers(itemID);
                    provider.SetItemPreprocessed(itemID);

                    // Log the items for which authors were resolved
                    itemsPreprocessed.Add(itemID.ToString());
                }

                this.LogMessage("Pre-processed " + itemsPreprocessed.Count.ToString() + " items.");
            }
            catch (Exception ex)
            {
                this.LogMessage("Error preprocessing data" + (itemID > 0 ? " for Item " + itemID.ToString() + "." : "."), ex);
                processSuccess = false;
            }

            return processSuccess;
        }

        private void GetVIAFIdentifiers(int itemID)
        {
            List<EFModel.BSSegment> segments = provider.SelectSegmentsForItem(itemID);
            foreach (EFModel.BSSegment segment in segments)
            {
                List<EFModel.BSSegmentAuthor> authors = provider.GetSegmentAuthorsForSegment(configParms.ImportSourceID, segment.SegmentID);
                foreach (EFModel.BSSegmentAuthor author in authors)
                {
                    // TODO: Put the following in a separate assembly for sharing with other apps
                    // TODO: Get the VIAF identifier for the author


                }

                articlesPreprocessed.Add(segment.SegmentID.ToString());
            }
        }

        #endregion Preprocessing

        #region Publish To Production

        public bool PublishToProduction(int? bhlItemID = null)
        {
            bool processSuccess = true;
            int itemID = 0;

            try
            {
                // Get all items with segments that are ready to be published
                List<EFModel.BSItem> items = provider.SelectItemsForPublishing(bhlItemID);

                foreach (EFModel.BSItem item in items)
                {
                    itemID = item.ItemID;

                    // Get all of the ready-to-publish segments for the item
                    List<EFModel.BSSegment> segments = provider.SelectSegmentsForPublishing(itemID);

                    foreach (EFModel.BSSegment segment in segments)
                    {
                        // Check the production database to see if we aleady have records for this segment's authors
                        provider.ResolveSegmentAuthors(segment.SegmentID);

                        // Publish the segment to production
                        provider.PublishSegment(Convert.ToInt32(item.BHLItemID), segment.SegmentID);

                        articlesPublished.Add(segment.SegmentID.ToString());
                    }

                    // Log the articles inserted/updated
                    this.LogMessage(string.Format("Published {0} articles for item {1}.", segments.Count().ToString(), item.BHLItemID.ToString()));

                    provider.SetItemPublished(itemID);
                    itemsPublished.Add(itemID.ToString());
                }
            }
            catch (Exception ex)
            {
                this.LogMessage("Error publishing to production" + (itemID > 0 ? " for Item " + itemID.ToString() + "." : "."), ex);
                try
                {
                    // Update the item to indicate the error
                    if (itemID > 0) provider.SetItemPublishError(itemID);
                }
                catch
                {
                    // do nothing
                }
                processSuccess = false;
            }

            return processSuccess;
        }

        #endregion Publish To Production

        #region Cluster Segments

        public bool ClusterSegments(int? itemID = null)
        {
            bool clusterSuccess = true;
            this.LogMessage("Segment clustering started.");

            try
            {
                new Clusterer().Cluster(itemID);
                this.LogMessage("Segment clustering complete" + ((itemID ?? 0) > 0 ? " for Item " + itemID.ToString() + "." : "."));
            }
            catch (Exception ex)
            {
                this.LogMessage("Error clustering segments" + ((itemID ?? 0) > 0 ? " for Item " + itemID.ToString() + "." : "."), ex);
                clusterSuccess = false;
            }

            return clusterSuccess;
        }

        #endregion Cluster Segments

        #region Utility methods

        private bool CheckItemAvailability(int? bhlItemID = null)
        {
            bool success = true;

            try
            {
                List<EFModel.BSItem> unavailableItems = provider.CheckItemAvailability(bhlItemID);
                foreach (EFModel.BSItem item in unavailableItems) itemsUnavailable.Add(item.ItemID.ToString());

                this.LogMessage("Found " + itemsUnavailable.Count.ToString() + " items inactive in BHL.");
            }
            catch (Exception ex)
            {
                this.LogMessage("Error checking item availability", ex);
                success = false;
            }

            return success;
        }

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
                    this.LogMessage("Invalid command line format.  Format is BHLBioStorHarvest.exe [/ITEM:itemid] [/FILE:filename] [/SINCEDATE:YYYY-MM-DD]");
                    return false;
                }

                if (String.Compare(split[0], "/ITEM", true) == 0)
                {
                    configParms.Mode = MODE_ITEM;
                    configParms.BHLItemID = Convert.ToInt32(split[1]);
                }

                if (String.Compare(split[0], "/FILE", true) == 0)
                {
                    configParms.Mode = MODE_FILE;
                    configParms.File = split[1];
                }

                if (String.Compare(split[0], "/SINCEDATE", true) == 0)
                {
                    configParms.Mode = MODE_SINCEDATE;
                    configParms.DateSince = Convert.ToDateTime(split[1]);
                }
            }

            return true;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            if (configParms.Mode == MODE_ITEM && configParms.BHLItemID <= 0)
            {
                this.LogMessage("Item not set correctly.  When Mode is \"ITEM\", Item must contain a valid value.  Check configuration file.");
                return false;
            }

            if (configParms.Mode == MODE_FILE && configParms.File == string.Empty)
            {
                this.LogMessage("File not set correctly.  When Mode is \"FILE\", File must contain a valid value.  Check configuration file.");
                return false;
            }

            if (configParms.Mode == MODE_SINCEDATE && configParms.DateSince == DateTime.MinValue)
            {
                this.LogMessage("Date not set correctly.  When Mode is \"SINCE\", SinceDate must contain a valid value.  Check configuration file.");
                return false;
            }

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
                if (itemsDownloaded.Count > 0 || itemsUnavailable.Count > 0 ||
                    itemsHarvested.Count > 0 || articlesHarvested.Count > 0 || 
                    itemsPreprocessed.Count > 0 || articlesPreprocessed.Count > 0 ||
                    itemsPublished.Count > 0 || articlesPublished.Count > 0 ||
                    errorMessages.Count > 0)
                {
                    String subject = String.Empty;
                    String thisComputer = Environment.MachineName;
                    if (this.errorMessages.Count == 0)
                    {
                        subject = "BHLBioStorHarvest: BioStor harvesting on " + thisComputer + " completed successfully.";
                    }
                    else
                    {
                        subject = "BHLBioStorHarvest: BioStor harvesting on " + thisComputer + " completed with errors.";
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

            sb.Append("BHLBioStorHarvest: BioStor harvesting on " + thisComputer + " complete." + endOfLine);
            if (this.itemsDownloaded.Count > 0)
            {
                sb.Append(endOfLine + this.itemsDownloaded.Count.ToString() + " Items were Downloaded from BioStor" + endOfLine);
            }
            if (this.itemsUnavailable.Count > 0)
            {
                sb.Append(endOfLine + this.itemsUnavailable.Count.ToString() + " Items are Unavailable in BHL" + endOfLine);
            }
            if (this.itemsHarvested.Count > 0)
            {
                sb.Append(endOfLine + this.itemsHarvested.Count.ToString() + " Items were Harvested" + endOfLine);
            }
            if (this.articlesHarvested.Count > 0)
            {
                sb.Append(endOfLine + this.articlesHarvested.Count.ToString() + " Articles were Harvested" + endOfLine);
            }
            if (this.itemsPreprocessed.Count > 0)
            {
                sb.Append(endOfLine + this.itemsPreprocessed.Count.ToString() + " Items were Preprocessed" + endOfLine);
            }
            if (this.articlesPreprocessed.Count > 0)
            {
                sb.Append(endOfLine + this.articlesPreprocessed.Count.ToString() + " Articles were Preprocessed" + endOfLine);
            }
            if (this.itemsPublished.Count > 0)
            {
                sb.Append(endOfLine + this.itemsPublished.Count.ToString() + " Items were Published to Production" + endOfLine);
            }
            if (this.articlesPublished.Count > 0)
            {
                sb.Append(endOfLine + this.articlesPublished.Count.ToString() + " Articles were Published to Production" + endOfLine);
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

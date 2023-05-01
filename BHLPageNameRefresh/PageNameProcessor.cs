using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MOBOT.BHL.PageNameRefresh
{
    public class PageNameProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private static ConfigParms configParms = new ConfigParms();
        private static List<string> processedItems= new List<string>();
        private static List<string> processedPages = new List<string>();
        private static List<ICollection<int>> processedPageNames = new List<ICollection<int>>();
        private static List<string> errorMessages = new List<string>();

        private const string MODE_NEW = "NEW";
        private const string MODE_EXPIRED = "OLD";
        private const string MODE_ITEM = "ITEM";

        private static Semaphore _semaphore = new Semaphore();

        /// <summary>
        /// Read and validate configuration parameters, and initiate the appropriate
        /// processor.
        /// </summary>
        public void Process()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            switch (configParms.Mode)
            {
                case MODE_NEW:
                    this.ProcessNew();
                    break;
                case MODE_EXPIRED:
                    this.ProcessExpired();
                    break;
                case MODE_ITEM:
                    this.ProcessItem();
                    break;
            }

            // Report the results of item/page processing
            this.ProcessResults();

            LogMessage("BHLPageNameRefresh Processing Complete", null);
        }

        /// <summary>
        /// Process all newly added items (items that have never had their page names 
        /// retreived) that have OCR files.
        /// </summary>
        private void ProcessNew()
        {
            ItemsClient itemRestClient = new ItemsClient(configParms.BHLWSEndpoint);
            PagesClient pageRestClient = new PagesClient(configParms.BHLWSEndpoint);
            OcrClient ocrRestClient = new OcrClient(configParms.BHLWSEndpoint);

            try
            {
                LogMessage("Processing new items...", null);

                // Get all items for which no Page Names have been retrieved
                LogMessage("Getting items for which Page Names have not been retrieved.", null);
                ICollection<Item> items = itemRestClient.GetItemWithoutNames();
                ICollection<Page> pages = null;

                foreach (Item item in items)
                {
                    try
                    {
                        // Make sure we have an OCR file for this item
                        LogMessage("Make sure an OCR file exists for item: " + item.BarCode, null);
                        if (ocrRestClient.ItemOcrExists((int)item.ItemID, configParms.OcrTextPath))
                        {
                            // Get the pages for this item and process them
                            LogMessage("Getting pages for which Page Names have not been retrieved for item: " + item.BarCode, null);
                            pages = pageRestClient.GetPageWithoutNames(item.ItemID);
                            this.ProcessPages(pages);
                            itemRestClient.UpdateItemLastPageNameLookupDate((int)item.ItemID);
                            processedItems.Add(item.BarCode);
                            LogMessage("Processing complete for item: " + item.BarCode, null);
                        }
                        else
                        {
                            LogMessage("No OCR files found for item ID: " + item.BarCode, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogMessage("Exception processing item " + item.BarCode, ex);
                        // don't bomb.  try next item
                    }
                }

                // Get any left-over individual pages for which no Page Names have been 
                // retrieved, and process them.
                LogMessage("Getting pages for which Page Names have not been retrieved.", null);
                pages = pageRestClient.GetPageWithoutNames();
                this.ProcessPages(pages);

                LogMessage("New item processing complete.", null);
            }
            catch (Exception ex)
            {
                LogMessage("Exception processing new items", ex);
            }
        }

        /// <summary>
        /// Process all items whose page names have expired.  Page names expire when the date
        /// that they were last retrieved exceeds a specified age.
        /// </summary>
        private void ProcessExpired()
        {
            ItemsClient itemRestClient = new ItemsClient(configParms.BHLWSEndpoint);
            PagesClient pageRestClient = new PagesClient(configParms.BHLWSEndpoint);
            OcrClient ocrRestClient = new OcrClient(configParms.BHLWSEndpoint);

            try
            {
                LogMessage("Processing expired page names...", null);

                // Get all items for which the Page Names are older than the specified maximum age
                LogMessage("Getting items with Page Names retrieved more than " + configParms.MaximumPageNameAge.ToString() + " days ago.", null);
                ICollection<Item> items = itemRestClient.GetItemWithExpiredNames(configParms.MaximumPageNameAge);

                foreach (Item item in items)
                {
                    try
                    {
                        // Make sure we have an OCR file for this item
                        LogMessage("Make sure an OCR file exists for item: " + item.BarCode, null);
                        if (ocrRestClient.ItemOcrExists((int)item.ItemID, configParms.OcrTextPath))
                        {
                            // Get the pages for this item and process them
                            LogMessage("Getting pages for item " + item.BarCode + " with Page Names retrieved more than " + configParms.MaximumPageNameAge.ToString() + " days ago.", null);
                            ICollection<Page> pages = pageRestClient.GetPagesWithExpiredNames((int)item.ItemID, configParms.MaximumPageNameAge);
                            this.ProcessPages(pages);
                            itemRestClient.UpdateItemLastPageNameLookupDate((int)item.ItemID);
                            processedItems.Add(item.BarCode);
                            LogMessage("Processing complete for item: " + item.BarCode, null);
                        }
                        else
                        {
                            LogMessage("No OCR files found for item ID: " + item.BarCode, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogMessage("Exception processing item " + item.BarCode, ex);
                        // don't bomb.  try next item
                    }
                }

                LogMessage("Expired page name processing complete.", null);
            }
            catch (Exception ex)
            {
                LogMessage("Exception processing expired items", ex);
            }
        }

        /// <summary>
        /// Process the specified item.
        /// </summary>
        private void ProcessItem()
        {
            ItemsClient itemRestClient = new ItemsClient(configParms.BHLWSEndpoint);
            OcrClient ocrRestClient = new OcrClient(configParms.BHLWSEndpoint);

            try
            {
                LogMessage("Processing a single item ID: " + configParms.Item + " ...", null);

                // Make sure we have an OCR file for the specified item
                LogMessage("Make sure an OCR file exists for item ID: " + configParms.Item, null);
                if (ocrRestClient.ItemOcrExists(Convert.ToInt32(configParms.Item), configParms.OcrTextPath))
                {
                    // Get the pages for this item and process them
                    LogMessage("Getting pages for item ID: " + configParms.Item, null);
                    ICollection<Page> pages = itemRestClient.GetItemPages(Convert.ToInt32(configParms.Item));
                    this.ProcessPages(pages);
                    itemRestClient.UpdateItemLastPageNameLookupDate(Convert.ToInt32(configParms.Item));
                    processedItems.Add(configParms.Item + " (ItemID)");
                    LogMessage("Processing complete for item ID: " + configParms.Item, null);
                }
                else
                {
                    LogMessage("No OCR files found for item ID: " + configParms.Item, null);
                }
            }
            catch (Exception ex)
            {
                LogMessage("Exception processing item ID " + configParms.Item, ex);
            }
        }

        /// <summary>
        /// For each of the pages in the specified list, check to see if an OCR file exists
        /// for the page and if so, do the following:
        ///     1) Get the list of page names from UBIO
        ///     2) Update the page names stored in the local database
        ///     3) Update the LastPageNameLookupDate for the page
        /// </summary>
        /// <param name="pages">The list pages to be processed</param>
        private void ProcessPages(ICollection<Page> pages)
        {
            if (configParms.DoAsync)
            {
                //ProcessPagesAsynch(pages);
                throw new NotImplementedException("Asynchronous processing of pages is not available");
            }
            else
            {
                ProcessPagesSynch(pages);
            }
        }

        /// <summary>
        /// Process pages synchronously.  Best approach for uBio services.
        /// </summary>
        /// <param name="pages"></param>
        private void ProcessPagesSynch(ICollection<Page> pages)
        {
            PagesClient pageRestClient = new PagesClient(configParms.BHLWSEndpoint);
            OcrClient ocrRestClient = new OcrClient(configParms.BHLWSEndpoint);

            try
            {
                LogMessage("Processing pages...", null);
                
                foreach (Page page in pages)
                {
                    DateTime startTime = System.DateTime.Now;

                    try
                    {
                        if (ocrRestClient.PageOcrExists((int)page.PageID))
                        {
                            LogMessage("Getting Page Names from UBIO for page: " + page.FileNamePrefix, null);
                            if (!ocrRestClient.PageEmptyOcr((int)page.PageID))
                            {
                                ICollection<NameFinderResponse> pageNames = ocrRestClient.GetNamesFromPageOcr((int)page.PageID);
                                //BHLWS.FindItItem[] pageNames = this.ClonePageNameList(ubioPageNames);
                                LogMessage("Updating Page Names for page " + page.FileNamePrefix + " with " + pageNames.Count + " Page Names from '" + configParms.NameServiceSourceName + "'.", null);
                                ICollection<int> updateStats = pageRestClient.UpdatePageNames((int)page.PageID, new PageNameModel
                                {
                                    Nameinfo = pageNames,
                                    Sourcename = configParms.NameServiceSourceName
                                });
                                processedPageNames.Add(updateStats);
                            }
                            pageRestClient.UpdatePageLastPageNameLookupDate((int)page.PageID);
                            processedPages.Add(page.FileNamePrefix);
                            LogMessage("Processing complete for page: " + page.FileNamePrefix, null);
                        }
                        else
                        {
                            LogMessage("No OCR file found for page ID: " + page.FileNamePrefix, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogMessage("Exception processing page " + page.FileNamePrefix, ex);
                        // don't bomb.  try next page
                    }
                    finally
                    {
                        // Wait until the specified interval has completed before continuing.
                        // This delay is included here so that we don't overwhelm the GN server.
                        try
                        {
                            // Determine how long it took to process the page
                            TimeSpan ts = DateTime.Now.Subtract(startTime);
                            // Calculate the number of milliseconds we still need to wait
                            int sleepTime = (configParms.ExternalWebServiceInterval * 500) - Convert.ToInt32(ts.TotalMilliseconds);
                            // Sleep if we haven't already used up the entire interval
                            if (sleepTime > 0) System.Threading.Thread.Sleep(sleepTime);
                        }
                        catch { }
                    }
                }

                LogMessage("Page processing complete.", null);
            }
            catch (Exception ex)
            {
                LogMessage("Exception processing pages", ex);
            }
        }

        /*
        /// <summary>
        /// Process pages synchronously.  Best approach for GNRD services.
        /// </summary>
        /// <param name="pages"></param>
        private void ProcessPagesAsynch(ICollection<Page> pages)
        {
            OcrClient ocrRestClient = new OcrClient(configParms.BHLWSEndpoint);

            try
            {
                LogMessage("Processing pages...", null);
                service.GetNamesFromOcrCompleted += GetNamesCompletedEventHandler;

                object thisLock = new object();

                bool doGet = false;
                foreach (Page page in pages)
                {
                    try
                    {
                        if (ocrRestClient.PageOcrExists((int)page.PageID))
                        {
                            doGet = false;
                            while (!doGet)
                            {
                                lock (_semaphore)
                                {
                                    // Limit the number of concurrent calls to the name finder service
                                    if (_semaphore.Count < configParms.MaxConcurrent)
                                    {
                                        _semaphore.Count++;
                                        doGet = true;
                                    }
                                }

                                if (!doGet) Thread.Sleep(100);
                            }

                            LogMessage("Getting Page Names from UBIO for page: " + page.FileNamePrefix, null);
                            ocrRestClient.GetNamesFromPageOcr((int)page.PageID);
                        }
                        else
                        {
                            LogMessage("No OCR file found for page ID: " + page.FileNamePrefix, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogMessage("Exception processing page " + page.FileNamePrefix, ex);
                        // don't bomb.  try next page
                    }
                }

                LogMessage("Page processing complete.", null);
            }
            catch (Exception ex)
            {
                LogMessage("Exception processing pages", ex);
            }
        }


        /// <summary>
        /// Process the response from the name finder service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void GetNamesCompletedEventHandler(object sender, GetNamesFromOcrCompletedEventArgs e)
        {
            PagesClient pageRestClient = new PagesClient(configParms.BHLWSEndpoint);
            StringBuilder sb = new StringBuilder();

            int pageID;
            string fileNamePrefix = string.Empty;

            try
            {
                // Get the PageID and FileNamePrefix
                string[] userState = e.UserState.ToString().Split('|');
                pageID = Convert.ToInt32(userState[0]);
                fileNamePrefix = userState[1];

                // Get the service results
                ICollection<NameFinderResponse> preferredResults = (NameFinderResponse[])e.Result;

                LogMessage("Updating Names for page " + fileNamePrefix + " with " + preferredResults.Count + " Names from '" + configParms.NameServiceSourceName + "'", null);
                ICollection<int> updateStats = pageRestClient.UpdatePageNames(pageID, new PageNameModel {
                    Nameinfo = preferredResults,
                    Sourcename = configParms.NameServiceSourceName
                });
                pageRestClient.UpdatePageLastPageNameLookupDate(pageID);
                processedPages.Add(fileNamePrefix);
                processedPageNames.Add(updateStats);
                LogMessage("Processing complete for page: " + fileNamePrefix, null);
            }
            catch (Exception ex)
            {
                LogMessage("Exception processing page " + fileNamePrefix, ex);
            }

            // Output the results and decrement the semaphore count so that another page can be sent to the name finder
            lock (_semaphore)
            {
                System.IO.File.AppendAllText("output.log", sb.ToString());
                if (_semaphore.Count > 0) _semaphore.Count--;
            }
        }
        */

        /// <summary>
        /// Examine the results of the item/page processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // send email with process results to Exchange group
                if (processedItems.Count > 0 || processedPages.Count > 0 || errorMessages.Count > 0)
                {
                    LogMessage("Sending Email....", null);
                    string message = this.GetEmailBody();
                    LogMessage(message, null);
                    SendEmail(message);
                }
                else
                {
                    LogMessage("No items or pages processed.  Email not sent.", null);
                }
            }
            catch (Exception ex)
            {
                LogMessage("Exception sending email", ex);
                return;
            }
        }

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

            for (int x=0; x<args.Length; x++)
            {
                if (x > 0)  // first argument is the EXE name; skip it
                {
                    string arg = args[x];
                    keyValue = arg.Split(':');

                    if (keyValue.Length != 2)
                    {
                        LogMessage("Invalid command line argument " + keyValue[0] + ".  Format is BHLPageNameRefresh.exe [/MODE:value [/ITEM:itemno]] [/INTERVAL:n]", null);
                        returnValue = false;
                        break;
                    }

                    switch (keyValue[0].ToUpper())
                    {
                        case "/MODE":
                            configParms.Mode = keyValue[1].ToUpper();
                            break;
                        case "/ITEM":
                            configParms.Item = keyValue[1];
                            break;
                        case "/INTERVAL":
                            configParms.ExternalWebServiceInterval = Convert.ToInt32(keyValue[1]);
                            break;
                        default:
                            LogMessage("Invalid command line argument " + keyValue[0] + ".  Format is BHLPageNameRefresh.exe [/MODE:value [/ITEM:itemno]] [/INTERVAL:n]", null);
                            returnValue = false;
                            break;
                    }
                }
            }

            if (configParms.Mode == MODE_ITEM && configParms.Item == string.Empty && returnValue == true)
            {
                LogMessage("Invalid command line arguments.  When /MODE:ITEM is specified, /ITEM:itemno must also be specified.  Format is BHLPageNameRefresh.exe [/MODE:value [/ITEM:itemno]] [/INTERVAL:n]", null);
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            if (configParms.MaximumPageNameAge < 0)
            {
                LogMessage("Maximum Page Name age not set correctly.  Check configuration file.", null);
                return false;
            }

            if (configParms.ExternalWebServiceInterval < 0)
            {
                LogMessage("External web service delay interval not set correctly.  Check configuration file.", null);
                return false;
            }

            if (configParms.Mode != MODE_NEW && configParms.Mode != MODE_EXPIRED && configParms.Mode != MODE_ITEM)
            {
                LogMessage("Mode not set correctly.  Valid values are \"NEW\", \"OLD\", and \"ITEM\".  Check configuration file.", null);
                return false;
            }

            if (configParms.Mode == MODE_ITEM && configParms.Item == string.Empty)
            {
                LogMessage("Item not set correctly.  When Mode is \"ITEM\", Item must contain a valid item number.  Check configuration file.", null);
                return false;
            }

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
            string itemType = string.Empty;
            switch (configParms.Mode)
            {
                case MODE_NEW:
                    itemType = "NEW items";
                    break;
                case MODE_EXPIRED:
                    itemType = "EXPIRED page names";
                    break;
                case MODE_ITEM:
                    itemType = "ITEM " + configParms.Item;
                    break;
            }

            sb.Append("BHLPageNameRefresh: Page Name Processing of " + itemType + " on " + thisComputer + " complete." + endOfLine);
            if (processedItems.Count > 0)
            {
                sb.Append(endOfLine + "Processed " + processedItems.Count.ToString() + " Items" + endOfLine);
            }
            if (processedPages.Count > 0)
            {
                sb.Append(endOfLine + "Processed " + processedPages.Count.ToString() + " Pages" + endOfLine);
            }
            if (processedPageNames.Count > 0)
            {
                int inserted = 0;
                int updated = 0;
                int deleted = 0;
                int unchanged = 0;

                foreach (ICollection<int> stats in processedPageNames)
                {
                    inserted += ((List<int>)stats)[0];
                    updated += ((List<int>)stats)[1];
                    deleted += ((List<int>)stats)[2];
                    unchanged += ((List<int>)stats)[3];
                }

                sb.Append(endOfLine + "Page Names Inserted: " + inserted.ToString() + endOfLine);
                sb.Append("Page Names Updated: " + updated.ToString() + endOfLine);
                sb.Append("Page Names Deleted: " + deleted.ToString() + endOfLine);
                sb.Append("Page Names Unchanged: " + unchanged.ToString() + endOfLine);
            }
            if (errorMessages.Count > 0)
            {
                sb.Append(endOfLine + errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details" + endOfLine);
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
                MailRequestModel mailRequest = new MailRequestModel();
                mailRequest.Subject = string.Format(
                    "BHLPageNameRefresh: Page Name Processing on {0} completed {1}.",
                    Environment.MachineName,
                    (errorMessages.Count == 0) ? "successfully" : "with errors");
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
                LogMessage("Email Exception", ex);
            }
        }

        private static void LogMessage(string message, Exception ex)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");

            if (ex != null)
            {
                log.Error(message, ex);
                while (ex.InnerException != null) ex = ex.InnerException;
                errorMessages.Add(message + ":" + ex.Message);

            // -- Uncomment during debugging --
            // Console.Write("ERROR: " + ex.Message + "\r\n");
            }
        }

        private class Semaphore
        {
            public int Count = 0;
        }
    }
}

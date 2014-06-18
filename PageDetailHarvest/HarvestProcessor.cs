using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PageDetailHarvest
{
    internal class HarvestProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();

        private List<string> processingIncompleteList = new List<string>();
        private List<string> processingErrorList = new List<string>();
        private List<string> notInDBList = new List<string>();
        private List<string> invalidCountList = new List<string>();
        private List<string> errorSaveList = new List<string>();
        private List<string> loadedPagesList = new List<string>();

        private List<string> errorMessages = new List<string>();
        private int numFilesHarvested = 0;
        private int numRecordsExported = 0;
        private int numClassifierFilesHarvested = 0;

        public void Process()
        {
            this.LogMessage("PageDetailHarvest Processing Start");

            try 
            {
                // Load app settings from the configuration file
                configParms.LoadAppConfig();
            }
            catch (Exception e)
            {
                this.LogMessage("LoadAppConfig Error: " + e.Message, true);
            }

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            switch (configParms.Mode)
            {
                case "HARVEST_EXTRACT":
                    HarvestExtract();
                    break;
                case "CLASSIFIER_EXPORT":
                    ClassifierExport();
                    break;
                case "CLASSIFIER_IMPORT":
                    ClassifierImport();
                    break;
                default:
                    Console.WriteLine("Unknown Mode");
                    break;
            }

            // Report the results
            this.ProcessResults();

            this.LogMessage("PageDetailHarvest Processing Complete");
        }

        /// <summary>
        /// Harvest the data extracted by the IMA algorithms
        /// </summary>
        private void HarvestExtract()
        {
            // Get new algorithm extracts
            GetNewExtracts();            

            // Read files to be processed from the input directory
            string[] inputFileNames = Directory.GetFiles(configParms.ExtractInputFolder);

            LogMessage(string.Format("Beginning harvest of {0} files", inputFileNames.Count()));

            foreach (string inputFileName in inputFileNames)
            {
                if (HasBeenProcessed(inputFileName))
                {
                    LogMessage(string.Format("Skipping previously harvested file \"{0}\"", inputFileName));
                    File.Delete(inputFileName);
                    continue;
                }

                try
                {
                    // Read the page details from the data file
                    List<PageDetail> pageDetailList = GetPageDetails(inputFileName);

                    // Get the items with incomplete pages
                    List<string> incompleteItems = (from i in pageDetailList
                                                    where i.ProcessingComplete == false
                                                    group i by i.Barcode into g
                                                    select g.Key.ToString()).ToList();

                    // Get the items with pages in error
                    List<string> errorItems = (from i in pageDetailList
                                               where i.ProcessingError == true
                                               group i by i.Barcode into g
                                               select g.Key.ToString()).ToList();

                    // Get the items with pages not found in the database
                    var missingItems = (from i in pageDetailList
                                        where i.PageID == 0
                                        group i by i.Barcode into g
                                        select g.Key.ToString()).ToList();

                    // Get the number of pages processed for each item
                    var pageCounts = from i in pageDetailList
                                     group i by i.Barcode into g
                                     select new { Barcode = g.Key, TotalPages = g.Count() };

                    // Get the number of pages for each item from the database
                    Dictionary<string, int> dbPageCounts = ItemSelectPageCounts();

                    // Accumulate a list of items where the number of pages in the database do not agree 
                    // with the number of pages being harvested
                    List<string> itemsMissingPages = new List<string>();
                    foreach (var i in pageCounts)
                    {
                        bool countsMatch = false;
                        if (dbPageCounts.ContainsKey(i.Barcode)) countsMatch = (dbPageCounts[i.Barcode] == i.TotalPages);
                        if (!countsMatch) itemsMissingPages.Add(i.Barcode);
                    }


                    // Write the metadata to the database
                    foreach (PageDetail pageDetail in pageDetailList)
                    {
                        if (incompleteItems.Contains(pageDetail.Barcode))
                        {
                            processingIncompleteList.Add(pageDetail.Id);    // This page is part of an item with incompletely processed pages
                        }
                        else if (errorItems.Contains(pageDetail.Barcode))
                        {
                            processingErrorList.Add(pageDetail.Id); // This page is part of an item with pages in error
                        }
                        else if (missingItems.Contains(pageDetail.Barcode))
                        {
                            notInDBList.Add(pageDetail.Id); // This page is part of an item with pages not found in the database
                        }
                        // IMPORTANT:
                        // Only do this check if it is confirmed that there are not expected to be pages missing 
                        // from the harvested data.  
                        // If it *IS* valid/normal for pages to be missing from the harvested data, then this 
                        // check is useless... and that means that in isolated cases we might end up attaching 
                        // the image metadata to the wrong page in BHL.
                        else if (itemsMissingPages.Contains(pageDetail.Barcode))
                        {
                            invalidCountList.Add(pageDetail.Id);    // This page is part of an item with invalid # of total pages
                        }
                        else
                        {
                            // Write image information to the database
                            if (SavePageDetail(pageDetail))
                                loadedPagesList.Add(pageDetail.Id);
                            else
                                errorSaveList.Add(pageDetail.Id);
                        }
                    }

                    // Log the results of the harvest
                    LogResults(inputFileName);

                    // Move the file to the "Complete" folder
                    CreateDirectory(configParms.ExtractCompleteFolder);
                    File.Move(inputFileName, configParms.ExtractCompleteFolder + Path.GetFileName(inputFileName));

                    LogMessage(string.Format("Done harvesting \"{0}\"", inputFileName));
                }
                catch (Exception ex)
                {
                    // Move the file to the Error folder
                    CreateDirectory(configParms.ExtractErrorFolder);
                    string errorFileName = configParms.ExtractErrorFolder + Path.GetFileName(inputFileName);
                    if (File.Exists(errorFileName)) File.Delete(errorFileName);
                    File.Move(inputFileName, errorFileName);

                    LogMessage(string.Format("Error harvesting \"{0}\"", inputFileName), ex);
                }
            }

            numFilesHarvested = inputFileNames.Count();
            LogMessage(string.Format("Done harvesting {0} files", numFilesHarvested));
        }

        /// <summary>
        /// Export to JSON the page details for import into the Classifier tool.
        /// </summary>
        private void ClassifierExport()
        {
            LogMessage("Beginning export for Classifier");

            try
            {
                List<PageClassifierExport> pageDetailList = PageDetailSelectForClassifierExport();

                CreateDirectory(configParms.ClassifierOutputFolder);

                // Accumulate the list to be serialized as JSON
                ClassifierExportJson export = new ClassifierExportJson();
                ClassifierExportItem exportItem = null;
                ClassifierExportPage exportPage = null;
                int prevItemID = 0;
                int prevPageID = 0;
                List<int> processedPages = new List<int>();
                foreach (PageClassifierExport pageDetail in pageDetailList)
                {
                    int itemID = pageDetail.ItemID;
                    int pageID = pageDetail.PageID;

                    if (pageID != prevPageID)
                    {
                        if (exportPage != null) exportItem.Pages.Add(exportPage);
                        exportPage = new ClassifierExportPage();

                        exportPage.PageUrl = configParms.PageUrlPrefix + pageID.ToString();
                        exportPage.SequenceOrder = pageDetail.SequenceOrder;
                        exportPage.AbbyyHasIllustration = (pageDetail.AbbyyHasImage == 1);
                        exportPage.ContrastHasIllustration = (pageDetail.ContrastHasImage == 1);
                        exportPage.PercentCoverage = pageDetail.PercentCoverage;
                        exportPage.Height = pageDetail.Height;
                        exportPage.Width = pageDetail.Width;

                        processedPages.Add(pageID);
                    }

                    if (pageDetail.Top != null)
                    {
                        ClassifierExportIllustration illustration = new ClassifierExportIllustration();
                        illustration.Top = (int)pageDetail.Top;
                        illustration.Bottom = (int)pageDetail.Bottom;
                        illustration.Left = (int)pageDetail.Left;
                        illustration.Right = (int)pageDetail.Right;
                        exportPage.Illustrations.Add(illustration);
                    }

                    if (itemID != prevItemID)
                    {
                        if (exportItem != null) export.Items.Add(exportItem);

                        if (processedPages.Count > configParms.ClassifierOutputFilePageLimit)
                        {
                            // Serialize the export to JSON and write it to a file
                            WriteClassifierExport(export);

                            // Update the status of the records that were just exported
                            UpdatePageDetailStatus(processedPages, PageDetailStatus.Classifying);

                            // Reset objects and counts
                            export = new ClassifierExportJson();
                            processedPages.Clear();
                        }

                        exportItem = new ClassifierExportItem();

                        exportItem.ItemUrl = configParms.ItemUrlPrefix + itemID.ToString();
                        exportItem.Barcode = pageDetail.BarCode ?? string.Empty;
                        exportItem.Author = pageDetail.Authors.Split('|')[0] ?? string.Empty;
                        exportItem.Title = pageDetail.ShortTitle ?? string.Empty;
                        exportItem.PublicationDetails = pageDetail.PublicationDetails ?? string.Empty;
                        exportItem.Volume = pageDetail.Volume ?? string.Empty;
                        exportItem.Copyright = pageDetail.CopyrightStatus ?? string.Empty;
                        exportItem.Date = (string.IsNullOrWhiteSpace(pageDetail.ItemYear) ? pageDetail.StartYear.ToString() : pageDetail.ItemYear);

                        string[] keywords = pageDetail.Keywords.Split('|');
                        foreach (string keyword in keywords)
                        {
                            if (!string.IsNullOrWhiteSpace(keyword)) exportItem.Subjects.Add(keyword);
                        }

                        exportItem.Contributor.ContributingLibrary = pageDetail.InstitutionName ?? string.Empty;
                        exportItem.Contributor.MemberLibrary = (pageDetail.BhlMemberLibrary == 1);
                    }

                    prevItemID = itemID;
                    prevPageID = pageID;
                }

                // Add the last page/item to the export
                if (exportItem != null)
                {
                    if (exportPage != null) exportItem.Pages.Add(exportPage);
                    export.Items.Add(exportItem);
                }

                // Serialize the final set of records to JSON
                WriteClassifierExport(export);

                // Update the status of the records that were just exported
                UpdatePageDetailStatus(processedPages, PageDetailStatus.Classifying);

                numRecordsExported = pageDetailList.Count();
                LogMessage(string.Format("Done exporting {0} records for Classifier", numRecordsExported));
            }
            catch (Exception ex)
            {
                LogMessage("Error exporting for Classifier", ex);
            }
        }

        /// <summary>
        /// Harvest the data imported from the Classifier application (Macaw).
        /// </summary>
        private void ClassifierImport()
        {
            // Get new files from the Classifier
            GetNewClassifierImports();            

            // Read files to be processed from the input directory
            string[] inputFileNames = Directory.GetFiles(configParms.ClassifierInputFolder);

            LogMessage(string.Format("Beginning import of {0} Classifier files", inputFileNames.Count()));

            foreach (string inputFileName in inputFileNames)
            {
                try
                {
                    // Parse the item and page details from the file
                    List<PageClassifierImport> pageDetails = ParseClassifierFile(inputFileName);

                    // Update the database
                    foreach (PageClassifierImport pageDetail in pageDetails)
                    {
                        SaveClassifierPageDetail(pageDetail);
                    }

                    // Move the file to the "Complete" folder
                    CreateDirectory(configParms.ClassifierCompleteFolder);
                    File.Move(inputFileName, configParms.ClassifierCompleteFolder + Path.GetFileName(inputFileName));

                    LogMessage(string.Format("Done importing \"{0}\"", inputFileName));
                }
                catch (Exception ex)
                {
                    // Move the file to the Error folder
                    CreateDirectory(configParms.ClassifierErrorFolder);
                    string errorFileName = configParms.ClassifierErrorFolder + Path.GetFileName(inputFileName);
                    if (File.Exists(errorFileName)) File.Delete(errorFileName);
                    File.Move(inputFileName, errorFileName);

                    LogMessage(string.Format("Error importing \"{0}\"", inputFileName), ex);
                }
            }

            numFilesHarvested = inputFileNames.Count();
            LogMessage(string.Format("Done importing {0} Classifier files", numClassifierFilesHarvested));
        }

        /// <summary>
        /// Look for new algorithm extracts.  If any are found, move them to the Extract Input folder.
        /// </summary>
        private void GetNewExtracts()
        {
            FtpFileSystemWatcher ftp = new FtpFileSystemWatcher(configParms.FtpIncomingFolder, configParms.ExtractInputFolder,
                1, configParms.FtpUsername, configParms.FtpPassword, true, true);
            ftp.Download();
        }

        /// <summary>
        /// Look for new files from the Classifier.  If any are found, move them to the Classifier Input folder.
        /// </summary>
        private void GetNewClassifierImports()
        {
            string[] fileNames = Directory.GetFiles(configParms.ClassifierIncomingFolder);
            foreach(string fileName in fileNames)
            {
                // Move the file to the Input folder
                CreateDirectory(configParms.ClassifierInputFolder);
                File.Move(fileName, configParms.ClassifierInputFolder + Path.GetFileName(fileName));
            }
        }

        /// <summary>
        /// Serialize the export object to json and write it to a file.
        /// </summary>
        /// <param name="export"></param>
        private void WriteClassifierExport(ClassifierExportJson export)
        {
            if (export.Items.Count > 0)
            {
                string json = JsonConvert.SerializeObject(export);
                string outputFileName = string.Format("{0}classifierout{1}.json", configParms.ClassifierOutputFolder, DateTime.Now.ToString("yyyyMMddHHmmss"));
                File.WriteAllText(outputFileName, json);
            }
        }

        /// <summary>
        /// Check the Extract Complete folder to see if a file with this name has already been processed.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool HasBeenProcessed(string fileName)
        {
            return File.Exists(configParms.ExtractCompleteFolder + Path.GetFileName(fileName));
        }

        /// <summary>
        /// Create the specified directory if it does not already exist
        /// </summary>
        /// <param name="directoryName"></param>
        private void CreateDirectory(string directoryName)
        {
            if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);
        }

        /// <summary>
        /// Read page details from the specified file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private List<PageDetail> GetPageDetails(string fileName)
        {
            // Parse the JSON from the data file
            string line;
            StreamReader fileReader = null;
            List<PageDetail> pageDetailList = new List<PageDetail>();

            using (fileReader = new StreamReader(fileName))
            {
                while ((line = fileReader.ReadLine()) != null)
                {
                    PageDetail pageDetail = ParseExtractJson(line);
                    pageDetailList.Add(pageDetail);
                }
            }

            return pageDetailList;
        }

        /// <summary>
        /// Parse the details for a page from the specified JSON string
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private PageDetail ParseExtractJson(string json)
        {
            PageDetail pageDetail = new PageDetail();

            dynamic metadata = JsonConvert.DeserializeObject<Object>(json);
            JToken idToken = metadata._id;
            pageDetail.Id = idToken.Value<string>("$oid").ToString();
            //pageDetail.ProcessingComplete = (metadata.processing_complete == "true");
            pageDetail.ProcessingComplete = (metadata.abbyy_complete == "true" && metadata.contrast_complete == "true");
            pageDetail.ProcessingError = (metadata.processing_error == "true");
            pageDetail.Barcode = metadata.scan_id;
            pageDetail.Height = metadata.abbyy.height;
            pageDetail.Width = metadata.abbyy.width;
            pageDetail.PercentCoverage = metadata.abbyy.total_coverage_sum;
            if (pageDetail.PercentCoverage == 0 && metadata.abbyy.coverage_sum > 0) // If the percent coverage is missing, see if we can calculate it
            {
                double coverageSum = metadata.abbyy.coverage_sum;
                pageDetail.PercentCoverage = Math.Round((double)metadata.abbyy.coverage_sum / ((double)pageDetail.Height * (double)pageDetail.Width) * 100, 2);
            }
            pageDetail.PixelDepth = 0; // metadata.pixel_depth;
            pageDetail.AbbyHasImage = (metadata.has_illustration.abbyy == "true");
            pageDetail.ContrastHasImage = (metadata.has_illustration.contrast == "true");

            dynamic pictureBlocks = metadata.abbyy.picture_blocks;
            foreach (dynamic pictureBlock in pictureBlocks)
            {
                PageIllustration block = new PageIllustration();
                block.Top = pictureBlock.t;
                block.Bottom = pictureBlock.b;
                block.Left = pictureBlock.l;
                block.Right = pictureBlock.r;
                pageDetail.Illustrations.Add(block);
            }

            // Update the page detail status based on the algorithm results
            if (!(pageDetail.AbbyHasImage || pageDetail.ContrastHasImage)) pageDetail.PageDetailStatus = PageDetailStatus.NoImageFound;

            // Get the Page ID related to this scan_id and page_num
            pageDetail.PageID = PageSelectByBarCodeAndSequence(pageDetail.Barcode, (int)metadata.page_num);

            return pageDetail;
        }

        /// <summary>
        /// Parse the Classifier-added details contained in the specified file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private List<PageClassifierImport> ParseClassifierFile(string fileName)
        {
            List<PageClassifierImport> imports = new List<PageClassifierImport>();

            string json = File.ReadAllText(fileName);

            dynamic metadata = JsonConvert.DeserializeObject<Object>(json);

            dynamic items = metadata.items;
            foreach (dynamic item in items)
            {
                dynamic pages = item.pages;
                foreach (dynamic page in pages)
                {
                    PageClassifierImport import = new PageClassifierImport();
                    import.ItemID = Convert.ToInt32(((string)item.itemid).Replace(configParms.ItemUrlPrefix, ""));
                    import.PageID = Convert.ToInt32(((string)page.pageid).Replace(configParms.PageUrlPrefix, ""));
                    import.BwColor = page.color_or_bw;
                    import.NoImage = (page.no_image_found != null);

                    dynamic pageTypes = page.page_type;
                    foreach (string pageType in pageTypes)
                    {
                        import.PageTypes.Add(pageType);
                    }

                    imports.Add(import);
                }
            }

            return imports;
        }

        /// <summary>
        /// Save the specified page details to the database.  If necessary, add an "Illustration" page type to the page.
        /// </summary>
        /// <param name="pageImage"></param>
        /// <returns></returns>
        private bool SavePageDetail(PageDetail pageImage)
        {
            bool success = true;

            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;

            using (sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BHL"].ToString()))
            {
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();

                try
                {
                    int pageDetailID = PageDetailInsertUpdate(sqlConnection, sqlTransaction, pageImage);

                    foreach (PageIllustration block in pageImage.Illustrations)
                    {
                        PageIllustrationInsert(sqlConnection, sqlTransaction, pageDetailID, block);
                    }

                    // Add the "Illustration" page type to the page record if both algorithms indicate an image.
                    if (pageImage.AbbyHasImage && pageImage.ContrastHasImage)
                    {
                        int paginationStatus = ItemSelectPaginationStatus(sqlConnection, sqlTransaction, pageImage.PageID);

                        // Make sure the item is not "Pagination Complete"
                        if (paginationStatus != (int)PaginationStatus.Complete)
                        {
                            // Make sure this page has not been edited by a BHL staff user
                            List<Tuple<DateTime, int>> pageHistory = PageSelectCurrentUpdateHistory(sqlConnection, sqlTransaction, pageImage.PageID);

                            bool edited = false;
                            foreach(Tuple<DateTime, int> history in pageHistory)
                            {
                                if (history.Item2 != configParms.DefaultUserID && 
                                    history.Item2 != configParms.ExtractionUserID &&
                                    history.Item2 != configParms.ClassifierUserID)
                                {
                                    edited = true;
                                    break;
                                }                                
                            }

                            if (!edited)
                            {
                                // Add the "Illustration" page type
                                PagePageTypeInsert(sqlConnection, sqlTransaction, pageImage.PageID, 
                                    configParms.PageTypeIllustration, configParms.ExtractionUserID);
                            }
                        }
                    }

                    sqlTransaction.Commit();
                }
                catch
                {
                    sqlTransaction.Rollback();
                    throw;
                }
                finally
                {
                    sqlTransaction.Dispose();
                }            
            }

            return success;
        }

        /// <summary>
        /// Save page details from the Classifier to the database
        /// </summary>
        /// <param name="pageImport"></param>
        /// <returns></returns>
        private bool SaveClassifierPageDetail(PageClassifierImport pageImport)
        {
            bool success = true;

            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;

            using (sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BHL"].ToString()))
            {
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();

                try
                {
                    PageDetailUpdateColor(sqlConnection, sqlTransaction, pageImport.PageID, pageImport.BwColor);

                    // Make sure the item is not "Pagination Complete"
                    int paginationStatus = ItemSelectPaginationStatus(sqlConnection, sqlTransaction, pageImport.PageID);
                    if (paginationStatus != (int)PaginationStatus.Complete)
                    {
                        // Remove any image page types not assigned by a BHL staff user
                        PagePageTypeDeleteAutoAssignedImages(sqlConnection, sqlTransaction, pageImport.PageID);

                        // Don't add page types if the NoImage flag is set
                        if (!pageImport.NoImage)
                        {
                            foreach (string pageType in pageImport.PageTypes)
                            {
                                // Add the new page type
                                PagePageTypeInsert(sqlConnection, sqlTransaction, pageImport.PageID, pageType, configParms.ClassifierUserID);
                            }
                        }
                    }

                    PageDetailStatus newStatus = (pageImport.NoImage ? PageDetailStatus.NoImageFound : PageDetailStatus.Classified);
                    PageDetailUpdateStatus(sqlConnection, sqlTransaction, pageImport.PageID, (int)newStatus);

                    sqlTransaction.Commit();
                }
                catch
                {
                    sqlTransaction.Rollback();
                    throw;
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return success;
        }

        /// <summary>
        /// Update the status of the specified PageDetail records
        /// </summary>
        /// <param name="pageDetailList"></param>
        /// <param name="status"></param>
        private void UpdatePageDetailStatus(List<int> pages, PageDetailStatus status)
        {
            foreach (var pageID in pages)
            {
                // Mark the record as exported to the classifier
                PageDetailUpdateStatus(Convert.ToInt32(pageID), (int)status);
            }
        }

        /// <summary>
        /// Reparse the input file, writing each line to a folder based on what was done with it (saved to DB,
        /// or rejected due to missing pages, incomplete processing, error during processing, etc).
        /// </summary>
        /// <remarks>
        /// Ideally we'd read the lines from the file into memory the first time we parse the file, but as files
        /// are expected to be exceedingly large, we reparse the file to preserve memory.
        /// </remarks>
        /// <param name="fileName"></param>
        private void LogResults(string fileName)
        {
            string line;
            StreamReader fileReader = null;
            List<PageDetail> pageDetailList = new List<PageDetail>();

            // Use the input file name and the current time for the log file names
            FileInfo fileInfo = new FileInfo(fileName);
            string logFileName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            string logFileDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string logFileExtention = fileInfo.Extension;

            // Set up the file folders
            CreateDirectory(configParms.ExtractErrorFolder);
            CreateDirectory(configParms.ExtractLoadedFolder);

            using (fileReader = new StreamReader(fileName))
            {
                while ((line = fileReader.ReadLine()) != null)
                {
                    // Get the identifier for the line
                    dynamic metadata = JsonConvert.DeserializeObject<Object>(line);
                    JToken idToken = metadata._id;
                    string id = idToken.Value<string>("$oid").ToString();

                    // Write the line to the appropriate file
                    StreamWriter fileWriter = null;
                    if (processingIncompleteList.Contains(id)) 
                    {
                        using (fileWriter = new StreamWriter(string.Format("{0}{1}_incomplete_{2}{3}", configParms.ExtractErrorFolder, logFileName, logFileDate, logFileExtention), true)) fileWriter.WriteLine(line);
                    }
                    if (processingErrorList.Contains(id))
                    {
                        using (fileWriter = new StreamWriter(string.Format("{0}{1}_error_{2}{3}", configParms.ExtractErrorFolder, logFileName, logFileDate, logFileExtention), true)) fileWriter.WriteLine(line);
                    }
                    if (notInDBList.Contains(id))
                    {
                        using (fileWriter = new StreamWriter(string.Format("{0}{1}_notindb_{2}{3}", configParms.ExtractErrorFolder, logFileName, logFileDate, logFileExtention), true)) fileWriter.WriteLine(line);
                    }
                    if (invalidCountList.Contains(id))
                    {
                        using (fileWriter = new StreamWriter(string.Format("{0}{1}_invalidcount_{2}{3}", configParms.ExtractErrorFolder, logFileName, logFileDate, logFileExtention), true)) fileWriter.WriteLine(line);
                    }
                    if (errorSaveList.Contains(id))
                    {
                        using (fileWriter = new StreamWriter(string.Format("{0}{1}_saveerror_{2}{3}", configParms.ExtractErrorFolder, logFileName, logFileDate, logFileExtention), true)) fileWriter.WriteLine(line);
                    }
                    if (loadedPagesList.Contains(id))
                    {
                        using (fileWriter = new StreamWriter(string.Format("{0}{1}_loaded_{2}{3}", configParms.ExtractLoadedFolder, logFileName, logFileDate, logFileExtention), true)) fileWriter.WriteLine(line);
                    }
                }
            }
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
                    Console.WriteLine("Invalid command line format.  Format is PageDetailHarvest.exe [/MODE:harvest_extract|classifier_export]");
                    return false;
                }

                if (String.Compare(split[0], "/MODE", true) == 0) configParms.Mode = split[1];
            }

            return true;
        }

        #region DAL

        private int PageDetailInsertUpdate(SqlConnection sqlConnection, SqlTransaction sqlTransaction, PageDetail pageImage)
        {
            SqlCommand sqlCommand = null;
            int pageDetailID = 0;

            string sql = string.Format("exec dbo.PageDetailInsertUpdate @PageID={0}, @PageDetailStatusID={1}, @Height={2}, @Width={3}, @PixelDepth={4}, @AbbyyHasImage={5}, @ContrastHasImage={6}, @PercentCoverage={7}", 
                pageImage.PageID, (int)pageImage.PageDetailStatus, pageImage.Height, pageImage.Width, pageImage.PixelDepth,
                pageImage.AbbyHasImage ? 1: 0, pageImage.ContrastHasImage ? 1: 0, pageImage.PercentCoverage);

            using (sqlCommand = new SqlCommand(sql, sqlConnection, sqlTransaction))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read()) pageDetailID = reader.GetInt32(reader.GetOrdinal("PageDetailID"));
                }
            }

            return pageDetailID;
        }

        private int PageIllustrationInsert(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int pageDetailID, PageIllustration block)
        {
            SqlCommand sqlCommand = null;
            int pageIllustrationID = 0;

            string sql = string.Format("exec dbo.PageIllustrationInsert @PageDetailID={0}, @Top={1}, @Bottom={2}, @Left={3}, @Right={4}",
                pageDetailID, block.Top, block.Bottom, block.Left, block.Right);

            using (sqlCommand = new SqlCommand(sql, sqlConnection, sqlTransaction))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read()) pageIllustrationID = reader.GetInt32(reader.GetOrdinal("PageIllustrationID"));
                }
            }

            return pageIllustrationID;
        }

        /// <summary>
        /// Get the pagination status of the item associated with the specified PageID.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        private int ItemSelectPaginationStatus(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlCommand sqlCommand = null;
            int paginationStatusID = 0;

            string sql = string.Format("exec dbo.ItemSelectByPageID @PageID={0}", pageID.ToString());

            using (sqlCommand = new SqlCommand(sql, sqlConnection, sqlTransaction))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read()) paginationStatusID = reader.GetInt32(reader.GetOrdinal("PaginationStatusID"));
                }
            }

            return paginationStatusID;
        }

        /// <summary>
        /// Get a list of the dates and users for the most recent updates to the Page record and all 
        /// related records (Page_PageType and IndicatedPage).
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        private List<Tuple<DateTime, int>> PageSelectCurrentUpdateHistory(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, int pageID)
        {
            SqlCommand sqlCommand = null;
            List<Tuple<DateTime, int>> history = new List<Tuple<DateTime, int>>();

            string sql = string.Format("exec dbo.PageSelectCurrentUpdateHistory @PageID={0}", pageID.ToString());

            using (sqlCommand = new SqlCommand(sql, sqlConnection, sqlTransaction))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime lastModifiedDate = reader.GetDateTime(reader.GetOrdinal("LastModifiedDate"));
                        int lastModifiedUserID = reader.GetInt32(reader.GetOrdinal("LastModifiedUserID"));
                        history.Add(new Tuple<DateTime, int>(lastModifiedDate, lastModifiedUserID));
                    }
                }
            }

            return history;
        }

        private int PageSelectByBarCodeAndSequence(string barcode, int sequence)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            int pageID = 0;

            using (sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BHL"].ToString()))
            {
                sqlConnection.Open();
                string sql = string.Format("exec dbo.PageSelectByBarCodeAndSequence '{0}', {1}", barcode, sequence.ToString());

                using (sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.Read()) pageID = reader.GetInt32(reader.GetOrdinal("PageID"));
                }
            }

            return pageID;
        }

        private Dictionary<string, int> ItemSelectPageCounts()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            Dictionary<string, int> itemCounts = new Dictionary<string, int>();

            using (sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BHL"].ToString()))
            {
                sqlConnection.Open();
                string sql = string.Format("select i.Barcode, count(*) as NumPages from page p inner join item i on p.itemid = i.itemid where i.itemstatusid = 40 and p.active = 1 group by i.barcode");

                using (sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        itemCounts.Add(reader.GetString(reader.GetOrdinal("Barcode")), reader.GetInt32(reader.GetOrdinal("NumPages")));
                    }
                }
            }

            return itemCounts;
        }

        /// <summary>
        /// Select the page details for export to the Classifier tool
        /// </summary>
        /// <returns></returns>
        private List<PageClassifierExport> PageDetailSelectForClassifierExport()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            List<PageClassifierExport> pageDetail = new List<PageClassifierExport>();

            using (sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BHL"].ToString()))
            {
                sqlConnection.Open();
                string sql = string.Format("exec dbo.PageDetailSelectForClassifierExport");

                using (sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.CommandTimeout = 1200;   // 20 minutes
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        PageClassifierExport row = new PageClassifierExport();

                        row.PageDetailID = reader.GetInt32(reader.GetOrdinal("PageDetailID"));
                        row.ItemID = reader.GetInt32(reader.GetOrdinal("ItemID"));
                        row.BarCode = reader.GetString(reader.GetOrdinal("BarCode"));
                        row.PageID = reader.GetInt32(reader.GetOrdinal("PageID"));
                        row.SequenceOrder = reader.GetInt32(reader.GetOrdinal("SequenceOrder"));
                        row.AbbyyHasImage = reader.GetInt16(reader.GetOrdinal("AbbyyHasImage"));
                        row.ContrastHasImage = reader.GetInt16(reader.GetOrdinal("ContrastHasImage"));
                        row.PercentCoverage = (double)reader.GetDecimal(reader.GetOrdinal("PercentCoverage"));
                        row.Height = reader.GetInt32(reader.GetOrdinal("Height"));
                        row.Width = reader.GetInt32(reader.GetOrdinal("Width"));
                        row.PixelDepth = reader.GetInt32(reader.GetOrdinal("PixelDepth"));
                        if (!reader.IsDBNull(reader.GetOrdinal("Top")))
                        {
                            row.Top = reader.GetInt32(reader.GetOrdinal("Top"));
                            row.Bottom = reader.GetInt32(reader.GetOrdinal("Bottom"));
                            row.Left = reader.GetInt32(reader.GetOrdinal("Left"));
                            row.Right = reader.GetInt32(reader.GetOrdinal("Right"));
                        }
                        row.Authors = reader.GetString(reader.GetOrdinal("Authors"));
                        row.ShortTitle = reader.GetString(reader.GetOrdinal("ShortTitle"));
                        row.PublicationDetails = reader.GetString(reader.GetOrdinal("PublicationDetails"));
                        row.CopyrightStatus = reader.GetString(reader.GetOrdinal("CopyrightStatus"));

                        if (!reader.IsDBNull(reader.GetOrdinal("PageYear"))) row.PageYear = reader.GetString(reader.GetOrdinal("PageYear"));
                        if (!reader.IsDBNull(reader.GetOrdinal("ItemYear"))) row.ItemYear = reader.GetString(reader.GetOrdinal("ItemYear"));
                        if (!reader.IsDBNull(reader.GetOrdinal("Volume"))) row.Volume = reader.GetString(reader.GetOrdinal("Volume"));
                        row.StartYear = reader.IsDBNull(reader.GetOrdinal("StartYear")) ? row.StartYear : reader.GetInt16(reader.GetOrdinal("StartYear"));
                        row.Keywords = reader.GetString(reader.GetOrdinal("Keywords"));
                        row.InstitutionName = reader.GetString(reader.GetOrdinal("InstitutionName"));
                        row.BhlMemberLibrary = reader.GetBoolean(reader.GetOrdinal("BHLMemberLibrary")) ? 1 : 0;

                        pageDetail.Add(row);
                    }
                }
            }

            return pageDetail;
        }

        private void PageDetailUpdateColor(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int pageID, string color)
        {
            SqlCommand sqlCommand = null;

            string sql = string.Format("exec dbo.PageDetailUpdateColor @PageID={0}, @Color='{1}'", pageID, color);
            using (sqlCommand = new SqlCommand(sql, sqlConnection, sqlTransaction))
            {
                sqlCommand.ExecuteNonQuery();
            }
        }

        private void PageDetailUpdateStatus(int pageID, int pageDetailStatusID)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            using (sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BHL"].ToString()))
            {
                sqlConnection.Open();
                string sql = string.Format("exec dbo.PageDetailUpdateStatus @PageID={0}, @PageDetailStatusID={1}",
                    pageID, pageDetailStatusID);

                using (sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        private void PageDetailUpdateStatus(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int pageID, int pageDetailStatusID)
        {
            SqlCommand sqlCommand = null;

            string sql = string.Format("exec dbo.PageDetailUpdateStatus @PageID={0}, @PageDetailStatusID={1}",
                pageID, pageDetailStatusID);

            using (sqlCommand = new SqlCommand(sql, sqlConnection, sqlTransaction))
            {
                sqlCommand.ExecuteNonQuery();
            }
        }

        private void PagePageTypeInsert(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID, 
            string pageTypeName, int userID)
        {
            SqlCommand sqlCommand = null;

            string sql = string.Format("exec dbo.Page_PageTypeInsert @PageID={0}, @PageTypeName='{1}', @UserID={2}", 
                pageID, pageTypeName, userID);
            using (sqlCommand = new SqlCommand(sql, sqlConnection, sqlTransaction))
            {
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Removed all image page types assigned by the Art Of Life processes or by the BHL data ingest processes.
        /// Page types assigned via the Paginator tool are not affected.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="pageID"></param>
        private void PagePageTypeDeleteAutoAssignedImages(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, int pageID)
        {
            SqlCommand sqlCommand = null;

            string sql = string.Format("exec dbo.Page_PageTypeDeleteAutoAssignedImages @PageID={0}", pageID);
            using (sqlCommand = new SqlCommand(sql, sqlConnection, sqlTransaction))
            {
                sqlCommand.ExecuteNonQuery();
            }
        }

        enum PaginationStatus
        {
            New = 5,
            Incomplete = 10,
            InProgress = 20,
            Complete = 30
        }

        #endregion DAL

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
                if (numFilesHarvested > 0 || numRecordsExported > 0 || numClassifierFilesHarvested > 0 || errorMessages.Count > 0)
                {
                    String subject = String.Empty;
                    String thisComputer = Environment.MachineName;
                    if (this.errorMessages.Count == 0)
                    {
                        subject = "PageDetailHarvest: Harvesting on " + thisComputer + " completed successfully.";
                    }
                    else
                    {
                        subject = "PageDetailHarvest: Harvesting on " + thisComputer + " completed with errors.";
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

            sb.Append(string.Format("PageDetailHarvest: Harvesting on {0} complete.{1}{2}", thisComputer, endOfLine, endOfLine));
            if (numFilesHarvested > 0)
            {
                sb.Append(string.Format("{0} files were harvested{1}", numFilesHarvested, endOfLine));
            }
            if (numRecordsExported > 0)
            {
                sb.Append(string.Format("{0} records were exported{1}", numRecordsExported, endOfLine));
            }
            if (numClassifierFilesHarvested > 0)
            {
                sb.Append(string.Format("{0} files were imported from the Classifier{1}", numClassifierFilesHarvested, endOfLine));
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

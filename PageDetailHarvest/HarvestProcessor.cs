using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageDetailHarvest
{
    internal class HarvestProcessor
    {
        private ConfigParms configParms = new ConfigParms();

        private List<string> processingIncompleteList = new List<string>();
        private List<string> processingErrorList = new List<string>();
        private List<string> notInDBList = new List<string>();
        private List<string> invalidCountList = new List<string>();
        private List<string> errorSaveList = new List<string>();
        private List<string> loadedPagesList = new List<string>();

        public void Process()
        {
            configParms.LoadAppConfig();

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
                default:
                    Console.WriteLine("Unknown Mode");
                    break;
            }

            Console.WriteLine("Done");
        }

        /// <summary>
        /// Harvest the data extracted by the IMA algorithms
        /// </summary>
        private void HarvestExtract()
        {
            // Read files to be processed from the input directory
            string[] inputFileNames = Directory.GetFiles(configParms.ExtractInputFolder);

            foreach (string inputFileName in inputFileNames)
            {
                // Read the page details from the data file
                List<PageDetail> pageDetailList = GetPageDetails(inputFileName);

                // Get the items with incomplete pages
                List<string> incompleteItems = (from i in pageDetailList where i.ProcessingComplete == false
                                                group i by i.Barcode into g select g.Key.ToString()).ToList();

                // Get the items with pages in error
                List<string> errorItems = (from i in pageDetailList where i.ProcessingError == true
                                          group i by i.Barcode into g select g.Key.ToString()).ToList();

                // Get the items with pages not found in the database
                var missingItems = (from i in pageDetailList where i.PageID == 0
                                   group i by i.Barcode into g select g.Key.ToString()).ToList();

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
                
            }
        }

        /// <summary>
        /// Export to JSON the page details for import into the Classifier tool.
        /// </summary>
        private void ClassifierExport()
        {
            List<PageClassifierExport> pageDetailList = PageDetailSelectForClassifierExport();

            // Accumulate the list to be serialized as JSON
            ClassifierExportJson export = new ClassifierExportJson();
            ClassifierExportItem exportItem = null;
            ClassifierExportPage exportPage = null;
            int prevItemID = 0;
            int prevPageID = 0;
            foreach (PageClassifierExport pageDetail in pageDetailList)
            {
                int itemID = pageDetail.ItemID;
                int pageID = pageDetail.PageID;

                if (pageID != prevPageID)
                {
                    if (exportPage != null) exportItem.Pages.Add(exportPage);
                    exportPage = new ClassifierExportPage();

                    exportPage.PageUrl = string.Format(configParms.PageUrlFormat, pageID.ToString());
                    exportPage.SequenceOrder = pageDetail.SequenceOrder;
                    exportPage.AbbyyHasIllustration = (pageDetail.AbbyyHasImage == 1);
                    exportPage.ContrastHasIllustration = (pageDetail.ContrastHasImage == 1);
                    exportPage.PercentCoverage = pageDetail.PercentCoverage;
                    exportPage.Height = pageDetail.Height;
                    exportPage.Width = pageDetail.Width;
                    //exportPage.PixelDepth = pageDetail.PixelDepth;
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
                    exportItem = new ClassifierExportItem();

                    exportItem.ItemUrl = string.Format(configParms.ItemUrlFormat, itemID.ToString());
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


            // Serialize everything to JSON
            CreateDirectory(configParms.ClassifierOutputFolder);
            string json = JsonConvert.SerializeObject(export);
            string outputFileName = string.Format("{0}classifierout{1}.json", configParms.ClassifierOutputFolder, DateTime.Now.ToString("yyyyMMddHHmmss"));
            File.WriteAllText(outputFileName, json);
            
            // Update the status of the record that were just exported
            UpdatePageDetailStatus(pageDetailList, PageDetailStatus.Classifying);
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
                    PageDetail pageDetail = ParseJson(line);
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
        private PageDetail ParseJson(string json)
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
        /// Save the specified page details to the database
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

                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    // Log errors

                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // Log errors
                    }
                    success = false;
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
        private void UpdatePageDetailStatus(List<PageClassifierExport> pageDetailList, PageDetailStatus status)
        {
            // Get the unique PageDetail identifiers
            var pageDetailIDs = from d in pageDetailList
                                group d by d.PageDetailID into g
                                select new { PageDetailID = g.Key };

            foreach (var pageDetailID in pageDetailIDs)
            {
                // Mark the record as exported to the classifier
                PageDetailUpdateStatus(Convert.ToInt32(pageDetailID.PageDetailID), (int)status);
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
                        row.StartYear = reader.GetInt16(reader.GetOrdinal("StartYear"));
                        row.Keywords = reader.GetString(reader.GetOrdinal("Keywords"));
                        row.InstitutionName = reader.GetString(reader.GetOrdinal("InstitutionName"));
                        row.BhlMemberLibrary = reader.GetBoolean(reader.GetOrdinal("BHLMemberLibrary")) ? 1 : 0;

                        pageDetail.Add(row);
                    }
                }
            }

            return pageDetail;
        }

        private void PageDetailUpdateStatus(int pageDetailID, int pageDetailStatusID)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            using (sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BHL"].ToString()))
            {
                sqlConnection.Open();
                string sql = string.Format("exec dbo.PageDetailUpdateStatus @PageDetailID={0}, @PageDetailStatusID={1}",
                    pageDetailID, pageDetailStatusID);

                using (sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        #endregion DAL

    }
}

using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Xml;

namespace IAHarvest
{
    public class IAHarvestProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new();
        private List<string> retrievedIds = new();
        private List<string> harvestedXml = new();
        private List<string> publishedItems = new();
        private List<string> errorMessages = new();

        private const string MODE_SETS = "SETS";
        private const string MODE_ITEM = "ITEM";

        // Dublin core attributes
        private const string DC_ATTRIB_CONTRIBUTOR = "contributor";
        private const string DC_ATTRIB_COVERAGE = "coverage";
        private const string DC_ATTRIB_CREATOR = "creator";
        private const string DC_ATTRIB_DATE = "date";
        private const string DC_ATTRIB_DESCRIPTION = "description";
        private const string DC_ATTRIB_FORMAT = "format";
        private const string DC_ATTRIB_IDENTIFIER = "identifier";
        private const string DC_ATTRIB_LANGUAGE = "language";
        private const string DC_ATTRIB_PUBLISHER = "publisher";
        private const string DC_ATTRIB_RELATION = "relation";
        private const string DC_ATTRIB_RIGHTS = "rights";
        private const string DC_ATTRIB_SOURCE = "source";
        private const string DC_ATTRIB_SUBJECT = "subject";
        private const string DC_ATTRIB_TITLE = "title";
        private const string DC_ATTRIB_TYPE = "type";

        // Sources of Dublin Core attribute data
        private const string DC_SOURCE_DC = "DC";
        private const string DC_SOURCE_META = "META";

        // Item Status identifiers
        private const int ITEMSTATUS_NEW = 10;
        private const int ITEMSTATUS_PENDINGAPPROVAL = 20;
        private const int ITEMSTATUS_APPROVED = 30;
        private const int ITEMSTATUS_COMPLETE = 40;
        private const int ITEMSTATUS_MARCMISSINGNEW = 80;
        private const int ITEMSTATUS_MARCMISSINGAPPROVED = 81;
        private const int ITEMSTATUS_MARCMISSINGONHOLD = 82;
        private const int ITEMSTATUS_XMLERROR = 90;
        private const int ITEMSTATUS_INAPPROPRIATE = 97;
        private const int ITEMSTATUS_IAERROR = 98;
        private const int ITEMSTATUS_ERROR = 99;

        // Create an IAHarvestProvider for use in this class
        BHLImportProvider provider = new();

        public void Process()
        {
            // Load the app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            if (configParms.Download)
            {
                switch (configParms.Mode)
                {
                    case MODE_SETS:
                        this.GetSetItems();
                        this.GetExtraSetItems();
                        break;
                    case MODE_ITEM:
                        this.GetItem(configParms.Item, null, null);
                        break;
                }

                this.HarvestXMLInformation();
            }

            if (configParms.Upload)
            {
                this.PublishToImportTables();
            }

            // Report the results of item/page processing
            this.ProcessResults();

            LogMessage("IAHarvest Processing Complete");
        }

        #region Get basic Item and Set information

        /// <summary>
        /// Get all of the items for the Internet Archive sets that are marked as "DownloadAll"
        /// in the Set table.
        /// </summary>
        private void GetSetItems()
        {
            try
            {
                LogMessage("Processing SETS");

                List<IASet> sets = provider.IASetSelectForDownload();
                foreach (IASet set in sets)
                {
                    DateTime? startDate = set.LastFullDownloadDate;
                    bool fullDownload = true;
                    if (startDate != null)
                    {
                        // If the last full download was less than 90 days ago, then just do a 
                        // partial download.  Otherwise, do a full download (startDate = null).
                        if ((DateTime.Now - (DateTime)startDate).Days < 90)
                        {
                            startDate = set.LastDownloadDate;

                            // 10/12/2015 - Always reset the fullDownloadDate.  Full downloads have 
                            // become too large (the IA query times out), and of questionable value,
                            // so they are no longer being used.
                            //fullDownload = false;
                        }
                        else
                        {
                            startDate = null;
                        }
                    }
                    this.GetItem(set.SetSpecification, set.SetID, startDate);

                    // If we got anything, update the date that the set information was downloaded
                    if (retrievedIds.Count > 0) provider.IASetUpdateLastDownloadDate(set.SetID, fullDownload);
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception processing sets.", ex);
                errorMessages.Add("Exception processing sets:  " + ex.Message);
            }
        }

        /// <summary>
        /// Get the items from other sets that have been identified as BHL-worthy
        /// </summary>
        private void GetExtraSetItems()
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
        private void GetItem(string itemIdentifier, int? setID, DateTime? startDate)
        {
            try
            {
                LogMessage("Processing item: " + itemIdentifier);

                String url = String.Empty;
                if (configParms.Mode == MODE_SETS)
                {
                    // Get the specified start date and the current (end) date
                    string startDateString = String.Empty;
                    string endDateString = DateTime.Now.ToString("yyyy-MM-dd");
                    startDateString = (startDate == null) ? "1900-01-01" : ((DateTime)startDate).ToString("yyyy-MM-dd");

                    // Now check to see if dates were specified on the command line... if so, use them instead
                    if (configParms.SearchStartDate != null) startDateString = ((DateTime)configParms.SearchStartDate).ToString("yyyy-MM-dd");
                    if (configParms.SearchEndDate != null) endDateString = ((DateTime)configParms.SearchEndDate).ToString("yyyy-MM-dd");

                    url = String.Format(configParms.SearchListIdentifiersUrl, itemIdentifier, startDateString, endDateString);
                }
                else // Mode = MODE_ITEM
                {
                    url = String.Format(configParms.SearchListIdentifiersItemUrl, itemIdentifier);
                }

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
                log.Error("Exception processing item: " + itemIdentifier, ex);
                errorMessages.Add("Exception processing item: " + itemIdentifier + "  " + ex.Message);
            }
        }

        #endregion Get basic Item and Set information

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
                LogMessage("Harvesting XML information");

                // Download the XML files for each item and parse the data into the database
                List<IAItem> items = provider.IAItemSelectForXMLDownload(configParms.Mode == MODE_ITEM ? configParms.Item : "");

                foreach (IAItem item in items)
                {
                    try
                    {
                        this.HarvestFileList(item);
                        this.DownloadFiles(item);
                        this.DownloadScandata(item);

                        // If the item information has not yet moved to production, go ahead and 
                        // harvest the data from the files
                        if (item.ItemStatusID != ITEMSTATUS_COMPLETE)
                        {
                            List<IAFile> fileList = provider.IAFileSelectByItem(item.ItemID);
                            this.HarvestDCMetadata(GetFile(fileList, configParms.DCMetadataExtension), item.ItemID, item.IAIdentifier, item.LocalFileFolder, item.LastXMLDataHarvestDate);
                            this.HarvestMetadataSourceData(GetFile(fileList, configParms.MetadataSourceExtension),  item.ItemID, item.IAIdentifier, item.LocalFileFolder, item.LastXMLDataHarvestDate);
                            this.HarvestMetadata(GetFile(fileList, configParms.MetadataExtension), item.ItemID, item.IAIdentifier, item.LocalFileFolder, item.LastXMLDataHarvestDate);
                            this.HarvestDJVUData(GetFile(fileList, configParms.DjvuExtension), item.ItemID, item.IAIdentifier, item.LocalFileFolder, item.LastXMLDataHarvestDate);
                            this.HarvestScandata(GetFile(fileList, configParms.ScandataExtension), item.ItemID, item.IAIdentifier, item.LocalFileFolder, item.LastXMLDataHarvestDate);
                            this.HarvestMarcData(GetFile(fileList, configParms.MarcExtension), item.ItemID, item.IAIdentifier, item.LocalFileFolder, item.LastXMLDataHarvestDate, (item.NoMARCOk == 1));
                        }
                        provider.IAItemUpdateLastXMLDataHarvestDate(item.ItemID);
                        provider.IAItemUpdateItemStatusIDAfterDataHarvest(item.ItemID,
                            configParms.AllowUnapprovedPublish,
                            configParms.MinimumDaysBeforeAllowUnapprovedPublish);
                        this.harvestedXml.Add(item.IAIdentifier);
                    }
                    catch (MARCNotFoundException)
                    {
                        log.Error("No MARC file found for " + item.IAIdentifier);
                        errorMessages.Add("No MARC file found for " + item.IAIdentifier);
                        if (item.ItemStatusID != ITEMSTATUS_COMPLETE && 
                            item.ItemStatusID != ITEMSTATUS_MARCMISSINGAPPROVED &&
                            item.ItemStatusID != ITEMSTATUS_MARCMISSINGONHOLD)
                        {
                            provider.IAItemUpdateItemStatusSetError(item.ItemID, ITEMSTATUS_MARCMISSINGNEW, "HarvestXMLInformation", "No MARC file found");
                        }
                        // don't rethrow; we want to continue processing
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception harvesting XML information for " + item.IAIdentifier, ex);
                        errorMessages.Add("Exception harvesting XML information for " + item.IAIdentifier + "  " + ex.Message);
                        if (item.ItemStatusID != ITEMSTATUS_COMPLETE)
                        {
                            // 2011-07-18 MWL -> Instead of setting item status to "Error", just leave it as-is.  That way
                            // it will automatically get reprocessed the next time the harvest process runs.
                            //provider.IAItemUpdateItemStatusSetError(item.ItemID, ITEMSTATUS_XMLERROR, "HarvestXMLInformation",
                            //    ex.Message.Substring(0, ((ex.Message.Length > 4000) ? 4000 : ex.Message.Length)));
                            provider.IAItemUpdateItemStatusSetError(item.ItemID, item.ItemStatusID, "HarvestXMLInformation",
                                ex.Message[..((ex.Message.Length > 4000) ? 4000 : ex.Message.Length)]);
                        }
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

        private void HarvestFileList(IAItem item)
        {
            LogMessage("Harvesting the file list for " + item.IAIdentifier);

            // Download the _FILES.XML file for this identifier
            //
            // NOTE: If the _FILES.XML file is not found, it is likely that the item has not
            // yet been scanned by Internet Archive.  A way to confirm this is to look for 
            // the _META.XML file for the item and examine the "status" item in the <curation>
            // element.  If that item does not have a value of "approved", then we shouldn't
            // expect to be able to harvest the item's information.
            //
            XmlDocument xml = provider.GetIAXmlData(String.Format(configParms.FileDownloadUrl, item.IAIdentifier, item.IAIdentifier + configParms.FilesExtension));

            // Save the file to a local folder
            if (item.ItemStatusID == ITEMSTATUS_COMPLETE)    // item files already moved to production location
            {
                xml.Save(item.LocalFileFolder + item.MARCBibID + "\\" + item.IAIdentifier + configParms.FilesExtension);
            }
            else
            {
                Directory.CreateDirectory(item.LocalFileFolder + item.IAIdentifier);
                xml.Save(item.LocalFileFolder + item.IAIdentifier + "\\" + item.IAIdentifier + configParms.FilesExtension);
            }

            // Parse the file information from the XML
            XmlNodeList files = xml.SelectNodes("//file");
            String[] externalFileNames = new String[files.Count];
            int counter = 0;
            foreach (XmlNode file in files)
            {
                String externalFileName = (file.Attributes["name"] == null) ? String.Empty : file.Attributes["name"].Value;
                String source = (file.Attributes["source"] == null) ? String.Empty : file.Attributes["source"].Value;
                XmlNode formatNode = file.SelectSingleNode("format");
                String format = (formatNode == null) ? String.Empty : formatNode.InnerText;
                XmlNode originalNode = file.SelectSingleNode("original");
                String original = (originalNode == null) ? String.Empty : originalNode.InnerText;

                // 1/7/2008 - discovered that <FORMAT> element might be missing from XML files
                // that we need to harvest... so if externalFileName ends in .XML and formatNode
                // is null, substitute "Metadata" for the format value
                if (formatNode == null && externalFileName.ToLower().EndsWith(".xml")) format = "Metadata";

                // Save the file information (FileName, Source, Format, Original)
                provider.SaveIAFile(item.ItemID, externalFileName, source, format, original);
                externalFileNames[counter++] = externalFileName;
            }

            // Delete local files and database entries that no longer exist in FILES.XML
            List<IAFile> itemFiles = provider.IAFileSelectByItem(item.ItemID);
            foreach (IAFile itemFile in itemFiles)
            {
                // Ignore the scandata.xml file (which will not appear in FILES.XML)
                if (!IsInIAList(itemFile.RemoteFileName, externalFileNames) &&
                    (itemFile.RemoteFileName != configParms.ScandataFile))
                {
                    provider.IAFileDeleteAuto(itemFile.FileID);

                    if (item.ItemStatusID != ITEMSTATUS_COMPLETE)    // only delete physical file if not yet in "production" status
                    {
                        if (itemFile.LocalFileName != String.Empty) System.IO.File.Delete(item.LocalFileFolder + item.IAIdentifier + "\\" + itemFile.LocalFileName);
                    }
                }
            }
        }

        /// <summary>
        /// Determine if the specified filename is in the specified list of external file names
        /// </summary>
        /// <param name="fileName">Filename for which to search</param>
        /// <param name="externalFileNames">List in which to search</param>
        /// <returns>True if found, false otherwise</returns>
        private static bool IsInIAList(string fileName, String[] externalFileNames)
        {
            bool inList = false;
            foreach (string externalFileName in externalFileNames)
            {
                if (fileName == externalFileName)
                {
                    inList = true;
                    break;
                }
            }

            return inList;
        }

        /// <summary>
        /// Select the file with the specified extension from the supplied list
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        private static IAFile GetFile(List<IAFile> fileList, string fileExtension)
        {
            IAFile file = null;

            foreach (IAFile iaFile in fileList)
            {
                if (iaFile.LocalFileName.EndsWith(fileExtension))
                {
                    file = iaFile;
                    break;
                }
            }

            return file;
        }

        private void DownloadFiles(IAItem item)
        {
            LogMessage("Downloading files for " + item.IAIdentifier);

            List<IAFile> files = provider.IAFileSelectForDownload(item.ItemID);
            foreach (IAFile file in files)
            {
                DateTime? remoteFileLastModifiedDate = DateTime.Parse("1/1/1980");

                // Download and save the file
                BinaryReader stream = provider.GetIARawData(String.Format(configParms.FileDownloadUrl, item.IAIdentifier, file.RemoteFileName), 
                    file.RemoteFileLastModifiedDate, out remoteFileLastModifiedDate);
                if (stream != null)
                {
                    // Save the file to a local folder
                    string fileName = string.Empty;
                    if (item.ItemStatusID == ITEMSTATUS_COMPLETE)    // item files already moved to production location
                    {
                        fileName = item.LocalFileFolder + item.MARCBibID + "\\" + file.RemoteFileName;
                    }
                    else
                    {
                        fileName = item.LocalFileFolder + item.IAIdentifier + "\\" + file.RemoteFileName;
                    }

                    using (FileStream fileStream = File.Open(fileName, FileMode.Create))
                    {
                        using (BinaryWriter writer = new(fileStream, Encoding.UTF8))
                        {
                            byte[] buffer = new byte[2048];
                            int count = stream.Read(buffer, 0, buffer.Length);
                            while (count != 0)
                            {
                                writer.Write(buffer, 0, count);
                                writer.Flush();
                                count = stream.Read(buffer, 0, buffer.Length);
                            }
                            writer.Dispose();
                        }
                        stream.Dispose();
                    }
                }

                // Get the remote file last modified date
                provider.SaveIAFileWithDownloadInfo(file.FileID, file.RemoteFileName, (DateTime)remoteFileLastModifiedDate);
            }
        }

        private void DownloadScandata(IAItem item)
        {
            LogMessage("Downloading scandata for " + item.IAIdentifier);

            // If the file does not exist locally (only get this it it doesn't already exist)
            string localFileName = item.LocalFileFolder + item.IAIdentifier + "\\" + item.IAIdentifier + configParms.ScandataExtension;
            if (!File.Exists(localFileName))
            {
                this.GetPhysicalFileLocation(item.IAIdentifier, out string host, out string dir);

                try
                {
                    // Initiate a download of the scandata.zip file... if it fails, then we know
                    // the file isn't there.  If it succeeds, abort the download and initiate a
                    // request for the scandata.xml file within the ZIP.
                    BinaryReader zipstream = provider.GetIARawData(String.Format(configParms.FileDownloadUrl, item.IAIdentifier, "scandata.zip"));
                    if (zipstream != null)
                    {
                        // Found the zip file
                        zipstream.Close();

                        // Download and save the xml file
                        BinaryReader stream = provider.GetIARawData(String.Format(configParms.ScandataDownloadUrl, host, dir));
                        if (stream != null)
                        {
                            // Save the file to a local folder
                            string fileName = string.Empty;
                            if (item.ItemStatusID == ITEMSTATUS_COMPLETE)    // item files already moved to production location
                            {
                                fileName = item.LocalFileFolder + item.MARCBibID + "\\" + item.IAIdentifier + configParms.ScandataExtension;
                            }
                            else
                            {
                                fileName = item.LocalFileFolder + item.IAIdentifier + "\\" + item.IAIdentifier + configParms.ScandataExtension;
                            }

                            using (FileStream fileStream = File.Open(fileName, FileMode.Create))
                            {
                                using (BinaryWriter writer = new(fileStream, Encoding.UTF8))
                                {
                                    byte[] buffer = new byte[2048];
                                    int count = stream.Read(buffer, 0, buffer.Length);
                                    while (count != 0)
                                    {
                                        writer.Write(buffer, 0, count);
                                        writer.Flush();
                                        count = stream.Read(buffer, 0, buffer.Length);
                                    }
                                    writer.Dispose();
                                }
                                stream.Dispose();
                            }
                        }

                        // Save the remote file information (use the current date/time as the last modified date)
                        IAFile newFile = provider.SaveIAFile(item.ItemID, configParms.ScandataFile, "", "Metadata from Scandata ZIP", "");
                        provider.SaveIAFileWithDownloadInfo(newFile.FileID, item.IAIdentifier + configParms.ScandataExtension, DateTime.Now);
                    }
                }
                catch (System.Net.WebException)
                {
                    // Do nothing... apparently the file we're looking for doesn't exist
                }
            }
        }

        private void HarvestDCMetadata(IAFile file, int itemID, string iaIdentifier, string localFileFolder, DateTime? lastXmlDataHarvestDate)
        {
            LogMessage("Harvesting Dublin Core metadata for " + iaIdentifier);

            // If the file exists
            if (file != null)
            {
                // If the file has changed since we last harvested
                if (DateTime.Compare((DateTime)file.RemoteFileLastModifiedDate, (DateTime)(lastXmlDataHarvestDate ?? DateTime.Parse("1/1/1980"))) > 0)
                {
                    // We are simply replacing any previously parsed elements, so delete anything
                    // that already exists
                    provider.IADCMetadataDeleteForItemAndSource(itemID, DC_SOURCE_DC);

                    // Open the file and parse the data within it
                    String localFileName = localFileFolder + iaIdentifier + "\\" + file.LocalFileName;
                    XmlDocument xml = new();
                    xml.Load(localFileName);

                    this.ReadAndSaveDCElements(itemID, xml, "dc/*", DC_SOURCE_DC);
                }
            }
            else
            {
                // No local file, so remove anything in the database
                provider.IADCMetadataDeleteForItemAndSource(itemID, DC_SOURCE_DC);
            }
        }

        private void HarvestMetadata(IAFile file, int itemID, string iaIdentifier, string localFileFolder, DateTime? lastXmlDataHarvestDate)
        {
            LogMessage("Harvesting metadata for " + iaIdentifier);

            // If the file exists
            if (file != null)
            {
                // If the file has changed since we last harvested
                if (DateTime.Compare((DateTime)file.RemoteFileLastModifiedDate, (DateTime)(lastXmlDataHarvestDate ?? DateTime.Parse("1/1/1980"))) > 0)
                {
                    // We will replace any previously parsed dublin core elements, so delete any
                    // that already exist for this source (the _META file)
                    provider.IADCMetadataDeleteForItemAndSource(itemID, DC_SOURCE_META);

                    // Open the file and parse the data within it
                    String localFileName = localFileFolder + iaIdentifier + "\\" + file.LocalFileName;
                    XmlDocument xml = new();
                    xml.Load(localFileName);

                    // Read the Dublin Core elements
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_CONTRIBUTOR, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_COVERAGE, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_CREATOR, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_DATE, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_DESCRIPTION, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_FORMAT, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_IDENTIFIER, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_LANGUAGE, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_PUBLISHER, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_RELATION, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_RIGHTS, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_SOURCE, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_SUBJECT, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_TITLE, DC_SOURCE_META);
                    this.ReadAndSaveDCElements(itemID, xml, "metadata/" + DC_ATTRIB_TYPE, DC_SOURCE_META);

                    // Read additional elements
                    String sponsor = String.Empty;
                    String sponsorDate = String.Empty;
                    String scanningCenter = String.Empty;
                    String callNumber = String.Empty;
                    int imageCount = 0;
                    String identifierAccessUrl = String.Empty;
                    DateTime? addedDate = null;
                    String volume = String.Empty;
                    String note = String.Empty;
                    String scanOperator = String.Empty;
                    String scanDate = String.Empty;
                    String curation;
                    String externalStatus = String.Empty;
                    String titleID = String.Empty;
                    String year = String.Empty;
                    String identifierBib = String.Empty;
                    String licenseUrl = String.Empty;
                    String rights = String.Empty;
                    String dueDiligence = String.Empty;
                    String possibleCopyrightStatus = String.Empty;
                    String copyrightRegion = String.Empty;
                    String copyrightComment = String.Empty;
                    String copyrightEvidence = String.Empty;
                    String copyrightEvidenceOperator = String.Empty;
                    String copyrightEvidenceDate = String.Empty;
                    String scanningInstitution = String.Empty;
                    String rightsHolder = String.Empty;
                    String itemDescription = String.Empty;
                    string pageProgression = string.Empty;

                    XmlNode element = xml.SelectSingleNode("metadata/sponsor");
                    if (element != null) sponsor = element.InnerText;
                    element = xml.SelectSingleNode("metadata/sponsordate");
                    if (element != null) sponsorDate = element.InnerText;
                    element = xml.SelectSingleNode("metadata/scanningcenter");
                    if (element != null) scanningCenter = element.InnerText;
                    element = xml.SelectSingleNode("metadata/call_number");
                    if (element != null) callNumber = element.InnerText;
                    element = xml.SelectSingleNode("metadata/imagecount");
                    if (element != null) imageCount = Convert.ToInt32(element.InnerText);
                    element = xml.SelectSingleNode("metadata/identifier-access");
                    if (element != null) identifierAccessUrl = element.InnerText;
                    element = xml.SelectSingleNode("metadata/volume");
                    if (element != null) volume = element.InnerText;
                    element = xml.SelectSingleNode("metadata/notes");
                    if (element != null) note = element.InnerText;
                    element = xml.SelectSingleNode("metadata/operator");
                    if (element != null) scanOperator = element.InnerText;
                    element = xml.SelectSingleNode("metadata/scandate");
                    if (element != null) scanDate = element.InnerText;
                    element = xml.SelectSingleNode("metadata/addeddate");
                    if (element != null) addedDate = DateTime.Parse(element.InnerText);
                    element = xml.SelectSingleNode("metadata/curation");
                    if (element != null)
                    {
                        curation = element.InnerText;
                        //if (curation.ToLower().Contains("[state]approved[/state]")) externalStatus = "approved";
                        int startStateTag = curation.ToLower().IndexOf("[state]");
                        int endStateTag = curation.ToLower().IndexOf("[/state]");
                        if ((startStateTag > -1) && ((startStateTag + 7) < endStateTag))
                        {
                            externalStatus = curation[(startStateTag + 7)..endStateTag].ToLower();
                        }
                    }
                    element = xml.SelectSingleNode("metadata/title_id");
                    if (element != null) titleID = element.InnerText;
                    element = xml.SelectSingleNode("metadata/year");
                    if (element != null) year = element.InnerText;
                    element = xml.SelectSingleNode("metadata/identifier-bib");
                    if (element != null) identifierBib = element.InnerText;
                    element = xml.SelectSingleNode("metadata/licenseurl");
                    if (element != null) licenseUrl = element.InnerText;
                    element = xml.SelectSingleNode("metadata/rights");
                    if (element != null) rights = element.InnerText;
                    element = xml.SelectSingleNode("metadata/duediligence");
                    if (element != null) dueDiligence = element.InnerText;
                    element = xml.SelectSingleNode("metadata/possible-copyright-status");
                    if (element != null) possibleCopyrightStatus = element.InnerText;
                    element = xml.SelectSingleNode("metadata/copyright-region");
                    if (element != null) copyrightRegion = element.InnerText;
                    element = xml.SelectSingleNode("metadata/copyright-comment");
                    if (element != null) copyrightComment = element.InnerText;
                    element = xml.SelectSingleNode("metadata/copyright-evidence");
                    if (element != null) copyrightEvidence = element.InnerText;
                    element = xml.SelectSingleNode("metadata/copyright-evidence-operator");
                    if (element != null) copyrightEvidenceOperator = element.InnerText;
                    element = xml.SelectSingleNode("metadata/copyright-evidence-date");
                    if (element != null) copyrightEvidenceDate = element.InnerText;
                    element = xml.SelectSingleNode("metadata/scanning-institution");
                    if (element != null) scanningInstitution = element.InnerText;
                    element = xml.SelectSingleNode("metadata/rights-holder");
                    if (element != null) rightsHolder = element.InnerText;
                    element = xml.SelectSingleNode("metadata/copy-specific-information");
                    if (element != null) itemDescription = element.InnerText;
                    element = xml.SelectSingleNode("metadata/page-progression");
                    if (element != null) pageProgression = element.InnerText;

                    provider.IAItemUpdateMetadata(itemID, sponsor, sponsorDate, scanningCenter, 
                        callNumber, imageCount, identifierAccessUrl, volume, note, scanOperator,
                        scanDate, addedDate, externalStatus, titleID, year, identifierBib,
                        licenseUrl, rights, dueDiligence, possibleCopyrightStatus, copyrightRegion,
                        copyrightComment, copyrightEvidence, copyrightEvidenceOperator,
                        copyrightEvidenceDate, scanningInstitution, rightsHolder, itemDescription,
                        pageProgression);

                    // Read the identifier information
                    provider.IAItemIdentifierDeleteByItem(itemID);  // Delete existing, as we're doing a full replace
                    this.ReadAndSaveItemIdentifierElements(itemID, xml, "metadata/identifier-doi");
                    this.ReadAndSaveItemIdentifierElements(itemID, xml, "metadata/identifier-ark");
                    this.ReadAndSaveItemIdentifierElements(itemID, xml, "metadata/external-identifier");
                    this.ReadAndSaveItemIdentifierElements(itemID, xml, "metadata/issn");
                    this.ReadAndSaveItemIdentifierElements(itemID, xml, "metadata/isbn");

                    // Read the set information
                    provider.IAItemSetDeleteByItem(itemID);  // Delete existing, as we're doing a full replace

                    XmlNodeList mediatypes = xml.SelectNodes("metadata/mediatype");
                    foreach (XmlNode mediatype in mediatypes)
                    {
                        String mediatypeValue = mediatype.Name + ":" + mediatype.InnerText;
                        IASet set = provider.SaveIASet(mediatypeValue);
                        provider.SaveIAItemSet(itemID, set.SetID);
                    }
                    XmlNodeList collections = xml.SelectNodes("metadata/collection");
                    foreach (XmlNode collection in collections)
                    {
                        String collectionValue = collection.Name + ":" + collection.InnerText;
                        IASet set = provider.SaveIASet(collectionValue);
                        provider.SaveIAItemSet(itemID, set.SetID);
                    }
                }
            }
            else
            {
                // No local file, so remove anything in the database
                provider.IADCMetadataDeleteForItemAndSource(itemID, DC_SOURCE_META);
                provider.IAItemSetDeleteByItem(itemID);
                provider.IAItemUpdateMetadata(itemID, "", "", "", "", 0, "", "", "", "", "", null, 
                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
        }

        /// <summary>
        /// Use the specified XPath query to read Dublin Core data from the specified XML
        /// document and store the information in the database.
        /// </summary>
        /// <param name="itemID">Item to which to associated the DC data</param>
        /// <param name="xml">Xml document from which to read the DC data</param>
        /// <param name="xPath">XPath query to use for reading the XML document</param>
        /// <param name="source">Source file for the Xml document</param>
        private void ReadAndSaveDCElements(int itemID, XmlDocument xml, string xPath, string source)
        {
            XmlNodeList elements = xml.SelectNodes(xPath);

            // If we didn't find any elements in the file, try again... after first
            // adding a namespace to the search
            if (elements.Count == 0)
            {
                XmlNamespaceManager nsmgr = new(xml.NameTable);
                nsmgr.AddNamespace("oai_dc", "http://www.openarchives.org/OAI/2.0/oai_dc/");
                String nsPrefix = "//oai_dc:";
                elements = xml.SelectNodes(nsPrefix + xPath, nsmgr);
            }

            foreach (XmlNode element in elements)
            {
                String elementName = element.Name.Replace("dc:", "");
                String elementValue = element.InnerText;
                provider.IADCMetadataInsert(itemID, elementName, elementValue, source);
            }
        }

        private void ReadAndSaveItemIdentifierElements(int itemID, XmlDocument xml, string xPath)
        {
            XmlNodeList ids = xml.SelectNodes(xPath);
            foreach (XmlNode id in ids)
            {
                provider.SaveIAItemIdentifier(itemID, id.Name, id.InnerText);
            }
        }

        private void HarvestMetadataSourceData(IAFile file, int itemID, string iaIdentifier, string localFileFolder, DateTime? lastXmlDataHarvestDate)
        {
            LogMessage("Harvesting metadata source information for " + iaIdentifier);

            // If the file exists
            if (file != null)
            {
                // If the file has changed since we last harvested
                if (DateTime.Compare((DateTime)file.RemoteFileLastModifiedDate, (DateTime)(lastXmlDataHarvestDate ?? DateTime.Parse("1/1/1980"))) > 0)
                {
                    // Open the file and parse the data within it
                    String localFileName = localFileFolder + iaIdentifier + "\\" + file.LocalFileName;
                    XmlDocument xml = new();
                    xml.Load(localFileName);

                    String sponsorName = String.Empty;
                    String zQuery = String.Empty;
                    XmlNode element = xml.SelectSingleNode("metasource/target");
                    if (element != null) sponsorName = element.InnerText;
                    element = xml.SelectSingleNode("metasource/zquery");
                    if (element != null) zQuery = element.InnerText;
                    provider.IAItemUpdateMetadataSource(itemID, sponsorName, zQuery);
                }
            }
            else
            {
                // No local file, so remove anything in the database
                provider.IAItemUpdateMetadataSource(itemID, "", "");
            }
        }

        private void HarvestMarcData(IAFile file, int itemID, string iaIdentifier, string localFileFolder, DateTime? lastXmlDataHarvestDate, bool loadWithoutMarc)
        {
            LogMessage("Harvesting MARC data for " + iaIdentifier);

            // If the file exists
            if (file != null)
            {
                // If the file has changed since we last harvested
                if (DateTime.Compare((DateTime)file.RemoteFileLastModifiedDate, (DateTime)(lastXmlDataHarvestDate ?? DateTime.Parse("1/1/1980"))) > 0)
                {
                    // Open the file and parse the data within it
                    String localFileName = localFileFolder + iaIdentifier + "\\" + file.LocalFileName;
                    XmlDocument xml = new();
                    xml.Load(localFileName);

                    XmlNamespaceManager nsmgr = new(xml.NameTable);
                    nsmgr.AddNamespace("marc", "http://www.loc.gov/MARC21/slim");

                    // Insert or update the root Marc information
                    String leader = String.Empty;
                    XmlNode marcNode = xml.SelectSingleNode("//marc:record/marc:leader", nsmgr) ?? xml.SelectSingleNode("//marc:record/leader", nsmgr);
                    if (marcNode != null) leader = marcNode.InnerText;
                    IAMarc marc = provider.SaveIAMarc(itemID, leader);

                    // Delete any existing Marc control information
                    provider.IAMarcControlDeleteByMarcID(marc.MarcID);

                    // Insert the new Marc control information
                    XmlNodeList controlFields = xml.SelectNodes("//marc:record/marc:controlfield", nsmgr);
                    if (controlFields.Count == 0) controlFields = xml.SelectNodes("//marc:record/controlfield", nsmgr);
                    foreach (XmlNode controlField in controlFields)
                    {
                        String tag = (controlField.Attributes["tag"] == null) ? String.Empty : controlField.Attributes["tag"].Value;
                        String value = controlField.InnerText;
                        provider.IAMarcControlInsertAuto(marc.MarcID, tag, value);
                    }

                    // Delete any existing Marc data field and subfield information
                    provider.IAMarcDataFieldDeleteByMarcID(marc.MarcID);
                    
                    // Insert the new Marc data field and subfield information
                    XmlNodeList dataFields = xml.SelectNodes("//marc:record/marc:datafield", nsmgr);
                    foreach (XmlNode dataField in dataFields)
                    {
                        String tag = (dataField.Attributes["tag"] == null) ? String.Empty : dataField.Attributes["tag"].Value;
                        String indicator1 = (dataField.Attributes["ind1"] == null) ? String.Empty : dataField.Attributes["ind1"].Value;
                        String indicator2 = (dataField.Attributes["ind2"] == null) ? String.Empty : dataField.Attributes["ind2"].Value;
                        IAMarcDataField marcDataField = provider.IAMarcDataFieldInsertAuto(marc.MarcID, tag, indicator1, indicator2);

                        XmlNodeList subFields = dataField.SelectNodes("marc:subfield", nsmgr);
                        foreach (XmlNode subField in subFields)
                        {
                            String code = (subField.Attributes["code"] == null) ? String.Empty : subField.Attributes["code"].Value;
                            String value = subField.InnerText;
                            provider.IAMarcSubFieldInsertAuto(marcDataField.MarcDataFieldID, code, value);
                        }
                    }
                }
            }
            else
            {
                // No local file, so remove anything in the database
                provider.IAMarcDeleteAllByItem(itemID);

                // If we're not loading this item without a MARC file, throw an exception to indicate 
                // the absence of a MARC file and halt processing of the item
                if (!loadWithoutMarc) throw new MARCNotFoundException();
            }
        }

        private void HarvestDJVUData(IAFile file, int itemID, string iaIdentifier, string localFileFolder, DateTime? lastXmlDataHarvestDate)
        {
            LogMessage("Harvesting pages from DJVU data for " + iaIdentifier);

            // If the file exists
            if (file != null)
            {
                // If the file has changed since we last harvested
                if (DateTime.Compare((DateTime)file.RemoteFileLastModifiedDate, lastXmlDataHarvestDate ?? DateTime.Parse("1/1/1980")) > 0)
                {
                    // Prepare the file system for the page files
                    if (Directory.Exists(localFileFolder + iaIdentifier + "\\" + iaIdentifier))
                    {
                        // Remove any existing files
                        foreach (string fileName in Directory.GetFiles(localFileFolder + iaIdentifier + "\\" + iaIdentifier))
                        {
                            File.Delete(fileName);
                        }

                        // Delete any page entries from the database
                        provider.IAPageDeleteByItem(itemID);
                    }
                    else
                    {
                        // Create folder
                        Directory.CreateDirectory(localFileFolder + iaIdentifier + "\\" + iaIdentifier);
                    }

                    try
                    {
                        StringBuilder pageText = new();
                        XmlReaderSettings settings = new() { Async = true, DtdProcessing = DtdProcessing.Parse };
                        int counter = 1;
                        string localFileName = localFileFolder + iaIdentifier + "\\" + file.LocalFileName;
                        using (XmlReader reader = XmlReader.Create(new StreamReader(localFileName), settings))
                        {
                            bool wordStarted = false;
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Element && reader.Name == "OBJECT") pageText.Clear();
                                if (reader.NodeType == XmlNodeType.Element && reader.Name == "WORD") wordStarted = true;
                                if (reader.NodeType == XmlNodeType.Text && wordStarted) pageText.Append(reader.Value + " ");
                                if (reader.NodeType == XmlNodeType.EndElement)
                                {
                                    if (reader.Name == "WORD") wordStarted = false;
                                    if (reader.Name == "LINE") pageText.AppendLine();
                                    if (reader.Name == "PARAGRAPH") pageText.AppendLine();
                                    if (reader.Name == "OBJECT")
                                    {
                                        File.WriteAllText(string.Format("{0}{1}\\{1}\\{1}_{2}.txt", localFileFolder, iaIdentifier, Convert.ToString(counter).PadLeft(4, '0')), pageText.ToString());

                                        // Write a record to the database for this page
                                        string externalUrl = this.GetPageExternalUrl(itemID, iaIdentifier, counter);
                                        string textFileName = string.Format("{0}_{1}.txt", iaIdentifier, Convert.ToString(counter).PadLeft(4, '0'));
                                        provider.IAPageInsertAuto(itemID, textFileName, counter, externalUrl);

                                        counter++;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // Problem transforming the DJVU to TXT
                        log.Error("Error  converting DJVU to TXT for " + iaIdentifier, e);
                        this.errorMessages.Add("Error  converting DJVU to TXT for " + iaIdentifier + ": " + e.Message);
                    }
                }
            }
            else
            {
                // No local file, so remove anything in the database and any existing page files
                if (Directory.Exists(localFileFolder + iaIdentifier + "\\" + iaIdentifier))
                {
                    // Remove any existing files
                    foreach (string fileName in Directory.GetFiles(localFileFolder + iaIdentifier + "\\" + iaIdentifier))
                    {
                        File.Delete(fileName);
                    }
                }

                // Delete any page entries from the database
                provider.IAPageDeleteByItem(itemID);
            }
        }

        /// <summary>
        /// Get the external host and directory for the specified identifier
        /// </summary>
        /// <param name="iaIdentifier"></param>
        /// <param name="host"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        private void GetPhysicalFileLocation(string iaIdentifier, out string host, out string dir)
        {
            host = String.Empty;
            dir = String.Empty;

            try
            {
                // Get the physical file locations from Internet Archive
                XmlDocument xml = provider.GetIAXmlData(String.Format(configParms.PhysicalLocationUrl, iaIdentifier));

                XmlNode xmlLocation = xml.SelectSingleNode("results/location");
                if (xmlLocation != null)
                {
                    host = (xmlLocation.Attributes["host"] != null) ? xmlLocation.Attributes["host"].Value : String.Empty;
                    dir = (xmlLocation.Attributes["dir"] != null) ? xmlLocation.Attributes["dir"].Value : String.Empty;
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception getting physical file location for " + iaIdentifier, ex);
                errorMessages.Add("Exception getting physical file location for " + iaIdentifier + "  " + ex.Message);
                // don't rethrow; we want to continue processing
            }
        }

        /// <summary>
        /// Build the External Url for a page
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="iaIdentifier"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        private string GetPageExternalUrl(int itemID, string iaIdentifier, int? sequence)
        {
            string externalUrl = string.Empty;

            if (sequence == null) return externalUrl;
            string sequenceString = sequence.ToString().PadLeft(4, '0');

            try
            {
                // Get the "Flippy" file name from the database
                IAFile file = provider.IAFileSelectByItemAndFormat(itemID, "Flippy ZIP");
                if (file != null)
                {
                    // Format the external url for the page
                    externalUrl = string.Format(configParms.PageExternalUrl, iaIdentifier, file.RemoteFileName, sequenceString);
                }
            }
            catch (Exception ex)
            {
                externalUrl = string.Empty;
                log.Error("Exception getting External Url for " + iaIdentifier + "_" + sequenceString, ex);
                errorMessages.Add("Exception getting External Url for " + iaIdentifier + "_" + sequenceString + "  " + ex.Message);
                // don't rethrow; we want to continue processing
            }

            return externalUrl;
        }

        private void HarvestScandata(IAFile file, int itemID, string iaIdentifier, string localFileFolder, DateTime? lastXmlDataHarvestDate)
        {
            LogMessage("Harvesting scandata information for " + iaIdentifier);

            // If the file exists
            if (file != null)
            {
                // If the file has changed since we last harvested (check both possible scandata locations)
                if (file == null) file = provider.IAFileSelectByItemAndRemoteFileName(itemID, configParms.ScandataFile);

                if (DateTime.Compare((DateTime)file.RemoteFileLastModifiedDate, (DateTime)(lastXmlDataHarvestDate ?? DateTime.Parse("1/1/1980"))) > 0)
                {
                    // Open the file and parse the data within it
                    String localFileName = localFileFolder + iaIdentifier + "\\" + file.LocalFileName;
                    XmlDocument xml = new();
                    xml.Load(localFileName);

                    String nsPrefix = String.Empty;
                    XmlNamespaceManager nsmgr = null;
                    XmlNodeList pages = xml.SelectNodes("book/pageData/page");
                    XmlNodeList segments = xml.SelectNodes("book/bhlSegmentData/segment");

                    // If we didn't find any pages in the file, try again... after first
                    // adding a namespace to the XML document
                    if (pages.Count == 0)
                    {
                        nsmgr = new XmlNamespaceManager(xml.NameTable);
                        nsmgr.AddNamespace("ns", "http://archive.org/scribe/xml");
                        nsPrefix = "ns:";
                        pages = xml.SelectNodes(nsPrefix + "book/" + nsPrefix + "pageData/" + nsPrefix + "page", nsmgr);
                        if (segments.Count == 0) segments = xml.SelectNodes(nsPrefix + "book/" + nsPrefix + "bhlSegmentData/" + nsPrefix + "segment", nsmgr);
                    }

                    // Get page metadata
                    foreach (XmlNode page in pages)
                    {
                        // The only scandata we need to save is pages that have an addToAccessFormats 
                        // element with a value of "true".
                        bool includePage = false;
                        XmlNode addToAccessFormatsNode = page.SelectSingleNode(nsPrefix + "addToAccessFormats", nsmgr);
                        if (addToAccessFormatsNode != null) includePage = Convert.ToBoolean(addToAccessFormatsNode.InnerText);

                        if (includePage)
                        {
                            // This is scandata that we need to save
                            String pageType = String.Empty;
                            String pageNumber = String.Empty;
                            String year = null;
                            String volume = null;
                            String issue = null;
                            String issuePrefix = null;
                            int sequence = 0;

                            XmlNode pageTypeNode = page.SelectSingleNode(nsPrefix + "pageType", nsmgr);
                            if (pageTypeNode != null) pageType = pageTypeNode.InnerText;
                            XmlNode pageNumberNode = page.SelectSingleNode(nsPrefix + "pageNumber", nsmgr);
                            if (pageNumberNode != null) pageNumber = pageNumberNode.InnerText;
                            XmlNode yearNode = page.SelectSingleNode(nsPrefix + "year", nsmgr);
                            if (yearNode != null) year = yearNode.InnerText;
                            XmlNode volumeNode = page.SelectSingleNode(nsPrefix + "volume", nsmgr);
                            if (volumeNode != null) volume = volumeNode.InnerText;
                            XmlNode issueNode = page.SelectSingleNode(nsPrefix + "piece", nsmgr);
                            if (issueNode != null)
                            {
                                issue = issueNode.InnerText;
                                if (issueNode.Attributes["prefix"] != null) issuePrefix = issueNode.Attributes["prefix"].Value;
                            }
                            if (page.Attributes["leafNum"] != null) sequence = Convert.ToInt32(page.Attributes["leafNum"].Value);

                            IAScandata scandata = provider.SaveIAScandata(itemID, sequence, pageType, pageNumber, year, volume, 
                                issue, issuePrefix);

                            // Check for alternative page types
                            XmlNodeList altPageTypes = page.SelectNodes(nsPrefix + "altPageTypes/" + nsPrefix + "altPageType", nsmgr);
                            foreach (XmlNode altPageType in altPageTypes)
                            {
                                string altPageTypeValue = altPageType.InnerText;
                                provider.SaveIAScandataAltPageType(scandata.ScandataID, altPageTypeValue);
                            }

                            // Check for alternative page numbers
                            XmlNodeList altPageNumbers = page.SelectNodes(nsPrefix + "altPageNumbers/" + nsPrefix + "altPageNumber", nsmgr);
                            int altPageNumSequence = 1;
                            foreach (XmlNode altPageNumber in altPageNumbers)
                            {
                                string altPageNumberValue = altPageNumber.InnerText;
                                string altPageNumberPrefix = (altPageNumber.Attributes["prefix"] != null ? altPageNumber.Attributes["prefix"].Value : string.Empty);
                                string altPageNumberImplied = (altPageNumber.Attributes["implied"] != null ? altPageNumber.Attributes["implied"].Value : string.Empty);
                                provider.SaveIAScandataAltPageNumber(scandata.ScandataID, altPageNumSequence, altPageNumberPrefix, altPageNumberValue, (altPageNumberImplied == "1"));
                                altPageNumSequence++;
                            }
                        }
                    }

                    // Get segment metadata
                    int segmentSequence = 1;
                    foreach (XmlNode segment in segments)
                    {
                        string title = string.Empty;
                        string volume = string.Empty;
                        string issue = string.Empty;
                        string series = string.Empty;
                        string date = string.Empty;
                        string language = string.Empty;
                        int genreId = int.MinValue;
                        string genreName = string.Empty;
                        string doi = string.Empty;

                        XmlNode titleNode = segment.SelectSingleNode(nsPrefix + "title", nsmgr);
                        if (titleNode != null) title = titleNode.InnerText;
                        XmlNode volumeNode = segment.SelectSingleNode(nsPrefix + "volume", nsmgr);
                        if (volumeNode != null) volume = volumeNode.InnerText;
                        XmlNode issueNode = segment.SelectSingleNode(nsPrefix + "issue", nsmgr);
                        if (issueNode != null) issue = issueNode.InnerText;
                        XmlNode seriesNode = segment.SelectSingleNode(nsPrefix + "series", nsmgr);
                        if (seriesNode != null) series = seriesNode.InnerText;
                        XmlNode dateNode = segment.SelectSingleNode(nsPrefix + "date", nsmgr);
                        if (dateNode != null) date = dateNode.InnerText;
                        XmlNode languageNode = segment.SelectSingleNode(nsPrefix + "language", nsmgr);
                        if (languageNode != null) language = languageNode.InnerText;
                        XmlNode genreNode = segment.SelectSingleNode(nsPrefix + "genre", nsmgr);
                        if (genreNode != null)
                        {
                            if (genreNode.Attributes["id"] != null)
                            {
                                Int32.TryParse(genreNode.Attributes["id"].Value, out genreId);
                            }
                            genreName = genreNode.InnerText;
                        }
                        XmlNode doiNode = segment.SelectSingleNode(nsPrefix + "doi", nsmgr);
                        if (doiNode != null) doi = doiNode.InnerText;

                        IASegment iaSegment = provider.SaveIASegment(itemID, segmentSequence, title, volume, issue, series, date, language, genreId, genreName, doi);

                        // Check for authors
                        XmlNodeList authors = segment.SelectNodes(nsPrefix + "authors/" + nsPrefix + "author", nsmgr);
                        int authorSequence = 1;
                        foreach (XmlNode author in authors)
                        {
                            int authorId = int.MinValue;
                            string fullName = string.Empty;
                            string firstName = string.Empty;
                            string lastName = string.Empty;
                            string startDate = string.Empty;
                            string endDate = string.Empty;
                            int identifierId = int.MinValue;
                            string identifierValue = string.Empty;

                            if (author.Attributes["authorId"] != null)
                            {
                                Int32.TryParse(author.Attributes["authorId"].Value, out authorId);
                            }

                            XmlNode nameNode = author.SelectSingleNode(nsPrefix + "name", nsmgr);
                            if (nameNode != null) fullName = nameNode.InnerText;
                            XmlNode lastNameNode = author.SelectSingleNode(nsPrefix + "lastName", nsmgr);
                            if (lastNameNode != null) lastName = lastNameNode.InnerText;
                            XmlNode firstNameNode = author.SelectSingleNode(nsPrefix + "firstName", nsmgr);
                            if (firstNameNode != null) firstName = firstNameNode.InnerText;
                            XmlNode startDateNode = author.SelectSingleNode(nsPrefix + "startDate", nsmgr);
                            if (startDateNode != null) startDate = startDateNode.InnerText;
                            XmlNode endDateNode = author.SelectSingleNode(nsPrefix + "endDate", nsmgr);
                            if (endDateNode != null) endDate = endDateNode.InnerText;
                            XmlNode identifierNode = author.SelectSingleNode(nsPrefix + "identifier", nsmgr);
                            if (identifierNode != null)
                            {
                                if (identifierNode.Attributes["typeId"] != null)
                                {
                                    Int32.TryParse(identifierNode.Attributes["typeId"].Value, out identifierId);
                                }
                                identifierValue = identifierNode.InnerText;
                            }

                            provider.SaveIASegmentAuthor(iaSegment.SegmentID, authorSequence, (authorId == int.MinValue || authorId == 0 ? (int?)null : authorId), 
                                fullName, lastName, firstName, startDate, endDate, (identifierId == int.MinValue ? (int?)null : identifierId), identifierValue);

                            authorSequence++;
                        }

                        // Check for pages
                        XmlNodeList segmentPages = segment.SelectNodes(nsPrefix + "leafNums/" + nsPrefix + "leafNum", nsmgr);
                        int altPageNumSequence = 1;
                        foreach (XmlNode segmentPage in segmentPages)
                        {
                            int pageSequence = Convert.ToInt32(segmentPage.InnerText);

                            provider.SaveIASegmentPage(iaSegment.SegmentID, pageSequence);
                            altPageNumSequence++;
                        }

                        segmentSequence++;
                    }
                }
            }
            else
            {
                // No local file, so remove anything in the database
                provider.IASegmentDeleteByItem(itemID);
                provider.IAScandataDeleteAllByItem(itemID);
            }
        }

        #endregion Harvest XML

        #region Publish

        /// <summary>
        /// Publish the information harvested from Internet Archive to the import tables.
        /// </summary>
        private void PublishToImportTables()
        {
            try
            {
                LogMessage("Publishing information to import tables");

                // Get the items that are ready to be published
                List<IAItem> items = provider.IAItemSelectForPublishToImportTables(configParms.Mode == MODE_ITEM ? configParms.Item : "");

                bool continuePublishing = true;
                foreach (IAItem item in items)
                {
                    if (continuePublishing)
                    {
                        try
                        {
                            // Publish the item
                            if (provider.IAItemPublishToImportTables(item.ItemID))
                            {
                                this.publishedItems.Add(item.IAIdentifier);
                                if (!this.FixFileLocations(item.ItemID))
                                {
                                    // Possible network error has occurred, so halt publishing
                                    //log.Error("Publishing of information to import tables HALTED due to error fixing file locations");
                                    //errorMessages.Add("Publishing of information to import tables HALTED due to error fixing file locations");
                                    //continuePublishing = false;
                                }
                            }
                            else
                            {
                                log.Error("Error publishing information to import tables for " + item.IAIdentifier);
                                errorMessages.Add("Error publishing information to import tables for " + item.IAIdentifier);
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("Exception publishing information to import tables for " + item.IAIdentifier, ex);
                            errorMessages.Add("Exception publishing information to import tables for " + item.IAIdentifier + "  " + ex.Message);
                            // don't rethrow; we want to continue processing
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception publishing data to import tables", ex);
                errorMessages.Add("Exception publishing data to import tables  " + ex.Message);
            }
        }

        /// <summary>
        /// Move the files downloaded from the Internet Archive to their permanent locations
        /// </summary>
        /// <param name="itemID"></param>
        public bool FixFileLocations(int itemID)
        {
            // Get the item
            IAItem item = provider.IAItemSelectAuto(itemID);

            try
            {
                // Get the elements of the folder names from the item
                String iaIdentifier = item.IAIdentifier;
                String marcBibID = item.MARCBibID;
                String barCode = item.BarCode;

                // Get the original (temp) and new (permanent) folder names
                String originalPageFolder = item.LocalFileFolder + iaIdentifier + "\\" + iaIdentifier;
                String originalItemFolder = item.LocalFileFolder + iaIdentifier;
                String newItemFolder = item.LocalFileFolder + marcBibID;
                String newPageFolder = item.LocalFileFolder + marcBibID + "\\" + barCode;

                if (Directory.Exists(newItemFolder))
                {
                    // Move the pages to the existing folder
                    if (!Directory.Exists(newPageFolder)) MoveFolder(originalPageFolder, newPageFolder);

                    // Move the files to the existing folder
                    foreach (String file in Directory.GetFiles(originalItemFolder))
                    {
                        String newFileName = newItemFolder + "\\" + (new FileInfo(file).Name);
                        if (!File.Exists(newFileName)) MoveFile(file, newFileName);
                    }

                    // Remove the original (temp) folder
                    if (originalItemFolder != newItemFolder) Directory.Delete(originalItemFolder, false);
                }
                else
                {
                    // No existing folder for this title, so just rename the temp folder
                    MoveFolder(originalItemFolder, newItemFolder);

                    // Also rename the page folder if necessary
                    if (!Directory.Exists(newPageFolder)) MoveFolder(originalPageFolder, newPageFolder);
                }

                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error fixing file locations for " + item.IAIdentifier, ex);
                errorMessages.Add("Error fixing file locations for " + item.IAIdentifier + "  " + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Make three attempts to move the source folder to the target folder.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private void MoveFolder(string source, string target)
        {
            int count = 1;
            while (count <= 3)
            {
                try
                {
                    Directory.Move(source, target); break;
                }
                catch
                {
                    if (count == 3) throw;  // After three attempts, give up
                }
                System.Threading.Thread.Sleep(1000);    // Pause one second between attempts
                count++;
            }
        }

        /// <summary>
        /// Make three attempts to move the source file to the target file.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private void MoveFile(string source, string target)
        {
            int count = 1;
            while (count <= 3)
            {
                try
                {
                    File.Move(source, target); break;
                }
                catch
                {
                    if (count == 3) throw;  // After three attempts, give up
                }
                System.Threading.Thread.Sleep(1000);    // Pause one second between attempts
                count++;
            }
        }

        #endregion Publish

        #region Custom exceptions

        // To be thrown if no MARC file exists for an item
        [Serializable]
        public class MARCNotFoundException : Exception { }

        #endregion Custom exceptions

        #region Process Results

        /// <summary>
        /// Examine the results of the process and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // Report the process results
                if (retrievedIds.Count > 0 || harvestedXml.Count > 0 || publishedItems.Count > 0 || errorMessages.Count > 0)
                {
                    LogMessage("Sending Email....");
                    string message = this.GetEmailBody();
                    LogMessage(message);

                    // Send email if not in Quiet mode or if errors occurred
                    if (!configParms.Quiet || errorMessages.Count > 0) this.SendEmail(message);
                }
                else
                {
                    LogMessage("No items or pages processed.  Email not sent.");
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
                    LogMessage("Invalid command line format.  Format is IAHarvest.exe [/ITEM:itemid] [/STARTDATE:YYYY/MM/DD] [/ENDDATE:YYYY/MM/DD] [/DOWNLOAD:truefalse] [/UPLOAD:truefalse]");
                    return false;
                }

                if (String.Compare(split[0], "/ITEM", true) == 0)
                {
                    configParms.Mode = MODE_ITEM;
                    configParms.Item = split[1];
                }
                if (String.Compare(split[0], "/STARTDATE", true) == 0) configParms.SearchStartDate = Convert.ToDateTime(split[1]);
                if (String.Compare(split[0], "/ENDDATE", true) == 0) configParms.SearchEndDate = Convert.ToDateTime(split[1]);
                if (String.Compare(split[0], "/DOWNLOAD", true) == 0) configParms.Download = Convert.ToBoolean(split[1]);
                if (String.Compare(split[0], "/UPLOAD", true) == 0) configParms.Upload = Convert.ToBoolean(split[1]);
                if (String.Compare(split[0], "/QUIET", true) == 0) configParms.Quiet = Convert.ToBoolean(split[1]);
            }

            return true;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            if (configParms.Mode == MODE_ITEM && configParms.Item == string.Empty)
            {
                LogMessage("Item not set correctly.  When Mode is \"ITEM\", Item must contain a valid value.  Check configuration file.");
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
            StringBuilder sb = new();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            sb.Append("IAHarvest: IA Harvesting on " + thisComputer + " complete." + endOfLine);
            if (this.retrievedIds.Count > 0)
            {
                sb.Append(endOfLine + "Retrieved " + this.retrievedIds.Count.ToString() + " Identifiers" + endOfLine);
            }
            if (this.harvestedXml.Count > 0)
            {
                sb.Append(endOfLine + "Harvested XML for " + this.harvestedXml.Count.ToString() + " Identifiers" + endOfLine);
            }
            if (this.publishedItems.Count > 0)
            {
                sb.Append(endOfLine + "Published data to import tables for " + this.publishedItems.Count.ToString() + " Identifiers" + endOfLine);
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
                MailRequestModel mailRequest = new()
                {
                    Subject = string.Format(
                    "IAHarvest: IA Harvesting on {0} completed {1}.",
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

        #endregion Utility methods

    }
}

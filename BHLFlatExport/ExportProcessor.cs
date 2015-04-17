using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace MOBOT.BHL.BHLFlatExport
{
    public class ExportProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private int exportedTitles = 0;
        private int exportedTitleIDs = 0;
        private int exportedKeywords = 0;
        private int exportedAuthors = 0;
        private int exportedDOIs = 0;
        private int exportedItems = 0;
        private int exportedSegments = 0;
        private int exportedSegmentAuthors = 0;
        private int exportedPageNames = 0;
        private int exportedPages = 0;
        private List<string> errorMessages = new List<string>();

        /// <summary>
        /// Read and validate configuration parameters, and initiate the exports.
        /// </summary>
        /// <remarks>
        /// IMPORTANT:  This application does not use the Data Access layers used by most (all?)
        /// other BHL processes.  This is done because of the need to read huge amounts of data
        /// and process it while placing minimal locks on the database tables.
        /// </remarks>
        public void Process()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Set up the output folder for the export files
            if (!Directory.Exists(configParms.FileFolder)) Directory.CreateDirectory(configParms.FileFolder);
            foreach(FileInfo file in new DirectoryInfo(configParms.FileFolder).GetFiles()) file.Delete();

            // Export the data
            Export("ExportTitle", configParms.TitleFile, WriteTitleHeader, GetTitleRow);
            Export("ExportTitleIdentifier", configParms.TitleIdentifierFile, WriteTitleIdentifierHeader, GetTitleIdentifierRow);
            Export("ExportKeyword", configParms.KeywordFile, WriteKeywordHeader, GetKeywordRow);
            Export("ExportAuthor", configParms.AuthorFile, WriteAuthorHeader, GetAuthorRow);
            Export("ExportDOI", configParms.DOIFile, WriteDOIHeader, GetDOIRow);
            Export("ExportItem", configParms.ItemFile, WriteItemHeader, GetItemRow);
            Export("ExportSegment", configParms.PartFile, WriteSegmentHeader, GetSegmentRow);
            Export("ExportSegmentAuthor", configParms.PartAuthorFile, WriteSegmentAuthorHeader, GetSegmentAuthorRow);
            Export("ExportPageName", configParms.PageNameFile, WritePageNameHeader, GetPageNameRow);
            Export("ExportPage", configParms.PageFile, WritePageHeader, GetPageRow);

            // TODO: Zip the data (here or externally)?
            // TODO: Copy and clean up data (here or externally?)

            // Report the results of processing
            this.ProcessResults();

            this.LogMessage("BHLFlatExport Processing Complete");
        }

        public void Export(string storedProcedureName, string fileName, WriteFileHeader writeHeaderMethod, GetExportRow exportRowMethod)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BHL"].ConnectionString))
            {
                connection.Open();

                // Enable this later if necessary to avoid locking problems.
                // It requires the database to be altered to allow Snapshot Isolation.
                //SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Snapshot);
                SqlTransaction transaction = null;

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection, transaction)) // CustomSqlHelper.CreateCommand(storedProcedureName, connection))
                {
                    SqlDataReader reader = null;
                    try
                    {
                        // Read the data
                        command.CommandTimeout = 1800;  // 30 minute timeout
                        reader = command.ExecuteReader();
                        
                        // Write the data to a file
                        ExportData(fileName, reader, writeHeaderMethod, exportRowMethod);
                    }
                    finally
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
            }
        }

        public void ExportData(string fileName, SqlDataReader reader, WriteFileHeader writeHeader, GetExportRow exportRow)
        {
            string filePath = string.Format("{0}\\{1}", configParms.FileFolder, fileName);
            this.LogMessage(string.Format("Starting export to {0}", filePath));

            // Write the header row to the file
            writeHeader(filePath);

            StringBuilder sb = new StringBuilder();
            int numProcessed = 0;
            while (reader.Read())
            {
                numProcessed++;

                string line = exportRow(reader);
                sb.AppendLine(line);

                // Write data to the file after every 10000 records
                if ((numProcessed % 10000) == 0)
                {
                    // Write the lines to a file
                    File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
                    this.LogMessage(string.Format("{0} rows exported to {1}", numProcessed, filePath));
                    sb.Clear();
                }
            }

            if (sb.Length > 0)
            {
                File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
                this.LogMessage(string.Format("{0} rows exported to {1}", numProcessed, filePath));
            }

            this.LogMessage(string.Format("Done exporting to {0}", filePath));
        }
        
        #region FileHeader delegates

        public delegate void WriteFileHeader(string filePath);

        public void WriteDOIHeader(string filePath)
        {
            File.AppendAllText(filePath, "EntityType\tEntityID\tDOI\tCreationDate" + Environment.NewLine, Encoding.UTF8);
        }

        public void WriteAuthorHeader(string filePath)
        {
            File.AppendAllText(filePath, "TitleID\tCreatorType\tCreatorName\tCreationDate" + Environment.NewLine, Encoding.UTF8);
        }

        public void WriteItemHeader(string filePath)
        {
            File.AppendAllText(filePath, "ItemID\tTitleID\tThumbnailPageID\tBarCode\tMARCItemID\tCallNumber\tVolumeInfo\tItemURL\tLocalID\tYear\tInstitutionName\tZQuery\tCreationDate" + Environment.NewLine, Encoding.UTF8);
        }

        public void WritePageHeader(string filePath)
        {
            File.AppendAllText(filePath, "PageID\tItemID\tSequenceOrder\tYear\tVolume\tIssue\tPagePrefix\tPageNumber\tPageTypeName\tCreationDate" + Environment.NewLine, Encoding.UTF8);
        }

        public void WritePageNameHeader(string filePath)
        {
            File.AppendAllText(filePath, "NameBankID\tNameConfirmed\tPageID\tCreationDate" + Environment.NewLine, Encoding.UTF8);
        }

        public void WriteSegmentHeader(string filePath)
        {
            File.AppendAllText(filePath, "PartID\tItemID\tContributorName\tSequenceOrder\tSegmentType\tTitle\tContainerTitle\tPublicationDetails\tVolume\tSeries\tIssue\tDate\tPageRange\tStartPageID\tLanguageName\tSegmentUrl\tExternalUrl\tDownloadUrl\tRightsStatus\tRightsStatement\tLicenseName\tLicenseUrl" + Environment.NewLine, Encoding.UTF8);
        }

        public void WriteSegmentAuthorHeader(string filePath)
        {
            File.AppendAllText(filePath, "PartID\tCreatorName\tCreationDate" + Environment.NewLine, Encoding.UTF8);
        }

        public void WriteKeywordHeader(string filePath)
        {
            File.AppendAllText(filePath, "TitleID\tSubject\tCreationDate" + Environment.NewLine, Encoding.UTF8);
        }

        public void WriteTitleHeader(string filePath)
        {
            File.AppendAllText(filePath, "TitleID\tMARCBibID\tMARCLeader\tFullTitle\tShortTitle\tPublicationDetails\tCallNumber\tStartYear\tEndYear\tLanguageCode\tTL2Author\tTitleURL\tCreationDate" + Environment.NewLine, Encoding.UTF8);
        }

        public void WriteTitleIdentifierHeader(string filePath)
        {
            File.AppendAllText(filePath, "TitleID\tIdentifierName\tIdentifierValue\tCreationDate" + Environment.NewLine, Encoding.UTF8);
        }

        #endregion FileHeader delegates

        #region Datareader helper methods

        private string GetDBString(SqlDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? string.Empty : reader.GetString(reader.GetOrdinal(columnName));
        }

        private string GetDBInt32(SqlDataReader reader, string columnName)
        {
            string columnValue = string.Empty;
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                columnValue = reader.GetInt32(reader.GetOrdinal(columnName)).ToString();
            }
            return columnValue;
        }

        private string GetDBInt16(SqlDataReader reader, string columnName)
        {
            string columnValue = string.Empty;
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                columnValue = reader.GetInt16(reader.GetOrdinal(columnName)).ToString();
            }
            return columnValue;
        }

        private string GetDBDateTime(SqlDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? string.Empty : reader.GetDateTime(reader.GetOrdinal(columnName)).ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        }

        #endregion Datareader helper methods

        #region GetExportRow delegates

        public delegate string GetExportRow(SqlDataReader reader);

        public string GetDOIRow(SqlDataReader reader)
        {
            this.exportedDOIs++;
            string entityTypeName = GetDBString(reader, "EntityType");
            string entityID = GetDBInt32(reader, "EntityID");
            string doi = GetDBString(reader, "DOI");
            string creationDate = GetDBDateTime(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}\t{3}", entityTypeName, entityID, doi, creationDate);
        }

        public string GetAuthorRow(SqlDataReader reader)
        {
            this.exportedAuthors++;
            string titleID = GetDBInt32(reader, "TitleID");
            string creatorType = GetDBString(reader, "CreatorType");
            string creatorName = GetDBString(reader, "CreatorName");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}\t{3}", titleID, creatorType, creatorName, creationDate);
        }

        public string GetItemRow(SqlDataReader reader)
        {
            this.exportedItems++;
            string itemID = GetDBInt32(reader, "ItemID");
            string titleID = GetDBInt32(reader, "TitleID");
            string thumbnailPageID = GetDBInt32(reader, "ThumbnailPageID");
            string barCode = GetDBString(reader, "BarCode");
            string marcItemID = GetDBString(reader, "MARCItemID");
            string callNumber = GetDBString(reader, "CallNumber");
            string volumeInfo = GetDBString(reader, "VolumeInfo");
            string itemURL = string.Format(configParms.ItemUrlFormat, itemID);
            string identifierBib = GetDBString(reader, "LocalID");
            string year = GetDBString(reader, "Year");
            string institutionName = GetDBString(reader, "InstitutionName");
            string zQuery = GetDBString(reader, "ZQuery");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}",
                itemID, titleID, thumbnailPageID, barCode, marcItemID, callNumber, volumeInfo, itemURL,
                identifierBib, year, institutionName, zQuery, creationDate);
        }

        public string GetPageRow(SqlDataReader reader)
        {
            this.exportedPages++;
            string pageID = GetDBInt32(reader, "PageID");
            string itemID = GetDBInt32(reader, "ItemID");
            string sequenceOrder = GetDBInt32(reader, "SequenceOrder");
            string year = GetDBString(reader, "Year");
            string volume = GetDBString(reader, "Volume");
            string issue = GetDBString(reader, "Issue");
            string pagePrefix = GetDBString(reader, "PagePrefix");
            string pageNumber = GetDBString(reader, "PageNumber");
            string pageTypeName = GetDBString(reader, "PageTypeName");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}", pageID, itemID, sequenceOrder,
                year, volume, issue, pagePrefix, pageNumber, pageTypeName, creationDate);
        }

        public string GetPageNameRow(SqlDataReader reader)
        {
            this.exportedPageNames++;
            string nameBankID = GetDBString(reader, "NameBankID");
            string nameConfirmed = GetDBString(reader, "NameConfirmed");
            string pageID = GetDBInt32(reader, "PageID");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}\t{3}", nameBankID, nameConfirmed, pageID, creationDate);
        }

        public string GetSegmentRow(SqlDataReader reader)
        {
            this.exportedSegments++;
            string partID = GetDBInt32(reader, "SegmentID");
            string itemID = GetDBInt32(reader, "ItemID");
            string contributorName = GetDBString(reader, "ContributorName");
            string sequenceOrder = GetDBInt16(reader, "SequenceOrder");
            string segmentType = GetDBString(reader, "SegmentType");
            string title = GetDBString(reader, "Title");
            string containerTitle = GetDBString(reader, "ContainerTitle");
            string publicationDetails = GetDBString(reader, "PublicationDetails");
            string volume = GetDBString(reader, "Volume");
            string series = GetDBString(reader, "Series");
            string issue = GetDBString(reader, "Issue");
            string date = GetDBString(reader, "Date");
            string pageRange = GetDBString(reader, "PageRange");
            string startPageID = GetDBString(reader, "StartPageID");
            string languageName = GetDBString(reader, "LanguageName");
            string segmentUrl = GetDBString(reader, "SegmentUrl");
            string externalUrl = GetDBString(reader, "ExternalUrl");
            string downloadUrl = GetDBString(reader, "DownloadUrl");
            string rightsStatus = GetDBString(reader, "RightsStatus");
            string rightsStatement = GetDBString(reader, "RightsStatement");
            string licenseName = GetDBString(reader, "LicenseName");
            string licenseUrl = GetDBString(reader, "LicenseUrl");
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}",
                partID, itemID, contributorName, sequenceOrder, segmentType, title, containerTitle, publicationDetails,
                volume, series, issue, date, pageRange, startPageID, languageName, segmentUrl, externalUrl, downloadUrl,
                rightsStatus, rightsStatement, licenseName, licenseUrl);
        }

        public string GetSegmentAuthorRow(SqlDataReader reader)
        {
            this.exportedSegmentAuthors++;
            string partID = GetDBInt32(reader, "SegmentID");
            string creatorName = GetDBString(reader, "CreatorName");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}", partID, creatorName, creationDate);
        }

        public string GetKeywordRow(SqlDataReader reader)
        {
            this.exportedKeywords++;
            string titleID = GetDBInt32(reader, "TitleID");
            string subject = GetDBString(reader, "Subject");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}", titleID, subject, creationDate);
        }

        public string GetTitleRow(SqlDataReader reader)
        {
            this.exportedTitles++;
            string titleID = GetDBInt32(reader, "TitleID");
            string marcBibID = GetDBString(reader, "MARCBibID");
            string marcLeader = GetDBString(reader, "MARCLeader");
            string fullTitle = GetDBString(reader, "FullTitle");
            string shortTitle = GetDBString(reader, "ShortTitle");
            string publicationDetails = GetDBString(reader, "PublicationDetails");
            string callNumber = GetDBString(reader, "CallNumber");
            string startYear = GetDBInt16(reader, "StartYear");
            string endYear = GetDBInt16(reader, "EndYear");
            string languageCode = GetDBString(reader, "LanguageCode");
            string tl2Author = GetDBString(reader, "TL2Author");
            string titleUrl = GetDBString(reader, "TitleURL");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}",
                titleID, marcBibID, marcLeader, fullTitle, shortTitle, publicationDetails, callNumber, 
                startYear, endYear, languageCode, tl2Author, titleUrl, creationDate);
        }

        public string GetTitleIdentifierRow(SqlDataReader reader)
        {
            this.exportedTitleIDs++;
            string titleID = GetDBInt32(reader, "TitleID");
            string identifierName = GetDBString(reader, "IdentifierName");
            string identifierValue = GetDBString(reader, "IdentifierValue");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}\t{3}", titleID, identifierName, identifierValue, creationDate);
        }

        #endregion GetExportRow delegates

        /// <summary>
        /// Examine the results of the item/page processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // send email with process results to Exchange group
                if (exportedAuthors > 0 || exportedDOIs > 0 || exportedItems > 0 || exportedKeywords > 0 ||
                    exportedPageNames > 0 || exportedPages > 0 || exportedSegmentAuthors > 0 || exportedSegments > 0 ||
                    exportedTitleIDs > 0 || exportedTitles > 0 || errorMessages.Count > 0)
                {
                    this.LogMessage("Sending Email....");
                    string message = this.GetEmailBody();
                    this.LogMessage(message);
                    this.SendEmail(message);
                }
                else
                {
                    this.LogMessage("No errors.  Email not sent.");
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
        private string GetEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            sb.Append("BHLFlatExport: Processing  on " + thisComputer + " complete." + endOfLine);
            if (this.exportedTitles > 0)
            {
                sb.Append(endOfLine + "Exported " + this.exportedTitles.ToString() + " Titles" + endOfLine);
            }
            if (this.exportedTitleIDs > 0)
            {
                sb.Append(endOfLine + "Exported " + this.exportedTitleIDs.ToString() + " Title Identifiers" + endOfLine);
            }
            if (this.exportedKeywords > 0)
            {
                sb.Append(endOfLine + "Exported " + this.exportedKeywords.ToString() + " Keywords" + endOfLine);
            }
            if (this.exportedAuthors > 0)
            {
                sb.Append(endOfLine + "Exported " + this.exportedAuthors.ToString() + " Title Authors" + endOfLine);
            }
            if (this.exportedDOIs > 0)
            {
                sb.Append(endOfLine + "Exported " + this.exportedDOIs.ToString() + " DOIs" + endOfLine);
            }
            if (this.exportedItems > 0)
            {
                sb.Append(endOfLine + "Exported " + this.exportedItems.ToString() + " Items" + endOfLine);
            }
            if (this.exportedSegments > 0)
            {
                sb.Append(endOfLine + "Exported " + this.exportedSegments.ToString() + " Segments" + endOfLine);
            }
            if (this.exportedSegmentAuthors > 0)
            {
                sb.Append(endOfLine + "Exported " + this.exportedSegmentAuthors.ToString() + " Segment Authors" + endOfLine);
            }
            if (this.exportedPageNames > 0)
            {
                sb.Append(endOfLine + "Exported " + this.exportedPageNames.ToString() + " Page Names" + endOfLine);
            }
            if (this.exportedPages > 0)
            {
                sb.Append(endOfLine + "Exported " + this.exportedPages.ToString() + " Pages" + endOfLine);
            }
            if (this.errorMessages.Count > 0)
            {
                sb.Append(endOfLine + this.errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details" + endOfLine);
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
                string thisComputer = Environment.MachineName;
                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress(configParms.EmailFromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(configParms.EmailToAddress);
                if (this.errorMessages.Count == 0)
                {
                    mailMessage.Subject = "BHLFlatExport: Processing on " + thisComputer + " completed successfully.";
                }
                else
                {
                    mailMessage.Subject = "BHLFlatExport: Processing on " + thisComputer + " completed with errors.";
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
    
    }
}

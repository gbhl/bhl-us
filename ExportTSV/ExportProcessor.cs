using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHL.Export.TSV
{
    public class ExportProcessor : IBHLExport
    {
        // Create a default logger for use in this class
        private ExportLogger _log = new ExportLogger();

        private ConfigParms configParms = new ConfigParms();
        private Dictionary<string, int> _stats = new Dictionary<string, int>();
        private List<string> _errors = new List<string>();

        public Dictionary<string, int> Stats() { return _stats; }
        public List<string> Errors() { return _errors; }

        public void SetLogger(ExportLogger log)
        {
            if (log != null) _log = log;
        }

        /// <summary>
        /// Read and validate configuration parameters, and initiate the export.
        /// </summary>
        public void Process()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Set up the output folder for the export files
            if (File.Exists(configParms.AuthorFile)) File.Delete(configParms.AuthorFile);
            if (File.Exists(configParms.DOIFile)) File.Delete(configParms.DOIFile);
            if (File.Exists(configParms.ItemFile)) File.Delete(configParms.ItemFile);
            if (File.Exists(configParms.KeywordFile)) File.Delete(configParms.KeywordFile);
            if (File.Exists(configParms.PageFile)) File.Delete(configParms.PageFile);
            if (File.Exists(configParms.PageNameFile)) File.Delete(configParms.PageNameFile);
            if (File.Exists(configParms.PartAuthorFile)) File.Delete(configParms.PartAuthorFile);
            if (File.Exists(configParms.PartFile)) File.Delete(configParms.PartFile);
            if (File.Exists(configParms.TitleFile)) File.Delete(configParms.TitleFile);
            if (File.Exists(configParms.TitleIdentifierFile)) File.Delete(configParms.TitleIdentifierFile);

            // Export the data
            Export("ExportTitle", configParms.TitleFile, WriteTitleHeader, GetTitleRow, "Titles");
            Export("ExportTitleIdentifier", configParms.TitleIdentifierFile, WriteTitleIdentifierHeader, GetTitleIdentifierRow, "Title Identifiers");
            Export("ExportKeyword", configParms.KeywordFile, WriteKeywordHeader, GetKeywordRow, "Keywords");
            Export("ExportAuthor", configParms.AuthorFile, WriteAuthorHeader, GetAuthorRow, "Title Authors");
            Export("ExportDOI", configParms.DOIFile, WriteDOIHeader, GetDOIRow, "DOIs");
            Export("ExportItem", configParms.ItemFile, WriteItemHeader, GetItemRow, "Items");
            Export("ExportSegment", configParms.PartFile, WriteSegmentHeader, GetSegmentRow, "Segments");
            Export("ExportSegmentAuthor", configParms.PartAuthorFile, WriteSegmentAuthorHeader, GetSegmentAuthorRow, "Segment Authors");
            Export("ExportPageName", configParms.PageNameFile, WritePageNameHeader, GetPageNameRow, "Page Names");
            Export("ExportPage", configParms.PageFile, WritePageHeader, GetPageRow, "Pages");
        }

        public void Export(string storedProcedureName, string fileName, WriteFileHeader writeHeaderMethod, GetExportRow exportRowMethod, string statType)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BHL"].ConnectionString))
            {
                connection.Open();

                SqlTransaction transaction = null;

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection, transaction))
                {
                    SqlDataReader reader = null;
                    try
                    {
                        // Read the data
                        command.CommandTimeout = 1800;  // 30 minute timeout
                        reader = command.ExecuteReader();

                        // Write the data to a file
                        ExportData(fileName, reader, writeHeaderMethod, exportRowMethod, statType);
                    }
                    finally
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
            }
        }

        public void ExportData(string fileName, SqlDataReader reader, WriteFileHeader writeHeader, GetExportRow exportRow, string statType)
        {
            _log.Info(string.Format("Starting export to {0}", fileName));

            // Write the header row to the file
            writeHeader(fileName);

            StringBuilder sb = new StringBuilder();
            int numProcessed = 0;
            while (reader.Read())
            {
                numProcessed++;

                string line = exportRow(reader, statType);
                sb.AppendLine(line);

                // Write data to the file after every 10000 records
                if ((numProcessed % 10000) == 0)
                {
                    // Write the lines to a file
                    File.AppendAllText(fileName, sb.ToString(), Encoding.UTF8);
                    _log.Info(string.Format("{0} rows exported to {1}", numProcessed, fileName));
                    sb.Clear();
                }
            }

            if (sb.Length > 0)
            {
                File.AppendAllText(fileName, sb.ToString(), Encoding.UTF8);
                _log.Info(string.Format("{0} rows exported to {1}", numProcessed, fileName));
            }

            _log.Info(string.Format("Done exporting to {0}", fileName));
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

        public delegate string GetExportRow(SqlDataReader reader, string statType);

        public string GetDOIRow(SqlDataReader reader, string statType)
        {
            UpdateStats(statType);
            string entityTypeName = GetDBString(reader, "EntityType");
            string entityID = GetDBInt32(reader, "EntityID");
            string doi = GetDBString(reader, "DOI");
            string creationDate = GetDBDateTime(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}\t{3}", entityTypeName, entityID, doi, creationDate);
        }

        public string GetAuthorRow(SqlDataReader reader, string statType)
        {
            UpdateStats(statType);
            string titleID = GetDBInt32(reader, "TitleID");
            string creatorType = GetDBString(reader, "CreatorType");
            string creatorName = GetDBString(reader, "CreatorName");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}\t{3}", titleID, creatorType, creatorName, creationDate);
        }

        public string GetItemRow(SqlDataReader reader, string statType)
        {
            UpdateStats(statType);
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

        public string GetPageRow(SqlDataReader reader, string statType)
        {
            UpdateStats(statType);
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

        public string GetPageNameRow(SqlDataReader reader, string statType)
        {
            UpdateStats(statType);
            string nameBankID = GetDBString(reader, "NameBankID");
            string nameConfirmed = GetDBString(reader, "NameConfirmed");
            string pageID = GetDBInt32(reader, "PageID");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}\t{3}", nameBankID, nameConfirmed, pageID, creationDate);
        }

        public string GetSegmentRow(SqlDataReader reader, string statType)
        {
            UpdateStats(statType);
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

        public string GetSegmentAuthorRow(SqlDataReader reader, string statType)
        {
            UpdateStats(statType);
            string partID = GetDBInt32(reader, "SegmentID");
            string creatorName = GetDBString(reader, "CreatorName");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}", partID, creatorName, creationDate);
        }

        public string GetKeywordRow(SqlDataReader reader, string statType)
        {
            UpdateStats(statType);
            string titleID = GetDBInt32(reader, "TitleID");
            string subject = GetDBString(reader, "Subject");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}", titleID, subject, creationDate);
        }

        public string GetTitleRow(SqlDataReader reader, string statType)
        {
            UpdateStats(statType);
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

        public string GetTitleIdentifierRow(SqlDataReader reader, string statType)
        {
            UpdateStats(statType);
            string titleID = GetDBInt32(reader, "TitleID");
            string identifierName = GetDBString(reader, "IdentifierName");
            string identifierValue = GetDBString(reader, "IdentifierValue");
            string creationDate = GetDBString(reader, "CreationDate");
            return string.Format("{0}\t{1}\t{2}\t{3}", titleID, identifierName, identifierValue, creationDate);
        }

        #endregion GetExportRow delegates

        /// <summary>
        /// Update the statistics for the given key value
        /// </summary>
        /// <param name="statsKey"></param>
        private void UpdateStats(string statsKey)
        {
            if (_stats.ContainsKey(statsKey))
                _stats[statsKey]++;
            else
                _stats.Add(statsKey, 1);
        }
    }
}

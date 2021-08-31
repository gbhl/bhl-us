using Excel;
using MOBOT.BHL.AdminWeb.MVCServices;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.AdminWeb.Models
{
    [Serializable]
    public class CitationImportModel
    {
        #region Properties

        private int _genre = -1;

        public int Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        private string _genreName = string.Empty;

        public string GenreName
        {
            get { return _genreName; }
            set { _genreName = value; }
        }

        private string _fileCreationDate = string.Empty;

        public string FileCreationDate
        {
            get { return _fileCreationDate; }
            set { _fileCreationDate = value; }
        }

        private bool _isOldFile = false;

        public bool IsOldFile
        {
            get { return _isOldFile; }
            set { _isOldFile = value; }
        }

        private string _dataSourceType = string.Empty;

        public string DataSourceType
        {
            get { return _dataSourceType; }
            set { _dataSourceType = value; }
        }

        private int? _importFileID = null;

        public int? ImportFileID
        {
            get { return _importFileID; }
            set { _importFileID = value; }
        }

        private string _fileName = string.Empty;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private string _filePath = string.Empty;

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public string FileNameClean
        {
            get
            {
                int position = _fileName.IndexOf('.');
                int length = _fileName.Length;
                return _fileName.Substring((position >= 0 && position != length - 1) ? position + 1 : 0);
            }
        }

        private string _importFileError = string.Empty;

        public string ImportFileError
        {
            get { return _importFileError; }
            set { _importFileError = value; }
        }

        private string _codePage = "65001"; // UTF-8

        public string CodePage
        {
            get { return _codePage; }
            set { _codePage = value; }
        }

        private string _textQualifier = string.Empty;

        public string TextQualifier
        {
            get { return _textQualifier; }
            set { _textQualifier = value; }
        }

        private string _rowDelimiter = "\\r\\n";

        public string RowDelimiter
        {
            get { return _rowDelimiter; }
            set { _rowDelimiter = value; }
        }

        private string _columnDelimiter = "\\t";

        public string ColumnDelimiter
        {
            get { return _columnDelimiter; }
            set { _columnDelimiter = value; }
        }

        private int _headerRowsToSkip = 0;

        public int HeaderRowsToSkip
        {
            get { return _headerRowsToSkip; }
            set { _headerRowsToSkip = value; }
        }

        private bool _columnNamesInFirstRow = true;

        public bool ColumnNamesInFirstRow
        {
            get { return _columnNamesInFirstRow; }
            set { _columnNamesInFirstRow = value; }
        }

        private List<CitationImportColumn> _columns = new List<CitationImportColumn>();

        public List<CitationImportColumn> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        private List<List<string>> _rows = new List<List<string>>();

        public List<List<string>> Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        private int _numRows = 0;

        public int NumRows
        {
            get { return _numRows; }
            set { _numRows = value; }
        }

        #endregion Properties

        #region Enum

        private enum ExcelType
        {
            Xls,
            Xlsx
        }

        private enum FileStatus
        {
            Loading = 5,
            New = 10,
            Imported = 20,
            Rejected = 30
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// This class follows the TypeSafe Enum pattern 
        /// </remarks>
        public sealed class TemplateColumn
        {
            public string name { get; private set; }
            public int value { get; private set; }

            public static readonly TemplateColumn SEGMENTID = new TemplateColumn(1, "Segment ID");
            public static readonly TemplateColumn TITLE = new TemplateColumn(2, "Title");
            public static readonly TemplateColumn TRANSLATEDTITLE = new TemplateColumn(3, "Translated Title");
            public static readonly TemplateColumn ITEMID = new TemplateColumn(4, "Item ID");
            public static readonly TemplateColumn VOLUME = new TemplateColumn(5, "Volume");
            public static readonly TemplateColumn ISSUE = new TemplateColumn(6, "Issue");
            public static readonly TemplateColumn SERIES = new TemplateColumn(7, "Series");
            public static readonly TemplateColumn DATE = new TemplateColumn(8, "Date");
            public static readonly TemplateColumn LANGUAGE = new TemplateColumn(9, "Language");
            public static readonly TemplateColumn AUTHORS = new TemplateColumn(10, "Authors");
            public static readonly TemplateColumn STARTPAGE = new TemplateColumn(11, "Start Page");
            public static readonly TemplateColumn ENDPAGE = new TemplateColumn(12, "End Page");
            public static readonly TemplateColumn STARTPAGEID = new TemplateColumn(13, "Start Page BHL ID");
            public static readonly TemplateColumn ENDPAGEID = new TemplateColumn(14, "End Page BHL ID");
            public static readonly TemplateColumn ADDITIONALPAGEIDS = new TemplateColumn(15, "Additional Page IDs");
            public static readonly TemplateColumn DOI = new TemplateColumn(16, "Article DOI");
            public static readonly TemplateColumn CONTRIBUTORS = new TemplateColumn(16, "Contributors");

            private TemplateColumn(int value, string name)
            {
                this.value = value;
                this.name = name;
            }
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// This class follows the TypeSafe Enum pattern 
        /// </remarks>
        public sealed class MappedColumn
        {
            public string name { get; private set; }
            public int value { get; private set; }

            public static readonly MappedColumn NONE = new MappedColumn(0, "");
            public static readonly MappedColumn ABSTRACT = new MappedColumn(1, "Abstract");
            public static readonly MappedColumn ADDITIONALPAGES = new MappedColumn(2, "Additional Page IDs");
            public static readonly MappedColumn ARK = new MappedColumn(3, "ARK");
            public static readonly MappedColumn AUTHORNAMES = new MappedColumn(4, "Author Name(s)");
            public static readonly MappedColumn BIOSTOR = new MappedColumn(5, "Biostor");
            public static readonly MappedColumn DOI = new MappedColumn(6, "Article DOI");
            public static readonly MappedColumn ARTICLEPAGERANGE = new MappedColumn(7, "Article Page Range");
            public static readonly MappedColumn ARTICLEENDPAGE = new MappedColumn(8, "Article End Page");
            public static readonly MappedColumn ARTICLEENDPAGEID = new MappedColumn(9, "Article End Page ID");
            public static readonly MappedColumn ARTICLESTARTPAGE = new MappedColumn(10, "Article Start Page");
            public static readonly MappedColumn ARTICLESTARTPAGEID = new MappedColumn(11, "Article Start Page ID");
            public static readonly MappedColumn ARTICLETITLE = new MappedColumn(12, "Article Title");
            public static readonly MappedColumn BOOKJOURNALTITLE = new MappedColumn(13, "Book/Journal Title");
            public static readonly MappedColumn CONTRIBUTORS = new MappedColumn(14, "Contributors");
            public static readonly MappedColumn COPYRIGHTSTATUS = new MappedColumn(15, "Copyright Status");
            public static readonly MappedColumn DOWNLOADURL = new MappedColumn(16, "Download Url");
            public static readonly MappedColumn DUEDILIGENCE = new MappedColumn(17, "Due Diligence");
            public static readonly MappedColumn EDITION = new MappedColumn(18, "Edition");
            public static readonly MappedColumn ISBN = new MappedColumn(19, "ISBN");
            public static readonly MappedColumn ISSN = new MappedColumn(20, "ISSN");
            public static readonly MappedColumn ISSUE = new MappedColumn(21, "Issue");
            public static readonly MappedColumn ITEMID = new MappedColumn(22, "Item ID");
            public static readonly MappedColumn JSTOR = new MappedColumn(23, "JSTOR");
            public static readonly MappedColumn KEYWORDS = new MappedColumn(24, "Keyword(s)");
            public static readonly MappedColumn LANGUAGE = new MappedColumn(25, "Language");
            public static readonly MappedColumn LCCN = new MappedColumn(26, "LCCN");
            public static readonly MappedColumn LICENSE = new MappedColumn(27, "License");
            public static readonly MappedColumn LICENSEURL = new MappedColumn(28, "License Url");
            public static readonly MappedColumn NOTES = new MappedColumn(29, "Notes");
            public static readonly MappedColumn OCLC = new MappedColumn(30, "OCLC");
            public static readonly MappedColumn PUBLICATIONDETAILS = new MappedColumn(31, "Publication Details");
            public static readonly MappedColumn PUBLISHERNAME = new MappedColumn(32, "Publisher Name");
            public static readonly MappedColumn PUBLISHERPLACE = new MappedColumn(33, "Publisher Place");
            public static readonly MappedColumn RIGHTS = new MappedColumn(34, "Rights");
            public static readonly MappedColumn SEGMENTID = new MappedColumn(35, "Segment ID");
            public static readonly MappedColumn SERIES = new MappedColumn(36, "Series");
            public static readonly MappedColumn TITLEID = new MappedColumn(37, "TitleID");
            public static readonly MappedColumn TL2 = new MappedColumn(38, "TL2");
            public static readonly MappedColumn TRANSLATEDTITLE = new MappedColumn(39, "Translated Title");
            public static readonly MappedColumn URL = new MappedColumn(40, "Url");
            public static readonly MappedColumn VOLUME = new MappedColumn(41, "Volume");
            public static readonly MappedColumn WIKIDATA = new MappedColumn(42, "Wikidata");
            public static readonly MappedColumn YEAR = new MappedColumn(43, "Year");

            private MappedColumn(int value, string name)
            {
                this.value = value;
                this.name = name;
            }
        }

        #endregion Enum

        #region Public Methods

        public MappedColumn GetMappedColumn(string templateColumn)
        {
            MappedColumn mappedColumn = MappedColumn.NONE;

            if (templateColumn == TemplateColumn.SEGMENTID.name) mappedColumn = MappedColumn.SEGMENTID;
            if (templateColumn == TemplateColumn.TITLE.name) mappedColumn = MappedColumn.ARTICLETITLE;
            if (templateColumn == TemplateColumn.TRANSLATEDTITLE.name) mappedColumn = MappedColumn.TRANSLATEDTITLE;
            if (templateColumn == TemplateColumn.ITEMID.name) mappedColumn = MappedColumn.ITEMID;
            if (templateColumn == TemplateColumn.VOLUME.name) mappedColumn = MappedColumn.VOLUME;
            if (templateColumn == TemplateColumn.ISSUE.name) mappedColumn = MappedColumn.ISSUE;
            if (templateColumn == TemplateColumn.SERIES.name) mappedColumn = MappedColumn.SERIES;
            if (templateColumn == TemplateColumn.DATE.name) mappedColumn = MappedColumn.YEAR;
            if (templateColumn == TemplateColumn.LANGUAGE.name) mappedColumn = MappedColumn.LANGUAGE;
            if (templateColumn == TemplateColumn.AUTHORS.name) mappedColumn = MappedColumn.AUTHORNAMES;
            if (templateColumn == TemplateColumn.STARTPAGE.name) mappedColumn = MappedColumn.ARTICLESTARTPAGE;
            if (templateColumn == TemplateColumn.ENDPAGE.name) mappedColumn = MappedColumn.ARTICLEENDPAGE;
            if (templateColumn == TemplateColumn.STARTPAGEID.name) mappedColumn = MappedColumn.ARTICLESTARTPAGEID;
            if (templateColumn == TemplateColumn.ENDPAGEID.name) mappedColumn = MappedColumn.ARTICLEENDPAGEID;
            if (templateColumn == TemplateColumn.ADDITIONALPAGEIDS.name) mappedColumn = MappedColumn.ADDITIONALPAGES;
            if (templateColumn == TemplateColumn.DOI.name) mappedColumn = MappedColumn.DOI;
            if (templateColumn == TemplateColumn.CONTRIBUTORS.name) mappedColumn = MappedColumn.CONTRIBUTORS;

            return mappedColumn;
        }

        public string GetDelimiter(string templateColumn)
        {
            string delimiter = "";

            if (templateColumn == TemplateColumn.AUTHORS.name ||
                templateColumn == TemplateColumn.CONTRIBUTORS.name ||
                templateColumn == TemplateColumn.ADDITIONALPAGEIDS.name) delimiter = ";";

            return delimiter;
        }

        /// <summary>
        /// Parse the columns from the specified file
        /// </summary>
        /// <returns></returns>
        public void GetColumns()
        {
            List<CitationImportColumn> columns = new List<CitationImportColumn>();

            switch (this.DataSourceType)
            {
                case "text/plain":
                    columns = GetColumnsFromTextFile();
                    break;
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    columns = GetColumnsFromExcel(ExcelType.Xlsx);
                    break;
                case "application/vnd.ms-excel":
                    columns = GetColumnsFromExcel(ExcelType.Xls);
                    break;
            }

            this.Columns = columns;
        }

        /// <summary>
        /// Parse rows from the specified file.  
        /// </summary>
        /// <param name="isPreview">If true, only 50 rows of data are returned.  Otherwise, all rows are returned.</param>
        /// <param name="persist">If true, rows will be saved to the database.</param>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        public void GetRows(bool isPreview, bool persist, int userId)
        {
            List<List<string>> rows = new List<List<string>>();

            switch (this.DataSourceType)
            {
                case "text/plain":
                    rows = GetRowsFromTextFile(isPreview, persist, userId);
                    break;
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    rows = GetRowsFromExcel(ExcelType.Xlsx, isPreview, persist, userId);
                    break;
                case "application/vnd.ms-excel":
                    rows = GetRowsFromExcel(ExcelType.Xls, isPreview, persist, userId);
                    break;
            }

            this.Rows = rows;
        }

        /// <summary>
        /// Count the number of data rows in the specified file
        /// </summary>
        public void GetRowCount()
        {
            int numRows = 0;

            switch(this.DataSourceType)
            {
                case "text/plain":
                    numRows = GetRowCountFromTextFile();
                    break;
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    numRows = GetRowCountFromExcel(ExcelType.Xlsx);
                    break;
                case "application/vnd.ms-excel":
                    numRows = GetRowCountFromExcel(ExcelType.Xls);
                    break;
            }

            this.NumRows = numRows;
        }

        /// <summary>
        /// Save the contents of a file to the "import" database tables.
        /// </summary>
        /// <param name="securityToken"></param>
        /// <returns>Identifier of the ImportFile record.</returns>
        public void ImportFile(int userId)
        {
            BHLProvider service = new BHLProvider();

            try
            {
                ImportFile existingImportFile = new BHLProvider().ImportFileSelectByFileName(this.FileName);
                if (existingImportFile != null)
                {
                    // This file has already been processed.  Return the ID of the file.
                    this.ImportFileID = existingImportFile.ImportFileID;
                }
                else
                {
                    // Insert a new ImportFile record.
                    ImportFile importFile = service.ImportFileInsertAuto((int)FileStatus.Loading, this.FileName, null, userId, this.Genre);
                    this.ImportFileID = importFile.ImportFileID;

                    // Read and save all of the rows from the file
                    GetRows(false, true, userId);

                    // Update the status of the ImportFile record.
                    importFile.ImportFileStatusID = (int)FileStatus.New;
                    importFile = service.ImportFileUpdateAuto(importFile);
                }
            }
            catch
            {
                if (this.ImportFileID != null)
                {
                    service.ImportFileDelete((int)this.ImportFileID);
                    this.ImportFileID = null;
                }

                throw;
            }
        }

        /// <summary>
        /// Publish the metadata for all "New" records in the file into the production tables, and set the status of the records to "Imported".
        /// </summary>
        /// <param name="securityToken"></param>
        public void PublishFile(int userId)
        {
            new BHLProvider().ImportFilePublishToProduction((int)this.ImportFileID, userId);
        }

        /// <summary>
        /// Set the status of the file and all "New" records in the file to "Rejected".
        /// </summary>
        /// <param name="securityToken"></param>
        public void RejectFile(int userId)
        {
            new BHLProvider().ImportFileRejectFile((int)this.ImportFileID, userId);
        }

        /// <summary>
        /// Get the details for the specified import file ID
        /// </summary>
        /// <param name="importFileID"></param>
        public void GetImportFileDetails(int importFileID)
        {
            ImportFile importFile = new BHLProvider().ImportFileSelectById(importFileID);
            if (importFile != null)
            {
                this.ImportFileID = importFile.ImportFileID;
                this.FileName = importFile.ImportFileName;
                this.Genre = importFile.SegmentGenreID ?? 0;
                this.GenreName = string.IsNullOrWhiteSpace(importFile.GenreName) ? "Unknown" : importFile.GenreName;
                this.FileCreationDate = importFile.CreationDate.ToShortDateString();
                this.IsOldFile = (DateTime.Now - importFile.CreationDate).TotalDays > 7;
            }
        }

        /// <summary>
        /// Get import records from the specified import file.
        /// </summary>
        /// <param name="importFileID">Identifier of the import file</param>
        /// <param name="numRows">Number of rows to return</param>
        /// <param name="startRow">First row to return (enables paging)</param>
        /// <param name="sortColumn">Column by which to sort data</param>
        /// <param name="sortDirection">Direction of sort</param>
        public ImportRecordJson.Rootobject GetImportRecords(int importFileID, int numRows, int startRow, string sortColumn, string sortDirection)
        {
            List<ImportRecordReview> records = new BHLProvider().ImportRecordSelectForReviewByImportFileID(importFileID,
                numRows, startRow, sortColumn, sortDirection, 1);

            ImportRecordJson.Rootobject json = new ImportRecordJson.Rootobject();
            json.iTotalRecords = (records.Count == 0) ? "0" : records[0].TotalRecords.ToString();
            json.iTotalDisplayRecords = json.iTotalRecords;

            ImportRecordJson.Datum[] aaData = new ImportRecordJson.Datum[records.Count];

            for(int x = 0; x < records.Count; x++)
            {
                string sPageID = records[x].NSStartPageID == null ? 
                    (records[x].ESStartPageID == null ? "" : string.Format(" (ID: {0})", records[x].ESStartPageID.ToString())) : 
                    string.Format(" (ID: {0})", records[x].NSStartPageID.ToString());
                string ePageID = records[x].NSEndPageID == null ? 
                    (records[x].ESEndPageID == null ? "" : string.Format(" (ID: {0})", records[x].ESEndPageID.ToString())) : 
                    string.Format(" (ID: {0})", records[x].NSEndPageID.ToString());

                aaData[x] = new ImportRecordJson.Datum()
                {
                    id = records[x].ImportRecordID.ToString(),
                    operation = (records[x].ImportSegmentID == null ? "NEW" : "UPDATE"),
                    segmentID = records[x].SegmentID.ToString(),
                    importSegmentID = records[x].ImportSegmentID.ToString(),
                    status = records[x].StatusName,
                    errors = records[x].ErrorString,
                    warnings = records[x].WarningString,
                    // Summary values
                    sumTitle = string.IsNullOrWhiteSpace(records[x].NSTitle) ? records[x].ESTitle : records[x].NSTitle,
                    sumItemID = records[x].NCItemID == null ? (records[x].ECItemID == null ? "" : records[x].ECItemID.ToString()) : records[x].NCItemID.ToString(),
                    sumJournal = records[x].NCTitle,
                    sumYear = records[x].NCYear,
                    sumVolume = records[x].NCVolume,
                    sumIssue = records[x].NCIssue,
                    sumStartPageID = records[x].NSStartPageID == null ? (records[x].ESStartPageID == null ? "" : records[x].ESStartPageID.ToString()) : records[x].NSStartPageID.ToString(),
                    sumStartPage = (string.IsNullOrWhiteSpace(records[x].NSStartPage) ? records[x].ESStartPage : records[x].NSStartPage), // + sPageID,
                    sumEndPage = (string.IsNullOrWhiteSpace(records[x].NSEndPage) ? records[x].ESEndPage : records[x].NSEndPage), // + ePageID,
                    // New values (detailed)
                    ncItemID = records[x].NCItemID.ToString(),
                    ncJournal = records[x].NCTitle,
                    ncVolume = records[x].NCVolume,
                    ncSeries = records[x].NCSeries,
                    ncIssue = records[x].NCIssue,
                    ncEdition = records[x].NCEdition,
                    ncPublicationDetails = records[x].NCPublicationDetails,
                    ncPublisherName = records[x].NCPublisherName,
                    ncPublisherPlace = records[x].NCPublisherPlace,
                    ncYear = records[x].NCYear,
                    ncRights = records[x].NCRights,
                    ncCopyrightStatus = records[x].NCCopyrightStatus,
                    ncLicenseUrl = records[x].NCLicenseUrl,
                    nsGenre = records[x].NSGenre,
                    nsTitle = records[x].NSTitle,
                    nsTranslatedTitle = records[x].NSTranslatedTitle,
                    nsContainerTitle = records[x].NSJournalTitle,
                    nsVolume = records[x].NSVolume,
                    nsSeries = records[x].NSSeries,
                    nsIssue = records[x].NSIssue,
                    nsEdition = records[x].NSEdition,
                    nsPublicationDetails = records[x].NSPublicationDetails,
                    nsPublisherName = records[x].NSPublisherName,
                    nsPublisherPlace = records[x].NSPublisherPlace,
                    nsYear = records[x].NSYear,
                    nsLanguage = records[x].NSLanguage,
                    nsSummary = records[x].NSSummary,
                    nsNotes = records[x].NSNotes,
                    nsRights = records[x].NSRights,
                    nsCopyrightStatus = records[x].NSCopyrightStatus,
                    nsLicense = records[x].NSLicense,
                    nsLicenseUrl = records[x].NSLicenseUrl,
                    nsPageRange = records[x].NSPageRange,
                    nsStartPageID = records[x].NSStartPageID.ToString(),
                    nsStartPage = records[x].NSStartPage,
                    nsEndPageID = records[x].NSEndPageID.ToString(),
                    nsEndPage = records[x].NSEndPage,
                    nsUrl = records[x].NSUrl,
                    nsDownloadUrl = records[x].NSDownloadUrl,
                    nsDoi = records[x].NSDOI,
                    nsIssn = records[x].NSISSN,
                    nsIsbn = records[x].NSISBN,
                    nsOclc = records[x].NSOCLC,
                    nsLccn = records[x].NSLCCN,
                    nsArk = records[x].NSARK,
                    nsBiostor = records[x].NSBiostor,
                    nsJstor = records[x].NSJSTOR,
                    nsTl2 = records[x].NSTL2,
                    nsWikidata = records[x].NSWikidata,
                    nsAuthors = records[x].NSAuthorString,
                    nsKeywords = records[x].NSKeywordString,
                    nsContributors = records[x].NSContributorString,
                    nsPages = records[x].NSPageString,
                    // Existing values
                    ecItemID = records[x].ECItemID.ToString(),
                    ecJournal = records[x].ECTitle,
                    ecVolume = records[x].ECVolume,
                    ecSeries = records[x].ECSeries,
                    ecIssue = records[x].ECIssue,
                    ecEdition = records[x].ECEdition,
                    ecPublicationDetails = records[x].ECPublicationDetails,
                    ecPublisherName = records[x].ECPublisherName,
                    ecPublisherPlace = records[x].ECPublisherPlace,
                    ecYear = records[x].ECYear,
                    ecRights = records[x].ECRights,
                    ecCopyrightStatus = records[x].ECCopyrightStatus,
                    ecLicenseUrl = records[x].ECLicenseUrl,
                    esGenre = records[x].ESGenre,
                    esTitle = records[x].ESTitle,
                    esTranslatedTitle = records[x].ESTranslatedTitle,
                    esContainerTitle = records[x].ESJournalTitle,
                    esVolume = records[x].ESVolume,
                    esSeries = records[x].ESSeries,
                    esIssue = records[x].ESIssue,
                    esEdition = records[x].ESEdition,
                    esPublicationDetails = records[x].ESPublicationDetails,
                    esPublisherName = records[x].ESPublisherName,
                    esPublisherPlace = records[x].ESPublisherPlace,
                    esYear = records[x].ESYear,
                    esLanguage = records[x].ESLanguage,
                    esSummary = records[x].ESSummary,
                    esNotes = records[x].ESNotes,
                    esRights = records[x].ESRights,
                    esCopyrightStatus = records[x].ESCopyrightStatus,
                    esLicense = records[x].ESLicense,
                    esLicenseUrl = records[x].ESLicenseUrl,
                    esPageRange = records[x].ESPageRange,
                    esStartPage = records[x].ESStartPage,
                    esStartPageID = records[x].ESStartPageID.ToString(),
                    esEndPage = records[x].ESEndPage,
                    esEndPageID = records[x].ESEndPageID.ToString(),
                    esUrl = records[x].ESUrl,
                    esDownloadUrl = records[x].ESDownloadUrl,
                    esDoi = records[x].ESDOI,
                    esIssn = records[x].ESISSN,
                    esIsbn = records[x].ESISBN,
                    esOclc = records[x].ESOCLC,
                    esLccn = records[x].ESLCCN,
                    esArk = records[x].ESARK,
                    esBiostor = records[x].ESBiostor,
                    esJstor = records[x].ESJSTOR,
                    esTl2 = records[x].ESTL2,
                    esWikidata = records[x].ESWikidata,
                    esAuthors = records[x].ESAuthorString,
                    esKeywords = records[x].ESKeywordString,
                    esContributors = records[x].ESContributorString,
                    esPages = records[x].ESPageString,
                    // Actions
                    actRow = records[x].ImportSegmentID == null ? "ADD" : "UPDATE"
                };
            }
            json.aaData = aaData;

            return json;
        }

        /// <summary>
        /// Get the specified import record
        /// </summary>
        /// <param name="importRecordID">Identifier of the import record</param>
        public ImportRecordJson.Rootobject GetImportRecord(int importRecordID)
        {
            List<ImportRecordReview> records = new BHLProvider().ImportRecordSelectForReviewByImportRecordID(importRecordID);

            ImportRecordJson.Rootobject json = new ImportRecordJson.Rootobject();
            ImportRecordJson.Datum[] aaData = new ImportRecordJson.Datum[records.Count];

            for (int x = 0; x < records.Count; x++)
            {
                string sPageID = records[x].NSStartPageID == null ?
                    (records[x].ESStartPageID == null ? "" : string.Format(" (ID: {0})", records[x].ESStartPageID.ToString())) :
                    string.Format(" (ID: {0})", records[x].NSStartPageID.ToString());
                string ePageID = records[x].NSEndPageID == null ?
                    (records[x].ESEndPageID == null ? "" : string.Format(" (ID: {0})", records[x].ESEndPageID.ToString())) :
                    string.Format(" (ID: {0})", records[x].NSEndPageID.ToString());

                aaData[x] = new ImportRecordJson.Datum()
                {
                    id = records[x].ImportRecordID.ToString(),
                    operation = (records[x].ImportSegmentID == null ? "NEW" : "UPDATE"),
                    segmentID = records[x].SegmentID.ToString(),
                    importSegmentID = records[x].ImportSegmentID.ToString(),
                    status = records[x].StatusName,
                    errors = records[x].ErrorString,
                    warnings = records[x].WarningString,
                    // Summary values
                    sumTitle = string.IsNullOrWhiteSpace(records[x].NSTitle) ? records[x].ESTitle : records[x].NSTitle,
                    sumItemID = records[x].NCItemID == null ? (records[x].ECItemID == null ? "" : records[x].ECItemID.ToString()) : records[x].NCItemID.ToString(),
                    sumJournal = records[x].NCTitle,
                    sumYear = records[x].NCYear,
                    sumVolume = records[x].NCVolume,
                    sumIssue = records[x].NCIssue,
                    sumStartPageID = records[x].NSStartPageID == null ? (records[x].ESStartPageID == null ? "" : records[x].ESStartPageID.ToString()) : records[x].NSStartPageID.ToString(),
                    sumStartPage = (string.IsNullOrWhiteSpace(records[x].NSStartPage) ? records[x].ESStartPage : records[x].NSStartPage), // + sPageID,
                    sumEndPage = (string.IsNullOrWhiteSpace(records[x].NSEndPage) ? records[x].ESEndPage : records[x].NSEndPage), // + ePageID,
                    // New values (detailed)
                    ncItemID = records[x].NCItemID.ToString(),
                    ncJournal = records[x].NCTitle,
                    ncVolume = records[x].NCVolume,
                    ncSeries = records[x].NCSeries,
                    ncIssue = records[x].NCIssue,
                    ncEdition = records[x].NCEdition,
                    ncPublicationDetails = records[x].NCPublicationDetails,
                    ncPublisherName = records[x].NCPublisherName,
                    ncPublisherPlace = records[x].NCPublisherPlace,
                    ncYear = records[x].NCYear,
                    ncRights = records[x].NCRights,
                    ncCopyrightStatus = records[x].NCCopyrightStatus,
                    ncLicenseUrl = records[x].NCLicenseUrl,
                    nsGenre = records[x].NSGenre,
                    nsTitle = records[x].NSTitle,
                    nsTranslatedTitle = records[x].NSTranslatedTitle,
                    nsContainerTitle = records[x].NSJournalTitle,
                    nsVolume = records[x].NSVolume,
                    nsSeries = records[x].NSSeries,
                    nsIssue = records[x].NSIssue,
                    nsEdition = records[x].NSEdition,
                    nsPublicationDetails = records[x].NSPublicationDetails,
                    nsPublisherName = records[x].NSPublisherName,
                    nsPublisherPlace = records[x].NSPublisherPlace,
                    nsYear = records[x].NSYear,
                    nsLanguage = records[x].NSLanguage,
                    nsSummary = records[x].NSSummary,
                    nsNotes = records[x].NSNotes,
                    nsRights = records[x].NSRights,
                    nsCopyrightStatus = records[x].NSCopyrightStatus,
                    nsLicense = records[x].NSLicense,
                    nsLicenseUrl = records[x].NSLicenseUrl,
                    nsPageRange = records[x].NSPageRange,
                    nsStartPageID = records[x].NSStartPageID.ToString(),
                    nsStartPage = records[x].NSStartPage,
                    nsEndPageID = records[x].NSEndPageID.ToString(),
                    nsEndPage = records[x].NSEndPage,
                    nsUrl = records[x].NSUrl,
                    nsDownloadUrl = records[x].NSDownloadUrl,
                    nsDoi = records[x].NSDOI,
                    nsIssn = records[x].NSISSN,
                    nsIsbn = records[x].NSISBN,
                    nsOclc = records[x].NSOCLC,
                    nsLccn = records[x].NSLCCN,
                    nsArk = records[x].NSARK,
                    nsBiostor = records[x].NSBiostor,
                    nsJstor = records[x].NSJSTOR,
                    nsTl2 = records[x].NSTL2,
                    nsWikidata = records[x].NSWikidata,
                    nsAuthors = records[x].NSAuthorString,
                    nsKeywords = records[x].NSKeywordString,
                    nsContributors = records[x].NSContributorString,
                    nsPages = records[x].NSPageString,
                    // Existing values
                    ecItemID = records[x].ECItemID.ToString(),
                    ecJournal = records[x].ECTitle,
                    ecVolume = records[x].ECVolume,
                    ecSeries = records[x].ECSeries,
                    ecIssue = records[x].ECIssue,
                    ecEdition = records[x].ECEdition,
                    ecPublicationDetails = records[x].ECPublicationDetails,
                    ecPublisherName = records[x].ECPublisherName,
                    ecPublisherPlace = records[x].ECPublisherPlace,
                    ecYear = records[x].ECYear,
                    ecRights = records[x].ECRights,
                    ecCopyrightStatus = records[x].ECCopyrightStatus,
                    ecLicenseUrl = records[x].ECLicenseUrl,
                    esGenre = records[x].ESGenre,
                    esTitle = records[x].ESTitle,
                    esTranslatedTitle = records[x].ESTranslatedTitle,
                    esContainerTitle = records[x].ESJournalTitle,
                    esVolume = records[x].ESVolume,
                    esSeries = records[x].ESSeries,
                    esIssue = records[x].ESIssue,
                    esEdition = records[x].ESEdition,
                    esPublicationDetails = records[x].ESPublicationDetails,
                    esPublisherName = records[x].ESPublisherName,
                    esPublisherPlace = records[x].ESPublisherPlace,
                    esYear = records[x].ESYear,
                    esLanguage = records[x].ESLanguage,
                    esSummary = records[x].ESSummary,
                    esNotes = records[x].ESNotes,
                    esRights = records[x].ESRights,
                    esCopyrightStatus = records[x].ESCopyrightStatus,
                    esLicense = records[x].ESLicense,
                    esLicenseUrl = records[x].ESLicenseUrl,
                    esPageRange = records[x].ESPageRange,
                    esStartPage = records[x].ESStartPage,
                    esStartPageID = records[x].ESStartPageID.ToString(),
                    esEndPage = records[x].ESEndPage,
                    esEndPageID = records[x].ESEndPageID.ToString(),
                    esUrl = records[x].ESUrl,
                    esDownloadUrl = records[x].ESDownloadUrl,
                    esDoi = records[x].ESDOI,
                    esIssn = records[x].ESISSN,
                    esIsbn = records[x].ESISBN,
                    esOclc = records[x].ESOCLC,
                    esLccn = records[x].ESLCCN,
                    esArk = records[x].ESARK,
                    esBiostor = records[x].ESBiostor,
                    esJstor = records[x].ESJSTOR,
                    esTl2 = records[x].ESTL2,
                    esWikidata = records[x].ESWikidata,
                    esAuthors = records[x].ESAuthorString,
                    esKeywords = records[x].ESKeywordString,
                    esContributors = records[x].ESContributorString,
                    esPages = records[x].ESPageString,
                    // Actions
                    actRow = records[x].ImportSegmentID == null ? "ADD" : "UPDATE"
                };
            }
            json.aaData = aaData;

            return json;
        }

        /// <summary>
        /// Update the specified import record with the specified import record status.
        /// </summary>
        /// <param name="importRecordID"></param>
        /// <param name="importRecordStatusID"></param>
        /// <param name="securityToken"></param>
        /// <returns>The name of the new status.</returns>
        public string UpdateRecordStatus(int importRecordID, int importRecordStatusID, int userId)
        {
            BHLProvider bhlService = new BHLProvider();
            ImportRecord updatedRecord = bhlService.ImportRecordUpdateRecordStatus(importRecordID, importRecordStatusID, userId);
            return bhlService.ImportRecordStatusSelectAuto(updatedRecord.ImportRecordStatusID).StatusName;
        }

        /// <summary>
        /// Update the specified import record creator with the specified author id.
        /// </summary>
        /// <param name="importRecordCreatorID"></param>
        /// <param name="authorID"></param>
        /// <param name="userId"></param>
        public void UpdateRecordCreatorID(int importRecordCreatorID, int? authorID, int userId)
        {
            BHLProvider bhlService = new BHLProvider();
            bhlService.ImportRecordCreatorUpdateAuthorID(importRecordCreatorID, authorID, userId);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Parse the columns from a text file
        /// </summary>
        /// <returns></returns>
        private List<CitationImportColumn> GetColumnsFromTextFile()
        {
            List<CitationImportColumn> importColumns = new List<CitationImportColumn>();

            // Read a row from the file
            string line = null;
            using (System.IO.StreamReader file =
                new System.IO.StreamReader(this.FilePath, Encoding.GetEncoding(Convert.ToInt32(this.CodePage))))
            {
                if (this.HeaderRowsToSkip > 0)
                {
                    // Skip the specified number of rows
                    int counter = 0;
                    while ((line = ReadTextLine(file, this.RowDelimiter)) != null && counter < this.HeaderRowsToSkip) counter++;
                }

                // Read the next row from the file
                if (file.Peek() >= 0) line = ReadTextLine(file, this.RowDelimiter);
            }

            if (line != null)
            {
                // Get the column values
                string[] columns = line.Split(GetDelimiterChar(this.ColumnDelimiter));//, StringSplitOptions.RemoveEmptyEntries);

                // Parse the details of each column
                for (int x = 0; x < columns.Length; x++)
                {
                    string column = columns[x];

                    // Remove text qualifiers (if a qualifier was specified)
                    if ((this.TextQualifier ?? string.Empty) != string.Empty) column = column.Replace(this.TextQualifier, "");

                    CitationImportColumn importColumn = new CitationImportColumn();
                    importColumn.Position = x + 1;

                    if (this.ColumnNamesInFirstRow)
                        importColumn.ColumnName = column;
                    else
                        importColumn.ColumnName = "Column" + x.ToString();

                    importColumns.Add(importColumn);
                }
            }

            return importColumns;
        }

        /// <summary>
        /// Parse the columns from an Excel file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="firstRowHasColumnNames"></param>
        /// <param name="excelType"></param>
        /// <returns></returns>
        private List<CitationImportColumn> GetColumnsFromExcel(ExcelType excelType)
        {
            List<CitationImportColumn> importColumns = new List<CitationImportColumn>();

            FileStream stream = File.Open(this.FilePath, FileMode.Open, System.IO.FileAccess.Read);

            IExcelDataReader excelReader = null;
            try
            {
                if (excelType == ExcelType.Xls)
                {
                    // Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else
                {
                    // Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                // Convert Excel contents to a DataSet - each individual sheet will be created in result.Tables
                //excelReader.IsFirstRowAsColumnNames = firstRowHasColumnNames;
                //DataSet result = excelReader.AsDataSet();

                // Data Reader methods
                if (excelReader.Read())
                {
                    for (int x = 0; x < excelReader.FieldCount; x++)
                    {
                        CitationImportColumn column = new CitationImportColumn();
                        column.Position = x + 1;

                        if (this.ColumnNamesInFirstRow)
                            column.ColumnName = excelReader.GetString(x);
                        else
                            column.ColumnName = "Column" + x.ToString();

                        importColumns.Add(column);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // Free resources (IExcelDataReader is IDisposable)
                if (!excelReader.IsClosed) excelReader.Close();
            }

            return importColumns;
        }

        /// <summary>
        /// Parse rows from a text file
        /// </summary>
        /// <param name="isPreview">If true, only 50 rows of data are returned.  Otherwise, all rows are returned.</param>
        /// <param name="persist">If true, rows will be saved to the database.</param>
        /// <returns></returns>
        private List<List<string>> GetRowsFromTextFile(bool isPreview, bool persist, int userId)
        {
            List<List<string>> importRows = new List<List<string>>();

            using (System.IO.StreamReader file = new System.IO.StreamReader(this.FilePath, Encoding.GetEncoding(Convert.ToInt32(this.CodePage))))
            {
                string line = null;

                if (this.HeaderRowsToSkip > 0)
                {
                    // Skip the specified number of rows
                    int counter = 0;
                    while ((line = ReadTextLine(file, this.RowDelimiter)) != null && counter < this.HeaderRowsToSkip) counter++;
                }

                if (this.ColumnNamesInFirstRow)
                {
                    // Skip the row with the column names
                    if (file.Peek() >= 0) line = ReadTextLine(file, this.RowDelimiter);
                }

                // Read the rows (only 25 rows if isPreview)
                int maxRows = (isPreview ? 25 : 10000000);
                int count = 0;
                while ((line = ReadTextLine(file, this.RowDelimiter)) != null && count < maxRows)
                {
                    // Get the values from the row
                    string[] values = line.Split(GetDelimiterChar(this.ColumnDelimiter));//, StringSplitOptions.RemoveEmptyEntries);

                    // Clean up and save each value
                    List<string> row = new List<string>();
                    for (int x = 0; x < values.Length; x++)
                    {
                        string value = values[x];

                        //// Remove text qualifiers (if a qualifier was specified)
                        //if ((this.TextQualifier ?? string.Empty) != string.Empty) value = value.Replace(this.TextQualifier, "");

                        value = RemoveQuoteQualifiers(value);
                        row.Add(value);
                    }

                    // Skip blank rows
                    if (!IsRowBlank(row))
                    {
                        if (persist)
                            this.SaveCitation(row, userId);
                        else
                            importRows.Add(row);
                    }
                    count++;
                }
            }

            return importRows;
        }

        /// <summary>
        /// Parse rows from an Excel file
        /// </summary>
        /// <param name="excelType"></param>
        /// <param name="isPreview">If true, only 50 rows of data are returned.  Otherwise, all rows are returned.</param>
        /// <param name="persist">If true, rows will be saved to the database.</param>
        /// <returns></returns>
        private List<List<string>> GetRowsFromExcel(ExcelType excelType, bool isPreview, bool persist, int userId)
        {
            List<List<string>> importRows = new List<List<string>>();

            FileStream stream = File.Open(this.FilePath, FileMode.Open, System.IO.FileAccess.Read);

            IExcelDataReader excelReader = null;
            try
            {
                if (excelType == ExcelType.Xls)
                {
                    // Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else
                {
                    // Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                // Read the rows (only 25 rows if isPreview)
                int maxRows = (isPreview ? 25 : 10000000) + (this.ColumnNamesInFirstRow ? 1 : 0);
                int count = 0;
                while (excelReader.Read() && count < maxRows)
                {
                    // Skip row with column names
                    if (!(this.ColumnNamesInFirstRow && count == 0))
                    {
                        // Read all values in the row separately
                        List<string> row = new List<string>();
                        for (int x = 0; x < excelReader.FieldCount; x++)
                        {
                            string value = excelReader.GetString(x) ?? string.Empty;
                            value = RemoveQuoteQualifiers(value);
                            row.Add(value);
                        }

                        // Skip blank rows
                        if (!IsRowBlank(row))
                        {
                            // Save the row
                            if (persist)
                                this.SaveCitation(row, userId);
                            else
                                importRows.Add(row);
                        }
                    }
                    count++;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // Free resources (IExcelDataReader is IDisposable)
                if (!excelReader.IsClosed) excelReader.Close();
            }

            return importRows;
        }

        /// <summary>
        /// Count the total number of rows in a text file
        /// </summary>
        /// <returns></returns>
        private int GetRowCountFromTextFile()
        {
            int numRows = 0;

            using (System.IO.StreamReader file = new System.IO.StreamReader(this.FilePath, Encoding.GetEncoding(Convert.ToInt32(this.CodePage))))
            {
                string line = null;

                // Skip the specified number of rows
                if (this.HeaderRowsToSkip > 0)
                {
                    int counter = 0;
                    while ((line = ReadTextLine(file, this.RowDelimiter)) != null && counter < this.HeaderRowsToSkip) counter++;
                }

                // Skip the row with the column names
                if (this.ColumnNamesInFirstRow) if (file.Peek() >= 0) line = ReadTextLine(file, this.RowDelimiter);

                // Read the rows
                while ((line = ReadTextLine(file, this.RowDelimiter)) != null) numRows++;
            }

            return numRows;
        }

        /// <summary>
        /// Count the total number of rows in an Excel file
        /// </summary>
        /// <param name="excelType"></param>
        /// <returns></returns>
        private int GetRowCountFromExcel(ExcelType excelType)
        {
            int numRows = 0;

            FileStream stream = File.Open(this.FilePath, FileMode.Open, System.IO.FileAccess.Read);
            IExcelDataReader excelReader = null;
            try
            {
                if (excelType == ExcelType.Xls)
                    // Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                else
                    // Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                numRows = excelReader.AsDataSet().Tables[excelReader.Name].Rows.Count;
            }
            catch
            {
                throw;
            }
            finally
            {
                // Free resources (IExcelDataReader is IDisposable)
                if (!excelReader.IsClosed) excelReader.Close();
            }

            return numRows;
        }

        /// <summary>
        /// Checks all the elements in the row for blank values
        /// </summary>
        /// <param name="row"></param>
        /// <returns>True if all values are blank, otherwise False</returns>
        private bool IsRowBlank(List<string> row)
        {
            bool isBlank = true;

            foreach(string value in row) 
            {
                if (!string.IsNullOrWhiteSpace(value)) { isBlank = false; break; }
            }

            return isBlank;
        }

        /// <summary>
        /// If the specified value includes " or ' qualifiers, remove them.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="qualifier"></param>
        /// <returns></returns>
        private string RemoveQuoteQualifiers(string value)
        {
            if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                (value.StartsWith("'") && value.EndsWith("'")))
            {
                value = value.Remove(value.Length - 1);
                value = value.Substring(1);
            }
            return value;
        }

        /// <summary>
        /// Read the next line (ending with the specified delimiter) from the file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="rowDelimiter"></param>
        /// <returns></returns>
        private string ReadTextLine(StreamReader file, string rowDelimiter)
        {
            string line = null;

            if (rowDelimiter == "\\r\\n" || rowDelimiter == "\\r" || rowDelimiter == "\\n")
            {
                line = file.ReadLine();
            }
            else
            {
                StringBuilder newLine = new StringBuilder();
                int startPosition = 0;
                char[] nextChar = new char[1];
                while (file.Read(nextChar, 0, 1) > 0)
                {
                    if (nextChar.SequenceEqual(GetDelimiterChar(rowDelimiter))) break;
                    newLine.Append(nextChar);
                    startPosition++;
                }
                if (newLine.Length > 0) line = newLine.ToString();
            }

            return line;
        }

        /// <summary>
        /// Get the actual delimiter characters for the user-selected delimiter string
        /// </summary>
        /// <param name="delimiterString"></param>
        /// <returns></returns>
        private char[] GetDelimiterChar(string delimiterString)
        {
            char[] delimiter;

            switch (delimiterString)
            {
                case "\\r":
                    delimiter = ("\r").ToCharArray();
                    break;
                case "\\n":
                    delimiter = ("\n").ToCharArray();
                    break;
                case ";":
                    delimiter = (";").ToCharArray();
                    break;
                case ":":
                    delimiter = (":").ToCharArray();
                    break;
                case ",":
                    delimiter = (",").ToCharArray();
                    break;
                case "\\t":
                    delimiter = ("\t").ToCharArray();
                    break;
                case "|":
                    delimiter = ("|").ToCharArray();
                    break;
                case "\\r\\n":
                default:
                    delimiter = ("\r\n").ToCharArray();
                    break;
            }

            return delimiter;
        }

        /// <summary>
        /// Return a new object of type ImportRecordCreator
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        private ImportRecordCreator GetNewCreator(string fullName, string firstName = "", string lastName = "",
            string startYear = "", string endYear = "", string authorType = "")
        {
            ImportRecordCreator importRecordCreator = new ImportRecordCreator();
            int importedAuthorID = default(int);
            if (Int32.TryParse(fullName, out importedAuthorID))
            {
                importRecordCreator.ImportedAuthorID = importedAuthorID;
            }
            else
            {
                importRecordCreator.FullName = fullName;
                importRecordCreator.FirstName = firstName;
                importRecordCreator.LastName = lastName;
                importRecordCreator.StartYear = startYear;
                importRecordCreator.EndYear = endYear;
                importRecordCreator.AuthorType = authorType;
            }
            return importRecordCreator;
        }

        /// <summary>
        /// Return a new object of type ImportRecordKeyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private ImportRecordKeyword GetNewKeyword(string keyword)
        {
            ImportRecordKeyword importRecordKeyword = new ImportRecordKeyword();
            importRecordKeyword.Keyword = keyword;
            return importRecordKeyword;
        }

        private ImportRecordContributor GetNewContributor(string contributor)
        {
            ImportRecordContributor importRecordContributor = new ImportRecordContributor();
            importRecordContributor.InstitutionCode = contributor;
            return importRecordContributor;
        }

        #endregion Private Methods

        #region Database interactions

        /// <summary>
        /// Persist the data contained in the data row to the database
        /// </summary>
        /// <param name="row"></param>
        /// <param name="securityToken"></param>
        private void SaveCitation(List<string> row, int userId)
        {
            if (row.Count != this.Columns.Count)
            {
                throw new CitationService.ImportFileException(string.Format(
                    "Error Importing File.  {0} columns defined, but row has {1} columns.",
                    this.Columns.Count, row.Count));
            }

            ImportRecord citation = new ImportRecord();
            citation.ImportFileID = (int)this.ImportFileID;
            citation.ImportRecordStatusID = default(int);
            citation.Genre = this.GenreName;

            string articleTitle = string.Empty;
            string bookJournalTitle = string.Empty;

            for (int x = 0; x < this.Columns.Count; x++)
            {
                // Accumulate all of the data to be saved
                CitationImportColumn column = this.Columns[x];

                if (column.MappedColumn == MappedColumn.ABSTRACT.name) citation.Summary = row[x];
                if (column.MappedColumn == MappedColumn.ARK.name) citation.ARK = row[x];
                if (column.MappedColumn == MappedColumn.ARTICLEPAGERANGE.name) citation.PageRange = row[x];
                if (column.MappedColumn == MappedColumn.ARTICLEENDPAGE.name) citation.EndPage = row[x];
                if (column.MappedColumn == MappedColumn.ARTICLEENDPAGEID.name) citation.EndPageIDString = row[x];
                if (column.MappedColumn == MappedColumn.ARTICLESTARTPAGE.name) citation.StartPage = row[x];
                if (column.MappedColumn == MappedColumn.ARTICLESTARTPAGEID.name) citation.StartPageIDString = row[x];
                if (column.MappedColumn == MappedColumn.ARTICLETITLE.name) articleTitle = row[x];

                if (column.MappedColumn == MappedColumn.ADDITIONALPAGES.name)
                {
                    string[] pages = string.IsNullOrWhiteSpace(column.ValueDelimiter) ?
                        new string[] { row[x] } :
                        row[x].Split(column.ValueDelimiter.ToCharArray()).Distinct().ToArray();

                    bool nullInList = false;
                    foreach(string page in pages)
                    {
                        if (!string.IsNullOrWhiteSpace(page))
                        {
                            if (page.ToUpper().Trim() == "NULL") nullInList = true;
                            else citation.PageIDs.Add(page);
                        }
                    }
                    if (nullInList && citation.PageIDs.Count == 0) citation.PageIDs.Add("NULL");
                }

                if (column.MappedColumn == MappedColumn.AUTHORNAMES.name)
                {
                    string[] authors = string.IsNullOrWhiteSpace(column.ValueDelimiter) ?
                        new string[] { row[x] } :
                        row[x].Split(column.ValueDelimiter.ToCharArray()).Distinct().ToArray();

                    bool nullInList = false;
                    foreach (string author in authors)
                    {
                        if (!string.IsNullOrWhiteSpace(author))
                        {
                            if (author.ToUpper().Trim() == "NULL") nullInList = true;
                            else citation.Authors.Add(GetNewCreator(author));
                        }
                    }
                    if (nullInList && citation.Authors.Count == 0) citation.Authors.Add(GetNewCreator("NULL"));
                }

                if (column.MappedColumn == MappedColumn.BIOSTOR.name) citation.Biostor = row[x];
                if (column.MappedColumn == MappedColumn.BOOKJOURNALTITLE.name) bookJournalTitle = row[x];
                if (column.MappedColumn == MappedColumn.COPYRIGHTSTATUS.name) citation.CopyrightStatus = row[x];
                if (column.MappedColumn == MappedColumn.DOI.name) citation.DOI = row[x];
                if (column.MappedColumn == MappedColumn.DOWNLOADURL.name) citation.DownloadUrl = row[x];
                if (column.MappedColumn == MappedColumn.DUEDILIGENCE.name) citation.DueDiligence = row[x];
                if (column.MappedColumn == MappedColumn.EDITION.name) citation.Edition = row[x];
                //if (column.MappedColumn == MappedColumn.GENRE.name) citation.Genre = row[x];
                if (column.MappedColumn == MappedColumn.ISBN.name) citation.ISBN = row[x];
                if (column.MappedColumn == MappedColumn.ISSN.name) citation.ISSN = row[x];
                if (column.MappedColumn == MappedColumn.ISSUE.name) citation.Issue = row[x];
                if (column.MappedColumn == MappedColumn.ITEMID.name) citation.ItemIDString = row[x];
                if (column.MappedColumn == MappedColumn.JSTOR.name) citation.JSTOR = row[x];

                if (column.MappedColumn == MappedColumn.CONTRIBUTORS.name)
                {
                    string[] contributors = string.IsNullOrWhiteSpace(column.ValueDelimiter) ?
                        new string[] { row[x] } :
                        row[x].Split(column.ValueDelimiter.ToCharArray()).Distinct().ToArray();

                    bool nullInList = false;
                    foreach(string contributor in contributors)
                    {
                        if (!string.IsNullOrWhiteSpace(contributor))
                        {
                            if (contributor.ToUpper().Trim() == "NULL") nullInList = true;
                            else citation.Contributors.Add(GetNewContributor(contributor));
                        }
                    }
                    if (nullInList && citation.Contributors.Count == 0) citation.Contributors.Add(GetNewContributor("NULL"));
                }

                if (column.MappedColumn == MappedColumn.KEYWORDS.name)
                {
                    string[] keywords = string.IsNullOrWhiteSpace(column.ValueDelimiter) ?
                        new string[] { row[x] } :
                        row[x].Split(column.ValueDelimiter.ToCharArray()).Distinct().ToArray();

                    bool nullInList = false;
                    foreach (string keyword in keywords)
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            if (keyword.ToUpper().Trim() == "NULL") nullInList = true;
                            else citation.Keywords.Add(GetNewKeyword(keyword));
                        }
                    }
                    if (nullInList && citation.Keywords.Count == 0) citation.Keywords.Add(GetNewKeyword("NULL"));
                }

                if (column.MappedColumn == MappedColumn.LANGUAGE.name) citation.Language = row[x];
                if (column.MappedColumn == MappedColumn.LCCN.name) citation.LCCN = row[x];
                if (column.MappedColumn == MappedColumn.LICENSE.name) citation.License = row[x];
                if (column.MappedColumn == MappedColumn.LICENSEURL.name) citation.LicenseUrl = row[x];
                if (column.MappedColumn == MappedColumn.NOTES.name) citation.Notes = row[x];
                if (column.MappedColumn == MappedColumn.OCLC.name) citation.OCLC = row[x];
                if (column.MappedColumn == MappedColumn.PUBLICATIONDETAILS.name) citation.PublicationDetails = row[x];
                if (column.MappedColumn == MappedColumn.PUBLISHERNAME.name) citation.PublisherName = row[x];
                if (column.MappedColumn == MappedColumn.PUBLISHERPLACE.name) citation.PublisherPlace = row[x];
                if (column.MappedColumn == MappedColumn.RIGHTS.name) citation.Rights = row[x];
                if (column.MappedColumn == MappedColumn.SEGMENTID.name) citation.SegmentIDString = row[x];
                if (column.MappedColumn == MappedColumn.SERIES.name) citation.Series = row[x];
                if (column.MappedColumn == MappedColumn.TITLEID.name) citation.TitleIDString = row[x];
                if (column.MappedColumn == MappedColumn.TL2.name) citation.TL2 = row[x];
                if (column.MappedColumn == MappedColumn.TRANSLATEDTITLE.name) citation.TranslatedTitle = row[x];
                if (column.MappedColumn == MappedColumn.URL.name) citation.Url = row[x];
                if (column.MappedColumn == MappedColumn.VOLUME.name) citation.Volume = row[x];
                if (column.MappedColumn == MappedColumn.WIKIDATA.name) citation.Wikidata = row[x];
                if (column.MappedColumn == MappedColumn.YEAR.name) citation.Year = row[x];
            }

            // Set the book, journal, and article titles
            if (!string.IsNullOrWhiteSpace(articleTitle))
            {
                citation.Title = articleTitle;
                citation.JournalTitle = bookJournalTitle;
            }
            else
            {
                citation.Title = bookJournalTitle;
            }

            // Save the citation to the database
            new BHLProvider().ImportRecordSave(citation, userId);
        }

        #endregion Database interactions

    }

    /// <summary>
    /// Model to hold the attributes of a column in a data file
    /// </summary>
    [Serializable]
    public class CitationImportColumn
    {
        private int _position;

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private string _columnName;

        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        private string _mappedColumn;

        public string MappedColumn
        {
            get { return _mappedColumn; }
            set { _mappedColumn = value; }
        }

        private string _valueDelimiter = string.Empty;

        public string ValueDelimiter
        {
            get { return _valueDelimiter; }
            set { _valueDelimiter = value; }
        }
    }

    /// <summary>
    /// Class used to produce the JSON representation of citations records that is needed by jQuery DataTables
    /// </summary>
    [Serializable]
    public class ImportRecordJson
    {
        public class Rootobject
        {
            public int sEcho { get; set; }
            public string iTotalRecords { get; set; }
            public string iTotalDisplayRecords { get; set; }
            public Datum[] aaData { get; set;}
        }

        public class Datum
        {
            public string id { get; set; }
            public string operation { get; set; }
            public string segmentID { get; set; }
            public string importSegmentID { get; set; }
            public string status { get; set; }
            public string errors { get; set; }
            public string warnings { get; set; }

            public string sumTitle { get; set; }
            public string sumItemID { get; set; }
            public string sumJournal { get; set; }
            public string sumYear { get; set; }
            public string sumVolume { get; set; }
            public string sumIssue { get; set; }
            public string sumStartPageID { get; set; }
            public string sumStartPage { get; set; }
            public string sumEndPage { get; set; }

            public string ncItemID { get; set; }
            public string ncJournal { get; set; }
            public string ncVolume { get; set; }
            public string ncSeries { get; set; }
            public string ncIssue { get; set; }
            public string ncEdition { get; set; }
            public string ncPublicationDetails { get; set; }
            public string ncPublisherName { get; set; }
            public string ncPublisherPlace { get; set; }
            public string ncYear { get; set; }
            public string ncRights { get; set; }
            public string ncCopyrightStatus { get; set; }
            public string ncLicenseUrl { get; set; }

            public string ecItemID { get; set; }
            public string ecJournal { get; set; }
            public string ecVolume { get; set; }
            public string ecSeries { get; set; }
            public string ecIssue { get; set; }
            public string ecEdition { get; set; }
            public string ecPublicationDetails { get; set; }
            public string ecPublisherName { get; set; }
            public string ecPublisherPlace { get; set; }
            public string ecYear { get; set; }
            public string ecRights { get; set; }
            public string ecCopyrightStatus { get; set; }
            public string ecLicenseUrl { get; set; }

            public string nsGenre { get; set; }
            public string nsTitle { get; set; }
            public string nsTranslatedTitle { get; set; }
            public string nsContainerTitle { get; set; }
            public string nsVolume { get; set; }
            public string nsSeries { get; set; }
            public string nsIssue { get; set; }
            public string nsEdition { get; set; }
            public string nsPublicationDetails { get; set; }
            public string nsPublisherName { get; set; }
            public string nsPublisherPlace { get; set; }
            public string nsYear { get; set; }
            public string nsLanguage { get; set; }
            public string nsSummary { get; set; }
            public string nsNotes { get; set; }
            public string nsRights { get; set; }
            public string nsCopyrightStatus { get; set; }
            public string nsLicense { get; set; }
            public string nsLicenseUrl { get; set; }
            public string nsPageRange { get; set; }
            public string nsStartPage { get; set; }
            public string nsStartPageID { get; set; }
            public string nsEndPage { get; set; }
            public string nsEndPageID { get; set; }
            public string nsUrl { get; set; }
            public string nsDownloadUrl { get; set; }
            public string nsDoi { get; set; }
            public string nsIssn { get; set; }
            public string nsIsbn { get; set; }
            public string nsOclc { get; set; }
            public string nsLccn { get; set; }
            public string nsArk { get; set; }
            public string nsBiostor { get; set; }
            public string nsJstor { get; set; }
            public string nsTl2 { get; set; }
            public string nsWikidata { get; set; }
            public string nsAuthors { get; set; }
            public string nsKeywords { get; set; }
            public string nsContributors { get; set; }
            public string nsPages { get; set; }

            public string esGenre { get; set; }
            public string esTitle { get; set; }
            public string esTranslatedTitle { get; set; }
            public string esContainerTitle { get; set; }
            public string esVolume { get; set; }
            public string esSeries { get; set; }
            public string esIssue { get; set; }
            public string esEdition { get; set; }
            public string esPublicationDetails { get; set; }
            public string esPublisherName { get; set; }
            public string esPublisherPlace { get; set; }
            public string esYear { get; set; }
            public string esLanguage { get; set; }
            public string esSummary { get; set; }
            public string esNotes { get; set; }
            public string esRights { get; set; }
            public string esCopyrightStatus { get; set; }
            public string esLicense { get; set; }
            public string esLicenseUrl { get; set; }
            public string esPageRange { get; set; }
            public string esStartPage { get; set; }
            public string esStartPageID { get; set; }
            public string esEndPage { get; set; }
            public string esEndPageID { get; set; }
            public string esUrl { get; set; }
            public string esDownloadUrl { get; set; }
            public string esDoi { get; set; }
            public string esIssn { get; set; }
            public string esIsbn { get; set; }
            public string esOclc { get; set; }
            public string esLccn { get; set; }
            public string esArk { get; set; }
            public string esBiostor { get; set; }
            public string esJstor { get; set; }
            public string esTl2 { get; set; }
            public string esWikidata { get; set; }
            public string esAuthors { get; set; }
            public string esKeywords { get; set; }
            public string esContributors { get; set; }
            public string esPages { get; set; }

            public string actRow { get; set; }
        }
    }
}
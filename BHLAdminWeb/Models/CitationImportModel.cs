using CustomDataAccess;
using Excel;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.Security.Client.MOBOTSecurity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class CitationImportModel
    {
        #region Properties

        private string _contributor = string.Empty;

        public string Contributor
        {
            get { return _contributor; }
            set { _contributor = value; }
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

        private bool _columnNamesInFirstRow = false;

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

        private enum RecordStatus
        {
            New = 10,
            Imported = 20,
            Rejected = 30,
            Error = 40
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
            //public static readonly MappedColumn AUTHORENDDATE = new MappedColumn(1, "Author End Date");
            //public static readonly MappedColumn AUTHORFIRSTNAME = new MappedColumn(2, "Author First Name");
            //public static readonly MappedColumn AUTHORLASTNAME = new MappedColumn(3, "Author Last Name");
            public static readonly MappedColumn AUTHORNAMES = new MappedColumn(2, "Author Name(s)");
            //public static readonly MappedColumn AUTHORSTARTDATE = new MappedColumn(5, "Author Start Date");
            //public static readonly MappedColumn AUTHORTYPE = new MappedColumn(6, "Author Type");
            public static readonly MappedColumn ARTICLEPAGERANGE = new MappedColumn(3, "Article Page Range");
            public static readonly MappedColumn ARTICLEENDPAGE = new MappedColumn(4, "Article End Page");
            public static readonly MappedColumn ARTICLESTARTPAGE = new MappedColumn(5, "Article Start Page");
            public static readonly MappedColumn ARTICLETITLE = new MappedColumn(6, "Article Title");
            public static readonly MappedColumn BOOKJOURNALTITLE = new MappedColumn(7, "Book/Journal Title");
            public static readonly MappedColumn COPYRIGHTSTATUS = new MappedColumn(8, "Copyright Status");
            public static readonly MappedColumn DOI = new MappedColumn(9, "DOI");
            public static readonly MappedColumn DOWNLOADURL = new MappedColumn(10, "Download Url");
            public static readonly MappedColumn DUEDILIGENCE = new MappedColumn(11, "Due Diligence");
            public static readonly MappedColumn EDITION = new MappedColumn(12, "Edition");
            public static readonly MappedColumn GENRE = new MappedColumn(13, "Genre");
            public static readonly MappedColumn ISBN = new MappedColumn(14, "ISBN");
            public static readonly MappedColumn ISSN = new MappedColumn(15, "ISSN");
            public static readonly MappedColumn ISSUE = new MappedColumn(16, "Issue");
            public static readonly MappedColumn JOURNALENDYEAR = new MappedColumn(17, "Journal End Year");
            public static readonly MappedColumn JOURNALSTARTYEAR = new MappedColumn(18, "Journal Start Year");
            public static readonly MappedColumn KEYWORDS = new MappedColumn(19, "Keyword(s)");
            public static readonly MappedColumn LANGUAGE = new MappedColumn(20, "Language");
            public static readonly MappedColumn LCCN = new MappedColumn(21, "LCCN");
            public static readonly MappedColumn LICENSE = new MappedColumn(22, "License");
            public static readonly MappedColumn LICENSEURL = new MappedColumn(23, "License Url");
            public static readonly MappedColumn NOTES = new MappedColumn(24, "Notes");
            public static readonly MappedColumn OCLC = new MappedColumn(26, "OCLC");
            public static readonly MappedColumn PUBLICATIONDETAILS = new MappedColumn(27, "Publication Details");
            public static readonly MappedColumn PUBLISHERNAME = new MappedColumn(28, "Publisher Name");
            public static readonly MappedColumn PUBLISHERPLACE = new MappedColumn(29, "Publisher Place");
            public static readonly MappedColumn RIGHTS = new MappedColumn(30, "Rights");
            public static readonly MappedColumn SERIES = new MappedColumn(31, "Series");
            public static readonly MappedColumn TRANSLATEDTITLE = new MappedColumn(32, "Translated Title");
            public static readonly MappedColumn URL = new MappedColumn(33, "Url");
            public static readonly MappedColumn VOLUME = new MappedColumn(34, "Volume");
            public static readonly MappedColumn YEAR = new MappedColumn(35, "Year");

            private MappedColumn(int value, string name)
            {
                this.value = value;
                this.name = name;
            }
        }

        #endregion Enum

        #region Public Methods

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
        public void GetRows(bool isPreview, bool persist, string securityToken)
        {
            List<List<string>> rows = new List<List<string>>();

            switch (this.DataSourceType)
            {
                case "text/plain":
                    rows = GetRowsFromTextFile(isPreview, persist, securityToken);
                    break;
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    rows = GetRowsFromExcel(ExcelType.Xlsx, isPreview, persist, securityToken);
                    break;
                case "application/vnd.ms-excel":
                    rows = GetRowsFromExcel(ExcelType.Xls, isPreview, persist, securityToken);
                    break;
            }

            this.Rows = rows;
        }

        /// <summary>
        /// Save the contents of a file to the "import" database tables.
        /// </summary>
        /// <param name="securityToken"></param>
        /// <returns>Identifier of the ImportFile record.</returns>
        public void ImportFile(string securityToken)
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
                    ImportFile importFile = service.ImportFileInsertAuto((int)FileStatus.Loading, this.FileName, this.Contributor,
                        GetUserID(securityToken));
                    this.ImportFileID = importFile.ImportFileID;

                    // Read and save all of the rows from the file
                    GetRows(false, true, securityToken);

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
        public void PublishFile(string securityToken)
        {
            new BHLProvider().ImportFilePublishToProduction((int)this.ImportFileID, GetUserID(securityToken));
        }

        /// <summary>
        /// Set the status of the file and all "New" records in the file to "Rejected".
        /// </summary>
        /// <param name="securityToken"></param>
        public void RejectFile(string securityToken)
        {
            new BHLProvider().ImportFileRejectFile((int)this.ImportFileID, GetUserID(securityToken));
        }

        /// <summary>
        /// Get the details for the specified import file ID
        /// </summary>
        /// <param name="importFileID"></param>
        public void GetImportFileDetails(int importFileID)
        {
            ImportFile importFile = new BHLProvider().ImportFileSelectAuto(importFileID);
            if (importFile != null)
            {
                this.ImportFileID = importFile.ImportFileID;
                this.FileName = importFile.ImportFileName;
                this.Contributor = importFile.ContributorCode;
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
            CustomGenericList<ImportRecord> records = new BHLProvider().ImportRecordSelectByImportFileID(importFileID,
                numRows, startRow, sortColumn, sortDirection);

            ImportRecordJson.Rootobject json = new ImportRecordJson.Rootobject();
            json.iTotalRecords = (records.Count == 0) ? "0" : records[0].TotalRecords.ToString();
            json.iTotalDisplayRecords = json.iTotalRecords;

            string[][] aaData = new string[records.Count][];
            for(int x = 0; x < records.Count; x++)
            {
                aaData[x] = new string[9]{
                    records[x].ImportRecordID.ToString(),
                    records[x].Genre,
                    records[x].Title,
                    records[x].JournalTitle,
                    records[x].Volume,
                    records[x].Series,
                    records[x].Issue,
                    records[x].Year,
                    records[x].StatusName};
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
        public string UpdateRecordStatus(int importRecordID, int importRecordStatusID, string securityToken)
        {
            BHLProvider bhlService = new BHLProvider();
            ImportRecord updatedRecord = bhlService.ImportRecordUpdateRecordStatus(importRecordID, importRecordStatusID, GetUserID(securityToken));
            return bhlService.ImportRecordStatusSelectAuto(updatedRecord.ImportRecordStatusID).StatusName;
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
                string[] columns = line.Split(GetDelimiterChar(this.ColumnDelimiter), StringSplitOptions.RemoveEmptyEntries);

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
        private List<List<string>> GetRowsFromTextFile(bool isPreview, bool persist, string securityToken)
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

                        // Remove text qualifiers (if a qualifier was specified)
                        if ((this.TextQualifier ?? string.Empty) != string.Empty) value = value.Replace(this.TextQualifier, "");

                        row.Add(value);
                    }

                    if (persist)
                        this.SaveCitation(row, securityToken);
                    else
                        importRows.Add(row);
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
        private List<List<string>> GetRowsFromExcel(ExcelType excelType, bool isPreview, bool persist, string securityToken)
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
                            row.Add(excelReader.GetString(x) ?? string.Empty);
                        }

                        // Save the row
                        if (persist)
                            this.SaveCitation(row, securityToken);
                        else
                            importRows.Add(row);
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
            importRecordCreator.FullName = fullName;
            importRecordCreator.FirstName = firstName;
            importRecordCreator.LastName = lastName;
            importRecordCreator.StartYear = startYear;
            importRecordCreator.EndYear = endYear;
            importRecordCreator.AuthorType = authorType;
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

        /// <summary>
        /// Get the ID of the user associated with the specified security token
        /// </summary>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        private int GetUserID(string securityToken)
        {
            SecUser user = Helper.GetSecProvider().SecUserSelect(securityToken);
            return (user == null) ? 1 : user.UserID;
        }
 
        #endregion Private Methods

        #region Database interactions

        /// <summary>
        /// Persist the data contained in the data row to the database
        /// </summary>
        /// <param name="row"></param>
        /// <param name="securityToken"></param>
        private void SaveCitation(List<string> row, string securityToken)
        {
            ImportRecord citation = new ImportRecord();
            citation.ImportFileID = (int)this.ImportFileID;
            citation.ImportRecordStatusID = (int)RecordStatus.New;

            string articleTitle = string.Empty;
            string bookJournalTitle = string.Empty;

            for (int x = 0; x < this.Columns.Count; x++)
            {
                // Accumulate all of the data to be saved
                CitationImportColumn column = this.Columns[x];

                if (column.MappedColumn == MappedColumn.ABSTRACT.name) citation.Summary = row[x];
                if (column.MappedColumn == MappedColumn.ARTICLEPAGERANGE.name) citation.PageRange = row[x];
                if (column.MappedColumn == MappedColumn.ARTICLEENDPAGE.name) citation.EndPage = row[x];
                if (column.MappedColumn == MappedColumn.ARTICLESTARTPAGE.name) citation.StartPage = row[x];
                if (column.MappedColumn == MappedColumn.ARTICLETITLE.name) articleTitle = row[x];

                if (column.MappedColumn == MappedColumn.AUTHORNAMES.name)
                {
                    string[] authors = string.IsNullOrWhiteSpace(column.ValueDelimiter) ?
                        new string[] { row[x] } :
                        row[x].Split(column.ValueDelimiter.ToCharArray());

                    foreach (string author in authors)
                    {
                        citation.Authors.Add(GetNewCreator(author));
                    }
                }

                if (column.MappedColumn == MappedColumn.BOOKJOURNALTITLE.name) bookJournalTitle = row[x];
                if (column.MappedColumn == MappedColumn.COPYRIGHTSTATUS.name) citation.CopyrightStatus = row[x];
                if (column.MappedColumn == MappedColumn.DOI.name) citation.DOI = row[x];
                if (column.MappedColumn == MappedColumn.DOWNLOADURL.name) citation.DownloadUrl = row[x];
                if (column.MappedColumn == MappedColumn.DUEDILIGENCE.name) citation.DueDiligence = row[x];
                if (column.MappedColumn == MappedColumn.EDITION.name) citation.Edition = row[x];
                if (column.MappedColumn == MappedColumn.GENRE.name) citation.Genre = row[x];
                if (column.MappedColumn == MappedColumn.ISBN.name) citation.ISBN = row[x];
                if (column.MappedColumn == MappedColumn.ISSN.name) citation.ISSN = row[x];
                if (column.MappedColumn == MappedColumn.ISSUE.name) citation.Issue = row[x];

                int year;
                if (column.MappedColumn == MappedColumn.JOURNALENDYEAR.name)
                {
                    if (Int32.TryParse(row[x], out year)) citation.EndYear = (short)year;
                }
                if (column.MappedColumn == MappedColumn.JOURNALSTARTYEAR.name)
                {
                    if (Int32.TryParse(row[x], out year)) citation.StartYear = (short)year;
                }

                if (column.MappedColumn == MappedColumn.KEYWORDS.name)
                {
                    string[] keywords = string.IsNullOrWhiteSpace(column.ValueDelimiter) ?
                        new string[] { row[x] } :
                        row[x].Split(column.ValueDelimiter.ToCharArray());

                    foreach (string keyword in keywords)
                    {
                        citation.Keywords.Add(GetNewKeyword(keyword));
                    }
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
                if (column.MappedColumn == MappedColumn.SERIES.name) citation.Series = row[x];
                if (column.MappedColumn == MappedColumn.TRANSLATEDTITLE.name) citation.TranslatedTitle = row[x];
                if (column.MappedColumn == MappedColumn.URL.name) citation.Url = row[x];
                if (column.MappedColumn == MappedColumn.VOLUME.name) citation.Volume = row[x];
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
            new BHLProvider().ImportRecordSave(citation, GetUserID(securityToken));
        }

        #endregion Database interactions

    }

    /// <summary>
    /// Model to hold the attributes of a column in a data file
    /// </summary>
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
    public class ImportRecordJson
    {
        public class Rootobject
        {
            public int sEcho { get; set; }
            public string iTotalRecords { get; set; }
            public string iTotalDisplayRecords { get; set; }
            public object[][] aaData { get; set; }
        }

    }
}
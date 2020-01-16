using MOBOT.BHL.AdminWeb.MVCServices;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.IO;

namespace MOBOT.BHL.AdminWeb.Models
{
    [Serializable]
    public class TextImportModel
    {
        private int _batchId = int.MinValue;

        public int BatchID
        {
            get { return _batchId; }
            set { _batchId = value; }
        }

        private int _batchStatusId = int.MinValue;

        public int BatchStatusID
        {
            get { return _batchStatusId; }
            set { _batchStatusId = value; }
        }

        private string _batchStatus = string.Empty;

        public string BatchStatus
        {
            get { return _batchStatus; }
            set { _batchStatus = value; }
        }

        private string _creationUser = string.Empty;

        public string CreationUser
        {
            get { return _creationUser; }
            set { _creationUser = value; }
        }

        private DateTime _creationDateTime;

        public DateTime CreationDateTime
        {
            get { return _creationDateTime; }
            set { _creationDateTime = value; }
        }

        private List<TextImportFileModel> _files = new List<TextImportFileModel>();

        public List<TextImportFileModel> Files
        {
            get { return _files; }
            set { _files = value; }
        }


        public void AddBatch(int userId)
        {
            BHLProvider provider = new BHLProvider();

            TextImportBatch batch = provider.TextImportBatchInsertAuto(
                TextImportService.GetTextImportBatchStatusNew(), userId);

            this.BatchID = batch.TextImportBatchID;

            foreach (TextImportFileModel file in Files)
            {
                file.AddBatchFile(this.BatchID, userId);
            }
        }

        public List<TextImportFileModel> GetFiles(int batchID)
        {
            List<TextImportFileModel> files = new List<TextImportFileModel>();
            List<TextImportBatchFile> batchFiles = new BHLProvider().TextImportBatchFileSelectForBatch(batchID);
            foreach(TextImportBatchFile batchFile in batchFiles)
            {
                TextImportFileModel file = new TextImportFileModel();
                file.FileID = batchFile.TextImportBatchFileID;
                file.FileStatusID = batchFile.TextImportBatchFileStatusID;
                file.FileStatus = batchFile.StatusName;
                file.FileName = batchFile.Filename;
                file.FilePath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["TextImportPath"], batchFile.Filename);
                file.FileFormat = batchFile.FileFormat;
                file.FileFormatName = new TextImportService().GetFileFormatValue(file.FileFormat);
                file.ErrorMessage = batchFile.ErrorMessage;
                file.ItemID = batchFile.ItemID.ToString();
                files.Add(file);
            }

            return files;
        }

        /// <summary>
        /// Get files from the specified text import batch.
        /// </summary>
        /// <param name="batchID">Identifier of the import batch</param>
        /// <param name="numRows">Number of rows to return</param>
        /// <param name="startRow">First row to return (enables paging)</param>
        /// <param name="sortColumn">Column by which to sort data</param>
        /// <param name="sortDirection">Direction of sort</param>
        public TextImportBatchFileJson.Rootobject GetFiles(int batchID, int numRows, int startRow, string sortColumn, string sortDirection)
        {
            List<TextImportBatchFile> files = new BHLProvider().TextImportBatchFileDetailSelectForBatch(batchID,
                numRows, startRow, sortColumn, sortDirection);

            TextImportBatchFileJson.Rootobject json = new TextImportBatchFileJson.Rootobject();
            json.iTotalRecords = (files.Count == 0) ? "0" : files[0].TotalFiles.ToString();
            json.iTotalDisplayRecords = json.iTotalRecords;

            TextImportBatchFileJson.Datum[] aaData = new TextImportBatchFileJson.Datum[files.Count];

            for (int x = 0; x < files.Count; x++)
            {
                aaData[x] = new TextImportBatchFileJson.Datum()
                {
                    // Summary
                    id = files[x].TextImportBatchFileID.ToString(),
                    filename = files[x].Filename,
                    fileformat = new TextImportService().GetFileFormatValue(files[x].FileFormat),
                    itemid = files[x].ItemID.ToString(),
                    itemdesc = files[x].ItemDescription,
                    status = files[x].StatusName,
                    errormessage = files[x].ErrorMessage
                };
            }
            json.aaData = aaData;

            return json;
        }

        public void GetImportBatchDetails(int batchID)
        {
            BHLProvider provider = new BHLProvider();
            TextImportBatch batch = provider.TextImportBatchSelectExpanded(batchID);

            this.BatchID = batchID;
            this.BatchStatusID = batch.TextImportBatchStatusID;
            this.BatchStatus = batch.StatusName;
            this.CreationUser = batch.CreationUser;
            this.CreationDateTime = batch.CreationDate;
            this.Files = this.GetFiles(batchID);            
        }

        public void QueueBatch(int userID)
        {
            new BHLProvider().TextImportBatchUpdateStatus(this.BatchID, TextImportService.GetTextImportBatchStatusQueued(), userID);
        }

        public void RejectBatch(int userID)
        {
            new BHLProvider().TextImportBatchUpdateStatus(this.BatchID, TextImportService.GetTextImportBatchStatusRejected(), userID);
        }
    }

    [Serializable]
    public class TextImportFileModel
    {
        private int _fileID = int.MinValue;

        public int FileID
        {
            get { return _fileID; }
            set { _fileID = value; }
        }

        private int _fileStatusID = int.MinValue;

        public int FileStatusID
        {
            get { return _fileStatusID; }
            set { _fileStatusID = value; }
        }

        private string _fileStatus = string.Empty;

        public string FileStatus
        {
            get { return _fileStatus; }
            set { _fileStatus = value; }
        }

        private string _itemID = string.Empty;

        public string ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
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

        private string _fileFormat = string.Empty;

        public string FileFormat
        {
            get { return _fileFormat; }
            set { _fileFormat = value; }
        }

        private string _fileFormatName = string.Empty;

        public string FileFormatName
        {
            get { return _fileFormatName; }
            set { _fileFormatName = value; }
        }

        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public void AddBatchFile(int batchId, int userId)
        {
            BHLProvider provider = new BHLProvider();

            int batchFileStatus = TextImportService.GetTextImportBatchFileStatusReady();
            string errorMessage = string.Empty;
            int itemIDInt;
            if (!Int32.TryParse(this.ItemID, out itemIDInt))
            {
                // Invalid Item ID format
                batchFileStatus = TextImportService.GetTextImportBatchFileStatusError();
                errorMessage += "Invalid Item ID.  Make sure the filename matches a BHL item identifier. | ";
            }
            else if (provider.ItemSelectAuto(Convert.ToInt32(this.ItemID)) == null)
            {
                // Item ID not found in BHL
                itemIDInt = 0;
                this.ItemID = string.Empty;
                batchFileStatus = TextImportService.GetTextImportBatchFileStatusError();
                errorMessage += "Invalid Item ID.  Make sure the filename matches a BHL item identifier. | ";
            }
            else if (provider.PageTextLogSelectForItem(Convert.ToInt32(this.ItemID)).Count > 0)
            {
                // Text to be replaced NOT from IA, so user must review and approve replacement
                batchFileStatus = TextImportService.GetTextImportBatchFileStatusReview();
            }

            if (string.IsNullOrWhiteSpace(this.FileFormat))
            {
                // No valid Item ID
                batchFileStatus = TextImportService.GetTextImportBatchFileStatusError();
                errorMessage += "Invalid file format.  Could not determine the format of the file. | ";
            }

            TextImportBatchFile file = provider.TextImportBatchFileInsertAuto(batchId, 
                batchFileStatus, (itemIDInt == 0 ?  (int?)null : itemIDInt), this.FileName, 
                this.FileFormat, errorMessage, userId);
        }

        /// <summary>
        /// Update the specified import record with the specified import record status.
        /// </summary>
        /// <param name="importRecordID"></param>
        /// <param name="importRecordStatusID"></param>
        /// <param name="securityToken"></param>
        /// <returns>The name of the new status.</returns>
        public string UpdateFileStatus(int fileID, int fileStatusID, int userId)
        {
            BHLProvider bhlService = new BHLProvider();
            TextImportBatchFile updatedFile = bhlService.TextImportBatchFileUpdateStatus(fileID, fileStatusID, userId);
            return TextImportService.TextImportBatchFileStatuses[updatedFile.TextImportBatchFileStatusID];
        }

        public List<Page> GetItemPages(int itemID)
        {
            List<Page> pages = new BHLProvider().PageSelectByItemID(itemID);
            return pages;
        }
    }

    /// <summary>
    /// Class used to produce the JSON representation of batch files that is needed by jQuery DataTables
    /// </summary>
    [Serializable]
    public class TextImportBatchFileJson
    {
        public class Rootobject
        {
            public int sEcho { get; set; }
            public string iTotalRecords { get; set; }
            public string iTotalDisplayRecords { get; set; }
            public Datum[] aaData { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string origfilename { get; set; }
            public string filename { get; set; }
            public string fileformat { get; set; }
            public string itemid { get; set; }
            public string itemdesc { get; set; }
            public string status { get; set; }
            public string errormessage { get; set; }
        }
    }
}
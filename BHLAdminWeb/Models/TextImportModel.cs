using CustomDataAccess;
using MOBOT.BHL.AdminWeb.MVCServices;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

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
            CustomGenericList<TextImportBatchFile> batchFiles = new BHLProvider().TextImportBatchFileSelectForBatch(batchID);
            foreach(TextImportBatchFile batchFile in batchFiles)
            {
                TextImportFileModel file = new TextImportFileModel();
                file.FileID = batchFile.TextImportBatchFileID;
                file.FileStatusID = batchFile.TextImportBatchFileStatusID;
                file.FileStatus = batchFile.StatusName;
                file.FileName = batchFile.Filename;
                file.FilePath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["TextImportPath"], file.FileName);
                file.FileFormat = batchFile.FileFormat;
                file.FileFormatName = new TextImportService().GetFileFormatValue(file.FileFormat);
                file.ItemID = batchFile.ItemID.ToString();
                files.Add(file);
            }

            return files;
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
            new BHLProvider().TextImportBatchUpdateStatus(this.BatchID, TextImportService.GetTextImportBatchStatusQueued());
        }

        public void RejectBatch(int userID)
        {
            new BHLProvider().TextImportBatchUpdateStatus(this.BatchID, TextImportService.GetTextImportBatchStatusRejected());
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

        public void AddBatchFile(int batchId, int userId)
        {
            BHLProvider provider = new BHLProvider();

            int batchFileStatus;
            if (this.ItemID == "")
            {
                // No valid Item ID
                batchFileStatus = TextImportService.GetTextImportBatchFileStatusError();
            }
            else if (provider.PageTextLogSelectForItem(Convert.ToInt32(this.ItemID)).Count > 0)
            {
                // Text to be replaced NOT from IA, so user must review and approve replacement
                batchFileStatus = TextImportService.GetTextImportBatchFileStatusReview();
            }
            else
            {
                // Validations passed; ok to import this file
                batchFileStatus = TextImportService.GetTextImportBatchFileStatusReady();
            }

            TextImportBatchFile file = provider.TextImportBatchFileInsertAuto(batchId, 
                batchFileStatus, this.ItemID == "" ? (int?)null : Convert.ToInt32(this.ItemID), 
                this.FileName, this.FileFormat, userId);
        }
    }
}
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public TextImportBatchFile TextImportBatchFileSelectAuto(int textImportBatchFileID)
        {
            return new TextImportBatchFileDAL().TextImportBatchFileSelectAuto(null, null, textImportBatchFileID);
        }

        public TextImportBatchFile TextImportBatchFileInsertAuto(int textImportBatchID, int textImportBatchFileStatusID, int? itemID,
            string fileName, string fileFormat, string errorMessage, int userID)
        {
            return new TextImportBatchFileDAL().TextImportBatchFileInsertAuto(null, null, textImportBatchID, textImportBatchFileStatusID, 
                itemID, fileName, fileFormat, errorMessage, userID, userID);
        }

        public TextImportBatchFile TextImportBatchFileUpdateAuto(TextImportBatchFile textImportBatchFile)
        {
            return new TextImportBatchFileDAL().TextImportBatchFileUpdateAuto(null, null, textImportBatchFile);
        }

        public CustomGenericList<TextImportBatchFileStatus> TextImportBatchFileStatusSelectAll()
        {
            return new TextImportBatchFileStatusDAL().SelectAll(null, null);
        }

        public CustomGenericList<TextImportBatchFile> TextImportBatchFileSelectForBatch(int batchID)
        {
            return new TextImportBatchFileDAL().TextImportBatchFileSelectForBatch(null, null, batchID);
        }

        public CustomGenericList<TextImportBatchFile> TextImportBatchFileDetailSelectForBatch(int batchID,
            int numRows, int startRow, string sortColumn, string sortDirection)
        {
            return new TextImportBatchFileDAL().TextImportBatchFileDetailSelectForBatch(null, null, batchID, numRows, startRow, sortColumn, sortDirection);
        }

        public CustomGenericList<TextImportBatchFile> TextImportBatchFileSelectForProcessing(int batchID)
        {
            return new TextImportBatchFileDAL().TextImportBatchFileSelectForProcessing(null, null, batchID);
        }

        public TextImportBatchFile TextImportBatchFileUpdate(int fileID, int fileStatusID, int? itemID, string fileName,
            string fileFormat, string errorMessage, int userID)
        {
            TextImportBatchFileDAL dal = new TextImportBatchFileDAL();
            TextImportBatchFile savedFile = dal.TextImportBatchFileSelectAuto(null, null, fileID);
            if (savedFile != null)
            {
                savedFile.TextImportBatchFileStatusID = fileStatusID;
                savedFile.ItemID = itemID;
                savedFile.Filename = fileName;
                savedFile.FileFormat = fileFormat;
                savedFile.ErrorMessage = errorMessage;
                savedFile.LastModifiedDate = DateTime.Now;
                savedFile.LastModifiedUserID = userID;
                savedFile = dal.TextImportBatchFileUpdateAuto(null, null, savedFile);
            }
            else
            {
                throw new Exception("Could not find existing TextImportBatchFile.");
            }
            return savedFile;
        }

        public TextImportBatchFile TextImportBatchFileUpdateStatus(int fileID, int fileStatusID, int userID)
        {
            TextImportBatchFileDAL dal = new TextImportBatchFileDAL();
            TextImportBatchFile savedFile = dal.TextImportBatchFileSelectAuto(null, null, fileID);
            if (savedFile != null)
            {
                savedFile.TextImportBatchFileStatusID = fileStatusID;
                savedFile.LastModifiedDate = DateTime.Now;
                savedFile.LastModifiedUserID = userID;
                savedFile = dal.TextImportBatchFileUpdateAuto(null, null, savedFile);
            }
            else
            {
                throw new Exception("Could not find existing TextImportBatchFile.");
            }
            return savedFile;
        }
    }
}

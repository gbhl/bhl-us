using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public TextImportBatch TextImportBatchSelectAuto(int textImportBatchID)
        {
            return new TextImportBatchDAL().TextImportBatchSelectAuto(null, null, textImportBatchID);
        }

        public TextImportBatch TextImportBatchInsertAuto(int textImportBatchStatusID, int userID)
        {
            return new TextImportBatchDAL().TextImportBatchInsertAuto(null, null, textImportBatchStatusID, userID, userID);
        }

        public TextImportBatch TextImportBatchUpdateAuto(TextImportBatch textImportBatch)
        {
            return new TextImportBatchDAL().TextImportBatchUpdateAuto(null, null, textImportBatch);
        }

        public List<TextImportBatch> TextImportBatchSelectDetails(int fileStatusID, int numberOfDays)
        {
            return new TextImportBatchDAL().TextImportBatchSelectDetails(null, null, fileStatusID, numberOfDays);
        }

        public TextImportBatch TextImportBatchSelectExpanded(int batchID)
        {
            return new TextImportBatchDAL().TextImportBatchSelectExpanded(null, null, batchID);
        }

        public List<TextImportBatch> TextImportBatchSelectForFileCreation()
        {
            return new TextImportBatchDAL().TextImportBatchSelectForFileCreation(null, null);
        }

        public List<TextImportBatchStatus> TextImportBatchStatusSelectAll()
        {
            return new TextImportBatchStatusDAL().SelectAll(null, null);
        }

        public TextImportBatch TextImportBatchUpdateStatus(int batchID, int statusID, int userID)
        {
            TextImportBatchDAL dal = new TextImportBatchDAL();
            TextImportBatch savedBatch = dal.TextImportBatchSelectAuto(null, null, batchID);
            if (savedBatch != null)
            {
                savedBatch.TextImportBatchStatusID = statusID;
                savedBatch.LastModifiedDate = DateTime.Now;
                savedBatch.LastModifiedUserID = userID;
                savedBatch = dal.TextImportBatchUpdateAuto(null, null, savedBatch);
            }
            else
            {
                throw new Exception("Could not find existing TextImportBatch.");
            }
            return savedBatch;
        }
    }
}

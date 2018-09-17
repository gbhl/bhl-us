﻿using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

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

        public CustomGenericList<TextImportBatch> TextImportBatchSelectDetails(int fileStatusID, int numberOfDays)
        {
            return new TextImportBatchDAL().TextImportBatchSelectDetails(null, null, fileStatusID, numberOfDays);
        }

        public CustomGenericList<TextImportBatchStatus> TextImportBatchStatusSelectAll()
        {
            return new TextImportBatchStatusDAL().SelectAll(null, null);
        }
    }
}

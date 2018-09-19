﻿using CustomDataAccess;
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
            string fileName, string fileFormat, int userID)
        {
            return new TextImportBatchFileDAL().TextImportBatchFileInsertAuto(null, null, textImportBatchID, textImportBatchFileStatusID, 
                itemID, fileName, fileFormat, userID, userID);
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
    }
}

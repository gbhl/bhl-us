using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public MarcImportBatch MarcImportBatchInsertAuto(String fileName, String institutionCode)
        {
            return new MarcImportBatchDAL().MarcImportBatchInsertAuto(null, null, fileName, institutionCode);
        }

        public bool MarcImportBatchDeleteAuto(int batchID)
        {
            return new MarcImportBatchDAL().MarcImportBatchDeleteAuto(null, null, batchID);
        }

        public List<MarcImportBatch> MarcImportBatchSelectStatsByInstitution(String institutionCode)
        {
            return new MarcImportBatchDAL().MarcImportBatchSelectStatsByInstitution(null, null, institutionCode);
        }
    }
}

using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

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

        public CustomGenericList<MarcImportBatch> MarcImportBatchSelectStatsByInstitution(String institutionCode)
        {
            return new MarcImportBatchDAL().MarcImportBatchSelectStatsByInstitution(null, null, institutionCode);
        }
    }
}

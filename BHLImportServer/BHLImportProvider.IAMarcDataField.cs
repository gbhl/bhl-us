using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public void IAMarcDataFieldDeleteByMarcID(int marcID)
        {
            new IAMarcDataFieldDAL().IAMarcDataFieldDeleteByMarcID(null, null, marcID);
        }

        public IAMarcDataField IAMarcDataFieldInsertAuto(int marcID, string tag, string indicator1, string indicator2)
        {
            return (new IAMarcDataFieldDAL().IAMarcDataFieldInsertAuto(null, null, marcID, tag, indicator1, indicator2));
        }
    }
}

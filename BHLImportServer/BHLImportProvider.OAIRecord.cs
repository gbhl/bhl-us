using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;


namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public void OAIRecordDeleteForHarvestLogID(int harvestLogID)
        {
            new OAIRecordDAL().OAIRecordDeleteForHarvestLogID(null, null, harvestLogID);
        }
    }
}

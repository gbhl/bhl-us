using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public DateTime OAIHarvestLogSelectLastDateForHarvestSet(int harvestSetID)
        {
            return new OAIHarvestLogDAL().OAIHarvestLogSelectLastDateForHarvestSet(null, null, harvestSetID);
        }
    }
}

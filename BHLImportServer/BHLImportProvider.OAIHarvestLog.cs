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

        public OAIHarvestLog OAIHarvestLogInsert(int harvestSetID, DateTime? harvestStartDateTime, DateTime? fromDate,
            DateTime? untilDate, DateTime? responseDateTime, string result, int numberHarvested)
        {
            return new OAIHarvestLogDAL().OAIHarvestLogInsertAuto(null, null, harvestSetID, harvestStartDateTime, fromDate, 
                untilDate, responseDateTime, result, numberHarvested);
        }
    }
}

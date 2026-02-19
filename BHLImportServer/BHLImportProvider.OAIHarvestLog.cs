using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public DateTime OAIHarvestLogSelectLastDateForHarvestSet(int harvestSetID)
        {
            return new OAIHarvestLogDAL().OAIHarvestLogSelectLastDateForHarvestSet(null, null, harvestSetID);
        }

        public List<OAIHarvestLog> OAIHarvestLogSelectWithNewRecords()
        {
            return new OAIHarvestLogDAL().OAIHarvestLogSelectWithNewRecords(null, null);
        }

        public OAIHarvestLog OAIHarvestLogInsert(int harvestSetID, DateTime? harvestStartDateTime, DateTime? fromDate,
            DateTime? untilDate, DateTime? responseDateTime, string result, int numberHarvested)
        {
            return new OAIHarvestLogDAL().OAIHarvestLogInsertAuto(null, null, harvestSetID, harvestStartDateTime, fromDate, 
                untilDate, responseDateTime, result, numberHarvested);
        }

        public void OAIHarvestLogUpdate(int harvestLogID, int harvestSetID, DateTime? harvestStartDateTime, 
            DateTime? fromDate, DateTime? untilDate, DateTime? responseDateTime, string result, int numberHarvested)
        {
            new OAIHarvestLogDAL().OAIHarvestLogUpdateAuto(null, null, harvestLogID, harvestSetID, harvestStartDateTime,
                fromDate, untilDate, responseDateTime, result, numberHarvested);
        }
    }
}

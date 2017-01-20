using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHL.Utility;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public void OAIRecordDeleteForHarvestLogID(int harvestLogID)
        {
            new OAIRecordDAL().OAIRecordDeleteForHarvestLogID(null, null, harvestLogID);
        }

        public void Save(OAIRecord oaiRecord)
        {
            // Clean up the date values before saving
            oaiRecord.Date = DataCleaner.CleanYear(oaiRecord.Date);
            new OAIRecordDAL().Save(null, null, oaiRecord);
        }
    }
}

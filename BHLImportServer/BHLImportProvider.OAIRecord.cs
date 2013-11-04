using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public void Save(OAIRecord oaiRecord)
        {
			new OAIRecordDAL().Save(null, null, oaiRecord);
        }
    }
}

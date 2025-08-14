using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public void WDEntityIdentifierDeleteByEntityType(string entityType)
        {
            new WDEntityIdentifierDAL().WDEntityIdentifierDeleteByEntityType(null, null, entityType);
        }

        public void WDEntityIdentifierInsert(string entityType, int entityId, 
            string identifierType, string identifierValue, DateTime harvestDate)
        {
            new WDEntityIdentifierDAL().WDEntityIdentifierInsert(null, null, 
                entityType, entityId, identifierType, identifierValue, harvestDate);
        }

        public List<WDEntityIdentifier> WDEntityIdentifierPublishAuthorIDs()
        {
            return new WDEntityIdentifierDAL().WDEntityIdentifierPublishAuthorIDs(null, null);
        }

        public List<WDEntityIdentifier> WDEntityIdentifierPublishTitleIDs()
        {
            return new WDEntityIdentifierDAL().WDEntityIdentifierPublishTitleIDs(null, null);
        }

        public List<WDEntityIdentifier> WDEntityIdentifierSelectNeedReview()
        {
            return new WDEntityIdentifierDAL().WDEntityIdentifierSelectNeedReview(null, null);
        }
    }
}

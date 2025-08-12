using MOBOT.BHLImport.DAL;
using System;

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

        public void WDEntityIdentifierPublishAuthorIDs()
        {
            new WDEntityIdentifierDAL().WDEntityIdentifierPublishAuthorIDs(null, null);
        }

        public void WDEntityIdentifierPublishTitleIDs()
        {
            new WDEntityIdentifierDAL().WDEntityIdentifierPublishTitleIDs(null, null);
        }
    }
}

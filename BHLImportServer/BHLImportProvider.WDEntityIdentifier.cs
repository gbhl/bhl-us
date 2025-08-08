using MOBOT.BHLImport.DAL;
using System;
using System.Net.NetworkInformation;

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
    }
}

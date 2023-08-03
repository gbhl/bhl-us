using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public void IAItemIdentifierDeleteByItem(int itemID)
        {
            new IAItemIdentifierDAL().IAItemIdentifierDeleteByItem(null, null, itemID);
        }

        public IAItemIdentifier SaveIAItemIdentifier(int itemID, string idDescription, string idValue)
        {
            IAItemIdentifierDAL dal = new IAItemIdentifierDAL();
            IAItemIdentifier savedItemIdentifier = dal.IAItemIdentifierSelect(null, null, itemID, idDescription, idValue) ?? 
                dal.IAItemIdentifierInsertAuto(null, null, itemID, idDescription, idValue);
            return savedItemIdentifier;
        }
    }
}

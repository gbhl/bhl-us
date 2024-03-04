using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public IABHLCreator IABHLCreatorInsertAuto(int itemID, string name)
        {
            return (new IABHLCreatorDAL().IABHLCreatorInsertAuto(null, null, itemID, name));
        }

        public IABHLCreatorIdentifier IABHLCreatorIdentifierInsertAuto(int iaBHLCreatorID, string type, string value)
        {
            return (new IABHLCreatorIdentifierDAL().IABHLCreatorIdentifierInsertAuto(null, null, iaBHLCreatorID, type, value));
        }

        public void IABHLCreatorDeleteByItem(int itemID)
        {
            new IABHLCreatorDAL().IABHLCreatorDeleteByItem(null, null, itemID);
        }
    }
}

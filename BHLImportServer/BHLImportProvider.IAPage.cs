using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public void IAPageDeleteByItem(int itemID)
        {
            new IAPageDAL().IAPageDeleteByItem(null, null, itemID);
        }

        public IAPage IAPageInsertAuto(int itemID, string localFileName, int? sequence, string externalUrl)
        {
            return (new IAPageDAL().IAPageInsertAuto(null, null, itemID, localFileName, sequence, externalUrl));
        }
    }
}

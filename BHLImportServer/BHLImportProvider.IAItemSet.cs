using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public void IAItemSetDeleteByItem(int itemID)
        {
            new IAItemSetDAL().IAItemSetDeleteByItem(null, null, itemID);
        }

        public IAItemSet IAItemSetInsertAuto(int itemID, int setID)
        {
            IAItemSet newItemSet = new IAItemSet
            {
                ItemID = itemID,
                SetID = setID
            };
            return (new IAItemSetDAL().IAItemSetInsertAuto(null, null, newItemSet));
        }

        /// <summary>
        /// Save a new ItemSet record if one does not already exist for the specified
        /// item and set identifiers.
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="setID"></param>
        /// <returns></returns>
        public IAItemSet SaveIAItemSet(int itemID, int setID)
        {
            IAItemSetDAL dal = new IAItemSetDAL();
            IAItemSet savedItemSet = dal.IAItemSetSelectAuto(null, null, itemID, setID) ?? this.IAItemSetInsertAuto(itemID, setID);
            return savedItemSet;
        }

    }
}

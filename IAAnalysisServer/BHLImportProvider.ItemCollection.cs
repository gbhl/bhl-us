using System;
using CustomDataAccess;
using MOBOT.IAAnalysis.DAL;
using MOBOT.IAAnalysis.DataObjects;

namespace MOBOT.IAAnalysis.Server
{
    public partial class IAAnalysisProvider
    {
        public ItemCollection ItemCollectionInsertAuto(int itemID, int collectionID)
        {
            ItemCollection newItemCollection = new ItemCollection();
            newItemCollection.ItemID = itemID;
            newItemCollection.CollectionID = collectionID;
            return (new ItemCollectionDAL().ItemCollectionInsertAuto(null, null, newItemCollection));
        }

        /// <summary>
        /// Save a new ItemCollection record if one does not already exist for the specified
        /// item and set identifiers.
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="collectionID"></param>
        /// <returns></returns>
        public ItemCollection SaveItemCollection(int itemID, int collectionID)
        {
            ItemCollectionDAL dal = new ItemCollectionDAL();
            ItemCollection savedItemCollection = dal.ItemCollectionSelectAuto(null, null, itemID, collectionID);

            if (savedItemCollection == null)
            {
                savedItemCollection = this.ItemCollectionInsertAuto(itemID, collectionID);
            }
            return savedItemCollection;
        }

    }
}

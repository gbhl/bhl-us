using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public bool ItemCollectionDeleteForCollection(int collectionID)
        {
            return new ItemCollectionDAL().ItemCollectionDeleteForCollection(null, null, collectionID);
        }

        public int ItemCountByCollection(int collectionID)
        {
            return new ItemCollectionDAL().ItemCountByCollection(null, null, collectionID);
        }

        public bool ItemCollectionInsertItemsForCollection(int collectionID)
        {
            return new ItemCollectionDAL().ItemCollectionInsertItemsForCollection(null, null, collectionID);
        }

        public ItemCollection ItemCollectionInsert(int itemID, int collectionID)
        {
            ItemCollectionDAL dal = new ItemCollectionDAL();
            ItemCollection itemCollection = null;

            List<ItemCollection> items = dal.ItemCollectionSelectByItemAndCollection(null, null, itemID, collectionID);
            if (items.Count == 0) itemCollection = dal.ItemCollectionInsertAuto(null, null, itemID, collectionID);

            return itemCollection;
        }
    }
}

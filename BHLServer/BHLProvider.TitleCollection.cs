using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public bool TitleCollectionDeleteForCollection(int collectionID)
        {
            return new TitleCollectionDAL().TitleCollectionDeleteForCollection(null, null, collectionID);
        }

        public int TitleCountByCollection(int collectionID)
        {
            return new TitleCollectionDAL().TitleCountByCollection(null, null, collectionID);
        }

        public bool TitleCollectionInsertTitlesForCollection(int collectionID)
        {
            return new TitleCollectionDAL().TitleCollectionInsertTitlesForCollection(null, null, collectionID);
        }

        /// <summary>
        /// Insert the specified title into the specified collection if it is not already there.
        /// </summary>
        /// <param name="titleID"></param>
        /// <param name="collectionID"></param>
        /// <returns></returns>
        public TitleCollection TitleCollectionInsert(int titleID, int collectionID)
        {
            TitleCollectionDAL dal = new TitleCollectionDAL();
            TitleCollection titleCollection = null;

            List<TitleCollection> titles = dal.TitleCollectionSelectByTitleAndCollection(null, null, titleID, collectionID);
            if (titles.Count == 0) titleCollection = dal.TitleCollectionInsertAuto(null, null, titleID, collectionID);

            return titleCollection;
        }
    }
}

using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public Collection CollectionSelectAuto(int collectionID)
        {
            return new CollectionDAL().CollectionSelectAuto(null, null, collectionID);
        }

        public CustomGenericList<Collection> CollectionSelectAll()
        {
            return new CollectionDAL().SelectAll(null, null);
        }

        public CustomGenericList<Collection> CollectionSelectActive()
        {
            return new CollectionDAL().SelectActive(null, null);
        }

        public Collection CollectionSelectFeatured()
        {
            return new CollectionDAL().SelectFeatured(null, null);
        }

        public CustomGenericList<Collection> CollectionSelectByContents(int canContainTitles, int canContainItems)
        {
            return new CollectionDAL().SelectByContents(null, null, canContainTitles, canContainItems);
        }

        public CustomGenericList<Collection> CollectionSelectByNameAndAllowedContent(string collectionName,
            short canContainTitles, short canContainItems)
        {
            return new CollectionDAL().CollectionSelectByNameAndAllowedContent(null, null, collectionName,
                canContainTitles, canContainItems);
        }

        public CustomGenericList<Collection> CollectionSelectByUrl(string collectionUrl)
        {
            return new CollectionDAL().CollectionSelectByUrl(null, null, collectionUrl);
        }

        public CustomGenericList<Collection> CollectionSelectByNameLike(string collectionName)
        {
            return new CollectionDAL().CollectionSelectByNameLike(null, null, collectionName);
        }

        public CustomGenericList<Collection> CollectionSelectAllForTitle(int titleID)
        {
            return new CollectionDAL().CollectionSelectAllForTitle(null, null, titleID);
        }

        public void SaveCollection(Collection collection)
        {
            new CollectionDAL().Save(null, null, collection);
        }

        public void DeleteCollection(int collectionID)
        {
            new CollectionDAL().Delete(null, null, collectionID);
        }
    }
}

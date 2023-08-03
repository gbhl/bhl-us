using System;
using CustomDataAccess;
using MOBOT.IAAnalysis.DAL;
using MOBOT.IAAnalysis.DataObjects;

namespace MOBOT.IAAnalysis.Server
{
    public partial class IAAnalysisProvider
    {
        public Collection SaveCollection(string collectionName)
        {
            CollectionDAL dal = new CollectionDAL();
            Collection savedCollection = dal.CollectionSelectByCollectionName(null, null, collectionName);
            if (savedCollection == null)
            {
                // Add the new set
                Collection newCollection = new Collection
                {
                    CollectionName = collectionName
                };
                savedCollection = dal.CollectionInsertAuto(null, null, newCollection);
            }
            return savedCollection;
        }
    }
}

using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<DisqusCache> DisqusCacheSelectByItemID(int itemID)
        {
            return (new DisqusCacheDAL().DisqusCacheSelectByItemID(null, null, "BHL", itemID));
        }

        public bool DisqusCacheDeleteByItemID(int itemID)
        {
            return (new DisqusCacheDAL().DisqusCacheDeleteByItemID(null, null, itemID));
        }

        public DisqusCache DisqusCacheInsertAuto(DisqusCache cache)
        {
            return (new DisqusCacheDAL().DisqusCacheInsertAuto(null,null,cache));
        }

    }
}

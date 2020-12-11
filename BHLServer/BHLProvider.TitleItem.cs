using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<ItemTitle> ItemTitleSelectByItem(int itemID)
        {
            return (new ItemTitleDAL().ItemTitleSelectByItem(null, null, itemID));
        }

    }
}

using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<TitleItem> TitleItemSelectByItem(int itemID)
        {
            return (new TitleItemDAL().TitleItemSelectByItem(null, null, itemID));
        }

    }
}

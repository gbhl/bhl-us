using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<ItemTitle> ItemTitleSelectByItem(int itemID)
        {
            return (new ItemTitleDAL().ItemTitleSelectByItem(null, null, itemID));
        }

    }
}

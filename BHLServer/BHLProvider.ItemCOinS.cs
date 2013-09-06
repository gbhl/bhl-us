using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public ItemCOinSView ItemCOinSSelectByTitleId(int titleId)
        {
            return (new ItemCOinSDAL().ItemCOinSSelectByTitleId(null, null, titleId));
        }

        public ItemCOinSView ItemCOinSSelectByItemId(int itemId)
        {
            return (new ItemCOinSDAL().ItemCOinSSelectByItemId(null, null, itemId));
        }
    }
}

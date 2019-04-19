using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<EditHistory> EditHistorySelectByItemID(int itemID)
        {
            return new EditHistoryDAL().EditHistorySelectByItemID(null, null, itemID);
        }
    }
}

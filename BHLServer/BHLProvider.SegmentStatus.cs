using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<ItemStatus> SegmentStatusSelectAll()
        {
            return (new SegmentStatusDAL().SegmentStatusSelectAll(null, null));
        }
    }
}

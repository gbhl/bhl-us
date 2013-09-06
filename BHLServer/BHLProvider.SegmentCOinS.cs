using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public SegmentCOinSView SegmentCOinSSelectBySegmentId(int segmentId)
        {
            return (new SegmentCOinSDAL().SegmentCOinSSelectBySegmentId(null, null, segmentId));
        }
    }
}

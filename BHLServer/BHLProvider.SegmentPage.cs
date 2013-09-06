using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;
namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<SegmentPage> SegmentPageSelectBySegmentID(int segmentID)
        {
            return new SegmentPageDAL().SegmentPageSelectBySegmentID(null, null, segmentID);
        }
    }
}

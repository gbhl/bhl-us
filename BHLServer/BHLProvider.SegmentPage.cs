using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<ItemPage> ItemPageSelectBySegmentID(int segmentID)
        {
            return new ItemPageDAL().ItemPageSelectBySegmentID(null, null, segmentID);
        }
    }
}

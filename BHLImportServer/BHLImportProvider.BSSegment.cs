using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<BSSegment> BSSegmentSelectByItem(int itemId)
        {
            return new BSSegmentDAL().BSSegmentSelectByItem(null, null, itemId);
        }
    }
}

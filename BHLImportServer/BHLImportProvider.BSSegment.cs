using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public CustomGenericList<BSSegment> BSSegmentSelectByItem(int itemId)
        {
            return new BSSegmentDAL().BSSegmentSelectByItem(null, null, itemId);
        }
    }
}

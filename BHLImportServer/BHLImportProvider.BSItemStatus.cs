using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<BSItemStatus> BSItemStatusSelectAll()
        {
            return new BSItemStatusDAL().BSItemStatusSelectAll(null, null);
        }
    }
}

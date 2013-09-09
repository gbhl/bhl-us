using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public CustomGenericList<BSItemStatus> BSItemStatusSelectAll()
        {
            return new BSItemStatusDAL().BSItemStatusSelectAll(null, null);
        }
    }
}

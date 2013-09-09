using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public CustomGenericList<IAItemStatus> IAItemStatusSelectAll()
        {
            return new IAItemStatusDAL().IAItemStatusSelectAll(null, null);
        }
    }
}

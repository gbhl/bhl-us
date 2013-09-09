using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public CustomGenericList<ImportLog> ImportLogSelectRecent(int numLogs)
        {
            return (new ImportLogDAL().ImportLogSelectRecent(null, null, numLogs));
        }
    }
}

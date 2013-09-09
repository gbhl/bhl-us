using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public CustomGenericList<ImportError> ImportErrorSelectRecent(int numErrors)
        {
            return (new ImportErrorDAL().ImportErrorSelectRecent(null, null, numErrors));
        }
    }
}

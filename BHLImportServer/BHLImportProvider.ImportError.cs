using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<ImportError> ImportErrorSelectRecent(int numErrors)
        {
            return (new ImportErrorDAL().ImportErrorSelectRecent(null, null, numErrors));
        }
    }
}

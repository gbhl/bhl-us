using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<IAItemError> IAItemErrorSelectRecent(int numErrors)
        {
            return (new IAItemErrorDAL().IAItemErrorSelectRecent(null, null, numErrors));
        }
    }
}

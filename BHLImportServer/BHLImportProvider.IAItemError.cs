using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public CustomGenericList<IAItemError> IAItemErrorSelectRecent(int numErrors)
        {
            return (new IAItemErrorDAL().IAItemErrorSelectRecent(null, null, numErrors));
        }
    }
}

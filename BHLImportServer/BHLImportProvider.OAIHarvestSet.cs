using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<vwOAIHarvestSet> OAIHarvestSetSelectAll(bool onlyActive = true)
        {
            return new OAIHarvestSetDAL().OAIHarvestSetSelectAll(null, null, (onlyActive ? 1 : 0));
        }
    }
}

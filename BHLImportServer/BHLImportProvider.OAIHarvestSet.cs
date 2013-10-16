using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public CustomGenericList<vwOAIHarvestSet> OAIHarvestSetSelectAll(bool onlyActive = true)
        {
            return new OAIHarvestSetDAL().OAIHarvestSetSelectAll(null, null, (onlyActive ? 1 : 0));
        }
    }
}

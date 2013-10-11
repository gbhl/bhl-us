using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public CustomGenericList<vwOAIHarvestSet> OAIHarvestSetSelectAll()
        {
            return new OAIHarvestSetDAL().OAIHarvestSetSelectAll(null, null);
        }
    }
}

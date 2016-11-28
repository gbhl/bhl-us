using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<ReportOrphan> ReportSelectOrphanedEntities()
        {
            return new ReportDAL().ReportSelectOrphanedEntities(null, null);
        }
    }
}

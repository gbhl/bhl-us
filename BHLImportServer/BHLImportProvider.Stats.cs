using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<Stats> StatsSelectReadyForProductionBySource(int importSourceID)
        {
            return (new StatsDAL().StatsSelectReadyForProductionBySource(null, null, importSourceID));
        }

        public Stats StatsCountIAItemPendingApproval(int ageInDays)
        {
            return (new StatsDAL().StatsCountIAItemPendingApproval(null, null, ageInDays));
        }

        public List<Stats> StatsSelectIAItemGroupByStatus()
        {
            return (new StatsDAL().StatsSelectIAItemGroupByStatus(null, null));
        }

        public List<Stats> StatsSelectIAItemPendingApprovalGroupByAge(int ageInDays)
        {
            return (new StatsDAL().StatsSelectIAItemPendingApprovalGroupByAge(null, null, ageInDays));
        }

        public List<Stats> StatsSelectBSItemGroupByStatus()
        {
            return (new StatsDAL().StatsSelectBSItemGroupByStatus(null, null));
        }
    }
}

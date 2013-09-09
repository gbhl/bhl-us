using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public CustomGenericList<Stats> StatsSelectReadyForProductionBySource(int importSourceID)
        {
            return (new StatsDAL().StatsSelectReadyForProductionBySource(null, null, importSourceID));
        }

        public Stats StatsCountIAItemPendingApproval(int ageInDays)
        {
            return (new StatsDAL().StatsCountIAItemPendingApproval(null, null, ageInDays));
        }

        public CustomGenericList<Stats> StatsSelectIAItemGroupByStatus()
        {
            return (new StatsDAL().StatsSelectIAItemGroupByStatus(null, null));
        }

        public CustomGenericList<Stats> StatsSelectIAItemPendingApprovalGroupByAge(int ageInDays)
        {
            return (new StatsDAL().StatsSelectIAItemPendingApprovalGroupByAge(null, null, ageInDays));
        }

        public CustomGenericList<Stats> StatsSelectBSItemGroupByStatus()
        {
            return (new StatsDAL().StatsSelectBSItemGroupByStatus(null, null));
        }
    }
}

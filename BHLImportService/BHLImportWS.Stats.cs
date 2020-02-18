using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;
using System.Collections.Generic;
using System.Web.Services;

namespace MOBOT.BHLImport.WebService
{
    public partial class BHLImportWS : System.Web.Services.WebService
    {
        [WebMethod]
        public List<Stats> StatsSelectReadyForProductionBySource(int importSourceID)
        {
            return (new BHLImportProvider().StatsSelectReadyForProductionBySource(importSourceID));
        }

        [WebMethod]
        public Stats StatsCountIAItemPendingApproval(int ageInDays)
        {
            return (new BHLImportProvider().StatsCountIAItemPendingApproval(ageInDays));
        }

        [WebMethod]
        public List<Stats> StatsSelectIAItemGroupByStatus()
        {
            return (new BHLImportProvider().StatsSelectIAItemGroupByStatus());
        }

        [WebMethod]
        public List<Stats> StatsSelectIAItemPendingApprovalGroupByAge(int ageInDays)
        {
            return (new BHLImportProvider().StatsSelectIAItemPendingApprovalGroupByAge(ageInDays));
        }

        [WebMethod]
        public List<Stats> StatsSelectBSItemGroupByStatus()
        {
            return (new BHLImportProvider().StatsSelectBSItemGroupByStatus());
        }
    }
}

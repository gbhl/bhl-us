using System;
using System.Web.Services;
using System.ComponentModel;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;

namespace MOBOT.BHLImport.WebService
{
    public partial class BHLImportWS : System.Web.Services.WebService
    {
        [WebMethod]
        public CustomGenericList<Stats> StatsSelectReadyForProductionBySource(int importSourceID)
        {
            return (new BHLImportProvider().StatsSelectReadyForProductionBySource(importSourceID));
        }

        [WebMethod]
        public Stats StatsCountIAItemPendingApproval(int ageInDays)
        {
            return (new BHLImportProvider().StatsCountIAItemPendingApproval(ageInDays));
        }

        [WebMethod]
        public CustomGenericList<Stats> StatsSelectIAItemGroupByStatus()
        {
            return (new BHLImportProvider().StatsSelectIAItemGroupByStatus());
        }

        [WebMethod]
        public CustomGenericList<Stats> StatsSelectIAItemPendingApprovalGroupByAge(int ageInDays)
        {
            return (new BHLImportProvider().StatsSelectIAItemPendingApprovalGroupByAge(ageInDays));
        }

        [WebMethod]
        public CustomGenericList<Stats> StatsSelectBSItemGroupByStatus()
        {
            return (new BHLImportProvider().StatsSelectBSItemGroupByStatus());
        }
    }
}

using System;
using System.Web.Services;
using System.ComponentModel;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.WebService
{
    public partial class BHLWS : System.Web.Services.WebService
    {
        [WebMethod]
        public int GetPaginationStatusPending()
        {
            return PaginationStatus.Pending;
        }

        [WebMethod]
        public int GetPaginationStatusInProgress()
        {
            return PaginationStatus.InProgress;
        }

        [WebMethod]
        public int GetPaginationStatusComplete()
        {
            return PaginationStatus.Complete;
        }

        [WebMethod]
        public string GetPaginationStatusString(int? paginationStatusID)
        {
            return PaginationStatus.GetStatusString(paginationStatusID);
        }

    }
}
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
        public void Page_PageTypeSave(int[] pageIDs, int pageTypeID, int userID)
        {
            BHLProvider bhlServer = new BHLProvider();
            bhlServer.Page_PageTypeSave(pageIDs, pageTypeID, userID);
        }

        [WebMethod]
        public void Page_PageTypeDeleteAllForPage(int[] pageIDs, int userID)
        {
            BHLProvider bhlServer = new BHLProvider();
            bhlServer.Page_PageTypeDeleteAllForPage(pageIDs, userID);
        }
    }
}

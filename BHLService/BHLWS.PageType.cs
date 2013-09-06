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
        public CustomGenericList<PageType> PageTypeSelectAll()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageTypeSelectAll();
        }

    }
}

using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Collections.Generic;
using System.Web.Services;

namespace MOBOT.BHL.WebService
{
    public partial class BHLWS : System.Web.Services.WebService
    {
        [WebMethod]
        public List<PageType> PageTypeSelectAll()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageTypeSelectAll();
        }

    }
}

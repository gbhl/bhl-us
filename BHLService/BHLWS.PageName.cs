using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.WebService
{
    public partial class BHLWS
    {
        [WebMethod]
        public int[] PageNameUpdateList(int pageID, List<NameFinderResponse> items, string sourceName)
        {
            BHLProvider provider = new BHLProvider();
            return provider.PageNameUpdateList(pageID, items, sourceName);
        }

        [WebMethod]
        public void NamePageDeleteByItemID(int itemID)
        {
            BHLProvider provider = new BHLProvider();
            provider.NamePageDeleteByItemID(itemID);

        }
    }
}

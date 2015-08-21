using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.AdminWeb.BHLImportWebService;

namespace MOBOT.BHL.AdminWeb
{
    public partial class IAHarvestItemStatusAbout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BHLImportWSSoapClient ws = new BHLImportWSSoapClient();
                IAItemStatus[] statuses = ws.IAItemStatusSelectAll();

                dlStatus.DataSource = statuses;
                dlStatus.DataBind();
            }

        }
    }
}
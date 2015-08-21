using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
    public partial class DOIStatusAbout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BHLProvider service = new BHLProvider();
                CustomGenericList<DOIStatus> statuses = service.DOIStatusSelectAll();

                dlStatus.DataSource = statuses;
                dlStatus.DataBind();
            }
        }
    }
}
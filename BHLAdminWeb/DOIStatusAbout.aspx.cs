using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOBOT.BHL.AdminWeb
{
    public partial class DOIStatusAbout : System.Web.UI.Page
    {
        private const int DOISTATUS_EXTERNAL = 200;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BHLProvider service = new BHLProvider();
                List<DOIStatus> statuses = service.DOIStatusSelectAll();

                dlStatus.DataSource = statuses.Where(i => i.DOIStatusID != DOISTATUS_EXTERNAL);
                dlStatus.DataBind();
            }
        }
    }
}
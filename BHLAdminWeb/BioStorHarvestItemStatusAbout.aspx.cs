using MOBOT.BHLImport.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class BioStorHarvestItemStatusAbout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BHLImportProvider service = new BHLImportProvider();
                dlStatus.DataSource = service.BSItemStatusSelectAll();
                dlStatus.DataBind();
            }
        }
    }
}
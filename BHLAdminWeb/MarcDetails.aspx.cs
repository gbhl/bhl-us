using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.AdminWeb
{
    public partial class MarcDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the Marc ID
                String marcID = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();

                int idInt;
                if (Int32.TryParse(marcID, out idInt))
                {
                    marcList.DataSource = new BHLProvider().MarcSelectFullDetailsForMarcID(idInt);
                    marcList.DataBind();
                }
            }
        }
    }
}

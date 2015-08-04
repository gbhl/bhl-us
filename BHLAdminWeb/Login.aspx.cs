using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if logged in proceed to dashboard
            if (Helper.IsUserAuthenticated(new HttpRequestWrapper(Request))) Response.Redirect("dashboard.aspx");
        }
    }
}

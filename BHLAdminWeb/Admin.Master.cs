using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.AdminWeb
{
	public partial class Admin : System.Web.UI.MasterPage
	{
		protected void Page_Load( object sender, EventArgs e )
		{
            bool debugMode = new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).IsDebugMode(Response, Request);
            if (debugMode) Page.Title = "***DEBUG MODE*** " + Page.Title;

			// Make sure user is logged in
            if (Helper.IsUserAuthenticated(new HttpRequestWrapper(Request)))
            {
                accountlink.Text = string.Format(accountlink.Text, Context.GetOwinContext().Request.User.Identity.Name);

                // Make sure the user is authorized
                if (!Helper.IsUserAuthorized(new HttpRequestWrapper(Request)))
                {
                    Response.Redirect("/AccessDenied.aspx");
                }
            }
            else
            {
                Response.Redirect("/account/Login");
            }

            // Set an alert message if the full-text catalog is offline
            if (ConfigurationManager.AppSettings["EnableFullTextSearch"] == "true") // full-text search is enable site-wide
            {
                // Though enabled, the full-text catalog might be offline for maintenance.
                if (!(new BHLProvider().SearchCatalogOnline())) litAdminAlert.Text = "Search catalog is currently offline.";
            }
		}

		protected override void OnInit( EventArgs e )
		{
			base.OnInit( e );
            if (!Helper.IsUserAuthenticated(new HttpRequestWrapper(Request))) Response.Redirect("/account/login");
		}
	}
}

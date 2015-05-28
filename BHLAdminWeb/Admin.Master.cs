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
using MOBOT.Security.Client;
using MOBOT.Security.Client.MOBOTSecurity;
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

			Response.Cookies[ "CallingUrl" ].Value = Request.Url.ToString();

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

            // Make sure that the authorized user is valid for the production site (some are
            // restricted to test)
            String isProduction = ConfigurationManager.AppSettings["IsProduction"] as String;
            if (String.Compare(isProduction, "true", true) == 0)
            {
                HttpCookie tokenCookie = Request.Cookies["MOBOTSecurityToken"];
                SecUser user = Helper.GetSecProvider().SecUserSelect(tokenCookie.Value);
                String userName = user.UserName;
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
            Response.Cookies["CallingUrl"].Value = Request.Url.ToString();
            if (!Helper.IsUserAuthenticated(new HttpRequestWrapper(Request))) Response.Redirect("/account/login");
		}
	}
}

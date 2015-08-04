using System.Web;
using System.Web.Mvc;
using MOBOT.Security.Client.MOBOTSecurity;

namespace MOBOT.BHL.AdminWeb.ActionFilters
{
    public class BHLAuthorizationFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Make sure user is logged in and has admin rights
            if (!Helper.IsUserAuthenticated(filterContext.HttpContext.Request))
            {
                filterContext.HttpContext.Response.Redirect("/account/login", true);
            }
            else
            {
                if (!Helper.IsUserAuthorized(filterContext.HttpContext.Request))
                {
                    filterContext.HttpContext.Response.Redirect("~/AccessDenied.aspx");
                }
            }
        }
    }
}
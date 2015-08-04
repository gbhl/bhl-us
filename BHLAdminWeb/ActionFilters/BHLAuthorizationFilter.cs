using System.Web;
using System.Web.Mvc;
using MOBOT.Security.Client.MOBOTSecurity;

namespace MOBOT.BHL.AdminWeb.ActionFilters
{
    public class BHLAuthorizationFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Cookies["CallingUrl"].Value = filterContext.HttpContext.Request.Url.ToString();

            // Make sure user is logged in and has admin rights
            HttpCookie tokenCookie = filterContext.HttpContext.Request.Cookies["MOBOTSecurityToken"];
            if (!Helper.IsUserAuthenticated(filterContext.HttpContext.Request))
            {
                filterContext.HttpContext.Response.Redirect("~/login.aspx", true);
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
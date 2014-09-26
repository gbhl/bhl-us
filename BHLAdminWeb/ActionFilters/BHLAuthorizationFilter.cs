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
            if (tokenCookie == null || tokenCookie.Value.Length == 0 || !IsAdmin(filterContext.HttpContext.Request))
            {
                filterContext.HttpContext.Response.Redirect("~/login.aspx", true);
            }
        }

        public static bool IsAdmin(HttpRequestBase request)
        {
            HttpCookie tokenCookie = request.Cookies["MOBOTSecurityToken"];

            if (tokenCookie != null && tokenCookie.Value.Length > 0)
            {
                MethodResult result = Helper.GetSecProvider().IsUserAuthorized(tokenCookie.Value, Helper.SecurityFunction.BHLAdminLogin.Value());
                return (result.ResultStatus == ResultStatusEnum.Success);
            }

            return false;
        }
    }
}
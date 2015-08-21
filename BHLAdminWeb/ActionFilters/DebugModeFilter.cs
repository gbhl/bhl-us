using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.ActionFilters
{
    public class DebugModeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool debugMode = IsDebugMode(filterContext.HttpContext.Response, filterContext.HttpContext.Request);
            if (debugMode) filterContext.Controller.ViewBag.PageTitle = "***DEBUG MODE*** ";
        }

        private bool IsDebugMode(HttpResponseBase response, HttpRequestBase request)
        {
            bool isValueSet = false;
            string cookieKey = "IsDebugMode";
            string requestKey = "directive";
            string expectedValue = ConfigurationManager.AppSettings["DebugValue"];

            //first check to see if debug mode is set in a cookie
            isValueSet = (request.Cookies[cookieKey] != null && request.Cookies[cookieKey].Value == "true");

            //next look for debug setting in the query string
            if (!isValueSet)
            {
                if ((request.QueryString[requestKey] != null && request.QueryString[requestKey].Trim().ToLower() == expectedValue))
                {
                    isValueSet = true;
                    if (response != null) response.Cookies.Add(new HttpCookie(cookieKey, "true"));
                }
            }

            //check to see if we need to turn off debug mode
            if (request.QueryString[requestKey] != null && request.QueryString[requestKey].Trim().ToLower() != expectedValue)
            {
                isValueSet = false;
                if (request.Cookies[cookieKey] != null && response != null) response.Cookies[cookieKey].Value = "false";
            }

            return isValueSet;
        }
    }
}
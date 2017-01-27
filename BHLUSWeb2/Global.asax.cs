using System;
using System.Configuration;
using System.Web;
using System.Web.Routing;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.Web2
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Handle redirects to the wiki site (external to the BHL domain)
            var url = Request.Url;
            var port = url.Port;
            if (url.AbsolutePath.Equals("/permissions", StringComparison.InvariantCultureIgnoreCase) ||
                url.AbsolutePath.Equals("/permissions/", StringComparison.InvariantCultureIgnoreCase))
            {
                Response.Redirect(ConfigurationManager.AppSettings["WikiPagePermissions"]);
            }
            if (url.AbsolutePath.Equals("/about", StringComparison.InvariantCultureIgnoreCase) ||
                url.AbsolutePath.Equals("/about/", StringComparison.InvariantCultureIgnoreCase))
            {
                Response.Redirect(ConfigurationManager.AppSettings["WikiPageAbout"]);
            }
            if (url.AbsolutePath.ToLower().Contains("admin/") && 
                !url.AbsolutePath.Equals("/admin/default.html", StringComparison.InvariantCultureIgnoreCase))
            {
                Response.Redirect("/admin/default.html");
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            if (!HttpContext.Current.IsDebuggingEnabled && !(new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).IsDebugMode(Response, Request)))
            {
                // Log the exception
                if (ConfigurationManager.AppSettings["LogExceptions"] == "true")
                {
                    ExceptionUtility.LogException(exception, "Global.Application_Error");
                }

                Response.Clear();

                // Default redirect on Application_Error..
                string redirect = "~/error";

                if (exception is HttpException)
                {
                    switch (((HttpException)exception).GetHttpCode())
                    {
                        case 404:
                            redirect = "~/pagenotfound";
                            break;
                        case 500:
                            redirect = "~/error";
                            break;
                    }
                }

                // Clear the error on server.
                Server.ClearError();

                // Tell IIS to behave and stop trying to inject the standard IIS error pages
                Response.TrySkipIisCustomErrors = true;

                Response.Redirect(redirect);
                Response.End();
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}

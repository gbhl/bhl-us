using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.AdminWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

         protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Log the exception.            
            Exception exception = Server.GetLastError();

            if (!HttpContext.Current.IsDebuggingEnabled && !DebugUtility.IsDebugMode(Response, Request))
            {
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
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

       protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            //routes.Ignore("{resource}.aspx/{*pathInfo}");

            routes.MapPageRoute("Default",
                "",
                "~/dashboard.aspx");

            routes.MapPageRoute("Login",
                "login",
                "~/login.aspx");

            routes.MapPageRoute("Error-General",
                "error",
                "~/Error.aspx");

            routes.MapPageRoute("PageNotFound",
                "pagenotfound",
                "~/PageNotFound.aspx");

            routes.MapRoute("MVCDefault", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });

            routes.MapRoute("CatchAll",
                "{*url}",
                "~/PageNotFound.aspx");
        }
    }
}
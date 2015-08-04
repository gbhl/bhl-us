using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MOBOT.BHL.Web.Utilities;
using System.Configuration;

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

            if (!HttpContext.Current.IsDebuggingEnabled && !(new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).IsDebugMode(Response, Request)))
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

            // Routes for original WebForms pages
            routes.MapRoute("Default", "", new { controller = "Home", action = "Index" });
            routes.MapRoute("Login", "login", new { controller = "Home", action = "Login" });
            routes.MapRoute("Error-General", "error", new { controller = "Home", action = "Error" });
            routes.MapRoute("PageNotFound", "pagenotfound", new { controller = "Home", action = "PageNotFound" });

            // Routes for MVC pages
            routes.MapRoute("MVCDefault", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });

            routes.MapRoute("CatchAll",
                "{*url}",
                "~/PageNotFound.aspx");
        }
    }
}
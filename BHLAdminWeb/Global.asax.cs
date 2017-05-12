using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MOBOT.BHL.Web.Utilities;
using System.Configuration;
using System.Web.Optimization;

namespace MOBOT.BHL.AdminWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

         protected void Application_BeginRequest(object sender, EventArgs e)
        {

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

        protected void Application_End(object sender, EventArgs e)
        {

        }

       protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }
    }
}
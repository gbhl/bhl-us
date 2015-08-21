using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MOBOT.BHL.AdminWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
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
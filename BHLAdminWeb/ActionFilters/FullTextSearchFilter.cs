using System.Configuration;
using System.Web.Mvc;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.AdminWeb.ActionFilters
{
    public class FullTextSearchFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Set an alert message if the full-text catalog is offline
            if (ConfigurationManager.AppSettings["EnableFullTextSearch"] == "true") // full-text search is enable site-wide
            {
                // Though enabled, the full-text catalog might be offline for maintenance.
                if (!(new BHLProvider().SearchCatalogOnline())) filterContext.Controller.ViewBag.AdminAlert = "Search catalog is currently offline.";
            }
        }
    }
}
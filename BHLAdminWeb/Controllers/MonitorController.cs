using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.Models;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    [BHLAuthorizationFilter]
    [DebugModeFilter]
    [FullTextSearchFilter]
    public class MonitorController : Controller
    {
        // GET: Monitor
        public ActionResult Index()
        {
            MonitorModel model = new MonitorModel();
            return View(model);
        }
    }
}
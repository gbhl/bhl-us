using MOBOT.BHL.AdminWeb.Models;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult Overview()
        {
            ViewBag.PageTitle += "Services Overview";
            ServiceOverviewModel model = new ServiceOverviewModel();
            return View(model);
        }

        public ActionResult Log()
        {
            return View();
        }
    }
}
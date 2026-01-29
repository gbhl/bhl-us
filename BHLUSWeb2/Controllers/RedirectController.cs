using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class RedirectController : Controller
    {
        // GET: Redirect
        public ActionResult Permissions()
        {
            return RedirectPermanent(AppConfig.WikiPagePermissions);
        }

        public ActionResult About()
        {
            return RedirectPermanent(AppConfig.WikiPageAbout);
        }
    }
}
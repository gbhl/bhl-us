using System.Configuration;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class RedirectController : Controller
    {
        // GET: Redirect
        public ActionResult Permissions()
        {
            return RedirectPermanent(ConfigurationManager.AppSettings["WikiPagePermissions"]);
        }

        public ActionResult About()
        {
            return RedirectPermanent(ConfigurationManager.AppSettings["WikiPageAbout"]);
        }
    }
}
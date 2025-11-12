using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class TitleNotFoundController : Controller
    {
        // GET: PageNotFound
        public ActionResult Index()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}
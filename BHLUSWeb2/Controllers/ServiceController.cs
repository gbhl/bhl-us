using MOBOT.BHL.Server;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult GetNameDataSources(string name)
        {
            // TODO: Read/write cached GN Resolver API data here, so we don't have to hit the remote API each time

            return Json(
                new BHLProvider().GetNameDetailFromGNResolver(name),
                JsonRequestBehavior.AllowGet
            );
        }
    }
}
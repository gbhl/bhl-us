using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult GetNameDataSources(string name)
        {
            List<GNResolverResponse> nameSources = new List<GNResolverResponse>();

            // TODO: Read/write cached GN Resolver API data here, so we don't have to hit the remote API each time
            nameSources = new BHLProvider().GetNameDetailFromGNResolver(name);

            return Json(nameSources, JsonRequestBehavior.AllowGet);
        }
    }
}
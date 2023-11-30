using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Caching;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult GetNameDataSources(string name)
        {
            List<GNVerifierResponse> nameSources = new BHLProvider().GetNameDetailFromGNVerifier(name);

            return Json(nameSources, JsonRequestBehavior.AllowGet);
        }
    }
}
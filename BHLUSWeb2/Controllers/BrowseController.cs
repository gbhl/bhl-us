using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class BrowseController : Controller
    {
        // GET: Browse/Authors
        public ActionResult Authors(string start, int page, int numPerPage)
        {
            return View();
        }

        // GET: Browse/Collection
        public ActionResult Collection(string name, string start, string sort, int page, int numPerPage)
        {
            return View();
        }

        // GET: Browse/Contributor
        public ActionResult Contributor(string id, string start, string sort, int page, int numPerPage)
        {
            return View();
        }

        // GET: Browse/Titles
        public ActionResult Titles(string start, string sort, int page, int numPerPage)
        {
            return View();
        }

        // GET: Browse/Year
        public ActionResult Year(int start, int end, string sort, int page, int numPerPage)
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MOBOT.BHL.AdminWeb.Models;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult SegmentResolutionLog()
        {
            ViewBag.PageTitle = "Segment Resolution Log";

            SegmentResolutionLogModel model = new SegmentResolutionLogModel();
            model.SegmentResolutionLogs.Add("Log ID: 1, Segment ID: 73279, Matching Segment ID: 69421, Score: 0.8618");
            model.SegmentResolutionLogs.Add("Log ID: 2, Segment ID: 73279, Matching Segment ID: 73310, Score: 0.6243");

            return View(model);
        }

    }
}

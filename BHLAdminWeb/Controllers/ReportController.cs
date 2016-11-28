using System.Web.Mvc;
using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.MVCServices;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    [BHLAuthorizationFilter]
    [DebugModeFilter]
    [FullTextSearchFilter]
    public class ReportController : Controller
    {
        //
        // GET: /Report/SegmentResolutionLog
        public ActionResult SegmentResolutionLog()
        {
            ViewBag.PageTitle += "Segment Resolution Log";

            SegmentResolutionLogModel model = new SegmentResolutionLogModel();
            model.SegmentResolutionLogs.Add("Log ID: 1, Segment ID: 73279, Matching Segment ID: 69421, Score: 0.8618");
            model.SegmentResolutionLogs.Add("Log ID: 2, Segment ID: 73279, Matching Segment ID: 73310, Score: 0.6243");

            return View(model);
        }

        // GET: /Report/ReportingStats
        public ActionResult ReportingStats()
        {
            ViewBag.PageTitle += "BHL Reporting Statistics";
            ReportingStatsModel model = new ReportingStatsModel();
            return View(model);
        }

        // POST: /Report/ReportingStats
        [HttpPost]
        public ActionResult ReportingStats(ReportingStatsModel model)
        {
            if (Request.Form["btnDownload"] != null)
            {
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = string.Format("BHLReportingStatistics{0}.csv", System.DateTime.Now.ToString("yyyyMMddHHmmss")),
                    Inline = false,  // prompt the user for downloading, set true to show the file in the browser
                };
                Response.AppendHeader("Content-Disposition", cd.ToString());

                model.GetCSV();  // Get the report data to be downloaded
                return File(model.DownloadStats, "text/csv");
            }
            else // if (Request.Form["btnShow"] != null)
            {
                model.GetStats();  // Get the report data
                return View(model);
            }
        }

        // GET: /Report/Orphans
        public ActionResult Orphans()
        {
            ViewBag.PageTitle += "Orphaned Titles/Items/Segments";
            OrphanModel model = new OrphanModel();
            return View(model);
        }

        // POST: /Report/ReportingStats
        [HttpPost]
        public ActionResult Orphans(OrphanModel model)
        {
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("Orphans{0}.csv", System.DateTime.Now.ToString("yyyyMMddHHmmss")),
                Inline = false,  // prompt the user for downloading, set true to show the file in the browser
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            model.GetCSV();  // Get the report data to be downloaded
            return File(model.DownloadOrphans, "text/csv");
        }

        //
        // GET: /Report/CitationImportHistory
        [HttpGet]
        public ActionResult CitationImportHistory()
        {
            /*
            CitationService service = new CitationService();
            ViewBag.PageTitle += "Citation Import History";
            ViewBag.ContributorList = service.InstitutionList();
            ViewBag.ImportFileStatusList = service.ImportFileStatusList();
            ViewBag.ReportDateRangeList = service.ReportDateRangeList();

             */
            CitationImportHistoryModel model = new CitationImportHistoryModel();
            model.Institution = string.Empty;
            model.ImportFileStatus = string.Empty;
            model.ReportDateRange = "30";

            CitationImportHistoryInit(model);

            return View(model);
        }

        //
        // POST: /Report/CitationImportHistory
        [HttpPost]
        public ActionResult CitationImportHistory(CitationImportHistoryModel model)
        {   
            /*
            CitationService service = new CitationService();
            ViewBag.PageTitle += "Citation Import History";
            ViewBag.ContributorList = service.InstitutionList();
            ViewBag.ImportFileStatusList = service.ImportFileStatusList();
            ViewBag.ReportDateRangeList = service.ReportDateRangeList();

            model.GetImportFileList();
             */
            CitationImportHistoryInit(model);

            return View(model);
        }

        private void CitationImportHistoryInit(CitationImportHistoryModel model)
        {
            CitationService service = new CitationService();
            ViewBag.PageTitle += "Citation Import History";
            ViewBag.ContributorList = service.InstitutionList();
            ViewBag.ImportFileStatusList = service.ImportFileStatusList();
            ViewBag.ReportDateRangeList = service.ReportDateRangeList();

            model.GetImportFileList();
        }
    }
}

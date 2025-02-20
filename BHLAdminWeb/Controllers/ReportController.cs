﻿using System.Web.Mvc;
using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.MVCServices;
using static MOBOT.BHL.AdminWeb.Models.PermissionsTitlesModel;

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

        [HttpGet]
        public ActionResult PermissionsTitles()
        {
            PermissionsTitlesModel model = new PermissionsTitlesModel();
            ViewBag.PageTitle += "Permissions Titles";
            return View(model);
        }

        [HttpPost]
        public ActionResult PermissionsTitles(PermissionsTitlesModel model)
        {
            if (Request.Form["btnDownload"] != null)
            {
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = string.Format("PermissionsTitles{0}.csv", System.DateTime.Now.ToString("yyyyMMddHHmmss")),
                    Inline = false,  // prompt the user for downloading, set true to show the file in the browser
                };
                Response.AppendHeader("Content-Disposition", cd.ToString());

                // Get the report data to be downloaded
                model.GetPermissionsTitlesCSV(model.TitleID, model.IncludeNotKnown, model.IncludeInCopyright, model.IncludeNotProvided);
                return File(model.DownloadTitles, "text/csv");
            }

            return View(model);
        }

        //
        // AJAX method to support /Report/PermissionsTitles
        [HttpGet]
        public ActionResult GetPermissionsTitleRecords(int? titleID, bool nk, bool ic, bool np, 
            int sEcho, int iDisplayStart, int iDisplayLength, int iSortCol_0, string sSortDir_0)
        {
            string sortColumn;

            switch (iSortCol_0)
            {
                case 0:
                    sortColumn = "TitleID";
                    break;
                case 1:
                    sortColumn = "SortTitle";
                    break;
                case 10:
                    sortColumn = "HasMovingWall";
                    break;
                case 11:
                    sortColumn = "HasDocumentation";
                    break;
                default:
                    sortColumn = "SortTitle";
                    break;
            }

            PermissionsTitlesJson.Rootobject json = new PermissionsTitlesModel().GetRecords(titleID, nk, ic, np, 
                iDisplayLength, iDisplayStart, sortColumn, sSortDir_0);
            json.sEcho = sEcho;
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Report/CitationImportHistory
        [HttpGet]
        public ActionResult CitationImportHistory()
        {
            CitationImportHistoryModel model = new CitationImportHistoryModel();
            model.User = string.Empty;
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
            CitationImportHistoryInit(model);

            return View(model);
        }

        private void CitationImportHistoryInit(CitationImportHistoryModel model)
        {
            CitationService service = new CitationService();
            ViewBag.PageTitle += "Segment Import History";
            ViewBag.UserList = service.UserList();
            ViewBag.ImportFileStatusList = service.ImportFileStatusList();
            ViewBag.ReportDateRangeList = service.ReportDateRangeList();

            model.GetImportFileList();
        }

        // GET: /Report/TextImportHistory
        [HttpGet]
        public ActionResult TextImportHistory()
        {
            TextImportHistoryModel model = new TextImportHistoryModel();
            model.Institution = string.Empty;
            model.ImportBatchStatus = string.Empty;
            model.ReportDateRange = "30";

            TextImportHistoryInit(model);

            return View(model);
        }

        // POST: /Report/TextImportHistory
        [HttpPost]
        public ActionResult TextImportHistory(TextImportHistoryModel model)
        {
            TextImportHistoryInit(model);

            return View(model);
        }

        private void TextImportHistoryInit(TextImportHistoryModel model)
        {
            TextImportService service = new TextImportService();
            ViewBag.PageTitle += "Text Import History";
            ViewBag.InstitutionList = service.InstitutionList();
            ViewBag.TextImportBatchStatusList = service.TextImportBatchStatusList();
            ViewBag.ReportDateRangeList = service.ReportDateRangeList();

            model.GetImportBatchList();
        }
    }
}

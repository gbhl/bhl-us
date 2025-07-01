using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.AdminWeb.MVCServices;
using System;
using System.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    [BHLAuthorizationFilter]
    [DebugModeFilter]
    [FullTextSearchFilter]
    public class MonitorController : Controller
    {
        // GET: Monitor
        public ActionResult Index()
        {
            MonitorModel model = new MonitorModel(Request.PhysicalApplicationPath);
            model.GetMonitorData();
            ViewBag.MessageQueueAdminAddress = ConfigurationManager.AppSettings["MessageQueueAdminAddress"];
            return View(model);
        }

        // GET: WebStats
        public ActionResult WebStats()
        {
            WebStatsModel model = new WebStatsModel(Request.PhysicalApplicationPath);
            model.GetWebStats();
            return View(model);
        }

        // GET: ServiceOverview
        [HttpGet]
        public ActionResult ServiceOverview()
        {
            ViewBag.PageTitle += "Services Overview";
            ServiceOverviewModel model = new ServiceOverviewModel();
            return View(model);
        }

        // GET: ServiceLog
        [HttpGet]
        public ActionResult ServiceLog(int? id = null)
        {
            ViewBag.PageTitle += "Service Log";

            ServiceLogService service = new ServiceLogService();
            ViewBag.ServiceList = service.ServiceList();
            ViewBag.SeverityList = service.SeverityList();

            ServiceLogModel model = new ServiceLogModel(id);
            model.ServiceID = (id == null ? string.Empty : id.ToString());
            return View(model);
        }

        // POST: ServiceLog
        [HttpPost]
        public ActionResult ServiceLog(ServiceLogModel model)
        {
            if (Request.Form["btnDownload"] != null)
            {
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = string.Format("ServiceLog{0}.csv", System.DateTime.Now.ToString("yyyyMMddHHmmss")),
                    Inline = false,  // prompt the user for downloading, set true to show the file in the browser
                };
                Response.AppendHeader("Content-Disposition", cd.ToString());

                model.GetServiceLogCSV();  // Get the report data to be downloaded
                return File(model.DownloadLog, "text/csv");
            }
            else
            {
                ServiceLogService service = new ServiceLogService();
                ViewBag.ServiceList = service.ServiceList();
                ViewBag.SeverityList = service.SeverityList();
            }

            return View(model);
        }

        //
        // AJAX method to support /Services/Log
        [HttpGet]
        public ActionResult GetServiceLogRecords(int? serviceID, int? severityID, DateTime? startDate, DateTime? endDate,
            int sEcho, int iDisplayStart, int iDisplayLength, int iSortCol_0, string sSortDir_0)
        {
            string sortColumn;

            switch (iSortCol_0)
            {
                case 0:
                    sortColumn = "s.Name " + sSortDir_0 + ", s.Param";
                    break;
                case 1:
                    sortColumn = "f.Label";
                    break;
                case 2:
                default:
                    sortColumn = "l.CreationDate";
                    break;
                case 3:
                    sortColumn = "l.Message";
                    break;
                case 4:
                    sortColumn = "sv.Label";
                    break;
            }

            ServiceLogJson.Rootobject json = new ServiceLogModel().GetServiceLogRecords(serviceID, severityID, startDate, endDate, iDisplayLength,
                iDisplayStart, sortColumn, sSortDir_0);
            json.sEcho = sEcho;
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}
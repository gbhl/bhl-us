using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.AdminWeb.MVCServices;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
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

        // POST: ServiceLog
        [HttpPost]
        public ActionResult WebStats(WebStatsModel model)
        {
            model.AppPath = Request.PhysicalApplicationPath;
            model.GetWebStats();
            return View(model);
        }

        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult DownloadAllTraffic()
        {
            // Get the data to be formatted as CSV
            Dictionary<DateTime, int> logs = new WebStatsModel(Request.PhysicalApplicationPath).GetCombinedTrafficData(
                new List<string> { ConfigurationManager.AppSettings["DailyStatsServer1"], ConfigurationManager.AppSettings["DailyStatsServer2"] });

            var records = new List<dynamic>();
            foreach (var log in logs)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("Date", log.Key.ToString("yyyy-MM-dd"));
                record.Add("Requests", log.Value.ToString());
                records.Add(record);
            }

            // Convert the output to a byte array of a CSV string
            byte[] downloadString = new CSV().FormatCSVData(records);

            // Write the byte array to the output stream
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("AllWebTraffic{0}.csv", System.DateTime.Now.ToString("yyyyMMddHHmmss")),
                Inline = false,  // prompt the user for downloading, set true to show the file in the browser
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(downloadString, "text/csv");
        }

        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult DownloadAllTrafficByPath()
        {
            // Get the data to be formatted as CSV
            Dictionary<string, int> logs = new WebStatsModel(Request.PhysicalApplicationPath).GetCombinedTrafficByStemData(
                new List<string> { ConfigurationManager.AppSettings["DailyByStemStatsServer1"], ConfigurationManager.AppSettings["DailyByStemStatsServer2"] });

            var records = new List<dynamic>();
            foreach (var log in logs)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                string[] dateAndStem = log.Key.Split(',');
                record.Add("Date", dateAndStem[0]);
                record.Add("URLPath", dateAndStem[1]);
                record.Add("Requests", log.Value.ToString());
                records.Add(record);
            }

            // Convert the output to a byte array of a CSV string
            byte[] downloadString = new CSV().FormatCSVData(records);

            // Write the byte array to the output stream
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("AllWebTrafficByPath{0}.csv", System.DateTime.Now.ToString("yyyyMMddHHmmss")),
                Inline = false,  // prompt the user for downloading, set true to show the file in the browser
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(downloadString, "text/csv");
        }

        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult DownloadAllTrafficByStatus()
        {
            // Get the data to be formatted as CSV
            Dictionary<string, int> logs = new WebStatsModel(Request.PhysicalApplicationPath).GetCombinedTrafficByStatusData(
                new List<string> { ConfigurationManager.AppSettings["DailyByStatusStatsServer1"], ConfigurationManager.AppSettings["DailyByStatusStatsServer2"] });

            var records = new List<dynamic>();
            foreach (var log in logs)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                string[] dateAndStem = log.Key.Split(',');
                record.Add("Date", dateAndStem[0]);
                record.Add("Status", dateAndStem[1]);
                record.Add("Requests", log.Value.ToString());
                records.Add(record);
            }

            // Convert the output to a byte array of a CSV string
            byte[] downloadString = new CSV().FormatCSVData(records);

            // Write the byte array to the output stream
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("AllWebTrafficByStatus{0}.csv", System.DateTime.Now.ToString("yyyyMMddHHmmss")),
                Inline = false,  // prompt the user for downloading, set true to show the file in the browser
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(downloadString, "text/csv");
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.MVCServices;
using System.Web.Script.Serialization;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    [BHLAuthorizationFilter]
    [DebugModeFilter]
    [FullTextSearchFilter]
    public class CitationImportController : Controller
    {
        //
        // GET: /CitationImport/Index
        [HttpGet]
        public ActionResult Index()
        {
            CitationService service = new CitationService();

            ViewBag.PageTitle += "Citation Import";
            ViewBag.Contributor = new SelectList(service.InstitutionList(), "InstitutionCode", "InstitutionName");
            ViewBag.DataSourceType = new SelectList(service.DataSourceTypeList(), "key", "value", "text/plain");

            return View();
        }

        //
        // POST: /CitationImport/Index
        [HttpPost]
        public RedirectToRouteResult Index(CitationImportModel model)
        {
            // Upload and save the file
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0) continue;
                string savedFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + Path.GetFileName(hpf.FileName);
                string savedFilePath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["CitationNewPath"], savedFileName);
                hpf.SaveAs(savedFilePath);
                model.FilePath = savedFilePath;
                model.FileName = savedFileName;
            }

            TempData["Model"] = model;
            return RedirectToAction("FileDetail");
        }

        //
        // GET: /CitationImport/FileDetail
        [HttpGet]
        public ActionResult FileDetail()
        {
            CitationImportModel model = (CitationImportModel)TempData["Model"] ?? new CitationImportModel();
            CitationService service = new CitationService();
            Dictionary<string, string> codePages = service.CodePageList();
            Dictionary<string, string> delimiters = service.RowColumnDelimiterList();

            ViewBag.PageTitle += "Citation Import";
            ViewBag.CodePage = new SelectList(codePages, "key", "value", model.CodePage);
            ViewBag.RowDelimiter = new SelectList(delimiters, "key", "value", model.RowDelimiter);
            ViewBag.ColumnDelimiter = new SelectList(delimiters, "key", "value", model.ColumnDelimiter);

            return View(model);
        }

        //
        // POST: /CitationImport/FileDetail
        [HttpPost]
        public RedirectToRouteResult FileDetail(CitationImportModel model)
        {
            // Since new file details have just been specified, clear the column mappings
            model.Columns.Clear();

            TempData["Model"] = model;
            return RedirectToAction("Mapping");
        }

        //
        // GET: /CitationImport/Mapping
        [HttpGet]
        public ActionResult Mapping()
        {
            CitationImportModel model = (CitationImportModel)TempData["Model"] ?? new CitationImportModel();
            CitationService service = new CitationService();
            
            if (model.Columns.Count == 0) model.GetColumns();

            ViewBag.PageTitle += "Citation Import";
            ViewBag.MappedColumns = service.MappedColumnList();

            return View(model);
        }

        //
        // POST: /CitationImport/Mapping
        [HttpPost]
        public RedirectToRouteResult Mapping(CitationImportModel model)
        {
            TempData["Model"] = model;

            if (Request.Form["btnBack"] != null)
            {
                return RedirectToAction("FileDetail");
            }
            else
            {
                return RedirectToAction("Preview");
            }
        }

        //
        // GET: /CitationImport/Preview
        [HttpGet]
        public ActionResult Preview()
        {
            CitationImportModel model = (CitationImportModel)TempData["Model"] ?? new CitationImportModel();

            int userId = Helper.GetCurrentUserUID(Request);//  new HttpRequestWrapper(Request));
            model.GetRows(true, false, userId);

            ViewBag.PageTitle += "Citation Import";

            return View(model);
        }

        //
        // POST: /CitationImport/Preview
        [HttpPost]
        public RedirectToRouteResult Preview(CitationImportModel model)
        {
            TempData["Model"] = model;

            if (Request.Form["btnBack"] != null)
            {
                return RedirectToAction("Mapping");
            }
            else
            {
                // Parse the contents of the file and save it to the database.  Pass record ID for the file to the next View.
                int userId = Helper.GetCurrentUserUID(Request);
                model.ImportFile(userId);
                return RedirectToAction("Review", new { id = model.ImportFileID });
            }
        }

        //
        // GET: /CitationImport/Review/<id>
        [HttpGet]
        public ActionResult Review(int? id = null)
        {
            CitationImportModel model = new CitationImportModel();
            if (id != null) model.GetImportFileDetails((int)id);

            ViewBag.ImportRecordStatuses = new JavaScriptSerializer().Serialize(new CitationService().GetImportRecordStatuses());
            ViewBag.PageTitle += "Citation Import Review";

            return View(model);
        }

        //
        // POST: /CitationImport/Review
        [HttpPost]
        public ActionResult Review(CitationImportModel model)
        {
            HttpContext.Server.ScriptTimeout = 600;  // 10 minutes

            int userId = Helper.GetCurrentUserUID(Request);
            if (Request.Form["btnImport"] != null)
            {
                // Create production records for all "New" ImportRecords in the file.  Update statuses to "Imported".
                model.PublishFile(userId);
            }
            else if (Request.Form["btnReject"] != null)
            {
                // Reject all "New" ImportRecords in the file.  Update statuses to "Rejected".
                model.RejectFile(userId);
            }

            return RedirectToAction("CitationImportHistory", "Report");
        }

        //
        // AJAX method to support /CitationImport/Review
        [HttpGet]
        public ActionResult GetRecords(int importFileID, int sEcho, int iDisplayStart, int iDisplayLength, 
            int iSortCol_0, string sSortDir_0)
        {
            ImportRecordJson.Rootobject json = new CitationImportModel().GetImportRecords(importFileID, iDisplayLength, 
                iDisplayStart, "Title", sSortDir_0);
            json.sEcho = sEcho;
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //
        // AJAX method to support /CitationImport/Review
        [HttpPost]
        public ActionResult UpdateRecordStatus(int importRecordID, string originalValue, string value)
        {
            int importRecordStatusID;
            string newStatus = originalValue;

            if (Int32.TryParse(value, out importRecordStatusID))
            {
                int userId = Helper.GetCurrentUserUID(Request);
                newStatus = new CitationImportModel().UpdateRecordStatus(importRecordID, importRecordStatusID, userId);
            }

            return Content(newStatus);
        }
    }
}

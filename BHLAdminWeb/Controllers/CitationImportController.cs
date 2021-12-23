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
            ViewBag.PageTitle += "Segment Import";
            return View();
        }

        //
        // POST: /CitationImport/Index
        [HttpPost]
        public RedirectToRouteResult Index(CitationImportModel model)
        {
            return RedirectToAction("SelectFile");
        }

        // GET: /CitationImport/ImportFileStatus
        [HttpGet]
        public ActionResult ImportFileStatus()
        {
            CitationService service = new CitationService();
            CitationImportFileStatusModel model = new CitationImportFileStatusModel();
            model.ImportFileStatuses = service.ImportFileStatusList();
            return View(model);
        }

        //
        // GET: /CitationImport/SelectFile
        [HttpGet]
        public ActionResult SelectFile()
        {
            CitationService service = new CitationService();

            ViewBag.PageTitle += "Segment Import";
            ViewBag.Genre = new SelectList(service.GenreList(), "SegmentGenreID", "GenreName");
            ViewBag.DataSourceType = new SelectList(service.DataSourceTypeList(), "key", "value", "text/plain");

            return View();
        }

        //
        // POST: /CitationImport/SelectFile
        [HttpPost]
        public RedirectToRouteResult SelectFile(CitationImportModel model)
        {
            // Upload and save the file
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file];
                if (hpf.ContentLength == 0) continue;
                string savedFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + Path.GetFileName(hpf.FileName);
                string savedFilePath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["CitationNewPath"], savedFileName);
                hpf.SaveAs(savedFilePath);
                model.FilePath = savedFilePath;
                model.FileName = savedFileName;
            }

            List<DataObjects.SegmentGenre> genres = new CitationService().GenreList();
            foreach(DataObjects.SegmentGenre genre in genres)
            {
                if (genre.SegmentGenreID == model.Genre) model.GenreName = genre.GenreName;
            }

            TempData["Model"] = model;
            return RedirectToAction("Mapping");
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

            ViewBag.PageTitle += "Segment Import";
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

            foreach(var column in model.Columns)
            {
                column.MappedColumn = model.GetMappedColumn(column.ColumnName).name;
                column.ValueDelimiter = model.GetDelimiter(column.ColumnName);
            }

            ViewBag.PageTitle += "Segment Import";
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
                return RedirectToAction("SelectFile");
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
            model.GetRowCount();
            model.GetRows(true, false, userId);

            ViewBag.PageTitle += "Segment Import";

            return View(model);
        }

        //
        // POST: /CitationImport/Preview
        [HttpPost]
        public ActionResult Preview(CitationImportModel model)
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
                model.ImportFileError = string.Empty;

                try
                {
                    model.ImportFile(userId);
                    return RedirectToAction("Review", new { id = model.ImportFileID });
                }
                catch (CitationService.ImportFileException ex)
                {
                    model.ImportFileError = ex.Message;
                    return View(model);
                }
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
            ViewBag.PageTitle += "Segment Import Review";

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
            string sortColumn = "Title";

            switch (iSortCol_0)
            {
                case 0:
                case 4:
                    sortColumn = "Title";
                    break;
                case 12:
                    sortColumn = "Status";
                    break;
                default:
                    sortColumn = string.Format("JournalTitle {0},Year {0},RIGHT(SPACE(20) + Volume, 20) {0},RIGHT(SPACE(20) + Issue,20) {0},RIGHT(SPACE(20) + StartPage,20)", sSortDir_0);
                    break;
            }

            ImportRecordJson.Rootobject json = new CitationImportModel().GetImportRecords(importFileID, iDisplayLength, 
                iDisplayStart, sortColumn, sSortDir_0);
            json.sEcho = sEcho;
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRecord(int importRecordID)
        {
            ImportRecordJson.Rootobject json = new CitationImportModel().GetImportRecord(importRecordID);
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

        // AJAX method to support/CitationImport/Review
        [HttpGet]
        public ActionResult UpdateRecordCreatorID(int importRecordCreatorID, string value)
        {
            int? authorID = null;
            int intValue;

            if (!string.IsNullOrWhiteSpace(value))
            {
                if (Int32.TryParse(value, out intValue)) authorID = intValue;
            }

            int userId = Helper.GetCurrentUserUID(Request);
            new CitationImportModel().UpdateRecordCreatorID(importRecordCreatorID, authorID, userId);

            return Content(authorID.ToString() ?? string.Empty);
        }
    }
}

using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.AdminWeb.MVCServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    [BHLAuthorizationFilter]
    [DebugModeFilter]
    [FullTextSearchFilter]
    public class TextImportController : Controller
    {
        // GET: /TextImport
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.PageTitle += "Item Text Import";
            return View();
        }

        //
        // POST: /TextImport/Index
        [HttpPost]
        public RedirectToRouteResult Index(TextImportModel model)
        {
            return RedirectToAction("SelectFile");
        }

        //
        // GET: /TextImport/SelectFile
        [HttpGet]
        public ActionResult SelectFile()
        {
            TextImportService service = new TextImportService();
            ViewBag.PageTitle += "Item Text Import";
            return View();
        }

        //
        // POST: /TextImport/SelectFile
        [HttpPost]
        public RedirectToRouteResult SelectFile(TextImportModel model)
        {
            // Upload and save the file
            for (int x = 0; x < Request.Files.Count; x++)
            {
                HttpPostedFileBase hpf = Request.Files[x] as HttpPostedFileBase;
                if (hpf.ContentLength == 0) continue;
                string savedFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + Path.GetFileName(hpf.FileName);
                string savedFilePath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["TextImportPath"], savedFileName);
                hpf.SaveAs(savedFilePath);

                TextImportFileModel fileModel = new TextImportFileModel();
                fileModel.ItemID = Path.GetFileNameWithoutExtension(hpf.FileName);
                fileModel.FilePath = savedFilePath;
                fileModel.FileName = savedFileName;
                fileModel.FileFormat = new TextImportService().GetFileFormat(savedFilePath);
                fileModel.FileFormatName = new TextImportService().GetFileFormatValue(fileModel.FileFormat);
                model.Files.Add(fileModel);
            }

            // Add database records for the batch and files
            model.AddBatch(Helper.GetCurrentUserUID(Request));

            TempData["Model"] = model;
            return RedirectToAction("Review", new { id = model.BatchID });
        }

        // GET: /TextImport/Review
        [HttpGet]
        public ActionResult Review(int? id = null)
        {
            /*
            Read the pagination info for the item from the database, match it to the pages
            in the imported file, and present the list to the user for review.
            */

            TextImportModel model = new TextImportModel();
            if (id != null) model.GetImportBatchDetails((int)id);

            ViewBag.TextImportBatchFileStatuses = new TextImportService().TextImportBatchFileStatusList();
            ViewBag.PageTitle += "Text Import Review";

            return View(model);
        }

        // POST: /TextImport/Review
        [HttpPost]
        public ActionResult Review (TextImportModel model)
        {
            int userId = Helper.GetCurrentUserUID(Request);

            if (Request.Form["btnImport"] != null)
            {
                // Set the batch status to "Queued".  This will signal the application that generates the 
                // new text files to process this batch.  Only files in "Ready to Import" status will be
                // processed.

                /*
                The process that creates the text files will do the following:

                1) Replace the existing text files for the item with the text in the imported file.
                2) Log the pages for which text was updated
                3) Flag the item/pages for name indexing and add the item to the index queue.
                */

                model.QueueBatch(userId);
            }
            else if (Request.Form["btnReject"] != null)
            {
                // Set the statuses of the batch and the files to "Rejected".
                model.RejectBatch(userId);
            }

            return RedirectToAction("TextImportHistory", "Report");
        }

        // AJAX method to support /TextImport/Review
        [HttpGet]
        public ActionResult GetFiles(int batchID, int sEcho, int iDisplayStart, int iDisplayLength,
            int iSortCol_0, string sSortDir_0)
        {
            string sortColumn = "Filename";

            switch (iSortCol_0)
            {
                case 0:
                case 1:
                    sortColumn = "Filename";
                    break;
                case 4:
                    sortColumn = "Status";
                    break;
                default:
                    sortColumn = string.Format("Filename {0}", sSortDir_0);
                    break;
            }

            ImportRecordJson.Rootobject json = new TextImportModel().GetFiles(batchID, iDisplayLength,
                iDisplayStart, sortColumn, sSortDir_0);
            json.sEcho = sEcho;
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // AJAX method to support /TextImport/Review
        [HttpPost]
        public ActionResult UpdateRecordStatus(int fileID, string originalValue, string value)
        {
            int fileStatusID;
            string newStatus = originalValue;

            if (Int32.TryParse(value, out fileStatusID))
            {
                int userId = Helper.GetCurrentUserUID(Request);
                newStatus = new TextImportFileModel().UpdateFileStatus(fileID, fileStatusID, userId);
            }

            return Content(newStatus);
        }
    }
}
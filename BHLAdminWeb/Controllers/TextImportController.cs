using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.AdminWeb.MVCServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return RedirectToAction("Review");
        }

        //
        // GET: /TextImport/Review
        [HttpGet]
        public ActionResult Review()
        {
            TextImportModel model = (TextImportModel)TempData["Model"] ?? new TextImportModel();


            /*
            Read the pagination info for the item from the database, match it to the pages
            in the imported file, and present the list to the user for review.
            */


            ViewBag.PageTitle += "ItemText Import";

            return View(model);
        }


        //
        // POST: /TextImport/Review
        [HttpPost]
        public ActionResult Review (TextImportModel model)
        {
            int userId = Helper.GetCurrentUserUID(Request);


            /*
            1) Replace the existing text files for the item with the text in the imported file.
            2) Log the pages for which text was updated
            3) Flag the item/pages for name indexing and add the item to the index queue.
            */


            return RedirectToAction("TextImportHistory", "Report");
        }
    }
}
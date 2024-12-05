using Microsoft.Ajax.Utilities;
using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    [Authorize]
    public class DoiController : Controller
    {
        private const int _doiTypeTitleID = 10;
        private const string _doiTypeTitleName = "Title";
        private const int _doiTypeSegmentID = 40;
        private const string _doiTypeSegmentName = "Segment";
        private const int _doiStatusQueuedID = 30;
        private const int _doiStatusSubmittedID = 50;
        private const int _doiStatusErrorID = 80;

        public DoiController()
        {
        }

        [Authorize(Roles = "BHL.Admin.PortalEditor, BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult Queue(string sort, string role)
        {
            var model = QueueAction(sort);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "BHL.Admin.PortalEditor, BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult Queue()
        {
            var model = QueueAction(Request["SortBy"]);
            return View(model);
        }

        /// <summary>
        /// Action for the account index page (both GET and POST actions)
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private List<EditDoiQueueViewModel> QueueAction(string sort)
        {
            // Set up the sort variables
            string sortBy = String.IsNullOrWhiteSpace(sort) ? "datequeued_desc" : sort;
            ViewBag.SortBy = sortBy;
            ViewBag.ETypeSort = "entitytype";
            ViewBag.EIDSort = "entityid";
            ViewBag.ActionSort = "action";
            ViewBag.AddedBySort = "addedby";
            ViewBag.DateQueuedSort = "datequeued";

            // Get list of queued DOIs
            List<DOI> dois = new BHLProvider().DOISelectQueued();

            List<EditDoiQueueViewModel> model = new List<EditDoiQueueViewModel>();
            foreach (var doi in dois)
            {
                var d = new EditDoiQueueViewModel(doi);
                model.Add(d);
            }

            // Sort the list as specified
            switch (sortBy)
            {
                case "entitytype_desc":
                    model = model.OrderByDescending(d => d.EntityType).ToList();
                    break;
                case "entityid_desc":
                    model = model.OrderByDescending(d => d.EntityID).ToList();
                    break;
                case "entityid":
                    model = model.OrderBy(d => d.EntityID).ToList();
                    ViewBag.EIDSort = sortBy + "_desc";
                    break;
                case "action_desc":
                    model = model.OrderByDescending(d => d.Action).ToList();
                    break;
                case "action":
                    model = model.OrderBy(d => d.Action).ToList();
                    ViewBag.ActionSort = sortBy + "_desc";
                    break;
                case "addedby_desc":
                    model = model.OrderByDescending(d => d.AddedBy).ToList();
                    break;
                case "addedby":
                    model = model.OrderBy(d => d.AddedBy).ToList();
                    ViewBag.AddedBySort = sortBy + "_desc";
                    break;
                case "datequeued_desc":
                    model = model.OrderByDescending(d => d.DateQueued).ToList();
                    break;
                case "datequeued":
                    model = model.OrderBy(d => d.DateQueued).ToList();
                    ViewBag.DateQueuedSort = sortBy + "_desc";
                    break;
                default:    // entitytype
                    model = model.OrderBy(d => d.EntityType).ToList();
                    ViewBag.ETypeSort = sortBy + "_desc";
                    break;
            }

            return model;
        }

        // GET: Doi/QueueAddInfo
        [Authorize(Roles = "BHL.Admin.PortalEditor, BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult QueueAddInfo()
        {
            return View();
        }

        // GET: Doi/QueueAdd
        [Authorize(Roles = "BHL.Admin.PortalEditor, BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult QueueAdd()
        {

            ViewBag.EntityTypes = GetDOIEntityTypes();
            return View(new QueueAddViewModel());
        }

        //
        // POST: Doi/QueueAdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BHL.Admin.PortalEditor, BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public async Task<ActionResult> QueueAdd(QueueAddViewModel model)
        {
            HttpContext.Server.ScriptTimeout = 600;  // 10 minutes

            if (ModelState.IsValid)
            {
                try
                {
                    List<string> titleIDs = new List<string>();
                    List<string> segmentIDs = new List<string>();
                    bool copyrightWarning = false;
                    bool documentWarning = false;

                    if (model.EntityTypeID == _doiTypeTitleID)
                    {
                        if (string.IsNullOrWhiteSpace(model.TitleIDs)) throw new Exception("One or more Title IDs are required.");

                        string[] modelIDs = model.TitleIDs.Split('\n');

                        List<int> ids = new List<int>();
                        foreach (string titleId in modelIDs)
                        {
                            if (!string.IsNullOrWhiteSpace(titleId))
                            {
                                ValidationOutput validation = ValidateTitle(titleId);
                                if (validation.IsValid) titleIDs.Add(titleId + "|" + validation.Action + "|" + validation.Title);
                                copyrightWarning = copyrightWarning ? copyrightWarning : (validation.CopyrightWarning && validation.Action != "UPDATE");
                                documentWarning = documentWarning ? documentWarning : (validation.DocumentWarning && validation.Action != "UPDATE");
                            }
                        }
                    }
                    else
                    {
                        switch (model.SegmentOption)
                        {
                            case "Title":
                                ValidationOutput titleValidation = ValidateTitle(model.TitleID, false);
                                if (titleValidation.IsValid)
                                {
                                    List<Segment> segments = new BHLProvider().SegmentSelectWithoutDOIByTitleID(Convert.ToInt32(model.TitleID));
                                    if (segments.Count > 0)
                                    {
                                        copyrightWarning = titleValidation.CopyrightWarning && titleValidation.Action != "UPDATE";
                                        documentWarning = titleValidation.DocumentWarning && titleValidation.Action != "UPDATE";

                                        foreach (Segment segment in segments)
                                        {
                                            segmentIDs.Add(segment.SegmentID.ToString() + "|" + titleValidation.Action + "|" + segment.Title);
                                        }
                                    }
                                    else
                                    {
                                        AddErrors(string.Format("Title {0} - No eligible Segments have been defined for this Title", model.TitleID));
                                    }
                                }
                                break;
                            case "Item":
                                ValidationOutput itemValidation = ValidateItem(model.ItemID);
                                if (itemValidation.IsValid)
                                {
                                    List<Segment> segments = new BHLProvider().SegmentSelectWithoutDOIByItemID(Convert.ToInt32(model.ItemID));
                                    if (segments.Count > 0)
                                    {
                                        copyrightWarning = itemValidation.CopyrightWarning && itemValidation.Action != "UPDATE";
                                        documentWarning = itemValidation.DocumentWarning && itemValidation.Action != "UPDATE";

                                        foreach (Segment segment in segments)
                                        {
                                            segmentIDs.Add(segment.SegmentID.ToString() + "|" + itemValidation.Action + "|" + segment.Title);
                                        }
                                    }
                                    else
                                    {
                                        AddErrors(string.Format("Item {0} - No eligible Segments have been defined for this Item", model.ItemID));
                                    }
                                }
                                break;
                            case "Segment":
                                if (string.IsNullOrWhiteSpace(model.SegmentIDs)) throw new Exception("One or more Segment IDs are required.");

                                string[] modelIDs = model.SegmentIDs.Split('\n');
                                foreach (string segmentId in modelIDs)
                                {
                                    if (!string.IsNullOrWhiteSpace(segmentId))
                                    {
                                        ValidationOutput segmentValidation = ValidateSegment(segmentId);
                                        if (segmentValidation.IsValid)
                                        {
                                            copyrightWarning = copyrightWarning ? copyrightWarning : (segmentValidation.CopyrightWarning && segmentValidation.Action != "UPDATE");
                                            documentWarning = documentWarning ? documentWarning : (segmentValidation.DocumentWarning && segmentValidation.Action != "UPDATE");
                                            segmentIDs.Add(segmentId + "|" + segmentValidation.Action + "|" + segmentValidation.Title);
                                        }
                                    }
                                }

                                break;
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        titleIDs.Sort();
                        segmentIDs.Sort();
                        TempData["CopyrightWarning"] = copyrightWarning;
                        TempData["DocumentWarning"] = documentWarning;
                        TempData["Titles"] = titleIDs;
                        TempData["Segments"] = segmentIDs;
                        return RedirectToAction("QueueAddConfirm", "Doi");
                    }
                } 
                catch (Exception ex)
                {
                    AddErrors(ex.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.EntityTypes = GetDOIEntityTypes();
            return View(model);
        }

        // GET: Doi/QueueAddConfirm
        [Authorize(Roles = "BHL.Admin.PortalEditor, BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult QueueAddConfirm()
        {
            bool copyrightWarning = (bool)TempData["CopyrightWarning"];
            if (copyrightWarning) ModelState.AddModelError("Copyright", "One or more of these items may be in copyright. Please ensure you have permission to assign DOIs before proceeding.");
            bool documentWarning = (bool)TempData["DocumentWarning"];
            if (documentWarning) ModelState.AddModelError("Document", "One or more of these items is lacking appopriate documentation. Please ensure you have permission to assign DOIs before proceeding.");
            List<string> titles = (List<string>)TempData["Titles"];
            List<string> segments = (List<string>)TempData["Segments"];
            return View(new QueueAddConfirmViewModel(copyrightWarning, documentWarning, titles, segments));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BHL.Admin.PortalEditor, BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        [ValidateInput(false)]
        public async Task<ActionResult> QueueAddConfirm(QueueAddConfirmViewModel model)
        {
            HttpContext.Server.ScriptTimeout = 600;  // 10 minutes

            int userId = Helper.GetCurrentUserUID(Request);

            if (model.Titles != null)
            {
                List<int> ids = new List<int>();
                foreach(string title in model.Titles) ids.Add(Convert.ToInt32(title.Split('|')[0]));
                new BHLProvider().DOIInsertQueue(_doiTypeTitleID, ids, userId);
            }

            if (model.Segments != null)
            {
                List<int> ids = new List<int>();
                foreach (string segment in model.Segments) ids.Add(Convert.ToInt32(segment.Split('|')[0]));
                new BHLProvider().DOIInsertQueue(_doiTypeSegmentID, ids, userId);
            }

            return RedirectToAction("Queue", "Doi");
        }

        [Authorize(Roles = "BHL.Admin.PortalEditor, BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult QueueDelete(string id = null, string type = null)
        {
            //string type = "0";

            DOI doi = new BHLProvider().DOISelectQueuedByTypeAndID(type, Convert.ToInt32(id));
            var model = new EditDoiQueueViewModel(doi);
            if (doi == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BHL.Admin.PortalEditor, BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult QueueDelete(EditDoiQueueViewModel model)
        {
            new BHLProvider().DOIDeleteQueuedByTypeAndID(model.EntityTypeID, model.EntityID);
            return RedirectToAction("Queue");
        }

        public IEnumerable<SelectListItem> GetDOIEntityTypes()
        {
            List<SelectListItem> doiEntityTypes = new BHLProvider().DOIEntityTypeSelectAll()
                .Where(n => n.DOIEntityTypeID == _doiTypeTitleID || n.DOIEntityTypeID == _doiTypeSegmentID)
                .OrderBy(n => n.DOIEntityTypeName)
                .Select(n => new SelectListItem
                {
                    Value = n.DOIEntityTypeID.ToString(),
                    Text = n.DOIEntityTypeName
                }).ToList();

            var typeTip = new SelectListItem() { Value = null, Text = "--- select type ---" };
            doiEntityTypes.Insert(0, typeTip);
            return new SelectList(doiEntityTypes, "Value", "Text");
        }

        private ValidationOutput ValidateTitle(string titleID, bool validateDOI = true)
        {
            ValidationOutput output = new ValidationOutput(true, false, false);
            int titleInt;
            int copyrightYear = DateTime.Now.Year - 95;

            // Make sure Title ID is numeric
            if (!Int32.TryParse(titleID, out titleInt))
            {
                AddErrors(string.Format("Title {0} - ID must be numeric", titleID));
                output.IsValid = false;
            }

            if (output.IsValid)
            {
                BHLProvider provider = new BHLProvider();

                // Make sure Title ID points to a published BHL title
                Title title = provider.TitleSelectAuto(titleInt);
                if (title == null)
                {
                    AddErrors(string.Format("Title {0} - Not a valid BHL ID", titleID));
                    output.IsValid = false;
                }
                else if (title.PublishReady == false)
                {
                    AddErrors(string.Format("Title {0}  - Not a published BHL Title", titleID));
                    output.IsValid = false;
                }
                else if ((title.StartYear ?? 0) >= copyrightYear)
                {
                    output.CopyrightWarning = true;
                }

                // Make sure appropriate documentation exists
                if (output.IsValid)
                {
                    bool hasDoiDoc = false;
                    List<TitleDocument> documents = provider.TitleDocumentSelectByTitleID(titleInt);
                    foreach (TitleDocument td in documents) if (td.Name == "DOI") hasDoiDoc = true;
                    if (!hasDoiDoc) output.DocumentWarning = true;
                }

                if (output.IsValid && validateDOI)
                {
                    // Determine if we are adding a new DOI or updating an existing one
                    List<Title_Identifier> existingDoi = provider.DOISelectValidForTitle(titleInt);
                    output.Action = (existingDoi.Count == 0) ? "ADD" : "UPDATE";

                    // Make sure a "queued" entry in the DOI table does not already exist for this title.
                    DOI doi = provider.DOISelectQueuedByTypeAndID(_doiTypeTitleName, titleInt);
                    if (doi != null)
                    {
                        AddErrors(GetDOIError("Title", doi));
                        output.IsValid = false;
                    }
                }

                if (output.IsValid) output.Title = title.ShortTitle;
            }

            return output;
        }

        private ValidationOutput ValidateItem(string itemID)
        {
            ValidationOutput output = new ValidationOutput(true, false, false);
            int itemInt;
            int copyrightYear = DateTime.Now.Year - 95;

            // Make sure ID is numeric
            if (!Int32.TryParse(itemID, out itemInt))
            {
                AddErrors(string.Format("Item {0} - ID must be numeric", itemID));
                output.IsValid = false;
            }

            if (output.IsValid)
            {
                BHLProvider provider = new BHLProvider();

                // Make sure ID points to a published BHL item
                Book book = provider.BookSelectByBarcodeOrItemID(itemInt, null);
                if (book == null)
                {
                    AddErrors(string.Format("Item {0} - Not a valid BHL ID", itemID));
                    output.IsValid = false;
                }

                if (output.IsValid)
                {
                    Item item = provider.ItemSelectAuto(book.ItemID);
                    if (item.ItemStatusID != 40)
                    {
                        AddErrors(string.Format("Item {0} - Not a published BHL Item", itemID));
                        output.IsValid = false;
                    }
                }

                if (output.IsValid)
                {
                    int bookYear;
                    if (Int32.TryParse(book.StartYear, out bookYear))
                    {
                        if (bookYear >= copyrightYear) output.CopyrightWarning = true;
                    }
                    else if (book.PrimaryTitleID != null)
                    {
                        Title title = provider.TitleSelectAuto((int)book.PrimaryTitleID);
                        if (title != null)
                        {
                            if ((title.StartYear ?? 0) >= copyrightYear) output.CopyrightWarning = true;
                        }
                    }

                }

                if (output.IsValid)
                {
                    // Make sure appropriate documentation exists
                    bool hasDoiDoc = false;
                    List<TitleDocument> documents = provider.TitleDocumentSelectByBookID(itemInt);
                    foreach (TitleDocument td in documents) if (td.Name == "DOI") hasDoiDoc = true;
                    if (!hasDoiDoc) output.DocumentWarning = true;
                }
            }

            return output;
        }

        private ValidationOutput ValidateSegment(string segmentID)
        {
            ValidationOutput output = new ValidationOutput(true, false, false);
            int segmentInt;
            int copyrightYear = DateTime.Now.Year - 95;

            // Make sure ID is numeric
            if (!Int32.TryParse(segmentID, out segmentInt))
            {
                AddErrors(string.Format("Segment {0} - ID must be numeric", segmentID));
                output.IsValid = false;
            }

            if (output.IsValid)
            {
                BHLProvider provider = new BHLProvider();

                // Make sure ID points to a published BHL segment
                Segment segment = provider.SegmentSelectForSegmentID(segmentInt);
                if (segment == null)
                {
                    AddErrors(string.Format("Segment {0} - Not a valid BHL ID", segmentID));
                    output.IsValid = false;
                }
                else if (segment.SegmentStatusID != 40 && segment.SegmentStatusID != 30)
                {
                    AddErrors(string.Format("Segment {0} - Not a published BHL Segment", segmentID));
                    output.IsValid = false;
                }
                else 
                {
                    // If the date cannot be parsed, then the copyright status cannot be determined, and no warning will be given.
                    DateTime segmentDate;
                    int segmentYear;
                    if (Int32.TryParse(segment.Date, out segmentYear) && segment.Date.Trim().Length == 4)
                    {
                        if (segmentYear >= copyrightYear) output.CopyrightWarning = true;
                    }
                    if (DateTime.TryParse(segment.Date, out segmentDate))
                    {
                        if (segmentDate.Year >= copyrightYear) output.CopyrightWarning = true;
                    }
                }

                if (output.IsValid)
                {
                    // Make sure appropriate documentation exists
                    bool hasDoiDoc = false;
                    List<TitleDocument> documents = provider.TitleDocumentSelectBySegmentID(segmentInt);
                    foreach (TitleDocument td in documents) if (td.Name == "DOI") hasDoiDoc = true;
                    if (!hasDoiDoc) output.DocumentWarning = true;
                }

                if (output.IsValid)
                {
                    // Indicate whether we are adding a new DOI or updating an existing one
                    output.Action = (string.IsNullOrWhiteSpace(segment.DOIName)) ? "ADD" : "UPDATE";

                    // Make sure a "queued" entry in the DOI table does not already exist for this segmemt.
                    DOI doi = provider.DOISelectQueuedByTypeAndID(_doiTypeSegmentName, segmentInt);
                    if (doi != null)
                    {
                        AddErrors(GetDOIError("Segment", doi));
                        output.IsValid = false;
                    }
                }

                if (output.IsValid) output.Title = segment.Title;
            }

            return output;
        }

        private string GetDOIError(string type, DOI doi)
        {
            string errorMessage = string.Format("{0} {1} - Already queued for DOI assignment", type, doi.EntityID);

            // Now that Crossref metadata updates are supported, the only possible error is another "Queued" entry.  Other statuses can be duplicated.
            /*
            switch (doi.DOIStatusID)
            {
                case _doiStatusQueuedID:
                    errorMessage = string.Format("{0} {1} - Already queued for DOI assignment", type, doi.EntityID);
                    break;
                case _doiStatusSubmittedID:
                    errorMessage = string.Format("{0} {1} - DOI {2} already submitted to CrossRef", type, doi.EntityID, doi.DOIName);
                    break;
                case _doiStatusErrorID:
                    errorMessage = string.Format("{0} {1} - DOI {2} previously rejected by CrossRef", type, doi.EntityID, doi.DOIName);
                    break;
                default:
                    errorMessage = string.Format("{0} {1} - DOI {2} already assigned", type, doi.EntityID, doi.DOIName);
                    break;
            }
            */

            return errorMessage;
        }

        private void AddErrors(string error)
        {
            ModelState.AddModelError("", error);
        }
    }

    class ValidationOutput
    {
        public bool IsValid { get; set; }
        public bool CopyrightWarning { get; set; }
        public bool DocumentWarning { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Action { get; set; } = "ADD";

        public ValidationOutput(bool isValid, bool copyrightWarning, bool documentWarning)
        {
            IsValid = isValid;
            CopyrightWarning = copyrightWarning;
            DocumentWarning = documentWarning;
        }
    }
}
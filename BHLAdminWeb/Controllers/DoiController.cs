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
        private int _doiTypeTitle = 10;
        private int _doiTypeSegment = 40;

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
            string sortBy = String.IsNullOrWhiteSpace(sort) ? "datequeued" : sort;
            ViewBag.SortBy = sortBy;
            ViewBag.ETypeSort = "entitytype";
            ViewBag.EIDSort = "entityid";
            ViewBag.AddedBySort = "addedby";
            ViewBag.DateQueuedSort = "datequeued";

            // Get list of queued DOIs
            List<DOI> dois = new BHLProvider().DOISelectQueued(); // new List<DOI>(); //   Db.Users.Where(r => r.Id != 1).OrderBy(r => r.UserName);

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

        // GET: /QueueAdd
        public ActionResult QueueAdd()
        {

            ViewBag.EntityTypes = GetDOIEntityTypes();
            return View();
        }

        //
        // POST: /QueueAdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> QueueAdd(QueueAddViewModel model)
        {
            int userId = Helper.GetCurrentUserUID(Request);

            if (ModelState.IsValid)
            {
                try
                {
                    List<int> titleIDs = new List<int>();
                    List<int> segmentIDs = new List<int>();

                    if (model.EntityTypeID == _doiTypeTitle)
                    {
                        if (string.IsNullOrWhiteSpace(model.TitleIDs)) throw new Exception("One or more Title IDs are required.");

                        string[] modelIDs = model.TitleIDs.Split('\n');

                        List<int> ids = new List<int>();
                        foreach (string titleId in modelIDs)
                        {
                            if (ValidateTitle(titleId)) titleIDs.Add(Convert.ToInt32(titleId));
                        }
                    }
                    else
                    {
                        // TODO: Handle segment adds
                    }

                    if (ModelState.IsValid)
                    {
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

        // GET: /QueueAddConfirm
        public ActionResult QueueAddConfirm()
        {
            List<int> titleIDs = (List<int>)TempData["Titles"];
            List<int> segmentIDs = (List<int>)TempData["Segments"];
            return View(new QueueAddConfirmViewModel(titleIDs, segmentIDs));
        }

        public IEnumerable<SelectListItem> GetDOIEntityTypes()
        {
            List<SelectListItem> doiEntityTypes = new BHLProvider().DOIEntityTypeSelectAll()
                .Where(n => n.DOIEntityTypeID == _doiTypeTitle || n.DOIEntityTypeID == _doiTypeSegment)
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

        private bool ValidateTitle(string titleID)
        {
            bool isValid = true;
            int titleInt;

            // Make sure Title ID is numeric
            if (!Int32.TryParse(titleID, out titleInt))
            {
                AddErrors(string.Format("Title ID {0} must be numeric", titleID));
                isValid = false;
            }

            if (isValid)
            {
                BHLProvider provider = new BHLProvider();

                // Make sure Title ID points to a published BHL title
                Title title = provider.TitleSelectAuto(titleInt);
                if (title == null)
                {
                    AddErrors(string.Format("{0} is not a valid BHL Title ID", titleID));
                    isValid = false;
                }
                else if (title.PublishReady == false)
                {
                    AddErrors(string.Format("{0} is not a published BHL Title", titleID));
                    isValid = false;
                }

                if (isValid)
                {
                    // Make sure a DOI has not already been assigned to this title.
                    // NOTE: At some point will be allowed, but for now we do not handle resubmission of metadata for existing DOIs.
                    List<DOI> dois = provider.DOISelectValidForTitle(titleInt);
                    if (dois.Count > 0)
                    {
                        foreach (DOI doi in dois) AddErrors(string.Format("DOI {0} exists for Title {1}", doi.DOIName, doi.EntityID));
                        isValid = false;
                    }
                }
            }

            return isValid;
        }

        private void AddErrors(string error)
        {
            ModelState.AddModelError("", error);
        }
    }
}
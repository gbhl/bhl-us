using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.AdminWeb.MVCServices;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    [BHLAuthorizationFilter]
    [DebugModeFilter]
    [FullTextSearchFilter]
    public class AdminController : Controller
    {
        // GET: /Admin/Groups
        [HttpGet]
        public ActionResult Groups()
        {
            InstitutionGroupsModel model = new InstitutionGroupsModel();
            return View(model);
        }

        // GET: /Admin/GroupEdit/0
        [HttpGet]
        public ActionResult GroupEdit(int id)
        {
            InstitutionGroupModel model = new InstitutionGroupModel(id);
            return View(model);
        }

        // POST: /Admin/GroupEdit/0
        [HttpPost]
        public ActionResult GroupEdit(InstitutionGroupModel model)
        {
            int userId = Helper.GetCurrentUserUID(Request);

            if (ModelState.IsValid)
            {
                model.Save(userId);
                return RedirectToAction("Groups", "Admin");
            }

            // If we got this far, something failed, redisplay form
            return View(model);

        }

        // GET method to support /Admin/Groups
        [HttpGet]
        public ActionResult GroupDelete(int id)
        {
            new AdminService().DeleteInstitutionGroup(id);
            return RedirectToAction("Groups", "Admin");
        }

        // GET: /Admin/GroupInstitutions
        [HttpGet]
        public ActionResult GroupInstitutions(int id)
        {
            InstitutionGroupModel model = new InstitutionGroupModel(id);
            model.GetInstitutions(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult GroupInstitutions(InstitutionGroupModel model)
        {
            // Save group institutions
            List<string> institutionCodes = model.SelectedGroupInstitutions == null ? new List<string>() : new List<string>(model.SelectedGroupInstitutions);
            model.SaveGroupInstitutions(institutionCodes);

            return RedirectToAction("Groups", "Admin");
        }
    }
}
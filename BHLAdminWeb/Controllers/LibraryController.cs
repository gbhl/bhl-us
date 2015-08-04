using System.Web.Mvc;
using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.MVCServices;
using System;
using System.Reflection;
using System.Configuration;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    [BHLAuthorizationFilter]
    [DebugModeFilter]
    [FullTextSearchFilter]
    public class LibraryController : Controller
    {
        // GET: /Library/Align
        public ActionResult Align()
        {
            ViewBag.PageTitle += "Page Metadata-Image-OCR Alignment Utility";
            return View();
        }

        // POST: /Library/Align
        [HttpPost]
        [MultipleButton(Name="submit", Argument="Add")]
        public ActionResult AlignAdd(LibraryModel model)
        {
            model.AddPagesToItem();
            ResetModelForAlign(model, model.AddItemID, model.AddIAID);
            ViewBag.Action = "#divAdd";
            return View("Align", model);
        }

        // POST: /Library/Align
        [HttpPost]
        [MultipleButton(Name = "submit", Argument = "Delete")]
        public ActionResult AlignDelete(LibraryModel model)
        {
            model.DeletePagesFromItem();
            ResetModelForAlign(model, model.DelItemID, model.DelIAID);
            ViewBag.Action = "#divDelete";
            return View("Align", model);
        }

        // POST: /Library/Align
        [HttpPost]
        [MultipleButton(Name = "submit", Argument = "Ocr")]
        public ActionResult AlignOcr(LibraryModel model)
        {
            model.OcrJobPath = ConfigurationManager.AppSettings["OCRJobNewPath"];
            model.CreateNewOCRJobFile();
            ResetModelForAlign(model, model.OcrItemID, model.OcrIAID);
            ViewBag.Action = "#divOcr";
            return View("Align", model);
        }

        /// <summary>
        /// Reset the form values of the Align view
        /// </summary>
        /// <param name="model"></param>
        /// <param name="itemID"></param>
        /// <param name="iaID"></param>
        private void ResetModelForAlign(LibraryModel model, string itemID, string iaID)
        {
            string message = model.Message;
            ModelState.Clear();
            model.AddItemID = itemID;
            model.AddIAID = iaID;
            model.AddPageID = string.Empty;
            model.AddNum = string.Empty;
            model.DelItemID = itemID;
            model.DelIAID = iaID;
            model.DelPageID = string.Empty;
            model.DelNum = string.Empty;
            model.OcrItemID = itemID;
            model.OcrIAID = iaID;
            model.Message = message;
        }
    }

    /// <summary>
    /// Decorate POST methods with this attribute when the POST action could be triggered by more than one input/button control.
    /// 
    /// Usage example
    /// -------------
    /// Consider the following buttons:
    /// <input class="formButton" type="submit" name="submit:Add" id="btnAdd" value="Add Pages" />
    /// <input class="formButton" type="submit" name="submit:Delete" id="btnDelete" value="Remove Pages" />
    /// 
    /// Decorate the methods to capture those input actions like this:
    /// 
    /// [MultipleButton(Name="submit", Argument="Add")]
    /// public ActionResult DoAdd(SomeModel model)
    /// 
    /// [MultipleButton(Name = "submit", Argument = "Delete")]
    /// public ActionResult DoDelete(SomeModel model)
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultipleButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public string Argument { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            var isValidName = false;
            var keyValue = string.Format("{0}:{1}", Name, Argument);
            var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

            if (value != null)
            {
                controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
                isValidName = true;
            }

            return isValidName;
        }
    }
}

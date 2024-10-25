using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.AdminWeb.MVCServices;
using System;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        [HttpGet]
        public ActionResult Overview()
        {
            ViewBag.PageTitle += "Services Overview";
            ServiceOverviewModel model = new ServiceOverviewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Log(int? id = null)
        {
            ViewBag.PageTitle += "Service Log";

            ServiceLogService service = new ServiceLogService();
            ViewBag.ServiceList = service.ServiceList();
            ViewBag.SeverityList = service.SeverityList();

            ServiceLogModel model = new ServiceLogModel(id);
            model.ServiceID = (id == null ? string.Empty : id.ToString());
            return View(model);
        }

        [HttpPost]
        public ActionResult Log(ServiceLogModel model)
        {
            ServiceLogService service = new ServiceLogService();
            ViewBag.ServiceList = service.ServiceList();
            ViewBag.SeverityList = service.SeverityList();

            model.ServiceLogList = model.GetServiceLog(
                string.IsNullOrWhiteSpace(model.ServiceID) ? (int?)null : Convert.ToInt32(model.ServiceID),
                string.IsNullOrWhiteSpace(model.SeverityID) ? (int?)null : Convert.ToInt32(model.SeverityID),
                model.StartDate, model.EndDate);
            return View(model);
        }
    }
}
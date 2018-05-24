using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Linq;

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
            MonitorModel model = new MonitorModel();
            model.GetMonitorData();
            ViewBag.MessageQueueAdminAddress = ConfigurationManager.AppSettings["MessageQueueAdminAddress"];
            return View(model);
        }

        // GET: AddToQueue
        public ActionResult AddToQueue()
        {
            return View(new MonitorModel());
        }

        // POST: /Monitor/AddToQueue
        [HttpPost]
        public ActionResult AddToQueue(MonitorModel model)
        {
            string selectedQueue = model.queueName;
            string response = "Messages queued successfully";

            try
            {
                SiteService.ArrayOfString messages = new SiteService.ArrayOfString();
                messages.AddRange(model.queueMessages
                    .Replace(System.Environment.NewLine, "~")
                    .Split('~'));

                SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                service.QueueMessages(selectedQueue, messages);
            }
            catch (System.Exception ex)
            {
                response = "Error adding messages to queue";
            }

            model = new MonitorModel();
            ModelState.Clear();
            model.queueResult = response;
            return View(model);
        }
    }
}
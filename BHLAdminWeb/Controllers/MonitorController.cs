using MOBOT.BHL.AdminWeb.ActionFilters;
using MOBOT.BHL.AdminWeb.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Linq;
using MOBOT.BHL.Web.Utilities;
using BHL.SiteServiceREST.v1.Client;

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
                List<string> messages = new List<string>();
                messages.AddRange(model.queueMessages
                    .Replace(System.Environment.NewLine, "~")
                    .Split('~'));

                Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                client.PutQueueMessages(selectedQueue, messages);
            }
            catch (System.Exception ex)
            {
                response = "Error adding messages to queue";
                if (ConfigurationManager.AppSettings["LogExceptions"] == "true")
                {
                    ExceptionUtility.LogException(ex, "MonitorController.AddToQueue");
                }
                if (System.Web.HttpContext.Current.IsDebuggingEnabled) throw (ex);
            }

            model = new MonitorModel();
            ModelState.Clear();
            model.queueResult = response;
            return View(model);
        }
    }
}
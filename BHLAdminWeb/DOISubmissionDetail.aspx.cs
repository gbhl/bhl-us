using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.AdminWeb
{
    public partial class DOISubmissionDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    string type = Request.QueryString["type"] as string;
                    string batchId = Request.QueryString["id"] as string;
                    type = (type == null) ? string.Empty : type.ToLower();
                    batchId = (batchId == null) ? string.Empty : batchId;

                    if (!string.IsNullOrEmpty(batchId) && (type == "d" || type == "s"))
                    {
                        SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                        litDetail.Text = service.DOIGetFileContents(batchId, type);
                    }
                }
                catch (Exception ex)
                {
                    string message = "Error retrieving File.<br><br>;";
                    if (new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).IsDebugMode(Response, Request))
                    {
                        message += ex.Message + "<br><br>";
                        if (ex.StackTrace != null) message += ex.StackTrace;
                    }
                    litDetail.Text = message;
                }
            }
        }
    }
}
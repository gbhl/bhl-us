using BHL.SiteServiceREST.v1.Client;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Configuration;
using System.Xml;

namespace MOBOT.BHL.AdminWeb
{
    public partial class DOISubmissionDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string output = string.Empty;
                try
                {
                    string type = Request.QueryString["type"] as string;
                    string batchId = Request.QueryString["id"] as string;
                    type = (type == null) ? string.Empty : type.ToLower();
                    batchId = (batchId == null) ? string.Empty : batchId;

                    if (!string.IsNullOrEmpty(batchId) && (type == "d" || type == "s"))
                    {
                        Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                        output = client.GetDOIFile(batchId, type);
                    }

                    if (!output.Contains("<")) output = "<doi_submission_detail>" + output + "</doi_submission_detail>";
                }
                catch (Exception ex)
                {
                    string message = "<error_details><error>Error retrieving File.</error>";
                    if (new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).IsDebugMode(Response, Request))
                    {
                        message += "<message>" + ex.Message + "</message>";
                        if (ex.StackTrace != null) message += "<stack_trace>" + ex.StackTrace + "</stack_trace>";
                    }
                    message += "</error_details>";
                    output = message;
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(output);
                Response.ContentType = "text/xml";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Expires = -1;
                doc.Save(Response.Output);
            }
        }
    }
}
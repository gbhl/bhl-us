using System;
using System.Configuration;
using System.Web;

namespace MOBOT.BHL.Web2
{
    public class MODSDownload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int id;
            string idType = context.Request.RequestContext.RouteData.Values["type"] as string;
            string idString = context.Request.RequestContext.RouteData.Values["id"] as string;

            if (!string.IsNullOrWhiteSpace(idString) && string.IsNullOrWhiteSpace(idType)) idType = "title";
            if (string.IsNullOrEmpty(idString))
            {
                idString = context.Request.QueryString["pid"] as string;
                idType = "part";
            }

            string response = string.Empty;
            string filename = "bhl";

            if (Int32.TryParse(idString, out id))
            {
                try
                {
                    filename += idType + idString;
                    OAI2.OAIRecord record = new OAI2.OAIRecord("oai:" + ConfigurationManager.AppSettings["OAIIdentifierNamespace"] + ":" + idType + "/" + id.ToString());
                    OAIMODS.Convert mods = new OAIMODS.Convert(record);
                    response = mods.ToString();
                }
                catch
                {
                    response = "Error retrieving MODS for " + idType + ".";
                }
            }

            context.Response.ContentType = "application/xml";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "_mods.xml");
            context.Response.Write(response);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
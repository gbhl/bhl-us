using MOBOT.BHL.Server;
using System;
using System.Web;

namespace MOBOT.BHL.Web2
{
    public class RISDownload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int id;
            string idType = context.Request.RequestContext.RouteData.Values["type"] as string;
            string idString = context.Request.RequestContext.RouteData.Values["id"] as string;
            string tidString = context.Request.QueryString["t"] as string;  // Secondary ID containing TitleID associated with "id"

            if (!string.IsNullOrWhiteSpace(idString) && string.IsNullOrWhiteSpace(idType)) idType = "item";
            if (string.IsNullOrWhiteSpace(idString))
            {
                idString = context.Request.QueryString["tid"] as string;
                idType = "title";
            }
            if (string.IsNullOrWhiteSpace(idString))
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
                    switch (idType.ToLower())
                    {
                        case "title":
                            response = new BHLProvider().ItemSelectRISCitationsForTitleID(id);
                            break;
                        case "part":
                            response = new BHLProvider().SegmentGetRISCitationStringForSegmentID(id);
                            break;
                        case "item":
                            response = new BHLProvider().BookSelectRISCitationStringForBookID(id);
                            break;
                        case "page":
                            response = new BHLProvider().PageSelectRISCitationStringForPageID(id);
                            break;
                    }
                }
                catch
                {
                    response = "Error retrieving RIS citations for this " + idType + ".";
                }
            }

            context.Response.ContentType = "application/x-research-info-systems";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".ris");
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
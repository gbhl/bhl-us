using MOBOT.BHL.Server;
using System;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for RISDownload
    /// </summary>
    public class RISDownload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int id;
            string idString = context.Request.RequestContext.RouteData.Values["id"] as string;
            string idType = "item";

            if (string.IsNullOrEmpty(idString))
            {
                idString = context.Request.QueryString["tid"] as string;
                idType = "title";
            }

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
                    switch (idType)
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
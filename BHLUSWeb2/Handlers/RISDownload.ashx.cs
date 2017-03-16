using System;
using System.Collections.Generic;
using System.Linq;
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
            string idType = "title";

            idString = context.Request.RequestContext.RouteData.Values["id"] as string;

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
                    if (idType == "title")
                    {
                        response = new MOBOT.BHL.Server.BHLProvider().ItemSelectRISCitationsForTitleID(id);
                    }
                    else
                    {
                        response = new MOBOT.BHL.Server.BHLProvider().SegmentGetRISCitationStringForSegmentID(id);
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
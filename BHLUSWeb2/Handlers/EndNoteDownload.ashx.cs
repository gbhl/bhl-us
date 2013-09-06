using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class EndNoteDownload : IHttpHandler
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
                        response = new MOBOT.BHL.Server.BHLProvider().TitleEndNoteGetCitationStringForTitleID(id,
                            ConfigurationManager.AppSettings["ItemPageUrl"].ToString());
                    }
                    else
                    {
                        response = new MOBOT.BHL.Server.BHLProvider().SegmentEndNoteGetCitationStringForSegmentID(id,
                            ConfigurationManager.AppSettings["PartPageUrl"].ToString(), false);
                    }
                }
                catch
                {
                    response = "Error retrieving EndNote citations for this " + idType + ".";
                }
            }

            context.Response.ContentType = "application/x-endnote-refer";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".enw");
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

using MOBOT.BHL.Server;
using System;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class BibTeXDownload : IHttpHandler
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
                            response = new BHLProvider().TitleBibTeXGetCitationStringForTitleID(id);
                            break;
                        case "part":
                            response = new BHLProvider().SegmentBibTeXGetCitationStringForSegmentID(id, false);
                            break;
                        case "item":
                            response = new BHLProvider().BookBibTeXGetCitationStringForBookID(id);
                            break;
                    }
                }
                catch
                {
                    response = "Error retrieving BibTex citations for this " + idType + ".";
                }
            }

            context.Response.ContentType = "application/x-bibtex";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".bib");
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

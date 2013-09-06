using System;
using System.Collections.Generic;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    //[WebService(Namespace = "http://biodiversitylibrary.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class BibTeXDownload : IHttpHandler
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
                        response = new MOBOT.BHL.Server.BHLProvider().TitleBibTeXGetCitationStringForTitleID(id);
                    }
                    else
                    {
                        response = new MOBOT.BHL.Server.BHLProvider().SegmentBibTeXGetCitationStringForSegmentID(id, false);
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

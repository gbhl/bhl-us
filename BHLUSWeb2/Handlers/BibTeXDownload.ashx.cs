﻿using MOBOT.BHL.Server;
using System;
using System.Web;

namespace MOBOT.BHL.Web2
{
    public class BibTeXDownload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
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

            if (Int32.TryParse(idString, out int id))
            {
                try
                {
                    int? tid = int.TryParse(tidString, out int tmp) ? (int?)tmp : null;
                    filename += idType + idString;
                    switch (idType.ToLower())
                    {
                        case "title":
                            response = new BHLProvider().TitleBibTeXGetCitationStringForTitleID(id);
                            break;
                        case "part":
                            response = new BHLProvider().SegmentBibTeXGetCitationStringForSegmentID(id, tid, false);
                            break;
                        case "item":
                            response = new BHLProvider().BookBibTeXGetCitationStringForBookID(id, tid);
                            break;
                        case "page":
                            response = new BHLProvider().PageBibTeXGetCitationStringForPageID(id, tid);
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

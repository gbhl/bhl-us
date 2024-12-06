using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
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
            string tidString = context.Request.QueryString["t"] as string;  // Secondary ID containing TitleID associated with "id"

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
                    if (!string.IsNullOrWhiteSpace(tidString))
                    {
                        if (int.TryParse(tidString, out int tidInt))
                        {
                            Title title = new BHLProvider().TitleSelectAuto(tidInt);
                            if (idType == "item" && record.Title != title.FullTitle)
                            {
                                record.Title = title.FullTitle;
                                record.PartName = title.PartName;
                                record.PartNumber = title.PartNumber;
                                record.Publisher = title.Datafield_260_b;
                                record.PublicationPlace = title.Datafield_260_a;
                            }
                            if (idType == "part" && record.JournalTitle != title.FullTitle)
                            {
                                record.JournalTitle = title.FullTitle;
                                record.PartName = title.PartName;
                                record.PartNumber = title.PartNumber;
                                record.Publisher = title.Datafield_260_b;
                                record.PublicationPlace = title.Datafield_260_a;
                            }
                        }
                    }
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
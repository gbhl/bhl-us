using System;
using System.IO;
using System.Net.Http;
using System.Web;

namespace MOBOT.BHL.Web2
{
    public class CSLDownload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int id;
            string idType = context.Request.RequestContext.RouteData.Values["type"] as string;
            string idString = context.Request.RequestContext.RouteData.Values["id"] as string;

            string response = string.Empty;
            string filename = "bhl";
            if (Int32.TryParse(idString, out id))
            {
                try
                {
                    filename += idType + idString;
                    string idTypeArg = string.Empty;
                    var baseAddress = new Uri(string.Format("{0}{1}/", 
                        context.Request.Url.GetLeftPart(UriPartial.Authority), 
                        context.Request.ApplicationPath.TrimEnd('/')));

                    switch (idType.ToLower())
                    {
                        case "title":
                            idTypeArg = "t"; break;
                        case "part":
                            idTypeArg = "s"; break;
                        case "page":
                            idTypeArg = "p"; break;
                    }

                    string path = string.Format("/service/GetCitationJSON?idType={0}&id={1}", idTypeArg, idString);
                    response = new HttpClient().GetStringAsync(new Uri(baseAddress, path)).Result;
                }
                catch
                {
                    response = string.Format("{{ \"Error\" : \"Error retrieving CSL for {0}:{1}\" }}", idType, idString);
                }
            }

            context.Response.ContentType = "application/json";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "_csl.json");
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
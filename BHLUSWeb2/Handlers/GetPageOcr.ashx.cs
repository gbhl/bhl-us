using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetPageOcr : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //String pageIDString = context.Request.QueryString["PageID"] as String;
            String pageIDString = HttpContext.Current.Request.RequestContext.RouteData.Values["pageid"].ToString(); 
            if (pageIDString == null) return;

            int pageID;
            if (Int32.TryParse(pageIDString, out pageID))
            {
                BHLProvider provider = new BHLProvider();
                Page p = provider.PageSelectOcrPathForPageID(pageID);
                String ocrText = string.Empty;

                // Make sure we found an active page
                if (p != null)
                {
                    String ocrTextLocation = String.Format(ConfigurationManager.AppSettings["OCRTextLocation"],
                        p.OcrFolderShare, p.FileRootFolder, p.BarCode, p.FileNamePrefix);
                    ocrText = provider.GetOcrText(ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true", ocrTextLocation);
                }
                if (ocrText == String.Empty)
                {
                    ocrText = "OCR text unavailable for this page.";
                }

                context.Response.ContentType = "text/plain";
                context.Response.Write(ocrText);
            }
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

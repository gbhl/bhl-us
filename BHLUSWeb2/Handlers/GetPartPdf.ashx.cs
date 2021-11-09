using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for GetPartPdf
    /// </summary>
    public class GetPartPdf : IHttpHandler
    {
        int pdfTimeout = 36000000;

        public void ProcessRequest(HttpContext context)
        {
            string idString = HttpContext.Current.Request.RequestContext.RouteData.Values["id"].ToString();
            if (idString == null) return;

            context.Server.ScriptTimeout = pdfTimeout / 1000;

            if (Int32.TryParse(idString, out int id))
            {
                BHLProvider provider = new BHLProvider();
                DataObjects.Item item = provider.ItemSelectFilenames(ItemType.Segment, id);

                context.Response.ContentType = "application/pdf";
                if (!string.IsNullOrWhiteSpace(item.PdfFilename))
                {
                    try
                    {
                        context.Response.Redirect(string.Format(ConfigurationManager.AppSettings["IADownloadLink"], item.BarCode, item.PdfFilename));
                    }
                    catch (System.Net.WebException wex)
                    {
                        if (wex.Message.Contains("404"))
                        {
                            context.Response.Redirect("~/pagenotfound");
                        }
                    }
                }
                else
                {
                    if (ConfigurationManager.AppSettings["UsePregeneratedPDFs"] == "true")
                    {
                        this.GetPregeneratedPDF(context, id);
                    }
                    else
                    {
                        context.Response.Redirect("~/pagenotfound");
                    }
                }
            }
        }

        private void GetPregeneratedPDF(HttpContext context, int id)
        {
            // Send the PDF to the client
            context.Response.Clear();
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.Buffer = true;
            context.Response.ContentType = "application/pdf";
            context.Response.AddHeader("content-disposition", "filename=part" + id.ToString() + ".pdf");

            Stream stream = null;
            try
            {
                stream = this.GetPdfStream(id);
            }
            catch (WebException wex)
            {
                if (stream != null) stream.Close();

                string redirect = "~/error";
                var response = wex.Response as HttpWebResponse;
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound) redirect = "~/pagenotfound";
                }
                context.Response.Redirect(redirect, true);
            }

            if (stream != null)
            {
                try
                {
                    stream.CopyTo(context.Response.OutputStream);
                    context.Response.Flush();
                }
                catch
                {
                    context.Response.Redirect("~/error", true);
                }
                finally
                {
                    stream.Close();
                }
            }
            else
            {
                context.Response.Redirect("~/pagenotfound");
            }

            context.Response.End();
        }

        private Stream GetPdfStream(int id)
        {
            SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
            byte[] pdf = service.GetItemPdf((int)ItemType.Segment, id);
            return new MemoryStream(pdf);
            /*
            Stream pdf = null;
            HttpWebResponse resp = this.HttpGet(url);
            if (resp != null) pdf = resp.GetResponseStream();
            return pdf;
            */
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
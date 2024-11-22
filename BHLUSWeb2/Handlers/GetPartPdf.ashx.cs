using BHL.SiteServiceREST.v1.Client;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
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

                if (!string.IsNullOrWhiteSpace(item.PdfFilename))
                {
                    try
                    {
                        var filePath = provider.GetRemoteFilePath(RemoteFileType.Pdf, item.BarCode, item.PdfFilename);

                        // Check if the file exists before redirecting to it
                        var exists = false;
                        using (var client = new HttpClient())
                        {
                            var request = new HttpRequestMessage(HttpMethod.Head, filePath);
                            var response = client.SendAsync(request).GetAwaiter().GetResult();
                            exists = response.IsSuccessStatusCode;
                        }
                        if (exists)
                        {
                            context.Response.ClearHeaders();
                            context.Response.ContentType = "application/pdf";
                            context.Response.Redirect(filePath);
                        }
                        else
                        {
                            context.Response.Redirect("~/pagenotfound");
                        }
                    }
                    catch (WebException wex)
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
            context.Response.ContentType = "application/pdf";
            context.Response.AddHeader("content-disposition", "filename=part" + id.ToString() + ".pdf");

            Stream stream = null;
            try
            {
                stream = this.GetPdfStream(id);
            }
            catch (WebException wex)
            {
                if (stream != null) stream.Dispose();

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
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex, "GetPartPDF.GetPregeneratedPdf");
                }
                finally
                {
                    stream.Dispose();
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
            Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);            
            var pdf = client.GetSegmentPdf(id);
            return pdf;
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
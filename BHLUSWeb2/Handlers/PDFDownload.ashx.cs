using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for PDFDownload
    /// </summary>
    public class PDFDownload : IHttpHandler
    {
        int pdfTimeout = 36000000;

        public void ProcessRequest(HttpContext context)
        {
            string folder = context.Request.RequestContext.RouteData.Values["folder"] as string;
            string filename = context.Request.RequestContext.RouteData.Values["filename"] as string;

            string pdfPath = string.Format(ConfigurationManager.AppSettings["PdfUrl"], folder, filename);
            int timeOut = context.Server.ScriptTimeout;
            context.Server.ScriptTimeout = pdfTimeout / 1000;

            // Send the PDF to the client
            context.Response.Clear();
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.Buffer = true;
            context.Response.ContentType = "application/pdf";
            context.Response.AddHeader("content-disposition", "filename=" + filename);

            Stream stream = null;
            try
            {
                stream = this.GetPdfStream(pdfPath);
            }
            catch (System.Net.WebException wex)
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

            /*
            // Read the PDF and write it to the output stream
            BinaryReader stream = null;
            try
            { 
                stream = this.GetPdf(pdfPath);
            }
            catch (System.Net.WebException wex)
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
                    byte[] pdfBuffer = stream.ReadBytes(4096);
                    while (pdfBuffer.Length > 0)
                    {
                        context.Response.BinaryWrite(pdfBuffer);
                        context.Response.Flush();
                        pdfBuffer = stream.ReadBytes(4096);
                    }
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
            */

            context.Response.End();
        }

        /// <summary>
        /// Return the raw data stream for the specified pdf
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /*
        private BinaryReader GetPdf(string url)
        {
            BinaryReader reader = null;
            HttpWebResponse resp = this.HttpGet(url);
            if (resp != null) reader = new BinaryReader(resp.GetResponseStream());
            return reader;
        }
        */

        private Stream GetPdfStream(string url)
        {
            Stream pdf = null;
            HttpWebResponse resp = this.HttpGet(url);
            if (resp != null) pdf = resp.GetResponseStream();
            return pdf;
        }

        private HttpWebResponse HttpGet(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            // Set timeout values to prevent file truncation while downloading
            req.Timeout = pdfTimeout;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            return resp;
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
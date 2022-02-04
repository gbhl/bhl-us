﻿using MOBOT.BHL.Web.Utilities;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading;
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
            //context.Response.Buffer = true;
            context.Response.ContentType = "application/pdf";
            context.Response.AddHeader("content-disposition", "filename=" + filename);

            Stream stream = null;
            try
            {
                stream = this.GetPdfStream(pdfPath);
            }
            catch (System.Net.WebException wex)
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
                    //context.Response.BeginFlush(res => context.Response.EndFlush(res), Thread.CurrentThread.ManagedThreadId);
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex, "PDFDownload.ProcessRequest");
                }
                finally
                {
                    stream.Dispose();
                }
            }

            context.Response.End();
        }

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
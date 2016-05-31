using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MOBOT.BHL.Server;
using Newtonsoft.Json;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Web2.Controls;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace MOBOT.BHL.Web2.services
{
    /// <summary>
    /// Summary description for GeneratePdf
    /// </summary>
    public class GeneratePdf : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int itemId = int.Parse(context.Request["itemId"]);
            List<int> pageIds = context.Request["pages"].Split(',').Select(x => int.Parse(x)).ToList();
            string email = context.Request["email"];
            string title = context.Request["title"];
            string authors = context.Request["authors"];
            string subjects = context.Request["subjects"];
            bool imagesOnly = context.Request["imagesOnly"] != null;

            bool isSuccess = false;

            BHLProvider bhlProvider = new BHLProvider();

            PDF pdf = null;

            try
            {
                if (pageIds.Count > 0 && !string.IsNullOrWhiteSpace(email) &&
                    (string.IsNullOrWhiteSpace(title + authors + subjects) || (!string.IsNullOrWhiteSpace(title))))
                {
                    pdf = bhlProvider.AddNewPdf(itemId, email, string.Empty, imagesOnly, title, authors, subjects, pageIds);
                    isSuccess = SendEmail(email, pdf.PdfID);
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "GeneratePdf.ProcessRequest");
            }

            context.Response.ContentType = "application/json";

            JsonTextWriter writer = new JsonTextWriter(context.Response.Output);
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings());

            serializer.Serialize(writer, new { isSuccess, pdfId = (pdf != null) ? pdf.PdfID : 0 });
            writer.Flush();
        }

        private bool SendEmail(string toEmail, int pdfId)
        {
            /*
             * REMOVED April 5, 2013.  We decided the email was not needed. 
            try
            {
                String[] recipients = new String[1];
                recipients[0] = toEmail;
                BHLWebService.BHLWSSoapClient service = new BHLWebService.BHLWSSoapClient();
                service.SendEmail("noreply@biodiversitylibrary.org", recipients, null, null,
                    "BHL PDF Generation request #" + pdfId.ToString(), GetEmailBody(pdfId));
            }
            catch (Exception ex)
            {
                return false;
            }
             */

            return true;
        }

        private string GetEmailBody(int pdfId)
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            sb.Append("Your PDF generation request has been received and will be processed shortly.");
            sb.Append(endOfLine);
            sb.Append(endOfLine);
            sb.Append("When the PDF has been created, an email containing a link to download your PDF, will be sent to this address.");
            sb.Append(endOfLine);
            sb.Append(endOfLine);
            sb.Append("If you have questions or need to report a problem, please contact us via our Feedback page: http://www.biodiversitylibrary.org/contact");
            sb.Append(endOfLine);
            sb.Append(endOfLine);
            sb.Append("Thank you for your interest.");

            return sb.ToString();
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
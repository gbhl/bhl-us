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
                LogException(ex, "GeneratePdf.ProcessRequest");
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
             * REMOVED April 5, 2013.  SendMessage() method doesn't work (needs to submit to BHL
             * Web service to send mail), but we decided the email was not needed anyway.
            try
            {
                //String[] recipients = new String[1];
                //recipients[0] = toEmail;
                //MOBOT.BHL.Web.BHLWebService.BHLWS service = new MOBOT.BHL.Web.BHLWebService.BHLWS();
                //service.SendEmail("noreply@biodiversitylibrary.org", recipients, null, null,
                //    "BHL PDF Generation request #" + pdfId.ToString(), GetEmailBody(pdfId));

                System.Web.UI.Page page = new System.Web.UI.Page();
                EmailGeneratePdfReceived generatePdfReceived = (EmailGeneratePdfReceived)page.LoadControl("~/EmailGeneratePdfReceived.ascx");
                generatePdfReceived.PdfID = pdfId; 
                SendHtmlMessage(toEmail, "BHL PDF Generation request #" + pdfId, generatePdfReceived.RenderControlToString()); 

            }
            catch (Exception ex)
            {
                //LogException(ex, "GeneratePdf.SendEmail");

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
            sb.Append("When the PDF has been created, a message will be sent to this address and to any email addresses that you chose to share this PDF with. ");
            sb.Append("That message will contain a link to download your PDF.");
            sb.Append(endOfLine);
            sb.Append(endOfLine);
            sb.Append("If you have questions or need to report a problem, please contact us via our Feedback page: http://www.biodiversitylibrary.org/contact");
            sb.Append(endOfLine);
            sb.Append(endOfLine);
            sb.Append("Thank you for your interest.");

            return sb.ToString();
        }

        public static void SendHtmlMessage(string toAddress, string toDisplayName, string subject, string body)
        {
            SendMessage(toAddress, toDisplayName, null, null, subject, body, true);
        }

        public static void SendHtmlMessage(string toAddress, string subject, string body)
        {
            SendMessage(toAddress, toAddress, null, null, subject, body, true);
        }

        public static void SendMessage(string toAddress, string toDisplayName, string from, string fromDisplayName, string subject, string body, bool isBodyHtml)
        {
            try
            {
            MailMessage msg = new MailMessage();
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = isBodyHtml;
            msg.To.Add(new MailAddress(toAddress, toDisplayName));

            if (!string.IsNullOrEmpty(from))
            {
                msg.From = new MailAddress(from, fromDisplayName ?? string.Empty);
            }

            using (SmtpClient smtpClient = new SmtpClient("MBGMail01.mobot.org"))
            {
                //NetworkCredential basicCredential = new NetworkCredential("bhl@ala.org.au", "bhl4ever!");
                //smtpClient.EnableSsl = true;
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = basicCredential;
                smtpClient.Send(msg);
            }
            }
            catch (Exception ex)
            {
                //LogException(ex, "GeneratePdf.SendMessage");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        // Log an Exception
        public static void LogException(Exception exc, string source)
        {
            // Include logic for logging exceptions
            // Get the absolute path to the log file
            string logFile = "../logs/ErrorLog.txt";
            logFile = HttpContext.Current.Server.MapPath(logFile);

            // Open the log file for append and write the log
            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            if (exc.InnerException != null)
            {
                sw.Write("Inner Exception Type: ");
                sw.WriteLine(exc.InnerException.GetType().ToString());
                sw.Write("Inner Exception: ");
                sw.WriteLine(exc.InnerException.Message);
                sw.Write("Inner Source: ");
                sw.WriteLine(exc.InnerException.Source);
                if (exc.InnerException.StackTrace != null)
                {
                    sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(exc.InnerException.StackTrace);
                }
            }
            sw.Write("Exception Type: ");
            sw.WriteLine(exc.GetType().ToString());
            sw.WriteLine("Exception: " + exc.Message);
            sw.WriteLine("Source: " + source);
            sw.WriteLine("Stack Trace: ");
            if (exc.StackTrace != null)
            {
                sw.WriteLine(exc.StackTrace);
                sw.WriteLine();
            }
            sw.Close();
        }

    }
}
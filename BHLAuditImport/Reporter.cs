using System;
using System.Net.Mail;
using System.Configuration;

namespace MOBOT.BHL.BHLAuditImport
{
    class Reporter
    {
       public static void SendReport(string reportSubject, string ReportMessage)
        {
            string fromEmail = ConfigurationManager.AppSettings["EmailFromAddress"];
            string ToEmail = ConfigurationManager.AppSettings["EmailToAddress"];
        
            MailMessage mail = new MailMessage(fromEmail, ToEmail, reportSubject, ReportMessage);
            if (mail.To.Count > 0) mail.To[0] = new MailAddress(mail.To[0].Address);
            mail.From = new MailAddress(mail.From.Address);
            mail.IsBodyHtml = false;
            try
            {
                SmtpClient mailclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPHost"]);
                mailclient.Send(mail);
            }
            catch
            {
                return;
            }
       }
    }
}

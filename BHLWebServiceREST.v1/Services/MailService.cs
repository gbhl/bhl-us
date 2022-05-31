using BHL.WebServiceREST.v1.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace BHL.WebServiceREST.v1.Services
{
    public class MailService : IMailService
    {
        public async Task SendEmailAsync(MailRequestModel mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailRequest.from);
            foreach (string to in mailRequest.to)
            {
                email.To.Add(MailboxAddress.Parse(to));
            }
            foreach (string cc in mailRequest.cc)
            {
                if (!string.IsNullOrWhiteSpace(cc)) email.Cc.Add(MailboxAddress.Parse(cc));
            }
            foreach (string bcc in mailRequest.bcc)
            {
                if (!string.IsNullOrWhiteSpace(bcc)) email.Bcc.Add(MailboxAddress.Parse(bcc));
            }
            email.Subject = mailRequest.subject;

            var builder = new BodyBuilder();
            builder.TextBody = mailRequest.body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(System.Configuration.ConfigurationManager.AppSettings["SMTPHost"],
                options: MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MOBOT.BHL.AdminWeb.MVCServices
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Credentials:
            var credentialUserName = ConfigurationManager.AppSettings["EmailFromName"];
            var sentFrom = ConfigurationManager.AppSettings["EmailFromAddress"];

            // Configure the client:
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SMTPHost"]);

            // Create the message:
            var mail = new System.Net.Mail.MailMessage(sentFrom, message.Destination);

            mail.Subject = message.Subject;
            mail.Body = message.Body;

            // Send
            return client.SendMailAsync(mail);
        }
    }
}

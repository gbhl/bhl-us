using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MOBOT.BHL.AdminWeb.MVCServices
{
    public interface IBHLIdentityMessageService : IIdentityMessageService
    {
        Task SendAsync(IdentityMessage message, List<string> ccList, List<string> bccList);
    }

    public class EmailService : IBHLIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return this.SendAsync(message, new List<string>(), new List<string>());
        }

        /// <summary>
        /// SendAsync overload that allows for the message to be copied or blind copied to additional recipeients
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ccList"></param>
        /// <param name="bccList"></param>
        /// <returns></returns>
        public Task SendAsync(IdentityMessage message, List<string> ccList, List<string> bccList)
        {
            // Credentials:
            var credentialUserName = ConfigurationManager.AppSettings["EmailFromName"];
            var sentFrom = ConfigurationManager.AppSettings["EmailFromAddress"];

            // Configure the client:
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SMTPHost"]);

            // Create the message:
            var mail = new System.Net.Mail.MailMessage(sentFrom, message.Destination);

            foreach(string cc in ccList) mail.CC.Add(cc);
            foreach (string bcc in bccList) mail.Bcc.Add(bcc);

            mail.Subject = message.Subject;
            mail.Body = message.Body;

            // Send
            return client.SendMailAsync(mail);
        }
    }
}

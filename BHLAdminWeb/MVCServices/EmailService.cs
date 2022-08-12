using BHL.SiteServiceREST.v1.Client;
using BHL.SiteServicesREST.v1;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

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
        public async Task SendAsync(IdentityMessage message, List<string> ccList, List<string> bccList)
        {
            List<string> recipients = new List<string>();
            List<string> cc = new List<string>();
            List<string> bcc = new List<string>();

            recipients.Add(message.Destination);
            if (ccList != null) foreach (string ccRecipient in ccList) cc.Add(ccRecipient);
            if (bccList != null) foreach (string bccRecipient in bccList) bcc.Add(bccRecipient);

            Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
            MailRequestModel mailRequest = new MailRequestModel();
            mailRequest.From = ConfigurationManager.AppSettings["EmailFromAddress"];
            mailRequest.To = recipients;
            mailRequest.Cc = cc;
            mailRequest.Bcc = bcc;
            mailRequest.Subject = message.Subject;
            mailRequest.Body = message.Body;
            await client.SendEmailAsync(mailRequest);
            return;
        }
    }
}

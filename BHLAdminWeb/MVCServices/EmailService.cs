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
            SiteService.ArrayOfString recipients = new SiteService.ArrayOfString();
            SiteService.ArrayOfString cc = new SiteService.ArrayOfString();
            SiteService.ArrayOfString bcc = new SiteService.ArrayOfString();

            recipients.Add(message.Destination);
            if (ccList != null) foreach (string ccRecipient in ccList) cc.Add(ccRecipient);
            if (bccList != null) foreach (string bccRecipient in bccList) bcc.Add(bccRecipient);

            SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
            return service.SendEmailAsync(ConfigurationManager.AppSettings["EmailFromAddress"], 
                recipients, cc, bcc, message.Subject, message.Body);
        }
    }
}

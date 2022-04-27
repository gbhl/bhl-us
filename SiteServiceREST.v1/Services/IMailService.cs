using BHL.SiteServicesREST.v1.Models;

namespace BHL.SiteServicesREST.v1.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequestModel mailRequest);
    }
}

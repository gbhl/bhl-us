using BHL.WebServiceREST.v1.Models;

namespace BHL.WebServiceREST.v1.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequestModel mailRequest);
    }
}

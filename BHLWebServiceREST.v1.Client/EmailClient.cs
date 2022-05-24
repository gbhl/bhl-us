using System.Net.Http;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class EmailClient : RestClient
    {
        public EmailClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task SendEmailAsync(MailRequestModel email)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.EmailSendAsync(email).ConfigureAwait(false);
                return;
            }
        }

        public void SendEmail(MailRequestModel email)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.EmailSend(email);
                return;
            }
        }


    }
}

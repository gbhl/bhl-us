using System.Net.Http;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class PageTextLogsClient : ClientBase
    {
        public PageTextLogsClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task InsertPageTextLogAsync(PageTextLogModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.InsertPageTextLogAsync(request).ConfigureAwait(false);
                return;
            }
        }

        public void InsertPageTextLog(PageTextLogModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.InsertPageTextLog(request);
                return;
            }
        }
    }
}

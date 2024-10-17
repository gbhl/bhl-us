using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class ServiceLogsClient : RestClient
    {
        public ServiceLogsClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task InsertServiceLogAsync(ServiceLogModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.InsertServiceLogAsync(request).ConfigureAwait(false);
                return;
            }
        }

        public void InsertServiceLog(ServiceLogModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.InsertServiceLog(request);
                return;
            }
        }
    }
}

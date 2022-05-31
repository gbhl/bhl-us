using System.Collections.Generic;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class DoiClient : RestClient
    {
        public DoiClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task AddDoiAsync(DoiModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.AddDoiAsync(request).ConfigureAwait(false);
                return;
            }
        }

        public void AddDoi(DoiModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.AddDoi(request);
                return;
            }
        }

        public async Task UpdateDoiBatchIDAsync(int doiID, DoiModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.UpdateDoiBatchIDAsync(doiID, request).ConfigureAwait(false);
                return;
            }
        }

        public void UpdateDoiBatchID(int doiID, DoiModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.UpdateDoiBatchID(doiID, request);
                return;
            }
        }

        public async Task UpdateDoiNameAsync(int doiID, DoiModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.UpdateDoiNameAsync(doiID, request).ConfigureAwait(false);
                return;
            }
        }

        public void UpdateDoiName(int doiID, DoiModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.UpdateDoiName(doiID, request);
                return;
            }
        }

        public async Task UpdateDoiStatusAsync(int doiID, DoiModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.UpdateDoiStatusAsync(doiID, request).ConfigureAwait(false);
                return;
            }
        }

        public void UpdateDoiStatus(int doiID, DoiModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.UpdateDoiStatus(doiID, request);
                return;
            }
        }

        public async Task AddDoiIdentifierAsync(DoiModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.AddDoiIdentifierAsync(request).ConfigureAwait(false);
                return;
            }
        }

        public void AddDoiIdentifier(DoiModel request)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.AddDoiIdentifier(request);
                return;
            }
        }

        public async Task<ICollection<DOI>> GetSubmittedDoisAsync(int minutesSinceSubmit)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSubmittedDoisAsync(minutesSinceSubmit).ConfigureAwait(false));
            }
        }

        public ICollection<DOI> GetSubmittedDois(int minutesSinceSubmit)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSubmittedDois(minutesSinceSubmit);
            }
        }
    }
}

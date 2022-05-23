using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class DoiClient : ClientBase
    {
        public DoiClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task AddDoiAsync(DoiModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.AddDoiAsync(request).ConfigureAwait(false);
                return;
            }
        }

        public void AddDoi(DoiModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.AddDoi(request);
                return;
            }
        }

        public async Task UpdateDoiAsync(int doiID, DoiUpdateTarget target, DoiModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.UpdateDoiAsync(doiID, target, request).ConfigureAwait(false);
                return;
            }
        }

        public void UpdateDoi(int doiID, DoiUpdateTarget target, DoiModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.UpdateDoi(doiID, target, request);
                return;
            }
        }

        public async Task AddDoiIdentifierAsync(DoiModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.AddDoiIdentifierAsync(request).ConfigureAwait(false);
                return;
            }
        }

        public void AddDoiIdentifier(DoiModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.AddDoiIdentifier(request);
                return;
            }
        }

        public async Task<ICollection<DOI>> GetSubmittedDoisAsync(int minutesSinceSubmit)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSubmittedDoisAsync(minutesSinceSubmit).ConfigureAwait(false));
            }
        }

        public ICollection<DOI> GetSubmittedDois(int minutesSinceSubmit)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSubmittedDois(minutesSinceSubmit);
            }
        }
    }
}

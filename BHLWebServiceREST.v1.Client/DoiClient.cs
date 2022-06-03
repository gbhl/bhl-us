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
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            await restClient.AddDoiAsync(request).ConfigureAwait(false);
            return;
        }

        public void AddDoi(DoiModel request)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            restClient.AddDoi(request);
            return;
        }

        public async Task UpdateDoiBatchIDAsync(int doiID, DoiModel request)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            await restClient.UpdateDoiBatchIDAsync(doiID, request).ConfigureAwait(false);
            return;
        }

        public void UpdateDoiBatchID(int doiID, DoiModel request)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            restClient.UpdateDoiBatchID(doiID, request);
            return;
        }

        public async Task UpdateDoiNameAsync(int doiID, DoiModel request)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            await restClient.UpdateDoiNameAsync(doiID, request).ConfigureAwait(false);
            return;
        }

        public void UpdateDoiName(int doiID, DoiModel request)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            restClient.UpdateDoiName(doiID, request);
            return;
        }

        public async Task UpdateDoiStatusAsync(int doiID, DoiModel request)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            await restClient.UpdateDoiStatusAsync(doiID, request).ConfigureAwait(false);
            return;
        }

        public void UpdateDoiStatus(int doiID, DoiModel request)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            restClient.UpdateDoiStatus(doiID, request);
            return;            
        }

        public async Task AddDoiIdentifierAsync(DoiModel request)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            await restClient.AddDoiIdentifierAsync(request).ConfigureAwait(false);
            return;
        }

        public void AddDoiIdentifier(DoiModel request)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            restClient.AddDoiIdentifier(request);
            return;
        }

        public async Task<ICollection<DOI>> GetSubmittedDoisAsync(int minutesSinceSubmit)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return (await restClient.GetSubmittedDoisAsync(minutesSinceSubmit).ConfigureAwait(false));
        }

        public ICollection<DOI> GetSubmittedDois(int minutesSinceSubmit)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return restClient.GetSubmittedDois(minutesSinceSubmit);
        }
    }
}

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class OcrClient : ClientBase
    {
        public OcrClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<ICollection<NameFinderResponse>> GetNamesFromPageOcrAsync(int pageID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetNamesFromPageOcrAsync(pageID).ConfigureAwait(false));
            }
        }

        public ICollection<NameFinderResponse> GetNamesFromPageOcr(int pageID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetNamesFromPageOcr(pageID);
            }
        }

        public async Task<bool> ItemOcrExistsAsync(int itemID, string ocrTextPath)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.ItemOcrExistsAsync(itemID, ocrTextPath).ConfigureAwait(false));
            }
        }

        public bool ItemOcrExists(int itemID, string ocrTextPath)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.ItemOcrExists(itemID, ocrTextPath);
            }
        }

        public async Task<bool> PageOcrExistsAsync(int pageID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.PageOcrExistsAsync(pageID).ConfigureAwait(false));
            }
        }

        public bool PageOcrExists(int pageID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.PageOcrExists(pageID);
            }
        }
    }
}

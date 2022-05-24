using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class PagesClient : RestClient
    {
        public PagesClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<ICollection<Page>> GetPagesWithExpiredNamesAsync(int itemID, int maxAge)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetPagesWithExpiredNamesAsync(itemID, maxAge).ConfigureAwait(false));
            }
        }

        public ICollection<Page> GetPagesWithExpiredNames(int itemID, int maxAge)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetPagesWithExpiredNames(itemID, maxAge);
            }
        }

        public async Task<ICollection<Page>> GetPageWithoutNamesAsync(int? itemID = null)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetPageWithoutNamesAsync(itemID).ConfigureAwait(false));
            }
        }

        public ICollection<Page> GetPageWithoutNames(int? itemID = null)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetPageWithoutNames(itemID);
            }
        }

        public async Task<Page> UpdatePageLastPageNameLookupDateAsync(int pageID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.UpdatePageLastPageNameLookupDateAsync(pageID).ConfigureAwait(false));
            }
        }

        public Page UpdatePageLastPageNameLookupDate(int pageID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.UpdatePageLastPageNameLookupDate(pageID);
            }
        }

        public async Task<ICollection<int>> UpdatePageNamesAsync(int pageID, PageNameModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.UpdatePageNamesAsync(pageID, request).ConfigureAwait(false));
            }
        }

        public ICollection<int> UpdatePageNames(int pageID, PageNameModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.UpdatePageNames(pageID, request);
            }
        }
    }
}

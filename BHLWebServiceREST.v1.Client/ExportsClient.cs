using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class ExportsClient : RestClient
    {
        private HttpClient _httpClient = null;

        public ExportsClient(string baseUrl) : base(baseUrl)
        {
            _httpClient = GetHttpClient();
        }

        ~ExportsClient()
        {
            _httpClient.Dispose();
            _httpClient = null;
        }

        public async Task<ICollection<TitleBibTeX>> GetTitleBibTexCitationsAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetTitleBibTexCitationsAsync().ConfigureAwait(false));
            }
        }

        public ICollection<TitleBibTeX> GetTitleBibTexCitations()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetTitleBibTexCitations();
            }
        }

        public async Task<ICollection<TitleBibTeX>> GetItemBibTexCitationsAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemBibTexCitationsAsync().ConfigureAwait(false));
            }
        }

        public ICollection<TitleBibTeX> GetItemBibTexCitations()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemBibTexCitations();
            }
        }

        public async Task<ICollection<TitleBibTeX>> GetSegmentBibTexCitationsAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentBibTexCitationsAsync().ConfigureAwait(false));
            }
        }

        public ICollection<TitleBibTeX> GetSegmentBibTexCitations()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentBibTexCitations();
            }
        }

        public async Task<string> GetTitleMODSAsync(int titleID)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return (await restClient.GetTitleMODSAsync(titleID).ConfigureAwait(false));
        }

        public string GetTitleMODS(int titleID)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return restClient.GetTitleMODS(titleID);
        }

        public async Task<string> GetItemMODSAsync(int itemID)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return (await restClient.GetItemMODSAsync(itemID).ConfigureAwait(false));
        }

        public string GetItemMODS(int itemID)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return restClient.GetItemMODS(itemID);
        }

        public async Task<string> GetSegmentMODSAsync(int segmentID)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return (await restClient.GetSegmentMODSAsync(segmentID).ConfigureAwait(false));
        }

        public string GetSegmentMODS(int segmentID)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return restClient.GetSegmentMODS(segmentID);
        }

        public async Task<ICollection<RISCitation>> GetTitleRISCitationsAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetTitleRISCitationsAsync().ConfigureAwait(false));
            }
        }

        public ICollection<RISCitation> GetTitleRISCitations()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetTitleRISCitations();
            }
        }

        public async Task<ICollection<RISCitation>> GetItemRISCitationsAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemRISCitationsAsync().ConfigureAwait(false));
            }
        }

        public ICollection<RISCitation> GetItemRISCitations()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemRISCitations();
            }
        }

        public async Task<ICollection<RISCitation>> GetSegmentRISCitationsAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentRISCitationsAsync().ConfigureAwait(false));
            }
        }

        public ICollection<RISCitation> GetSegmentRISCitations()
        {
            using (var httpClient = GetHttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentRISCitations();
            }
        }

        public async Task<string> GetRISCitationStringAsync(RISCitation citation)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return (await restClient.GetRISCitationStringAsync(citation).ConfigureAwait(false));
        }

        public string GetRISCitationString(RISCitation citation)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return restClient.GetRISCitationString(citation);
        }

        public async Task<ICollection<string>> GetIAIdentifiersAsync()
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return (await restClient.GetIAIdentifiersAsync().ConfigureAwait(false));
        }

        public ICollection<string> GetIAIdentifiers()
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return restClient.GetIAIdentifiers();
        }

        public async Task<ICollection<KBART>>GetKBARTAsync(string urlRoot)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return (await restClient.GetKBARTAsync(urlRoot).ConfigureAwait(false));
        }

        public ICollection<KBART> GetKBART(string urlRoot)
        {
            BHLWS restClient = new BHLWS(_baseUrl, _httpClient);
            return restClient.GetKBART(urlRoot);
        }
    }
}

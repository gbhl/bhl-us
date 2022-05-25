using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class ExportsClient : RestClient
    {
        public ExportsClient(string baseUrl) : base(baseUrl)
        {
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
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetTitleMODSAsync(titleID).ConfigureAwait(false));
            }
        }

        public string GetTitleMODS(int titleID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetTitleMODS(titleID);
            }
        }

        public async Task<string> GetItemMODSAsync(int itemID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemMODSAsync(itemID).ConfigureAwait(false));
            }
        }

        public string GetItemMODS(int itemID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemMODS(itemID);
            }
        }

        public async Task<string> GetSegmentMODSAsync(int segmentID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentMODSAsync(segmentID).ConfigureAwait(false));
            }
        }

        public string GetSegmentMODS(int segmentID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentMODS(segmentID);
            }
        }

        public async Task<ICollection<RISCitation>> GetTitleRISCitationsAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetTitleRISCitationsAsync().ConfigureAwait(false));
            }
        }

        public ICollection<RISCitation> GetTitleRISCitations()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetTitleRISCitations();
            }
        }

        public async Task<ICollection<RISCitation>> GetItemRISCitationsAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemRISCitationsAsync().ConfigureAwait(false));
            }
        }

        public ICollection<RISCitation> GetItemRISCitations()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemRISCitations();
            }
        }

        public async Task<ICollection<RISCitation>> GetSegmentRISCitationsAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentRISCitationsAsync().ConfigureAwait(false));
            }
        }

        public ICollection<RISCitation> GetSegmentRISCitations()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentRISCitations();
            }
        }

        public async Task<string> GetRISCitationStringAsync(RISCitation citation)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetRISCitationStringAsync(citation).ConfigureAwait(false));
            }
        }

        public string GetRISCitationString(RISCitation citation)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetRISCitationString(citation);
            }
        }

        public async Task<ICollection<string>> GetIAIdentifiersAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetIAIdentifiersAsync().ConfigureAwait(false));
            }
        }

        public ICollection<string> GetIAIdentifiers()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetIAIdentifiers();
            }
        }

        public async Task<ICollection<KBART>>GetKBARTAsync(string urlRoot)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetKBARTAsync(urlRoot).ConfigureAwait(false));
            }
        }

        public ICollection<KBART> GetKBART(string urlRoot)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetKBART(urlRoot);
            }
        }
    }
}

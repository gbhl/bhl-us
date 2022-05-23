using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class TitlesClient : ClientBase
    {
        public TitlesClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<Title> GetTitleAsync(int titleID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetTitleAsync(titleID).ConfigureAwait(false));
            }
        }

        public Title GetTitle(int titleID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetTitle(titleID);
            }
        }

        public async Task<Title> GetTitleDetailsAsync(int titleID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetTitleDetailsAsync(titleID).ConfigureAwait(false));
            }
        }

        public Title GetTitleDetails(int titleID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetTitleDetails(titleID);
            }
        }

        public async Task<ICollection<Title_Identifier>> GetTitleIdentifiersAsync(int titleID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetTitleIdentifiersAsync(titleID).ConfigureAwait(false));
            }
        }

        public ICollection<Title_Identifier> GetTitleIdentifiers(int titleID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetTitleIdentifiers(titleID);
            }
        }

        public async Task<ICollection<Title_Identifier>> GetTitleDoisAsync(int titleID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetTitleDoisAsync(titleID).ConfigureAwait(false));
            }
        }

        public ICollection<Title_Identifier> GetTitleDois(int titleID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetTitleDois(titleID);
            }
        }

        public async Task<ICollection<DOI>> GetTitleWithoutDoisAsync(int numberToReturn)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetTitleWithoutDoisAsync(numberToReturn).ConfigureAwait(false));
            }
        }

        public ICollection<DOI> GetTitleWithoutDois(int numberToReturn)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetTitleWithoutDois(numberToReturn);
            }
        }

        public async Task<ICollection<Title>> GetTitlesPublishedAsync()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetTitlesPublishedAsync().ConfigureAwait(false));
            }
        }

        public ICollection<Title> GetTitlesPublished()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetTitlesPublished();
            }
        }
    }
}

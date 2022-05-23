using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class PageFlickrClient : ClientBase
    {
        public PageFlickrClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<ICollection<PageFlickr>> GetPageFlickrRandomAsync(int numberToReturn)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetPageFlickrRandomAsync(numberToReturn).ConfigureAwait(false));
            }
        }

        public ICollection<PageFlickr> GetPageFlickrRandom(int numberToReturn)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetPageFlickrRandom(numberToReturn);
            }
        }

    }
}

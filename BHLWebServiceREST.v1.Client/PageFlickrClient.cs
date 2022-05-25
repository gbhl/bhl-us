using System.Collections.Generic;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class PageFlickrClient : RestClient
    {
        public PageFlickrClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<ICollection<PageFlickr>> GetPageFlickrRandomAsync(int numberToReturn)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetPageFlickrRandomAsync(numberToReturn).ConfigureAwait(false));
            }
        }

        public ICollection<PageFlickr> GetPageFlickrRandom(int numberToReturn)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetPageFlickrRandom(numberToReturn);
            }
        }

    }
}

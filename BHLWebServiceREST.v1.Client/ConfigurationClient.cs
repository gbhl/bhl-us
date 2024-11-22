using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class ConfigurationClient : RestClient
    {
        public ConfigurationClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<string> GetDjvuFilePathAysnc(string barcode, string fileName)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetDjvuFilePathAsync(barcode, fileName).ConfigureAwait(false));
            }
        }

        public string GetDjvuFilePath(string barcode, string fileName)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetDjvuFilePath(barcode, fileName);
            }
        }
    }
}

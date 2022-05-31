using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class VaultsClient : RestClient
    {
        public VaultsClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<Vault> GetVaultAysnc(int vaultID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetVaultAsync(vaultID).ConfigureAwait(false));
            }
        }

        public Vault GetVault(int vaultID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetVault(vaultID);
            }
        }
    }
}

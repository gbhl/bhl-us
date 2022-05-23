using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class ItemsClient : ClientBase
    {
        public ItemsClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<Item> GetItemAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemAsync(itemID).ConfigureAwait(false));
            }
        }

        public Item GetItem(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItem(itemID);
            }
        }

        public async Task<ICollection<Item>> GetItemWithExpiredNamesAsync(int maxAge)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemWithExpiredNamesAsync(maxAge).ConfigureAwait(false));
            }
        }

        public ICollection<Item> GetItemWithExpiredNames(int maxAge)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemWithExpiredNames(maxAge);
            }
        }

        public async Task<ICollection<Item>> GetItemWithoutNamesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemWithoutNamesAsync().ConfigureAwait(false));
            }
        }

        public ICollection<Item> GetItemWithoutNames()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemWithoutNames();
            }
        }

        public async Task<ICollection<Title>> GetItemsPublishedAsync()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemsPublishedAsync().ConfigureAwait(false));
            }
        }

        public ICollection<Title> GetItemsPublished()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemsPublished();
            }
        }

        public async Task<Item> GetItemFilenamesAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemFilenamesAsync(itemID).ConfigureAwait(false));
            }
        }

        public Item GetItemFilenames(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemFilenames(itemID);
            }
        }

        public async Task<ICollection<Page>> GetItemPagesAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemPagesAsync(itemID).ConfigureAwait(false));
            }
        }

        public ICollection<Page> GetItemPages(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemPages(itemID);
            }
        }

        public async Task<ICollection<Institution>> GetItemInstitutionsByRoleAsync(int itemID, string roleName)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemInstitutionsByRoleAsync(itemID, roleName).ConfigureAwait(false));
            }
        }

        public ICollection<Institution> GetItemInstitutionsByRole(int itemID, string roleName)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemInstitutionsByRole(itemID, roleName);
            }
        }

        public async Task<ICollection<Institution>> GetItemNamesXmlAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemNamesXmlAsync(itemID).ConfigureAwait(false));
            }
        }

        public ICollection<Institution> GetItemNamesXml(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemNamesXml(itemID);
            }
        }

        public async Task DeleteItemNamesAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.DeleteItemNamesAsync(itemID).ConfigureAwait(false);
                return;
            }
        }

        public void DeleteItemNames(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.DeleteItemNames(itemID);
                return;
            }
        }

        public async Task UpdateItemLastPageNameLookupDateAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.UpdateItemLastPageNameLookupDateAsync(itemID).ConfigureAwait(false);
                return;
            }
        }

        public void UpdateItemLastPageNameLookupDate(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.UpdateItemLastPageNameLookupDate(itemID);
                return;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class ItemNameFileLogClient : ClientBase
    {
        public ItemNameFileLogClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task ItemNameFileLogRefreshAsync(DateTime sinceDate)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.ItemNameFileLogRefreshAsync(sinceDate).ConfigureAwait(false);
                return;
            }
        }

        public void ItemNameFileLogRefresh(DateTime sinceDate)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.ItemNameFileLogRefresh(sinceDate);
                return;
            }
        }

        public async Task<ICollection<ItemNameFileLog>> GetItemNameFileLogForCreateAsync()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemNameFileLogForCreateAsync().ConfigureAwait(false));
            }
        }

        public ICollection<ItemNameFileLog> GetItemNameFileLogForCreate()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemNameFileLogForCreate();
            }
        }

        public async Task<ICollection<ItemNameFileLog>> GetItemNameFileLogForUploadAsync()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemNameFileLogForUploadAsync().ConfigureAwait(false));
            }
        }

        public ICollection<ItemNameFileLog> GetItemNameFileLogForUpload()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemNameFileLogForUpload();
            }
        }

        public async Task UpdateItemNameFileLogAsync(int logID, ItemNameFileLogUpdateTarget updateTarget)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.UpdateItemNameFileLogAsync(logID, updateTarget).ConfigureAwait(false);
                return;
            }
        }

        public void UpdateItemNameFileLog(int logID, ItemNameFileLogUpdateTarget updateTarget)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.UpdateItemNameFileLog(logID, updateTarget);
                return;
            }
        }
    }
}

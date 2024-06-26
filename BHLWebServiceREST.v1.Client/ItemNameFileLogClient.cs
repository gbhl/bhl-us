﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class ItemNameFileLogClient : RestClient
    {
        public ItemNameFileLogClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task ItemNameFileLogRefreshAsync(DateTime sinceDate)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.ItemNameFileLogRefreshAsync(sinceDate).ConfigureAwait(false);
                return;
            }
        }

        public void ItemNameFileLogRefresh(DateTime sinceDate)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.ItemNameFileLogRefresh(sinceDate);
                return;
            }
        }

        public async Task<ICollection<ItemNameFileLog>> GetItemNameFileLogForCreateAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemNameFileLogForCreateAsync().ConfigureAwait(false));
            }
        }

        public ICollection<ItemNameFileLog> GetItemNameFileLogForCreate()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemNameFileLogForCreate();
            }
        }

        public async Task<ICollection<ItemNameFileLog>> GetItemNameFileLogForUploadAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetItemNameFileLogForUploadAsync().ConfigureAwait(false));
            }
        }

        public ICollection<ItemNameFileLog> GetItemNameFileLogForUpload()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetItemNameFileLogForUpload();
            }
        }

        public async Task UpdateItemNameFileLogCreateDateAsync(int logID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.UpdateItemNameFileLogCreateDateAsync(logID).ConfigureAwait(false);
                return;
            }
        }

        public void UpdateItemNameFileLogCreateDate(int logID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.UpdateItemNameFileLogCreateDate(logID);
                return;
            }
        }

        public async Task UpdateItemNameFileLogUploadDateAsync(int logID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.UpdateItemNameFileLogUploadDateAsync(logID).ConfigureAwait(false);
                return;
            }
        }

        public void UpdateItemNameFileLogUploadDate(int logID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.UpdateItemNameFileLogUploadDate(logID);
                return;
            }
        }
    }
}

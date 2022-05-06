using BHL.SiteServicesREST.v1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
//using System.Runtime.Serialization.Formatters.Binary;

namespace BHL.SiteServiceREST.v1.Client
{
    public class Client
    {
        private string _baseUrl = string.Empty;
        private string _segmentPdfUriFormat = "v1/Segments/{0}/Pdf";
        private string _itemPdfUriFormat = "v1/Items/{0}/Pdf";

        public Client(string baseUrl)
        { 
            _baseUrl = baseUrl; 
        }

        public async Task<string> GetItemTextAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return await restClient.GetItemTextAsync(itemID).ConfigureAwait(false);
            }
        }

        public string GetItemText(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return restClient.GetItemText(itemID);
            }
        }

        public async Task<Stream> GetItemPdfAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Stream pdf = null;
                string requestUri = string.Format(_itemPdfUriFormat, itemID.ToString());
                HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode) pdf = await response.Content.ReadAsStreamAsync();
                return pdf;
            }
        }

        public Stream GetItemPdf(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = string.Format(_itemPdfUriFormat, itemID.ToString());
                return Task.Run(async () => await httpClient.GetStreamAsync(requestUri)).GetAwaiter().GetResult();
            }
        }

        public async Task<ICollection<ViewerPageModel>> GetItemPageImageDimensionsAsync(int itemID, List<ViewerPageModel> pageModels)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return await restClient.GetItemPageImageDimensionsAsync(itemID, pageModels).ConfigureAwait(false);
            }
        }

        public ICollection<ViewerPageModel> GetItemPageImageDimensions(int itemID, List<ViewerPageModel> pageModels)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return restClient.GetItemPageImageDimensions(itemID, pageModels);
            }
        }

        public async Task<string> GetDOIFileAsync(string batchId, string type)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return await restClient.GetDOIFileAsync(batchId, type).ConfigureAwait(false);
            }
        }

        public string GetDOIFile(string batchId, string type)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return restClient.GetDOIFile(batchId, type);
            }
        }

        public async Task SendEmailAsync(MailRequestModel email)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                await restClient.EmailSendAsync(email).ConfigureAwait(false);
                return;
            }
        }

        public void SendEmail(MailRequestModel email)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                restClient.EmailSend(email);
                return;
            }
        }

        public async Task<string> GetMarcFileAsync(int id, string type)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return await restClient.GetMarcFileAsync(id, type).ConfigureAwait(false);
            }
        }

        public string GetMarcFile(int id, string type)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return restClient.GetMarcFile(id, type);
            }
        }

        public async Task CreateMarcFileAsync(string marcBibID, string content)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                await restClient.CreateMarcFileAsync(marcBibID, content).ConfigureAwait(false);
                return;
            }
        }

        public void CreateMarcFile(string marcBibID, string content)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                restClient.CreateMarcFile(marcBibID, content);
                return;
            }
        }

        public async Task<bool> OcrJobExistsAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return await restClient.OcrJobExistsAsync(itemID).ConfigureAwait(false);
            }
        }

        public bool OcrJobExists(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return restClient.OcrJobExists(itemID);
            }
        }

        public async Task CreateOcrJobAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                await restClient.CreateOcrJobAsync(itemID).ConfigureAwait(false);
                return;
            }
        }

        public void CreateOcrJob(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                restClient.CreateOcrJob(itemID);
                return;
            }
        }

        public async Task<string> GetPageTextAsync(int pageID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return await restClient.GetPageTextAsync(pageID).ConfigureAwait(false);
            }
        }

        public string GetPageText(int pageID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return restClient.GetPageText(pageID);
            }
        }

        public async Task PutQueueMessagesAsync(string queueName, List<string> messages)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                await restClient.PutQueueMessagesAsync(queueName, messages).ConfigureAwait(false);
                return;
            }
        }

        public void PutQueueMessages(string queueName, List<string> messages)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                PutQueueMessages(queueName, messages);
                return;
            }
        }

        public async Task<int> GetQueueMessageCountAsync(string queueName)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return await restClient.GetQueueMessageCountAsync(queueName).ConfigureAwait(false);
            }
        }

        public int GetQueueMessageCount(string queueName)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return restClient.GetQueueMessageCount(queueName);
            }
        }

        public async Task<string> GetSegmentTextAsync(int segmentID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return await restClient.GetSegmentTextAsync(segmentID).ConfigureAwait(false);
            }
        }

        public string GetSegmentText(int segmentID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return restClient.GetSegmentText(segmentID);
            }
        }

        public async Task<Stream> GetSegmentPdfAsync(int segmentID)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Stream pdf = null;
                string requestUri = string.Format(_segmentPdfUriFormat, segmentID.ToString());
                HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode) pdf = await response.Content.ReadAsStreamAsync();
                return pdf;
            }
        }

        public Stream GetSegmentPdf(int segmentID)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = string.Format(_segmentPdfUriFormat, segmentID.ToString());
                return Task.Run(async () => await httpClient.GetStreamAsync(requestUri)).GetAwaiter().GetResult();
            }
        }

        public async Task<ICollection<ViewerPageModel>> GetSegmentPageImageDimensionsAsync(int segmentID, List<ViewerPageModel> pageModels)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return await restClient.GetSegmentPageImageDimensionsAsync(segmentID, pageModels).ConfigureAwait(false);
            }
        }

        public ICollection<ViewerPageModel> GetSegmentPageImageDimensions(int segmentID, List<ViewerPageModel> pageModels)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                return restClient.GetSegmentPageImageDimensions(segmentID, pageModels);
            }
        }
    }
}

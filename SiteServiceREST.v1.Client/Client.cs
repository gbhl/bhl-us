using BHL.SiteServicesREST.v1;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;

namespace BHL.SiteServiceREST.v1.Client
{
    public class Client
    {
        private string _baseUrl = string.Empty;

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

        public async Task<byte[]> GetItemPdfAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                var restResponse = await restClient.GetItemPdfAsync(itemID).ConfigureAwait(false);

                //return await restClient.GetItemPdfAsync(itemID).ConfigureAwait(false);

                /*
                byte[] byteArray;
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, restResponse);
                    byteArray = stream.ToArray();
                    //byteArray = new byte[stream.Length];
                    //stream.Seek(0, SeekOrigin.Begin);
                    //stream.Read(byteArray, 0, (int)stream.Length);
                }

                return byteArray;
                */
                return string.IsNullOrWhiteSpace(restResponse) ? null : Encoding.UTF8.GetBytes(restResponse);
            }
        }

        public byte[] GetItemPdf(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                var restResponse = restClient.GetItemPdf(itemID);

                //return restClient.GetItemPdf(itemID);

                /*
                byte[] byteArray;
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, restResponse);
                    byteArray = stream.ToArray();
                    //byteArray = new byte[stream.Length];
                    //stream.Seek(0, SeekOrigin.Begin);
                    //stream.Read(byteArray, 0, (int)stream.Length);
                }

                return byteArray;
                */
                return string.IsNullOrWhiteSpace(restResponse) ? null : Encoding.UTF8.GetBytes(restResponse);
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

        public async Task<byte[]> GetSegmentPdfAsync(int segmentID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                var restResponse = await restClient.GetSegmentPdfAsync(segmentID).ConfigureAwait(false);
                return string.IsNullOrWhiteSpace(restResponse) ? null : Encoding.UTF8.GetBytes(restResponse);
            }
        }

        public byte[] GetSegmentPdf(int segmentID)
        {
            using (var httpClient = new HttpClient())
            {
                SiteService restClient = new SiteService(_baseUrl, httpClient);
                var restResponse = restClient.GetSegmentPdf(segmentID);
                return string.IsNullOrWhiteSpace(restResponse) ? null : Encoding.UTF8.GetBytes(restResponse);
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

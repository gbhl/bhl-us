using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class PdfClient : RestClient
    {
        public PdfClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<ICollection<PDF>> GetPdfsForCreationAsync()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetPdfsForCreationAsync().ConfigureAwait(false));
            }
        }

        public ICollection<PDF> GetPdfsForCreation()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetPdfsForCreation();
            }
        }

        public async Task<ICollection<PDF>> GetPdfsForDeletionAsync()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetPdfsForDeletionAsync().ConfigureAwait(false));
            }
        }

        public ICollection<PDF> GetPdfsForDeletion()
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetPdfsForDeletion();
            }
        }

        public async Task<PDF> UpdatePdfDeletionDateAsync(int pdfID, PdfModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.UpdatePdfDeletionDateAsync(pdfID, request).ConfigureAwait(false));
            }
        }

        public PDF UpdatePdfDeletionDate(int pdfID, PdfModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.UpdatePdfDeletionDate(pdfID, request);
            }
        }

        public async Task<PDF> UpdatePdfGenerationInfoAsync(int pdfID, PdfModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.UpdatePdfGenerationInfoAsync(pdfID, request).ConfigureAwait(false));
            }
        }

        public PDF UpdatePdfGenerationInfo(int pdfID, PdfModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.UpdatePdfGenerationInfo(pdfID, request);
            }
        }

        public async Task UpdatePdfStatusAsync(int pdfID, PdfModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                await restClient.UpdatePdfStatusAsync(pdfID, request).ConfigureAwait(false);
                return;
            }
        }

        public void UpdatePdfStatus(int pdfID, PdfModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                restClient.UpdatePdfStatus(pdfID, request);
                return;
            }
        }
    }
}

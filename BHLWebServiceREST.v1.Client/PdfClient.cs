using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class PdfClient : ClientBase
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

        public async Task<PDF> UpdatePdfAsync(int pdfID, PdfUpdateTarget target, PdfModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.UpdatePdfAsync(pdfID, target, request).ConfigureAwait(false));
            }
        }

        public PDF UpdatePdf(int pdfID, PdfUpdateTarget target, PdfModel request)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.UpdatePdf(pdfID, target, request);
            }
        }
    }
}

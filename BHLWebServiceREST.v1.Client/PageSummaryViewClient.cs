using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class PageSummaryViewClient : ClientBase
    {
        public PageSummaryViewClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<ICollection<PageSummaryView>> GetPageSummaryViewByPdfAysnc(int pdfID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetPageSummaryViewByPdfIDAsync(pdfID).ConfigureAwait(false));
            }
        }

        public ICollection<PageSummaryView> GetPageSummaryViewByPdf(int pdfID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetPageSummaryViewByPdfID(pdfID);
            }
        }
    }
}

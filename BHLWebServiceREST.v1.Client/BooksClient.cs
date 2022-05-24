using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class BooksClient : RestClient
    {
        public BooksClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<Book> GetBookAsync(int bookID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetBookAsync(bookID).ConfigureAwait(false));
            }
        }

        public Book GetBook(int bookID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetBook(bookID);
            }
        }

        public async Task<Book> GetBookByItemIDAsync(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetBookByItemIDAsync(itemID).ConfigureAwait(false));
            }
        }

        public Book GetBookByItemID(int itemID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetBookByItemID(itemID);
            }
        }

        public async Task<ICollection<Book>> GetBooksRecentlyChangedAsync(string sinceDate)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetBooksRecentlyChangedAsync(sinceDate).ConfigureAwait(false));
            }
        }

        public ICollection<Book> GetBooksRecentlyChanged(string sinceDate)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetBooksRecentlyChanged(sinceDate);
            }
        }

        public async Task<Item> GetBookFilenamesAsync(int bookID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetBookFilenamesAsync(bookID).ConfigureAwait(false));
            }
        }

        public Item GetBookFilenames(int bookID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetBookFilenames(bookID);
            }
        }

        public async Task<ICollection<Page>> GetBookPagesAsync(int bookID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetBookPagesAsync(bookID).ConfigureAwait(false));
            }
        }

        public ICollection<Page> GetBookPages(int bookID)
        {
            using (var httpClient = new HttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetBookPages(bookID);
            }
        }
    }
}

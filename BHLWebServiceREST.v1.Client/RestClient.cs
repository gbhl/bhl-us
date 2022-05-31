using System;
using System.Net.Http;

namespace BHL.WebServiceREST.v1.Client
{
    public class RestClient : IRestClient
    {
        protected string _baseUrl = string.Empty;
        public TimeSpan? Timeout { get; set; } = null;

        public RestClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        protected HttpClient GetHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = (TimeSpan)(Timeout ?? httpClient.Timeout);
            return httpClient;
        }
    }
}

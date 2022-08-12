using System;
using System.Net.Http;

namespace BHL.WebServiceREST.v1.Client
{
    public class RestClient : IRestClient
    {
        protected string _baseUrl = string.Empty;
        protected HttpClient _httpClient = null;
        
        public RestClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = GetHttpClient();
        }

        ~RestClient()
        {
            _httpClient.Dispose();
            _httpClient = null;
        }

        protected HttpClient GetHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            
            // Add any default HttpClient settings here

            return httpClient;
        }
    }
}

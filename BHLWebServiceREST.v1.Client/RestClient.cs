namespace BHL.WebServiceREST.v1.Client
{
    public class RestClient : IRestClient
    {
        protected string _baseUrl = string.Empty;

        public RestClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
    }
}

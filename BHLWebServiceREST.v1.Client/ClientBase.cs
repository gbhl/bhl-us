namespace BHL.WebServiceREST.v1.Client
{
    public class ClientBase
    {
        protected string _baseUrl = string.Empty;

        public ClientBase(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
    }
}

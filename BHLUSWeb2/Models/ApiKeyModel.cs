namespace MOBOT.BHL.Web2.Models
{
    public class ApiKeyModel
    {
        public int? ApiKeyID { get; set; }
        public string ContactName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string ApiKeyValue { get; set; } = string.Empty;
        public string ErrorText { get; set; } = string.Empty;
        public bool Success { get; set; } = false;

        public ApiKeyModel()
        {

        }

    }
}
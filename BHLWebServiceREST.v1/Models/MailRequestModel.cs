namespace BHL.WebServiceREST.v1.Models
{
    public class MailRequestModel
    {
        public string from { get; set; } = string.Empty;
        public List<string> to { get; set; } = new List<string>();
        public List<string> cc { get; set; } = new List<string>();
        public List<string> bcc { get; set; } = new List<string>();
        public string subject { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
    }
}

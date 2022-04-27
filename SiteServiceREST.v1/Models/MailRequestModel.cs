namespace BHL.SiteServicesREST.v1.Models
{
    public class MailRequestModel
    {
        public string from { get; set; }
        public string[] to { get; set; }
        public string[] cc { get; set; }
        public string[] bcc { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}

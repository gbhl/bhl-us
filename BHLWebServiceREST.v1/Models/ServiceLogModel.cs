namespace BHL.WebServiceREST.v1.Models
{
    public class ServiceLogModel
    {
        public DateTime logdate { get; set; }
        public string servicename { get; set; } = string.Empty;
        public string serviceparam { get; set; } = string.Empty;
        public string severityname { get; set; } = "Information";
        public int? errornumber { get; set; } = null;
        public string procedure { get; set; } = string.Empty;
        public int? line { get; set; } = null;
        public string message { get; set; } = string.Empty;
        public string stacktrace { get; set; } = string.Empty;
    }
}

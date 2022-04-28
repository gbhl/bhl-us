namespace BHL.SiteServicesREST.v1.Models
{
    public class ViewerPageModel
    {
        public string ExternalBaseUrl { get; set; } = string.Empty;
        public string AltExternalUrl { get; set; } = string.Empty;
        public string BarCode { get; set; } = string.Empty;
        public string FlickrUrl { get; set; } = String.Empty;
        public int? SequenceOrder { get; set; } = null;
        public int Width { get; set; } = 1600;
        public int Height { get; set; } = 2400;
    }
}

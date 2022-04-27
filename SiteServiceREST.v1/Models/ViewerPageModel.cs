namespace BHL.SiteServicesREST.v1.Models
{
    public class ViewerPageModel
    {
        public string ExternalBaseUrl { get; set; }
        public string AltExternalUrl { get; set; }
        public string BarCode { get; set; }
        public string FlickrUrl { get; set; }
        public int? SequenceOrder { get; set; }
        public int Width { get; set; } = 1600;
        public int Height { get; set; } = 2400;
    }
}

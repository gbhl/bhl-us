namespace BHL.WebServiceREST.v1.Models
{
    public class PdfModel
    {
        public int? pdfstatusid { get; set; } = null;
        public string fileLocation { get; set; } = null;
        public string fileUrl { get; set; } = null;
        public int? numberImagesMissing { get; set; } = null;
        public int? numberOcrMissing { get; set; } = null;
    }
}

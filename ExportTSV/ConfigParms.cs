using System.Configuration;

namespace BHL.Export.TSV
{
    public class ConfigParms
    {
        public string DOIFile { get; set; } = string.Empty;
        public string AuthorFile { get; set; } = string.Empty;
        public string ItemFile { get; set; } = string.Empty;
        public string PageFile { get; set; } = string.Empty;
        public string PageNameFile { get; set; } = string.Empty;
        public string PartFile { get; set; } = string.Empty;
        public string PartAuthorFile { get; set; } = string.Empty;
        public string KeywordFile { get; set; } = string.Empty;
        public string TitleFile { get; set; } = string.Empty;
        public string TitleIdentifierFile { get; set; } = string.Empty;
        public string ItemUrlFormat { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            DOIFile = ConfigurationManager.AppSettings["TSVDOIFile"];
            AuthorFile = ConfigurationManager.AppSettings["TSVAuthorFile"];
            ItemFile = ConfigurationManager.AppSettings["TSVItemFile"];
            PageFile = ConfigurationManager.AppSettings["TSVPageFile"];
            PageNameFile = ConfigurationManager.AppSettings["TSVPageNameFile"];
            PartFile = ConfigurationManager.AppSettings["TSVPartFile"];
            PartAuthorFile = ConfigurationManager.AppSettings["TSVPartAuthorFile"];
            KeywordFile = ConfigurationManager.AppSettings["TSVKeywordFile"];
            TitleFile = ConfigurationManager.AppSettings["TSVTitleFile"];
            TitleIdentifierFile = ConfigurationManager.AppSettings["TSVTitleIdentifierFile"];
            ItemUrlFormat = ConfigurationManager.AppSettings["TSVItemUrlFormat"];
        }
    }
}

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
        public string PartIdentifierFile { get; set; } = string.Empty;
        public string KeywordFile { get; set; } = string.Empty;
        public string TitleFile { get; set; } = string.Empty;
        public string TitleIdentifierFile { get; set; } = string.Empty;
        public string AuthorIdentifierFile { get; set; } = string.Empty;
        public string ItemUrlFormat { get; set; } = string.Empty;
        public string ItemTextUrlFormat { get; set; } = string.Empty;
        public string ItemPDFUrlFormat { get; set; } = string.Empty;
        public string ItemImagesUrlFormat { get; set; } = string.Empty;

        public string InternalDOIFile { get; set; } = string.Empty;
        public string InternalAuthorFile { get; set; } = string.Empty;
        public string InternalItemFile { get; set; } = string.Empty;
        public string InternalPartFile { get; set; } = string.Empty;
        public string InternalPartAuthorFile { get; set; } = string.Empty;
        public string InternalPartIdentifierFile { get; set; } = string.Empty;
        public string InternalKeywordFile { get; set; } = string.Empty;
        public string InternalTitleFile { get; set; } = string.Empty;
        public string InternalTitleIdentifierFile { get; set; } = string.Empty;
        public string InternalAuthorIdentifierFile { get; set; } = string.Empty;


        public void LoadAppConfig()
        {
            DOIFile = ConfigurationManager.AppSettings["TSVDOIFile"];
            AuthorFile = ConfigurationManager.AppSettings["TSVAuthorFile"];
            ItemFile = ConfigurationManager.AppSettings["TSVItemFile"];
            PageFile = ConfigurationManager.AppSettings["TSVPageFile"];
            PageNameFile = ConfigurationManager.AppSettings["TSVPageNameFile"];
            PartFile = ConfigurationManager.AppSettings["TSVPartFile"];
            PartAuthorFile = ConfigurationManager.AppSettings["TSVPartAuthorFile"];
            PartIdentifierFile = ConfigurationManager.AppSettings["TSVPartIdentifierFile"];
            KeywordFile = ConfigurationManager.AppSettings["TSVKeywordFile"];
            TitleFile = ConfigurationManager.AppSettings["TSVTitleFile"];
            TitleIdentifierFile = ConfigurationManager.AppSettings["TSVTitleIdentifierFile"];
            AuthorIdentifierFile = ConfigurationManager.AppSettings["TSVAuthorIdentifierFile"];
            ItemUrlFormat = ConfigurationManager.AppSettings["TSVItemURLFormat"];
            ItemTextUrlFormat = ConfigurationManager.AppSettings["TSVItemTextURLFormat"];
            ItemPDFUrlFormat = ConfigurationManager.AppSettings["TSVItemPDFURLFormat"];
            ItemImagesUrlFormat = ConfigurationManager.AppSettings["TSVItemImagesURLFormat"];

            InternalDOIFile = ConfigurationManager.AppSettings["TSVInternalDOIFile"];
            InternalAuthorFile = ConfigurationManager.AppSettings["TSVInternalAuthorFile"];
            InternalItemFile = ConfigurationManager.AppSettings["TSVInternalItemFile"];
            InternalPartFile = ConfigurationManager.AppSettings["TSVInternalPartFile"];
            InternalPartAuthorFile = ConfigurationManager.AppSettings["TSVInternalPartAuthorFile"];
            InternalPartIdentifierFile = ConfigurationManager.AppSettings["TSVInternalPartIdentifierFile"];
            InternalKeywordFile = ConfigurationManager.AppSettings["TSVInternalKeywordFile"];
            InternalTitleFile = ConfigurationManager.AppSettings["TSVInternalTitleFile"];
            InternalTitleIdentifierFile = ConfigurationManager.AppSettings["TSVInternalTitleIdentifierFile"];
            InternalAuthorIdentifierFile = ConfigurationManager.AppSettings["TSVInternalAuthorIdentifierFile"];
        }
    }
}

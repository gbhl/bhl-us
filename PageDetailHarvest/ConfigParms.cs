using System.Configuration;

namespace PageDetailHarvest
{
    public class ConfigParms
    {
        public string SMTPHost { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailToAddress { get; set; }

        public string Mode { get; set; }

        public string ItemUrlFormat { get; set; }
        public string PageUrlFormat { get; set; }

        public string ExtractInputFolder { get; set; }
        public string ExtractErrorFolder { get; set; }
        public string ExtractLoadedFolder { get; set; }
        public string ExtractCompleteFolder { get; set; }
        public string ClassifierOutputFolder { get; set; }

        public void LoadAppConfig()
        {
            SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            Mode = ConfigurationManager.AppSettings["Mode"];
            ItemUrlFormat = ConfigurationManager.AppSettings["ItemUrlFormat"];
            PageUrlFormat = ConfigurationManager.AppSettings["PageUrlFormat"];
            ExtractInputFolder = ConfigurationManager.AppSettings["ExtractInputFolder"];
            ExtractErrorFolder = ConfigurationManager.AppSettings["ExtractErrorFolder"];
            ExtractLoadedFolder = ConfigurationManager.AppSettings["ExtractLoadedFolder"];
            ExtractCompleteFolder = ConfigurationManager.AppSettings["ExtractCompleteFolder"];
            ClassifierOutputFolder = ConfigurationManager.AppSettings["ClassifierOutputFolder"];
        }
    }
}

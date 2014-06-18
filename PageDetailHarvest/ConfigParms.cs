using System;
using System.Configuration;

namespace PageDetailHarvest
{
    public class ConfigParms
    {
        public string SMTPHost { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailToAddress { get; set; }

        public string Mode { get; set; }

        public string ItemUrlPrefix { get; set; }
        public string PageUrlPrefix { get; set; }

        public string ExtractInputFolder { get; set; }
        public string ExtractErrorFolder { get; set; }
        public string ExtractLoadedFolder { get; set; }
        public string ExtractCompleteFolder { get; set; }
        public string ClassifierOutputFolder { get; set; }
        public string ClassifierInputFolder { get; set; }
        public string ClassifierCompleteFolder { get; set; }
        public string ClassifierErrorFolder { get; set; }

        public int DefaultUserID { get; set; }
        public int ExtractionUserID { get; set; }
        public int ClassifierUserID { get; set; }
        public int DescriptorUserID { get; set; }

        public string PageTypeIllustration { get; set; }

        public string ClassifierIncomingFolder { get; set; }

        public string FtpIncomingFolder { get; set; }
        public string FtpUsername { get; set; }
        public string FtpPassword { get; set; }

        public int ClassifierOutputFilePageLimit { get; set; }

        public void LoadAppConfig()
        {
            SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            Mode = ConfigurationManager.AppSettings["Mode"];
            ItemUrlPrefix = ConfigurationManager.AppSettings["ItemUrlPrefix"];
            PageUrlPrefix = ConfigurationManager.AppSettings["PageUrlPrefix"];
            ExtractInputFolder = ConfigurationManager.AppSettings["ExtractInputFolder"];
            ExtractErrorFolder = ConfigurationManager.AppSettings["ExtractErrorFolder"];
            ExtractLoadedFolder = ConfigurationManager.AppSettings["ExtractLoadedFolder"];
            ExtractCompleteFolder = ConfigurationManager.AppSettings["ExtractCompleteFolder"];
            ClassifierOutputFolder = ConfigurationManager.AppSettings["ClassifierOutputFolder"];
            ClassifierInputFolder = ConfigurationManager.AppSettings["ClassifierInputFolder"];
            ClassifierCompleteFolder = ConfigurationManager.AppSettings["ClassifierCompleteFolder"];
            ClassifierErrorFolder = ConfigurationManager.AppSettings["ClassifierErrorFolder"];
            DefaultUserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUserID"]);
            ExtractionUserID = Convert.ToInt32(ConfigurationManager.AppSettings["ExtractionUserID"]);
            ClassifierUserID = Convert.ToInt32(ConfigurationManager.AppSettings["ClassifierUserID"]);
            DescriptorUserID = Convert.ToInt32(ConfigurationManager.AppSettings["DescriptorUserID"]);
            PageTypeIllustration = ConfigurationManager.AppSettings["PageTypeIllustration"];
            ClassifierIncomingFolder = ConfigurationManager.AppSettings["ClassifierIncomingFolder"];
            FtpIncomingFolder = ConfigurationManager.AppSettings["FtpIncomingFolder"];
            FtpUsername = ConfigurationManager.AppSettings["FtpUsername"];
            FtpPassword = ConfigurationManager.AppSettings["FtpPassword"];
            ClassifierOutputFilePageLimit = Convert.ToInt32(ConfigurationManager.AppSettings["ClassifierOutputFilePageLimit"]);
        }
    }
}

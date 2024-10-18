using System;
using System.Configuration;
using System.Xml;

namespace MOBOT.BHL.BHLPDFGenerator
{
    class ConfigParms
    {
        public string EmailFromAddress { get; set; } = string.Empty;
        public string EmailToAddress { get; set; } = string.Empty;
        public bool EmailOnError { get; set; } = true;
        public string PdfFilePath { get; set; } = string.Empty;
        public string PdfUrl { get; set; } = string.Empty;
        public string OcrTextLocation { get; set; } = string.Empty;
        public int RetryImageWait { get; set; } = 0;
        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            XmlDocument doc = new XmlDocument();
            string configPath = AppDomain.CurrentDomain.FriendlyName + ".config";
            doc.Load(configPath);
            foreach (XmlNode node in doc["configuration"]["appSettings"])
            {
                if (node.Name == "add")
                {
                    EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
                    EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
                    EmailOnError = ConfigurationManager.AppSettings["EmailOnError"].ToLower() == "true";
                    PdfFilePath = ConfigurationManager.AppSettings["PdfFilePath"];
                    PdfUrl = ConfigurationManager.AppSettings["PdfUrl"];
                    OcrTextLocation = ConfigurationManager.AppSettings["OCRTextLocation"];
                    RetryImageWait = Convert.ToInt32(ConfigurationManager.AppSettings["RetryImageWait"]);
                    BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
                }
            }
        }
    }
}

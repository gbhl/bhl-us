using System;
using System.Configuration;
using System.Xml;

namespace MOBOT.BHL.PageNameRefresh
{
    public class ConfigParms
    {
        public string OcrTextPath { get; set; } = string.Empty;
        public string OcrTextLocation { get; set; } = string.Empty;
        public string EmailFromAddress { get; set; } = string.Empty;
        public string EmailToAddress { get; set; } = string.Empty;
        public bool EmailOnError { get; set; } = false;
        public int MaximumPageNameAge { get; set; } = 0;
        public int ExternalWebServiceInterval { get; set; } = 0;
        public string NameServiceSourceName { get; set; } = string.Empty;
        public bool DoAsync { get; set; } = false;
        public int MaxConcurrent { get; set; } = 2;
        public string Mode { get; set; } = string.Empty;
        public string Item { get; set; } = string.Empty;
        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            this.OcrTextPath = ConfigurationManager.AppSettings["OCRTextPath"];
            this.OcrTextLocation = ConfigurationManager.AppSettings["OCRTextLocation"];
            this.EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            this.EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            this.EmailOnError = ConfigurationManager.AppSettings["EmailOnError"].ToLower() == "true";
            this.MaximumPageNameAge = Convert.ToInt32(ConfigurationManager.AppSettings["MaximumPageNameAge"]);
            this.ExternalWebServiceInterval = Convert.ToInt32(ConfigurationManager.AppSettings["ExternalWebServiceInterval"]);
            this.NameServiceSourceName = ConfigurationManager.AppSettings["NameServiceSourceName"];
            this.DoAsync = (ConfigurationManager.AppSettings["MaxConcurrent"] == "true");
            this.MaxConcurrent = Convert.ToInt32(ConfigurationManager.AppSettings["MaxConcurrent"]);
            this.Mode = ConfigurationManager.AppSettings["Mode"];
            this.Item = ConfigurationManager.AppSettings["Item"];
            this.BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
        }
    }
}

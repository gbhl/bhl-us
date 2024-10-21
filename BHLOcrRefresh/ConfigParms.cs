using System.Configuration;

namespace MOBOT.BHL.BHLOcrRefresh
{
    class ConfigParms
    {
        public string SMTPHost { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailToAddress { get; set; }
        public bool EmailOnError { get; set; }
        public string OcrJobNewPath { get; set; }
        public string OcrJobProcessingPath { get; set; }
        public string OcrJobCompletePath { get; set; }
        public string OcrJobErrorPath { get; set; }
        public string OcrJobTempPath { get; set; }
        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            this.SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            this.EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            this.EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            this.EmailOnError = ConfigurationManager.AppSettings["EmailOnError"].ToLower() == "true";
            this.OcrJobNewPath = ConfigurationManager.AppSettings["OcrJobNewPath"];
            this.OcrJobProcessingPath = ConfigurationManager.AppSettings["OcrJobProcessingPath"];
            this.OcrJobCompletePath = ConfigurationManager.AppSettings["OcrJobCompletePath"];
            this.OcrJobErrorPath = ConfigurationManager.AppSettings["OcrJobErrorPath"];
            this.OcrJobTempPath = ConfigurationManager.AppSettings["OcrJobTempPath"];
            this.BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
        }
    }
}

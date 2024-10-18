using System;
using System.Configuration;

namespace BHL.TextImportProcessor
{
    public class ConfigParms
    {
        public bool DebugMode { get; set; }
        public string DebugPath { get; set; }
        public string SMTPHost { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailToAddress { get; set; }
        public bool EmailOnError { get; set; }
        public string TextFilePath { get; set; }
        public int TextImportBatchStatusImported { get; set; }
        public int TextImportBatchFileStatusImported { get; set; }
        public int TextImportBatchFileStatusError { get; set; }
        public string TextImportFilePath { get; set; }
        public string TextImportLocalFilePath { get; set; }
        public string BHLWSEndpoint { get; set; } = string.Empty;

        public ConfigParms()
        {
            DebugMode = true;
            DebugPath = string.Empty;
            SMTPHost = string.Empty;
            EmailFromAddress = string.Empty;
            EmailToAddress = string.Empty;
            EmailOnError = true;
            TextFilePath = string.Empty;
            TextImportBatchStatusImported = 0;
            TextImportBatchFileStatusImported = 0;
            TextImportBatchFileStatusError = 0;
            TextImportFilePath = string.Empty;
            TextImportLocalFilePath = string.Empty;
            BHLWSEndpoint = String.Empty;
        }

        public void LoadAppConfig()
        {
            this.DebugMode = (ConfigurationManager.AppSettings["DebugMode"].ToUpper() != "FALSE");
            this.DebugPath = ConfigurationManager.AppSettings["DebugPath"];
            this.SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            this.EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            this.EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            this.EmailOnError = ConfigurationManager.AppSettings["EmailOnError"].ToLower() == "true";
            this.TextFilePath = ConfigurationManager.AppSettings["OCRTextLocation"];
            this.TextImportBatchStatusImported = Convert.ToInt32(ConfigurationManager.AppSettings["TextImportBatchStatusImported"]);
            this.TextImportBatchFileStatusImported = Convert.ToInt32(ConfigurationManager.AppSettings["TextImportBatchFileStatusImported"]);
            this.TextImportBatchFileStatusError = Convert.ToInt32(ConfigurationManager.AppSettings["TextImportBatchFileStatusError"]);
            this.TextImportFilePath = ConfigurationManager.AppSettings["TextImportFilePath"];
            this.TextImportLocalFilePath = ConfigurationManager.AppSettings["TextImportLocalFilePath"];
            this.BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
        }
    }
}

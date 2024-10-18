using System;
using System.IO;
using System.Configuration;

namespace MOBOT.BHL.BHLMETSUpload
{
    public class ConfigParms
    {
        public string UploadFilePath { get; set; }
        public string MetsEmail { get; set; }
        public string IA3AccessKey { get; set; }
        public string IA3SecretKey { get; set; }
        public string IAFileName { get; set; }
        public string SMTPHost { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailToAddress { get; set; }
        public bool EmailOnError { get; set; }
        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            this.UploadFilePath = Path.GetTempPath();
            MetsEmail = ConfigurationManager.AppSettings["METSEmail"];
            IA3AccessKey = ConfigurationManager.AppSettings["IAS3AccessKey"];
            IA3SecretKey = ConfigurationManager.AppSettings["IAS3SecretKey"];
            IAFileName = ConfigurationManager.AppSettings["IAFileName"];
            SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            EmailOnError = StringToBool(ConfigurationManager.AppSettings["EmailOnError"]);
            BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
        }

        private bool StringToBool(string value)
        {
            switch (value)
            {
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    return true;
            }
        }
    }
}

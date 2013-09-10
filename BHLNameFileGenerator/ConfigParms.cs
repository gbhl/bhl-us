using System;
using System.Configuration;
using System.Xml;

namespace MOBOT.BHL.BHLNameFileGenerator
{
    public class ConfigParms
    {
        public const string SMTPHostKey = "SMTPHost";
        public const string EmailFromAddressKey = "EmailFromAddress";
        public const string EmailToAddressKey = "EmailToAddress";
        public const string GetItemsKey = "GetItems";
        public const string CreateFilesKey = "CreateFiles";
        public const string UploadFilesKey = "UploadFiles";
        public const string LastGetItemsDateKey = "LastGetItemsDate";
        public const string NameFileFormatKey = "NameFileFormat";
        public const string NameFilePathFormatKey = "NameFilePathFormat";
        public const string IAS3AccessKeyKey = "IAS3AccessKey";
        public const string IAS3SecretKeyKey = "IAS3AccessKey";

        private string _smtpHost = "";
        public string SMTPHost
        {
            get { return _smtpHost; }
            set { _smtpHost = value; }
        }

        private string _emailFromAddress = "";
        public string EmailFromAddress
        {
            get { return _emailFromAddress; }
            set { _emailFromAddress = value; }
        }

        private string _emailToAddress = "";
        public string EmailToAddress
        {
            get { return _emailToAddress; }
            set { _emailToAddress = value; }
        }

        private bool _getItems = true;
        public bool GetItems
        {
            get { return _getItems; }
            set { _getItems = value; }
        }

        private bool _createFiles = true;
        public bool CreateFiles
        {
            get { return _createFiles; }
            set { _createFiles = value; }
        }

        private bool _uploadFiles = true;
        public bool UploadFiles
        {
            get { return _uploadFiles; }
            set { _uploadFiles = value; }
        }

        private DateTime _lastGetItemsDate = DateTime.Now;
        public DateTime LastGetItemsDate
        {
            get { return _lastGetItemsDate; }
            set { _lastGetItemsDate = value; }
        }

        private string _nameFileFormat = string.Empty;
        public string NameFileFormat
        {
            get { return _nameFileFormat; }
            set { _nameFileFormat = value; }
        }

        private string _nameFilePathFormat = string.Empty;
        public string NameFilePathFormat
        {
            get { return _nameFilePathFormat; }
            set { _nameFilePathFormat = value; }
        }

        private string _iaS3AccessKey = string.Empty;
        public string IaS3AccessKey
        {
            get { return _iaS3AccessKey; }
            set { _iaS3AccessKey = value; }
        }

        private string _iaS3SecretKey = string.Empty;
        public string IaS3SecretKey
        {
            get { return _iaS3SecretKey; }
            set { _iaS3SecretKey = value; }
        }

        public void LoadAppConfig()
        {
            this.SMTPHost = ConfigurationManager.AppSettings[SMTPHostKey];
            this.EmailFromAddress = ConfigurationManager.AppSettings[EmailFromAddressKey];
            this.EmailToAddress = ConfigurationManager.AppSettings[EmailToAddressKey];
            this.GetItems = this.StringToBool(ConfigurationManager.AppSettings[GetItemsKey]);
            this.CreateFiles = this.StringToBool(ConfigurationManager.AppSettings[CreateFilesKey]);
            this.UploadFiles = this.StringToBool(ConfigurationManager.AppSettings[UploadFilesKey]);
            this.LastGetItemsDate = Convert.ToDateTime(ConfigurationManager.AppSettings[LastGetItemsDateKey]);
            this.NameFileFormat = ConfigurationManager.AppSettings[NameFileFormatKey];
            this.NameFilePathFormat = ConfigurationManager.AppSettings[NameFilePathFormatKey];
            this.IaS3AccessKey = ConfigurationManager.AppSettings[IAS3AccessKeyKey];
            this.IaS3SecretKey = ConfigurationManager.AppSettings[IAS3SecretKeyKey];
        }

        public void UpdateAppSetting(string key, object value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            this.LoadAppConfig();
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

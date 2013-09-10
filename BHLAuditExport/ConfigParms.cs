using System;
using System.Configuration;
using System.Xml;

namespace MOBOT.BHL.BHLAuditExport
{
    class ConfigParms
    {
        public const string SMTPHostKey = "SMTPHost";
        public const string EmailFromAddressKey = "EmailFromAddress";
        public const string EmailToAddressKey = "EmailToAddress";
        public const string LastExportDateKey = "LastExportDate";
        public const string TempFolderKey = "TempFolder";
        public const string JSONFilenameFormatKey = "JSONFilenameFormat";
        public const string ZIPFilenameFormatKey = "ZIPFilenameFormat";
        public const string WebFolderKey = "WebFolder";
        public const string AuditExportManifestKey = "AuditExportManifest";

        private string _smtpHost = string.Empty;
        public string SMTPHost
        {
            get { return _smtpHost; }
            set { _smtpHost = value; }
        }

        private string _emailFromAddress = string.Empty;
        public string EmailFromAddress
        {
            get { return _emailFromAddress; }
            set { _emailFromAddress = value; }
        }

        private string _emailToAddress = string.Empty;
        public string EmailToAddress
        {
            get { return _emailToAddress; }
            set { _emailToAddress = value; }
        }

        private DateTime _lastExportDate = DateTime.Now;

        public DateTime LastExportDate
        {
            get { return _lastExportDate; }
            set { _lastExportDate = value; }
        }

        private string _tempFolder = string.Empty;

        public string TempFolder
        {
            get { return _tempFolder; }
            set { _tempFolder = value; }
        }

        private string _jsonFilenameFormat = string.Empty;

        public string JsonFilenameFormat
        {
            get { return _jsonFilenameFormat; }
            set { _jsonFilenameFormat = value; }
        }

        private string _zipFilenameFormat = string.Empty;

        public string ZipFilenameFormat
        {
            get { return _zipFilenameFormat; }
            set { _zipFilenameFormat = value; }
        }

        private string _webFolder = string.Empty;

        public string WebFolder
        {
            get { return _webFolder; }
            set { _webFolder = value; }
        }

        private string _auditExportManifest = string.Empty;

        public string AuditExportManifest
        {
            get { return _auditExportManifest; }
            set { _auditExportManifest = value; }
        }

        public void LoadAppConfig()
        {
            this.SMTPHost = ConfigurationManager.AppSettings[SMTPHostKey];
            this.EmailFromAddress = ConfigurationManager.AppSettings[EmailFromAddressKey];
            this.EmailToAddress = ConfigurationManager.AppSettings[EmailToAddressKey];
            this.LastExportDate = Convert.ToDateTime(ConfigurationManager.AppSettings[LastExportDateKey]);
            this.TempFolder = ConfigurationManager.AppSettings[TempFolderKey];
            this.JsonFilenameFormat = ConfigurationManager.AppSettings[JSONFilenameFormatKey];
            this.ZipFilenameFormat = ConfigurationManager.AppSettings[ZIPFilenameFormatKey];
            this.WebFolder = ConfigurationManager.AppSettings[WebFolderKey];
            this.AuditExportManifest = ConfigurationManager.AppSettings[AuditExportManifestKey];
        }

        public void UpdateAppSetting(string key, object value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            this.LoadAppConfig();
        }
    }
}

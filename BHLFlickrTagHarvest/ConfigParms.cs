using System;
using System.Configuration;
using System.IO;

namespace BHLFlickrTagHarvest
{
    public class ConfigParms
    {
        public string EmailFromAddress { get; set; }
        public string EmailToAddress { get; set; }
        public string FlickrApiKey { get; set; }
        public string BHLFlickrUserID { get; set; }
        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            FlickrApiKey = ConfigurationManager.AppSettings["FlickrApiKey"];
            BHLFlickrUserID = ConfigurationManager.AppSettings["BHLFlickrUserID"];
            BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
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

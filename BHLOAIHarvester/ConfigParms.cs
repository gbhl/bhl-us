using System.Configuration;

namespace BHLOAIHarvester
{
    public class ConfigParms
    {
        public string EmailFromAddress { get; set; }
        public string EmailToAddress { get; set; }
        public bool EmailOnError { get; set; }

        public string HarvestSetID { get; set; }
        public string FromDate { get; set; }
        public string UntilDate { get; set; }
        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            EmailOnError = ConfigurationManager.AppSettings["EmailOnError"].ToLower() == "true";
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

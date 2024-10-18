using System;
using System.IO;
using System.Configuration;
using BHL.WebServiceREST.v1;

namespace MOBOT.BHL.BHLBioStorHarvest
{
    public class ConfigParms
    {
        public string EmailFromAddress { get; set; }
        public string EmailToAddress { get; set; }
        public bool EmailOnError { get; set; }

        public string Mode { get; set; }
        public DateTime DateSince { get; set; }
        public int BHLItemID { get; set; }
        public string File { get; set; }

        public int ImportSourceID { get; set; }

        public int SegmentStatusHarvestedID { get; set; }
        public int SegmentStatusPublishedID { get; set; }
        public int SegmentStatusSkippedID { get; set; }
        
        public bool NoDownload { get; set; }
        public bool NoPublish { get; set; }
        public bool NoCluster { get; set; }

        public string JsonFolder { get; set; }
        public string JsonFileFormat { get; set; }

        public string BioStorItemArticlesUrl { get; set; }
        public string BioStorItemsChangedSinceUrl { get; set; }
        public string CrossRefOpenUrlDOIGet { get; set; }
        
        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            EmailOnError = StringToBool(ConfigurationManager.AppSettings["EmailOnError"]);

            Mode = ConfigurationManager.AppSettings["Mode"];
            DateSince = Convert.ToDateTime(ConfigurationManager.AppSettings["SinceDate"]);
            BHLItemID = Convert.ToInt32(ConfigurationManager.AppSettings["BHLItemID"]);
            File = ConfigurationManager.AppSettings["File"];

            ImportSourceID = Convert.ToInt32(ConfigurationManager.AppSettings["ImportSourceID"]);
            SegmentStatusHarvestedID = Convert.ToInt32(ConfigurationManager.AppSettings["SegmentStatusHarvestedID"]);
            SegmentStatusPublishedID = Convert.ToInt32(ConfigurationManager.AppSettings["SegmentStatusPublishedID"]);
            SegmentStatusSkippedID = Convert.ToInt32(ConfigurationManager.AppSettings["SegmentStatusSkippedID"]);

            NoDownload = (ConfigurationManager.AppSettings["NoDownload"] == "true");
            NoPublish = (ConfigurationManager.AppSettings["NoPublish"] == "true");
            NoCluster = (ConfigurationManager.AppSettings["NoCluster"] == "true");

            JsonFolder = ConfigurationManager.AppSettings["JsonFolder"];
            JsonFileFormat = ConfigurationManager.AppSettings["JsonFileFormat"];

            BioStorItemArticlesUrl = ConfigurationManager.AppSettings["BioStorItemArticlesUrl"];
            BioStorItemsChangedSinceUrl = ConfigurationManager.AppSettings["BioStorItemsChangedSinceUrl"];
            CrossRefOpenUrlDOIGet = ConfigurationManager.AppSettings["CrossRefOpenUrlDOIGet"];

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

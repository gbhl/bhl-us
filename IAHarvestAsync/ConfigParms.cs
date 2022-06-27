using System;
using System.Configuration;
using System.Xml;

namespace IAHarvestAsync
{
    public class ConfigParms
    {
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

        private bool _upload = false;
        public bool Upload
        {
            get { return _upload; }
            set { _upload = value; }
        }

        private bool _download = true;
        public bool Download
        {
            get { return _download; }
            set { _download = value; }
        }

        private bool _downloadAll = true;
        public bool DownloadAll
        {
            get { return _downloadAll; }
            set { _downloadAll = value; }
        }

        private bool _quiet = true;
        public bool Quiet
        {
            get { return _quiet; }
            set { _quiet = value; }
        }

        private string _searchListIdentifiersUrl;
        public string SearchListIdentifiersUrl
        {
            get { return _searchListIdentifiersUrl; }
            set { _searchListIdentifiersUrl = value; }
        }

        private string _localFileFolder;
        public string LocalFileFolder
        {
            get { return _localFileFolder; }
            set { _localFileFolder = value; }
        }

        private string _iaHarvestExecutable = "IAHarvest.exe";

        public string IAHarvestExecutable
        {
            get { return _iaHarvestExecutable; }
            set { _iaHarvestExecutable = value; }
        }

        private string _iaHarvestProcessName = "IAHarvest";

        public string IAHarvestProcessName
        {
            get { return _iaHarvestProcessName; }
            set { _iaHarvestProcessName = value; }
        }

        private int _iaHarvestMaxInstances = 2;

        public int IAHarvestMaxInstances
        {
            get { return _iaHarvestMaxInstances; }
            set { _iaHarvestMaxInstances = value; }
        }

        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            DownloadAll = Convert.ToBoolean(ConfigurationManager.AppSettings["DownloadAll"]);
            Download = Convert.ToBoolean(ConfigurationManager.AppSettings["DownloadItem"]);
            Upload = Convert.ToBoolean(ConfigurationManager.AppSettings["UploadItem"]);
            Quiet = Convert.ToBoolean(ConfigurationManager.AppSettings["Quiet"]);
            SearchListIdentifiersUrl = ConfigurationManager.AppSettings["SearchListIdentifiersUrl"];
            LocalFileFolder = ConfigurationManager.AppSettings["LocalFileFolder"];
            IAHarvestExecutable = ConfigurationManager.AppSettings["IAHarvestExecutable"];
            IAHarvestProcessName = ConfigurationManager.AppSettings["IAHarvestProcessName"];
            IAHarvestMaxInstances = Convert.ToInt32(ConfigurationManager.AppSettings["IAHarvestMaxInstances"]);
            BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
        }
    }
}

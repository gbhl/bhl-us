using System;
using System.Xml;

namespace IAHarvestAsync
{
    public class ConfigParms
    {
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
            XmlDocument doc = new XmlDocument();
            string configPath = AppDomain.CurrentDomain.FriendlyName + ".config";
            doc.Load(configPath);
            foreach (XmlNode node in doc["configuration"]["appSettings"])
            {
                if (node.Name == "add")
                {
                    if (node.Attributes.GetNamedItem("key").Value == "SMTPHost")
                    {
                        this.SMTPHost = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EmailFromAddress")
                    {
                        this.EmailFromAddress = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EmailToAddress")
                    {
                        this.EmailToAddress = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DownloadAll")
                    {
                        this.DownloadAll = Convert.ToBoolean(node.Attributes.GetNamedItem("value").Value.ToLower());
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DownloadItem")
                    {
                        this.Download = Convert.ToBoolean(node.Attributes.GetNamedItem("value").Value.ToLower());
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "UploadItem")
                    {
                        this.Upload = Convert.ToBoolean(node.Attributes.GetNamedItem("value").Value.ToLower());
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "Quiet")
                    {
                        this.Quiet = Convert.ToBoolean(node.Attributes.GetNamedItem("value").Value.ToLower());
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "SearchListIdentifiersUrl")
                    {
                        this.SearchListIdentifiersUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "LocalFileFolder")
                    {
                        this.LocalFileFolder = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "IAHarvestExecutable")
                    {
                        this.IAHarvestExecutable = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "IAHarvestProcessName")
                    {
                        this.IAHarvestProcessName = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "IAHarvestMaxInstances")
                    {
                        this.IAHarvestMaxInstances = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BHLWSUrl")
                    {
                        this.BHLWSEndpoint = node.Attributes.GetNamedItem("value").Value;
                    }
                }
            }
        }
    }
}

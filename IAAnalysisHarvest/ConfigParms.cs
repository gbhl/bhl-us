using System;
using System.Xml;

namespace IAAnalysisHarvest
{
    public class ConfigParms
    {
        private DateTime? _startDate = null;

        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime? _endDate = null;

        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        private string _emailFromAddress = "";
        public string EmailFromAddress
        {
            get
            {
                return _emailFromAddress;
            }
            set
            {
                _emailFromAddress = value;
            }
        }

        private string _emailToAddress = "";
        public string EmailToAddress
        {
            get
            {
                return _emailToAddress;
            }
            set
            {
                _emailToAddress = value;
            }
        }

        private bool _downloadIDs;
        public bool DownloadIDs
        {
            get { return _downloadIDs; }
            set { _downloadIDs = value; }
        }

        private bool _getXml;
        public bool GetXml
        {
            get { return _getXml; }
            set { _getXml = value; }
        }

        private string _searchListIdentifiersUrl;
        public string SearchListIdentifiersUrl
        {
            get
            {
                return _searchListIdentifiersUrl;
            }
            set
            {
                _searchListIdentifiersUrl = value;
            }
        }

        private string _metadataExtension;
        public string MetadataExtension
        {
            get
            {
                return _metadataExtension;
            }
            set
            {
                _metadataExtension = value;
            }
        }

        private string _marcExtension;
        public string MarcExtension
        {
            get
            {
                return _marcExtension;
            }
            set
            {
                _marcExtension = value;
            }
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
                    if (node.Attributes.GetNamedItem("key").Value == "StartDate")
                    {
                        if (DateTime.TryParse(node.Attributes.GetNamedItem("value").Value, out DateTime value)) this.StartDate = value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EndDate")
                    {
                        if (DateTime.TryParse(node.Attributes.GetNamedItem("value").Value, out DateTime value)) this.EndDate = value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EmailFromAddress")
                    {
                        this.EmailFromAddress = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EmailToAddress")
                    {
                        this.EmailToAddress = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DownloadIDs")
                    {
                        this.DownloadIDs = Convert.ToBoolean(node.Attributes.GetNamedItem("value").Value.ToLower());
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "GetXML")
                    {
                        this.GetXml = Convert.ToBoolean(node.Attributes.GetNamedItem("value").Value.ToLower());
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "SearchListIdentifiersUrl")
                    {
                        this.SearchListIdentifiersUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MetadataExtension")
                    {
                        this.MetadataExtension = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MarcExtension")
                    {
                        this.MarcExtension = node.Attributes.GetNamedItem("value").Value;
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

using System;
using System.Xml;

namespace IAHarvest
{
    public class ConfigParms
    {
        private string _smtpHost = "";
        public string SMTPHost
        {
            get
            {
                return _smtpHost;
            }
            set
            {
                _smtpHost = value;
            }
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

        private string _mode = string.Empty;
        public string Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
            }
        }

        private bool _upload = false;
        public bool Upload
        {
            get
            {
                return _upload;
            }
            set
            {
                _upload = value;
            }
        }

        private bool _download = true;
        public bool Download
        {
            get
            {
                return _download;
            }
            set
            {
                _download = value;
            }
        }

        private bool _quiet = false;

        public bool Quiet
        {
            get
            {
                return _quiet;
            }
            set
            {
                _quiet = value;
            }
        }

        private bool _allowUnapprovedPublish = true;
        public bool AllowUnapprovedPublish
        {
            get
            {
                return _allowUnapprovedPublish;
            }
            set
            {
                _allowUnapprovedPublish = value;
            }
        }

        private int _minimumDaysBeforeAllowUnapprovedPublish = 45;
        public int MinimumDaysBeforeAllowUnapprovedPublish
        {
            get
            {
                return _minimumDaysBeforeAllowUnapprovedPublish;
            }
            set
            {
                _minimumDaysBeforeAllowUnapprovedPublish = value;
            }
        }

        private string _itemPrefix = string.Empty;
        public string ItemPrefix
        {
            get
            {
                return _itemPrefix;
            }
            set
            {
                _itemPrefix = value;
            }
        }

        private string _item = string.Empty;
        public string Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
            }
        }

        public string FullItemIdentifier
        {
            get
            {
                return _itemPrefix + ":" + _item;
            }
        }

        private string _oaiListIdentifiersUrl;
        public string OaiListIdentifiersUrl
        {
            get
            {
                return _oaiListIdentifiersUrl;
            }
            set
            {
                _oaiListIdentifiersUrl = value;
            }
        }

        private string _oaiGetRecordUrl;
        public string OaiGetRecordUrl
        {
            get
            {
                return _oaiGetRecordUrl;
            }
            set
            {
                _oaiGetRecordUrl = value;
            }
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

        private string _searchListIdentifiersItemUrl;
        public string SearchListIdentifiersItemUrl
        {
            get
            {
                return _searchListIdentifiersItemUrl;
            }
            set
            {
                _searchListIdentifiersItemUrl = value;
            }
        }

        private string _fileDownloadUrl;
        public string FileDownloadUrl
        {
            get
            {
                return _fileDownloadUrl;
            }
            set
            {
                _fileDownloadUrl = value;
            }
        }

        private string _scandataDownloadUrl;
        public string ScandataDownloadUrl
        {
            get
            {
                return _scandataDownloadUrl;
            }
            set
            {
                _scandataDownloadUrl = value;
            }
        }

        private string _physicalLocationUrl;
        public string PhysicalLocationUrl
        {
            get
            {
                return _physicalLocationUrl;
            }
            set
            {
                _physicalLocationUrl = value;
            }
        }

        private string _pageExternalUrl;
        public string PageExternalUrl
        {
            get
            {
                return _pageExternalUrl;
            }
            set
            {
                _pageExternalUrl = value;
            }
        }

        private string _filesExtension;
        public string FilesExtension
        {
            get
            {
                return _filesExtension;
            }
            set
            {
                _filesExtension = value;
            }
        }

        private string _dcMetadataExtension;
        public string DCMetadataExtension
        {
            get
            {
                return _dcMetadataExtension;
            }
            set
            {
                _dcMetadataExtension = value;
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

        private string _metadataSourceExtension;
        public string MetadataSourceExtension
        {
            get
            {
                return _metadataSourceExtension;
            }
            set
            {
                _metadataSourceExtension = value;
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

        private string _djvuExtension;
        public string DjvuExtension
        {
            get
            {
                return _djvuExtension;
            }
            set
            {
                _djvuExtension = value;
            }
        }

        private string _scandataExtension;
        public string ScandataExtension
        {
            get
            {
                return _scandataExtension;
            }
            set
            {
                _scandataExtension = value;
            }
        }

        private string _scandataFile;
        public string ScandataFile
        {
            get
            {
                return _scandataFile;
            }
            set
            {
                _scandataFile = value;
            }
        }

        private string _localFileFolder;
        public string LocalFileFolder
        {
            get
            {
                return _localFileFolder;
            }
            set
            {
                _localFileFolder = value;
            }
        }

        private DateTime? _searchStartDate;
        public DateTime? SearchStartDate
        {
            get
            {
                return _searchStartDate;
            }
            set
            {
                _searchStartDate = value;
            }
        }

        private DateTime? _searchEndDate;
        public DateTime? SearchEndDate
        {
            get 
            { 
                return _searchEndDate;
            }
            set 
            { 
                _searchEndDate = value;
            }
        }

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
                    if (node.Attributes.GetNamedItem("key").Value == "Mode")
                    {
                        this.Mode = node.Attributes.GetNamedItem("value").Value.ToUpper();
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "Download")
                    {
                        this.Download = Convert.ToBoolean(node.Attributes.GetNamedItem("value").Value.ToLower());
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "Upload")
                    {
                        this.Upload = Convert.ToBoolean(node.Attributes.GetNamedItem("value").Value.ToLower());
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "Quiet")
                    {
                        this.Quiet = Convert.ToBoolean(node.Attributes.GetNamedItem("value").Value.ToLower());
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "AllowUnapprovedPublish")
                    {
                        this.AllowUnapprovedPublish = Convert.ToBoolean(node.Attributes.GetNamedItem("value").Value.ToLower());
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MinimumDaysBeforeAllowUnapprovedPublish")
                    {
                        this.MinimumDaysBeforeAllowUnapprovedPublish = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "ItemPrefix")
                    {
                        this.ItemPrefix = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "Item")
                    {
                        this.Item = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "OAIListIdentifiersUrl")
                    {
                        this.OaiListIdentifiersUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "OAIGetRecordUrl")
                    {
                        this.OaiGetRecordUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "SearchListIdentifiersUrl")
                    {
                        this.SearchListIdentifiersUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "SearchListIdentifiersItemUrl")
                    {
                        this.SearchListIdentifiersItemUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "FileDownloadUrl")
                    {
                        this.FileDownloadUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "ScandataDownloadUrl")
                    {
                        this.ScandataDownloadUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "PhysicalLocationUrl")
                    {
                        this.PhysicalLocationUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "PageExternalUrl")
                    {
                        this.PageExternalUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "FilesExtension")
                    {
                        this.FilesExtension = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DCMetadataExtension")
                    {
                        this.DCMetadataExtension = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MetadataExtension")
                    {
                        this.MetadataExtension = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MetadataSourceExtension")
                    {
                        this.MetadataSourceExtension = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MarcExtension")
                    {
                        this.MarcExtension = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DjvuExtension")
                    {
                        this.DjvuExtension = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "ScandataExtension")
                    {
                        this.ScandataExtension = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "ScandataFile")
                    {
                        this.ScandataFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "LocalFileFolder")
                    {
                        this.LocalFileFolder = node.Attributes.GetNamedItem("value").Value;
                    }
                }
            }
        }
    }
}

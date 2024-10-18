using System;
using System.Configuration;
using System.Web;
using System.Xml;

namespace IAHarvest
{
    public class ConfigParms
    {
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

        private bool _emailOnError = false;
        public bool EmailOnError
        {
            get { return _emailOnError; }
            set { _emailOnError = value; }
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

        private string _bhlCreatorExtension;
        public string BHLCreatorExtension
        {
            get
            {
                return _bhlCreatorExtension;
            }
            set
            {
                _bhlCreatorExtension = value;
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

        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            EmailOnError = ConfigurationManager.AppSettings["EmailOnError"].ToLower() == "true";
            Mode = ConfigurationManager.AppSettings["Mode"];
            Download = Convert.ToBoolean(ConfigurationManager.AppSettings["Download"]);
            Upload = Convert.ToBoolean(ConfigurationManager.AppSettings["Upload"]);
            Quiet = Convert.ToBoolean(ConfigurationManager.AppSettings["Quiet"]);
            AllowUnapprovedPublish = Convert.ToBoolean(ConfigurationManager.AppSettings["AllowUnapprovedPublish"]);
            MinimumDaysBeforeAllowUnapprovedPublish = Convert.ToInt32(ConfigurationManager.AppSettings["MinimumDaysBeforeAllowUnapprovedPublish"]);
            ItemPrefix = ConfigurationManager.AppSettings["ItemPrefix"];
            Item = ConfigurationManager.AppSettings["Item"];
            OaiListIdentifiersUrl = ConfigurationManager.AppSettings["OAIListIdentifiersUrl"];
            OaiGetRecordUrl = ConfigurationManager.AppSettings["OAIGetRecordUrl"];
            SearchListIdentifiersUrl = ConfigurationManager.AppSettings["SearchListIdentifiersUrl"];
            SearchListIdentifiersItemUrl = ConfigurationManager.AppSettings["SearchListIdentifiersItemUrl"];
            FileDownloadUrl = ConfigurationManager.AppSettings["FileDownloadUrl"];
            ScandataDownloadUrl = ConfigurationManager.AppSettings["ScandataDownloadUrl"];
            PhysicalLocationUrl = ConfigurationManager.AppSettings["PhysicalLocationUrl"];
            PageExternalUrl = ConfigurationManager.AppSettings["PageExternalUrl"];
            FilesExtension = ConfigurationManager.AppSettings["FilesExtension"];
            DCMetadataExtension = ConfigurationManager.AppSettings["DCMetadataExtension"];
            MetadataExtension = ConfigurationManager.AppSettings["MetadataExtension"];
            MetadataSourceExtension = ConfigurationManager.AppSettings["MetadataSourceExtension"];
            MarcExtension = ConfigurationManager.AppSettings["MarcExtension"];
            DjvuExtension = ConfigurationManager.AppSettings["DjvuExtension"];
            ScandataExtension = ConfigurationManager.AppSettings["ScandataExtension"];
            BHLCreatorExtension = ConfigurationManager.AppSettings["BHLCreatorExtension"];
            ScandataFile = ConfigurationManager.AppSettings["ScandataFile"];
            LocalFileFolder = ConfigurationManager.AppSettings["LocalFileFolder"];
            BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
        }
    }
}

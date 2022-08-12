using System;
using System.Configuration;
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
            if (DateTime.TryParse(ConfigurationManager.AppSettings["StartDate"], out DateTime startDate)) this.StartDate = startDate;
            if (DateTime.TryParse(ConfigurationManager.AppSettings["EndDate"], out DateTime endDate)) this.EndDate = endDate;
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            DownloadIDs = Convert.ToBoolean(ConfigurationManager.AppSettings["DownloadIDs"]);
            GetXml = Convert.ToBoolean(ConfigurationManager.AppSettings["GetXML"]);
            SearchListIdentifiersUrl = ConfigurationManager.AppSettings["SearchListIdentifiersUrl"];
            MetadataExtension = ConfigurationManager.AppSettings["MetadataExtension"];
            MarcExtension = ConfigurationManager.AppSettings["MarcExtension"];
            BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
        }
    }
}

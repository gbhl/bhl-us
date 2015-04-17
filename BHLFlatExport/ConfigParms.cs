using System;
using System.Xml;

namespace MOBOT.BHL.BHLFlatExport
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

        private string _fileFolder = string.Empty;

        public string FileFolder
        {
            get { return _fileFolder; }
            set { _fileFolder = value; }
        }

        private string _doiFile = string.Empty;

        public string DOIFile
        {
            get { return _doiFile; }
            set { _doiFile = value; }
        }

        private string _authorFile = string.Empty;

        public string AuthorFile
        {
            get { return _authorFile; }
            set { _authorFile = value; }
        }

        private string _itemFile = string.Empty;

        public string ItemFile
        {
            get { return _itemFile; }
            set { _itemFile = value; }
        }

        private string _pageFile = string.Empty;

        public string PageFile
        {
            get { return _pageFile; }
            set { _pageFile = value; }
        }

        private string _pageNameFile = string.Empty;

        public string PageNameFile
        {
            get { return _pageNameFile; }
            set { _pageNameFile = value; }
        }

        private string _partFile = string.Empty;

        public string PartFile
        {
            get { return _partFile; }
            set { _partFile = value; }
        }

        private string _partAuthorFile = string.Empty;

        public string PartAuthorFile
        {
            get { return _partAuthorFile; }
            set { _partAuthorFile = value; }
        }

        private string _keywordFile = string.Empty;

        public string KeywordFile
        {
            get { return _keywordFile; }
            set { _keywordFile = value; }
        }

        private string _titleFile = string.Empty;

        public string TitleFile
        {
            get { return _titleFile; }
            set { _titleFile = value; }
        }

        private string _titleIdentifierFile = string.Empty;

        public string TitleIdentifierFile
        {
            get { return _titleIdentifierFile; }
            set { _titleIdentifierFile = value; }
        }

        private string _itemUrlFormat = string.Empty;

        public string ItemUrlFormat
        {
            get { return _itemUrlFormat; }
            set { _itemUrlFormat = value; }
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
                    if (node.Attributes.GetNamedItem("key").Value == "FileFolder")
                    {
                        this.FileFolder = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DoiFile")
                    {
                        this.DOIFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "AuthorFile")
                    {
                        this.AuthorFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "ItemFile")
                    {
                        this.ItemFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "PageFile")
                    {
                        this.PageFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "PageNameFile")
                    {
                        this.PageNameFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "PartFile")
                    {
                        this.PartFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "PartAuthorFile")
                    {
                        this.PartAuthorFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "KeywordFile")
                    {
                        this.KeywordFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "TitleFile")
                    {
                        this.TitleFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "TitleIdentifierFile")
                    {
                        this.TitleIdentifierFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "ItemURLFormat")
                    {
                        this.ItemUrlFormat = node.Attributes.GetNamedItem("value").Value;
                    }
                }
            }
        }
    }
}

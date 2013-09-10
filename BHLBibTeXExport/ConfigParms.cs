using System;
using System.Xml;

namespace MOBOT.BHL.BHLBibTeXExport
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

        private string _bibTexItemTempFile = string.Empty;

        public string BibTexItemTempFile
        {
            get { return _bibTexItemTempFile; }
            set { _bibTexItemTempFile = value; }
        }

        private string _bibTexItemFile = string.Empty;

        public string BibTexItemFile
        {
            get { return _bibTexItemFile; }
            set { _bibTexItemFile = value; }
        }

        private string _bibTexItemZipFile = String.Empty;

        public string BibTexItemZipFile
        {
            get { return _bibTexItemZipFile; }
            set { _bibTexItemZipFile = value; }
        }

        private string _bibTexTitleTempFile = string.Empty;

        public string BibTexTitleTempFile
        {
            get { return _bibTexTitleTempFile; }
            set { _bibTexTitleTempFile = value; }
        }

        private string _bibTexTitleFile = string.Empty;

        public string BibTexTitleFile
        {
            get { return _bibTexTitleFile; }
            set { _bibTexTitleFile = value; }
        }

        private string _bibTexTitleZipFile = String.Empty;

        public string BibTexTitleZipFile
        {
            get { return _bibTexTitleZipFile; }
            set { _bibTexTitleZipFile = value; }
        }

        private string _bibTexSegmentTempFile = string.Empty;

        public string BibTexSegmentTempFile
        {
            get { return _bibTexSegmentTempFile; }
            set { _bibTexSegmentTempFile = value; }
        }

        private string _bibTexSegmentFile = string.Empty;

        public string BibTexSegmentFile
        {
            get { return _bibTexSegmentFile; }
            set { _bibTexSegmentFile = value; }
        }

        private string _bibTexSegmentZipFile = string.Empty;

        public string BibTexSegmentZipFile
        {
            get { return _bibTexSegmentZipFile; }
            set { _bibTexSegmentZipFile = value; }
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
                    if (node.Attributes.GetNamedItem("key").Value == "BibTeXItemTempFile")
                    {
                        this.BibTexItemTempFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibTeXItemFile")
                    {
                        this.BibTexItemFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibTeXItemZipFile")
                    {
                        this.BibTexItemZipFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibTeXTitleTempFile")
                    {
                        this.BibTexTitleTempFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibTeXTitleFile")
                    {
                        this.BibTexTitleFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibTeXTitleZipFile")
                    {
                        this.BibTexTitleZipFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibTeXSegmentTempFile")
                    {
                        this.BibTexSegmentTempFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibTeXSegmentFile")
                    {
                        this.BibTexSegmentFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibTeXSegmentZipFile")
                    {
                        this.BibTexSegmentZipFile = node.Attributes.GetNamedItem("value").Value;
                    }
                }
            }
        }
    }
}

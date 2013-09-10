using System;
using System.Xml;

namespace MOBOT.BHL.BHLMODSExport
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

        private string _modsSegmentTempFile = string.Empty;

        public string MODSSegmentTempFile
        {
            get { return _modsSegmentTempFile; }
            set { _modsSegmentTempFile = value; }
        }

        private string _modsSegmentFile = string.Empty;

        public string MODSSegmentFile
        {
            get { return _modsSegmentFile; }
            set { _modsSegmentFile = value; }
        }

        private string _modsSegmentZipFile = string.Empty;

        public string MODSSegmentZipFile
        {
            get { return _modsSegmentZipFile; }
            set { _modsSegmentZipFile = value; }
        }

        private string _modsItemTempFile = string.Empty;

        public string MODSItemTempFile
        {
            get { return _modsItemTempFile; }
            set { _modsItemTempFile = value; }
        }

        private string _modsItemFile = string.Empty;

        public string MODSItemFile
        {
            get { return _modsItemFile; }
            set { _modsItemFile = value; }
        }

        private string _modsItemZipFile = String.Empty;

        public string MODSItemZipFile
        {
            get { return _modsItemZipFile; }
            set { _modsItemZipFile = value; }
        }

        private string _modsTitleTempFile = string.Empty;

        public string MODSTitleTempFile
        {
            get { return _modsTitleTempFile; }
            set { _modsTitleTempFile = value; }
        }

        private string _modsTitleFile = string.Empty;

        public string MODSTitleFile
        {
            get { return _modsTitleFile; }
            set { _modsTitleFile = value; }
        }

        private string _modsTitleZipFile = String.Empty;

        public string MODSTitleZipFile
        {
            get { return _modsTitleZipFile; }
            set { _modsTitleZipFile = value; }
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
                    if (node.Attributes.GetNamedItem("key").Value == "MODSSegmentTempFile")
                    {
                        this.MODSSegmentTempFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MODSSegmentFile")
                    {
                        this.MODSSegmentFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MODSSegmentZipFile")
                    {
                        this.MODSSegmentZipFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MODSItemTempFile")
                    {
                        this.MODSItemTempFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MODSItemFile")
                    {
                        this.MODSItemFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MODSItemZipFile")
                    {
                        this.MODSItemZipFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MODSTitleTempFile")
                    {
                        this.MODSTitleTempFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MODSTitleFile")
                    {
                        this.MODSTitleFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MODSTitleZipFile")
                    {
                        this.MODSTitleZipFile = node.Attributes.GetNamedItem("value").Value;
                    }
                }
            }
        }
    }
}

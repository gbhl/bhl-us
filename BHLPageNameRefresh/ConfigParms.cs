using System;
using System.Xml;

namespace MOBOT.BHL.PageNameRefresh
{
    public class ConfigParms
    {
        private string _ocrTextPath = "";
        public string OcrTextPath
        {
            get
            {
                return _ocrTextPath;
            }
            set
            {
                _ocrTextPath = value;
            }
        }

        private string _ocrTextLocation = "";
        public string OcrTextLocation
        {
            get
            {
                return _ocrTextLocation;
            }
            set
            {
                _ocrTextLocation = value;
            }
        }

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

        private int _maximumPageNameAge = 0;
        public int MaximumPageNameAge
        {
            get
            {
                return _maximumPageNameAge;
            }
            set
            {
                _maximumPageNameAge = value;
            }
        }

        private int _externalWebServiceInterval = 0;
        public int ExternalWebServiceInterval
        {
            get
            {
                return _externalWebServiceInterval;
            }
            set
            {
                _externalWebServiceInterval = value;
            }
        }

        private string _nameServiceSourceName = string.Empty;

        public string NameServiceSourceName
        {
            get { return _nameServiceSourceName; }
            set { _nameServiceSourceName = value; }
        }

        private bool _doAsync = false;

        public bool DoAsync
        {
            get { return _doAsync; }
            set { _doAsync = value; }
        }

        private int _maxConcurrent = 2;

        public int MaxConcurrent
        {
            get { return _maxConcurrent; }
            set { _maxConcurrent = value; }
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

        private string  _item = string.Empty;
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

        public void LoadAppConfig()
        {
            XmlDocument doc = new XmlDocument();
            string configPath = AppDomain.CurrentDomain.FriendlyName + ".config";
            doc.Load(configPath);
            foreach (XmlNode node in doc["configuration"]["appSettings"])
            {
                if (node.Name == "add")
                {
                    if (node.Attributes.GetNamedItem("key").Value == "OCRTextPath")
                    {
                        this.OcrTextPath = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "OCRTextLocation")
                    {
                        this.OcrTextLocation = node.Attributes.GetNamedItem("value").Value;
                    }
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
                    if (node.Attributes.GetNamedItem("key").Value == "MaximumPageNameAge")
                    {
                        this.MaximumPageNameAge = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "ExternalWebServiceInterval")
                    {
                        this.ExternalWebServiceInterval = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "NameServiceSourceName")
                    {
                        this.NameServiceSourceName = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DoAsync")
                    {
                        this.DoAsync = (node.Attributes.GetNamedItem("value").Value == "true");
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MaxConcurrent")
                    {
                        this.MaxConcurrent = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "Mode")
                    {
                        this.Mode = node.Attributes.GetNamedItem("value").Value.ToUpper();
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "Item")
                    {
                        this.Item = node.Attributes.GetNamedItem("value").Value;
                    }
                }
            }
        }
    }
}

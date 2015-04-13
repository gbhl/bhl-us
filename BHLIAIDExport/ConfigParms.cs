using System;
using System.Xml;

namespace MOBOT.BHL.BHLIAIDExport
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

        private string _iaIDFolder = string.Empty;

        public string IAIDFolder
        {
            get { return _iaIDFolder; }
            set { _iaIDFolder = value; }
        }

        private string _iaIDFile = string.Empty;

        public string IAIDFile
        {
            get { return _iaIDFile; }
            set { _iaIDFile = value; }
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
                    if (node.Attributes.GetNamedItem("key").Value == "IAIDFolder")
                    {
                        this.IAIDFolder = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "IAIDFile")
                    {
                        this.IAIDFile = node.Attributes.GetNamedItem("value").Value;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MOBOT.BHL.BHLOcrRefresh
{
    class ConfigParms
    {
        private string _smtpHost = string.Empty;
        public string SMTPHost
        {
            get { return _smtpHost; }
            set { _smtpHost = value; }
        }

        private string _emailFromAddress = string.Empty;
        public string EmailFromAddress
        {
            get { return _emailFromAddress; }
            set { _emailFromAddress = value; }
        }

        private string _emailToAddress = string.Empty;
        public string EmailToAddress
        {
            get { return _emailToAddress; }
            set { _emailToAddress = value; }
        }

        private string _ocrJobNewPath = string.Empty;
        public string OcrJobNewPath
        {
            get { return _ocrJobNewPath; }
            set { _ocrJobNewPath = value; }
        }

        private string _ocrJobProcessingPath = string.Empty;
        public string OcrJobProcessingPath
        {
            get { return _ocrJobProcessingPath; }
            set { _ocrJobProcessingPath = value; }
        }

        private string _ocrJobCompletePath = string.Empty;
        public string OcrJobCompletePath
        {
            get { return _ocrJobCompletePath; }
            set { _ocrJobCompletePath = value; }
        }

        private string _ocrJobErrorPath = string.Empty;
        public string OcrJobErrorPath
        {
            get { return _ocrJobErrorPath; }
            set { _ocrJobErrorPath = value; }
        }

        private string _ocrJobTempPath = string.Empty;
        public string OcrJobTempPath
        {
            get { return _ocrJobTempPath; }
            set { _ocrJobTempPath = value; }
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
                    if (node.Attributes.GetNamedItem("key").Value == "OcrJobNewPath")
                    {
                        this.OcrJobNewPath = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "OcrJobProcessingPath")
                    {
                        this.OcrJobProcessingPath = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "OcrJobCompletePath")
                    {
                        this.OcrJobCompletePath = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "OcrJobErrorPath")
                    {
                        this.OcrJobErrorPath = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "OcrJobTempPath")
                    {
                        this.OcrJobTempPath = node.Attributes.GetNamedItem("value").Value;
                    }
                }
            }
        }
    }
}

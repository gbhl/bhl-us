using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MOBOT.BHL.BHLPDFGenerator
{
    class ConfigParms
    {
        private string _smtpHost = "";
        public string SMTPHost
        {
            get { return _smtpHost; }
            set { _smtpHost = value; }
        }

        private string _emailFromAddress = "";
        public string EmailFromAddress
        {
            get { return _emailFromAddress; }
            set { _emailFromAddress = value; }
        }

        private string _emailToAddress = "";
        public string EmailToAddress
        {
            get { return _emailToAddress; }
            set { _emailToAddress = value; }
        }

        private string _pdfFilePath = String.Empty;
        public string PdfFilePath
        {
            get { return _pdfFilePath; }
            set { _pdfFilePath = value; }
        }

        private string _pdfUrl = String.Empty;
        public string PdfUrl
        {
            get { return _pdfUrl; }
            set { _pdfUrl = value; }
        }

        private string _ocrTextLocation = String.Empty;
        public string OcrTextLocation
        {
            get { return _ocrTextLocation; }
            set { _ocrTextLocation = value; }
        }

        private int _retryImageWait = 0;
        public int RetryImageWait
        {
            get { return _retryImageWait; }
            set { _retryImageWait = value; }
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
                    if (node.Attributes.GetNamedItem("key").Value == "PdfFilePath")
                    {
                        this.PdfFilePath = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "PdfUrl")
                    {
                        this.PdfUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "OCRTextLocation")
                    {
                        this.OcrTextLocation = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "RetryImageWait")
                    {
                        this.RetryImageWait = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    
                }
            }
        }
    }
}

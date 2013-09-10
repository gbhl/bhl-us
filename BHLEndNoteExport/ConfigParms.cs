using System;
using System.Xml;

namespace MOBOT.BHL.BHLEndNoteExport
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

        private string _endNoteItemTempFile = string.Empty;

        public string EndNoteItemTempFile
        {
            get { return _endNoteItemTempFile; }
            set { _endNoteItemTempFile = value; }
        }

        private string _endNoteItemFile = string.Empty;

        public string EndNoteItemFile
        {
            get { return _endNoteItemFile; }
            set { _endNoteItemFile = value; }
        }

        private string _endNoteItemZipFile = String.Empty;

        public string EndNoteItemZipFile
        {
            get { return _endNoteItemZipFile; }
            set { _endNoteItemZipFile = value; }
        }

        private string _endNoteTitleTempFile = string.Empty;

        public string EndNoteTitleTempFile
        {
            get { return _endNoteTitleTempFile; }
            set { _endNoteTitleTempFile = value; }
        }

        private string _endNoteTitleFile = string.Empty;

        public string EndNoteTitleFile
        {
            get { return _endNoteTitleFile; }
            set { _endNoteTitleFile = value; }
        }

        private string _endNoteTitleZipFile = String.Empty;

        public string EndNoteTitleZipFile
        {
            get { return _endNoteTitleZipFile; }
            set { _endNoteTitleZipFile = value; }
        }

        private string _endNoteSegmentTempFile = string.Empty;

        public string EndNoteSegmentTempFile
        {
            get { return _endNoteSegmentTempFile; }
            set { _endNoteSegmentTempFile = value; }
        }

        private string _endNoteSegmentFile = string.Empty;

        public string EndNoteSegmentFile
        {
            get { return _endNoteSegmentFile; }
            set { _endNoteSegmentFile = value; }
        }

        private string _endNoteSegmentZipFile = String.Empty;

        public string EndNoteSegmentZipFile
        {
            get { return _endNoteSegmentZipFile; }
            set { _endNoteSegmentZipFile = value; }
        }

        private string _titleUrl = String.Empty;

        public string TitleUrl
        {
            get { return _titleUrl; }
            set { _titleUrl = value; }
        }

        private string _itemUrl = String.Empty;

        public string ItemUrl
        {
            get { return _itemUrl; }
            set { _itemUrl = value; }
        }

        private string _segmentUrl = String.Empty;

        public string SegmentUrl
        {
            get { return _segmentUrl; }
            set { _segmentUrl = value; }
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
                    if (node.Attributes.GetNamedItem("key").Value == "EndNoteItemTempFile")
                    {
                        this.EndNoteItemTempFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EndNoteItemFile")
                    {
                        this.EndNoteItemFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EndNoteItemZipFile")
                    {
                        this.EndNoteItemZipFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EndNoteTitleTempFile")
                    {
                        this.EndNoteTitleTempFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EndNoteTitleFile")
                    {
                        this.EndNoteTitleFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EndNoteTitleZipFile")
                    {
                        this.EndNoteTitleZipFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EndNoteSegmentTempFile")
                    {
                        this.EndNoteSegmentTempFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EndNoteSegmentFile")
                    {
                        this.EndNoteSegmentFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EndNoteSegmentZipFile")
                    {
                        this.EndNoteSegmentZipFile = node.Attributes.GetNamedItem("value").Value;
                    }                    
                    if (node.Attributes.GetNamedItem("key").Value == "TitleUrl")
                    {
                        this.TitleUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "ItemUrl")
                    {
                        this.ItemUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "SegmentUrl")
                    {
                        this.SegmentUrl = node.Attributes.GetNamedItem("value").Value;
                    }
                }
            }
        }
    }
}

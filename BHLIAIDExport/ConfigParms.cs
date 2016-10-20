using System;
using System.Configuration;
using System.Xml;

namespace MOBOT.BHL.BHLIAIDExport
{
    public class ConfigParms
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
            SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            IAIDFolder = ConfigurationManager.AppSettings["IAIDFolder"];
            IAIDFile = ConfigurationManager.AppSettings["IAIDFile"];
        }
    }
}

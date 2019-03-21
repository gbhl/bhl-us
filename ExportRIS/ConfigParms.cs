using System;
using System.Configuration;

namespace BHL.Export.RIS
{
    public class ConfigParms
    {
        public string RISItemTempFile { get; set; }
        public string RISItemFile { get; set; }
        public string RISItemZipFile { get; set; }
        public string RISTitleTempFile { get; set; }
        public string RISTitleFile { get; set; }
        public string RISTitleZipFile { get; set; }
        public string RISSegmentTempFile { get; set; }
        public string RISSegmentFile { get; set; }
        public string RISSegmentZipFile { get; set; }

        public ConfigParms()
        {
            RISItemTempFile = string.Empty;
            RISItemFile = string.Empty;
            RISItemZipFile = string.Empty;
            RISTitleTempFile = string.Empty;
            RISTitleFile = string.Empty;
            RISTitleZipFile = string.Empty;
            RISSegmentTempFile = string.Empty;
            RISSegmentFile = string.Empty;
            RISSegmentZipFile = string.Empty;
        }

        public void LoadAppConfig()
        {
            this.RISItemTempFile = ConfigurationManager.AppSettings["RISItemTempFile"];
            this.RISItemFile = ConfigurationManager.AppSettings["RISItemFile"];
            this.RISItemZipFile = ConfigurationManager.AppSettings["RISItemZipFile"];
            this.RISTitleTempFile = ConfigurationManager.AppSettings["RISTitleTempFile"];
            this.RISTitleFile = ConfigurationManager.AppSettings["RISTitleFile"];
            this.RISTitleZipFile = ConfigurationManager.AppSettings["RISTitleZipFile"];
            this.RISSegmentTempFile = ConfigurationManager.AppSettings["RISSegmentTempFile"];
            this.RISSegmentFile = ConfigurationManager.AppSettings["RISSegmentFile"];
            this.RISSegmentZipFile = ConfigurationManager.AppSettings["RISSegmentZipFile"];
        }
    }
}

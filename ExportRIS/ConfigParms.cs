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

        public string RISInternalItemTempFile { get; set; }
        public string RISInternalItemFile { get; set; }
        public string RISInternalItemZipFile { get; set; }
        public string RISInternalTitleTempFile { get; set; }
        public string RISInternalTitleFile { get; set; }
        public string RISInternalTitleZipFile { get; set; }
        public string RISInternalSegmentTempFile { get; set; }
        public string RISInternalSegmentFile { get; set; }
        public string RISInternalSegmentZipFile { get; set; }

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

            this.RISInternalItemTempFile = ConfigurationManager.AppSettings["RISInternalItemTempFile"];
            this.RISInternalItemFile = ConfigurationManager.AppSettings["RISInternalItemFile"];
            this.RISInternalItemZipFile = ConfigurationManager.AppSettings["RISInternalItemZipFile"];
            this.RISInternalTitleTempFile = ConfigurationManager.AppSettings["RISInternalTitleTempFile"];
            this.RISInternalTitleFile = ConfigurationManager.AppSettings["RISInternalTitleFile"];
            this.RISInternalTitleZipFile = ConfigurationManager.AppSettings["RISInternalTitleZipFile"];
            this.RISInternalSegmentTempFile = ConfigurationManager.AppSettings["RISInternalSegmentTempFile"];
            this.RISInternalSegmentFile = ConfigurationManager.AppSettings["RISInternalSegmentFile"];
            this.RISInternalSegmentZipFile = ConfigurationManager.AppSettings["RISInternalSegmentZipFile"];
        }
    }
}

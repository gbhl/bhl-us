using System;
using System.Configuration;

namespace BHL.Export.BibTeX
{
    public class ConfigParms
    {
        public string BibTexItemTempFile { get; set; } = string.Empty;
        public string BibTexItemFile { get; set; } = string.Empty;
        public string BibTexItemZipFile { get; set; } = String.Empty;
        public string BibTexTitleTempFile { get; set; } = string.Empty;
        public string BibTexTitleFile { get; set; } = string.Empty;
        public string BibTexTitleZipFile { get; set; } = String.Empty;
        public string BibTexSegmentTempFile { get; set; } = string.Empty;
        public string BibTexSegmentFile { get; set; } = string.Empty;
        public string BibTexSegmentZipFile { get; set; } = string.Empty;

        public string BibTexInternalItemTempFile { get; set; } = string.Empty;
        public string BibTexInternalItemFile { get; set; } = string.Empty;
        public string BibTexInternalItemZipFile { get; set; } = String.Empty;
        public string BibTexInternalTitleTempFile { get; set; } = string.Empty;
        public string BibTexInternalTitleFile { get; set; } = string.Empty;
        public string BibTexInternalTitleZipFile { get; set; } = String.Empty;
        public string BibTexInternalSegmentTempFile { get; set; } = string.Empty;
        public string BibTexInternalSegmentFile { get; set; } = string.Empty;
        public string BibTexInternalSegmentZipFile { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            this.BibTexItemTempFile = ConfigurationManager.AppSettings["BibTexItemTempFile"];
            this.BibTexItemFile = ConfigurationManager.AppSettings["BibTexItemFile"];
            this.BibTexItemZipFile = ConfigurationManager.AppSettings["BibTexItemZipFile"];
            this.BibTexTitleTempFile = ConfigurationManager.AppSettings["BibTexTitleTempFile"];
            this.BibTexTitleFile = ConfigurationManager.AppSettings["BibTexTitleFile"];
            this.BibTexTitleZipFile = ConfigurationManager.AppSettings["BibTexTitleZipFile"];
            this.BibTexSegmentTempFile = ConfigurationManager.AppSettings["BibTexSegmentTempFile"];
            this.BibTexSegmentFile = ConfigurationManager.AppSettings["BibTexSegmentFile"];
            this.BibTexSegmentZipFile = ConfigurationManager.AppSettings["BibTexSegmentZipFile"];

            this.BibTexInternalItemTempFile = ConfigurationManager.AppSettings["BibTexInternalItemTempFile"];
            this.BibTexInternalItemFile = ConfigurationManager.AppSettings["BibTexInternalItemFile"];
            this.BibTexInternalItemZipFile = ConfigurationManager.AppSettings["BibTexInternalItemZipFile"];
            this.BibTexInternalTitleTempFile = ConfigurationManager.AppSettings["BibTexInternalTitleTempFile"];
            this.BibTexInternalTitleFile = ConfigurationManager.AppSettings["BibTexInternalTitleFile"];
            this.BibTexInternalTitleZipFile = ConfigurationManager.AppSettings["BibTexInternalTitleZipFile"];
            this.BibTexInternalSegmentTempFile = ConfigurationManager.AppSettings["BibTexInternalSegmentTempFile"];
            this.BibTexInternalSegmentFile = ConfigurationManager.AppSettings["BibTexInternalSegmentFile"];
            this.BibTexInternalSegmentZipFile = ConfigurationManager.AppSettings["BibTexInternalSegmentZipFile"];
        }
    }
}

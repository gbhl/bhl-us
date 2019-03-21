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

        public ConfigParms()
        {
            BibTexItemTempFile = string.Empty;
            BibTexItemFile = string.Empty;
            BibTexItemZipFile = string.Empty;
            BibTexTitleTempFile = string.Empty;
            BibTexTitleFile = string.Empty;
            BibTexTitleZipFile = string.Empty;
            BibTexSegmentTempFile = string.Empty;
            BibTexSegmentFile = string.Empty;
            BibTexSegmentZipFile = string.Empty;
        }

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
        }
    }
}

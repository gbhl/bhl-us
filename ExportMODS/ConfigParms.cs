using System;
using System.Configuration;

namespace BHL.Export.MODS
{
    public class ConfigParms
    {
        public string MODSSegmentTempFile { get; set; } = string.Empty;
        public string MODSSegmentFile { get; set; } = string.Empty;
        public string MODSSegmentZipFile { get; set; } = string.Empty;
        public string MODSItemTempFile { get; set; } = string.Empty;
        public string MODSItemFile { get; set; } = string.Empty;
        public string MODSItemZipFile { get; set; } = String.Empty;
        public string MODSTitleTempFile { get; set; } = string.Empty;
        public string MODSTitleFile { get; set; } = string.Empty;
        public string MODSTitleZipFile { get; set; } = String.Empty;

        public string MODSInternalSegmentTempFile { get; set; } = string.Empty;
        public string MODSInternalSegmentFile { get; set; } = string.Empty;
        public string MODSInternalSegmentZipFile { get; set; } = string.Empty;
        public string MODSInternalItemTempFile { get; set; } = string.Empty;
        public string MODSInternalItemFile { get; set; } = string.Empty;
        public string MODSInternalItemZipFile { get; set; } = String.Empty;
        public string MODSInternalTitleTempFile { get; set; } = string.Empty;
        public string MODSInternalTitleFile { get; set; } = string.Empty;
        public string MODSInternalTitleZipFile { get; set; } = String.Empty;

        public void LoadAppConfig()
        {
            this.MODSItemTempFile = ConfigurationManager.AppSettings["MODSItemTempFile"];
            this.MODSItemFile = ConfigurationManager.AppSettings["MODSItemFile"];
            this.MODSItemZipFile = ConfigurationManager.AppSettings["MODSItemZipFile"];
            this.MODSTitleTempFile = ConfigurationManager.AppSettings["MODSTitleTempFile"];
            this.MODSTitleFile = ConfigurationManager.AppSettings["MODSTitleFile"];
            this.MODSTitleZipFile = ConfigurationManager.AppSettings["MODSTitleZipFile"];
            this.MODSSegmentTempFile = ConfigurationManager.AppSettings["MODSSegmentTempFile"];
            this.MODSSegmentFile = ConfigurationManager.AppSettings["MODSSegmentFile"];
            this.MODSSegmentZipFile = ConfigurationManager.AppSettings["MODSSegmentZipFile"];

            this.MODSInternalItemTempFile = ConfigurationManager.AppSettings["MODSInternalItemTempFile"];
            this.MODSInternalItemFile = ConfigurationManager.AppSettings["MODSInternalItemFile"];
            this.MODSInternalItemZipFile = ConfigurationManager.AppSettings["MODSInternalItemZipFile"];
            this.MODSInternalTitleTempFile = ConfigurationManager.AppSettings["MODSInternalTitleTempFile"];
            this.MODSInternalTitleFile = ConfigurationManager.AppSettings["MODSInternalTitleFile"];
            this.MODSInternalTitleZipFile = ConfigurationManager.AppSettings["MODSInternalTitleZipFile"];
            this.MODSInternalSegmentTempFile = ConfigurationManager.AppSettings["MODSInternalSegmentTempFile"];
            this.MODSInternalSegmentFile = ConfigurationManager.AppSettings["MODSInternalSegmentFile"];
            this.MODSInternalSegmentZipFile = ConfigurationManager.AppSettings["MODSInternalSegmentZipFile"];
        }
    }
}

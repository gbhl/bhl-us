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

        public ConfigParms()
        {
            MODSItemTempFile = string.Empty;
            MODSItemFile = string.Empty;
            MODSItemZipFile = string.Empty;
            MODSTitleTempFile = string.Empty;
            MODSTitleFile = string.Empty;
            MODSTitleZipFile = string.Empty;
            MODSSegmentTempFile = string.Empty;
            MODSSegmentFile = string.Empty;
            MODSSegmentZipFile = string.Empty;
        }

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
        }
    }
}

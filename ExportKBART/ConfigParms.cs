using System.Configuration;

namespace BHL.Export.KBART
{
    public class ConfigParms
    {
        public string KBARTFile { get; set; } = string.Empty;
        public string KBARTUrlRoot { get; set; } = string.Empty;
        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            KBARTFile = ConfigurationManager.AppSettings["KBARTFile"];
            KBARTUrlRoot = ConfigurationManager.AppSettings["KBARTUrlRoot"];
            BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
        }
    }
}

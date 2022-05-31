using System.Configuration;

namespace BHL.Export.IAID
{
    public class ConfigParms
    {
        public string IAIDFolder { get; set; } = string.Empty;
        public string IAIDFile { get; set; } = string.Empty;
        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            IAIDFolder = ConfigurationManager.AppSettings["IAIDFolder"];
            IAIDFile = ConfigurationManager.AppSettings["IAIDFile"];
            BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
        }
    }
}

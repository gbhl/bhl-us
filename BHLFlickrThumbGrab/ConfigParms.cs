using System.Configuration;

namespace MOBOT.BHL.BHLFlickrThumbGrab
{
    public class ConfigParms
    {
        public string FlickrAPIKey { get; set; }
        public string SMTPHost { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailToAddress { get; set; }
        public bool EmailOnError { get; set; }
        public string FlickrAPIUrl_photoGetInfo { get; set; }
        public string FlickrDownloadUrl { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFolder { get; set; }
        public string NumberToGrab { get; set; }
        public string ImageListFilePath { get; set; }
        public string DefaultFilesFolder { get; set; }
        public string BHLWSEndpoint { get; set; } = string.Empty;

        public void LoadAppConfig()
        {
            FlickrAPIKey = ConfigurationManager.AppSettings["FlickrAPIKey"];
            SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            EmailOnError = StringToBool(ConfigurationManager.AppSettings["EmailOnError"]);
            FlickrAPIUrl_photoGetInfo = ConfigurationManager.AppSettings["FlickrAPIUrl_photo.getInfo"];
            FlickrDownloadUrl = ConfigurationManager.AppSettings["FlickrDownloadUrl"];
            ImageFileName = ConfigurationManager.AppSettings["ImageFileName"];
            ImageFolder = ConfigurationManager.AppSettings["ImageFolder"];
            ImageListFilePath = ConfigurationManager.AppSettings["ImageListFilePath"];
            NumberToGrab = ConfigurationManager.AppSettings["NumberToGrab"];
            DefaultFilesFolder = ConfigurationManager.AppSettings["DefaultFilesFolder"];
            BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
        }

        private bool StringToBool(string value)
        {
            switch (value)
            {
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    return true;
            }
        }
    }
}

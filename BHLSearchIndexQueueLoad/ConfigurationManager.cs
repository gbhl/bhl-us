using Microsoft.Extensions.Configuration;

namespace BHL.SearchIndexQueueLoad
{
    static public class ConfigurationManager
    {
        private static IConfigurationRoot _configuration;

        static ConfigurationManager()
        {
            // Define the sources of configuration information for the application.
            var builder = new ConfigurationBuilder()
                .AddXmlFile("AppConfig.xml");

            // Create the configuration object to be used to retrieve configuration information.
            _configuration = builder.Build();
        }

        static public string AppSettings(string key)
        {
            return _configuration.GetSection("AppSettings")[key];
        }

        static public string ConnectionStrings(string key)
        {
            return _configuration.GetConnectionString(key);
        }
    }
}

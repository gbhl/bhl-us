using Microsoft.Extensions.Configuration;

namespace BHL.SearchIndexer
{
    public class ConfigurationManager
    {
        private static IConfigurationRoot _configuration;

        public ConfigurationManager(string fileName)
        {
            // Define the sources of configuration information for the application.
            var builder = new ConfigurationBuilder()
                .AddXmlFile(fileName);

            // Create the configuration object to be used to retrieve configuration information.
            _configuration = builder.Build();
        }

        public string AppSettings(string key)
        {
            return _configuration.GetSection("AppSettings")[key];
        }

        public string ConnectionStrings(string key)
        {
            return _configuration.GetConnectionString(key);
        }
    }
}

using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        /// <summary>
        /// Select values from Configuration by configuration name.
        /// </summary>
        /// <param name="configurationName">Unique key for configuration record.</param>
        /// <returns>Object of type Configuration.</returns>
        public Configuration ConfigurationSelectByName(String configurationName)
        {
            return new ConfigurationDAL().ConfigurationSelectByName(null, null, configurationName);
        }

        public Configuration ConfigurationSave(string configurationName, string configurationValue)
        {
            ConfigurationDAL dal = new ConfigurationDAL();
            Configuration configuration = dal.ConfigurationSelectByName(null, null, configurationName);

            if (configuration != null)
            {
                configuration.ConfigurationValue = configurationValue;
                configuration = dal.ConfigurationUpdateAuto(null, null, configuration);
            }
            return configuration;
        }

        public bool SearchCatalogOnline()
        {
            return new ConfigurationDAL().SearchCatalogOnline(null, null);
        }
    }
}
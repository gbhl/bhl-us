using BHL.WebServiceREST.v1.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BHL.Export.IAID
{
    public class ExportProcessor : IBHLExport
    {
        // Create a default logger for use in this class
        private ExportLogger _log = new ExportLogger();

        private ConfigParms configParms = new ConfigParms();
        private Dictionary<string, int> _stats = new Dictionary<string, int>();
        private List<string> _errors = new List<string>();

        public Dictionary<string, int> Stats() { return _stats; }
        public List<string> Errors() { return _errors; }

        public void SetLogger(ExportLogger log)
        {
            if (log != null) _log = log;
        }

        /// <summary>
        /// Read and validate configuration parameters, and initiate the export.
        /// </summary>
        public void Process()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Generate the BibTeX
            // Generate the list of identifiers
            this.ProcessIdentifiers();
        }

        /// <summary>
        /// Get the list of identifiers and write it to a file.
        /// </summary>
        private void ProcessIdentifiers()
        {
            try
            {
                string statsKey = "IA IDs";
                _log.Info("Exporting identifiers...");

                // Get the list of identifiers
                ExportsClient restClient = new ExportsClient(configParms.BHLWSEndpoint);
                ICollection<string> identifiers = restClient.GetIAIdentifiers();

                // Build the content of the file
                StringBuilder sb = new StringBuilder();
                foreach (string identifier in identifiers)
                {
                    sb.AppendLine(identifier);
                    UpdateStats(statsKey);
                }

                // Write the content to the file
                if (!Directory.Exists(configParms.IAIDFolder)) Directory.CreateDirectory(configParms.IAIDFolder);
                File.WriteAllText(string.Format("{0}\\{1}", configParms.IAIDFolder, configParms.IAIDFile), sb.ToString());

                _log.Info(string.Format("{0} identifier(s) exported", _stats[statsKey]));
            }
            catch (Exception ex)
            {
                _log.Error("Exception processing identifiers.", ex);
                _errors.Add(string.Format("Exception processing identifiers:  {0}", ex.Message));
            }
        }

        /// <summary>
        /// Update the statistics for the given key value
        /// </summary>
        /// <param name="statsKey"></param>
        private void UpdateStats(string statsKey)
        {
            if (_stats.ContainsKey(statsKey))
                _stats[statsKey]++;
            else
                _stats.Add(statsKey, 1);
            if (_stats[statsKey] % 1000 == 0) _log.Info(string.Format("{0} {1} processed.", _stats[statsKey].ToString(), statsKey));
        }
    }
}

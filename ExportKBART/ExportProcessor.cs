using BHL.WebServiceREST.v1.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BHL.Export.KBART
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

            // Create the output folder, if it does not exist
            if (!Directory.Exists("data")) Directory.CreateDirectory("data");
            string outputFile = string.Format("data\\{0}", configParms.KBARTFile);
            if (File.Exists(outputFile)) File.Delete(outputFile);

            // Generate the KBART
            this.ProcessKBART(outputFile);
        }

        /// <summary>
        /// Get the list of identifiers and write it to a file.
        /// </summary>
        private void ProcessKBART(string outputFile)
        {
            try
            {
                string statsKey = "KBART";
                _log.Info("Exporting KBART...");

                // Get the list of identifiers
                ExportsClient restClient = new ExportsClient(configParms.BHLWSEndpoint);
                ICollection<BHL.WebServiceREST.v1.KBART> kbartList = restClient.GetKBART(configParms.KBARTUrlRoot);

                // Build the content of the file
                StringBuilder sb = new StringBuilder();

                // Add header
                string[] headerFields = new string[] {
                    "publication_title",
                    "print_identifier",
                    "online_identifier",
                    "date_first_issue_online",
                    "num_first_vol_online",
                    "num_first_issue_online",
                    "date_last_issue_online",
                    "num_last_vol_online",
                    "num_last_issue_online",
                    "title_url",
                    "first_author",
                    "title_id",
                    "embargo_info",
                    "coverage_depth",
                    "notes",
                    "publisher_name",
                    "publication_type",
                    "date_monograph_published_print",
                    "date_monograph_published_online",
                    "monograph_volume",
                    "monograph_edition",
                    "first_editor",
                    "parent_publication_title_id",
                    "preceding_publication_title_id",
                    "access_type"
                };
                sb.AppendLine(string.Join("\t", headerFields));

                int linesToWrite = 0;
                foreach (BHL.WebServiceREST.v1.KBART kbart in kbartList)
                {
                    linesToWrite++;

                    string kbartLine = 
                        string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}\t{23}\t{24}",
                        ScrubData(kbart.PublicationTitle),
                        ScrubData(kbart.PrintIdentifier),
                        ScrubData(kbart.OnlineIdentifier),
                        kbart.DateFirstIssueOnline,
                        kbart.NumFirstVolOnline,
                        kbart.NumFirstIssueOnline,
                        kbart.DateLastIssueOnline,
                        kbart.NumLastVolOnline,
                        kbart.NumLastIssueOnline,
                        kbart.TitleUrl,
                        ScrubData(kbart.FirstAuthor),
                        kbart.TitleID,
                        ScrubData(kbart.EmbargoInfo),
                        kbart.CoverageDepth,
                        kbart.Notes,
                        ScrubData(kbart.PublisherName),
                        kbart.PublicationType,
                        ScrubData(kbart.DateMonographPublishedPrint),
                        ScrubData(kbart.DateMonographPublishedOnline),
                        ScrubData(kbart.MonographVolume),
                        ScrubData(kbart.MonographEdition),
                        ScrubData(kbart.FirstEditor),
                        ScrubData(kbart.ParentPublicationTitleID),
                        ScrubData(kbart.PrecedingPublicationTitleID),
                        kbart.AccessType
                        );
                    sb.AppendLine(kbartLine);

                    UpdateStats(statsKey);

                    if (linesToWrite >= 100)
                    {
                        // Write the content to the file
                        File.AppendAllText(outputFile, sb.ToString(), Encoding.UTF8);
                        sb.Clear();
                        linesToWrite = 0;
                    }
                }

                // Write the last of the content to the file
                if (linesToWrite > 0) File.AppendAllText(outputFile, sb.ToString(), Encoding.UTF8);

                _log.Info(string.Format("{0} items exported", _stats[statsKey]));
            }
            catch (Exception ex)
            {
                _log.Error("Exception processing KBART.", ex);
                _errors.Add(string.Format("Exception processing KBART:  {0}", ex.Message));
            }
        }

        /// <summary>
        /// Remove invalid characters from the data to be exported
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string ScrubData(string data)
        {
            // Remove extra tabs and line breaks
            return data.Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' ');
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

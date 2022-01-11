using BHL.Export.RIS.BHLWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BHL.Export.RIS
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

            // validate config values
            if (!ValidateConfiguration()) return;

            // Generate the RIS
            ProcessRIS();
        }

        private void ProcessRIS()
        {
            BHLWS.BHLWSSoapClient service = null;

            try
            {
                service = new BHLWS.BHLWSSoapClient();
                service.InnerChannel.OperationTimeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                RISCitation[] citations = null;

                _log.Info("Processing items...");
                _log.Info("Getting RIS data for all items.");
                citations = service.ItemSelectAllRISCitations();
                GenerateCitations(citations, configParms.RISItemTempFile,
                    configParms.RISItemFile, configParms.RISItemZipFile,
                    configParms.RISInternalItemTempFile, configParms.RISInternalItemFile, 
                    configParms.RISInternalItemZipFile, "Items");
                _log.Info("Item processing complete.");

                _log.Info("Processing titles...");
                _log.Info("Getting RIS data for all titles.");
                citations = service.TitleSelectAllRISCitations();
                GenerateCitations(citations, configParms.RISTitleTempFile,
                    configParms.RISTitleFile, configParms.RISTitleZipFile,
                    configParms.RISInternalTitleTempFile, configParms.RISInternalTitleFile, 
                    configParms.RISInternalTitleZipFile, "Titles");
                _log.Info("Title processing complete.");

                _log.Info("Processing segments...");
                _log.Info("Getting RIS data for all segments.");
                citations = service.SegmentSelectAllRISCitations();
                GenerateCitations(citations, configParms.RISSegmentTempFile,
                    configParms.RISSegmentFile, configParms.RISSegmentZipFile,
                    configParms.RISInternalSegmentTempFile, configParms.RISInternalSegmentFile, 
                    configParms.RISInternalSegmentZipFile, "Segments");
                _log.Info("Segment processing complete.");
            }
            catch (Exception ex)
            {
                _log.Error("Exception processing citations.", ex);
                _errors.Add(string.Format("Exception processing citation:  {0}", ex.Message));
            }
        }

        private void GenerateCitations(RISCitation[] citations, string risTempFile,
            string risFile, string risZipFile, string risInternalTempFile,
            string risInternalFile, string risInternalZipFile, string statsKey)
        {
            // Clean up any existing temp files
            if (File.Exists(risTempFile)) File.Delete(risTempFile);
            if (File.Exists(risInternalTempFile)) File.Delete(risInternalTempFile);

            if (citations.Length > 0)
            {
                BHLWS.BHLWSSoapClient service = new BHLWSSoapClient();

                foreach (RISCitation citation in citations)
                {
                    try
                    {
                        string citationText = service.GenerateRISCitation(citation);
                        File.AppendAllText(risTempFile, citationText, Encoding.UTF8);
                        UpdateStats(statsKey);

                        // If this is content held internally within BHL, write it to the "internal" file
                        if (citation.HasLocalContent)
                        {
                            File.AppendAllText(risInternalTempFile, citationText, Encoding.UTF8);
                            UpdateStats(string.Format("{0} (Internal)", statsKey));
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(string.Format("Exception processing citation: {0}", citation.Url), ex);
                        _errors.Add(string.Format("Exception processing citation {0}:  {1}", citation.Url, ex.Message));
                        // don't bomb.  try next citation
                    }
                }

                if ((_errors.Count / Convert.ToDouble(citations.Length) * 100) > 1.0)
                {
                    _log.Error(string.Format("RIS processing failed. {0} out of {1} item citations produced errors.", _errors.Count.ToString(), citations.Length));
                    _errors.Add(string.Format("RIS processing failed. {0} out of {1} item citations produced errors.", _errors.Count.ToString(), citations.Length));
                }
                else
                {
                    // Move the newly created files to "production"
                    File.Delete(risFile);
                    File.Delete(risInternalFile);
                    if (File.Exists(risTempFile)) File.Move(risTempFile, risFile);
                    if (File.Exists(risInternalTempFile)) File.Move(risInternalTempFile, risInternalFile);

                    // Create a compressed version of the files
                    if (File.Exists(risFile)) new ExportFile(_log).Compress(risFile, risZipFile);
                    if (File.Exists(risInternalFile)) new ExportFile(_log).Compress(risInternalFile, risInternalZipFile);
                }
            }

            _log.Info("Citation processing complete.");
        }

        private void UpdateStats(string statsKey)
        {
            if (_stats.ContainsKey(statsKey))
                _stats[statsKey]++;
            else
                _stats.Add(statsKey, 1);
            if (_stats[statsKey] % 1000 == 0) _log.Info(string.Format("{0} {1} processed.", _stats[statsKey].ToString(), statsKey));
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            return true;
        }
    }
}

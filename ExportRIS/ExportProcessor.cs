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
                    configParms.RISItemFile, configParms.RISItemZipFile, "Items");
                _log.Info("Item processing complete.");

                _log.Info("Processing titles...");
                _log.Info("Getting RIS data for all titles.");
                citations = service.TitleSelectAllRISCitations();
                GenerateCitations(citations, configParms.RISTitleTempFile,
                    configParms.RISTitleFile, configParms.RISTitleZipFile, "Titles");
                _log.Info("Title processing complete.");

                _log.Info("Processing segments...");
                _log.Info("Getting RIS data for all segments.");
                citations = service.SegmentSelectAllRISCitations();
                GenerateCitations(citations, configParms.RISSegmentTempFile,
                    configParms.RISSegmentFile, configParms.RISSegmentZipFile, "Segments");
                _log.Info("Segment processing complete.");
            }
            catch (Exception ex)
            {
                _log.Error("Exception processing citations.", ex);
                _errors.Add("Exception processing citation:  " + ex.Message);
            }
        }

        private void GenerateCitations(RISCitation[] citations, string risTempFile,
            string risFile, string risZipFile, string statsKey)
        {
            // Clean up an existing temp file
            if (File.Exists(risTempFile)) File.Delete(risTempFile);

            if (citations.Length > 0)
            {
                BHLWS.BHLWSSoapClient service = new BHLWSSoapClient();

                double numErrors = 0;
                foreach (RISCitation citation in citations)
                {
                    try
                    {
                        string citationText = service.GenerateRISCitation(citation);
                        File.AppendAllText(risTempFile, citationText, Encoding.UTF8);
                        if (_stats.ContainsKey(statsKey))
                        {
                            _stats[statsKey]++;
                        }
                        else
                        {
                            _stats.Add(statsKey, 1);
                        }

                        _log.Info("Processing complete for citation: " + citation.Url);
                    }
                    catch (Exception ex)
                    {
                        _log.Error("Exception processing citation: " + citation.Url, ex);
                        _errors.Add("Exception processing citation " + citation.Url + ":  " + ex.Message);
                        numErrors++;
                        // don't bomb.  try next citation
                    }
                }

                if ((numErrors / Convert.ToDouble(citations.Length) * 100) > 1.0)
                {
                    _log.Error("RIS processing failed. " + numErrors.ToString() + " out of " + citations.Length + " item citations produced errors.");
                    _errors.Add("RIS processing failed. " + numErrors.ToString() + " out of " + citations.Length + " item citations produced errors.");
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(risFile);
                    File.Move(risTempFile, risFile);

                    // Create a compressed version of the file
                    new ExportFile(_log).Compress(risFile, risZipFile);
                }
            }

            _log.Info("Citation processing complete.");
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

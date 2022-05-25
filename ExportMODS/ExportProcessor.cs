using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BHL.Export.MODS
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

            // Generate the MODS files
            this.GenerateTitleMODS("Titles");
            this.GenerateItemMODS("Items");
            this.GenerateSegmentMODS("Segments");
        }

        private void GenerateTitleMODS(string statsKey)
        {
            ExportsClient restClient = new ExportsClient(configParms.BHLWSEndpoint);

            try
            {
                _log.Info(string.Format("Processing {0}...", statsKey));

                // Clean up any existing temp files
                if (File.Exists(configParms.MODSTitleTempFile)) File.Delete(configParms.MODSTitleTempFile);
                if (File.Exists(configParms.MODSInternalTitleTempFile)) File.Delete(configParms.MODSInternalTitleTempFile);

                _log.Info(string.Format("Getting data for all {0}.", statsKey));
                ICollection<Title> titles = new TitlesClient(configParms.BHLWSEndpoint).GetTitlesPublished();

                // Build the MODS files
                File.AppendAllText(configParms.MODSTitleTempFile, "<modsCollection>\n", Encoding.UTF8);
                File.AppendAllText(configParms.MODSInternalTitleTempFile, "<modsCollection>\n", Encoding.UTF8);
                foreach (Title title in titles)
                {
                    try
                    {
                        string mods = restClient.GetTitleMODS((int)title.TitleID);
                        File.AppendAllText(configParms.MODSTitleTempFile, mods, Encoding.UTF8);
                        UpdateStats(statsKey);

                        // If this is content held internally within BHL, write it to the "internal" file
                        if (title.HasLocalContent ?? false)
                        {
                            File.AppendAllText(configParms.MODSInternalTitleTempFile, mods, Encoding.UTF8);
                            UpdateStats(string.Format("{0} (Internal)", statsKey));
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(string.Format("Exception processing {0} ID {1}: ", statsKey, title.TitleID.ToString()), ex);
                        _errors.Add(string.Format("Exception processing {0} ID {1}: {2} ", statsKey, title.TitleID.ToString(), ex.Message));
                        // don't bomb.  try next title
                    }
                }
                File.AppendAllText(configParms.MODSTitleTempFile, "</modsCollection>\n", Encoding.UTF8);
                File.AppendAllText(configParms.MODSInternalTitleTempFile, "</modsCollection>\n", Encoding.UTF8);

                if ((_errors.Count / Convert.ToDouble(titles.Count) * 100) > 1.0)
                {
                    _log.Error(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), titles.Count, statsKey));
                    _errors.Add(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), titles.Count, statsKey));
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(configParms.MODSTitleFile);
                    File.Delete(configParms.MODSInternalTitleFile);
                    File.Move(configParms.MODSTitleTempFile, configParms.MODSTitleFile);
                    File.Move(configParms.MODSInternalTitleTempFile, configParms.MODSInternalTitleFile);

                    // Create a compressed version of the file
                    new ExportFile(_log).Compress(configParms.MODSTitleFile, configParms.MODSTitleZipFile);
                    new ExportFile(_log).Compress(configParms.MODSInternalTitleFile, configParms.MODSInternalTitleZipFile);
                }

                _log.Info(string.Format("{0} processing complete.", statsKey));
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Exception processing {0}.", statsKey), ex);
                _errors.Add(string.Format("Exception processing {0}: {1}", statsKey, ex.Message));
            }
        }

        private void GenerateItemMODS(string statsKey)
        {
            ExportsClient restClient = new ExportsClient(configParms.BHLWSEndpoint);

            try
            {
                _log.Info(string.Format("Processing {0}...", statsKey));

                // Clean up any existing temp files
                if (File.Exists(configParms.MODSItemTempFile)) File.Delete(configParms.MODSItemTempFile);
                if (File.Exists(configParms.MODSInternalItemTempFile)) File.Delete(configParms.MODSInternalItemTempFile);

                _log.Info(string.Format("Getting data for all {0}.", statsKey));
                ICollection<Item> items = new ItemsClient(configParms.BHLWSEndpoint).GetItemsPublished();

                // Build the MODS files
                File.AppendAllText(configParms.MODSItemTempFile, "<modsCollection>\n", Encoding.UTF8);
                File.AppendAllText(configParms.MODSInternalItemTempFile, "<modsCollection>\n", Encoding.UTF8);
                foreach (Item item in items)
                {
                    try
                    {
                        string mods = restClient.GetItemMODS((int)item.ItemID);
                        File.AppendAllText(configParms.MODSItemTempFile, mods, Encoding.UTF8);
                        UpdateStats(statsKey);

                        // If this is content held internally within BHL, write it to the "internal" file
                        if (item.HasLocalContent ?? false)
                        {
                            File.AppendAllText(configParms.MODSInternalItemTempFile, mods, Encoding.UTF8);
                            UpdateStats(string.Format("{0} (Internal)", statsKey));
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(string.Format("Exception processing {0} ID {1}: ", statsKey, item.ItemID.ToString()), ex);
                        _errors.Add(string.Format("Exception processing {0} ID {1}: {2} ", statsKey, item.ItemID.ToString(), ex.Message));
                        // don't bomb.  try next item
                    }
                }
                File.AppendAllText(configParms.MODSItemTempFile, "</modsCollection>\n", Encoding.UTF8);
                File.AppendAllText(configParms.MODSInternalItemTempFile, "</modsCollection>\n", Encoding.UTF8);

                if ((_errors.Count / Convert.ToDouble(items.Count) * 100) > 1.0)
                {
                    _log.Error(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), items.Count, statsKey));
                    _errors.Add(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), items.Count, statsKey));
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(configParms.MODSItemFile);
                    File.Delete(configParms.MODSInternalItemFile);
                    File.Move(configParms.MODSItemTempFile, configParms.MODSItemFile);
                    File.Move(configParms.MODSInternalItemTempFile, configParms.MODSInternalItemFile);

                    // Create a compressed version of the file
                    new ExportFile(_log).Compress(configParms.MODSItemFile, configParms.MODSItemZipFile);
                    new ExportFile(_log).Compress(configParms.MODSInternalItemFile, configParms.MODSInternalItemZipFile);
                }

                _log.Info(string.Format("{0} processing complete.", statsKey));
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Exception processing {0}.", statsKey), ex);
                _errors.Add(string.Format("Exception processing {0}: {1}", statsKey, ex.Message));
            }
        }

        private void GenerateSegmentMODS(string statsKey)
        {
            ExportsClient restClient = new ExportsClient(configParms.BHLWSEndpoint);

            try
            {
                _log.Info(string.Format("Processing {0}...", statsKey));

                // Clean up any existing temp files
                if (File.Exists(configParms.MODSSegmentTempFile)) File.Delete(configParms.MODSSegmentTempFile);
                if (File.Exists(configParms.MODSInternalSegmentTempFile)) File.Delete(configParms.MODSInternalSegmentTempFile);

                _log.Info(string.Format("Getting data for all {0}.", statsKey));
                ICollection<Segment> segments = new SegmentsClient(configParms.BHLWSEndpoint).GetSegmentsPublished();

                // Build the MODS files
                File.AppendAllText(configParms.MODSSegmentTempFile, "<modsCollection>\n", Encoding.UTF8);
                File.AppendAllText(configParms.MODSInternalSegmentTempFile, "<modsCollection>\n", Encoding.UTF8);
                foreach (Segment segment in segments)
                {
                    try
                    {
                        string mods = restClient.GetSegmentMODS((int)segment.SegmentID);
                        File.AppendAllText(configParms.MODSSegmentTempFile, mods, Encoding.UTF8);
                        UpdateStats(statsKey);

                        // If this is content held internally within BHL, write it to the "internal" file
                        if (segment.HasLocalContent ?? false)
                        {
                            File.AppendAllText(configParms.MODSInternalSegmentTempFile, mods, Encoding.UTF8);
                            UpdateStats(string.Format("{0} (Internal)", statsKey));
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(string.Format("Exception processing {0} ID {1}: ", statsKey, segment.SegmentID.ToString()), ex);
                        _errors.Add(string.Format("Exception processing {0} ID {1}: {2} ", statsKey, segment.SegmentID.ToString(), ex.Message));
                        // don't bomb.  try next segment
                    }
                }
                File.AppendAllText(configParms.MODSSegmentTempFile, "</modsCollection>\n", Encoding.UTF8);
                File.AppendAllText(configParms.MODSInternalSegmentTempFile, "</modsCollection>\n", Encoding.UTF8);

                if ((_errors.Count / Convert.ToDouble(segments.Count) * 100) > 1.0)
                {
                    _log.Error(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), segments.Count, statsKey));
                    _errors.Add(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), segments.Count, statsKey));
                }
                else
                {
                    // Move the newly created files to "production"
                    File.Delete(configParms.MODSSegmentFile);
                    File.Delete(configParms.MODSInternalSegmentFile);
                    File.Move(configParms.MODSSegmentTempFile, configParms.MODSSegmentFile);
                    File.Move(configParms.MODSInternalSegmentTempFile, configParms.MODSInternalSegmentFile);

                    // Create a compressed version of the files
                    new ExportFile(_log).Compress(configParms.MODSSegmentFile, configParms.MODSSegmentZipFile);
                    new ExportFile(_log).Compress(configParms.MODSInternalSegmentFile, configParms.MODSInternalSegmentZipFile);
                }

                _log.Info(string.Format("{0} processing complete.", statsKey));
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Exception processing {0}.", statsKey), ex);
                _errors.Add(string.Format("Exception processing {0}: {1}", statsKey, ex.Message));
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

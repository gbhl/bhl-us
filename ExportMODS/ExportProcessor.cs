using BHL.Export.MODS.BHLWS;
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
            BHLWS.BHLWSSoapClient service = null;

            try
            {
                _log.Info(string.Format("Processing {0}...", statsKey));

                // Clean up an existing temp file
                if (File.Exists(configParms.MODSTitleTempFile)) File.Delete(configParms.MODSTitleTempFile);

                _log.Info(string.Format("Getting data for all {0}.", statsKey));
                service = new BHLWSSoapClient();
                Title[] titles = service.TitleSelectAllPublished();

                // Build the MODS file
                File.AppendAllText(configParms.MODSTitleTempFile, "<modsCollection>\n", Encoding.UTF8);
                foreach (Title title in titles)
                {
                    try
                    {
                        string mods = service.GetMODSRecordForTitle(title.TitleID);
                        File.AppendAllText(configParms.MODSTitleTempFile, mods, Encoding.UTF8);
                        UpdateStats(statsKey);
                    }
                    catch (Exception ex)
                    {
                        _log.Error(string.Format("Exception processing {0} ID {1}: ", statsKey, title.TitleID.ToString()), ex);
                        _errors.Add(string.Format("Exception processing {0} ID {1}: {2} ", statsKey, title.TitleID.ToString(), ex.Message));
                        // don't bomb.  try next title
                    }
                }
                File.AppendAllText(configParms.MODSTitleTempFile, "</modsCollection>\n", Encoding.UTF8);

                if ((_errors.Count / Convert.ToDouble(titles.Length) * 100) > 1.0)
                {
                    _log.Error(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), titles.Length, statsKey));
                    _errors.Add(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), titles.Length, statsKey));
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(configParms.MODSTitleFile);
                    File.Move(configParms.MODSTitleTempFile, configParms.MODSTitleFile);

                    // Create a compressed version of the file
                    new ExportFile(_log).Compress(configParms.MODSTitleFile, configParms.MODSTitleZipFile);
                }

                _log.Info(string.Format("{0} processing complete.", statsKey));
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Exception processing {0}.", statsKey), ex);
                _errors.Add(string.Format("Exception processing {0}: {1}", statsKey, ex.Message));
            }
            finally
            {
                if (service != null) service.Close();
            }
        }

        private void GenerateItemMODS(string statsKey)
        {
            BHLWS.BHLWSSoapClient service = null;

            try
            {
                _log.Info(string.Format("Processing {0}...", statsKey));
                service = new BHLWS.BHLWSSoapClient();

                // Clean up an existing temp file
                if (File.Exists(configParms.MODSItemTempFile)) File.Delete(configParms.MODSItemTempFile);

                _log.Info(string.Format("Getting data for all {0}.", statsKey));
                service = new BHLWSSoapClient();
                Item[] items = service.ItemSelectPublished();

                // Build the MODS file
                File.AppendAllText(configParms.MODSItemTempFile, "<modsCollection>\n", Encoding.UTF8);
                foreach (Item item in items)
                {
                    try
                    {
                        string mods = service.GetMODSRecordForItem(item.ItemID);
                        File.AppendAllText(configParms.MODSItemTempFile, mods, Encoding.UTF8);
                        UpdateStats(statsKey);
                    }
                    catch (Exception ex)
                    {
                        _log.Error(string.Format("Exception processing {0} ID {1}: ", statsKey, item.ItemID.ToString()), ex);
                        _errors.Add(string.Format("Exception processing {0} ID {1}: {2} ", statsKey, item.ItemID.ToString(), ex.Message));
                        // don't bomb.  try next item
                    }
                }
                File.AppendAllText(configParms.MODSItemTempFile, "</modsCollection>\n", Encoding.UTF8);

                if ((_errors.Count / Convert.ToDouble(items.Length) * 100) > 1.0)
                {
                    _log.Error(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), items.Length, statsKey));
                    _errors.Add(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), items.Length, statsKey));
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(configParms.MODSItemFile);
                    File.Move(configParms.MODSItemTempFile, configParms.MODSItemFile);

                    // Create a compressed version of the file
                    new ExportFile(_log).Compress(configParms.MODSItemFile, configParms.MODSItemZipFile);
                }

                _log.Info(string.Format("{0} processing complete.", statsKey));
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Exception processing {0}.", statsKey), ex);
                _errors.Add(string.Format("Exception processing {0}: {1}", statsKey, ex.Message));
            }
            finally
            {
                if (service != null) service.Close();
            }
        }

        private void GenerateSegmentMODS(string statsKey)
        {
            BHLWS.BHLWSSoapClient service = null;

            try
            {
                _log.Info(string.Format("Processing {0}...", statsKey));

                // Clean up an existing temp file
                if (File.Exists(configParms.MODSSegmentTempFile)) File.Delete(configParms.MODSSegmentTempFile);

                _log.Info(string.Format("Getting data for all {0}.", statsKey));
                service = new BHLWSSoapClient();
                Segment[] segments = service.SegmentSelectPublished();

                // Build the MODS file
                File.AppendAllText(configParms.MODSSegmentTempFile, "<modsCollection>\n", Encoding.UTF8);
                foreach (Segment segment in segments)
                {
                    try
                    {
                        string mods = service.GetMODSRecordForSegment(segment.SegmentID);
                        File.AppendAllText(configParms.MODSSegmentTempFile, mods, Encoding.UTF8);
                        UpdateStats(statsKey);
                    }
                    catch (Exception ex)
                    {
                        _log.Error(string.Format("Exception processing {0} ID {1}: ", statsKey, segment.SegmentID.ToString()), ex);
                        _errors.Add(string.Format("Exception processing {0} ID {1}: {2} ", statsKey, segment.SegmentID.ToString(), ex.Message));
                        // don't bomb.  try next segment
                    }
                }
                File.AppendAllText(configParms.MODSSegmentTempFile, "</modsCollection>\n", Encoding.UTF8);

                if ((_errors.Count / Convert.ToDouble(segments.Length) * 100) > 1.0)
                {
                    _log.Error(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), segments.Length, statsKey));
                    _errors.Add(string.Format("MODS processing failed. {0} out of {1} {2} produced errors.", _errors.Count.ToString(), segments.Length, statsKey));
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(configParms.MODSSegmentFile);
                    File.Move(configParms.MODSSegmentTempFile, configParms.MODSSegmentFile);

                    // Create a compressed version of the file
                    new ExportFile(_log).Compress(configParms.MODSSegmentFile, configParms.MODSSegmentZipFile);
                }

                _log.Info(string.Format("{0} processing complete.", statsKey));
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Exception processing {0}.", statsKey), ex);
                _errors.Add(string.Format("Exception processing {0}: {1}", statsKey, ex.Message));
            }
            finally
            {
                if (service != null) service.Close();
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

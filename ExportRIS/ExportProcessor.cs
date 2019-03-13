using BHL.Export.RIS.BHLWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BHL.Export.RIS
{
    public class ExportProcessor : IBHLExport
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private Dictionary<string, int> _stats = new Dictionary<string, int>();
        private List<string> _errors = new List<string>();

        public Dictionary<string, int> Stats() { return _stats; }
        public List<string> Errors() { return _errors; }

        /// <summary>
        /// Read and validate configuration parameters, and initiate the appropriate
        /// processor.
        /// </summary>
        public void Process()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            // Generate the RIS
            this.ProcessRIS();
        }

        private void ProcessRIS()
        {
            BHLWS.BHLWSSoapClient service = null;

            try
            {
                service = new BHLWS.BHLWSSoapClient();
                service.InnerChannel.OperationTimeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                RISCitation[] citations = null;

                this.LogMessage("Processing items...");
                this.LogMessage("Getting RIS data for all items.");
                citations = service.ItemSelectAllRISCitations();
                this.GenerateCitations(citations, configParms.RISItemTempFile,
                    configParms.RISItemFile, configParms.RISItemZipFile, "Items");
                this.LogMessage("Item processing complete.");

                this.LogMessage("Processing titles...");
                this.LogMessage("Getting RIS data for all titles.");
                citations = service.TitleSelectAllRISCitations();
                this.GenerateCitations(citations, configParms.RISTitleTempFile,
                    configParms.RISTitleFile, configParms.RISTitleZipFile, "Titles");
                this.LogMessage("Title processing complete.");

                this.LogMessage("Processing segments...");
                this.LogMessage("Getting RIS data for all segments.");
                citations = service.SegmentSelectAllRISCitations();
                this.GenerateCitations(citations, configParms.RISSegmentTempFile,
                    configParms.RISSegmentFile, configParms.RISSegmentZipFile, "Segments");
                this.LogMessage("Segment processing complete.");
            }
            catch (Exception ex)
            {
                log.Error("Exception processing citations.", ex);
                _errors.Add("Exception processing citation:  " + ex.Message);
            }
        }

        private void GenerateCitations(RISCitation[] citations, String risTempFile,
            String risFile, String risZipFile, string statsKey)
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
                        String citationText = service.GenerateRISCitation(citation);
                        File.AppendAllText(risTempFile, citationText, Encoding.UTF8);
                        if (_stats.ContainsKey(statsKey))
                        {
                            _stats[statsKey]++;
                        }
                        else
                        {
                            _stats.Add(statsKey, 1);
                        }

                        this.LogMessage("Processing complete for citation: " + citation.Url);
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception processing citation: " + citation.Url, ex);
                        _errors.Add("Exception processing citation " + citation.Url + ":  " + ex.Message);
                        numErrors++;
                        // don't bomb.  try next citation
                    }
                }

                if ((numErrors / Convert.ToDouble(citations.Length) * 100) > 1.0)
                {
                    log.Error("RIS processing failed. " + numErrors.ToString() + " out of " + citations.Length + " item citations produced errors.");
                    _errors.Add("RIS processing failed. " + numErrors.ToString() + " out of " + citations.Length + " item citations produced errors.");
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(risFile);
                    File.Move(risTempFile, risFile);

                    // Create a compressed version of the file
                    this.CompressFile(risFile, risZipFile);
                }
            }

            this.LogMessage("Citation processing complete.");
        }

        private void CompressFile(String filename, String compressedFilename)
        {
            this.LogMessage("Generating a compressed version of the citations");
            String errorMessage = String.Empty;

            try
            {
                // Write the compressed file
                ICSharpCode.SharpZipLib.Zip.ZipFile zip = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(compressedFilename);
                zip.BeginUpdate();
                zip.Add(filename);
                zip.CommitUpdate();
                zip.Close();

                long originalSize = new FileInfo(filename).Length;
                long compressedSize = new FileInfo(compressedFilename).Length;
                this.LogMessage("Original size: " + originalSize.ToString() + ", Compressed size: " + compressedSize.ToString());

            } // end try
            catch (Exception ex)
            {
                errorMessage = "Error: Unable to compress file: " + ex.Message;
            }
            finally
            {
                if (errorMessage != String.Empty)
                {
                    log.Error(errorMessage);
                    _errors.Add(errorMessage);
                }
            }
        }

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        private bool ReadCommandLineArguments()
        {
            bool returnValue = true;
            return returnValue;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            return true;
        }

        private void LogMessage(string message)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");
        }
    }
}

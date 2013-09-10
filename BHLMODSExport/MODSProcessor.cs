using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using System.Linq;
using System.Text;
using MOBOT.BHL.BHLMODSExport.BHLWS;
using ICSharpCode.SharpZipLib;

namespace MOBOT.BHL.BHLMODSExport
{
    public class MODSProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> processedTitles = new List<string>();
        private List<string> processedItems = new List<string>();
        private List<string> processedSegments = new List<string>();
        private List<string> errorMessages = new List<string>();

        public void Process()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            // Generate the files
            this.GenerateTitleMODS();
            this.GenerateItemMODS();
            this.GenerateSegmentMODS();

            // Report the results of item/page processing
            this.ProcessResults();

            this.LogMessage("Processing Complete");
        }

        private void GenerateTitleMODS()
        {
            BHLWS.BHLWSSoapClient service = null;

            try
            {
                this.LogMessage("Processing titles...");

                // Clean up an existing temp file
                if (File.Exists(configParms.MODSTitleTempFile)) File.Delete(configParms.MODSTitleTempFile);

                this.LogMessage("Getting data for all titles.");
                service = new BHLWSSoapClient();
                Title[] titles = service.TitleSelectAllPublished();

                // Build the MODS file
                double numErrors = 0;
                File.AppendAllText(configParms.MODSTitleTempFile, "<modsCollection>\n", Encoding.UTF8);
                foreach (Title title in titles)
                {
                    try
                    {
                        string mods = service.GetMODSRecordForTitle(title.TitleID);
                        File.AppendAllText(configParms.MODSTitleTempFile, mods, Encoding.UTF8);
                        processedTitles.Add(title.TitleID.ToString());
                        if (processedTitles.Count % 100 == 0) this.LogMessage(processedTitles.Count.ToString() + " titles processed.");
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception processing title: " + title.TitleID.ToString(), ex);
                        errorMessages.Add("Exception processing title " + title.TitleID.ToString() + ":  " + ex.Message);
                        numErrors++;
                        // don't bomb.  try next title
                    }   
                }
                File.AppendAllText(configParms.MODSTitleTempFile, "</modsCollection>\n", Encoding.UTF8);

                if ((numErrors / Convert.ToDouble(titles.Length) * 100) > 1.0)
                {
                    log.Error("MODS processing failed. " + numErrors.ToString() + " out of " + titles.Length + " titles produced errors.");
                    errorMessages.Add("MODS processing failed. " + numErrors.ToString() + " out of " + titles.Length + " titles produced errors.");
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(configParms.MODSTitleFile);
                    File.Move(configParms.MODSTitleTempFile, configParms.MODSTitleFile);

                    // Create a compressed version of the file
                    this.CompressFile(configParms.MODSTitleFile, configParms.MODSTitleZipFile);
                }

                this.LogMessage("Title processing complete.");
            }
            catch (Exception ex)
            {
                log.Error("Exception processing titles.", ex);
                errorMessages.Add("Exception processing titles:  " + ex.Message);
            }
            finally
            {
                if (service != null) service.Close();
            }
        }

        private void GenerateItemMODS()
        {
            BHLWS.BHLWSSoapClient service = null;

            try
            {
                this.LogMessage("Processing items...");
                service = new BHLWS.BHLWSSoapClient();

                // Clean up an existing temp file
                if (File.Exists(configParms.MODSItemTempFile)) File.Delete(configParms.MODSItemTempFile);

                this.LogMessage("Getting data for all items.");
                service = new BHLWSSoapClient();
                Item[] items = service.ItemSelectPublished();

                // Build the MODS file
                double numErrors = 0;
                File.AppendAllText(configParms.MODSItemTempFile, "<modsCollection>\n", Encoding.UTF8);
                foreach (Item item in items)
                {
                    try
                    {
                        string mods = service.GetMODSRecordForItem(item.ItemID);
                        File.AppendAllText(configParms.MODSItemTempFile, mods, Encoding.UTF8);
                        processedItems.Add(item.ItemID.ToString());
                        if (processedItems.Count % 100 == 0) this.LogMessage(processedItems.Count.ToString() + " items processed.");
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception processing item: " + item.ItemID.ToString(), ex);
                        errorMessages.Add("Exception processing item " + item.ItemID.ToString() + ":  " + ex.Message);
                        numErrors++;
                        // don't bomb.  try next item
                    }
                }
                File.AppendAllText(configParms.MODSItemTempFile, "</modsCollection>\n", Encoding.UTF8);

                if ((numErrors / Convert.ToDouble(items.Length) * 100) > 1.0)
                {
                    log.Error("MODS processing failed. " + numErrors.ToString() + " out of " + items.Length + " items produced errors.");
                    errorMessages.Add("MODS processing failed. " + numErrors.ToString() + " out of " + items.Length + " items produced errors.");
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(configParms.MODSItemFile);
                    File.Move(configParms.MODSItemTempFile, configParms.MODSItemFile);

                    // Create a compressed version of the file
                    this.CompressFile(configParms.MODSItemFile, configParms.MODSItemZipFile);
                }

                this.LogMessage("Item processing complete.");
            }
            catch (Exception ex)
            {
                log.Error("Exception processing items.", ex);
                errorMessages.Add("Exception processing items:  " + ex.Message);
            }
            finally
            {
                if (service != null) service.Close();
            }
        }

        private void GenerateSegmentMODS()
        {
            BHLWS.BHLWSSoapClient service = null;

            try
            {
                this.LogMessage("Processing segments...");

                // Clean up an existing temp file
                if (File.Exists(configParms.MODSSegmentTempFile)) File.Delete(configParms.MODSSegmentTempFile);

                this.LogMessage("Getting data for all segments.");
                service = new BHLWSSoapClient();
                Segment[] segments = service.SegmentSelectPublished();

                // Build the MODS file
                double numErrors = 0;
                File.AppendAllText(configParms.MODSSegmentTempFile, "<modsCollection>\n", Encoding.UTF8);
                foreach (Segment segment in segments)
                {
                    try
                    {
                        string mods = service.GetMODSRecordForSegment(segment.SegmentID);
                        File.AppendAllText(configParms.MODSSegmentTempFile, mods, Encoding.UTF8);
                        processedSegments.Add(segment.SegmentID.ToString());
                        if (processedSegments.Count % 100 == 0) this.LogMessage(processedSegments.Count.ToString() + " segments processed.");
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception processing segment: " + segment.SegmentID.ToString(), ex);
                        errorMessages.Add("Exception processing segment " + segment.SegmentID.ToString() + ":  " + ex.Message);
                        numErrors++;
                        // don't bomb.  try next segment
                    }
                }
                File.AppendAllText(configParms.MODSSegmentTempFile, "</modsCollection>\n", Encoding.UTF8);

                if ((numErrors / Convert.ToDouble(segments.Length) * 100) > 1.0)
                {
                    log.Error("MODS processing failed. " + numErrors.ToString() + " out of " + segments.Length + " segments produced errors.");
                    errorMessages.Add("MODS processing failed. " + numErrors.ToString() + " out of " + segments.Length + " segments produced errors.");
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(configParms.MODSSegmentFile);
                    File.Move(configParms.MODSSegmentTempFile, configParms.MODSSegmentFile);

                    // Create a compressed version of the file
                    this.CompressFile(configParms.MODSSegmentFile, configParms.MODSSegmentZipFile);
                }

                this.LogMessage("Segment processing complete.");
            }
            catch (Exception ex)
            {
                log.Error("Exception processing segments.", ex);
                errorMessages.Add("Exception processing segments:  " + ex.Message);
            }
            finally
            {
                if (service != null) service.Close();
            }
        }

        /// <summary>
        /// Zip the specified file
        /// </summary>
        /// <param name="filename">Name of file to be compressed</param>
        /// <param name="compressedFilename">Name to assign to the compressed file</param>
        private void CompressFile(String filename, String compressedFilename)
        {
            this.LogMessage("Generating a compressed version of " + filename);
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
                    errorMessages.Add(errorMessage);
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

        /// <summary>
        /// Examine the results of the processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // send email with process results to Exchange group
                if (processedTitles.Count > 0 || processedItems.Count > 0 || processedSegments.Count > 0 || errorMessages.Count > 0)
                {
                    this.LogMessage("Sending Email....");
                    string message = this.GetEmailBody();
                    this.LogMessage(message);
                    this.SendEmail(message);
                }
                else
                {
                    this.LogMessage("Nothing processed.  Email not sent.");
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception sending email.", ex);
                return;
            }
        }

        /// <summary>
        /// Constructs the body of an email message to be sent
        /// </summary>
        /// <returns>Body of email message to be sent</returns>
        private string GetEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            sb.Append("BHLMODSExport: Processing  on " + thisComputer + " complete." + endOfLine);
            if (this.processedTitles.Count > 0)
            {
                sb.Append(endOfLine + "Processed " + this.processedTitles.Count.ToString() + " Titles" + endOfLine);
            }
            if (this.processedItems.Count > 0)
            {
                sb.Append(endOfLine + "Processed " + this.processedItems.Count.ToString() + " Items" + endOfLine);
            }
            if (this.processedSegments.Count > 0)
            {
                sb.Append(endOfLine + "Processed " + this.processedSegments.Count.ToString() + " Segments" + endOfLine);
            }
            if (this.errorMessages.Count > 0)
            {
                sb.Append(endOfLine + this.errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details");
                foreach (string message in errorMessages)
                {
                    sb.Append(message + endOfLine);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Send the specified email message 
        /// </summary>
        /// <param name="message">Body of the message to be sent</param>
        private void SendEmail(string message)
        {
            try
            {
                string thisComputer = Environment.MachineName;
                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress(configParms.EmailFromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(configParms.EmailToAddress);
                if (this.errorMessages.Count == 0)
                {
                    mailMessage.Subject = "BHLMODSExport: Processing on " + thisComputer + " completed successfully.";
                }
                else
                {
                    mailMessage.Subject = "BHLMODSExport: Processing on " + thisComputer + " completed with errors.";
                }
                mailMessage.Body = message;

                SmtpClient smtpClient = new SmtpClient(configParms.SMTPHost);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                log.Error("Email Exception: ", ex);
            }
        }

        private void LogMessage(string message)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Data.Common;
using Newtonsoft.Json;
using System.Collections;
using System.Data.Sql;
using System.Data;
using Ionic.Zip;

namespace MOBOT.BHL.BHLAuditExport
{
    public class ExportProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> auditRows = new List<string>();
        private List<string> errorMessages = new List<string>();

        private Dictionary<string, string> primaryKeyNames = new Dictionary<string, string>();
        private DateTime lastAuditDate = DateTime.Now;
        private int filesWritten = 0;
        List<string> jsonFiles = new List<string>();

        public void Process()
        {
            this.LogMessage("BHLAuditExport Processing Starting");

            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            // Get the primary key column names
            if (this.fillPrimaryKeyNames())
            {
                // Create the export file
                this.CreateAuditExport();
            }

            // Report the results of pdf generation
            this.ProcessResults();

            this.LogMessage("BHLAuditExport Processing Complete");
        }

        public bool fillPrimaryKeyNames()
        {
            DataTable tmpDT = DataAbstractLayer.GetPrimaryKeys();
            try
            {
                string LastTable = null;
                foreach (DataRow tmpDR in tmpDT.Rows)
                {
                    int KeyCounter = 0;
                    string CurrentTable = tmpDR["TABLE_NAME"].ToString();
                    if (CurrentTable == LastTable)
                    {
                        KeyCounter++;
                    }
                    else
                    {
                        KeyCounter = 0;
                    }

                    try
                    {
                        if (primaryKeyNames.ContainsKey(String.Concat(tmpDR["TABLE_SCHEMA"], ".", tmpDR["TABLE_NAME"].ToString())))
                        {
                            primaryKeyNames.Add(String.Concat(tmpDR["TABLE_SCHEMA"], ".", tmpDR["TABLE_NAME"].ToString(), KeyCounter.ToString()), tmpDR["COLUMN_NAME"].ToString());
                        }
                        else
                        {
                            primaryKeyNames.Add(String.Concat(tmpDR["TABLE_SCHEMA"], ".", tmpDR["TABLE_NAME"].ToString()), tmpDR["COLUMN_NAME"].ToString());
                        }
                    }
                    catch
                    {
                    }

                    LastTable = tmpDR["TABLE_NAME"].ToString();
                }

                this.LogMessage("Primary key column names retrieved");
                return true;
            }
            catch (Exception ex)
            {
                this.LogMessage("Error getting primary key column names: " + ex.Message, true);
                return false;
            }
        }

        public void CreateAuditExport()
        {
            this.LogMessage("Audit Export Starting");

            try
            {
                // Get the audit records
                CreateAuditFiles(configParms.LastExportDate);

                // Zip the audit files and copy the zip to the web site
                CreateZipFile();
                
                // Save the date of the last audit record
                configParms.UpdateAppSetting(ConfigParms.LastExportDateKey, this.lastAuditDate);

                this.LogMessage("Audit Export Complete");
            }
            catch (Exception ex)
            {
                this.LogMessage("Error exporting data: " + ex.Message, true);
            }
        }

        public void CreateAuditFiles(DateTime lastExportDate)
        {
            this.LogMessage("Audit File Creation Starting");

            List<AuditEntry> auditEntryList = new List<AuditEntry>();

            IDataReader tbAuditRecords = DataAbstractLayer.RecentAuditTable(lastExportDate);

            int rowCount = 0;
            while (tbAuditRecords.Read())
            {
                // Save the date from the last audit record
                this.lastAuditDate = tbAuditRecords.GetDateTime(tbAuditRecords.GetOrdinal("AuditDate"));

                string auditOperation = tbAuditRecords.GetString(tbAuditRecords.GetOrdinal("Operation")); // dr["Operation"].ToString();
                string auditEntity = tbAuditRecords.GetString(tbAuditRecords.GetOrdinal("EntityName")); // dr["EntityName"].ToString();
                Dictionary<string, string> entityKeys = new Dictionary<string, string>();

                entityKeys.Add(primaryKeyNames[tbAuditRecords.GetString(tbAuditRecords.GetOrdinal("EntityName"))], tbAuditRecords.GetString(tbAuditRecords.GetOrdinal("EntityKey1")));
                //this relies on key count, not as flexible as rest of system

                if (!tbAuditRecords.IsDBNull(tbAuditRecords.GetOrdinal("EntityKey2")))
                {
                    if (!string.IsNullOrEmpty(tbAuditRecords.GetString(tbAuditRecords.GetOrdinal("EntityKey2"))))
                    {
                        entityKeys.Add(primaryKeyNames[tbAuditRecords.GetString(tbAuditRecords.GetOrdinal("EntityName")) + 1], tbAuditRecords.GetString(tbAuditRecords.GetOrdinal("EntityKey2")));
                    }
                }

                if (!tbAuditRecords.IsDBNull(tbAuditRecords.GetOrdinal("EntityKey3")))
                {
                    if (!string.IsNullOrEmpty(tbAuditRecords.GetString(tbAuditRecords.GetOrdinal("EntityKey3"))))
                    {
                        entityKeys.Add(primaryKeyNames[tbAuditRecords.GetString(tbAuditRecords.GetOrdinal("EntityName")) + 2], tbAuditRecords.GetString(tbAuditRecords.GetOrdinal("EntityKey3")));
                    }
                }

                AuditEntry auditEntry = CreateAuditEntry(auditOperation, auditEntity, entityKeys);
                if (auditEntry != null)
                {
                    auditEntryList.Add(auditEntry);
                    auditRows.Add(auditOperation);

                    rowCount++;
                    if (rowCount >= 100000)
                    {
                        // Serialize the audit records into a JSON object
                        string retJSON = JsonConvert.SerializeObject(auditEntryList);

                        // Write JSON to a file
                        this.CreateExportFile(retJSON);

                        auditEntryList.Clear();
                        rowCount = 0;
                    }
                }
            }

            if (rowCount > 0)
            {
                // Serialize the audit records into a JSON object
                string retJSON = JsonConvert.SerializeObject(auditEntryList);

                // Write JSON to a file
                this.CreateExportFile(retJSON);
            }

            this.LogMessage("Audit File Creation Complete");
        }

        public AuditEntry CreateAuditEntry(string auditOperation, string tableName, Dictionary<string, string> entityKeys)
        {
            this.LogMessage("Creating audit entry for operation:" + auditOperation + ", table:" + tableName + ", key1:" + entityKeys.First().Value);

            AuditEntry auditEntry = null;

            DataRow rowData = null;
            if (auditOperation != "D") rowData = DataAbstractLayer.GetDataForRow(tableName, entityKeys);
            if (rowData != null || auditOperation == "D")    // We might get null rowData here if the target row has been deleted
            {
                auditEntry = new AuditEntry();

                switch (auditOperation)
                {
                    case "U":
                        auditEntry.type = "update";
                        break;
                    case "I":
                        auditEntry.type =  "insert";
                        break;
                    case "D":
                        auditEntry.type = "delete";
                        break;
                }

                auditEntry.table = tableName;
                int pkCounter = 0; //primary key counter
                foreach (KeyValuePair<string, string> tmpKVP in entityKeys)
                {
                    //Need to get the Column Name, which is the Key
                    switch (pkCounter)
                    {
                        case 0: auditEntry.primaryKeyName0 = tmpKVP.Key;break;
                        case 1: auditEntry.primaryKeyName1 = tmpKVP.Key;break;
                        case 2: auditEntry.primaryKeyName2 = tmpKVP.Key;break;
                    }
                    pkCounter++;
                }

                if (auditOperation != "D")
                {
                    // Add data retrieved from database to the audit entry
                    foreach (DataColumn item in rowData.Table.Columns)
                    {
                        auditEntry.row.Add(item.ColumnName, rowData.IsNull(item.ColumnName) ? "dbnull" : rowData[item.ColumnName].ToString());
                    }
                }
                else
                {
                    // Add key data to the audit entry
                    foreach (KeyValuePair<string, string> tmpKVP in entityKeys)
                    {
                        auditEntry.row.Add(tmpKVP.Key, tmpKVP.Value);
                    }
                }
            }
            return auditEntry;
        }

        public void CreateExportFile(string json)
        {
            this.LogMessage("Export File Creation Starting");

            filesWritten++;

            // Write the JSON to a file, zip the file, and then remove the (non-zip) file
            string jsonFilename = String.Format(configParms.JsonFilenameFormat, lastAuditDate.ToString("yyyyMMddHHmmss") + "." + filesWritten.ToString("000"));
            File.WriteAllText(configParms.TempFolder + jsonFilename, json, Encoding.Unicode);
            jsonFiles.Add(jsonFilename);

            this.LogMessage("Export File Creation Complete");
        }

        public void CreateZipFile()
        {
            this.LogMessage("Zip File Creation Starting");

            string zipFileName = String.Format(configParms.ZipFilenameFormat, lastAuditDate.ToString("yyyyMMddHHmmss"));

            // Add the individual json files to a zip file
            using (ZipFile zip = new ZipFile())
            {
                foreach (string jsonFilename in jsonFiles)
                {
                    string jsonFilenameRename = jsonFilename.Remove(5, 14).Insert(5, lastAuditDate.ToString("yyyyMMddHHmmss"));
                    File.Move(configParms.TempFolder + jsonFilename, configParms.TempFolder + jsonFilenameRename);
                    zip.AddFile(configParms.TempFolder + jsonFilenameRename, "\\");
                }
                zip.Save(configParms.TempFolder + zipFileName);
            }

            // Remove the individual json files
            foreach (string jsonFilename in jsonFiles)
            {
                File.Delete(configParms.TempFolder + jsonFilename.Remove(5, 14).Insert(5, lastAuditDate.ToString("yyyyMMddHHmmss")));
            }

            // Copy zip file to network location
            File.Copy(configParms.TempFolder + zipFileName, configParms.WebFolder + zipFileName, true);
            File.Delete(configParms.TempFolder + zipFileName);

            // Update the manifest of zip files
            File.AppendAllText(configParms.WebFolder + configParms.AuditExportManifest,
                String.Format("{0},{1},{2},{3}\n",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    zipFileName,
                    auditRows.Count.ToString(),
                    filesWritten.ToString()));

            this.LogMessage("Zip File Creation Complete");
        }

        #region Get and validate parameters

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        private bool ReadCommandLineArguments()
        {
            return true;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            return true;
        }

        #endregion Get and validate parameters

        #region Process results

        /// <summary>
        /// Examine the results of the item/page processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                if (auditRows.Count > 0 || errorMessages.Count > 0)
                {
                    String subject = String.Empty;
                    String thisComputer = Environment.MachineName;
                    if (this.errorMessages.Count == 0)
                    {
                        subject = "BHLAuditExport: Processing on " + thisComputer + " completed successfully.";
                    }
                    else
                    {
                        subject = "BHLAuditExport: Processing on " + thisComputer + " completed with errors.";
                    }

                    this.LogMessage("Sending Email....");
                    String message = this.GetCompletionEmailBody();
                    this.LogMessage(message);
                    this.SendEmail(subject, message, configParms.EmailFromAddress, configParms.EmailToAddress, "");
                }
                else
                {
                    this.LogMessage("Nothing to do.  Email not sent.");
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
        private String GetCompletionEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            sb.Append("BHLAuditExport: Processing on " + thisComputer + " complete." + endOfLine);
            if (this.auditRows.Count > 0)
            {
                sb.Append(endOfLine + this.auditRows.Count.ToString() + " audit entries were exported" + endOfLine);
            }
            if (this.errorMessages.Count > 0)
            {
                sb.Append(endOfLine + this.errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details" + endOfLine + endOfLine);
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
        private void SendEmail(String subject, String message, String fromAddress,
            String toAddress, String ccAddresses)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress(fromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(toAddress);
                if (ccAddresses != String.Empty) mailMessage.CC.Add(ccAddresses);
                mailMessage.Subject = subject;
                mailMessage.Body = message;

                SmtpClient smtpClient = new SmtpClient(configParms.SMTPHost);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                log.Error("Email Exception: ", ex);
            }
        }

        #endregion Process results

        #region Logging

        private void LogMessage(string message)
        {
            this.LogMessage(message, false);
        }

        private void LogMessage(string message, bool isError)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");

            // If this is an error message, add it to the in-memory list of error messages
            if (isError) errorMessages.Add(message);
        }


        #endregion Logging
    }
}

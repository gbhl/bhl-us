using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using MOBOT.BHL.BHLAuditExport;
using System.ComponentModel;

namespace MOBOT.BHL.BHLAuditImport
{
    class JSON_Load
    {
        private string _myDatabaseName;

        public string myDatabaseName
        {
            get { return _myDatabaseName; }
            set { _myDatabaseName = value; }
        }

        public JSON_Load(string databaseName)
        {
            this.myDatabaseName = databaseName;
        }

        public string  loadJSON()
        {
            string retString = null;
            StreamReader streamReader = new StreamReader(@"c:\audit20112811163638.json");
            string loadedJSON = streamReader.ReadToEnd();
            streamReader.Close();
 
            List<AuditEntry> values = JsonConvert.DeserializeObject<List<AuditEntry>>(loadedJSON);

            if (values.Count > 0)
            {
                retString = @"Success!";

                foreach (AuditEntry tmpObject in values)
                {
                    string sqlStatement = null;
                    switch (tmpObject.type)
                    {
                        case "insert":
                        sqlStatement=InsertStatement(tmpObject);
                            break;
                        case "update":
                        sqlStatement = UpdateStatement(tmpObject);
                            break;
                        case "delete":
                        sqlStatement = DeleteStatement(tmpObject);
                            break;
                    }
                }
            }

            return retString;
        }

        public string loadJSON(string[] filenames)
        {
            string retString = null;
            int processCount = 0;
            bool hasErrorOccurred = false;
            DataAbstractLayer myDataAbstract = new DataAbstractLayer(this.myDatabaseName);
            string loadedJSON;
            string LogFileAppend = String.Concat(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute);
            string LogFileName = String.Concat(@"c:\ImportLog", LogFileAppend, ".txt");

            if (File.Exists(LogFileName))
            {
                File.Delete(LogFileName);
            }

            foreach (string tmpFileName in filenames)
            {
                StreamReader streamReader = new StreamReader(tmpFileName, Encoding.Unicode);
                loadedJSON = String.Empty;
                loadedJSON = streamReader.ReadToEnd();
                streamReader.Close();

                StreamWriter writerVer = new StreamWriter(LogFileName, true);
                writerVer.WriteLine(String.Concat(DateTime.Now.ToShortDateString(), " ", DateTime.Now.ToShortTimeString(), @"Filename:", tmpFileName, "\r\n"));
                writerVer.Flush();
                writerVer.Close();

                List<AuditEntry> values = JsonConvert.DeserializeObject<List<AuditEntry>>(loadedJSON);

                if (values.Count > 0)
                {

                    retString = @"Success!";

                    foreach (AuditEntry tmpObject in values)
                    {
                        string sqlStatement = null;
                        switch (tmpObject.type)
                        {
                            case "insert":
                                sqlStatement = InsertStatement(tmpObject);
                                break;
                            case "update":
                                sqlStatement = UpdateStatement(tmpObject);
                                break;
                            case "delete":
                                sqlStatement = DeleteStatement(tmpObject);
                                break;

                        }
                        processCount++;
                        bool boolResult = myDataAbstract.RunObjectDataCommmand(sqlStatement, LogFileName);
                        if (!boolResult)
                        {
                            hasErrorOccurred = true;
                        }
                    }
                }
            }
            if (hasErrorOccurred)
            {
                retString = hasErrorOccurred.ToString();
            }
            return retString;
        }

        public string loadJSON(BackgroundWorker sender, string[] filenames)
        {
            string retString = null;
            int processCount = 0;
            bool hasErrorOccurred = false;
            DataAbstractLayer myDataAbstract = new DataAbstractLayer(this.myDatabaseName);
            string loadedJSON;
            string LogFileAppend = String.Concat(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute);
            string LogFileName = String.Concat(@"c:\ImportLog", LogFileAppend, ".txt");

            if (File.Exists(LogFileName))
            {
                File.Delete(LogFileName);
            }
            foreach (string tmpFileName in filenames)
            {
                StreamReader streamReader = new StreamReader(tmpFileName, Encoding.Unicode);       
                loadedJSON = String.Empty;
                loadedJSON = streamReader.ReadToEnd();
                streamReader.Close();

                StreamWriter writerVer = new StreamWriter(LogFileName, true);
                writerVer.WriteLine(String.Concat(DateTime.Now.ToShortDateString(), " ",DateTime.Now.ToShortTimeString(), @"Filename:",tmpFileName, "\r\n"));
                writerVer.Flush();
                writerVer.Close();

                List<AuditEntry> values = JsonConvert.DeserializeObject<List<AuditEntry>>(loadedJSON);

                if (values.Count > 0)
                {
                    retString = @"Success!";

                    foreach (AuditEntry tmpObject in values)
                    {
                        string sqlStatement = null;
                        switch (tmpObject.type)
                        {
                            case "insert":
                                sqlStatement = InsertStatement(tmpObject);
                                break;
                            case "update":
                                sqlStatement = UpdateStatement(tmpObject);
                                break;
                            case "delete":
                                sqlStatement = DeleteStatement(tmpObject);
                                break;
                        }
                        processCount++;
                        bool boolResult = myDataAbstract.RunObjectDataCommmand(sqlStatement, LogFileName);
                        if (!boolResult)
                        {
                            hasErrorOccurred = true;
                        }

                        double progress = (Convert.ToDouble(processCount) /( Convert.ToDouble(values.Count) * Convert.ToDouble(filenames.Length))) * 100;

                        sender.ReportProgress(Convert.ToInt32(Math.Floor(progress)));
                    }
                }
            }
            if (hasErrorOccurred)
            {
                retString = hasErrorOccurred.ToString();
            }
            return retString;
        }

        public string UpdateStatement(AuditEntry updateRecord) {

            StringBuilder updateStatement = new StringBuilder();
            StringBuilder updateValues = new StringBuilder();

            updateStatement.Append(@"UPDATE ");
            updateStatement.Append(updateRecord.table);
            updateStatement.Append(@" SET ");
            foreach (KeyValuePair<string, string> tmpKVP in updateRecord.row)
            {
                if (tmpKVP.Key.ToString() != updateRecord.primaryKeyName0 && tmpKVP.Key.ToString() != updateRecord.primaryKeyName1 && tmpKVP.Key.ToString() != updateRecord.primaryKeyName2) // primaryKeyName0 may be an identity column unless there is a value in primaryKeyName1
                {
                    updateValues.Append(tmpKVP.Key.ToString());
                    if (!String.Equals(tmpKVP.Value.ToString(),"dbnull"))
                    {
                        updateValues.Append(@"='");
                        updateValues.Append(tmpKVP.Value.ToString().Replace("'","''"));
                        updateValues.Append(@"',");
                    }
                    else {
                        updateValues.Append(@"=null,");
                    }
                }
            }

            updateValues.Remove(updateValues.Length - 1, 1);

            updateStatement.Append(updateValues.ToString());
            updateStatement.Append(@" WHERE ");

            updateStatement.Append(String.Concat(updateRecord.primaryKeyName0, "='", updateRecord.row[updateRecord.primaryKeyName0].ToString().Replace("'", "''"), "'"));
            if (! string.IsNullOrEmpty(updateRecord.primaryKeyName1)){
                updateStatement.Append(String.Concat(" AND ", updateRecord.primaryKeyName1, "='", updateRecord.row[updateRecord.primaryKeyName1].ToString().Replace("'", "''"), "'"));
            }
            if (! string.IsNullOrEmpty(updateRecord.primaryKeyName2)){
                updateStatement.Append(String.Concat(" AND ", updateRecord.primaryKeyName2, "='", updateRecord.row[updateRecord.primaryKeyName2].ToString().Replace("'", "''"), "'"));
            }

            return updateStatement.ToString();
        }

        public string InsertStatement(AuditEntry insertRecord)
        {
            StringBuilder insertStatement = new StringBuilder();
            StringBuilder insertColumns = new StringBuilder();
            StringBuilder insertValues = new StringBuilder();

            insertStatement.Append(@"If not exists (select * from ");
            insertStatement.Append(insertRecord.table);
            insertStatement.Append(@" WHERE ");
            insertStatement.Append(String.Concat(insertRecord.primaryKeyName0, "='", insertRecord.row[insertRecord.primaryKeyName0].ToString().Replace("'","''"), "'"));
            if (!string.IsNullOrEmpty(insertRecord.primaryKeyName1))
            {
                insertStatement.Append(String.Concat(" AND ", insertRecord.primaryKeyName1, "='", insertRecord.row[insertRecord.primaryKeyName1].ToString().Replace("'", "''"), "'"));
            }
            if (!string.IsNullOrEmpty(insertRecord.primaryKeyName2))
            {
                insertStatement.Append(String.Concat(" AND ", insertRecord.primaryKeyName2, "='", insertRecord.row[insertRecord.primaryKeyName2].ToString().Replace("'", "''"), "'"));
            }

            insertStatement.Append(") BEGIN ");
            insertStatement.Append(String.Concat("if (SELECT OBJECTPROPERTY(object_id('", insertRecord.table, "'), 'TableHasIdentity'))=1 BEGIN"));
            insertStatement.Append(" SET IDENTITY_INSERT ");
            insertStatement.Append(insertRecord.table);
            insertStatement.Append(" ON; ");
            insertStatement.Append(" END; ");

            insertStatement.Append(@"INSERT ");
            insertStatement.Append(insertRecord.table);
            foreach (KeyValuePair<string, string> tmpKVP in insertRecord.row)
            {
                insertColumns.Append(tmpKVP.Key.ToString());
                insertColumns.Append(@",");
                if (!String.Equals(tmpKVP.Value.ToString(),"dbnull"))
                {
                    insertValues.Append(String.Concat("'", tmpKVP.Value.ToString().Replace("'","''"), "',"));
                }
                else
                {
                    insertValues.Append(String.Concat("null,"));
                }
            }
            insertColumns.Remove(insertColumns.Length - 1, 1);
            insertValues.Remove(insertValues.Length - 1, 1);

            insertStatement.Append(String.Concat(" (", insertColumns.ToString(), ") VALUES (", insertValues.ToString(), "); "));
            insertStatement.Append(String.Concat("IF (SELECT OBJECTPROPERTY(object_id('", insertRecord.table, "'), 'TableHasIdentity'))=1 BEGIN"));
            insertStatement.Append(" SET IDENTITY_INSERT ");
            insertStatement.Append(insertRecord.table);
            insertStatement.Append(" OFF;");
            insertStatement.Append(" END; ");
            insertStatement.Append(" END;");

            return insertStatement.ToString();
        }

        public string DeleteStatement(AuditEntry deleteRecord)
        {
            StringBuilder deleteStatement = new StringBuilder();

            deleteStatement.Append(@"DELETE FROM ");
            deleteStatement.Append(deleteRecord.table);
            deleteStatement.Append(@" WHERE ");
            deleteStatement.Append(String.Concat(deleteRecord.primaryKeyName0, "='", deleteRecord.row[deleteRecord.primaryKeyName0].ToString().Replace("'", "''"), "'"));
            if (!string.IsNullOrEmpty(deleteRecord.primaryKeyName1))
            {
                deleteStatement.Append(String.Concat(" AND ", deleteRecord.primaryKeyName1, "='", deleteRecord.row[deleteRecord.primaryKeyName1].ToString().Replace("'", "''"), "'"));
            }
            if (!string.IsNullOrEmpty(deleteRecord.primaryKeyName2))
            {
                deleteStatement.Append(String.Concat(" AND ", deleteRecord.primaryKeyName2, "='", deleteRecord.row[deleteRecord.primaryKeyName2].ToString().Replace("'","''"),"'"));
            }

            return deleteStatement.ToString();
        }
    }
}

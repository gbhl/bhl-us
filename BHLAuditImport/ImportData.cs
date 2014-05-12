using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace MOBOT.BHL.BHLAuditImport
{
    class ImportData
    {
        private string _currentDatabaseName;

        public string currentDatabaseName
        {
            get { return _currentDatabaseName; }
            set { _currentDatabaseName = value; }
        }

        public void mainThread()
        {
            Manifest myManifest = new Manifest();
            myManifest.GetManifest();
            List<ManifestEntry> remoteEntries = Manifest.remoteManifestData();
            List<ManifestEntry> localEntries = Manifest.localManifestData();
            remoteEntries.Sort();
            localEntries.Sort();
            List<ManifestEntry> moreRecentEntries = new List<ManifestEntry>();
            foreach (ManifestEntry tmpEntry in remoteEntries)
            {
                if (tmpEntry.date.CompareTo(localEntries[localEntries.Count - 1].date) == 1)
                {
                    moreRecentEntries.Add(tmpEntry);
                }
            }

            foreach (ManifestEntry tmpEntry in moreRecentEntries)
            {
                try
                {
                    myManifest.GetImportFile(tmpEntry.filename);
                    myManifest.UnzipJSON(tmpEntry.filename);
                    bool success = this.runLoad(String.Concat(ConfigurationManager.AppSettings["importFileLocation"],tmpEntry.filename.Replace(".zip","")));

                    //if successful, continue, if not - exit.
                    if (success)
                    {
                        Manifest.addEntryToLocalManifest(tmpEntry);
                        if (Convert.ToBoolean(ConfigurationManager.AppSettings["deleteJSONPostImport"]))
                        {
                            Manifest.removeJSONFiles(tmpEntry.filename);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Reporter.SendReport("Error in Import Process", String.Concat("There was an error, ", ex.Message, " in the main thread of the import process"));
                    return;
                }
            }
        }

        private bool runLoadIntoDatabase(string importDirectory, string databaseName)
        {
            bool isComplete = false;
            JSON_Load testLoad = new JSON_Load(databaseName);
            string[] auditFiles = Directory.GetFiles(importDirectory);
            string result = testLoad.loadJSON(auditFiles);
            if (String.Equals(result, "true")) //an error occured
            {
                result = testLoad.loadJSON(auditFiles);
                if (String.Equals(result, "true"))
                {
                    //Errors occured both times, send email alert
                    isComplete = false;
                    Reporter.SendReport(String.Concat(_currentDatabaseName, " Import Failed"), String.Concat("The ingestion of the latest import to ", databaseName, " has run twice, and generated Errors both times."));
                }
            }
            else
            {
                isComplete = true;
                Reporter.SendReport(String.Concat(_currentDatabaseName, " Import Successful"), String.Concat("The ingestion of the latest import to ", databaseName, " has been successful"));
            }

            return isComplete;
        }

        private bool runLoad(string importDirectory)
        {
            bool isComplete = false;

            bool isStageComplete = false;
            isStageComplete = runLoadIntoDatabase(importDirectory, ConfigurationManager.AppSettings["stagingDatabase"]);
            if (isStageComplete)
            {
                bool isProductionComplete = this.runLoadIntoDatabase(importDirectory, ConfigurationManager.AppSettings["productionDatabase"]);
                isComplete = isProductionComplete;
            }
            else //stage import failed
            {
                isComplete = false;
            }
            return isComplete;
        }
    }
}

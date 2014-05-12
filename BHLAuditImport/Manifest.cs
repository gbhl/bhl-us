using System;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System.IO;
using Ionic.Zip;

namespace MOBOT.BHL.BHLAuditImport
{
    class Manifest
    {
        public void GetManifest()
        {
            //Download file to location
            WebClient downloader = new WebClient();
         
            string location = ConfigurationManager.AppSettings["remoteManifestLocation"];
            string savelocation = ConfigurationManager.AppSettings["localManifest"];
            if (File.Exists(savelocation))
            {
                File.Delete(savelocation);
            }
            try
            {
                downloader.DownloadFile(location, savelocation);
                bool isDownloaded = File.Exists(savelocation);
            }
            catch (Exception e)
            {
                Reporter.SendReport(@"BHL Import: Error Obtaining Manifest", String.Concat(@"An error, ", e.Message, @" occured while trying to download the manifest file from MOBOT"));
            }
        }

        public void GetImportFile(string filename)
        {
            WebClient downloader = new WebClient();
            string fileLocation = String.Concat(ConfigurationManager.AppSettings["remoteFileLocation"], filename);
            string localFileLocation = String.Concat(ConfigurationManager.AppSettings["importFileLocation"], filename);
            if (File.Exists(localFileLocation))
            {
                File.Delete(localFileLocation);
            }
            try
            {
                downloader.DownloadFile(fileLocation, localFileLocation);
                bool isDownloaded= File.Exists(localFileLocation);
            }
            catch (Exception e)
            {
                Reporter.SendReport(@"BHL Import: Error Getting JSON Files", String.Concat(@"An error, ", e.Message, @" occured while trying to download the JSON delta file from MOBOT"));
            }
        }

        public void UnzipJSON(string filename)
        {
            string localFileLocation = String.Concat(ConfigurationManager.AppSettings["importFileLocation"], filename);
            string unzipDirectory = localFileLocation.Replace(".zip", "");
            try
            {
                using (ZipFile zipController = ZipFile.Read(localFileLocation))
                {
                    foreach (ZipEntry e in zipController)
                    {
                        e.Extract(unzipDirectory, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
            catch (Exception e) {
                Reporter.SendReport(@"BHL Import: Error extracting JSON Files", String.Concat(@"An error, ", e.Message, @" occured while trying to extract the JSON delta file from MOBOT"));
            }
        }

        public static List<ManifestEntry> localManifestData()
        {
            List<ManifestEntry> localEntries = new List<ManifestEntry>();
            string location = ConfigurationManager.AppSettings["localImportedManifest"];

            // Read the file and display it line by line.
            string line;
            System.IO.StreamReader file =      new System.IO.StreamReader(location);
            while ((line = file.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                if (values.Length > 1) {
                ManifestEntry tmpEntry = new ManifestEntry(values[0], values[1], Convert.ToInt64(values[2]) , Convert.ToInt16(values[3]));
                localEntries.Add(tmpEntry);}
            }

            file.Close();

            return localEntries;
        }

        public static List<ManifestEntry> remoteManifestData()
        {
            List<ManifestEntry> remoteEntries = new List<ManifestEntry>();
            string savelocation = ConfigurationManager.AppSettings["localManifest"];
            
            // Read the file and create entry line by line.
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(savelocation);
            while ((line = file.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                if (values.Length > 1)
                {
                    ManifestEntry tmpEntry = new ManifestEntry(values[0], values[1], Convert.ToInt64(values[2]), Convert.ToInt16(values[3]));
                    remoteEntries.Add(tmpEntry);
                }
            }

            file.Close();
            return remoteEntries;
        }

        public static void addEntryToLocalManifest(ManifestEntry newEntry)
        {
            string path = ConfigurationManager.AppSettings["localImportedManifest"];
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(String.Concat(newEntry.date.ToString("yyyy/MM/dd HH:mm:ss"),",",newEntry.filename,",",newEntry.filesize,",",newEntry.numberOfFiles));
            }    
        }

        public static void removeJSONFiles(string filename)
        {
            string localFileLocation = String.Concat(ConfigurationManager.AppSettings["importFileLocation"], filename);
            string unzipDirectory = localFileLocation.Replace(".zip", "");

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["deleteJSONPostImport"])){
                if (File.Exists(localFileLocation))
                {
                    File.Delete(localFileLocation);
                }
                if (Directory.Exists(unzipDirectory))
                {
                    if (unzipDirectory.Equals(@"C:\"))
                    {
                        //don't delete things!
                    }
                    else
                    {
                        Directory.Delete(unzipDirectory, true);
                    }
                }
            }
        }
    }
}

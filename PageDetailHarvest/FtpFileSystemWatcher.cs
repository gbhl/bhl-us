using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;

namespace PageDetailHarvest
{
    public class FtpFileSystemWatcher
    {
        public string FtpUserName { get; set; }
        public string FtpPassword { get; set; }
        public string FtpLocationToWatch { get; set; }
        public string DownloadTo { get; set; }
        public bool KeepOriginal { get; set; }
        public bool OverwriteExisting { get; set; }
        public int RecheckIntervalInSeconds { get; set; }
        private bool DownloadInprogress { get; set; }

        private System.Timers.Timer JobProcessor;

        /// <summary>
        /// Initialize the FptFileSystemWatcher
        /// </summary>
        /// <param name="FtpLocationToWatch">FTP server and path to check for files to download</param>
        /// <param name="DownloadTo">Local folder into which to download files</param>
        /// <param name="RecheckIntervalInSeconds">Number of seconds to wait before polling for files</param>
        /// <param name="UserName">FTP username</param>
        /// <param name="Password">FTP password</param>
        /// <param name="KeepOriginal">True to leave the file on the FTP site, False to delete it</param>
        /// <param name="OverwriteExisting">True to overwrite a local file, False to keep it</param>
        public FtpFileSystemWatcher(string FtpLocationToWatch = "", string DownloadTo = "", 
            int RecheckIntervalInSeconds = 1, string UserName = "", string Password = "", 
            bool KeepOriginal = false, bool OverwriteExisting = false)
        {
            this.FtpUserName = UserName;
            this.FtpPassword = Password;
            this.FtpLocationToWatch = FtpLocationToWatch;
            this.DownloadTo = DownloadTo;
            this.KeepOriginal = KeepOriginal;
            this.RecheckIntervalInSeconds = RecheckIntervalInSeconds;
            this.OverwriteExisting = OverwriteExisting;

            if (this.RecheckIntervalInSeconds < 1) this.RecheckIntervalInSeconds = 1;
        }

        /// <summary>
        /// Check once for files and download any that are found
        /// </summary>
        public void Download()
        {
            DoDownload();
        }

        /// <summary>
        /// Poll for files to download until explicitly stopped
        /// </summary>
        public void StartDownloading()
        {
            JobProcessor = new Timer(this.RecheckIntervalInSeconds * 1000);
            JobProcessor.AutoReset = false;
            JobProcessor.Enabled = false;
            JobProcessor.Elapsed += (sender, e) =>
            {
                try
                {
                    DoDownload();
                    JobProcessor.Enabled = true;
                }
                catch (Exception exp)
                {
                    JobProcessor.Enabled = true;
                    Console.WriteLine(exp.Message);
                }
            };

            JobProcessor.Start();
        }

        /// <summary>
        /// Stop polling for files to download
        /// </summary>
        public void StopDownloading()
        {
            try
            {
                this.JobProcessor.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// Check for files and download any that are found
        /// </summary>
        private void DoDownload()
        {
            string[] FilesList = GetFilesList(this.FtpLocationToWatch, this.FtpUserName, this.FtpPassword);

            if (FilesList == null || FilesList.Length < 1) return;

            foreach (string FileName in FilesList)
            {
                if (!string.IsNullOrWhiteSpace(FileName))
                {
                    DownloadFile(this.FtpLocationToWatch, this.DownloadTo, FileName.Trim(), this.FtpUserName, this.FtpPassword, this.OverwriteExisting);
                    if (!this.KeepOriginal) DeleteFile(Path.Combine(this.FtpLocationToWatch, FileName.Trim()), this.FtpUserName, this.FtpPassword);
                }
            }
        }

        /// <summary>
        /// Delete the specified file
        /// </summary>
        /// <param name="FtpFilePath"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        private void DeleteFile(string FtpFilePath, string UserName, string Password)
        {
            FtpWebRequest FtpRequest;
            FtpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(FtpFilePath));
            FtpRequest.UseBinary = true;
            FtpRequest.Method = WebRequestMethods.Ftp.DeleteFile;

            FtpRequest.Credentials = new NetworkCredential(UserName, Password);
            FtpWebResponse response = (FtpWebResponse)FtpRequest.GetResponse();
            response.Close();
        }

        /// <summary>
        /// Download the specified file to the specified location
        /// </summary>
        /// <param name="FtpLocation"></param>
        /// <param name="FileSystemLocation"></param>
        /// <param name="FileName"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="OverwriteExisting"></param>
        private void DownloadFile(string FtpLocation, string FileSystemLocation, string FileName, string UserName, string Password, bool OverwriteExisting)
        {
            try
            {
                const int BufferSize = 2048;
                byte[] Buffer = new byte[BufferSize];

                if (File.Exists(Path.Combine(FileSystemLocation, FileName)))
                {
                    if (OverwriteExisting)
                    {
                        File.Delete(Path.Combine(FileSystemLocation, FileName));
                    }
                    else
                    {
                        return;
                    }
                }

                FtpWebRequest Request = (FtpWebRequest)FtpWebRequest.Create(new Uri(Path.Combine(FtpLocation, FileName)));
                Request.Credentials = new NetworkCredential(UserName, Password);
                Request.Proxy = null;
                Request.Method = WebRequestMethods.Ftp.DownloadFile;
                Request.UseBinary = true;

                FtpWebResponse Response = (FtpWebResponse)Request.GetResponse();

                using (Stream s = Response.GetResponseStream())
                {
                    using (FileStream fs = new FileStream(Path.Combine(FileSystemLocation, FileName), FileMode.CreateNew, FileAccess.ReadWrite))
                    {
                        while (s.Read(Buffer, 0, BufferSize) != -1)
                        {
                            fs.Write(Buffer, 0, BufferSize);
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        ///  Get the list of files found in the specified directory
        /// </summary>
        /// <param name="FtpFolderPath"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        private string[] GetFilesList(string FtpFolderPath, string UserName, string Password)
        {
            try
            {
                FtpWebRequest Request;
                FtpWebResponse Response;

                Request = (FtpWebRequest)FtpWebRequest.Create(new Uri(FtpFolderPath));
                Request.Credentials = new NetworkCredential(UserName, Password);
                Request.Proxy = null;
                Request.Method = WebRequestMethods.Ftp.ListDirectory;
                Request.UseBinary = true;

                Response = (FtpWebResponse)Request.GetResponse();
                StreamReader reader = new StreamReader(Response.GetResponseStream());
                string Data = reader.ReadToEnd();

                return Data.Split('\n');
            }
            catch
            {
                return null;
            }
        }
    }
}

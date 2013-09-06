using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


namespace MOBOT.FileAccess
{
    public class FileAccessProvider : MarshalByRefObject, IFileAccessProvider
    {
        public string Echo(string toEcho)
        {
            return toEcho;
        }
        public byte[] GetSerializedImage(string path)
        {
            Bitmap bitmap = null;
            MemoryStream ms = null;
            try
            {
                bitmap = new Bitmap(path);
                ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
            finally
            {
                if (bitmap != null)
                    bitmap.Dispose();

                if (ms != null)
                    ms.Dispose();
            }
        }
        public void SaveImage(byte[] buffer, string path)
        {
            MemoryStream ms = null;
            Bitmap bitmap = null;
            try
            {
                string filePathOnly = path.Substring(0, path.LastIndexOf("\\"));
                if (!Directory.Exists(filePathOnly))
                    Directory.CreateDirectory(filePathOnly);

                ms = new MemoryStream(buffer);
                bitmap = new Bitmap(ms);
                bitmap.Save(path, ImageFormat.Jpeg);
            }
            finally
            {
                if (bitmap != null)
                    bitmap.Dispose();

                if (ms != null)
                    ms.Dispose();
            }
        }

        public void SaveFile(byte[] buffer, string path)
        {
            using (FileStream fileStream = File.Open(path, System.IO.FileMode.Create))
            {
                using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fileStream))
                {
                    writer.Write(buffer);
                    writer.Close();
                }
            }
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public long GetFileSizeInKB(string path)
        {
            if (FileExists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                return (fileInfo.Length / 1024);
            }
            else
                return 0;
        }

        public long GetFileSizeInMB(string path)
        {
            if (FileExists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                return (fileInfo.Length / 1048576);
            }
            else
                return 0;
        }

        public string GetFileText(string path)
        {
            if (FileExists(path))
            {
                TextReader tr = null;
                try
                {
                    tr = File.OpenText(path);
                    StringBuilder sb = new StringBuilder();
                    while (tr.Peek() > -1)
                        sb.Append(tr.ReadLine() + "\n");
                    return sb.ToString();
                }
                finally
                {
                    if (tr != null)
                        tr.Close();
                }

            }
            return "";
        }

        public void DeleteFile(string path)
        {
            if (FileExists(path))
                File.Delete(path);
        }

        public string[] GetSubDirectories(string path)
        {
            if (Directory.Exists(path))
                return Directory.GetDirectories(path);
            else
                return new string[0];
        }

        public string[] GetFiles(string path)
        {
            if (Directory.Exists(path))
                return Directory.GetFiles(path);
            else
                return new string[0];
        }

        public void MoveFile(string sourceFileName, string destinationFileName)
        {
            if (FileExists(sourceFileName))
            {
                if (!Directory.Exists(destinationFileName.Substring(0, destinationFileName.LastIndexOf("\\"))))
                {
                    Directory.CreateDirectory(destinationFileName.Substring(0, destinationFileName.LastIndexOf("\\")));
                }
                //if a file of the same name already exists, delete it first before moving the new file over
                DeleteFile(destinationFileName);
                File.Move(sourceFileName, destinationFileName);
            }
        }

        public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
        {
            File.Copy(sourceFileName, destinationFileName, overwrite);
        }

        public void CreateDirectory(string directoryName)
        {
            if (!Directory.Exists(directoryName.Substring(0, directoryName.LastIndexOf("\\"))))
            {
                Directory.CreateDirectory(directoryName.Substring(0, directoryName.LastIndexOf("\\")));
            }
        }
    }
}

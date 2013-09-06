using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace MOBOT.FileAccess
{
    public interface IFileAccessProvider
    {
        string Echo(string toEcho);
        byte[] GetSerializedImage(string path);
        void SaveImage(byte[] buffer, string path);
        void SaveFile(byte[] buffer, string path);
        bool FileExists(string path);
        long GetFileSizeInKB(string path);
        long GetFileSizeInMB(string path);
        string GetFileText(string path);
        void DeleteFile(string path);
        string[] GetSubDirectories(string path);
        string[] GetFiles(string path);
        void MoveFile(string sourceFileName, string destinationFileName);
        void CopyFile(string sourceFileName, string destinationFileName, bool overwrite);
        void CreateDirectory(string directoryName);
    }
}

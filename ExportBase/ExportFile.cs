using System;
using System.IO;

namespace BHL.Export
{
    public class ExportFile
    {
        // Create a default logger for use in this class
        private ExportLogger _log = new ExportLogger();

        public ExportFile(ExportLogger log = null)
        {
            if (log != null) _log = log;
        }

        /// <summary>
        /// Zip the specified file
        /// </summary>
        /// <param name="filename">Name of file to be compressed</param>
        /// <param name="compressedFilename">Name to assign to the compressed file</param>
        public void Compress(string filename, string compressedFilename)
        {
            _log.Info(string.Format("Generating a compressed version of {0}", filename));
            string errorMessage = string.Empty;

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
                _log.Info(string.Format("Original size: {0}, Compressed size: {1}", originalSize.ToString(), compressedSize.ToString()));

            } // end try
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error: Unable to compress file: {0}", ex.Message));
            }
        }


    }
}

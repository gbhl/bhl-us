using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MOBOT.BHL.AdminWeb.MVCServices
{
    public class TextImportService
    {
        private Dictionary<string, string> FileFormats = new Dictionary<string, string> {
            { "", "Unknown" },
            {"dv", "DigiVol" },
            {"ftp", "From The Page" },
            {"stc", "Smithsonian Transcription Center" }
        };

        private static Dictionary<int, string> TextImportBatchStatuses = new Dictionary<int, string>
        {
            { 10, "New" },
            { 20, "Queued" },
            { 30, "Processing" },
            { 40, "Imported" },
            { 50, "Rejected" }
        };

        private static Dictionary<int, string> TextImportBatchFileStatuses = new Dictionary<int, string>
        {
            { 10, "Ready" },
            { 20, "Review" },
            { 30, "Rejected" },
            { 40, "Error" }
        };

        public class ImportFileException : Exception
        {
            public ImportFileException() : base() { }
            public ImportFileException(string message) : base(message) { }
            public ImportFileException(string message, System.Exception inner) : base(message, inner) { }
        }

        public static int GetTextImportBatchStatusNew()
        {
            return TextImportBatchStatuses.Where(s => s.Value == "New").Select(k => k.Key).First();
        }

        public static int GetTextImportBatchFileStatusReady()
        {
            return TextImportBatchFileStatuses.Where(s => s.Value == "Ready").Select(k => k.Key).First();
        }

        public static int GetTextImportBatchFileStatusReview()
        {
            return TextImportBatchFileStatuses.Where(s => s.Value == "Review").Select(k => k.Key).First();
        }

        public static int GetTextImportBatchFileStatusError()
        {
            return TextImportBatchFileStatuses.Where(s => s.Value == "Error").Select(k => k.Key).First();
        }

        public string GetFileFormat(string savedFileName)
        {
            string fileFormat = "";
            string fileContent = File.ReadAllText(savedFileName);

            if (fileContent.Contains("<div class=\"page-content\">"))
            {
                fileFormat = "ftp";
            }
            else if (fileContent.Contains(",\"occurrenceRemarks\","))
            {
                fileFormat = "dv";
            }
            else if (fileContent.Contains("tl1_text"))
            {
                fileFormat = "stc";
            }

            return fileFormat;
        }

        public string GetFileFormatValue(string fileFormatKey)
        {
            return FileFormats[fileFormatKey];
        }
    }
}
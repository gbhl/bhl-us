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


        public class ImportFileException : Exception
        {
            public ImportFileException() : base() { }
            public ImportFileException(string message) : base(message) { }
            public ImportFileException(string message, System.Exception inner) : base(message, inner) { }
        }

        #region DropDownList data

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

        #endregion DropDownList data

    }
}
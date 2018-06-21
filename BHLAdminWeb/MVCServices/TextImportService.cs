using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOBOT.BHL.AdminWeb.MVCServices
{
    public class TextImportService
    {
        private Dictionary<string, string> FileFormats = new Dictionary<string, string> {
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

        /// <summary>
        /// Provide the list of data sources to be used during the segment import process
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> FileFormatList()
        {
            return FileFormats;
        }

        public string GetFileFormatValue(string fileFormatKey)
        {
            return FileFormats[fileFormatKey];
        }

        #endregion DropDownList data

    }
}
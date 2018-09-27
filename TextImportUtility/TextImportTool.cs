using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BHL.TextImportUtility
{
    public class TextImportTool
    {
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

        public string GetText(string fileName, string seqNo, string fileFormat = "")
        {
            int sequence;
            string fileText = string.Empty;
            Dictionary<int, string> fileContents = new Dictionary<int, string>();

            if (!Int32.TryParse(seqNo, out sequence)) throw new Exception(string.Format("Invalid sequence number: {0}", seqNo));

            if (fileFormat == "") fileFormat = GetFileFormat(fileName);

            switch (fileFormat)
            {
                case "ftp":
                    fileContents = ParseFTP(fileName);
                    break;
                case "dv":
                    fileContents = ParseDV(fileName);
                    break;
                case "stc":
                    fileContents = ParseSTC(fileName);
                    break;
            }

            if (sequence > fileContents.Count)
                fileText = string.Format("Page {0} not found in file.", seqNo);
            else
                fileText = fileContents[sequence];

            return fileText;
        }

        /// <summary>
        /// Parse the transcription contents from a From The Page output file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Dictionary<int, string> ParseFTP(string fileName)
        {
            Dictionary<int, string> contents = new Dictionary<int, string>();

            string fileContents = File.ReadAllText(fileName);

            int sequenceNumber = 1;
            while (fileContents.IndexOf("<div class=\"page-content\">") >= 0)
            {
                int pageStart = fileContents.IndexOf("<div class=\"page-content\">") + "<div class=\"page-content\">".Length;
                int pageEnd = fileContents.IndexOf("</div>", pageStart);

                string pageText = fileContents.Substring(pageStart, pageEnd - pageStart);
                contents.Add(sequenceNumber++, pageText.Replace("<br>", "\n").Replace("<p>", "").Replace("</p>", "\r\n"));
                fileContents = fileContents.Substring(pageEnd + "</div>".Length);
            }

            return contents;
        }

        /// <summary>
        /// Parse the transcription contents from a DigiVol output file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Dictionary<int, string> ParseDV(string fileName)
        {
            Dictionary<int, string> contents = new Dictionary<int, string>();

            using (StreamReader reader = File.OpenText(fileName))
            {
                CsvReader csv = new CsvReader(reader);
                csv.Configuration.HasHeaderRecord = true;

                var dvRecord = new
                {
                    taskID = default(int),
                    taskURL = string.Empty,
                    validationStatus = string.Empty,
                    transcriberID = string.Empty,
                    validatorID = string.Empty,
                    externalIdentifier = string.Empty,
                    exportComment = string.Empty,
                    dateTranscribed = new DateTime(),
                    dateValidated = new DateTime(),
                    individualCount = default(int),
                    institutionCode = string.Empty,
                    occurrenceRemarks = string.Empty,
                    sequenceNumber = default(int),  // 1-based index
                    transcriberNotes = string.Empty,
                    validatorNotes = string.Empty
                };
                var records = csv.GetRecords(dvRecord);

                foreach (var record in records)
                {
                    contents.Add(record.sequenceNumber, record.occurrenceRemarks.Replace("\\n", "\n"));
                }
            }

            return contents;
        }

        /// <summary>
        /// Parse the transcription contents from a Smithsonian Transcription Center output file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Dictionary<int, string> ParseSTC(string fileName)
        {
            Dictionary<int, string> contents = new Dictionary<int, string>();

            using (StreamReader reader = File.OpenText(fileName))
            {
                CsvReader csv = new CsvReader(reader);
                csv.Configuration.HasHeaderRecord = true;

                var dvRecord = new
                {
                    asset_uan = string.Empty,
                    edan_id = string.Empty,
                    tl1_text = string.Empty
                };
                var records = csv.GetRecords(dvRecord);

                int sequenceNumber = 1;
                foreach (var record in records)
                {
                    contents.Add(sequenceNumber++, record.tl1_text);
                }
            }

            return contents;
        }

    }
}

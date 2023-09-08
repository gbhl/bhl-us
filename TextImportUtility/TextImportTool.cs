using CsvHelper;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BHL.TextImportUtility
{
    public class TextImportTool
    {
        public string GetFileFormat(string savedFileName)
        {
            string fileFormat;
            string fileContent = File.ReadAllText(savedFileName);

            if (fileContent.Contains("<div class=\"page-content\">"))
            {
                fileFormat = "ftp";
            }
            else if (fileContent.Contains("occurrenceRemarks"))
            {
                fileFormat = "dv";
            }
            else if (fileContent.Contains("tl1_text"))
            {
                fileFormat = "stc";
            }
            else if (fileContent.ToLower().Contains("pageid") && 
                    fileContent.ToLower().Contains("sequencenumber") && 
                    fileContent.ToLower().Contains("text"))
            {
                fileFormat = "bhlcsv";
            }
            else
            {
                fileFormat = string.Empty;  // unknown
            }

            return fileFormat;
        }

        public int PageCount(string fileName, string fileFormat = "")
        {
            Dictionary<int, string> fileContents = new Dictionary<int, string>();

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
                case "bhlcsv":
                    fileContents = ParseBHLCSV(fileName);
                    break;
            }

            return fileContents.Count;
        }

        public string GetText(string fileName, string seqNo, string fileFormat = "")
        {
            string fileText;
            Dictionary<int, string> fileContents = new Dictionary<int, string>();

            if (!Int32.TryParse(seqNo, out int sequence)) throw new Exception(string.Format("Invalid sequence number: {0}", seqNo));

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
                case "bhlcsv":
                    fileContents = ParseBHLCSV(fileName);
                    break;
            }

            if (!fileContents.ContainsKey(sequence))
                fileText = string.Format("Page {0} not found in file.", seqNo);
            else
                fileText = fileContents[sequence];

            return fileText;
        }

        public bool TextAvailable(string fileName, string seqNo, string fileFormat = "")
        {
            Dictionary<int, string> fileContents = new Dictionary<int, string>();

            if (!Int32.TryParse(seqNo, out int sequence)) throw new Exception(string.Format("Invalid sequence number: {0}", seqNo));

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
                case "bhlcsv":
                    fileContents = ParseBHLCSV(fileName);
                    break;
            }

            return fileContents.ContainsKey(sequence);
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
                contents.Add(sequenceNumber++, pageText.Replace("<br>", "").Replace("<br/>", "").Replace("<p>", "").Replace("</p>", "").Replace("&amp;", "&"));
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
                var config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = (_1) => { }
                };
                CsvReader csv = new CsvReader(reader, config);

                var dvRecord = new
                {
                    taskID = string.Empty,
                    taskURL = string.Empty,
                    validationStatus = string.Empty,
                    transcriberID = string.Empty,
                    validatorID = string.Empty,
                    externalIdentifier = string.Empty,
                    exportComment = string.Empty,
                    dateTranscribed = string.Empty,
                    dateValidated = string.Empty,
                    individualCount = string.Empty,
                    //institutionCode = string.Empty,
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
                var config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                };
                CsvReader csv = new CsvReader(reader, config);

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

        /// <summary>
        /// Parse the transcription contents from a file that uses the generic BHL CSV format
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Dictionary<int, string> ParseBHLCSV(string fileName)
        {
            Dictionary<int, string> contents = new Dictionary<int, string>();

            using (StreamReader reader = File.OpenText(fileName))
            {
                var config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                };
                CsvReader csv = new CsvReader(reader, config);

                var dvRecord = new
                {
                    PageID = string.Empty,
                    SequenceNumber = string.Empty,
                    Text = string.Empty
                };
                var records = csv.GetRecords(dvRecord);

                // Get the pages for the item from the database
                string itemType = Path.GetFileNameWithoutExtension(fileName).Substring(15, 1);  // Get the item type indicator from the filename
                string entityID = Path.GetFileNameWithoutExtension(fileName).Substring(17);   // ignore the info added to the filename
                int itemID;
                if (itemType.ToUpper() == "S")
                {
                    Segment segment = new BHLProvider().SegmentSelectAuto(Convert.ToInt32(entityID));
                    itemID = segment.ItemID;
                }
                else // itemType == "I")
                {
                    Book book = new BHLProvider().BookSelectAuto(Convert.ToInt32(entityID));
                    itemID = book.ItemID;
                }
                List<Page> pages = new BHLProvider().PageSelectByItemID(itemID);

                foreach (var record in records)
                {
                    int? sequenceOrder = null;
                    if (!string.IsNullOrWhiteSpace(record.PageID))
                    {
                        // Get the sequence number for the page from the database records
                        var bhlPages = (
                            from page in pages
                            where page.PageID == Convert.ToInt32(record.PageID)
                            select new
                            {
                                page.SequenceOrder
                            });
                        if (bhlPages.Count() > 0) sequenceOrder = (int)bhlPages.First().SequenceOrder;
                    }
                    else
                    {
                        // Get the sequence order from the file
                        sequenceOrder = Convert.ToInt32(record.SequenceNumber);
                    }

                    if (sequenceOrder != null) contents.Add((int)sequenceOrder, record.Text);
                }
            }

            return contents;
        }

        /// <summary>
        /// Add sequence numbers to the specified file.
        /// </summary>
        /// <remarks>Only applies to BHL CSV files.</remarks>
        /// <param name="fileName"></param>
        public void AddSequenceNumbers(string fileName)
        {
            // Make sure this is a BHL CSV file.  If not, do nothing.
            if (this.GetFileFormat(fileName) == "bhlcsv")
            {
                // Parse the records in the file, and add Sequence Numbers
                var writeRecords = new List<object>();

                using (StreamReader reader = File.OpenText(fileName))
                {
                    var config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true
                    };
                    CsvReader csv = new CsvReader(reader, config);

                    var dvRecord = new
                    {
                        PageID = string.Empty,
                        SequenceNumber = string.Empty,
                        Text = string.Empty
                    };
                    var readRecords = csv.GetRecords(dvRecord);

                    // Get the pages for the item from the database
                    string itemType = Path.GetFileNameWithoutExtension(fileName).Substring(15, 1);  // Get the item type indicator from the filename
                    string entityID = Path.GetFileNameWithoutExtension(fileName).Substring(17);   // ignore the info added to the filename
                    int itemID;
                    if (itemType.ToUpper() == "S")
                    {
                        Segment segment = new BHLProvider().SegmentSelectAuto(Convert.ToInt32(entityID));
                        itemID = segment.ItemID;
                    }
                    else // itemType == "I")
                    {
                        Book book = new BHLProvider().BookSelectAuto(Convert.ToInt32(entityID));
                        itemID = book.ItemID;
                    }
                    List<Page> pages = new BHLProvider().PageSelectByItemID(itemID);

                    foreach (var record in readRecords)
                    {
                        string sequenceOrder = null;
                        if (string.IsNullOrWhiteSpace(record.PageID))
                        {
                            // Use the record as-is
                            writeRecords.Add(record);
                        }
                        else
                        {
                            // Get the sequence number for the page from the database records
                            var bhlPages = (
                                from page in pages
                                where page.PageID == Convert.ToInt32(record.PageID) && page.ItemID == itemID
                                select new
                                {
                                    page.SequenceOrder
                                });
                            if (bhlPages.Count() == 0) throw new Exception(string.Format(
                                    "Page {0} not found in Item {1}.  Make sure all Page IDs are valid for the Item.", 
                                    record.PageID, 
                                    entityID
                                ));
                            sequenceOrder = bhlPages.First().SequenceOrder.ToString();

                            // Add the sequence to the record in the file
                            writeRecords.Add(new { record.PageID, SequenceNumber = sequenceOrder, record.Text });
                        }
                    }
                }

                // Write the updated records to the file
                using (var writer = new StreamWriter(fileName))
                using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(writeRecords);
                }

            }
        }
    }
}

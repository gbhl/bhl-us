using CsvHelper;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BHL.TextImportUtility
{
    public class TextImportTool
    {
        string _fileName = string.Empty;
        string _fileContent = string.Empty;

        public TextImportTool(string fileName)
        {
            _fileName = fileName;
            _fileContent = File.ReadAllText(fileName);
        }

        public string GetFileFormat()
        {
            string fileFormat;

            if (_fileContent.Contains("<div class=\"page-content\">"))
            {
                fileFormat = "ftp";
            }
            else if (_fileContent.Contains("occurrenceRemarks"))
            {
                fileFormat = "dv";
            }
            else if (_fileContent.Contains("tl1_text"))
            {
                fileFormat = "stc";
            }
            else if (_fileContent.ToLower().Contains("pageid") &&
                    _fileContent.ToLower().Contains("sequencenumber") &&
                    _fileContent.ToLower().Contains("text"))
            {
                fileFormat = "bhlcsv";
            }
            else
            {
                fileFormat = string.Empty;  // unknown
            }

            return fileFormat;
        }

        public int PageCount(string fileFormat = "")
        {
            Dictionary<int, string> fileContents = new Dictionary<int, string>();

            if (fileFormat == "") fileFormat = GetFileFormat();
            switch (fileFormat)
            {
                case "ftp":
                    fileContents = ParseFTP(false, false);
                    break;
                case "dv":
                    fileContents = ParseDV(false);
                    break;
                case "stc":
                    fileContents = ParseSTC(false);
                    break;
                case "bhlcsv":
                    fileContents = ParseBHLCSV(false);
                    break;
            }

            return fileContents.Count;
        }

        public string GetText(string seqNo, string fileFormat = "")
        {
            string fileText;
            Dictionary<int, string> fileContents = new Dictionary<int, string>();

            if (!Int32.TryParse(seqNo, out int sequence)) throw new Exception(string.Format("Invalid sequence number: {0}", seqNo));

            if (fileFormat == "") fileFormat = GetFileFormat();

            switch (fileFormat)
            {
                case "ftp":
                    fileContents = ParseFTP(true, true);
                    break;
                case "dv":
                    fileContents = ParseDV(true);
                    break;
                case "stc":
                    fileContents = ParseSTC(true);
                    break;
                case "bhlcsv":
                    fileContents = ParseBHLCSV(true);
                    break;
            }

            if (!fileContents.ContainsKey(sequence))
                fileText = string.Format("Page {0} not found in file.", seqNo);
            else
                fileText = fileContents[sequence];

            return fileText;
        }

        public bool TextAvailable(string seqNo, string fileFormat = "")
        {
            Dictionary<int, string> fileContents = new Dictionary<int, string>();

            if (!Int32.TryParse(seqNo, out int sequence)) throw new Exception(string.Format("Invalid sequence number: {0}", seqNo));

            if (fileFormat == "") fileFormat = GetFileFormat();

            switch (fileFormat)
            {
                case "ftp":
                    fileContents = ParseFTP(false, false);
                    break;
                case "dv":
                    fileContents = ParseDV(false);
                    break;
                case "stc":
                    fileContents = ParseSTC(false);
                    break;
                case "bhlcsv":
                    fileContents = ParseBHLCSV(false);
                    break;
            }

            return fileContents.ContainsKey(sequence);
        }

        /// <summary>
        /// Parse the transcription contents from a From The Page output file
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, string> ParseFTP(bool includeContent, bool clean)
        {
            Dictionary<int, string> contents = new Dictionary<int, string>();

            string fileContents = _fileContent;

            int sequenceNumber = 1;
            string pattern = @"<div class=""page-content"">([\s\S]*?)</div>";
            MatchCollection matches = Regex.Matches(fileContents, pattern);
            foreach (Match match in matches)
            {
                string pageText = string.Empty;
                if (includeContent)
                {
                    pageText = match.Groups[1].Value;
                    pageText = pageText.Replace("<br>", "").Replace("<br/>", "").Replace("<p>", "").Replace("</p>", "").Replace("&amp;", "&");
                    if (clean) pageText = NormalizeMarkup(pageText);
                }

                contents.Add(sequenceNumber++, pageText);
            }

            return contents;

            /*
            int sequenceNumber = 1;
            string pageStartTag = "<div class=\"page-content\">";
            string pageEndTag = "</div>";
            int pageStartTagLength = pageStartTag.Length;
            int pageEndTagLength = pageEndTag.Length;
            while (fileContents.IndexOf(pageStartTag) >= 0)
            {
                int pageStart = fileContents.IndexOf(pageStartTag) + pageStartTagLength;
                int pageEnd = fileContents.IndexOf(pageEndTag, pageStart);

                string pageText = string.Empty;
                if (includeContent)
                {
                    pageText = fileContents.Substring(pageStart, pageEnd - pageStart);
                    pageText = pageText.Replace("<br>", "").Replace("<br/>", "").Replace("<p>", "").Replace("</p>", "").Replace("&amp;", "&");
                    if (clean) pageText = NormalizeMarkup(pageText);
                }

                contents.Add(sequenceNumber++, pageText);
                fileContents = fileContents.Substring(pageEnd + pageEndTagLength);
            }

            return contents;
            */
        }

        /// <summary>
        /// Parse the transcription contents from a DigiVol output file
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, string> ParseDV(bool clean)
        {
            Dictionary<int, string> contents = new Dictionary<int, string>();

            byte[] contentArray = System.Text.Encoding.UTF8.GetBytes(_fileContent);
            MemoryStream contentStream = new MemoryStream(contentArray);

            using (StreamReader reader = new StreamReader(contentStream))
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
                    string pageText = record.occurrenceRemarks.Replace("\\n", "\n");
                    if (clean) pageText = NormalizeMarkup(pageText);
                    contents.Add(record.sequenceNumber, pageText);
                }
            }

            return contents;
        }

        /// <summary>
        /// Parse the transcription contents from a Smithsonian Transcription Center output file
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, string> ParseSTC(bool clean)
        {
            Dictionary<int, string> contents = new Dictionary<int, string>();

            byte[] contentArray = System.Text.Encoding.UTF8.GetBytes(_fileContent);
            MemoryStream contentStream = new MemoryStream(contentArray);

            using (StreamReader reader = new StreamReader(contentStream))
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
                    string pageText = record.tl1_text;
                    if (clean) pageText = NormalizeMarkup(pageText);
                    contents.Add(sequenceNumber++, pageText);
                }
            }

            return contents;
        }

        /// <summary>
        /// Parse the transcription contents from a file that uses the generic BHL CSV format
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, string> ParseBHLCSV(bool clean)
        {
            Dictionary<int, string> contents = new Dictionary<int, string>();

            byte[] contentArray = System.Text.Encoding.UTF8.GetBytes(_fileContent);
            MemoryStream contentStream = new MemoryStream(contentArray);

            using (StreamReader reader = new StreamReader(contentStream))
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
                string itemType = Path.GetFileNameWithoutExtension(_fileName).Substring(15, 1);  // Get the item type indicator from the filename
                string entityID = Path.GetFileNameWithoutExtension(_fileName).Substring(17);   // ignore the info added to the filename
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

                    if (sequenceOrder != null)
                    {
                        string pageText = record.Text;
                        if (clean) pageText = NormalizeMarkup(pageText);
                        contents.Add((int)sequenceOrder, pageText);
                    }
                }
            }

            return contents;
        }

        /// <summary>
        /// Replace supported HTML markup in the specified string with BHL-approved markup, and remove any unsupported markup
        /// </summary>
        /// <param name="fileContents"></param>
        /// <returns></returns>
        private string NormalizeMarkup(string fileContents)
        {
            /*
            Additions 
                Replace 
                    <add>some text</add>
                    <span class="addition">some text</span>
                with 
                    [some text]
            */
            fileContents = fileContents.Replace("<add>", "[").Replace("</add>", "]");
            fileContents = ReplaceMarkup(fileContents, "<\\s*span[^>]*class=[\"']addition[\"'][^>]*>([\\S\\s]*?)<\\s*\\/\\s*span>", @"[$1]");

            /*
            Footnotes
                Replace 
                    <footnote>some text</footnote>
                    <span class="footnote-body">some text</span>
                with 
                    [[footnote]]some text[[/footnote]]
            */
            fileContents = fileContents.Replace("<footnote>", "[[footnote]]").Replace("</footnote>", "[[/footnote]]");
            fileContents = ReplaceMarkup(fileContents, "<\\s*span[^>]*class=[\"']footnote-body[\"'][^>]*>([\\S\\s]*?)<\\s*\\/\\s*span>", @"[[footnote]]$1[[/footnote]]");

            /*
            Illegible/Unclear text
                Replace 
                    <unclear>some text</unclear>
                    <span class="unclear">some text</span>
                with 
                    [[unclear]]some text[[/unclear]]
            */
            fileContents = fileContents.Replace("<unclear>", "[[unclear]]").Replace("</unclear>", "[[/unclear]]");
            fileContents = ReplaceMarkup(fileContents, "<\\s*span[^>]*class=[\"']unclear[\"'][^>]*>([\\S\\s]*?)<\\s*\\/\\s*span>", @"[[unclear]]$1[[/unclear]]");

            /*
            Images
                Replace 
                    <figure>some text</figure>
                    <span>some text{Figure}</span>
                    <img alt="some text"/>                
                    <img/>
                with 
                    [[illustration]]some text[[/illustration]]
                    [[illustration]][[/illustration]]
            */
            fileContents = fileContents.Replace("<figure>", "[[illustration]]").Replace("</figure>", "[[/illustration]]");
            fileContents = ReplaceMarkup(fileContents, "<\\s*span[\\S\\s]*?>([\\S\\s]*?){[Ff]igure}<\\s*\\/\\s*span>", "[[illustration]]$1[[/illustration]]");

            Regex regex = new Regex("<\\s*img[^>]*[\\/]?[^>]*>", RegexOptions.Multiline);
            MatchCollection imgMatches = regex.Matches(fileContents);
            foreach (Match imgMatch in imgMatches)
            {
                string altText = string.Empty;
                string img = imgMatch.ToString();
                regex = new Regex("(?:alt=)[\"'](.*?)[\"']", RegexOptions.Multiline);
                Match altTextMatch = regex.Match(img);
                if (altTextMatch != null) altText = altTextMatch.Groups[1].Value;
                string replacement = string.Format("[[illustration]]{0}[[/illustration]]", altText);
                fileContents = fileContents.Replace(img, replacement);
            }

            /*
            Marginalia
                Replace 
                    <margin>some text</margin>
                    <span class="marginalia">some text</span>
                with 
                    [[margin]]some text[[/margin]]
            */
            fileContents = fileContents.Replace("<margin>", "[[margin]]").Replace("</margin>", "[[/margin]]");
            fileContents = ReplaceMarkup(fileContents, "<\\s*span[^>]*class=[\"']marginalia[\"'][^>]*>([\\S\\s]*?)<\\s*\\/\\s*span>", @"[[margin]]$1[[/margin]]");

            /*
            Annotation
                Replace 
                    <annotation>some text</annotation>
                    <span class="annotation">some text</span>
                with 
                    [[annotation]]some text[[/annotation]]
            */
            fileContents = fileContents.Replace("<annotation>", "[[annotation]]").Replace("</annotation>", "[[/annotation]]");
            fileContents = ReplaceMarkup(fileContents, "<\\s*span[^>]*class=[\"']annotation[\"'][^>]*>([\\S\\s]*?)<\\s*\\/\\s*span>", @"[[annotation]]$1[[/annotation]]");

            /*
            Missing text
                Replace 
                    <gap>some text</gap>
                    <span class="gap">some text</span>
                with 
                    [[loss]]some text[[/loss]]
            */
            fileContents = fileContents.Replace("<gap>", "[[loss]]").Replace("</gap>", "[[/loss]]");
            fileContents = ReplaceMarkup(fileContents, "<\\s*span[^>]*class=[\"']gap[\"'][^>]*>([\\S\\s]*?)<\\s*\\/\\s*span>", @"[[loss]]$1[[/loss]]");

            /*
            Strikethrough
                Replace 
                    <strike>some text</strike>
                    <s>some text</s>
                with 
                    [[strike]]some text[[/strike]]
            */
            fileContents = fileContents.Replace("<strike>", "[[strike]]").Replace("</strike>", "[[/strike]]");
            fileContents = fileContents.Replace("<s>", "[[strike]]").Replace("</s>", "[[/strike]]");

            /*
            Underline
                Replace 
                    <u>some text</u>
                with 
                    [[underline]]some text[[/underline]]
            */
            fileContents = fileContents.Replace("<u>", "[[underline]]").Replace("</u>", "[[/underline]]");

            /*
            Tables
                Replace
                    <table>
                        <thead>
                            <tr><th>Head 1</th><th>Head 2</th><th>Head 3</th></tr>
                        </thead>
                        <tbody>
                            <tr><td>Row 1 Cell 1</td><td>Row 1 Cell 2</td><td>Row 1 Cell 3</td></tr>
                            <tr><td>Row 2 Cell 1</td><td>Row 2 Cell 2</td><td>Row 2 Cell 3</td></tr>
                        </tbody>
                    </table>
                with
                    Head 1 | Head 2 | Head 3
                    ----------------------------------------
                    Row 1 Cell 1 | Row 1 Cell 2 | Row 1 Cell 3
                    Row 2 Cell 1 | Row 2 Cell 2 | Row 2 Cell 3
            */
            fileContents = fileContents.Replace("<th/>", "<th></th>");
            fileContents = fileContents.Replace("<td/>", "<td></td>");
            fileContents = ReplaceMarkup(fileContents, "<\\/t[d|h]>\\S*\\s*<t[d|h][^>]*>", @" | ");  // Convert cell divisions to |
            fileContents = fileContents.Replace("</tr>", "\r\n");  // Convert table row endings to CRLF
            fileContents = fileContents.Replace("</thead>", "\r\n----------------------------------------\r\n");

            // Remove all remaining HTML elements
            fileContents = Regex.Replace(fileContents, "<.*?>", string.Empty);

            // Return the cleaned file contents
            return fileContents;
        }

        /// <summary>
        /// Perform the specified Regex replacement
        /// </summary>
        /// <param name="fileContents"></param>
        /// <param name="pattern"></param>
        /// <param name="substitution"></param>
        /// <returns></returns>
        private static string ReplaceMarkup(string fileContents, string pattern, string substitution)
        {
            RegexOptions options = RegexOptions.Multiline;
            Regex regex = new Regex(pattern, options);
            return regex.Replace(fileContents, substitution);
        }

        /// <summary>
        /// Add sequence numbers to the specified file.
        /// </summary>
        /// <remarks>Only applies to BHL CSV files.</remarks>
        /// <param name="fileName"></param>
        public void AddSequenceNumbers()
        {
            // Make sure this is a BHL CSV file.  If not, do nothing.
            if (this.GetFileFormat() == "bhlcsv")
            {
                // Parse the records in the file, and add Sequence Numbers
                var writeRecords = new List<object>();

                using (StreamReader reader = File.OpenText(_fileName))
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
                    string itemType = Path.GetFileNameWithoutExtension(_fileName).Substring(15, 1);  // Get the item type indicator from the filename
                    string entityID = Path.GetFileNameWithoutExtension(_fileName).Substring(17);   // ignore the info added to the filename
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
                using (var writer = new StreamWriter(_fileName))
                using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(writeRecords);
                }

            }
        }
    }
}

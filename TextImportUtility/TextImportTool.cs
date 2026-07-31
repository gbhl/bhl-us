using CsvHelper;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BHL.TextImportUtility
{
    public class TextImportTool
    {
        string _fileName = string.Empty;
        string _fileContent = string.Empty;
        Dictionary<int, string> _fileContentParsed = null;

        // Constructors cannot be async in C#, so a static async factory method has been
        // added in addition to the constructor.
        // To use the async methods, use `await TextImportTool.CreateAsync(fileName)`
        // instead of `new TextImportTool(fileName)` to instantiate the class.
        public TextImportTool(string fileName)
        {
            _fileName = fileName;
            _fileContent = File.ReadAllText(fileName);
        }

        public static async Task<TextImportTool> CreateAsync(string fileName)
        {
            var tool = new TextImportTool(fileName);
            tool._fileContent = File.ReadAllText(fileName);
            return tool;
        }

        public string GetFileFormat()
        {
            // Pure in-memory string inspection; no I/O, so this stays synchronous.
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

        public async Task<int> PageCountAsync(string fileFormat = "")
        {
            Dictionary<int, string> parsedContent = await GetParsedFileContextAsync(fileFormat, false, false);
            return parsedContent.Count;
        }

        public async Task<string> GetTextAsync(string seqNo, string fileFormat = "")
        {
            if (!Int32.TryParse(seqNo, out int sequence)) throw new Exception(string.Format("Invalid sequence number: {0}", seqNo));

            Dictionary<int, string> parsedContent = await GetParsedFileContextAsync(fileFormat, true, true);

            string fileText;
            if (!parsedContent.ContainsKey(sequence))
                fileText = string.Format("Page {0} not found in file.", seqNo);
            else
                fileText = parsedContent[sequence];

            return fileText;
        }

        public async Task<bool> TextAvailableAsync(string seqNo, string fileFormat = "")
        {
            if (!Int32.TryParse(seqNo, out int sequence)) throw new Exception(string.Format("Invalid sequence number: {0}", seqNo));

            Dictionary<int, string> parsedContent = await GetParsedFileContextAsync(fileFormat, false, false);
            return parsedContent.ContainsKey(sequence);
        }

        private async Task<Dictionary<int, string>> GetParsedFileContextAsync(string fileFormat, bool includeContent, bool clean)
        {
            if (_fileContentParsed == null)
            {
                if (fileFormat == "") fileFormat = GetFileFormat();

                switch (fileFormat)
                {
                    case "ftp":
                        _fileContentParsed = ParseFTP(includeContent, clean);
                        break;
                    case "dv":
                        _fileContentParsed = await ParseDVAsync(clean);
                        break;
                    case "stc":
                        _fileContentParsed = await ParseSTCAsync(clean);
                        break;
                    case "bhlcsv":
                        _fileContentParsed = await ParseBHLCSVAsync(clean);
                        break;
                }
            }

            return _fileContentParsed;
        }

        /// <summary>
        /// Parse the transcription contents from a From The Page output file.
        /// Operates entirely on the already-loaded in-memory string (regex only),
        /// so there is no I/O to make async here.
        /// </summary>
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
        }

        /// <summary>
        /// Parse the transcription contents from a DigiVol output file
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<int, string>> ParseDVAsync(bool clean)
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
                    occurrenceRemarks = string.Empty,
                    sequenceNumber = default(int),  // 1-based index
                    transcriberNotes = string.Empty,
                    validatorNotes = string.Empty
                };

                // CsvHelper's GetRecordsAsync returns an IAsyncEnumerable<T>
                var records = csv.GetRecordsAsync(dvRecord);

                var enumerator = records.GetAsyncEnumerator();
                try
                {
                    while (await enumerator.MoveNextAsync())
                    {
                        var record = enumerator.Current;
                        string pageText = record.occurrenceRemarks.Replace("\\n", "\n");
                        if (clean) pageText = NormalizeMarkup(pageText);
                        contents.Add(record.sequenceNumber, pageText);
                    }
                }
                finally
                {
                    if (enumerator != null) await enumerator.DisposeAsync();
                }
            }

            return contents;
        }

        /// <summary>
        /// Parse the transcription contents from a Smithsonian Transcription Center output file
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<int, string>> ParseSTCAsync(bool clean)
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

                var records = csv.GetRecordsAsync(dvRecord);

                int sequenceNumber = 1;
                var enumerator = records.GetAsyncEnumerator();
                try
                {
                    while (await enumerator.MoveNextAsync())
                    {
                        var record = enumerator.Current;
                        string pageText = record.tl1_text;
                        if (clean) pageText = NormalizeMarkup(pageText);
                        contents.Add(sequenceNumber++, pageText);
                    }
                }
                finally
                {
                    if (enumerator != null) await enumerator.DisposeAsync();
                }
            }

            return contents;
        }

        /// <summary>
        /// Parse the transcription contents from a file that uses the generic BHL CSV format
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<int, string>> ParseBHLCSVAsync(bool clean)
        {
            Dictionary<int, string> contents = new Dictionary<int, string>();

            byte[] contentArray = System.Text.Encoding.UTF8.GetBytes(_fileContent);
            MemoryStream contentStream = new MemoryStream(contentArray);

            using (StreamReader reader = new StreamReader(contentStream))
            {
                var config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    PrepareHeaderForMatch = args => args.Header.ToLower(), // Convert both header and member name (allow case-insensitive headers)
                };
                CsvReader csv = new CsvReader(reader, config);

                var dvRecord = new
                {
                    PageID = string.Empty,
                    SequenceNumber = string.Empty,
                    Text = string.Empty
                };

                // Materialize records first since we need them alongside the async DB calls below.
                List<object> rawRecords = new List<object>();
                var records = csv.GetRecordsAsync(dvRecord);
                var recordList = new List<dynamic>();
                var enumerator = records.GetAsyncEnumerator();
                try
                {
                    while (await enumerator.MoveNextAsync())
                    {
                        var record = enumerator.Current;
                        recordList.Add(record);
                    }
                }
                finally
                {
                    if (enumerator != null) await enumerator.DisposeAsync();
                }

                // Get the pages for the item from the database
                string itemType = Path.GetFileNameWithoutExtension(_fileName).Substring(15, 1);  // Get the item type indicator from the filename
                string entityID = Path.GetFileNameWithoutExtension(_fileName).Substring(17);   // ignore the info added to the filename
                int itemID;
                if (itemType.ToUpper() == "S")
                {
                    Segment segment = await Task.Run(() => new BHLProvider().SegmentSelectAuto(Convert.ToInt32(entityID)));
                    itemID = segment.ItemID;
                }
                else // itemType == "I")
                {
                    Book book = await Task.Run(() => new BHLProvider().BookSelectAuto(Convert.ToInt32(entityID)));
                    itemID = book.ItemID;
                }
                List<Page> pages = await Task.Run(() => new BHLProvider().PageSelectByItemID(Convert.ToInt32(itemID)));

                foreach (var record in recordList)
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
        /// Replace supported HTML markup in the specified string with BHL-approved markup, and remove any unsupported markup.
        /// Pure string/regex transformation; kept synchronous.
        /// </summary>
        /// <param name="fileContents"></param>
        /// <returns></returns>
        private string NormalizeMarkup(string fileContents)
        {
            //---------------------------------------------------------------------------------------
            // The following statements clean up markup commonly found in FromThePage transcriptions

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

            //---------------------------------------------------------------------------------------
            // The following statements clean up markup commonly found in Smithsonian Transcription Center transcriptions

            fileContents = Regex.Replace(fileContents, @"\[\[[D|d]itto[|s]?(\s*for[:| ]\s*|\s?[:|-]\s?)(.*?)\]\]", @">>>>$2<<<<");

            // Convert markup for image to a temporary form
            fileContents = Regex.Replace(fileContents, @"\[\[([I|i]mage|[P|p]hoto|IMAGE)( of | ?[:|-] ?)(.*?)\]\]", @">>illustration<<$3>>/illustration<<");
            fileContents = fileContents.Replace("[[[I|i]mage]]", ">>illustration<<");

            // Convert markup for strikethrough to a temporary form
            fileContents = Regex.Replace(fileContents, @"\[\[/.*?[S|s]trikethrough.*?\]\]", ">>/strike<<");
            fileContents = Regex.Replace(fileContents, @"\[\[.*?[S|s]trikethrough.*?\]\]", ">>strike<<");
            fileContents = Regex.Replace(fileContents, @"\[\[/.*?[S|s]trikeout.*?\]\]", ">>/strike<<");
            fileContents = Regex.Replace(fileContents, @"\[\[.*?[S|s]trikeout.*?\]\]", ">>strike<<");

            // Convert markup for underline to a temporary form
            fileContents = Regex.Replace(fileContents, @"\[\[/.*?[U|u]nderline.*?\]\]", ">>/underline<<");
            fileContents = Regex.Replace(fileContents, @"\[\[.*?[U|u]nderline.*?\]\]", ">>underline<<");

            // Convert markup for male/female symbols to a temporary form
            fileContents = Regex.Replace(fileContents, @"\[\[male symbol\]\]", ">>male<<", RegexOptions.IgnoreCase);
            fileContents = Regex.Replace(fileContents, @"\[\[2 male symbols\]\]", ">>male<< >>male<<", RegexOptions.IgnoreCase);
            fileContents = Regex.Replace(fileContents, @"\[\[female symbol\]\]", ">>female<<", RegexOptions.IgnoreCase);
            fileContents = Regex.Replace(fileContents, @"\[\[2 female symbols\]\]", ">>female<< >>female<<", RegexOptions.IgnoreCase);

            // Convert markup for margins to a temporary form
            fileContents = Regex.Replace(fileContents, @"\[\[marginalia\]\]", ">>margin<<", RegexOptions.IgnoreCase);
            fileContents = Regex.Replace(fileContents, @"\[\[/marginalia\]\]", ">>/margin<<", RegexOptions.IgnoreCase);
            fileContents = Regex.Replace(fileContents, @"\[\[\s?(left|right|top|in)+ margin\]\]", ">>margin<<", RegexOptions.IgnoreCase);
            fileContents = Regex.Replace(fileContents, @"\[\[\s?(\\|/)(left|right|top|in)+ margin\]\]", ">>/margin<<", RegexOptions.IgnoreCase);

            // Clean up (remove) remaining [[ ]] markup
            fileContents = Regex.Replace(fileContents, @"\[\[\s*[Tt]able.*?\]\]", "");      // [[table]] by itself or with additional text (e.g., [[table of contents]])
            fileContents = Regex.Replace(fileContents, @"\[\[.*?\b[Tt]able\s*\]\]", "");    // [[table]] preceded by additional text (e.g., [[data table]] or [[/table]]) 
            fileContents = Regex.Replace(fileContents, @"\[\[\s*[Bb]lank.*?\]\]", "");      // [[blank]] by itself or with additional text (e.g., [[blank page]])
            fileContents = Regex.Replace(fileContents, @"\[\[.*?\b[Bb]lank\s*\]\]", "");    // [[blank]] preceded by additional text (e.g., [[last blank]] or [[/blank]]) 
            fileContents = Regex.Replace(fileContents, @"\[\[\s*[Ss]tart.*?\]\]", "");      // [[start]] by itself or with additional text (e.g., [[start of page 2]])
            fileContents = Regex.Replace(fileContents, @"\[\[.*?\b[Ss]tart\s*\]\]", "");    // [[start]] preceded by additional text (e.g., [[Page 2 start]] or [[/start]])
            fileContents = Regex.Replace(fileContents, @"\[\[\s*[Ee]nd\b.*?\]\]", "");      // [[end]] by itself or with additional text (e.g., [[end of page 2]])
            fileContents = Regex.Replace(fileContents, @"\[\[.*?\b[Ee]nd\s*\]\]", "");      // [[end]] preceded by additional text (e.g., [[Page 2 end]] or [[/end]]) 

            // Convert the temporary markup to the standard BHL form
            fileContents = Regex.Replace(fileContents, @">>>>(.*?)<<<<", @"[$1]");
            fileContents = Regex.Replace(fileContents, @">>illustration<<(.*?)>>/illustration<<", @"[[illustration]]$1[[/illustration]]");
            fileContents = fileContents.Replace(">>illustration<<", "[[illustration]]");
            fileContents = fileContents.Replace(">>strike<<", "[[strike]]").Replace(">>/strike<<", "[[/strike]]");
            fileContents = fileContents.Replace(">>underline<<", "[[underline]]").Replace(">>/underline<<", "[[/underline]]");
            fileContents = fileContents.Replace(">>male<<", "[male]");
            fileContents = fileContents.Replace(">>female<<", "[female]");
            fileContents = fileContents.Replace(">>margin<<", "[[margin]]").Replace(">>/margin<<", "[[/margin]]");

            //---------------------------------------------------------------------------------------
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

        /*
         
        /// <summary>
        /// Add sequence numbers to the specified file.
        /// </summary>
        /// <remarks>Only applies to BHL CSV files.</remarks>
        /// <param name="fileName"></param>
        public async Task AddSequenceNumbersAsync()
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
                        HasHeaderRecord = true,
                        PrepareHeaderForMatch = args => args.Header.ToLower(), // Convert both header and member name (allow case-insensitive headers)
                    };
                    CsvReader csv = new CsvReader(reader, config);

                    var dvRecord = new
                    {
                        PageID = string.Empty,
                        SequenceNumber = string.Empty,
                        Text = string.Empty
                    };

                    // Get the pages for the item from the database
                    string itemType = Path.GetFileNameWithoutExtension(_fileName).Substring(15, 1);  // Get the item type indicator from the filename
                    string entityID = Path.GetFileNameWithoutExtension(_fileName).Substring(17);   // ignore the info added to the filename
                    int itemID;
                    if (itemType.ToUpper() == "S")
                    {
                        Segment segment = await Task.Run(() => new BHLProvider().SegmentSelectAuto(Convert.ToInt32(entityID)));
                        itemID = segment.ItemID;
                    }
                    else // itemType == "I")
                    {
                        Book book = await Task.Run(() => new BHLProvider().BookSelectAuto(Convert.ToInt32(entityID)));
                        itemID = book.ItemID;
                    }
                    List<Page> pages = await Task.Run(() => new BHLProvider().PageSelectByItemID(Convert.ToInt32(itemID)));

                    var readRecords = csv.GetRecordsAsync(dvRecord);
                    var enumerator = readRecords.GetAsyncEnumerator();
                    try
                    {
                        while (await enumerator.MoveNextAsync())
                        {
                            var record = enumerator.Current;

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
                    finally
                    {
                        if (enumerator != null) await enumerator.DisposeAsync();
                    }
                }

                // Write the updated records to the file
                using (var writer = new StreamWriter(_fileName))
                using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                {
                    await csv.WriteRecordsAsync(writeRecords);
                }
            }
        }
        */

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
                        HasHeaderRecord = true,
                        PrepareHeaderForMatch = args => args.Header.ToLower(), // Convert both header and member name (allow case-insensitive headers)
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

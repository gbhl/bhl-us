using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace MOBOT.BHL.BHLPDFGenerator
{
    public class PDFDocument
    {
        #region Attributes

        private ICollection<Page> _pageMetadata = null;
        private PDF _pdfRecord = null;
        public PDF PdfRecord
        {
            get { return _pdfRecord; }
            set { 
                _pdfRecord = value; 
                _pageMetadata = (_pdfRecord.BookID == null ? new SegmentsClient(_bhlWSUrl).GetSegmentPages((int)_pdfRecord.SegmentID) : new BooksClient(_bhlWSUrl).GetBookPages((int)_pdfRecord.BookID));
            }
        }

        private List<String> _pageUrls = null;
        public List<String> PageUrls
        {
            get { return _pageUrls; }
            set { _pageUrls = value; }
        }

        private String _filePathFormat = String.Empty;
        public String FilePathFormat
        {
            get { return _filePathFormat; }
            set { _filePathFormat = value; }
        }

        private String _urlFormat = String.Empty;
        public String UrlFormat
        {
            get { return _urlFormat; }
            set { _urlFormat = value; }
        }

        private String _bhlWSUrl = String.Empty;
        public String BHLWSUrl
        {
            get { return _bhlWSUrl; }
            set { _bhlWSUrl = value; }
        }

        private String _fileName = String.Empty;
        public String FileName
        {
            get { return _fileName; }
        }

        private String _fileLocation = String.Empty;
        public String FileLocation
        {
            get { return _fileLocation; }
        }

        private String _fileUrl = String.Empty;
        public String FileUrl
        {
            get { return _fileUrl; }
        }

        private int _numberImagesMissing = 0;
        public int NumberImagesMissing
        {
            get { return _numberImagesMissing; }
            set { _numberImagesMissing = value; }
        }

        private int _numberOcrMissing = 0;
        public int NumberOcrMissing
        {
            get { return _numberOcrMissing; }
            set { _numberOcrMissing = value; }
        }

        private int _imageQuality = 40;
        public int ImageQuality
        {
            get { return _imageQuality; }
            set { _imageQuality = value; }
        }

        private List<string> _imageErrors = new List<string>();
        public List<string> ImageErrors
        {
            get { return _imageErrors; }
            set { _imageErrors = value; }
        }

        private List<String> _pageLabels = new List<string>();

        #endregion Attributes

        #region Constructors

        public PDFDocument()
        {
        }

        public PDFDocument(PDF pdfRecord, List<String> pageUrls, String filePathFormat, String urlFormat, string bhlWSUrl)
        {
            this.BHLWSUrl = bhlWSUrl;
            this.PdfRecord = pdfRecord;
            this.PageUrls = pageUrls;
            this.FilePathFormat = filePathFormat;
            this.UrlFormat = urlFormat;
        }

        #endregion Constructors

        public bool GenerateFile(int retryImageWait = 0)
        {
            if (this.PdfRecord == null) throw (new Exception("No PDF record specified"));
            if (this.PageUrls.Count == 0) throw (new Exception("No page Urls specified"));
            if (this.FilePathFormat == String.Empty) throw (new Exception("No file path specified"));

            String fileName = String.Empty;
            iTextSharp.text.Document doc = null;

            try
            {
                // Build the filename for the pdf.  Use PDFID, item/part indicator, and the ItemID
                // to construct the filename.
                // ex. 0001000i00001000.pdf, 0001001p00023546.pdf
                fileName = this.PdfRecord.PdfID.ToString().PadLeft(7, '0') + (this.PdfRecord.BookID != null ? "i" : "p") +
                    (this.PdfRecord.BookID ?? this.PdfRecord.SegmentID).ToString().PadLeft(8, '0');

                // Initialize the PDF document
                doc = new Document();
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc,
                    new FileStream(String.Format(this.FilePathFormat, fileName), FileMode.Create));

                // Add metadata
                AddMetadata(doc);
                writer.XmpMetadata = this.GetXmpMetadata();

                // Set margins and page size 
                SetStandardPageSize(doc);

                // Start writing the PDF
                doc.Open();

                // Add header pages to the PDF
                this.AddHeaderPages(doc, fileName);

                // Add the page images to the PDF
                doc.SetMargins(0, 0, 0, 0);
                foreach (String pageUrl in PageUrls)
                {
                    // Add a page image and its associated text
                    List<Tuple<string, float, float, float, float, float>> pageWords = new List<Tuple<string, float, float, float, float, float>>();
                    try
                    {
                        pageWords = this.LoadOcrPageText(pageUrl);
                    }
                    catch(Exception ex)
                    {
                        this.ImageErrors.Add(string.Format("Page Text: {0}\r\nMessage: {1}\r\nStack Trace: {2}", pageUrl.Split('|')[1], ex.Message, ex.StackTrace));
                        this._numberOcrMissing++;
                    }
                    this.AddImageAndOCRToPDF(doc, writer, pageUrl, retryImageWait, pageWords);
                }

                // Add page labels to the PDF
                int pageNumber = 0;
                iTextSharp.text.pdf.PdfPageLabels pdfPageLabels = new iTextSharp.text.pdf.PdfPageLabels();
                foreach (String pageLabel in _pageLabels)
                {
                    pdfPageLabels.AddPageLabel(++pageNumber, iTextSharp.text.pdf.PdfPageLabels.EMPTY, pageLabel);
                }
                writer.PageLabels = pdfPageLabels;

                doc.Close();

                // Add PDF extension to temp file
                if (File.Exists(String.Format(this.FilePathFormat, fileName + ".pdf"))) File.Delete(String.Format(this.FilePathFormat, fileName + ".pdf"));
                File.Move(String.Format(this.FilePathFormat, fileName), String.Format(this.FilePathFormat, fileName + ".pdf"));

                fileName += ".pdf";
                this._fileName = fileName;
                this._fileLocation = String.Format(this.FilePathFormat, fileName);
                this._fileUrl = String.Format(this.UrlFormat, fileName);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (doc != null)
                {
                    // Finish writing the PDF
                    if (doc.IsOpen()) doc.Close();
                }
            }

            return true;
        }

        private void AddMetadata(iTextSharp.text.Document doc)
        {
            doc.AddCreator("Biodiversity Heritage Library");
            doc.AddKeywords("PDF ID: " + this.PdfRecord.PdfID.ToString());

            if (this.PdfRecord.ArticleTitle != String.Empty)
            {
                doc.AddTitle(this.PdfRecord.ArticleTitle);
                doc.AddHeader("title", this.PdfRecord.ArticleTitle);
            }
            if (this.PdfRecord.ArticleCreators != String.Empty)
            {
                doc.AddHeader("author", this.PdfRecord.ArticleCreators);
            }

            if (this.PdfRecord.ArticleTitle == string.Empty && this.PdfRecord.ArticleCreators == string.Empty && this.PdfRecord.SegmentID != null)
            {
                Segment segment = new SegmentsClient(_bhlWSUrl).GetSegmentDetails((int)this.PdfRecord.SegmentID);
                if (segment != null)
                {
                    doc.AddTitle(segment.Title);
                    doc.AddHeader("title", segment.Title);

                    if (!string.IsNullOrWhiteSpace(segment.Authors)) doc.AddHeader("author", segment.Authors);
                }
            }

            if (this.PdfRecord.ArticleTags != String.Empty)
            {
                doc.AddHeader("subject", this.PdfRecord.ArticleTags);
            }
        }

        private byte[] GetXmpMetadata()
        {
            byte[] buffer = new byte[65536];
            System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer, true);

            try
            {
                iTextSharp.text.xml.xmp.XmpWriter xmp = new iTextSharp.text.xml.xmp.XmpWriter(ms);
                iTextSharp.text.xml.xmp.XmpSchema dc = new iTextSharp.text.xml.xmp.DublinCoreSchema();

                Segment segment = null;
                if (PdfRecord.SegmentID != null) segment = new SegmentsClient(_bhlWSUrl).GetSegmentDetails((int)this.PdfRecord.SegmentID);

                // Add Dublin Core attributes
                iTextSharp.text.xml.xmp.LangAlt dcTitle = new iTextSharp.text.xml.xmp.LangAlt();
                if (string.IsNullOrWhiteSpace(PdfRecord.ArticleTitle) && segment != null)
                    dcTitle.Add("x-default", segment.Title);
                else
                    dcTitle.Add("x-default", this.PdfRecord.ArticleTitle);
                dc.SetProperty(iTextSharp.text.xml.xmp.DublinCoreSchema.TITLE, dcTitle);

                iTextSharp.text.xml.xmp.XmpArray dcAuthor = new iTextSharp.text.xml.xmp.XmpArray(iTextSharp.text.xml.xmp.XmpArray.ORDERED);
                if (string.IsNullOrWhiteSpace(PdfRecord.ArticleCreators) && segment != null)
                {
                    foreach(ItemAuthor sa in segment.AuthorList) dcAuthor.Add(sa.FullName);
                }
                else
                {
                    String[] authors = this.PdfRecord.ArticleCreators.Split(',');
                    foreach (String author in authors)
                    {
                        dcAuthor.Add(author);
                    }
                }
                dc.SetProperty(iTextSharp.text.xml.xmp.DublinCoreSchema.CREATOR, dcAuthor);

                iTextSharp.text.xml.xmp.XmpArray dcSubject = new iTextSharp.text.xml.xmp.XmpArray(iTextSharp.text.xml.xmp.XmpArray.UNORDERED);
                String[] subjects = this.PdfRecord.ArticleTags.Split(',');
                foreach (String subject in subjects)
                {
                    dcSubject.Add(subject);
                }
                dc.SetProperty(iTextSharp.text.xml.xmp.DublinCoreSchema.SUBJECT, dcSubject);

                xmp.AddRdfDescription(dc);
                xmp.Close();    // This flushes the XMP metadata into the memory buffer

                //---------------------------------------------------------------------------------
                // Shrink the buffer to the correct size (discard empty elements of the byte array)
                int bufsize = buffer.Length;
                int bufcount = 0;
                foreach (byte b in buffer)
                {
                    if (b == 0) break;
                    bufcount++;
                }
                System.IO.MemoryStream ms2 = new System.IO.MemoryStream(buffer, 0, bufcount);
                buffer = ms2.ToArray();
                //---------------------------------------------------------------------------------

                return buffer;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ms.Dispose();
            }
        }

        private static void SetStandardPageSize(iTextSharp.text.Document doc)
        {
            doc.SetPageSize(new iTextSharp.text.Rectangle(iTextSharp.text.PageSize.A4.Width,
                iTextSharp.text.PageSize.A4.Height));
            doc.SetMargins(50, 50, 50, 50);
        }

        private void AddImageAndOCRToPDF(Document pdf, PdfWriter writer, string pageUrl, int retryImageWait,
            List<Tuple<string, float, float, float, float, float>> pageWords)
        {
            string imagePath = pageUrl.Split('|')[1];
            Font standardFont = new Font(Font.HELVETICA, 10, Font.NORMAL, Color.BLACK);

            Image image = null;
            bool downloadFailed = false;

            try
            {
                int attempts = 0;
                bool tryDownload = true;

                while (tryDownload)
                {
                    attempts++;
                    try
                    {
                        // Get a re-sampled instance of the image (decrease quality, hopefully undetectable or nearly so)
                        using (Stream imageStream = new WebClient().OpenRead(new Uri(imagePath)))
                        {
                            var resampledStream = ReduceImageQuality(imageStream);
                            image = Image.GetInstance(((MemoryStream)resampledStream).ToArray());
                        }
                        // Use this if not worried about resizing/resampling images
                        //image = Image.GetInstance(new Uri(imagePath));
                        tryDownload = false;    // no need to continue downloads
                    }
                    catch (Exception ex)
                    {
                        this.ImageErrors.Add(string.Format(
                            "Image: {0}\r\nAttempt: {1}\r\nMessage: {2}\r\nStack Trace: {3}",
                            imagePath, attempts.ToString(), ex.Message, ex.StackTrace));

                        // If three attempts have been made and no image has been obtained, just rethrow the error
                        if (attempts >= 3)
                        {
                            downloadFailed = true;
                            throw ex;
                        }
                        System.Threading.Thread.Sleep(retryImageWait);   // Wait before re-trying the download
                    }
                }

                float imageWidth = image.Width;
                float imageHeight = image.Height;
                pdf.SetMargins(0, 0, 0, 0);

                float scaleFactor = (PageSize.A4.Height / image.Height);
                float newWidth = image.Width * scaleFactor;
                image.ScaleAbsolute(newWidth, PageSize.A4.Height);
                pdf.SetPageSize(new Rectangle(newWidth, PageSize.A4.Height));

                pdf.NewPage();

                // Add a layer with the OCR text
                foreach (Tuple<string, float, float, float, float, float> ocrWord in pageWords)
                {
                    this.AddHiddenText(writer, Element.ALIGN_LEFT, standardFont, ocrWord, scaleFactor, image.Height);
                }

                image.SetAbsolutePosition(0, 0);  // point (0,0) is the upper left corner of the page
                pdf.Add(image); // SetAbsolutePosition() will cause text to "overright" this image
            }
            catch (Exception ex)
            {
                if (!downloadFailed)
                {
                    // Not a download error, so we need to log it (download errors logged elsewhere)
                    this.ImageErrors.Add(string.Format("Image: {0}\r\nMessage: {1}\r\nStack Trace: {2}", imagePath, ex.Message, ex.StackTrace));
                }

                // Error getting the image, add a "Page Unavailable" placeholder
                this._numberImagesMissing++;
                SetStandardPageSize(pdf);
                pdf.NewPage();
                this.AddParagraph(pdf, Element.ALIGN_CENTER, new Font(Font.HELVETICA, 12, Font.NORMAL, Color.BLACK), "Page Unavailable");
            }
        }

        private Stream ReduceImageQuality(Stream stream)
        {
            var outputStream = new MemoryStream();
            using (var skData = SkiaSharp.SKData.Create(stream))
            {
                using (var codec = SkiaSharp.SKCodec.Create(skData))
                {
                    using (var destinationImage = SkiaSharp.SKBitmap.Decode(codec))
                    {
                        using (var outputImage = SkiaSharp.SKImage.FromBitmap(destinationImage))
                        {
                            using (var data = outputImage.Encode(SkiaSharp.SKEncodedImageFormat.Jpeg, quality: this.ImageQuality))
                            {
                                data.SaveTo(outputStream);
                            }
                        }
                    }
                }
            }
            return outputStream;
        }

        private List<Tuple<string, float, float, float, float, float>> LoadOcrPageText(string pageUrl)
        {
            List<Tuple<string, float, float, float, float, float>> ocrWords;

            // Get the DJVU from IA
            Stream djvu = GetDJVU(this.PdfRecord.BookID.ToString());

            // Convert the DJVU for the page into a list of words
            int sequenceOrder = Int32.Parse(pageUrl.Split('|')[3]);
            ocrWords = LoadDjvuForPage(djvu, sequenceOrder);

            return ocrWords;
        }

        /// <summary>
        /// Get the contents of the DJVU file for the specified barcode from Internet Archive
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private Stream GetDJVU(string bookID)
        {
            Stream djvu = Stream.Null;
            BooksClient booksClient = new BooksClient(this.BHLWSUrl);
            ConfigurationClient configurationClient = new ConfigurationClient(this.BHLWSUrl);

            Item item = booksClient.GetBookFilenames(Convert.ToInt32(bookID));
            string djvuPath = configurationClient.GetDjvuFilePath(item.BarCode, item.DjvuFilename);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(djvuPath);
            req.Method = "GET";
            req.Timeout = 15000;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            djvu = resp.GetResponseStream();

            return djvu;
        }

        /// <summary>
        /// Convert the specified page's DJVU stream to a list of words
        /// </summary>
        /// <param name="djvu">DJVU stream from which to extract words</param>
        /// <param name="sequenceOrder">Page for which to extract words</param>
        private List<Tuple<string, float, float, float, float, float>> LoadDjvuForPage(Stream djvu, int sequenceOrder)
        {
            List<Tuple<string, float, float, float, float, float>> pageWords = new List<Tuple<string, float, float, float, float, float>>();

            StringBuilder pageText = new StringBuilder();
            XmlReaderSettings settings = new XmlReaderSettings() { Async = true, DtdProcessing = DtdProcessing.Parse };
            using (XmlReader reader = XmlReader.Create(djvu, settings))
            {
                bool wordStarted = false;
                float leftX = 0;
                float leftY = 0;
                float rightX = 0;
                float rightY = 0;
                int pageSequence = 0;
                bool pageToRead = false;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "OBJECT")
                    {
                        pageSequence++;
                        if (pageSequence == sequenceOrder) pageToRead = true;
                    }
                    if (reader.NodeType == XmlNodeType.Element && pageToRead && reader.Name == "WORD")
                    {
                        wordStarted = true;
                        // Coords in a DJVU file are listed in the following order: lower-left-x, lower-left-y, upper-right-x, upper-right-y
                        // The upper left corner of a page is point (0,0)
                        /*  Example: 
                            <LINE>
                            <WORD coords="131,641,435,573" x-confidence="54">JOURNAL </WORD>
                            <WORD coords="435,641,544,575" x-confidence="24">OF </WORD>
                            <WORD coords="544,642,942,576" x-confidence="37">MICROSCOPY </WORD>
                            </LINE>
                        */
                        string coords = reader.GetAttribute("coords");
                        string[] coordList = coords.Split(',');
                        leftX = float.Parse(coordList[0]);
                        leftY = float.Parse(coordList[1]);
                        rightX = float.Parse(coordList[2]);
                        rightY = float.Parse(coordList[3]);
                    }
                    if (reader.NodeType == XmlNodeType.Text && pageToRead && wordStarted) pageText.Append(reader.Value + " ");
                    if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == "WORD" && pageToRead)
                        {
                            pageWords.Add(new Tuple<string, float, float, float, float, float>(
                                pageText.ToString(), rightX, rightY, leftX, leftY, 0)
                            );

                            wordStarted = false;
                            leftX = 0;
                            leftY = 0;
                            rightX = 0;
                            rightY = 0;
                            pageText.Clear();
                        }
                        if (reader.Name == "OBJECT" && pageToRead)
                        {
                            break;
                        }
                    }
                }
            }

            return pageWords;
        }

        private void AddHiddenText(PdfWriter writer, int alignment, Font font, Tuple<string, float, float, float, float, float> ocrWord,
            float scaleFactor, float imageHeight)
        {
            string content = ocrWord.Item1;
            float llx = (ocrWord.Item4 * scaleFactor);
            float lly = ((imageHeight - ocrWord.Item5) * scaleFactor) - 10; // -10 adjustment to correctly align with image
            float urx = (ocrWord.Item2 * scaleFactor);
            float ury = ((imageHeight - ocrWord.Item3) * scaleFactor) - 10; // -10 adjustment to correctly align with image

            ColumnText ct = new ColumnText(writer.DirectContentUnder);   // DirectContent for testing (visible), DirectContentUnder for production (hidden)
            ct.SetSimpleColumn(llx, lly, urx, ury);
            ct.AddElement(new Paragraph(content)
            {
                Alignment = alignment,
                Font = font,
                Leading = 0f
                //,MultipliedLeading = 0.9f   // If this used, then -10 adjustment of Y values not needed above.  However, many words dropped (why?).
            });
            ct.Go();
        }

        private void AddHeaderPages(Document doc, String fileName)
        {
            //BHLWS.BHLWS service = new MOBOT.BHL.BHLPDFGenerator.BHLWS.BHLWS();
            ICollection<PageSummaryView> pages = new PageSummaryViewClient(_bhlWSUrl).GetPageSummaryViewByPdf((int)this.PdfRecord.PdfID);

            if (pages.Count > 0)
            {
                PageSummaryView firstPage = ((List<PageSummaryView>)pages)[0];

                _pageLabels.Add("Title Page");
                _pageLabels.Add(" ");

                Title title = new Title();
                title = new TitlesClient(_bhlWSUrl).GetTitle((int)firstPage.TitleID);

                // Set up the fonts to be used
                iTextSharp.text.Font largeFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 14, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);

                // Generate links
                String bhlUrl = "https://www.biodiversitylibrary.org/";
                Anchor bhlAnchor = new Anchor(bhlUrl, standardFont);
                bhlAnchor.Reference = bhlUrl;
                String titleUrl = "https://www.biodiversitylibrary.org/bibliography/" + firstPage.TitleID.ToString();
                Anchor titleAnchor = new Anchor(titleUrl, standardFont);
                titleAnchor.Reference = titleUrl;
                String itemUrl;
                if (this.PdfRecord.BookID != null)
                    itemUrl = "https://www.biodiversitylibrary.org/item/" + this.PdfRecord.BookID.ToString(); // pages[0].BookID.ToString();
                else
                    itemUrl = "https://www.biodiversitylibrary.org/segment/" + this.PdfRecord.SegmentID.ToString();
                Anchor itemAnchor = new Anchor(itemUrl, standardFont);
                itemAnchor.Reference = itemUrl;
                Anchor pdfAnchor = new Anchor(String.Format(this.UrlFormat, fileName + ".pdf"), smallFont);
                pdfAnchor.Reference = String.Format(this.UrlFormat, fileName + ".pdf");

                // ---------------- First page ----------------

                // Add the BHL logo
                String appPath = System.IO.Directory.GetCurrentDirectory();
                iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance(appPath + "\\bhllogo.png");
                //logoImage.ScaleToFit(logoImage.Width, logoImage.Height); 
                logoImage.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                doc.Add(logoImage);
                logoImage = null;

                // Add text
                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, standardFont, bhlAnchor);
                this.AddSpace(doc, standardFont);
                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, largeFont, firstPage.FullTitle, 60, 60);
                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, title.PublicationDetails, 60, 60);
                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, titleAnchor, 60, 60);
                this.AddSpace(doc, standardFont);

                if (PdfRecord.SegmentID != null)
                {
                    // Add the article title
                    Segment segment = null;
                    if (this._pdfRecord.SegmentID != null)
                    {
                        segment = new SegmentsClient(_bhlWSUrl).GetSegment((int)this._pdfRecord.SegmentID);
                        if (segment != null)
                        {
                            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, largeFont, segment.Title, 60, 60);
                        }
                    }
                }

                Paragraph volumeInfo = new Paragraph();
                if (PdfRecord.BookID != null)
                {
                    // Include the volume
                    if ((firstPage.Volume ?? "") == "")
                    {
                        volumeInfo.Add(new Chunk((_pdfRecord.BookID != null ? "Item: " : "Part: "), largeFont));
                    }
                    else
                    {
                        volumeInfo.Add(new Chunk(firstPage.Volume + ": ", largeFont));
                    }
                }
                volumeInfo.Add(itemAnchor);

                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, volumeInfo, 60, 60);
                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, standardFont, " ");

                // Add article metadata, if it is available
                if (this.PdfRecord.ArticleTitle.Trim() != string.Empty) this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, "Article/Chapter Title: " + this.PdfRecord.ArticleTitle.Trim(), 60, 60);
                if (this.PdfRecord.ArticleCreators.Trim() != string.Empty) this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, "Author(s): " + this.PdfRecord.ArticleCreators.Trim(), 60, 60);
                if (this.PdfRecord.ArticleTags.Trim() != string.Empty) this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, "Subject(s): " + this.PdfRecord.ArticleTags.Trim(), 60, 60);

                // Include the list of pages
                Paragraph pageInfo = new Paragraph();
                pageInfo.Add("Page(s): ");
                String pageList = String.Empty;
                foreach (PageSummaryView page in pages)
                {
                    String pageDesc = this.GetPageDescription((int)page.PageID);
                    _pageLabels.Add(pageDesc);
                    if (pageDesc != String.Empty)
                    {
                        if (pageList != String.Empty) pageList += ", ";
                        pageList += pageDesc;
                    }
                }
                pageInfo.Add(pageList);
                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, pageInfo, 60, 60);
                this.AddSpace(doc, standardFont);

                string sponsor = string.Empty;
                if (this.PdfRecord.BookID != null)
                {
                    Book book = new BooksClient(_bhlWSUrl).GetBook((int)this.PdfRecord.BookID);
                    if (book != null) sponsor = book.Sponsor;
                }

                string role;
                ICollection<Institution> institutions;
                if (this.PdfRecord.BookID != null)
                {
                    role = "Holding Institution";
                    institutions = new ItemsClient(_bhlWSUrl).GetItemInstitutionsByRole((int)firstPage.ItemID, role);
                }
                else
                {
                    role = "Contributor";
                    institutions = new SegmentsClient(_bhlWSUrl).GetSegmentInstitutionsByRole((int)this.PdfRecord.SegmentID, role);
                }

                if (institutions != null || sponsor != string.Empty)
                {
                    Institution institution = ((List<Institution>)institutions)[0];
                    if (institution != null) this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, role + ": " + institution.InstitutionName, 60, 60);
                    if (sponsor != String.Empty) this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, "Sponsored by: " + sponsor, 60, 60);
                    this.AddSpace(doc, standardFont);
                }

                // Add the page footer information
                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, smallFont,
                    "Generated " +
                    DateTime.Now.Day.ToString() + " " +
                    System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) + " " +
                    DateTime.Now.Year.ToString() + " " +
                    DateTime.Now.ToShortTimeString(), 60, 60);

                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, smallFont, pdfAnchor, 60, 60);

                // ---------------- Second page ----------------

                doc.NewPage();

                // Show the legal text if we have any, else add a blank page
                String legal = System.IO.File.ReadAllText(appPath + "\\legal.txt");
                if (legal.Length > 0)
                {
                    _pageLabels[1] = "Legal";
                    this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, legal);
                }
                else
                {
                    _pageLabels[1] = "Blank";
                    this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, standardFont, "This page intentionally left blank.");
                }
            }
        }

        private String GetPageDescription(int pageID)
        {
            String pageDescription = String.Empty;

            foreach (Page pageMeta in this._pageMetadata)
            {
                if (pageMeta.PageID == pageID)
                {
                    pageDescription = (pageMeta.IndicatedPages == "" ? pageMeta.PageTypes : pageMeta.IndicatedPages);
                    break;
                }
            }

            return pageDescription;
        }

        private void AddParagraph(Document doc, int alignment, iTextSharp.text.Font font, object content, 
            float indentationRight = 0, float indentationLeft = 0)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.SetLeading(0f, 1.2f);
            paragraph.Alignment = alignment;
            paragraph.Font = font;
            paragraph.IndentationLeft = indentationLeft;
            paragraph.IndentationRight = indentationRight;
            paragraph.Add(content);
            doc.Add(paragraph);
        }

        private void AddSpace(Document doc, iTextSharp.text.Font font)
        {
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, font, " ");
            this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_CENTER, font, " ");
        }
    }
}

using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;

// iText 9.7 (formerly iTextSharp 4.1.2)
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.XMP;
using iText.Kernel.XMP.Options;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

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

        /// <summary>
        /// Small bundle of PdfFont + size + color, standing in for the old iTextSharp
        /// Font object (which combined face, size, style and color in one instance).
        /// </summary>
        private readonly struct HeaderFont
        {
            public readonly PdfFont Font;
            public readonly float Size;
            public readonly Color Color;

            public HeaderFont(PdfFont font, float size, Color color)
            {
                Font = font;
                Size = size;
                Color = color;
            }
        }

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
            PdfDocument pdfDoc = null;
            Document doc = null;

            try
            {
                // Build the filename for the pdf.  Use PDFID, item/part indicator, and the ItemID
                // to construct the filename.
                // ex. 0001000i00001000.pdf, 0001001p00023546.pdf
                fileName = this.PdfRecord.PdfID.ToString().PadLeft(7, '0') + (this.PdfRecord.BookID != null ? "i" : "p") +
                    (this.PdfRecord.BookID ?? this.PdfRecord.SegmentID).ToString().PadLeft(8, '0');

                // Initialize the PDF document
                PdfWriter writer = new PdfWriter(String.Format(this.FilePathFormat, fileName));
                pdfDoc = new PdfDocument(writer);
                doc = new Document(pdfDoc, PageSize.A4);
                doc.SetMargins(50, 50, 50, 50);

                // Add metadata
                AddMetadata(pdfDoc);
                pdfDoc.SetXmpMetadata(GetXmpMetadata());

                // Add header pages to the PDF
                this.AddHeaderPages(doc, pdfDoc, fileName);

                // Add the page images to the PDF
                foreach (String pageUrl in PageUrls)
                {
                    // Add a page image and its associated text
                    List<Tuple<string, float, float, float, float, float>> pageWords = new List<Tuple<string, float, float, float, float, float>>();
                    try
                    {
                        pageWords = this.LoadOcrPageText(pageUrl);
                    }
                    catch (Exception ex)
                    {
                        this.ImageErrors.Add(string.Format("Page Text: {0}\r\nMessage: {1}\r\nStack Trace: {2}", pageUrl.Split('|')[1], ex.Message, ex.StackTrace));
                        this._numberOcrMissing++;
                    }
                    this.AddImageAndOCRToPDF(pdfDoc, pageUrl, retryImageWait, pageWords);
                }

                // Add page labels to the PDF (one per page, in page order)
                for (int i = 0; i < _pageLabels.Count; i++)
                {
                    // Passing a null numbering style means only the prefix text is shown -
                    // equivalent to iTextSharp's PdfPageLabels.EMPTY numbering style.
                    pdfDoc.GetPage(i + 1).SetPageLabel(null, _pageLabels[i]);
                }

                // Closing the layout Document also closes/flushes the underlying PdfDocument.
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
                if (doc != null && pdfDoc != null && !pdfDoc.IsClosed())
                {
                    // Finish writing the PDF
                    doc.Close();
                }
            }

            return true;
        }

        private void AddMetadata(PdfDocument pdfDoc)
        {
            PdfDocumentInfo info = pdfDoc.GetDocumentInfo();

            info.SetCreator("Biodiversity Heritage Library");
            info.SetKeywords("PDF ID: " + this.PdfRecord.PdfID.ToString());

            if (this.PdfRecord.ArticleTitle != String.Empty)
            {
                info.SetTitle(this.PdfRecord.ArticleTitle);
                info.SetMoreInfo("title", this.PdfRecord.ArticleTitle);
            }
            if (this.PdfRecord.ArticleCreators != String.Empty)
            {
                info.SetMoreInfo("author", this.PdfRecord.ArticleCreators);
            }

            if (this.PdfRecord.ArticleTitle == string.Empty && this.PdfRecord.ArticleCreators == string.Empty && this.PdfRecord.SegmentID != null)
            {
                Segment segment = new SegmentsClient(_bhlWSUrl).GetSegmentDetails((int)this.PdfRecord.SegmentID);
                if (segment != null)
                {
                    info.SetTitle(segment.Title);
                    info.SetMoreInfo("title", segment.Title);

                    if (!string.IsNullOrWhiteSpace(segment.Authors)) info.SetMoreInfo("author", segment.Authors);
                }
            }

            if (this.PdfRecord.ArticleTags != String.Empty)
            {
                info.SetMoreInfo("subject", this.PdfRecord.ArticleTags);
            }
        }

        private XMPMeta GetXmpMetadata()
        {
            try
            {
                XMPMeta xmp = XMPMetaFactory.Create();

                Segment segment = null;
                if (PdfRecord.SegmentID != null) segment = new SegmentsClient(_bhlWSUrl).GetSegmentDetails((int)this.PdfRecord.SegmentID);

                // Dublin Core - title
                string dcTitle = string.IsNullOrWhiteSpace(PdfRecord.ArticleTitle) && segment != null ? segment.Title : this.PdfRecord.ArticleTitle;
                xmp.SetLocalizedText(XMPConst.NS_DC, "title", null, "x-default", dcTitle);

                // Dublin Core - creator (ordered array)
                if (string.IsNullOrWhiteSpace(PdfRecord.ArticleCreators) && segment != null)
                {
                    foreach (ItemAuthor sa in segment.AuthorList)
                    {
                        xmp.AppendArrayItem(XMPConst.NS_DC, "creator", new PropertyOptions(PropertyOptions.ARRAY_ORDERED), sa.FullName, null);
                    }
                }
                else
                {
                    String[] authors = this.PdfRecord.ArticleCreators.Split(',');
                    foreach (String author in authors)
                    {
                        xmp.AppendArrayItem(XMPConst.NS_DC, "creator", new PropertyOptions(PropertyOptions.ARRAY_ORDERED), author, null);
                    }
                }

                // Dublin Core - subject (unordered array)
                String[] subjects = this.PdfRecord.ArticleTags.Split(',');
                foreach (String subject in subjects)
                {
                    xmp.AppendArrayItem(XMPConst.NS_DC, "subject", new PropertyOptions(PropertyOptions.ARRAY), subject, null);
                }

                return xmp;// XMPMetaFactory.SerializeToBuffer(xmp, new SerializeOptions().SetOmitPacketWrapper(false));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AddImageAndOCRToPDF(PdfDocument pdfDoc, string pageUrl, int retryImageWait,
            List<Tuple<string, float, float, float, float, float>> pageWords)
        {
            string imagePath = pageUrl.Split('|')[1];
            PdfFont ocrFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            const float ocrFontSize = 6.0f;

            ImageData imageData = null;
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
                            MemoryStream resampledStream = (MemoryStream)ReduceImageQuality(imageStream);
                            imageData = ImageDataFactory.Create(resampledStream.ToArray());
                        }
                        // Use this if not worried about resizing/resampling images
                        //imageData = ImageDataFactory.Create(new Uri(imagePath));
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

                float imageWidth = imageData.GetWidth();
                float imageHeight = imageData.GetHeight();

                float scaleFactor = PageSize.A4.GetHeight() / imageHeight;
                float newWidth = imageWidth * scaleFactor;

                // Each image page is sized to match the (scaled) image, same as the original.
                PageSize customPageSize = new PageSize(newWidth, PageSize.A4.GetHeight());
                PdfPage page = pdfDoc.AddNewPage(customPageSize);
                PdfCanvas canvas = new PdfCanvas(page);

                canvas.AddImageFittedIntoRectangle(imageData, new Rectangle(0, 0, newWidth, PageSize.A4.GetHeight()), false);

                // Add an invisible OCR text layer (text-rendering mode 3) over the image.
                // This makes the page's text selectable/searchable without being visible -
                // functionally equivalent to the old DirectContentUnder + ColumnText approach.
                foreach (Tuple<string, float, float, float, float, float> ocrWord in pageWords)
                {
                    this.AddHiddenText(canvas, ocrFont, ocrFontSize, ocrWord, scaleFactor, imageHeight);
                }
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

                PdfPage fallbackPage = pdfDoc.AddNewPage(PageSize.A4);
                PdfFont fallbackFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                Canvas fallbackCanvas = new Canvas(fallbackPage, fallbackPage.GetPageSize());
                fallbackCanvas.Add(new Paragraph("Page Unavailable")
                    .SetFont(fallbackFont)
                    .SetFontSize(12)
                    .SetFontColor(ColorConstants.BLACK)
                    .SetTextAlignment(TextAlignment.CENTER));
                fallbackCanvas.Close();
            }
        }

        /// <remarks>
        /// Version 2.88.9 of SkiaSharp is in use, rather than the current version (3.116.1), due to the problem detailed 
        /// at https://github.com/mono/SkiaSharp/issues/2607. Deployment of version 3.116.1 fails for .NET Framework projects.
        /// </remarks>
        /// <param name="stream"></param>
        /// <returns></returns>
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
            string djvuLocalLocation = pageUrl.Split('|')[2];
            using (Stream djvu = GetDJVU(djvuLocalLocation))
            {

                // Convert the DJVU for the page into a list of words
                int sequenceOrder = Int32.Parse(pageUrl.Split('|')[3]);
                ocrWords = LoadDjvuForPage(djvu, sequenceOrder);
            }

            return ocrWords;
        }

        /// <summary>
        /// Get the contents of the DJVU file for the item
        /// </summary>
        /// <remarks>
        /// First tries reading a local file.  If not found, it reads the DJVU from Internet Archive.
        /// </remarks>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private Stream GetDJVU(string djvuLocalLocation)
        {
            Stream djvu;

            // Get the path to the local DJVU file for the item
            Item item;
            if (this.PdfRecord.BookID != null)
                item = new BooksClient(this.BHLWSUrl).GetBookFilenames((int)this.PdfRecord.BookID);
            else
                item = new SegmentsClient(this.BHLWSUrl).GetSegmentFilenames((int)this.PdfRecord.SegmentID);

            string djvuLocalPath = djvuLocalLocation + item.DjvuFilename;

            // Open the DJVU file
            if (File.Exists(djvuLocalPath))
            {
                // Open a local DJVU file
                djvu = File.Open(djvuLocalPath, FileMode.Open);
            }
            else
            {
                // Open a remote DJVU file
                string djvuPath = new ConfigurationClient(this.BHLWSUrl).GetDjvuFilePath(item.BarCode, item.DjvuFilename);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(djvuPath);
                req.Method = "GET";
                req.Timeout = 15000;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                djvu = resp.GetResponseStream();
            }

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
                          <OBJECT>
                           <HIDDENTEXT>
                            <PAGECOLUMN>
                             <REGION>
                              <PARAGRAPH>
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

        /// <summary>
        /// Draws a single OCR word onto the page as invisible text (text-rendering mode 3),
        /// positioned to align with the underlying page image. This replaces the old
        /// ColumnText-on-DirectContentUnder approach - since the text is invisible either way,
        /// it can simply be painted directly onto the page canvas.
        /// </summary>
        private void AddHiddenText(PdfCanvas canvas, PdfFont font, float fontSize,
            Tuple<string, float, float, float, float, float> ocrWord, float scaleFactor, float imageHeight)
        {
            string content = ocrWord.Item1;
            float llx = ocrWord.Item4 * scaleFactor;
            float lly = ((imageHeight - ocrWord.Item5) * scaleFactor); //- 5; // -5 adjustment to correctly align with image

            canvas.SaveState();
            canvas.BeginText()
                  .SetFontAndSize(font, fontSize)
                  .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.INVISIBLE)
                  .MoveText(llx, lly)
                  .ShowText(content)
                  .EndText();
            canvas.RestoreState();
        }

        private void AddHeaderPages(Document doc, PdfDocument pdfDoc, String fileName)
        {
            ICollection<PageSummaryView> pages = new PageSummaryViewClient(_bhlWSUrl).GetPageSummaryViewByPdf((int)this.PdfRecord.PdfID);

            if (pages.Count > 0)
            {
                PageSummaryView firstPage = ((List<PageSummaryView>)pages)[0];

                _pageLabels.Add("Title Page");
                _pageLabels.Add(" ");

                Title title = new TitlesClient(_bhlWSUrl).GetTitle((int)firstPage.TitleID);

                // Set up the fonts to be used
                HeaderFont largeFont = new HeaderFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD), 14, ColorConstants.BLACK);
                HeaderFont standardFont = new HeaderFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 12, ColorConstants.BLACK);
                HeaderFont smallFont = new HeaderFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 8, ColorConstants.BLACK);

                // Generate links
                String bhlUrl = "https://www.biodiversitylibrary.org/";
                Link bhlLink = new Link(bhlUrl, PdfAction.CreateURI(bhlUrl));
                bhlLink.GetLinkAnnotation().SetBorder(new PdfAnnotationBorder(0, 0, 0));  // Remove the border
                String titleUrl = "https://www.biodiversitylibrary.org/bibliography/" + firstPage.TitleID.ToString();
                Link titleLink = new Link(titleUrl, PdfAction.CreateURI(titleUrl));
                titleLink.GetLinkAnnotation().SetBorder(new PdfAnnotationBorder(0, 0, 0));  // Remove the border
                String itemUrl;
                if (this.PdfRecord.BookID != null)
                    itemUrl = "https://www.biodiversitylibrary.org/item/" + this.PdfRecord.BookID.ToString(); // pages[0].BookID.ToString();
                else
                    itemUrl = "https://www.biodiversitylibrary.org/segment/" + this.PdfRecord.SegmentID.ToString();
                Link itemLink = new Link(itemUrl, PdfAction.CreateURI(itemUrl));
                itemLink.GetLinkAnnotation().SetBorder(new PdfAnnotationBorder(0, 0, 0));  // Remove the border
                String pdfUrl = String.Format(this.UrlFormat, fileName + ".pdf");
                Link pdfLink = new Link(pdfUrl, PdfAction.CreateURI(pdfUrl));
                pdfLink.GetLinkAnnotation().SetBorder(new PdfAnnotationBorder(0, 0, 0));  // Remove the border

                // ---------------- First page ----------------

                // Add the BHL logo
                String appPath = System.IO.Directory.GetCurrentDirectory();
                ImageData logoImageData = ImageDataFactory.Create(appPath + "\\bhllogo.png");
                Image logoImage = new Image(logoImageData).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                doc.Add(logoImage);
                logoImage = null;

                // Add text
                this.AddParagraph(doc, TextAlignment.CENTER, standardFont, bhlLink);
                this.AddSpace(doc, standardFont);
                this.AddParagraph(doc, TextAlignment.LEFT, largeFont, firstPage.FullTitle, 60, 60, 0, 0);
                this.AddParagraph(doc, TextAlignment.LEFT, standardFont, title.PublicationDetails, 60, 60, 0, 0);
                this.AddParagraph(doc, TextAlignment.LEFT, standardFont, titleLink, 60, 60, 0, 0);
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
                            this.AddParagraph(doc, TextAlignment.LEFT, largeFont, segment.Title, 60, 60, 0, 0);
                        }
                    }
                }

                List<ILeafElement> volumeInfoParts = new List<ILeafElement>();
                if (PdfRecord.BookID != null)
                {
                    // Include the volume
                    if ((firstPage.Volume ?? "") == "")
                    {
                        volumeInfoParts.Add(MakeStyledText((_pdfRecord.BookID != null ? "Item: " : "Part: "), largeFont));
                    }
                    else
                    {
                        volumeInfoParts.Add(MakeStyledText(firstPage.Volume + ": ", largeFont));
                    }
                }
                volumeInfoParts.Add(itemLink);

                this.AddParagraph(doc, TextAlignment.LEFT, standardFont, volumeInfoParts.ToArray(), 60, 60, 0, 0);
                this.AddParagraph(doc, TextAlignment.CENTER, standardFont, " ");

                // Add article metadata, if it is available
                if (this.PdfRecord.ArticleTitle.Trim() != string.Empty) this.AddParagraph(doc, TextAlignment.LEFT, standardFont, "Article/Chapter Title: " + this.PdfRecord.ArticleTitle.Trim(), 60, 60, 0, 0);
                if (this.PdfRecord.ArticleCreators.Trim() != string.Empty) this.AddParagraph(doc, TextAlignment.LEFT, standardFont, "Author(s): " + this.PdfRecord.ArticleCreators.Trim(), 60, 60, 0, 0);
                if (this.PdfRecord.ArticleTags.Trim() != string.Empty) this.AddParagraph(doc, TextAlignment.LEFT, standardFont, "Subject(s): " + this.PdfRecord.ArticleTags.Trim(), 60, 60, 0, 0);

                // Include the list of pages
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
                this.AddParagraph(doc, TextAlignment.LEFT, standardFont, "Page(s): " + pageList, 60, 60, 0, 0);
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
                    if (institution != null) this.AddParagraph(doc, TextAlignment.LEFT, standardFont, role + ": " + institution.InstitutionName, 60, 60, 0, 0);
                    if (sponsor != String.Empty) this.AddParagraph(doc, TextAlignment.LEFT, standardFont, "Sponsored by: " + sponsor, 60, 60, 0, 0);
                    this.AddSpace(doc, standardFont);
                }

                // Add the page footer information
                this.AddParagraph(doc, TextAlignment.LEFT, smallFont,
                    "Generated " +
                    DateTime.Now.Day.ToString() + " " +
                    System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) + " " +
                    DateTime.Now.Year.ToString() + " " +
                    DateTime.Now.ToShortTimeString(), 60, 60, 0, 0);

                this.AddParagraph(doc, TextAlignment.LEFT, smallFont, pdfLink, 60, 60);

                // ---------------- Second page ----------------

                doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

                // Show the legal text if we have any, else add a blank page
                String legal = System.IO.File.ReadAllText(appPath + "\\legal.txt");
                if (legal.Length > 0)
                {
                    _pageLabels[1] = "Legal";
                    this.AddParagraph(doc, TextAlignment.LEFT, standardFont, legal, 60, 60);
                }
                else
                {
                    _pageLabels[1] = "Blank";
                    this.AddParagraph(doc, TextAlignment.CENTER, standardFont, "This page intentionally left blank.");
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

        private Text MakeStyledText(string content, HeaderFont font)
        {
            return new Text(content)
                .SetFont(font.Font)
                .SetFontSize(font.Size)
                .SetFontColor(font.Color);
        }

        private void AddParagraph(Document doc, TextAlignment alignment, HeaderFont font, String content,
            float indentationRight = 0, float indentationLeft = 0, float indentationTop = 0, 
            float indentationBottom = 8f)
        {
            Paragraph paragraph = new Paragraph(content)
                .SetMultipliedLeading(1.2f)
                .SetTextAlignment(alignment)
                .SetFont(font.Font)
                .SetFontSize(font.Size)
                .SetFontColor(font.Color)
                .SetMarginLeft(indentationLeft)
                .SetMarginRight(indentationRight)
                .SetMarginTop(indentationTop)
                .SetMarginBottom(indentationBottom);
            doc.Add(paragraph);
        }

        private void AddParagraph(Document doc, TextAlignment alignment, HeaderFont font, ILeafElement content,
            float indentationRight = 0, float indentationLeft = 0, float indentationTop = 0,
            float indentationBottom = 8f)
        {
            Paragraph paragraph = new Paragraph()
                .SetMultipliedLeading(1.2f)
                .SetTextAlignment(alignment)
                .SetFont(font.Font)
                .SetFontSize(font.Size)
                .SetFontColor(font.Color)
                .SetMarginLeft(indentationLeft)
                .SetMarginRight(indentationRight)
                .SetMarginTop(indentationTop)
                .SetMarginBottom(indentationBottom);
            paragraph.Add(content);
            doc.Add(paragraph);
        }

        private void AddParagraph(Document doc, TextAlignment alignment, HeaderFont font, ILeafElement[] parts,
            float indentationRight = 0, float indentationLeft = 0, float indentationTop = 0,
            float indentationBottom = 8f)
        {
            Paragraph paragraph = new Paragraph()
                .SetMultipliedLeading(1.2f)
                .SetTextAlignment(alignment)
                .SetFont(font.Font)
                .SetFontSize(font.Size)
                .SetFontColor(font.Color)
                .SetMarginLeft(indentationLeft)
                .SetMarginRight(indentationRight)
                .SetMarginTop(indentationTop)
                .SetMarginBottom(indentationBottom);
            foreach (ILeafElement part in parts)
            {
                paragraph.Add(part);
            }
            doc.Add(paragraph);
        }

        private void AddSpace(Document doc, HeaderFont font)
        {
            this.AddParagraph(doc, TextAlignment.CENTER, font, " ", indentationTop: 4f, indentationBottom: 8f);
            this.AddParagraph(doc, TextAlignment.CENTER, font, " ", indentationTop: 4f, indentationBottom: 8f);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MOBOT.BHL.BHLPDFGenerator.BHLWS;
using iTextSharp.text;

namespace MOBOT.BHL.BHLPDFGenerator
{
    public class PDFDocument
    {
        #region Attributes

        private Page[] _pageMetadata = null;
        private PDF _pdfRecord = null;
        public PDF PdfRecord
        {
            get { return _pdfRecord; }
            set { 
                _pdfRecord = value; 
                _pageMetadata = (new BHLWS.BHLWS()).PageMetadataSelectByItemID(_pdfRecord.ItemID);
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

        public PDFDocument(PDF pdfRecord, List<String> pageUrls, String filePathFormat, String urlFormat)
        {
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
                // Build the filename for the pdf.  Use PDFID and the ItemID
                // to construct the filename.
                // ex. 000100000001000.pdf, 000100100023546.pdf
                fileName = this.PdfRecord.PdfID.ToString().PadLeft(7, '0') +
                    this.PdfRecord.ItemID.ToString().PadLeft(8, '0');

                // Initialize the PDF document
                doc = new Document();
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc,
                    new System.IO.FileStream(String.Format(this.FilePathFormat, fileName), System.IO.FileMode.Create));

                // Add metadata if this is to be the final document
                if (this.PdfRecord.ImagesOnly)
                {
                    AddMetadata(doc);
                    writer.XmpMetadata = this.GetXmpMetadata();
                }

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
                    this.AddImageToPDF(doc, pageUrl.Split('|')[1], retryImageWait);
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

                // If requested, add the OCR to the PDF
                if (!this.PdfRecord.ImagesOnly)
                {
                    _pageLabels.Add("OCR");
                    this.AddOCRToPDF(String.Format(this.FilePathFormat, fileName));
                }
                else
                {
                    // Add PDF extension to temp file
                    if (System.IO.File.Exists(String.Format(this.FilePathFormat, fileName + ".pdf"))) System.IO.File.Delete(String.Format(this.FilePathFormat, fileName + ".pdf"));
                    System.IO.File.Move(String.Format(this.FilePathFormat, fileName), String.Format(this.FilePathFormat, fileName + ".pdf"));
                }

                fileName += ".pdf";
                this._fileName = fileName;
                this._fileLocation = String.Format(this.FilePathFormat, fileName);
                this._fileUrl = String.Format(this.UrlFormat, fileName);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (doc != null)
                {
                    // Finish writing the PDF
                    if (doc.IsOpen()) doc.Close();
                    doc = null;
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
                //doc.AddAuthor(this.PdfRecord.ArticleCreators);
                doc.AddHeader("author", this.PdfRecord.ArticleCreators);
            }
            if (this.PdfRecord.ArticleTags != String.Empty)
            {
                //doc.AddSubject(this.PdfRecord.ArticleTags);
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

                // Add Dublin Core attributes
                iTextSharp.text.xml.xmp.LangAlt dcTitle = new iTextSharp.text.xml.xmp.LangAlt();
                dcTitle.Add("x-default", this.PdfRecord.ArticleTitle);
                dc.SetProperty(iTextSharp.text.xml.xmp.DublinCoreSchema.TITLE, dcTitle);

                iTextSharp.text.xml.xmp.XmpArray dcAuthor = new iTextSharp.text.xml.xmp.XmpArray(iTextSharp.text.xml.xmp.XmpArray.ORDERED);
                String[] authors = this.PdfRecord.ArticleCreators.Split(',');
                foreach (String author in authors)
                {
                    dcAuthor.Add(author);
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
            catch (Exception ex)
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
            doc.SetPageSize(new iTextSharp.text.Rectangle(iTextSharp.text.PageSize.LETTER.Width,
                iTextSharp.text.PageSize.LETTER.Height));
            doc.SetMargins(50, 50, 50, 50);
        }

        private void AddOCRToPDF(String fileLocation)
        {
            // Create a temp PDF with the OCR pages
            this.CreateOCRPDF(fileLocation);

            // Concatenate the PDFs... first the images, then the text
            // We do this because a problem was encountered adding the text directly
            // to the "original" PDF containing the images.  An unwanted blank page 
            // was inserted between  the images and text.  An apparent iTextSharp bug.

            iTextSharp.text.pdf.PdfReader reader = null;
            Document doc = null;
            iTextSharp.text.pdf.PdfSmartCopy copy = null;

            try
            {
                reader = new iTextSharp.text.pdf.PdfReader(fileLocation);
                doc = new Document(reader.GetPageSizeWithRotation(1));
                copy = new iTextSharp.text.pdf.PdfSmartCopy(doc,
                    new System.IO.FileStream(fileLocation + ".pdf", System.IO.FileMode.Create));
                this.AddMetadata(doc);
                copy.XmpMetadata = this.GetXmpMetadata();
                doc.Open();

                // Add the images to the final PDF
                for (int x = 1; x <= reader.NumberOfPages; x++)
                {
                    iTextSharp.text.pdf.PdfImportedPage ipage = copy.GetImportedPage(reader, x);
                    copy.AddPage(ipage);
                }
                reader.Close();

                // Add the OCR to the final PDF
                reader = new iTextSharp.text.pdf.PdfReader(fileLocation + "OCR");
                for (int x = 1; x <= reader.NumberOfPages; x++)
                {
                    iTextSharp.text.pdf.PdfImportedPage ipage = copy.GetImportedPage(reader, x);
                    copy.AddPage(ipage);
                }

                // Add page labels to the final PDF
                int pageNumber = 0;
                iTextSharp.text.pdf.PdfPageLabels pdfPageLabels = new iTextSharp.text.pdf.PdfPageLabels();
                foreach (String pageLabel in _pageLabels)
                {
                    pdfPageLabels.AddPageLabel(++pageNumber, iTextSharp.text.pdf.PdfPageLabels.EMPTY, pageLabel);
                }
                pdfPageLabels.AddPageLabel(++pageNumber, iTextSharp.text.pdf.PdfPageLabels.EMPTY, "OCR");
                copy.PageLabels = pdfPageLabels;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (doc != null)
                {
                    if (doc.IsOpen()) doc.Close();
                    doc = null;
                }
                if (copy != null)
                {
                    copy.Close();
                    copy = null;
                }

                if (System.IO.File.Exists(fileLocation)) System.IO.File.Delete(fileLocation);
                if (System.IO.File.Exists(fileLocation + "OCR")) System.IO.File.Delete(fileLocation + "OCR");
            }
        }

        private void CreateOCRPDF(String fileLocation)
        {
            String fileName = String.Empty;
            iTextSharp.text.Document doc = null;

            try
            {
                fileName = fileLocation + "OCR";

                // Initialize the PDF document
                doc = new Document();
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc,
                    new System.IO.FileStream(fileName, System.IO.FileMode.Create));

                // Set up the fonts to be used
                iTextSharp.text.Font standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font boldFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                // Set margins and page size 
                SetStandardPageSize(doc);
                doc.Open();

                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, boldFont, "The following text is generated from uncorrected OCR or manual transcriptions.");

                foreach (String pageUrl in PageUrls)
                {
                    String ocrUrl = pageUrl.Split('|')[2];
                    String ocrText = String.Empty;

                    if (System.IO.File.Exists(ocrUrl))
                    {
                        ocrText = System.IO.File.ReadAllText(ocrUrl);
                    }
                    else
                    {
                        this._numberOcrMissing++;
                        ocrText = "OCR text unavailable for this page.";
                    }

                    this.AddSpace(doc, standardFont);
                    this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, boldFont, "[Begin Page: " + this.GetPageDescription(Convert.ToInt32(pageUrl.Split('|')[0])) + "]");
                    this.AddSpace(doc, standardFont);
                    this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, ocrText);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (doc != null)
                {
                    // Finish writing the PDF
                    if (doc.IsOpen()) doc.Close();
                    doc = null;
                }
            }
        }

        private void AddImageToPDF(Document pdf, String imagePath, int retryImageWait)
        {
            iTextSharp.text.Image image = null;
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
                        image = iTextSharp.text.Image.GetInstance(new Uri(imagePath));
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
                pdf.SetPageSize(new iTextSharp.text.Rectangle(imageWidth, imageHeight + 37));
                pdf.NewPage();
                pdf.Add(image);

                // This is a cludge for working around an iTextSharp bug.  The first text page after
                // a page containing only an image is blank.  To avoid this, we add this extra blank
                // "chunk" after the image.  This is also why extra space is added to the page height.
                pdf.Add(new Chunk("", new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 1, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            }
            catch (Exception ex)
            {
                if (!downloadFailed)
                {
                    // Not a download error, so we need to log it (download errors logged elsewhere)
                    this.ImageErrors.Add(string.Format(
                        "Image: {0}\r\nMessage: {1}\r\nStack Trace: {2}",
                        imagePath, ex.Message, ex.StackTrace));
                }

                // Error getting the image, add a "Page Unavailable" placeholder
                this._numberImagesMissing++;
                SetStandardPageSize(pdf);
                pdf.NewPage();
                this.AddParagraph(pdf,
                    iTextSharp.text.Element.ALIGN_CENTER,
                    new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK),
                    "Page Unavailable");
            }
            finally
            {
                image = null;
            }
        }

        private void AddHeaderPages(Document doc, String fileName)
        {
            BHLWS.BHLWS service = new MOBOT.BHL.BHLPDFGenerator.BHLWS.BHLWS();
            PageSummaryView[] pages = service.PDFPageSummaryViewSelectByPdfID(this.PdfRecord.PdfID);

            if (pages.Length > 0)
            {
                _pageLabels.Add("Title Page");
                _pageLabels.Add(" ");

                Title title = new Title();
                title = service.TitleSelectByTitleID(pages[0].TitleID);

                // Set up the fonts to be used
                iTextSharp.text.Font largeFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 14, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                iTextSharp.text.Font smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);

                // Generate links
                String bhlUrl = "https://www.biodiversitylibrary.org/";
                Anchor bhlAnchor = new Anchor(bhlUrl, standardFont);
                bhlAnchor.Reference = bhlUrl;
                String titleUrl = "https://www.biodiversitylibrary.org/bibliography/" + pages[0].TitleID.ToString();
                Anchor titleAnchor = new Anchor(titleUrl, standardFont);
                titleAnchor.Reference = titleUrl;
                String itemUrl = "https://www.biodiversitylibrary.org/item/" + pages[0].ItemID.ToString();
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
                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, largeFont, pages[0].FullTitle, 60, 60);
                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, title.PublicationDetails, 60, 60);
                this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, titleAnchor, 60, 60);
                this.AddSpace(doc, standardFont);

                // Include the volume
                Paragraph volumeInfo = new Paragraph();
                if ((pages[0].Volume ?? "") == "")
                {
                    volumeInfo.Add(new Chunk("Item: ", largeFont));
                }
                else
                {
                    volumeInfo.Add(new Chunk(pages[0].Volume + ": ", largeFont));
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
                    String pageDesc = this.GetPageDescription(page.PageID);
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
                Item item = service.ItemSelectAuto(pages[0].ItemID);
                if (item != null) sponsor = item.Sponsor;
                Institution[] institutions = service.InstitutionSelectByItemIDAndRole(pages[0].ItemID, "Holding Institution");
                
                if (institutions != null || sponsor != string.Empty)
                {
                    Institution institution = institutions[0];
                    if (institution != null) this.AddParagraph(doc, iTextSharp.text.Element.ALIGN_LEFT, standardFont, "Holding Institution: " + institution.InstitutionName, 60, 60);
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

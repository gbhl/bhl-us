using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Data.SqlClient;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class LibraryModel
    {
        #region Align Properties

        private string _message = string.Empty;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _ocrJobPath = string.Empty;

        public string OcrJobPath
        {
            get { return _ocrJobPath; }
            set { _ocrJobPath = value; }
        }

        public string AddItemType { get; set; }

        private string _addItemID = string.Empty;

        public string AddItemID
        {
            get { return _addItemID; }
            set { _addItemID = value; }
        }

        private string _addIAID = string.Empty;

        public string AddIAID
        {
            get { return _addIAID; }
            set { _addIAID = value; }
        }

        private string _addPageID = string.Empty;

        public string AddPageID
        {
            get { return _addPageID; }
            set { _addPageID = value; }
        }

        private string _addNum = string.Empty;

        public string AddNum
        {
            get { return _addNum; }
            set { _addNum = value; }
        }

        public string DelItemType { get; set; }

        private string _delItemID = string.Empty;

        public string DelItemID
        {
            get { return _delItemID; }
            set { _delItemID = value; }
        }

        private string _delIAID = string.Empty;

        public string DelIAID
        {
            get { return _delIAID; }
            set { _delIAID = value; }
        }

        private string _delPageID = string.Empty;

        public string DelPageID
        {
            get { return _delPageID; }
            set { _delPageID = value; }
        }

        private string _delNum = string.Empty;

        public string DelNum
        {
            get { return _delNum; }
            set { _delNum = value; }
        }

        public string OcrItemType { get; set; }

        private string _ocrItemID = string.Empty;

        public string OcrItemID
        {
            get { return _ocrItemID; }
            set { _ocrItemID = value; }
        }

        private string _ocrIAID = string.Empty;

        public string OcrIAID
        {
            get { return _ocrIAID; }
            set { _ocrIAID = value; }
        }

        private bool _forceOverwrite = false;

        public bool ForceOverwrite
        {
            get { return _forceOverwrite; }
            set { _forceOverwrite = value; }
        }

        #endregion Align Properties

        #region Public methods

        public void AddPagesToItem()
        {
            if (ValidateForm(this.AddItemID, this.AddIAID, this.AddPageID, this.AddNum, out string id, out string barcode))
            {
                this.AddItemID = id;
                this.AddIAID = barcode;

                try
                {
                    new BHLProvider().PageInsertIntoItem(this.AddIAID, Int32.Parse(this.AddPageID), Int32.Parse(this.AddNum));
                    this.Message = string.Format("Inserted {0} page(s) into {1} {2} ({3}).", this.AddNum, this.AddItemType, this.AddItemID, this.AddIAID);
                }
                catch (SqlException ex)
                {
                    this.Message = ex.Message;
                }
            }
        }

        public void DeletePagesFromItem()
        {
            if (ValidateForm(this.DelItemID, this.DelIAID, this.DelPageID, this.DelNum, out string id, out string barcode))
            {
                this.DelItemID = id;
                this.DelIAID = barcode;

                try
                {
                    new BHLProvider().PageDeleteFromItem(this.DelIAID, Int32.Parse(this.DelPageID), Int32.Parse(this.DelNum));
                    this.Message = string.Format("Deleted {0} page(s) from {1} {2} ({3}).", this.DelNum, this.DelItemType, this.DelItemID, this.DelIAID);
                }
                catch (SqlException ex)
                {
                    this.Message = ex.Message;
                }
            }
        }

        public bool ValidateForm(string itemIDIn, string barcodeIn, string pageIDIn, string numPages, out string idOut, out string barcodeOut)
        {
            bool isValid = true;
            idOut = string.Empty;
            barcodeOut = string.Empty;

            int itemID = -1;
            int pageID = -1;
            int addNum = -1;

            if (!Int32.TryParse(itemIDIn, out itemID) && !string.IsNullOrWhiteSpace(itemIDIn) && isValid)
            {
                this.Message = "ID must be a numeric value.";
                isValid = false;
            }
            if (!Int32.TryParse(pageIDIn, out pageID) && isValid)
            {
                this.Message = "Page ID must be a numeric value.";
                isValid = false;
            }

            if (!Int32.TryParse(numPages, out addNum) && isValid)
            {
                this.Message = "Number of Pages must be a numeric value.";
                isValid = false;
            }
            if (addNum <= 0 && isValid)
            {
                this.Message = "Number of Pages must be a positive value.";
                isValid = false;
            }

            if (isValid)
            {
                BHLProvider provider = new BHLProvider();

                if (this.AddItemType == "Book")
                {
                    Book book = null;
                    if (!string.IsNullOrWhiteSpace(barcodeIn))
                        book = provider.BookSelectByBarcodeOrItemID(null, barcodeIn);
                    else
                        book = provider.BookSelectAuto(itemID);

                    if (book == null)
                    {
                        this.Message = "Book not found.";
                        isValid = false;
                    }
                    else
                    {
                        idOut = book.BookID.ToString();
                        barcodeOut = book.BarCode;
                    }
                }
                else
                {
                    Segment segment = null;
                    if (!string.IsNullOrWhiteSpace(barcodeIn))
                        segment = provider.SegmentSelectByBarCode(barcodeIn);
                    else
                        segment = provider.SegmentSelectAuto(itemID);

                    if (segment == null)
                    {
                        this.Message = "Segment not found.";
                        isValid = false;
                    }
                    else
                    {
                        idOut = segment.SegmentID.ToString();
                        barcodeOut = segment.BarCode;
                    }
                }
            }

            return isValid;
        }

        public void CreateNewOCRJobFile()
        {
            int itemID;

            if (!Int32.TryParse(this.OcrItemID, out int id) && !string.IsNullOrWhiteSpace(this.OcrItemID))
            {
                this.Message = "ID must be a numeric value.";
                return;
            }

            BHLProvider provider = new BHLProvider();
            if (this.AddItemType == "Book")
            {
                Book book = null;
                if (!string.IsNullOrWhiteSpace(this.OcrIAID))
                    book = provider.BookSelectByBarcodeOrItemID(null, this.OcrIAID);
                else
                    book = provider.BookSelectAuto(Int32.Parse(this.OcrItemID));

                if (book == null)
                {
                    this.Message = "Book not found.";
                    return;
                }

                this.OcrItemID = book.BookID.ToString();
                this.OcrIAID = book.BarCode;
                itemID = book.ItemID;
            }
            else
            {
                Segment segment = null;
                if (!string.IsNullOrWhiteSpace(this.OcrIAID))
                    segment = provider.SegmentSelectByBarCode(this.OcrIAID);
                else
                    segment = provider.SegmentSelectAuto(Int32.Parse(this.OcrItemID));

                if (segment == null)
                {
                    this.Message = "Segment not found.";
                    return;
                }

                this.OcrItemID = segment.SegmentID.ToString();
                this.OcrIAID = segment.BarCode;
                itemID = segment.ItemID;
            }

            if (provider.PageTextLogSelectNonOCRForItem(itemID).Count > 0 && !this.ForceOverwrite)
            {
                this.Message = "Existing text is NOT derived from OCR.  Check 'Force Override' to update the text anyway.";
                return;
            }

            SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
            if (service.OcrJobExists(itemID))
            {
                this.Message = string.Format("Ocr regeneration job pending for {0} {1} ({2}).", this.OcrItemType, this.OcrItemID, this.OcrIAID);
                return;
            }

            service.OcrCreateJob(itemID);
            this.Message = string.Format("Ocr regeneration job created for {0} {1} ({2}).", this.OcrItemType, this.OcrItemID, this.OcrIAID);
        }

        #endregion Public methods

    }
}
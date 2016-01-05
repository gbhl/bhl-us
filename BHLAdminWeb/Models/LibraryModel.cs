using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        #endregion Align Properties

        #region Public methods

        public void AddPagesToItem()
        {
            int itemID = -1;
            int pageID = -1;
            int addNum = -1;

            if (!Int32.TryParse(this.AddItemID, out itemID) && !string.IsNullOrWhiteSpace(this.AddItemID))
            {
                this.Message = "Item ID must be a numeric value.";
                return;
            }
            if (!Int32.TryParse(this.AddPageID, out pageID))
            {
                this.Message = "Page ID must be a numeric value.";
                return;
            }

            if (!Int32.TryParse(this.AddNum, out addNum))
            {
                this.Message = "Number of Pages to Add must be a numeric value.";
                return;
            }
            if (addNum <= 0)
            {
                this.Message = "Number of Pages to Add must be a positive value.";
                return;
            }

            BHLProvider provider = new BHLProvider();
            Item item = null;
            if (!string.IsNullOrWhiteSpace(this.AddIAID))
            {
                item = provider.ItemSelectByBarCode(this.AddIAID);
            }
            else
            {
                item = provider.ItemSelectAuto(itemID);
            }

            if (item == null)
            {
                this.Message = "Item not found.";
                return;
            }

            this.AddItemID = item.ItemID.ToString();
            this.AddIAID = item.BarCode;

            try
            {
                provider.PageInsertIntoItem(this.AddIAID, pageID, addNum);
                this.Message = string.Format("Inserted {0} page(s) into item {1} ({2}).", addNum.ToString(),
                    itemID.ToString(), this.AddIAID);
            }
            catch (SqlException ex)
            {
                this.Message = ex.Message;
            }
        }

        public void DeletePagesFromItem()
        {
            int itemID = -1;
            int pageID = -1;
            int delNum = -1;

            if (!Int32.TryParse(this.DelItemID, out itemID) && !string.IsNullOrWhiteSpace(this.DelItemID))
            {
                this.Message = "Item ID must be a numeric value.";
                return;
            }
            if (!Int32.TryParse(this.DelPageID, out pageID))
            {
                this.Message = "Page ID must be a numeric value.";
                return;
            }

            if (!Int32.TryParse(this.DelNum, out delNum))
            {
                this.Message = "Number of Pages to Delete must be a numeric value.";
                return;
            }
            if (delNum <= 0)
            {
                this.Message = "Number of Pages to Delete must be a positive value.";
                return;
            }

            BHLProvider provider = new BHLProvider();
            Item item = null;
            if (!string.IsNullOrWhiteSpace(this.DelIAID))
            {
                item = provider.ItemSelectByBarCode(this.DelIAID);
            }
            else
            {
                item = provider.ItemSelectAuto(itemID);
            }

            if (item == null)
            {
                this.Message = "Item not found.";
                return;
            }

            this.DelItemID = item.ItemID.ToString();
            this.DelIAID = item.BarCode;

            try
            {
                provider.PageDeleteFromItem(this.DelIAID, pageID, delNum);
                this.Message = string.Format("Deleted {0} page(s) from item {1} ({2}).", delNum.ToString(),
                    itemID.ToString(), this.DelIAID);
            }
            catch (SqlException ex)
            {
                this.Message = ex.Message;
            }
        }

        public void CreateNewOCRJobFile()
        {
            int itemID = -1;

            if (!Int32.TryParse(this.OcrItemID, out itemID) && !string.IsNullOrWhiteSpace(this.OcrItemID))
            {
                this.Message = "Item ID must be a numeric value.";
                return;
            }

            BHLProvider provider = new BHLProvider();
            Item item = null;
            if (!string.IsNullOrWhiteSpace(this.OcrIAID))
            {
                item = provider.ItemSelectByBarCode(this.OcrIAID);
            }
            else
            {
                item = provider.ItemSelectAuto(itemID);
            }

            if (item == null)
            {
                this.Message = "Item not found.";
                return;
            }

            this.OcrItemID = item.ItemID.ToString();
            this.OcrIAID = item.BarCode;

            SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
            if (service.OcrJobExists(item.ItemID))
            {
                this.Message = string.Format("Ocr regeneration job pending for item {0} ({1}).", this.OcrItemID, this.OcrIAID);
                return;
            }

            service.OcrCreateJob(item.ItemID);
            this.Message = string.Format("Ocr regeneration job created for item {0} ({1}).", this.OcrItemID, this.OcrIAID);
        }

        #endregion Public methods

    }
}
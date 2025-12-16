using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        private const int BSITEMSTATUS_NEW = 10;
        private const int BSITEMSTATUS_HARVESTED = 20;
        private const int BSITEMSTATUS_PREPROCESSED = 30;
        private const int BSITEMSTATUS_PUBLISHED = 40;
        private const int BSITEMSTATUS_ITEMAVAILABLE = 90;
        private const int BSITEMSTATUS_HARVESTERROR = 91;
        private const int BSITEMSTATUS_PUBLISHERROR = 92;

        /// <summary>
        /// Update the specified item to the specified status.  If setting to NEW, then item is fully
        /// set for re-download.
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemStatusId"></param>
        /// <returns>Array of two elements. First element contains "true" or "false".  Second element 
        /// contains message detailing the success or error.
        /// </returns>
        public string[] BSItemUpdateStatus(int itemId, int itemStatusId)
        {
            string[] results = new string[2];
            string returnValue = "true";
            string returnMessage = string.Empty;

            try
            {
                BSItemDAL itemDal = new BSItemDAL();
                BSItem item = itemDal.BSItemSelectAuto(null, null, itemId);

                if (item != null)
                {
                    // Setting item for re-download
                    if (itemStatusId == BSITEMSTATUS_NEW) itemDal.BSItemDeleteAllSegments(null, null, item.ItemID);

                    item.ItemStatusID = itemStatusId;
                    itemDal.BSItemUpdateAuto(null, null, item);
                }
            }
            catch (Exception ex)
            {
                returnValue = "false";
                returnMessage = ex.Message;
            }

            results[0] = returnValue;
            results[1] = returnMessage;
            return results;
        }

        public List<BSItem> BSItemSelectByStatus(int itemStatusID, int numberOfRows, int pageNumber,
        string sortColumn, string sortDirection)
        {
            return new BSItemDAL().BSItemSelectByStatus(null, null, itemStatusID, numberOfRows, pageNumber, sortColumn, sortDirection);
        }

        public List<BSItem> SelectItemsForDownload(int? itemID)
        {
            return new BSItemDAL().BSItemSelectByItemAndStatus(null, null, itemID, BSITEMSTATUS_NEW);
        }

        public List<BSItem> SelectItemsForAuthorResolution(int? bhlItemID)
        {
            return new BSItemDAL().BSItemSelectByBHLItemAndStatus(null, null, bhlItemID, BSITEMSTATUS_HARVESTED);
        }

        public List<BSItem> SelectItemsForPublishing(int? bhlItemID)
        {     
            return new BSItemDAL().BSItemSelectByBHLItemAndStatus(null, null, bhlItemID, BSITEMSTATUS_PREPROCESSED);
        }

        /// <summary>
        /// Check NEW items to make sure that none are unavailable in production BHL.  Return a list of any that *are* unavailable.
        /// </summary>
        /// <returns></returns>
        public List<BSItem> CheckItemAvailability(int? bhlItemID)
        {
            //BHLImportEntities context = GetDataContext();
            //var unavailableItems = context.BSItemAvailabilityCheck(bhlItemID);
            //return unavailableItems.ToList();

            return new BSItemDAL().BSItemAvailabilityCheck(null, null, bhlItemID);
        }

        public int AddItem(BSItem item)
        {
            int itemID;

            // See if this BHL item is already in queue to be processed
            List<BSItem> queued = new BSItemDAL().BSItemSelectQueuedByBHLItem(null, null, item.BHLItemID);
            //var existingItem = context.BSItems.Where(i =>
            //    i.BHLItemID == item.BHLItemID && (i.ItemStatusID != BSITEMSTATUS_PUBLISHED)).Take(1);

            if (queued.Count > 0)
            {
                // If already in the queue , delete all segment records and reset to NEW
                itemID = queued.First().ItemID;
                this.DeleteAllSegmentsForItem(itemID);
                this.SetItemStatus(itemID, BSITEMSTATUS_NEW);
            }
            else
            {
                // If not already in the queue, add it
                this.SetItemDefaults(item);
                BSItem newItem = new BSItemDAL().BSItemInsertAuto(null, null, item);
                itemID = newItem.ItemID;
            }

            return itemID;
        }

        private void DeleteAllSegmentsForItem(int itemID)
        {
            new BSItemDAL().BSItemDeleteAllSegments(null, null, itemID);
        }

        /// <summary>
        /// Set the defaults for any required fields that are null.
        /// </summary>
        /// <param name="item"></param>
        private void SetItemDefaults(BSItem item)
        {
            DateTime date = DateTime.Now;
            item.ItemStatusID = (item.ItemStatusID <= 0 ? BSITEMSTATUS_NEW : item.ItemStatusID);
            item.CreationDate = (item.CreationDate == DateTime.MinValue ? date : item.CreationDate);
            item.LastModifiedDate = (item.LastModifiedDate == DateTime.MinValue ? date : item.LastModifiedDate);
        }

        public void SetItemHarvested(int itemID)
        {
            this.SetItemStatus(itemID, BSITEMSTATUS_HARVESTED);
        }

        public void SetItemPreprocessed(int itemID)
        {
            this.SetItemStatus(itemID, BSITEMSTATUS_PREPROCESSED);
        }

        public void SetItemPublished(int itemID)
        {
            this.SetItemStatus(itemID, BSITEMSTATUS_PUBLISHED);
        }

        public void SetItemHarvestError(int itemID)
        {
            this.SetItemStatus(itemID, BSITEMSTATUS_HARVESTERROR);
        }

        public void SetItemPublishError(int itemID)
        {
            this.SetItemStatus(itemID, BSITEMSTATUS_PUBLISHERROR);
        }

        private void SetItemStatus(int itemID, int itemStatusID)
        {
            BSItem item = new BSItemDAL().BSItemSelectAuto(null, null, itemID);
            if (item != null)
            {
                item.ItemStatusID = itemStatusID;
                new BSItemDAL().BSItemUpdateAuto(null, null, item);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOBOT.BHLImport.BHLImportEFDataModel;

namespace MOBOT.BHLImport.BHLImportEFDataService
{
    public partial class DataService
    {
        private const int BSITEMSTATUS_NEW = 10;
        private const int BSITEMSTATUS_HARVESTED = 20;
        private const int BSITEMSTATUS_PREPROCESSED = 30;
        private const int BSITEMSTATUS_PUBLISHED = 40;
        private const int BSITEMSTATUS_HARVESTERROR = 91;
        private const int BSITEMSTATUS_PUBLISHERROR = 92;

        public int AddItem(BSItem item)
        {
            int itemID;

            BHLImportEntities context = GetDataContext();

            // See if this BHL item is already in queue to be processed
            var existingItem = context.BSItems.Where(i => 
                i.BHLItemID == item.BHLItemID && (i.ItemStatusID != BSITEMSTATUS_PUBLISHED)).Take(1);

            if (existingItem.Count() > 0)
            {
                // If already in the queue , delete all segment records and reset to NEW
                itemID = existingItem.First().ItemID;
                this.DeleteAllSegmentsForItem(itemID);
                this.SetItemStatus(itemID, BSITEMSTATUS_NEW);
            }
            else
            {
                // If not already in the queue, add it
                this.SetItemDefaults(item);
                context.BSItems.Add(item);
                context.SaveChanges();
                itemID = item.ItemID;
            }

            return itemID;
        }

        private void DeleteAllSegmentsForItem(int itemID)
        {
            BHLImportEntities context = GetDataContext();
            context.BSItemDeleteAllSegments(itemID);
        }

        /// <summary>
        /// Check NEW items to make sure that none are unavailable in production BHL.  Return a list of any that *are* unavailable.
        /// </summary>
        /// <returns></returns>
        public List<BSItem> CheckItemAvailability(int? bhlItemID)
        {
            BHLImportEntities context = GetDataContext();
            var unavailableItems = context.BSItemAvailabilityCheck(bhlItemID);
            return unavailableItems.ToList();
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
            BHLImportEntities context = GetDataContext();
            context.BSItemSetStatus(itemID, itemStatusID);
        }

        public List<BSItem> SelectItemsForDownload(int? itemID)
        {
            BHLImportEntities context = GetDataContext();
            var items = context.BSItems.OrderBy(i => i.ItemID).Where(i => 
                (i.ItemStatusID == BSITEMSTATUS_NEW && (itemID == null || i.ItemID == itemID)));
            return items.ToList();
        }

        public List<BSItem> SelectItemsForAuthorResolution(int? bhlItemID)
        {
            BHLImportEntities context = GetDataContext();
            var items = context.BSItems.Where(i => 
                (i.ItemStatusID == BSITEMSTATUS_HARVESTED && (bhlItemID == null || i.BHLItemID == bhlItemID)));
            return items.ToList();
        }

        public List<BSItem> SelectItemsForPublishing(int? bhlItemID)
        {
            BHLImportEntities context = GetDataContext();
            var items = context.BSItems.OrderBy(i => i.ItemID).Where(i => 
                (i.ItemStatusID == BSITEMSTATUS_PREPROCESSED && (bhlItemID == null || i.BHLItemID == bhlItemID)));
            return items.ToList();
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
    }
}

using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

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
            string returnIdentifier = string.Empty;

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

        public CustomGenericList<BSItem> BSItemSelectByStatus(int itemStatusID, int numberOfRows, int pageNumber,
        string sortColumn, string sortDirection)
        {
            return new BSItemDAL().BSItemSelectByStatus(null, null, itemStatusID, numberOfRows, pageNumber, sortColumn, sortDirection);
        }
    }
}

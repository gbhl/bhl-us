using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;

namespace MOBOT.BHLImport.WebService
{
    /// <summary>
    /// Summary description for BHLImportWS
    /// </summary>
    [WebService(Namespace = "http://www.mobot.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public partial class BHLImportWS : System.Web.Services.WebService
    {

        #region Dashboard methods

        [WebMethod]
        public List<IAItem> IAItemSelectPendingApproval(int ageInDays)
        {
            return (new BHLImportProvider().IAItemSelectPendingApproval(ageInDays));
        }

        [WebMethod]
        public List<BotanicusHarvestLog> BotanicusHarvestLogSelectRecent(int numLogs)
        {
            return (new BHLImportProvider().BotanicusHarvestLogSelectRecent(numLogs));
        }

        [WebMethod]
        public List<IAItemError> IAItemErrorSelectRecent(int numErrors)
        {
            return (new BHLImportProvider().IAItemErrorSelectRecent(numErrors));
        }

        [WebMethod]
        public List<ImportError> ImportErrorSelectRecent(int numErrors)
        {
            return (new BHLImportProvider().ImportErrorSelectRecent(numErrors));
        }

        [WebMethod]
        public string[] IAItemQueueForDownload(string iaIdentifier, string localFileFolder)
        {
            return (new BHLImportProvider().IAItemQueueForDownload(iaIdentifier, localFileFolder));
        }

        [WebMethod]
        public List<IAItemStatus> IAItemStatusSelectAll()
        {
            return new BHLImportProvider().IAItemStatusSelectAll();
        }

        [WebMethod]
        public List<IAItem> IAItemSelectByStatus(int itemStatusId, int numberOfRows, int pageNumber,
            string sortColumn, string sortDirection)
        {
            return new BHLImportProvider().IAItemSelectByStatus(itemStatusId, numberOfRows, pageNumber, 
                sortColumn, sortDirection);
        }

        [WebMethod]
        public string[] IAItemUpdateStatus(int itemId, int itemStatusId)
        {
            return new BHLImportProvider().IAItemUpdateStatus(itemId, itemStatusId);
        }

        [WebMethod]
        public List<BSItemStatus> BSItemStatusSelectAll()
        {
            return new BHLImportProvider().BSItemStatusSelectAll();
        }

        [WebMethod]
        public List<BSItem> BSItemSelectByStatus(int itemStatusId, int numberOfRows, int pageNumber,
            string sortColumn, string sortDirection)
        {
            return new BHLImportProvider().BSItemSelectByStatus(itemStatusId, numberOfRows, pageNumber,
                sortColumn, sortDirection);
        }

        [WebMethod]
        public string[] BSItemUpdateStatus(int itemId, int itemStatusId)
        {
            return new BHLImportProvider().BSItemUpdateStatus(itemId, itemStatusId);
        }

        [WebMethod]
        public List<BSSegment> BSSegmentSelectByItem(int itemId)
        {
            return new BHLImportProvider().BSSegmentSelectByItem(itemId);
        }

        [WebMethod]
        public List<PageFlickrTag> PageFlickrTagSelectForPageID(int pageID)
        {
            return new BHLImportProvider().PageFlickrTagSelectForPageID(pageID);
        }

        [WebMethod]
        public void PageFlickrTagUpdateForPageID(int pageID, PageFlickrTag[] tags)
        {
            new BHLImportProvider().PageFlickrTagUpdateForPageID(pageID, tags);
        }

        [WebMethod]
        public List<PageFlickrNote> PageFlickrNoteSelectForPageID(int pageID)
        {
            return new BHLImportProvider().PageFlickrNoteSelectForPageID(pageID);
        }

        [WebMethod]
        public void PageFlickrNoteUpdateForPageID(int pageID, PageFlickrNote[] notes)
        {
            new BHLImportProvider().PageFlickrNoteUpdateForPageID(pageID, notes);
        }

        #endregion Dashboard methods
    }
}

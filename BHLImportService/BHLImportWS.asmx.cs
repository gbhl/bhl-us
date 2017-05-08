using System;
using System.Web.Services;
using System.ComponentModel;
using Config = System.Configuration;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;

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
        public CustomGenericList<IAItem> IAItemSelectPendingApproval(int ageInDays)
        {
            return (new BHLImport.Server.BHLImportProvider().IAItemSelectPendingApproval(ageInDays));
        }

        [WebMethod]
        public CustomGenericList<BotanicusHarvestLog> BotanicusHarvestLogSelectRecent(int numLogs)
        {
            return (new BHLImport.Server.BHLImportProvider().BotanicusHarvestLogSelectRecent(numLogs));
        }

        [WebMethod]
        public CustomGenericList<IAItemError> IAItemErrorSelectRecent(int numErrors)
        {
            return (new BHLImport.Server.BHLImportProvider().IAItemErrorSelectRecent(numErrors));
        }

        [WebMethod]
        public CustomGenericList<ImportError> ImportErrorSelectRecent(int numErrors)
        {
            return (new BHLImport.Server.BHLImportProvider().ImportErrorSelectRecent(numErrors));
        }

        [WebMethod]
        public CustomGenericList<ImportLog> ImportLogSelectRecent(int numLogs)
        {
            return (new BHLImport.Server.BHLImportProvider().ImportLogSelectRecent(numLogs));
        }

        [WebMethod]
        public string[] IAItemQueueForDownload(string iaIdentifier, string localFileFolder)
        {
            return (new BHLImport.Server.BHLImportProvider().IAItemQueueForDownload(iaIdentifier, localFileFolder));
        }

        [WebMethod]
        public CustomGenericList<IAItemStatus> IAItemStatusSelectAll()
        {
            return new BHLImport.Server.BHLImportProvider().IAItemStatusSelectAll();
        }

        [WebMethod]
        public CustomGenericList<IAItem> IAItemSelectByStatus(int itemStatusId, int numberOfRows, int pageNumber,
            string sortColumn, string sortDirection)
        {
            return new BHLImport.Server.BHLImportProvider().IAItemSelectByStatus(itemStatusId, numberOfRows, pageNumber, 
                sortColumn, sortDirection);
        }

        [WebMethod]
        public string[] IAItemUpdateStatus(int itemId, int itemStatusId)
        {
            return new BHLImport.Server.BHLImportProvider().IAItemUpdateStatus(itemId, itemStatusId);
        }

        [WebMethod]
        public CustomGenericList<BSItemStatus> BSItemStatusSelectAll()
        {
            return new BHLImport.Server.BHLImportProvider().BSItemStatusSelectAll();
        }

        [WebMethod]
        public CustomGenericList<BSItem> BSItemSelectByStatus(int itemStatusId, int numberOfRows, int pageNumber,
            string sortColumn, string sortDirection)
        {
            return new BHLImport.Server.BHLImportProvider().BSItemSelectByStatus(itemStatusId, numberOfRows, pageNumber,
                sortColumn, sortDirection);
        }

        [WebMethod]
        public string[] BSItemUpdateStatus(int itemId, int itemStatusId)
        {
            return new BHLImport.Server.BHLImportProvider().BSItemUpdateStatus(itemId, itemStatusId);
        }

        [WebMethod]
        public CustomGenericList<BSSegment> BSSegmentSelectByItem(int itemId)
        {
            return new BHLImport.Server.BHLImportProvider().BSSegmentSelectByItem(itemId);
        }

        [WebMethod]
        public CustomGenericList<PageFlickrTag> PageFlickrTagSelectForPageID(int pageID)
        {
            return new BHLImportProvider().PageFlickrTagSelectForPageID(pageID);
        }

        [WebMethod]
        public void PageFlickrTagUpdateForPageID(int pageID, PageFlickrTag[] tags)
        {
            new BHLImportProvider().PageFlickrTagUpdateForPageID(pageID, tags);
        }

        [WebMethod]
        public CustomGenericList<PageFlickrNote> PageFlickrNoteSelectForPageID(int pageID)
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

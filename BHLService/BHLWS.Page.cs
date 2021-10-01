using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Services;

namespace MOBOT.BHL.WebService
{
    public partial class BHLWS : System.Web.Services.WebService
    {
        #region Page Methods

        [WebMethod]
        public List<Page> PageSelectByItemID(int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageSelectByBookID(itemID);
        }

        [WebMethod]
        public List<Page> PageSelectFileNameByItemID(int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageSelectFileNameByItemID(itemID);
        }

        [WebMethod]
        public List<Page> PageMetadataSelectByItemID(int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageMetadataSelectByItemID(itemID);
        }

        [WebMethod]
        public List<Page> PageMetadataSelectBySegmentID(int segmentID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageMetadataSelectBySegmentID(segmentID);
        }

        [WebMethod]
        public List<Page> PageSelectWithExpiredPageNamesByItemID(int itemID, int maxAge)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageSelectWithExpiredPageNamesByItemID(itemID, maxAge);
        }

        [WebMethod]
        public List<Page> PageSelectWithoutPageNamesByItemID(int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageSelectWithoutPageNamesByItemID(itemID);
        }

        [WebMethod]
        public List<Page> PageSelectWithoutPageNames()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageSelectWithoutPageNames();
        }

        [WebMethod]
        public void PageUpdateYear(int[] pageIDs, string year, int userID)
        {
            BHLProvider bhlServer = new BHLProvider();
            bhlServer.PageUpdateYear(pageIDs, year, userID);
        }

        [WebMethod]
        public void PageUpdateVolume(int[] pageIDs, string volume, int userID)
        {
            BHLProvider bhlServer = new BHLProvider();
            bhlServer.PageUpdateVolume(pageIDs, volume, userID);
        }

        [WebMethod]
        public void PageUpdateIssue(int[] pageIDs, string issuePrefix, string issue, int userID)
        {
            BHLProvider bhlServer = new BHLProvider();
            bhlServer.PageUpdateIssue(pageIDs, issuePrefix, issue, userID);
        }

        [WebMethod]
        public void PageUpdateLastPageNameLookupDate(int pageID)
        {
            BHLProvider bhlServer = new BHLProvider();
            bhlServer.PageUpdateLastPageNameLookupDate(pageID);
        }

        [WebMethod]
        public bool PageCheckForOcrText(int pageID, string ocrTextLocation)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageCheckForOcrText(pageID, ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true");
        }

        #endregion Page Methods

        [WebMethod]
        public void PageTextLogInsertForItem(int itemID, string textSource, int userID)
        {
            BHLProvider bhlServer = new BHLProvider();
            bhlServer.PageTextLogInsertForItem(itemID, textSource, userID);
        }

        [WebMethod]
        public List<PageFlickr> PageFlickrSelectRandom(int numberToReturn)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageFlickrSelectRandom(numberToReturn);
        }

        [WebMethod]
        public List<PageFlickr> PageFlickrSelectAll()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageFlickrSelectAll();
        }
    }
}

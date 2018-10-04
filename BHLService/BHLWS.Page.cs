using System;
using System.Web.Services;
using System.Collections.Generic;
using System.ComponentModel;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Configuration;

namespace MOBOT.BHL.WebService
{
    public partial class BHLWS : System.Web.Services.WebService
    {
        #region Page Methods

        [WebMethod]
        public CustomGenericList<Page> PageSelectByItemID(int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageSelectByItemID(itemID);
        }

        [WebMethod]
        public CustomGenericList<Page> PageSelectFileNameByItemID(int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageSelectFileNameByItemID(itemID);
        }

        [WebMethod]
        public CustomGenericList<Page> PageMetadataSelectByItemID(int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageMetadataSelectByItemID(itemID);
        }

        [WebMethod]
        public CustomGenericList<Page> PageSelectWithExpiredPageNamesByItemID(int itemID, int maxAge)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageSelectWithExpiredPageNamesByItemID(itemID, maxAge);
        }

        [WebMethod]
        public CustomGenericList<Page> PageSelectWithoutPageNamesByItemID(int itemID)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageSelectWithoutPageNamesByItemID(itemID);
        }

        [WebMethod]
        public CustomGenericList<Page> PageSelectWithoutPageNames()
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
        public CustomGenericList<PageFlickr> PageFlickrSelectRandom(int numberToReturn)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageFlickrSelectRandom(numberToReturn);
        }

        [WebMethod]
        public CustomGenericList<PageFlickr> PageFlickrSelectAll()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PageFlickrSelectAll();
        }
    }
}

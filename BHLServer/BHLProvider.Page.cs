using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
	{
		private PageDAL pageDal = null;

		#region Select methods

		/// <summary>
		/// Select all Page objects for a particular Book ID.
		/// </summary>
		/// <param name="bookID"></param>
		/// <returns>Object of type Title.</returns>
		public List<Page> PageSelectByBookID( int bookID )
		{
			return ( new PageDAL().PageSelectByBookID( null, null, bookID ) );
		}

		public List<Page> PageSelectByItemID(int itemID)
		{
			return (new PageDAL().PageSelectByItemID(null, null, itemID));
		}

		public List<Page> PageSelectFileNameByItemID(int itemID)
        {
            return (new PageDAL().PageSelectFileNameByItemID(null, null, itemID));
        }

		public List<Page> PageMetadataSelectByItemID( int itemID )
		{
			return ( new PageDAL().PageMetadataSelectByItemID( null, null, itemID ) );
		}

		public List<Page> PageMetadataSelectBySegmentID(int segmentID)
        {
			return new PageDAL().PageMetadataSelectBySegmentID(null, null, segmentID);
		}

		public Page PageMetadataSelectByPageID(int pageID)
        {
            return (new PageDAL().PageMetadataSelectByPageID(null, null, pageID));
        }

		/// <summary>
		/// Select all Page objects for a particular Item ID that have expired Page Names.
		/// </summary>
		/// <param name="itemID"></param>
		/// <param name="maxAge"></param>
		/// <returns>List of objects of type Page.</returns>
		public List<Page> PageSelectWithExpiredPageNamesByItemID( int itemID, int maxAge )
		{
			return ( new PageDAL().PageSelectWithExpiredPageNamesByItemID( null, null, itemID, maxAge ) );
		}

		/// <summary>
		/// Select all Page objects for a particular Item ID that do not have Page Names.
		/// </summary>
		/// <param name="itemID"></param>
		/// <returns>List of objects of type Page.</returns>
		public List<Page> PageSelectWithoutPageNamesByItemID( int itemID )
		{
			return ( new PageDAL().PageSelectWithoutPageNamesForItem( null, null, itemID ) );
		}

		/// <summary>
		/// Select all Page objects that are associated with an item with Page Names, but 
		/// that do not have Page Names of their own.
		/// </summary>
		/// <returns>List of objects of type Page.</returns>
		public List<Page> PageSelectWithoutPageNames()
		{
			return ( new PageDAL().PageSelectWithoutPageNames( null, null ) );
		}

		/// <summary>
		/// Select count of all pages for a particular Item ID.
		/// </summary>
		/// <param name="itemID"></param>
		/// <returns>Count of pages</returns>
		public int PageSelectCountByItemID( int itemID )
		{
			return ( new PageDAL().PageSelectCountByItemID( null, null, itemID ) );
		}

		public Page PageSelectAuto( int pageID )
		{
			return GetPageDalInstance().PageSelectAuto( null, null, pageID );
		}

        public List<Page> PageSelectRangeForPagesAndItem(int startPageID, int endPageID, int? itemID)
        {
            return GetPageDalInstance().PageSelectRangeForPagesAndItem(null, null, startPageID, endPageID, itemID);
        }

        public List<Page> PageSelectByItemAndPageNumber(int itemID, string volume, string issue, string pageNumber)
        {
            return GetPageDalInstance().PageSelectByItemAndPageNumber(null, null, itemID, volume, issue, pageNumber);
        }

		public List<Page> PageSelectBySegmentAndPageNumber(int segmentID, string pageNumber)
		{
			return GetPageDalInstance().PageSelectBySegmentAndPageNumber(null, null, segmentID, pageNumber);
		}

		#endregion

		public Page PageUpdateYear( int pageID, string year, int userID )
		{
			// This should be done outside of this "Update" procedure.  Different "Clean"
			// processes may apply at different times.
            //year = DataCleaner.CleanYear(year);

            PageDAL dal = new PageDAL();
			Page storedPage = dal.PageSelectAuto( null, null, pageID );
			if ( storedPage != null )
			{
				storedPage.Year = year;
                storedPage.LastModifiedUserID = userID;
				storedPage.PaginationUserID = userID;
				storedPage.PaginationDate = DateTime.Now;
				storedPage = dal.PageUpdateAuto( null, null, storedPage );
			}
			else
			{
				throw new Exception( "Could not find existing page record" );
			}
			return storedPage;
		}

		public Page PageUpdateVolume( int pageID, string volume, int userID )
		{
			PageDAL dal = new PageDAL();
			Page storedPage = dal.PageSelectAuto( null, null, pageID );
			if ( storedPage != null )
			{
				storedPage.Volume = volume;
                storedPage.LastModifiedUserID = userID;
                storedPage.PaginationUserID = userID;
				storedPage.PaginationDate = DateTime.Now;
				storedPage = dal.PageUpdateAuto( null, null, storedPage );
			}
			else
			{
				throw new Exception( "Could not find existing page record" );
			}
			return storedPage;
		}

		public Page PageUpdateIssue( int pageID, string issuePrefix, string issue, int userID )
		{
			PageDAL dal = new PageDAL();
			Page storedPage = dal.PageSelectAuto( null, null, pageID );
			if ( storedPage != null )
			{
				storedPage.IssuePrefix = issuePrefix;
				storedPage.Issue = issue;
                storedPage.LastModifiedUserID = userID;
                storedPage.PaginationUserID = userID;
				storedPage.PaginationDate = DateTime.Now;
				storedPage = dal.PageUpdateAuto( null, null, storedPage );
			}
			else
			{
				throw new Exception( "Could not find existing page record" );
			}
			return storedPage;
		}

		public void PageUpdateYear( int[] pageIDs, string year, int userID )
		{
			int index = 0;
			for ( index = 0; index < pageIDs.Length; index++ )
			{
				this.PageUpdateYear( pageIDs[ index ], year, userID );
			}
		}

		public void PageUpdateVolume( int[] pageIDs, string volume, int userID )
		{
			int index = 0;
			for ( index = 0; index < pageIDs.Length; index++ )
			{
				this.PageUpdateVolume( pageIDs[ index ], volume, userID );
			}
		}

		public void PageUpdateIssue( int[] pageIDs, string issuePrefix, string issue, int userID )
		{
			int index = 0;
			for ( index = 0; index < pageIDs.Length; index++ )
			{
				this.PageUpdateIssue( pageIDs[ index ], issuePrefix, issue, userID );
			}
		}
	
		public Page PageUpdateLastPageNameLookupDate( int pageID )
		{
			return GetPageDalInstance().PageUpdateLastPageNameLookupDate( null, null, pageID );
		}

        public void PageUpdateAndLogTextChange(int pageID, string textSource, int batchID, int userID)
        {
            GetPageDalInstance().PageUpdateAndLogTextChange(null, null, pageID, textSource, batchID, userID);
        }

        private void PageSetPaginationInfo( int pageID, int userID, TransactionController transactionController )
		{
			Page page = GetPageDalInstance().PageSelectAuto( transactionController.Connection, transactionController.Transaction, pageID );
			page.PaginationUserID = userID;
			page.PaginationDate = DateTime.Now;
			pageDal.PageUpdateAuto( transactionController.Connection, transactionController.Transaction, page );
		}

		private PageDAL GetPageDalInstance()
		{
			if ( pageDal == null )
				pageDal = new PageDAL();

			return pageDal;
		}

		/// <summary>
		/// Check for existence of an OCR file for the specified page
		/// </summary>
		/// <param name="pageID"></param>
		/// <param name="ocrTextLocation"></param>
		/// <returns></returns>
		public bool PageCheckForOcrText(int pageID)
		{
			try
			{
				PageSummaryView ps = new BHLProvider().PageSummarySelectByPageId( pageID );
				if (ps == null) ps = new BHLProvider().PageSummarySegmentSelectByPageID(pageID);
				return this.GetFileAccessProvider().FileExists(ps.OcrTextLocation);
			}
			catch ( Exception ex )
			{
				throw new Exception( "Error checking for OCR file for page " + pageID + ":  " + ex.Message );
			}
		}

        public Page PageSelectFirstPageForItem(int itemID)
        {
            return GetPageDalInstance().PageSelectFirstPageForItem(null, null, itemID);
        }

        public Page PageSelectOcrPathForPageID(int pageID)
        {
            return GetPageDalInstance().PageSelectOcrPathForPageID(null, null, pageID);
        }

        public Page PageSelectExternalUrlForPageID(int pageID)
        {
            return GetPageDalInstance().PageSelectExternalUrlForPageID(null, null, pageID);
        }

        public void PageInsertIntoItem(string barCode, int pageID, int numPagesToAdd)
        {
            new PageDAL().PageInsertIntoItem(null, null, barCode, pageID, numPagesToAdd);
        }

        public void PageDeleteFromItem(string barCode, int pageID, int numPagesToDelete)
        {
            new PageDAL().PageDeleteFromItem(null, null, barCode, pageID, numPagesToDelete);
        }

        public List<PageTextLog> PageTextLogSelectForItem(int itemID)
        {
            return new PageTextLogDAL().PageTextLogSelectForItem(null, null, itemID);
        }

        public void PageTextLogInsertForItem(int itemID, string textSource, int userID)
        {
            new PageTextLogDAL().PageTextLogInsertForItem(null, null, itemID, textSource, userID);
        }

        public List<PageTextLog> PageTextLogSelectNonOCRForItem(int itemID)
        {
            return new PageTextLogDAL().PageTextLogSelectNonOCRForItem(null, null, itemID);
        }
    }
}

using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
	public partial class BHLProvider
	{
		private Page_PageTypeDAL page_pageTypeDal = null;

		private void Page_PageTypeSave( Page_PageType value, int userID, TransactionController transactionController )
		{
			GetPage_PageTypeDalInstance().Page_PageTypeSave( transactionController.Connection, 
				transactionController.Transaction, value, userID );
			PageSetPaginationInfo( value.PageID, userID, transactionController );
		}

		public void Page_PageTypeSave( int[] pageIDs, int pageTypeID, int userID )
		{
			TransactionController transactionController = new TransactionController();
			try
			{
				transactionController.BeginTransaction();
				int index = 0;
				Page_PageType ppt = new Page_PageType();
				ppt.PageTypeID = pageTypeID;
				ppt.Verified = true;

				for ( index = 0; index < pageIDs.Length; index++ )
				{
					ppt.PageID = pageIDs[ index ];
					this.Page_PageTypeSave( ppt, userID, transactionController );
				}
				transactionController.CommitTransaction();
			}
			catch
			{
				transactionController.RollbackTransaction();
			}
			finally
			{
				transactionController.Dispose();
			}
		}

		private void Page_PageTypeDeleteAllForPage( int pageID, int userID, TransactionController transactionController )
		{
			GetPage_PageTypeDalInstance().Page_PageTypeDeleteAllForPage( transactionController.Connection, transactionController.Transaction, pageID );
			PageSetPaginationInfo( pageID, userID, transactionController );
		}

		public void Page_PageTypeDeleteAllForPage( int[] pageIDs, int userID )
		{
			TransactionController transactionController = new TransactionController();
			try
			{
				transactionController.BeginTransaction();
				int index = 0;
				for ( index = 0; index < pageIDs.Length; index++ )
				{
					this.Page_PageTypeDeleteAllForPage( pageIDs[ index ], userID, transactionController );
				}
				transactionController.CommitTransaction();
			}
			catch
			{
				transactionController.RollbackTransaction();
			}
			finally
			{
				transactionController.Dispose();
			}
		}

		private Page_PageTypeDAL GetPage_PageTypeDalInstance()
		{
			if ( page_pageTypeDal == null )
				page_pageTypeDal = new Page_PageTypeDAL();

			return page_pageTypeDal;
		}

    }
}

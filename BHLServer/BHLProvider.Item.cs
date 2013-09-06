using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
	public partial class BHLProvider
	{
		#region Select methods

		public Item ItemSelectByBarcodeOrItemID( int? itemId, string barcode )
		{
			return new ItemDAL().ItemSelectByBarCodeOrItemID( null, null, itemId, barcode );
		}

		public CustomGenericList<Item> ItemSelectByTitleId( int titleID )
		{
			return new ItemDAL().ItemSelectByTitleID( null, null, titleID );
		}

        public CustomGenericList<Item> ItemSelectByMarcBibId(string marcBibId)
        {
            return new ItemDAL().ItemSelectByMarcBibId(null, null, marcBibId);
        }

		public Item ItemSelectByBarCode( string barCode )
		{
			return ( new ItemDAL().ItemSelectByBarCode( null, null, barCode ) );
		}

		public Item ItemSelectAuto( int itemID )
		{
			ItemDAL dal = new ItemDAL();
			return dal.ItemSelectAuto( null, null, itemID );
		}

		public Item ItemSelectPagination( int itemID )
		{
			return new ItemDAL().ItemSelectPagination( null, null, itemID );
		}
        
        public CustomGenericList<Item> ItemSelectRecent( int top, string languageCode, string institutionCode)
        {
            return new ItemDAL().ItemSelectRecent(null, null, top, languageCode, institutionCode);
        }

		/// <summary>
		/// Select all Items that have expired Page Names.
		/// </summary>
		/// <param name="maxAge"></param>
		/// <returns></returns>
		/// <remarks>
		/// Page Names are considered to be expired if the LastPageNameLookupDate on the
		/// Item object is older than the specified number of days.
		/// </remarks>
		public CustomGenericList<Item> ItemSelectWithExpiredPageNames( int maxAge )
		{
			return ( new ItemDAL().ItemSelectWithExpiredPageNames( null, null, maxAge ) );
		}

		/// <summary>
		/// Select all Items that do not have Page Names.
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Items are considered to not have page names if the LastPageNameLookupDate 
		/// is null.
		/// </remarks>
		public CustomGenericList<Item> ItemSelectWithoutPageNames()
		{
			return ( new ItemDAL().ItemSelectWithoutPageNames( null, null ) );
		}

		public CustomGenericList<Item> ItemSelectPaginationReport(int paginationStatusId, DateTime startDate, DateTime endDate,
            int numRows, int pageNum, string sortColumn, string sortDirection)
		{
			return new ItemDAL().ItemSelectPaginationReport( null, null, paginationStatusId, startDate, endDate, numRows,
                pageNum, sortColumn, sortDirection);
		}

		#endregion

		public Item ItemUpdateStatus( int itemID, int itemStatusID )
		{
			ItemDAL dal = new ItemDAL();
			Item item = dal.ItemSelectAuto( null, null, itemID );
			if ( item != null )
			{
				item.ItemStatusID = itemStatusID;
				item = dal.ItemUpdateAuto( null, null, item );
			}
			else
			{
				throw new Exception( "Could not find existing item record" );
			}
			return item;
		}

		public Item ItemUpdatePaginationStatus( int itemID, int paginationStatusID, int userID )
		{
			ItemDAL dal = new ItemDAL();
			Item savedItem = dal.ItemSelectAuto( null, null, itemID );
			if ( savedItem != null )
			{
				savedItem.PaginationStatusID = paginationStatusID;
				savedItem.PaginationStatusUserID = userID;
				savedItem.PaginationStatusDate = DateTime.Now;
				savedItem = dal.ItemUpdateAuto( null, null, savedItem );
			}
			else
			{
				throw new Exception( "Could not find existing Item record." );
			}
			return savedItem;
		}

		public Item ItemUpdateLastPageNameLookupDate( int itemID )
		{
			ItemDAL dal = new ItemDAL();
			return dal.ItemUpdateLastPageNameLookupDate( null, null, itemID );
		}

		/// <summary>
		/// Check for existence of OCR files in the folder for the specified item 
		/// </summary>
		/// <param name="itemID"></param>
		/// <param name="pageID"></param>
		/// <param name="ocrTextPath"></param>
		/// <returns></returns>
		public bool ItemCheckForOcrText( int itemID, string ocrTextPath )
		{
			try
			{
				PageSummaryView ps = new BHLProvider().PageSummarySelectByItemAndSequence( itemID, 1 );
				if ( ps != null )
				{
					string filepath = String.Format( ocrTextPath, ps.OCRFolderShare, ps.FileRootFolder, ps.BarCode );

                    string[] files = this.GetFileAccessProvider(true).GetFiles( filepath );
					if ( files.Length == 0 )
						return false;
					else
						return true;
				}
				else
				{
					return false;
				}
			}
			catch ( Exception ex )
			{
				throw new Exception( "Error checking for OCR files for item " + itemID + ":  " + ex.Message );
			}
		}

		public void ItemSave( Item item, int userId )
		{
			new ItemDAL().Save( null, null, item, userId );
		}

        public CustomGenericList<ItemSuspectCharacter> ItemSelectWithSuspectCharacters(String institutionCode, int maxAge)
        {
            return new ItemDAL().ItemSelectWithSuspectCharacters(null, null, institutionCode, maxAge);
        }

        public string ItemGetNamesXMLByItemID(int itemID)
        {
            return new ItemDAL().ItemGetNamesXMLByItemID(null, null, itemID);
        }

        public CustomGenericList<Item> ItemSelectByCollection(int collectionID)
        {
            return new ItemDAL().ItemSelectByCollection(null, null, collectionID);
        }

        public CustomGenericList<Item> ItemSelectPublished()
        {
            return new ItemDAL().ItemSelectPublished(null, null);
        }

        public CustomGenericList<Item> ItemSelectRecentlyChanged(string startDate)
        {
            return new ItemDAL().ItemSelectRecentlyChanged(null, null, startDate);
        }

        public CustomGenericList<NonMemberMonograph> ItemSelectNonMemberMonograph(string sinceDate, 
            int isMember, string institutionCode)
        {
            return new ItemDAL().ItemSelectNonMemberMonograph(null, null, sinceDate, isMember, institutionCode);
        }

        public CustomGenericList<Item> ItemSelectByInstitution(string institutionCode, int returnCode, string sortBy)
        {
            return new ItemDAL().ItemSelectByInstitution(null, null, institutionCode, returnCode, sortBy);
        }
    }
}

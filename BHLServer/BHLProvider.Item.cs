using System;
using System.Data;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Utility;

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

        public Item ItemSelectOAIDetail(int itemID)
        {
            return new ItemDAL().ItemSelectOAIDetail(null, null, itemID);
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

		public CustomGenericList<Item> ItemSelectPaginationReport(int publishedOnly, string institutionCode, DataTable statusIDs, 
            DateTime startDate, DateTime endDate, int numRows, int pageNum, string sortColumn, string sortDirection)
		{
			return new ItemDAL().ItemSelectPaginationReport( null, null, publishedOnly, institutionCode, statusIDs, startDate, endDate, numRows,
                pageNum, sortColumn, sortDirection);
		}

        public CustomGenericList<RISCitation> ItemSelectAllRISCitations()
        {
            return new ItemDAL().ItemSelectAllRISCitations(null, null);
        }

        public string ItemSelectRISCitationsForTitleID(int titleID)
        {
            System.Text.StringBuilder risString = new System.Text.StringBuilder("");
            CustomGenericList<RISCitation> citations = new ItemDAL().ItemSelectRISCitationsForTitleID(null, null, titleID);
            foreach (RISCitation citation in citations)
            {
                risString.Append(this.GenerateRISCitation(citation));
            }
            return risString.ToString();
        }

        public Item ItemSelectFilenames(int itemID)
        {
            return (new ItemDAL().ItemSelectFilenames(null, null, itemID));
        }

        public CustomGenericList<Item> ItemResolve(string title, string issn, string isbn, string oclc,
            string volume, string issue, string year)
        {
            return new ItemDAL().ItemResolve(null, null, title, issn, isbn, oclc, volume, issue, year);
        }

        public Item ItemSelectTextPathForItemID(int itemID)
        {
            return new ItemDAL().ItemSelectTextPathForItemID(null, null, itemID);
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
        /// Update the assigned iteminstitution of the specified item.
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="originalInstitution"></param>
        /// <param name="newInstitution"></param>
        /// <param name="roleID"></param>
        /// <returns>Array of two elements. First element contains "true" or "false".  Second element 
        /// contains message detailing the success or error.</returns>
        public string[] ItemUpdateInstitution(int itemID, string newInstitution, int roleID, int userID)
        {
            string[] results = new string[3];
            string returnValue = "true";
            string returnMessage = string.Empty;

            try
            {
                ItemDAL iDAL = new ItemDAL();
                ItemInstitutionDAL iiDAL = new ItemInstitutionDAL();

                Item savedItem = iDAL.ItemSelectAuto(null, null, itemID);
                ItemInstitution savedItemInstitution = iiDAL.ItemInstitutionSelectByItemAndRole(null, null, itemID, roleID);
                if (savedItem != null)
                {
                    DateTime updateDate = DateTime.Now;

                    if (savedItemInstitution == null)
                    {
                        // Role not assigned previously, so add a new record
                        ItemInstitution newItemInstitution = new ItemInstitution();
                        newItemInstitution.ItemID = itemID;
                        newItemInstitution.InstitutionCode = newInstitution;
                        newItemInstitution.InstitutionRoleID = roleID;
                        newItemInstitution.CreationDate = updateDate;
                        newItemInstitution.CreationUserID = userID;
                        newItemInstitution.LastModifiedDate = updateDate;
                        newItemInstitution.LastModifiedUserID = userID;
                        iiDAL.ItemInstitutionInsertAuto(null, null, newItemInstitution);
                    }
                    else if (newInstitution == string.Empty)
                    {
                        // Setting new role to blank, so delete existing record
                        iiDAL.ItemInstitutionDeleteAuto(null, null, savedItemInstitution.ItemInstitutionID);
                    }
                    else
                    {
                        // Changing role to new value, so update existing record
                        savedItemInstitution.InstitutionCode = newInstitution;
                        savedItemInstitution.LastModifiedDate = updateDate;
                        savedItemInstitution.LastModifiedUserID = userID;
                        iiDAL.ItemInstitutionUpdateAuto(null, null, savedItemInstitution);
                    }

                    savedItem.LastModifiedDate = updateDate;
                    savedItem.LastModifiedUserID = userID;
                    savedItem = iDAL.ItemUpdateAuto(null, null, savedItem);
                }
                else
                {
                    throw new Exception("Could not find existing Item record.");
                }
            }
            catch (Exception ex)
            {
                returnValue = "false";
                returnMessage = ex.Message;
            }

            results[0] = returnValue;
            results[1] = returnMessage;
            results[2] = itemID.ToString();
            return results;
        }

        /// <summary>
        /// Check for existence of OCR files in the folder for the specified item 
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="pageID"></param>
        /// <param name="ocrTextPath"></param>
        /// <returns></returns>
        public bool ItemCheckForOcrText( int itemID, string ocrTextPath, bool useRemoteProvider )
		{
			try
			{
				PageSummaryView ps = new BHLProvider().PageSummarySelectByItemAndSequence( itemID, 1 );
				if ( ps != null )
				{
					string filepath = String.Format( ocrTextPath, ps.OCRFolderShare, ps.FileRootFolder, ps.BarCode );

                    string[] files = this.GetFileAccessProvider(useRemoteProvider).GetFiles( filepath );
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
            item.Year = DataCleaner.CleanYear(item.Year);
            item.EndYear = DataCleaner.CleanYear(item.EndYear);

            // Parse the volume into its component parts.
            // NOTE: Once a UI for the component parts of the volume string is available, the parsing should probably be removed from here.
            VolumeData volumeData = DataCleaner.ParseVolumeString(item.Volume);
            item.Year = string.IsNullOrWhiteSpace(item.Year) && string.IsNullOrWhiteSpace(item.EndYear) ? volumeData.StartYear : item.Year;
            item.EndYear = string.IsNullOrWhiteSpace(item.Year) && string.IsNullOrWhiteSpace(item.EndYear) ?  volumeData.EndYear : item.EndYear;
            item.StartVolume = volumeData.StartVolume;
            item.EndVolume = volumeData.EndVolume;
            item.StartIssue = volumeData.StartIssue;
            item.EndIssue = volumeData.EndIssue;
            item.StartPart = volumeData.StartPart;
            item.EndPart = volumeData.EndPart;
            item.StartNumber = volumeData.StartNumber;
            item.EndNumber = volumeData.EndNumber;
            item.StartSeries = volumeData.StartSeries;
            item.EndSeries = volumeData.EndSeries;

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

        public CustomGenericList<Item> ItemSelectByInstitutionAndRole(string institutionCode, int institutionRoleID, string barcode, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            return new ItemDAL().ItemSelectByInstitutionAndRole(null, null, institutionCode, institutionRoleID, barcode, numRows, pageNum, sortColumn, sortOrder);
        }

        public int ItemCountByInstitution(string institutionCode)
        {
            return new ItemDAL().ItemCountByInstitution(null, null, institutionCode);
        }

        public CustomGenericList<Item> ItemSelectBarcodes()
        {
            return new ItemDAL().ItemSelectBarcodes(null, null);
        }

        public CustomGenericList<Item> ItemInFlickrByTitleID(int titleId)
        {
            return new ItemDAL().ItemInFlickrByTitleID(null, null, titleId);
        }

        public Item ItemInFlickrByItemID(int itemId)
        {
            return new ItemDAL().ItemInFlickrByItemID(null, null, itemId);
        }
    }
}

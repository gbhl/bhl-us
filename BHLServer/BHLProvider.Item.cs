using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using System;
using System.Collections.Generic;
using System.Data;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
	{
		#region Select methods

		public Book BookSelectByBarcodeOrItemID( int? itemId, string barcode )
		{
			return new BookDAL().BookSelectByBarCodeOrItemID( null, null, itemId, barcode );
		}

        public List<Book> BookSelectByMarcBibId(string marcBibId)
        {
            return new BookDAL().BookSelectByMarcBibId(null, null, marcBibId);
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

		/// <summary>
		/// Select all Items that have expired Page Names.
		/// </summary>
		/// <param name="maxAge"></param>
		/// <returns></returns>
		/// <remarks>
		/// Page Names are considered to be expired if the LastPageNameLookupDate on the
		/// Item object is older than the specified number of days.
		/// </remarks>
		public List<Item> ItemSelectWithExpiredPageNames( int maxAge )
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
		public List<Item> ItemSelectWithoutPageNames()
		{
			return ( new ItemDAL().ItemSelectWithoutPageNames( null, null ) );
		}

		public List<Item> ItemSelectPaginationReport(int publishedOnly, string institutionCode, DataTable statusIDs, 
            DateTime startDate, DateTime endDate, int numRows, int pageNum, string sortColumn, string sortDirection)
		{
			return new ItemDAL().ItemSelectPaginationReport( null, null, publishedOnly, institutionCode, statusIDs, startDate, endDate, numRows,
                pageNum, sortColumn, sortDirection);
		}

        public List<RISCitation> ItemSelectAllRISCitations()
        {
            return new ItemDAL().ItemSelectAllRISCitations(null, null);
        }

        public string ItemSelectRISCitationsForTitleID(int titleID)
        {
            System.Text.StringBuilder risString = new System.Text.StringBuilder("");
            List<RISCitation> citations = new ItemDAL().ItemSelectRISCitationsForTitleID(null, null, titleID);
            foreach (RISCitation citation in citations)
            {
                risString.Append(this.GenerateRISCitation(citation));
            }
            return risString.ToString();
        }

        public Item ItemSelectFilenames(ItemType itemType, int entityID)
        {
            if (itemType == ItemType.Book)
            {
                return (new ItemDAL().ItemSelectFilenames(null, null, entityID));
            }
            else
            {
                return new ItemDAL().ItemSelectFilenamesBySegmentID(null, null, entityID);
            }
        }

        public List<Item> ItemResolve(string title, string issn, string isbn, string oclc,
            string volume, string issue, string year)
        {
            return new ItemDAL().ItemResolve(null, null, title, issn, isbn, oclc, volume, issue, year);
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
                if (ps == null) ps = new BHLProvider().PageSummarySegmentSelectByItemAndSequence(itemID, 1);
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

        public List<ItemSuspectCharacter> ItemSelectWithSuspectCharacters(String institutionCode, int maxAge)
        {
            return new ItemDAL().ItemSelectWithSuspectCharacters(null, null, institutionCode, maxAge);
        }

        public string ItemGetNamesXMLByItemID(int itemID, string barcode)
        {
            return new ItemDAL().ItemGetNamesXMLByItemID(null, null, itemID, barcode);
        }

        public List<Item> ItemSelectPublished()
        {
            return new ItemDAL().ItemSelectPublished(null, null);
        }

        public List<NonMemberMonograph> ItemSelectNonMemberMonograph(string sinceDate, 
            int isMember, string institutionCode)
        {
            return new ItemDAL().ItemSelectNonMemberMonograph(null, null, sinceDate, isMember, institutionCode);
        }

        public int ItemCountByInstitution(string institutionCode)
        {
            return new ItemDAL().ItemCountByInstitution(null, null, institutionCode);
        }

        public List<Item> ItemSelectBarcodes()
        {
            return new ItemDAL().ItemSelectBarcodes(null, null);
        }

        public List<Item> ItemInFlickrByTitleID(int titleId)
        {
            return new ItemDAL().ItemInFlickrByTitleID(null, null, titleId);
        }

        public Item ItemInFlickrByItemID(int itemId)
        {
            return new ItemDAL().ItemInFlickrByItemID(null, null, itemId);
        }
    }
}

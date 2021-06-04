
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class PageDAL
	{
		#region Select methods

		/// <summary>
		/// Select all Page objects for a particular Item ID.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID">Item identifier</param>
		/// <returns>Object of type Title.</returns>
		public List<Page> PageSelectByItemID(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction,
				int itemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageSelectByItemID", connection, transaction,
					CustomSqlHelper.CreateInputParameter( "ItemID", SqlDbType.Int, null, false, itemID ) ) )
			{
				using ( CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>() )
				{
					List<Page> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		public List<Page> PageMetadataSelectByItemID(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction,
				int itemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageMetadataSelectByItemID", connection, transaction,
					CustomSqlHelper.CreateInputParameter( "ItemID", SqlDbType.Int, null, false, itemID ) ) )
			{
				using ( CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>() )
				{
					List<Page> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		public Page PageMetadataSelectByPageID( SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageMetadataSelectByPageID", connection, transaction,
					CustomSqlHelper.CreateInputParameter( "PageID", SqlDbType.Int, null, false, pageID ) ) )
			{
				using ( CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>() )
				{
					List<Page> list = helper.ExecuteReader( command );
					if ( list.Count > 0 )
						return list[ 0 ];
					else
						return null;
				}
			}
		}

		/// <summary>
		/// Select the count of all pages for a particular Item ID.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID">Item identifier</param>
		/// <returns>Count of Pages</returns>
		public int PageSelectCountByItemID(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction,
				int itemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageSelectCountByItemID", connection, transaction,
					CustomSqlHelper.CreateInputParameter( "ItemID", SqlDbType.Int, null, false, itemID ),
                    CustomSqlHelper.CreateReturnValueParameter("Count", SqlDbType.Int, null, false) ) )
			{
				int k = (int)CustomSqlHelper.ExecuteScalar(command);
                return k;
			}
		}

		/// <summary>
		/// Select all Page objects for a particular Item ID that have expired Page Names.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID">Item identifier</param>
		/// <param name="maxAge">Maximum age (in days) of page names</param>
		/// <returns>List of objects of type Page.</returns>
		/// <remarks>
		/// Page Names are considered to be expired if the LastPageNameLookupDate on the
		/// Page object is older than the specified number of days.
		/// </remarks>
		public List<Page> PageSelectWithExpiredPageNamesByItemID(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction,
				int itemID,
				int maxAge )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageSelectWithExpiredPageNamesByItemID", connection, transaction,
					CustomSqlHelper.CreateInputParameter( "ItemID", SqlDbType.Int, null, false, itemID ),
					CustomSqlHelper.CreateInputParameter( "MaxAge", SqlDbType.Int, null, false, maxAge ) ) )
			{
				using ( CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>() )
				{
					List<Page> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		/// <summary>
		/// Select all Page objects for a particular Item ID that do not have Page Names.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null</param>
		/// <param name="sqlTransaction">Sql transaction or null</param>
		/// <param name="itemID">Item identifier</param>
		/// <returns>List of objects of type Page.</returns>
		/// <remarks>
		/// Pages are considered to not have page names if the LastPageNameLookupDate 
		/// is null.
		/// </remarks>
		public List<Page> PageSelectWithoutPageNamesForItem(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction,
				int itemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageSelectWithoutPageNamesByItemID", connection, transaction,
					CustomSqlHelper.CreateInputParameter( "ItemID", SqlDbType.Int, null, false, itemID ) ) )
			{
				using ( CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>() )
				{
					List<Page> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

        public List<Page> PageSelectFileNameByItemID(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSelectFileNameByItemID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
                {
                    List<Page> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
		/// Select all Page objects that are associated with an item with Page Names, but 
		/// that do not have Page Names of their own.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null</param>
		/// <param name="sqlTransaction">Sql transaction or null</param>
		/// <returns>List of objects of type Page.</returns>
		/// <remarks>
		/// Pages are considered to not have page names if the LastPageNameLookupDate 
		/// is null.
		/// </remarks>
		public List<Page> PageSelectWithoutPageNames(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageSelectWithoutPageNames", connection, transaction ) )
			{
				using ( CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>() )
				{
					List<Page> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

        /// <summary>
        /// Return a list of all pages that fall between the start and end page.  If the start and end pages
        /// are not part of the specified item, or do not belong to the same item, nothing is returned.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="startPageID"></param>
        /// <param name="endPageID"></param>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public List<Page> PageSelectRangeForPagesAndItem(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, int startPageID, int endPageID, int? itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.PageSelectRangeForPagesAndItem", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("StartPageID", SqlDbType.Int, null, false, startPageID),
                    CustomSqlHelper.CreateInputParameter("EndPageID", SqlDbType.Int, null, false, endPageID),
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID)))
            {
                using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
                {
                    List<Page> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Return the page IDs that match the specified Item and Page Number.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="itemID"></param>
        /// <param name="volume"></param>
        /// <param name="issue"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public List<Page> PageSelectByItemAndPageNumber(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID, string volume, string issue, string pageNumber)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.PageSelectByItemAndPageNumber", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                    CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 20, false, volume),
                    CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 10, false, issue),
                    CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, false, pageNumber)))
            {
                using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
                {
                    List<Page> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Return the page IDs that match the specified Segment and Page Number.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="segmentID"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public List<Page> PageSelectBySegmentAndPageNumber(SqlConnection sqlConnection,SqlTransaction sqlTransaction, int segmentID, string pageNumber)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.PageSelectBySegmentAndPageNumber", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                    CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, false, pageNumber)))
            {
                using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
                {
                    List<Page> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        #endregion

        /// <summary>
        /// Update the LastPageNameLookupDate for the specified Page.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null</param>
        /// <param name="sqlTransaction">Sql transaction or null</param>
        /// <param name="pageID">Identifier of a specific page</param>
        /// <returns>The updated page</returns>
        public Page PageUpdateLastPageNameLookupDate( SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( 
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageUpdateLastPageNameLookupDate", connection, 
				transaction,
				CustomSqlHelper.CreateInputParameter( "PageID", SqlDbType.Int, null, false, pageID ) ) )
			{
				using ( CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>() )
				{
					List<Page> list = helper.ExecuteReader( command );
					if ( list.Count > 0 )
						return list[ 0 ];
					else
						return null;
				}
			}
		}

        public void PageUpdateAndLogTextChange(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int pageID, string textSource, int textImportBatchID, int userID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.PageUpdateAndLogTextChange", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
                CustomSqlHelper.CreateInputParameter("TextSource", SqlDbType.NVarChar, 50, false, textSource),
                CustomSqlHelper.CreateInputParameter("BatchID", SqlDbType.Int, null, false, textImportBatchID),
                CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID)))
            {
                using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
                {
                    List<Page> list = helper.ExecuteReader(command);
                }
            }
        }

        public Page PageSelectFirstPageForItem(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSelectFirstPageForItem", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
                {
                    List<Page> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public Page PageSelectOcrPathForPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSelectOcrPathForPageID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
                {
                    List<Page> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public Page PageSelectExternalUrlForPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSelectExternalUrlForPageID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
                {
                    List<Page> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public void PageInsertIntoItem(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            string barCode, int pageID, int numPagesToAdd)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageInsertIntoItem", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Barcode", SqlDbType.NVarChar, 200, false, barCode),
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
                    CustomSqlHelper.CreateInputParameter("NumPagesToAdd", SqlDbType.Int, null, false, numPagesToAdd)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void PageDeleteFromItem(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string barCode, int pageID, int numPagesToDelete)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageDeleteFromItem", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Barcode", SqlDbType.NVarChar, 200, false, barCode),
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
                    CustomSqlHelper.CreateInputParameter("NumPagesToDelete", SqlDbType.Int, null, false, numPagesToDelete)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }
    }
}

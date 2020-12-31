using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class ItemDAL
	{
		#region Select methods

		/// <summary>
		/// Select values from Item by barcode.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="barCode">Unique barcode for Item record.</param>
		/// <returns>Object of type Item.</returns>
		public Item ItemSelectByBarCode(
			SqlConnection sqlConnection,
			SqlTransaction sqlTransaction,
			string barCode )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "ItemSelectByBarCode", connection, transaction,
							CustomSqlHelper.CreateInputParameter( "BarCode", SqlDbType.NVarChar, 40, false, barCode ) ) )
			{
				using ( CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>() )
				{
					List<Item> list = helper.ExecuteReader( command );
					if ( list.Count > 0 )
					{
						return list[ 0 ];
					}
					else
					{
						return null;
					}
				}
			}
		}

		/// <summary>
		/// Select all Items that have expired Page Names.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="maxAge">Maximum age (in days) of Page Names.</param>
		/// <returns>List of objects of type Item.</returns>
		/// <remarks>
		/// Page Names are considered to be expired if the LastPageNameLookupDate on the
		/// Item object is older than the specified number of days.
		/// </remarks>
		public List<Item> ItemSelectWithExpiredPageNames(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction,
				int maxAge )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "ItemSelectWithExpiredPageNames", connection, transaction,
					CustomSqlHelper.CreateInputParameter( "MaxAge", SqlDbType.Int, null, false, maxAge ) ) )
			{
				using ( CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>() )
				{
					List<Item> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		/// <summary>
		/// Select all Items that do not have Page Names.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <returns>List of objects of type Item.</returns>
		/// <remarks>
		/// Items are considered to not have page names if the LastPageNameLookupDate 
		/// is null.
		/// </remarks>
		public List<Item> ItemSelectWithoutPageNames(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "ItemSelectWithoutPageNames", connection, transaction ) )
			{
				using ( CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>() )
				{
					List<Item> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		public List<Item> ItemSelectPaginationReport( SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, int publishedOnly, string institutionCode, DataTable statusIDs, 
            DateTime startDate, DateTime endDate, int numRows, int pageNum, string sortColumn, string sortDirection)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( 
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.ItemSelectPaginationReport", connection, transaction))
            {
                command.Parameters.Add(CustomSqlHelper.CreateInputParameter("PublishedOnly", SqlDbType.Int, null, false, publishedOnly));
                command.Parameters.Add(CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode));
                SqlParameter idListParam = command.Parameters.AddWithValue("@StatusIDList", statusIDs);
                idListParam.SqlDbType = SqlDbType.Structured;
                idListParam.TypeName = "dbo.IDListInt";
                //command.Parameters.Add((CustomSqlHelper.CreateInputParameter("StatusIDList", SqlDbType.Structured, null, false, statusIDs).TypeName = "dbo.IDListInt"));
                command.Parameters.Add(CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, false, startDate));
                command.Parameters.Add(CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.DateTime, null, false, endDate));
                command.Parameters.Add(CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numRows));
                command.Parameters.Add(CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum));
                command.Parameters.Add(CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn));
                command.Parameters.Add(CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortDirection));

                using ( CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>() )
				{
					List<Item> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

        /// <summary>
        /// Select data for RIS citations for all items.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of type RISCitation.</returns>
        public List<RISCitation> ItemSelectAllRISCitations(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectAllRISCitations", connection, transaction))
            {
                using (CustomSqlHelper<RISCitation> helper = new CustomSqlHelper<RISCitation>())
                {
                    List<RISCitation> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select data for all item RIS citations for the specified title.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of type RISCitation.</returns>
        public List<RISCitation> ItemSelectRISCitationsForTitleID(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectRISCitationsForTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<RISCitation> helper = new CustomSqlHelper<RISCitation>())
                {
                    List<RISCitation> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Find item, given some limited metadata.  Used during the ImportRecord process.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="title"></param>
        /// <param name="issn"></param>
        /// <param name="isbn"></param>
        /// <param name="oclc"></param>
        /// <param name="doi"></param>
        /// <param name="volume"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<Item> ItemResolve(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string issn, string isbn, string oclc, string volume, string issue, string year)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ItemResolve", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("JournalTitle", SqlDbType.NVarChar, 2000, false, title),
                    CustomSqlHelper.CreateInputParameter("ISSNValue", SqlDbType.NVarChar, 2000, false, issn),
                    CustomSqlHelper.CreateInputParameter("ISBNValue", SqlDbType.NVarChar, 2000, false, isbn),
                    CustomSqlHelper.CreateInputParameter("OCLCValue", SqlDbType.NVarChar, 2000, false, oclc),
                    CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 2000, false, volume),
                    CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 2000, false, issue),
                    CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 2000, false, year)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    List<Item> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        #endregion

        /// <summary>
        /// Update the LastPageNameLookupDate for the specified Item.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null</param>
        /// <param name="sqlTransaction">Sql transaction or null</param>
        /// <param name="itemID">Identifier of a specific item</param>
        /// <returns>The updated item</returns>
        public Item ItemUpdateLastPageNameLookupDate( SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "ItemUpdateLastPageNameLookupDate", connection,
				transaction,
				CustomSqlHelper.CreateInputParameter( "ItemID", SqlDbType.Int, null, false, itemID ) ) )
			{
				using ( CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>() )
				{
					List<Item> list = helper.ExecuteReader( command );
					if ( list.Count > 0 )
						return list[ 0 ];
					else
						return null;
				}
			}
		}

        public Item ItemUpdatePrimaryTitleID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemUpdatePrimaryTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    List<Item> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Item item, int userId)
		{
			SqlConnection connection = sqlConnection;
			SqlTransaction transaction = sqlTransaction;

			if ( connection == null )
			{
				connection =
					CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ) );
			}

			bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator( transaction );

            try
            {
                transaction = CustomSqlHelper.BeginTransaction(connection, transaction, isTransactionCoordinator);

                CustomDataAccessStatus<Item> updatedItem =
                    new ItemDAL().ItemManageAuto(connection, transaction, item, userId);

                if (item.Institutions.Count > 0)
                {
                    ItemInstitutionDAL itemInstitutionDAL = new ItemInstitutionDAL();
                    foreach (Institution institution in item.Institutions)
                    {
                        if (institution.IsDeleted)
                        {
                            itemInstitutionDAL.ItemInstitutionDeleteAuto(connection, transaction, (int)institution.EntityInstitutionID);
                        }
                        if (institution.IsNew)
                        {
                            itemInstitutionDAL.ItemInstitutionInsert(connection, transaction, updatedItem.ReturnObject.ItemID, 
                                institution.InstitutionCode, institution.InstitutionRoleName, userId);
                        }
                    }
                }

				if ( item.Pages.Count > 0 )
				{
					PageDAL pageDAL = new PageDAL();
					foreach ( Page page in item.Pages )
					{
						pageDAL.PageManageAuto( connection, transaction, page, userId );
					}
				}

                if (item.ItemTitles.Count > 0)
                {
                    ItemTitleDAL itemTitleDAL = new ItemTitleDAL();
                    foreach (ItemTitle itemTitle in item.ItemTitles)
                    {
                        itemTitleDAL.ItemTitleManageAuto(connection, transaction, itemTitle, userId);
                    }
                }

                if (item.ItemLanguages.Count > 0)
                {
                    ItemLanguageDAL itemLanguageDAL = new ItemLanguageDAL();
                    foreach (ItemLanguage itemLanguage in item.ItemLanguages)
                    {
                        itemLanguageDAL.ItemLanguageManageAuto(connection, transaction, itemLanguage, userId);
                    }
                }

                if (item.ItemCollections.Count > 0)
                {
                    ItemCollectionDAL itemCollectionDAL = new ItemCollectionDAL();
                    foreach (ItemCollection itemCollection in item.ItemCollections)
                    {
                        if (itemCollection.ItemID == 0) itemCollection.ItemID = updatedItem.ReturnObject.ItemID;
                        itemCollectionDAL.ItemCollectionManageAuto(connection, transaction, itemCollection);
                    }
                }

                if (item.Segments.Count > 0)
                {
                    SegmentDAL segmentDAL = new SegmentDAL();
                    foreach (Segment segment in item.Segments)
                    {
                        segmentDAL.SegmentManageAuto(connection, transaction, segment, userId);
                    }
                }

				CustomSqlHelper.CommitTransaction( transaction, isTransactionCoordinator );
			}
			catch ( Exception ex )
			{
				CustomSqlHelper.RollbackTransaction( transaction, isTransactionCoordinator );

				throw;
			}
			finally
			{
				CustomSqlHelper.CloseConnection( connection, isTransactionCoordinator );
			}
		}

        /// <summary>
        /// Returns a list of items that have suspected character encoding problems.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="institutionCode">Institution for which to return items</param>
        /// <param name="maxAge">Age in days of items to consider (i.e. items new in the last 30 days)</param>
        /// <returns></returns>
        public List<ItemSuspectCharacter> ItemSelectWithSuspectCharacters(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                String institutionCode,
                int maxAge)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectWithSuspectCharacters", connection, transaction,
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                CustomSqlHelper.CreateInputParameter("MaxAge", SqlDbType.Int, null, false, maxAge)))
            {
                using (CustomSqlHelper<ItemSuspectCharacter> helper = new CustomSqlHelper<ItemSuspectCharacter>())
                {
                    List<ItemSuspectCharacter> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public string ItemGetNamesXMLByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemGetNamesXMLByItemID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateReturnValueParameter("Count", SqlDbType.Int, null, false)))
            {
                return CustomSqlHelper.ExecuteScalar(command).ToString();
            }
        }

        /// <summary>
        /// Select all items that have been published on BHL (Item.ItemStatusID = 40 and Title.PublishReady = 1)
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        public List<Item> ItemSelectPublished(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectPublished", connection, transaction))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    List<Item> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<NonMemberMonograph> ItemSelectNonMemberMonograph(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string sinceDate,
            int isMember,
            string institutionCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectNonMemberMonograph", connection, transaction,
                 CustomSqlHelper.CreateInputParameter("SinceDate", SqlDbType.DateTime, null, false, sinceDate),
                 CustomSqlHelper.CreateInputParameter("IsMember", SqlDbType.Int, null, false, isMember),
                 CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode)
                ))
            {
                using (CustomSqlHelper<NonMemberMonograph> helper = new CustomSqlHelper<NonMemberMonograph>())
                {
                    List<NonMemberMonograph> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public int ItemCountByInstitution(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string institutionCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemCountByInstitution", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode)))
            {
                return (int)CustomSqlHelper.ExecuteScalar(command);
            }
        }

        public List<Item> ItemSelectBarcodes(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ExportIAIdentifiers", connection, transaction))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    List<Item> barcodes = helper.ExecuteReader(command);
                    return barcodes;
                }
            }
        }

        /// <summary>
        /// Get a list of item IDs for the specified title, including an indicator of whether there
        /// are pages from each item in the BHL Flickr collection.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="titleId"></param>
        /// <returns></returns>
        public List<Item> ItemInFlickrByTitleID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int titleId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInFlickrByTitleId", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleId", SqlDbType.Int, null, false, titleId)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    List<Item> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Get an indicator of whether there are pages in the BHL Flickr collection for the specified item.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Item ItemInFlickrByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int itemId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInFlickrByItemId", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemId", SqlDbType.Int, null, false, itemId)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    List<Item> list = helper.ExecuteReader(command);
                    if (list == null || list.Count == 0)
                        return null;
                    else
                        return list[0];
                }
            }
        }

        public Item ItemSelectFilenames(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int itemId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectFilenames", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemId", SqlDbType.Int, null, false, itemId)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    List<Item> list = helper.ExecuteReader(command);
                    if (list == null || list.Count == 0)
                        return null;
                    else
                        return list[0];
                }
            }
        }
    }
}

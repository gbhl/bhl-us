using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class ItemDAL
	{
		#region Select methods

		public Item ItemSelectByBarCodeOrItemID( SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int? itemId, string barCode )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "ItemSelectByBarCodeOrItemID", connection, transaction,
				CustomSqlHelper.CreateInputParameter( "ItemID", SqlDbType.Int, null, true, itemId ),
				CustomSqlHelper.CreateInputParameter( "BarCode", SqlDbType.NVarChar, 40, true, barCode ) ) )
			{
				using ( CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>() )
				{
					CustomGenericList<Item> list = helper.ExecuteReader( command );
					if ( list.Count > 0 )
					{
						Item item = (Item)list[ 0 ];
						item.Pages = new PageDAL().PageSelectByItemID( connection, transaction, item.ItemID );
                        //item.Titles = new TitleDAL().TitleSelectByItem(connection, transaction, item.ItemID);
                        item.TitleItems = new TitleItemDAL().TitleItemSelectByItem(connection, transaction, item.ItemID);
                        item.ItemLanguages = new ItemLanguageDAL().ItemLanguageSelectByItemID(connection, transaction, item.ItemID);
                        item.ItemCollections = new ItemCollectionDAL().SelectByItem(connection, transaction, item.ItemID);
                        item.Segments = new SegmentDAL().SegmentSelectByItemID(connection, transaction, item.ItemID, 1);
                        return item;
					}
					else
					{
						return null;
					}
				}
			}
		}

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
					CustomGenericList<Item> list = helper.ExecuteReader( command );
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
		/// Select all Items for a particular Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <returns>Object of type Title.</returns>
		public CustomGenericList<Item> ItemSelectByTitleID(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction,
				int titleID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( 
        CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "ItemSelectByTitleID", connection, transaction,
					CustomSqlHelper.CreateInputParameter( "TitleID", SqlDbType.Int, null, false, titleID ) ) )
			{
				using ( CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>() )
				{
					CustomGenericList<Item> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		// This does not filter on item status
		public CustomGenericList<Item> ItemSelectByMarcBibId( SqlConnection sqlConnection,	SqlTransaction sqlTransaction,
			string marcBibId)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "ItemSelectByMARCBibID", connection, transaction,
					CustomSqlHelper.CreateInputParameter( "MARCBibID", SqlDbType.NVarChar, 50, false, marcBibId) ) )
			{
				using ( CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>() )
				{
					CustomGenericList<Item> list = helper.ExecuteReader( command );
					return ( list );
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
		public CustomGenericList<Item> ItemSelectWithExpiredPageNames(
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
					CustomGenericList<Item> list = helper.ExecuteReader( command );
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
		public CustomGenericList<Item> ItemSelectWithoutPageNames(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "ItemSelectWithoutPageNames", connection, transaction ) )
			{
				using ( CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>() )
				{
					CustomGenericList<Item> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		public CustomGenericList<Item> ItemSelectPaginationReport( SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, int paginationStatusId, DateTime startDate, DateTime endDate,
            int numRows, int pageNum, string sortColumn, string sortDirection)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( 
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "ItemSelectPaginationReport", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PaginationStatusID", SqlDbType.Int, null, false, paginationStatusId),
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, false, startDate),
                CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.DateTime, null, false, endDate),
                CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numRows),
                CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
                CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
                CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortDirection)))
			{
				using ( CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>() )
				{
					CustomGenericList<Item> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		public Item ItemSelectPagination( SqlConnection sqlConnection,	SqlTransaction sqlTransaction, int itemId )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "ItemSelectPagination", connection, transaction,
				CustomSqlHelper.CreateInputParameter( "ItemID", SqlDbType.Int, null, false, itemId ) ) )
			{
				using ( CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>() )
				{
					CustomGenericList<Item> list = helper.ExecuteReader( command );
					if ( list == null || list.Count == 0 )
					{
						return null;
					}
					else
					{
						return list[ 0 ];
					}
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
					CustomGenericList<Item> list = helper.ExecuteReader( command );
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
                    CustomGenericList<Item> list = helper.ExecuteReader(command);
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
				transaction = CustomSqlHelper.BeginTransaction( connection, transaction, isTransactionCoordinator );

                CustomDataAccessStatus<Item> updatedItem =
                    new ItemDAL().ItemManageAuto(connection, transaction, item, userId);

				if ( item.Pages.Count > 0 )
				{
					PageDAL pageDAL = new PageDAL();
					foreach ( Page page in item.Pages )
					{
						pageDAL.PageManageAuto( connection, transaction, page, userId );
					}
				}

                if (item.TitleItems.Count > 0)
                {
                    TitleItemDAL titleItemDAL = new TitleItemDAL();
                    foreach (TitleItem titleItem in item.TitleItems)
                    {
                        titleItemDAL.TitleItemManageAuto(connection, transaction, titleItem, userId);
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
        /// Select recent values from Item.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="top">Number of values to return</param>
        /// <param name="languageCode">Language of items to be included</param>
        /// <param name="institutionCode">Contributing institution of items to be included</param>
        /// <returns>List of objects of type Item.</returns>
        public CustomGenericList<Item> ItemSelectRecent(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int top,
            string languageCode,
            string institutionCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectRecent", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Top", SqlDbType.Int, null, false, top),
                CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    CustomGenericList<Item> list = helper.ExecuteReader(command);
                    return list;
                }
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
        public CustomGenericList<ItemSuspectCharacter> ItemSelectWithSuspectCharacters(
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
                    CustomGenericList<ItemSuspectCharacter> list = helper.ExecuteReader(command);
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
        /// Select titles associated with the specified collection
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="collectionID"></param>
        /// <returns></returns>
        public CustomGenericList<Item> ItemSelectByCollection(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int collectionID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectByCollection",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    CustomGenericList<Item> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select all items that have been published on BHL (Item.ItemStatusID = 40 and Title.PublishReady = 1)
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        public CustomGenericList<Item> ItemSelectPublished(
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
                    CustomGenericList<Item> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<Item> ItemSelectRecentlyChanged(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string startDate)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectRecentlyChanged", connection, transaction,
                 CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, false, startDate)
                ))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    CustomGenericList<Item> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<NonMemberMonograph> ItemSelectNonMemberMonograph(
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
                    CustomGenericList<NonMemberMonograph> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Return the specified number of items associated with the specified institution.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="institutionCode">Identifier of the institution</param>
        /// <param name="returnCount">Number of items to return</param>
        /// <param name="sortBy">'Date' or 'Title' are valid values.  'Date' returns list sorted by CreationDate DESC.</param>
        /// <returns>List of items.</returns>
        public CustomGenericList<Item> ItemSelectByInstitution(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string institutionCode,
            int returnCount,
            string sortBy)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectByInstitution", connection, transaction,
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount),
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.NVarChar, 10, false, sortBy)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    CustomGenericList<Item> list = helper.ExecuteReader(command);
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
    }
}


#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class BookDAL
	{
        /// <summary>
        /// Select all Books for a particular Title.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>Object of type Title.</returns>
        public List<Book> BookSelectByTitleID(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction,
				int titleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
		CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectByTitleID", connection, transaction,
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
			{
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					return (list);
				}
			}
		}

		public Book BookSelectByItemID(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction,
				int itemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
			CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

			using (SqlCommand command = CustomSqlHelper.CreateCommand("BookSelectByItemID", connection, transaction,
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
			{
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						return list[0];
					}
					else
					{
						return null;
					}
				}
			}
		}

		public Book BookSelectOAIDetail(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int bookID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectOAIDetail", connection, transaction,
							CustomSqlHelper.CreateInputParameter("BookID", SqlDbType.Int, null, false, bookID)))
			{
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						return list[0];
					}
					else
					{
						return null;
					}
				}
			}
		}

		public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Book book, int userId)
		{
			SqlConnection connection = sqlConnection;
			SqlTransaction transaction = sqlTransaction;

			if (connection == null)
			{
				connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
			}
			bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator(transaction);

			try
			{
				transaction = CustomSqlHelper.BeginTransaction(connection, transaction, isTransactionCoordinator);

				CustomDataAccessStatus<Book> updatedBook =
					new BookDAL().BookManageAuto(connection, transaction, book, userId);

				new ItemDAL().ItemManageAuto(connection, transaction, book.Item, userId);

				if (book.Institutions.Count > 0)
				{
					ItemInstitutionDAL itemInstitutionDAL = new ItemInstitutionDAL();
					foreach (Institution institution in book.Institutions)
					{
						if (institution.IsDeleted)
						{
							itemInstitutionDAL.ItemInstitutionDeleteAuto(connection, transaction, (int)institution.EntityInstitutionID);
						}
						if (institution.IsNew)
						{
							itemInstitutionDAL.ItemInstitutionInsert(connection, transaction, (int)updatedBook.ReturnObject.ItemID,
								institution.InstitutionCode, institution.InstitutionRoleName, userId);
						}
					}
				}

				if (book.Pages.Count > 0)
				{
					PageDAL pageDAL = new PageDAL();
					foreach (Page page in book.Pages)
					{
						pageDAL.PageManageAuto(connection, transaction, page, userId);
					}
				}

				if (book.ItemTitles.Count > 0)
				{
					ItemTitleDAL itemTitleDAL = new ItemTitleDAL();
					foreach (ItemTitle itemTitle in book.ItemTitles)
					{
						itemTitleDAL.ItemTitleManageAuto(connection, transaction, itemTitle, userId);
					}
				}

				if (book.ItemLanguages.Count > 0)
				{
					ItemLanguageDAL itemLanguageDAL = new ItemLanguageDAL();
					foreach (ItemLanguage itemLanguage in book.ItemLanguages)
					{
						itemLanguageDAL.ItemLanguageManageAuto(connection, transaction, itemLanguage, userId);
					}
				}

				if (book.ItemCollections.Count > 0)
				{
					ItemCollectionDAL itemCollectionDAL = new ItemCollectionDAL();
					foreach (ItemCollection itemCollection in book.ItemCollections)
					{
						if (itemCollection.ItemID == 0) itemCollection.ItemID = (int)updatedBook.ReturnObject.ItemID;
						itemCollectionDAL.ItemCollectionManageAuto(connection, transaction, itemCollection);
					}
				}

				if (book.ItemRelationships.Count > 0)
				{
					ItemRelationshipDAL irDAL = new ItemRelationshipDAL();
					foreach (ItemRelationship ir in book.ItemRelationships)
					{
						if (ir.ParentID == 0) ir.ParentID = (int)updatedBook.ReturnObject.ItemID;
						irDAL.ItemRelationshipManageAuto(connection, transaction, ir, userId);
					}
				}

				/*
				if (book.Segments.Count > 0)
				{
					SegmentDAL segmentDAL = new SegmentDAL();
					foreach (Segment segment in book.Segments)
					{
						segmentDAL.SegmentManageAuto(connection, transaction, segment, userId);
					}
				}
				*/

				CustomSqlHelper.CommitTransaction(transaction, isTransactionCoordinator);
			}
			catch (Exception ex)
			{
				CustomSqlHelper.RollbackTransaction(transaction, isTransactionCoordinator);

				throw;
			}
			finally
			{
				CustomSqlHelper.CloseConnection(connection, isTransactionCoordinator);
			}
		}

		public Book BookSelectByBarCodeOrItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int? itemId, string barCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectByBarCodeOrItemID", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemId),
				CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 200, true, barCode)))
			{
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Book book = (Book)list[0];
						book.Item = new ItemDAL().ItemSelectAuto(connection, transaction, book.ItemID);
						book.Pages = new PageDAL().PageSelectByItemID(connection, transaction, book.BookID);
						book.ItemTitles = new ItemTitleDAL().ItemTitleSelectByItem(connection, transaction, book.BookID);
						book.ItemLanguages = new ItemLanguageDAL().ItemLanguageSelectByItemID(connection, transaction, book.ItemID);
						book.ItemCollections = new ItemCollectionDAL().SelectByItem(connection, transaction, book.ItemID);
						book.Segments = new SegmentDAL().SegmentSelectByItemID(connection, transaction, book.BookID, 1);
						book.Institutions = new InstitutionDAL().InstitutionSelectByItemID(connection, transaction, book.ItemID);
						book.ItemRelationships = new ItemRelationshipDAL().ItemRelationshipSelectByItemID(connection, transaction, book.ItemID);
						return book;
					}
					else
					{
						return null;
					}
				}
			}
		}

		public Book BookSelectTextPathForItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectTextPathForItemID", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
			{
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					if (list.Count > 0)
						return list[0];
					else
						return null;
				}
			}
		}

		/// <summary>
		/// Select recent values from Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="top">Number of values to return</param>
		/// <param name="languageCode">Language of items to be included</param>
		/// <param name="institutionCode">Contributing institution of items to be included</param>
		/// <returns>List of objects of type Item.</returns>
		public List<Book> BookSelectRecent(
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
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					return list;
				}
			}
		}

		/// <summary>
		/// Select items/books associated with the specified collection
		/// </summary>
		/// <param name="sqlConnection"></param>
		/// <param name="sqlTransaction"></param>
		/// <param name="collectionID"></param>
		/// <returns></returns>
		public List<Book> BookSelectByCollection(
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
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					return (list);
				}
			}
		}

		public List<Book> BookSelectRecentlyChanged(
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
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					return list;
				}
			}
		}

		public List<Book> BookSelectByInstitutionAndRole(
			SqlConnection sqlConnection,
			SqlTransaction sqlTransaction,
			string institutionCode,
			int institutionRoleID,
			string barcode,
			int numRows,
			int pageNum,
			string sortColumn,
			string sortOrder)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectByInstitutionAndRole", connection, transaction,
				CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
				CustomSqlHelper.CreateInputParameter("InstitutionRoleID", SqlDbType.Int, null, false, institutionRoleID),
				CustomSqlHelper.CreateInputParameter("Barcode", SqlDbType.NVarChar, 200, false, barcode),
				CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numRows),
				CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
				CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
				CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortOrder)))
			{
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					return list;
				}
			}
		}

		// This does not filter on item status
		public List<Book> BookSelectByMarcBibId(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string marcBibId)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectByMARCBibID", connection, transaction,
					CustomSqlHelper.CreateInputParameter("MARCBibID", SqlDbType.NVarChar, 50, false, marcBibId)))
			{
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					return (list);
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
		public List<Book> BookSelectByInstitution(
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
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					return list;
				}
			}
		}

		public Book BookSelectPagination(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemId)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectPagination", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
			{
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					if (list == null || list.Count == 0)
					{
						return null;
					}
					else
					{
						return list[0];
					}
				}
			}
		}

		/// <summary>
		/// Select data for all item RIS citations for the specified Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <returns>List of type RISCitation.</returns>
		public List<RISCitation> BookSelectRISCitationsForItemID(
						SqlConnection sqlConnection,
						SqlTransaction sqlTransaction,
						int bookID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BookSelectRISCitationsForBookID", connection, transaction,
				CustomSqlHelper.CreateInputParameter("BookID", SqlDbType.Int, null, false, bookID)))
			{
				using (CustomSqlHelper<RISCitation> helper = new CustomSqlHelper<RISCitation>())
				{
					List<RISCitation> list = helper.ExecuteReader(command);
					return (list);
				}
			}
		}

		/// <summary>
		/// Select data for BibTex references for the specified Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bookId">Title identifier for which to get BibTex data</param>
		/// <returns>List of type TitleBibTeX.</returns>
		public List<TitleBibTeX> BookBibTeXSelectForBookID(
						SqlConnection sqlConnection,
						SqlTransaction sqlTransaction,
						int bookId)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BookBibTeXSelectForBookID", connection, transaction,
				CustomSqlHelper.CreateInputParameter("BookID", SqlDbType.Int, null, false, bookId)))
			{
				using (CustomSqlHelper<TitleBibTeX> helper = new CustomSqlHelper<TitleBibTeX>())
				{
					List<TitleBibTeX> list = helper.ExecuteReader(command);
					return (list);
				}
			}
		}
	}
}



// Generated 1/18/2008 11:10:47 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PageTypeDAL is based upon PageType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class PageTypeDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	partial class PageTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTypeID">Unique identifier for each Page Type record.</param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageTypeID", SqlDbType.Int, null, false, pageTypeID)))
			{
				using (CustomSqlHelper<PageType> helper = new CustomSqlHelper<PageType>())
				{
					CustomGenericList<PageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageType o = list[0];
						list = null;
						return o;
					}
					else
					{
						return null;
					}
				}
			}
		}
		
		/// <summary>
		/// Select values from PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTypeID">Unique identifier for each Page Type record.</param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> PageTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PageTypeID", SqlDbType.Int, null, false, pageTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTypeName">Name of a Page Type.</param>
		/// <param name="pageTypeDescription">Description of the Page Type.</param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string pageTypeName,
			string pageTypeDescription)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PageTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("PageTypeName", SqlDbType.NVarChar, 30, false, pageTypeName),
					CustomSqlHelper.CreateInputParameter("PageTypeDescription", SqlDbType.NVarChar, 255, true, pageTypeDescription), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageType> helper = new CustomSqlHelper<PageType>())
				{
					CustomGenericList<PageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageType o = list[0];
						list = null;
						return o;
					}
					else
					{
						return null;
					}
				}
			}
		}

		/// <summary>
		/// Insert values into PageType. Returns an object of type PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageType.</param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageType value)
		{
			return PageTypeInsertAuto(sqlConnection, sqlTransaction, 
				value.PageTypeName,
				value.PageTypeDescription);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTypeID">Unique identifier for each Page Type record.</param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageTypeID", SqlDbType.Int, null, false, pageTypeID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				int returnCode = CustomSqlHelper.ExecuteNonQuery(command, "ReturnCode");
				
				if (transaction == null)
				{
					CustomSqlHelper.CloseConnection(connection);
				}
				
				if (returnCode == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		
		#endregion ===== DELETE =====

 		#region ===== UPDATE =====

		/// <summary>
		/// Update values in PageType. Returns an object of type PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTypeID">Unique identifier for each Page Type record.</param>
		/// <param name="pageTypeName">Name of a Page Type.</param>
		/// <param name="pageTypeDescription">Description of the Page Type.</param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTypeID,
			string pageTypeName,
			string pageTypeDescription)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageTypeID", SqlDbType.Int, null, false, pageTypeID),
					CustomSqlHelper.CreateInputParameter("PageTypeName", SqlDbType.NVarChar, 30, false, pageTypeName),
					CustomSqlHelper.CreateInputParameter("PageTypeDescription", SqlDbType.NVarChar, 255, true, pageTypeDescription), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageType> helper = new CustomSqlHelper<PageType>())
				{
					CustomGenericList<PageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageType o = list[0];
						list = null;
						return o;
					}
					else
					{
						return null;
					}
				}
			}
		}
		
		/// <summary>
		/// Update values in PageType. Returns an object of type PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageType.</param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageType value)
		{
			return PageTypeUpdateAuto(sqlConnection, sqlTransaction,
				value.PageTypeID,
				value.PageTypeName,
				value.PageTypeDescription);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage PageType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageType.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageType>.</returns>
		public CustomDataAccessStatus<PageType> PageTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageType value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				PageType returnValue = PageTypeInsertAuto(sqlConnection, sqlTransaction, 
					value.PageTypeName,
						value.PageTypeDescription);
				
				return new CustomDataAccessStatus<PageType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PageTypeDeleteAuto(sqlConnection, sqlTransaction, 
					value.PageTypeID))
				{
				return new CustomDataAccessStatus<PageType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<PageType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				PageType returnValue = PageTypeUpdateAuto(sqlConnection, sqlTransaction, 
					value.PageTypeID,
						value.PageTypeName,
						value.PageTypeDescription);
					
				return new CustomDataAccessStatus<PageType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<PageType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

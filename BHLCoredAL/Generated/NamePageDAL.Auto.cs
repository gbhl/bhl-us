
// Generated 10/29/2012 3:17:36 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class NamePageDAL is based upon NamePage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class NamePageDAL
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
	partial class NamePageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from NamePage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="namePageID"></param>
		/// <returns>Object of type NamePage.</returns>
		public NamePage NamePageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int namePageID)
		{
			return NamePageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	namePageID );
		}
			
		/// <summary>
		/// Select values from NamePage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="namePageID"></param>
		/// <returns>Object of type NamePage.</returns>
		public NamePage NamePageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int namePageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NamePageID", SqlDbType.Int, null, false, namePageID)))
			{
				using (CustomSqlHelper<NamePage> helper = new CustomSqlHelper<NamePage>())
				{
					CustomGenericList<NamePage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NamePage o = list[0];
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
		/// Select values from NamePage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="namePageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> NamePageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int namePageID)
		{
			return NamePageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", namePageID );
		}
		
		/// <summary>
		/// Select values from NamePage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="namePageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> NamePageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int namePageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("NamePageID", SqlDbType.Int, null, false, namePageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into NamePage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameID"></param>
		/// <param name="pageID"></param>
		/// <param name="nameSourceID"></param>
		/// <param name="isFirstOccurrence"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NamePage.</returns>
		public NamePage NamePageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameID,
			int pageID,
			int nameSourceID,
			short isFirstOccurrence,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return NamePageInsertAuto( sqlConnection, sqlTransaction, "BHL", nameID, pageID, nameSourceID, isFirstOccurrence, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into NamePage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameID"></param>
		/// <param name="pageID"></param>
		/// <param name="nameSourceID"></param>
		/// <param name="isFirstOccurrence"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NamePage.</returns>
		public NamePage NamePageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameID,
			int pageID,
			int nameSourceID,
			short isFirstOccurrence,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("NamePageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("NameID", SqlDbType.Int, null, false, nameID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("NameSourceID", SqlDbType.Int, null, false, nameSourceID),
					CustomSqlHelper.CreateInputParameter("IsFirstOccurrence", SqlDbType.SmallInt, null, false, isFirstOccurrence),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NamePage> helper = new CustomSqlHelper<NamePage>())
				{
					CustomGenericList<NamePage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NamePage o = list[0];
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
		/// Insert values into NamePage. Returns an object of type NamePage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NamePage.</param>
		/// <returns>Object of type NamePage.</returns>
		public NamePage NamePageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NamePage value)
		{
			return NamePageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into NamePage. Returns an object of type NamePage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NamePage.</param>
		/// <returns>Object of type NamePage.</returns>
		public NamePage NamePageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NamePage value)
		{
			return NamePageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NameID,
				value.PageID,
				value.NameSourceID,
				value.IsFirstOccurrence,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from NamePage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="namePageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NamePageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int namePageID)
		{
			return NamePageDeleteAuto( sqlConnection, sqlTransaction, "BHL", namePageID );
		}
		
		/// <summary>
		/// Delete values from NamePage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="namePageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NamePageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int namePageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NamePageID", SqlDbType.Int, null, false, namePageID), 
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
		/// Update values in NamePage. Returns an object of type NamePage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="namePageID"></param>
		/// <param name="nameID"></param>
		/// <param name="pageID"></param>
		/// <param name="nameSourceID"></param>
		/// <param name="isFirstOccurrence"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NamePage.</returns>
		public NamePage NamePageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int namePageID,
			int nameID,
			int pageID,
			int nameSourceID,
			short isFirstOccurrence,
			int? lastModifiedUserID)
		{
			return NamePageUpdateAuto( sqlConnection, sqlTransaction, "BHL", namePageID, nameID, pageID, nameSourceID, isFirstOccurrence, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in NamePage. Returns an object of type NamePage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="namePageID"></param>
		/// <param name="nameID"></param>
		/// <param name="pageID"></param>
		/// <param name="nameSourceID"></param>
		/// <param name="isFirstOccurrence"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NamePage.</returns>
		public NamePage NamePageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int namePageID,
			int nameID,
			int pageID,
			int nameSourceID,
			short isFirstOccurrence,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NamePageID", SqlDbType.Int, null, false, namePageID),
					CustomSqlHelper.CreateInputParameter("NameID", SqlDbType.Int, null, false, nameID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("NameSourceID", SqlDbType.Int, null, false, nameSourceID),
					CustomSqlHelper.CreateInputParameter("IsFirstOccurrence", SqlDbType.SmallInt, null, false, isFirstOccurrence),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NamePage> helper = new CustomSqlHelper<NamePage>())
				{
					CustomGenericList<NamePage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NamePage o = list[0];
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
		/// Update values in NamePage. Returns an object of type NamePage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NamePage.</param>
		/// <returns>Object of type NamePage.</returns>
		public NamePage NamePageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NamePage value)
		{
			return NamePageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in NamePage. Returns an object of type NamePage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NamePage.</param>
		/// <returns>Object of type NamePage.</returns>
		public NamePage NamePageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NamePage value)
		{
			return NamePageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NamePageID,
				value.NameID,
				value.PageID,
				value.NameSourceID,
				value.IsFirstOccurrence,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage NamePage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in NamePage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NamePage.</param>
		/// <returns>Object of type CustomDataAccessStatus<NamePage>.</returns>
		public CustomDataAccessStatus<NamePage> NamePageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NamePage value , int userId )
		{
			return NamePageManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage NamePage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in NamePage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NamePage.</param>
		/// <returns>Object of type CustomDataAccessStatus<NamePage>.</returns>
		public CustomDataAccessStatus<NamePage> NamePageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NamePage value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				NamePage returnValue = NamePageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameID,
						value.PageID,
						value.NameSourceID,
						value.IsFirstOccurrence,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<NamePage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (NamePageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NamePageID))
				{
				return new CustomDataAccessStatus<NamePage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<NamePage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				NamePage returnValue = NamePageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NamePageID,
						value.NameID,
						value.PageID,
						value.NameSourceID,
						value.IsFirstOccurrence,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<NamePage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<NamePage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

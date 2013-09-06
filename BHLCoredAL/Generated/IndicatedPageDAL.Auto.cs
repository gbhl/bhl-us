
// Generated 5/17/2010 4:03:17 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IndicatedPageDAL is based upon IndicatedPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class IndicatedPageDAL
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
	partial class IndicatedPageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="sequence"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			short sequence)
		{
			return IndicatedPageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	pageID, sequence );
		}
			
		/// <summary>
		/// Select values from IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <param name="sequence"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID,
			short sequence )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.SmallInt, null, false, sequence)))
			{
				using (CustomSqlHelper<IndicatedPage> helper = new CustomSqlHelper<IndicatedPage>())
				{
					CustomGenericList<IndicatedPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IndicatedPage o = list[0];
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
		/// Select values from IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="sequence"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IndicatedPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			short sequence)
		{
			return IndicatedPageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", pageID, sequence );
		}
		
		/// <summary>
		/// Select values from IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <param name="sequence"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IndicatedPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID,
			short sequence)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.SmallInt, null, false, sequence)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="sequence"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			short sequence,
			string pagePrefix,
			string pageNumber,
			bool implied,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return IndicatedPageInsertAuto( sqlConnection, sqlTransaction, "BHL", pageID, sequence, pagePrefix, pageNumber, implied, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <param name="sequence"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID,
			short sequence,
			string pagePrefix,
			string pageNumber,
			bool implied,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.SmallInt, null, false, sequence),
					CustomSqlHelper.CreateInputParameter("PagePrefix", SqlDbType.NVarChar, 40, true, pagePrefix),
					CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, true, pageNumber),
					CustomSqlHelper.CreateInputParameter("Implied", SqlDbType.Bit, null, false, implied),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IndicatedPage> helper = new CustomSqlHelper<IndicatedPage>())
				{
					CustomGenericList<IndicatedPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IndicatedPage o = list[0];
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
		/// Insert values into IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IndicatedPage value)
		{
			return IndicatedPageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IndicatedPage value)
		{
			return IndicatedPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageID,
				value.Sequence,
				value.PagePrefix,
				value.PageNumber,
				value.Implied,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="sequence"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IndicatedPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			short sequence)
		{
			return IndicatedPageDeleteAuto( sqlConnection, sqlTransaction, "BHL", pageID, sequence );
		}
		
		/// <summary>
		/// Delete values from IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <param name="sequence"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IndicatedPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID,
			short sequence)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.SmallInt, null, false, sequence), 
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
		/// Update values in IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="sequence"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			short sequence,
			string pagePrefix,
			string pageNumber,
			bool implied,
			int? lastModifiedUserID)
		{
			return IndicatedPageUpdateAuto( sqlConnection, sqlTransaction, "BHL", pageID, sequence, pagePrefix, pageNumber, implied, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <param name="sequence"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID,
			short sequence,
			string pagePrefix,
			string pageNumber,
			bool implied,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.SmallInt, null, false, sequence),
					CustomSqlHelper.CreateInputParameter("PagePrefix", SqlDbType.NVarChar, 40, true, pagePrefix),
					CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, true, pageNumber),
					CustomSqlHelper.CreateInputParameter("Implied", SqlDbType.Bit, null, false, implied),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IndicatedPage> helper = new CustomSqlHelper<IndicatedPage>())
				{
					CustomGenericList<IndicatedPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IndicatedPage o = list[0];
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
		/// Update values in IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IndicatedPage value)
		{
			return IndicatedPageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IndicatedPage value)
		{
			return IndicatedPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageID,
				value.Sequence,
				value.PagePrefix,
				value.PageNumber,
				value.Implied,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage IndicatedPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<IndicatedPage>.</returns>
		public CustomDataAccessStatus<IndicatedPage> IndicatedPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IndicatedPage value , int userId )
		{
			return IndicatedPageManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage IndicatedPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<IndicatedPage>.</returns>
		public CustomDataAccessStatus<IndicatedPage> IndicatedPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IndicatedPage value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				IndicatedPage returnValue = IndicatedPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageID,
						value.Sequence,
						value.PagePrefix,
						value.PageNumber,
						value.Implied,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<IndicatedPage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IndicatedPageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageID,
						value.Sequence))
				{
				return new CustomDataAccessStatus<IndicatedPage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IndicatedPage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				IndicatedPage returnValue = IndicatedPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageID,
						value.Sequence,
						value.PagePrefix,
						value.PageNumber,
						value.Implied,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<IndicatedPage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IndicatedPage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

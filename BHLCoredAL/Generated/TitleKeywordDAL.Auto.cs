
// Generated 5/3/2012 1:28:21 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleKeywordDAL is based upon TitleKeyword.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleKeywordDAL
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
	partial class TitleKeywordDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from TitleKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleKeywordID"></param>
		/// <returns>Object of type TitleKeyword.</returns>
		public TitleKeyword TitleKeywordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleKeywordID)
		{
			return TitleKeywordSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleKeywordID );
		}
			
		/// <summary>
		/// Select values from TitleKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleKeywordID"></param>
		/// <returns>Object of type TitleKeyword.</returns>
		public TitleKeyword TitleKeywordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleKeywordID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleKeywordSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleKeywordID", SqlDbType.Int, null, false, titleKeywordID)))
			{
				using (CustomSqlHelper<TitleKeyword> helper = new CustomSqlHelper<TitleKeyword>())
				{
					CustomGenericList<TitleKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleKeyword o = list[0];
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
		/// Select values from TitleKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleKeywordID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleKeywordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleKeywordID)
		{
			return TitleKeywordSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleKeywordID );
		}
		
		/// <summary>
		/// Select values from TitleKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleKeywordID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleKeywordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleKeywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleKeywordSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleKeywordID", SqlDbType.Int, null, false, titleKeywordID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into TitleKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="keywordID"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcSubFieldCode"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleKeyword.</returns>
		public TitleKeyword TitleKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			int keywordID,
			string marcDataFieldTag,
			string marcSubFieldCode,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return TitleKeywordInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, keywordID, marcDataFieldTag, marcSubFieldCode, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into TitleKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="keywordID"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcSubFieldCode"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleKeyword.</returns>
		public TitleKeyword TitleKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			int keywordID,
			string marcDataFieldTag,
			string marcSubFieldCode,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleKeywordInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleKeywordID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("KeywordID", SqlDbType.Int, null, false, keywordID),
					CustomSqlHelper.CreateInputParameter("MarcDataFieldTag", SqlDbType.NVarChar, 50, true, marcDataFieldTag),
					CustomSqlHelper.CreateInputParameter("MarcSubFieldCode", SqlDbType.NVarChar, 50, true, marcSubFieldCode),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleKeyword> helper = new CustomSqlHelper<TitleKeyword>())
				{
					CustomGenericList<TitleKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleKeyword o = list[0];
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
		/// Insert values into TitleKeyword. Returns an object of type TitleKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleKeyword.</param>
		/// <returns>Object of type TitleKeyword.</returns>
		public TitleKeyword TitleKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleKeyword value)
		{
			return TitleKeywordInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into TitleKeyword. Returns an object of type TitleKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleKeyword.</param>
		/// <returns>Object of type TitleKeyword.</returns>
		public TitleKeyword TitleKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleKeyword value)
		{
			return TitleKeywordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.KeywordID,
				value.MarcDataFieldTag,
				value.MarcSubFieldCode,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from TitleKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleKeywordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleKeywordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleKeywordID)
		{
			return TitleKeywordDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleKeywordID );
		}
		
		/// <summary>
		/// Delete values from TitleKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleKeywordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleKeywordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleKeywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleKeywordDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleKeywordID", SqlDbType.Int, null, false, titleKeywordID), 
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
		/// Update values in TitleKeyword. Returns an object of type TitleKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleKeywordID"></param>
		/// <param name="titleID"></param>
		/// <param name="keywordID"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcSubFieldCode"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleKeyword.</returns>
		public TitleKeyword TitleKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleKeywordID,
			int titleID,
			int keywordID,
			string marcDataFieldTag,
			string marcSubFieldCode,
			int? lastModifiedUserID)
		{
			return TitleKeywordUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleKeywordID, titleID, keywordID, marcDataFieldTag, marcSubFieldCode, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in TitleKeyword. Returns an object of type TitleKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleKeywordID"></param>
		/// <param name="titleID"></param>
		/// <param name="keywordID"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcSubFieldCode"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleKeyword.</returns>
		public TitleKeyword TitleKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleKeywordID,
			int titleID,
			int keywordID,
			string marcDataFieldTag,
			string marcSubFieldCode,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleKeywordUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleKeywordID", SqlDbType.Int, null, false, titleKeywordID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("KeywordID", SqlDbType.Int, null, false, keywordID),
					CustomSqlHelper.CreateInputParameter("MarcDataFieldTag", SqlDbType.NVarChar, 50, true, marcDataFieldTag),
					CustomSqlHelper.CreateInputParameter("MarcSubFieldCode", SqlDbType.NVarChar, 50, true, marcSubFieldCode),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleKeyword> helper = new CustomSqlHelper<TitleKeyword>())
				{
					CustomGenericList<TitleKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleKeyword o = list[0];
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
		/// Update values in TitleKeyword. Returns an object of type TitleKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleKeyword.</param>
		/// <returns>Object of type TitleKeyword.</returns>
		public TitleKeyword TitleKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleKeyword value)
		{
			return TitleKeywordUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in TitleKeyword. Returns an object of type TitleKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleKeyword.</param>
		/// <returns>Object of type TitleKeyword.</returns>
		public TitleKeyword TitleKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleKeyword value)
		{
			return TitleKeywordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleKeywordID,
				value.TitleID,
				value.KeywordID,
				value.MarcDataFieldTag,
				value.MarcSubFieldCode,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage TitleKeyword object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleKeyword.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleKeyword>.</returns>
		public CustomDataAccessStatus<TitleKeyword> TitleKeywordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleKeyword value , int userId )
		{
			return TitleKeywordManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage TitleKeyword object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleKeyword.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleKeyword>.</returns>
		public CustomDataAccessStatus<TitleKeyword> TitleKeywordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleKeyword value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TitleKeyword returnValue = TitleKeywordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.KeywordID,
						value.MarcDataFieldTag,
						value.MarcSubFieldCode,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TitleKeyword>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleKeywordDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleKeywordID))
				{
				return new CustomDataAccessStatus<TitleKeyword>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleKeyword>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TitleKeyword returnValue = TitleKeywordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleKeywordID,
						value.TitleID,
						value.KeywordID,
						value.MarcDataFieldTag,
						value.MarcSubFieldCode,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TitleKeyword>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleKeyword>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

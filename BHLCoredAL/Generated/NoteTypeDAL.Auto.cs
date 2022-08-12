
// Generated 1/5/2021 3:26:30 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class NoteTypeDAL is based upon dbo.NoteType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class NoteTypeDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	partial class NoteTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.NoteType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="noteTypeID"></param>
		/// <returns>Object of type NoteType.</returns>
		public NoteType NoteTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int noteTypeID)
		{
			return NoteTypeSelectAuto(	sqlConnection, sqlTransaction, "BHL",	noteTypeID );
		}
			
		/// <summary>
		/// Select values from dbo.NoteType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="noteTypeID"></param>
		/// <returns>Object of type NoteType.</returns>
		public NoteType NoteTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int noteTypeID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NoteTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NoteTypeID", SqlDbType.Int, null, false, noteTypeID)))
			{
				using (CustomSqlHelper<NoteType> helper = new CustomSqlHelper<NoteType>())
				{
					List<NoteType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NoteType o = list[0];
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
		/// Select values from dbo.NoteType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="noteTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> NoteTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int noteTypeID)
		{
			return NoteTypeSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", noteTypeID );
		}
		
		/// <summary>
		/// Select values from dbo.NoteType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="noteTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> NoteTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int noteTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NoteTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("NoteTypeID", SqlDbType.Int, null, false, noteTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.NoteType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="noteTypeName"></param>
		/// <param name="noteTypeDisplay"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcIndicator1"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NoteType.</returns>
		public NoteType NoteTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string noteTypeName,
			string noteTypeDisplay,
			string marcDataFieldTag,
			string marcIndicator1,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return NoteTypeInsertAuto( sqlConnection, sqlTransaction, "BHL", noteTypeName, noteTypeDisplay, marcDataFieldTag, marcIndicator1, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.NoteType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="noteTypeName"></param>
		/// <param name="noteTypeDisplay"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcIndicator1"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NoteType.</returns>
		public NoteType NoteTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string noteTypeName,
			string noteTypeDisplay,
			string marcDataFieldTag,
			string marcIndicator1,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NoteTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("NoteTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("NoteTypeName", SqlDbType.NVarChar, 50, false, noteTypeName),
					CustomSqlHelper.CreateInputParameter("NoteTypeDisplay", SqlDbType.NVarChar, 50, false, noteTypeDisplay),
					CustomSqlHelper.CreateInputParameter("MarcDataFieldTag", SqlDbType.NVarChar, 5, false, marcDataFieldTag),
					CustomSqlHelper.CreateInputParameter("MarcIndicator1", SqlDbType.NVarChar, 5, false, marcIndicator1),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NoteType> helper = new CustomSqlHelper<NoteType>())
				{
					List<NoteType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NoteType o = list[0];
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
		/// Insert values into dbo.NoteType. Returns an object of type NoteType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NoteType.</param>
		/// <returns>Object of type NoteType.</returns>
		public NoteType NoteTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NoteType value)
		{
			return NoteTypeInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.NoteType. Returns an object of type NoteType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NoteType.</param>
		/// <returns>Object of type NoteType.</returns>
		public NoteType NoteTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NoteType value)
		{
			return NoteTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NoteTypeName,
				value.NoteTypeDisplay,
				value.MarcDataFieldTag,
				value.MarcIndicator1,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.NoteType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="noteTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NoteTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int noteTypeID)
		{
			return NoteTypeDeleteAuto( sqlConnection, sqlTransaction, "BHL", noteTypeID );
		}
		
		/// <summary>
		/// Delete values from dbo.NoteType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="noteTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NoteTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int noteTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NoteTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NoteTypeID", SqlDbType.Int, null, false, noteTypeID), 
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
		/// Update values in dbo.NoteType. Returns an object of type NoteType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="noteTypeID"></param>
		/// <param name="noteTypeName"></param>
		/// <param name="noteTypeDisplay"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcIndicator1"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NoteType.</returns>
		public NoteType NoteTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int noteTypeID,
			string noteTypeName,
			string noteTypeDisplay,
			string marcDataFieldTag,
			string marcIndicator1,
			int? lastModifiedUserID)
		{
			return NoteTypeUpdateAuto( sqlConnection, sqlTransaction, "BHL", noteTypeID, noteTypeName, noteTypeDisplay, marcDataFieldTag, marcIndicator1, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.NoteType. Returns an object of type NoteType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="noteTypeID"></param>
		/// <param name="noteTypeName"></param>
		/// <param name="noteTypeDisplay"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcIndicator1"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NoteType.</returns>
		public NoteType NoteTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int noteTypeID,
			string noteTypeName,
			string noteTypeDisplay,
			string marcDataFieldTag,
			string marcIndicator1,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NoteTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NoteTypeID", SqlDbType.Int, null, false, noteTypeID),
					CustomSqlHelper.CreateInputParameter("NoteTypeName", SqlDbType.NVarChar, 50, false, noteTypeName),
					CustomSqlHelper.CreateInputParameter("NoteTypeDisplay", SqlDbType.NVarChar, 50, false, noteTypeDisplay),
					CustomSqlHelper.CreateInputParameter("MarcDataFieldTag", SqlDbType.NVarChar, 5, false, marcDataFieldTag),
					CustomSqlHelper.CreateInputParameter("MarcIndicator1", SqlDbType.NVarChar, 5, false, marcIndicator1),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NoteType> helper = new CustomSqlHelper<NoteType>())
				{
					List<NoteType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NoteType o = list[0];
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
		/// Update values in dbo.NoteType. Returns an object of type NoteType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NoteType.</param>
		/// <returns>Object of type NoteType.</returns>
		public NoteType NoteTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NoteType value)
		{
			return NoteTypeUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.NoteType. Returns an object of type NoteType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NoteType.</param>
		/// <returns>Object of type NoteType.</returns>
		public NoteType NoteTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NoteType value)
		{
			return NoteTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NoteTypeID,
				value.NoteTypeName,
				value.NoteTypeDisplay,
				value.MarcDataFieldTag,
				value.MarcIndicator1,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.NoteType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.NoteType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NoteType.</param>
		/// <returns>Object of type CustomDataAccessStatus<NoteType>.</returns>
		public CustomDataAccessStatus<NoteType> NoteTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NoteType value , int userId )
		{
			return NoteTypeManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.NoteType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.NoteType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NoteType.</param>
		/// <returns>Object of type CustomDataAccessStatus<NoteType>.</returns>
		public CustomDataAccessStatus<NoteType> NoteTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NoteType value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				NoteType returnValue = NoteTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NoteTypeName,
						value.NoteTypeDisplay,
						value.MarcDataFieldTag,
						value.MarcIndicator1,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<NoteType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (NoteTypeDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NoteTypeID))
				{
				return new CustomDataAccessStatus<NoteType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<NoteType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				NoteType returnValue = NoteTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NoteTypeID,
						value.NoteTypeName,
						value.NoteTypeDisplay,
						value.MarcDataFieldTag,
						value.MarcIndicator1,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<NoteType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<NoteType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


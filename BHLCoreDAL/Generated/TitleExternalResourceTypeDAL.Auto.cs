
// Generated 2/22/2023 5:08:40 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleExternalResourceTypeDAL is based upon dbo.TitleExternalResourceType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleExternalResourceTypeDAL
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
	partial class TitleExternalResourceTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.TitleExternalResourceType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <returns>Object of type TitleExternalResourceType.</returns>
		public TitleExternalResourceType TitleExternalResourceTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleExternalResourceTypeID)
		{
			return TitleExternalResourceTypeSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleExternalResourceTypeID );
		}
			
		/// <summary>
		/// Select values from dbo.TitleExternalResourceType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <returns>Object of type TitleExternalResourceType.</returns>
		public TitleExternalResourceType TitleExternalResourceTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleExternalResourceTypeID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleExternalResourceTypeID", SqlDbType.Int, null, false, titleExternalResourceTypeID)))
			{
				using (CustomSqlHelper<TitleExternalResourceType> helper = new CustomSqlHelper<TitleExternalResourceType>())
				{
					List<TitleExternalResourceType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleExternalResourceType o = list[0];
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
		/// Select values from dbo.TitleExternalResourceType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleExternalResourceTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleExternalResourceTypeID)
		{
			return TitleExternalResourceTypeSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleExternalResourceTypeID );
		}
		
		/// <summary>
		/// Select values from dbo.TitleExternalResourceType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleExternalResourceTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleExternalResourceTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleExternalResourceTypeID", SqlDbType.Int, null, false, titleExternalResourceTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.TitleExternalResourceType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="externalResourceTypeName"></param>
		/// <param name="externalResourceTypeLabel"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleExternalResourceType.</returns>
		public TitleExternalResourceType TitleExternalResourceTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string externalResourceTypeName,
			string externalResourceTypeLabel,
			int creationUserID,
			int lastModifiedUserID)
		{
			return TitleExternalResourceTypeInsertAuto( sqlConnection, sqlTransaction, "BHL", externalResourceTypeName, externalResourceTypeLabel, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.TitleExternalResourceType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="externalResourceTypeName"></param>
		/// <param name="externalResourceTypeLabel"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleExternalResourceType.</returns>
		public TitleExternalResourceType TitleExternalResourceTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string externalResourceTypeName,
			string externalResourceTypeLabel,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleExternalResourceTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ExternalResourceTypeName", SqlDbType.VarChar, 100, false, externalResourceTypeName),
					CustomSqlHelper.CreateInputParameter("ExternalResourceTypeLabel", SqlDbType.VarChar, 100, false, externalResourceTypeLabel),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleExternalResourceType> helper = new CustomSqlHelper<TitleExternalResourceType>())
				{
					List<TitleExternalResourceType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleExternalResourceType o = list[0];
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
		/// Insert values into dbo.TitleExternalResourceType. Returns an object of type TitleExternalResourceType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleExternalResourceType.</param>
		/// <returns>Object of type TitleExternalResourceType.</returns>
		public TitleExternalResourceType TitleExternalResourceTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleExternalResourceType value)
		{
			return TitleExternalResourceTypeInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.TitleExternalResourceType. Returns an object of type TitleExternalResourceType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleExternalResourceType.</param>
		/// <returns>Object of type TitleExternalResourceType.</returns>
		public TitleExternalResourceType TitleExternalResourceTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleExternalResourceType value)
		{
			return TitleExternalResourceTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ExternalResourceTypeName,
				value.ExternalResourceTypeLabel,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.TitleExternalResourceType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleExternalResourceTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleExternalResourceTypeID)
		{
			return TitleExternalResourceTypeDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleExternalResourceTypeID );
		}
		
		/// <summary>
		/// Delete values from dbo.TitleExternalResourceType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleExternalResourceTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleExternalResourceTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleExternalResourceTypeID", SqlDbType.Int, null, false, titleExternalResourceTypeID), 
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
		/// Update values in dbo.TitleExternalResourceType. Returns an object of type TitleExternalResourceType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <param name="externalResourceTypeName"></param>
		/// <param name="externalResourceTypeLabel"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleExternalResourceType.</returns>
		public TitleExternalResourceType TitleExternalResourceTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleExternalResourceTypeID,
			string externalResourceTypeName,
			string externalResourceTypeLabel,
			int lastModifiedUserID)
		{
			return TitleExternalResourceTypeUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleExternalResourceTypeID, externalResourceTypeName, externalResourceTypeLabel, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.TitleExternalResourceType. Returns an object of type TitleExternalResourceType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <param name="externalResourceTypeName"></param>
		/// <param name="externalResourceTypeLabel"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleExternalResourceType.</returns>
		public TitleExternalResourceType TitleExternalResourceTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleExternalResourceTypeID,
			string externalResourceTypeName,
			string externalResourceTypeLabel,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleExternalResourceTypeID", SqlDbType.Int, null, false, titleExternalResourceTypeID),
					CustomSqlHelper.CreateInputParameter("ExternalResourceTypeName", SqlDbType.VarChar, 100, false, externalResourceTypeName),
					CustomSqlHelper.CreateInputParameter("ExternalResourceTypeLabel", SqlDbType.VarChar, 100, false, externalResourceTypeLabel),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleExternalResourceType> helper = new CustomSqlHelper<TitleExternalResourceType>())
				{
					List<TitleExternalResourceType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleExternalResourceType o = list[0];
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
		/// Update values in dbo.TitleExternalResourceType. Returns an object of type TitleExternalResourceType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleExternalResourceType.</param>
		/// <returns>Object of type TitleExternalResourceType.</returns>
		public TitleExternalResourceType TitleExternalResourceTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleExternalResourceType value)
		{
			return TitleExternalResourceTypeUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.TitleExternalResourceType. Returns an object of type TitleExternalResourceType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleExternalResourceType.</param>
		/// <returns>Object of type TitleExternalResourceType.</returns>
		public TitleExternalResourceType TitleExternalResourceTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleExternalResourceType value)
		{
			return TitleExternalResourceTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleExternalResourceTypeID,
				value.ExternalResourceTypeName,
				value.ExternalResourceTypeLabel,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.TitleExternalResourceType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleExternalResourceType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleExternalResourceType.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleExternalResourceType>.</returns>
		public CustomDataAccessStatus<TitleExternalResourceType> TitleExternalResourceTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleExternalResourceType value , int userId )
		{
			return TitleExternalResourceTypeManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.TitleExternalResourceType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleExternalResourceType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleExternalResourceType.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleExternalResourceType>.</returns>
		public CustomDataAccessStatus<TitleExternalResourceType> TitleExternalResourceTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleExternalResourceType value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TitleExternalResourceType returnValue = TitleExternalResourceTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ExternalResourceTypeName,
						value.ExternalResourceTypeLabel,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TitleExternalResourceType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleExternalResourceTypeDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleExternalResourceTypeID))
				{
				return new CustomDataAccessStatus<TitleExternalResourceType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleExternalResourceType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TitleExternalResourceType returnValue = TitleExternalResourceTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleExternalResourceTypeID,
						value.ExternalResourceTypeName,
						value.ExternalResourceTypeLabel,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TitleExternalResourceType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleExternalResourceType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


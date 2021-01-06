
// Generated 1/5/2021 3:27:12 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleAssociation_TitleIdentifierDAL is based upon dbo.TitleAssociation_TitleIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleAssociation_TitleIdentifierDAL
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
	partial class TitleAssociation_TitleIdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociation_TitleIdentifierID)
		{
			return TitleAssociation_TitleIdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleAssociation_TitleIdentifierID );
		}
			
		/// <summary>
		/// Select values from dbo.TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociation_TitleIdentifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociation_TitleIdentifierID", SqlDbType.Int, null, false, titleAssociation_TitleIdentifierID)))
			{
				using (CustomSqlHelper<TitleAssociation_TitleIdentifier> helper = new CustomSqlHelper<TitleAssociation_TitleIdentifier>())
				{
					List<TitleAssociation_TitleIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociation_TitleIdentifier o = list[0];
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
		/// Select values from dbo.TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleAssociation_TitleIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociation_TitleIdentifierID)
		{
			return TitleAssociation_TitleIdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleAssociation_TitleIdentifierID );
		}
		
		/// <summary>
		/// Select values from dbo.TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleAssociation_TitleIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociation_TitleIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleAssociation_TitleIdentifierID", SqlDbType.Int, null, false, titleAssociation_TitleIdentifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociationID"></param>
		/// <param name="titleIdentifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociationID,
			int titleIdentifierID,
			string identifierValue,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return TitleAssociation_TitleIdentifierInsertAuto( sqlConnection, sqlTransaction, "BHL", titleAssociationID, titleIdentifierID, identifierValue, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociationID"></param>
		/// <param name="titleIdentifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociationID,
			int titleIdentifierID,
			string identifierValue,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleAssociation_TitleIdentifierID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleAssociationID", SqlDbType.Int, null, false, titleAssociationID),
					CustomSqlHelper.CreateInputParameter("TitleIdentifierID", SqlDbType.Int, null, false, titleIdentifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.VarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleAssociation_TitleIdentifier> helper = new CustomSqlHelper<TitleAssociation_TitleIdentifier>())
				{
					List<TitleAssociation_TitleIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociation_TitleIdentifier o = list[0];
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
		/// Insert values into dbo.TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociation_TitleIdentifier value)
		{
			return TitleAssociation_TitleIdentifierInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociation_TitleIdentifier value)
		{
			return TitleAssociation_TitleIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleAssociationID,
				value.TitleIdentifierID,
				value.IdentifierValue,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleAssociation_TitleIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociation_TitleIdentifierID)
		{
			return TitleAssociation_TitleIdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleAssociation_TitleIdentifierID );
		}
		
		/// <summary>
		/// Delete values from dbo.TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleAssociation_TitleIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociation_TitleIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociation_TitleIdentifierID", SqlDbType.Int, null, false, titleAssociation_TitleIdentifierID), 
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
		/// Update values in dbo.TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <param name="titleAssociationID"></param>
		/// <param name="titleIdentifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociation_TitleIdentifierID,
			int titleAssociationID,
			int titleIdentifierID,
			string identifierValue,
			int? lastModifiedUserID)
		{
			return TitleAssociation_TitleIdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleAssociation_TitleIdentifierID, titleAssociationID, titleIdentifierID, identifierValue, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <param name="titleAssociationID"></param>
		/// <param name="titleIdentifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociation_TitleIdentifierID,
			int titleAssociationID,
			int titleIdentifierID,
			string identifierValue,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociation_TitleIdentifierID", SqlDbType.Int, null, false, titleAssociation_TitleIdentifierID),
					CustomSqlHelper.CreateInputParameter("TitleAssociationID", SqlDbType.Int, null, false, titleAssociationID),
					CustomSqlHelper.CreateInputParameter("TitleIdentifierID", SqlDbType.Int, null, false, titleIdentifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.VarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleAssociation_TitleIdentifier> helper = new CustomSqlHelper<TitleAssociation_TitleIdentifier>())
				{
					List<TitleAssociation_TitleIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociation_TitleIdentifier o = list[0];
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
		/// Update values in dbo.TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociation_TitleIdentifier value)
		{
			return TitleAssociation_TitleIdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociation_TitleIdentifier value)
		{
			return TitleAssociation_TitleIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleAssociation_TitleIdentifierID,
				value.TitleAssociationID,
				value.TitleIdentifierID,
				value.IdentifierValue,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.TitleAssociation_TitleIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleAssociation_TitleIdentifier>.</returns>
		public CustomDataAccessStatus<TitleAssociation_TitleIdentifier> TitleAssociation_TitleIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociation_TitleIdentifier value , int userId )
		{
			return TitleAssociation_TitleIdentifierManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.TitleAssociation_TitleIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleAssociation_TitleIdentifier>.</returns>
		public CustomDataAccessStatus<TitleAssociation_TitleIdentifier> TitleAssociation_TitleIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociation_TitleIdentifier value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TitleAssociation_TitleIdentifier returnValue = TitleAssociation_TitleIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAssociationID,
						value.TitleIdentifierID,
						value.IdentifierValue,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TitleAssociation_TitleIdentifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleAssociation_TitleIdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAssociation_TitleIdentifierID))
				{
				return new CustomDataAccessStatus<TitleAssociation_TitleIdentifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleAssociation_TitleIdentifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TitleAssociation_TitleIdentifier returnValue = TitleAssociation_TitleIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAssociation_TitleIdentifierID,
						value.TitleAssociationID,
						value.TitleIdentifierID,
						value.IdentifierValue,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TitleAssociation_TitleIdentifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleAssociation_TitleIdentifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


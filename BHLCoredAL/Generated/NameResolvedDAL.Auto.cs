
// Generated 12/10/2012 3:05:47 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class NameResolvedDAL is based upon NameResolved.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class NameResolvedDAL
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
	partial class NameResolvedDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from NameResolved by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameResolvedID"></param>
		/// <returns>Object of type NameResolved.</returns>
		public NameResolved NameResolvedSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameResolvedID)
		{
			return NameResolvedSelectAuto(	sqlConnection, sqlTransaction, "BHL",	nameResolvedID );
		}
			
		/// <summary>
		/// Select values from NameResolved by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameResolvedID"></param>
		/// <returns>Object of type NameResolved.</returns>
		public NameResolved NameResolvedSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameResolvedID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameResolvedID", SqlDbType.Int, null, false, nameResolvedID)))
			{
				using (CustomSqlHelper<NameResolved> helper = new CustomSqlHelper<NameResolved>())
				{
					CustomGenericList<NameResolved> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameResolved o = list[0];
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
		/// Select values from NameResolved by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameResolvedID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> NameResolvedSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameResolvedID)
		{
			return NameResolvedSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", nameResolvedID );
		}
		
		/// <summary>
		/// Select values from NameResolved by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameResolvedID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> NameResolvedSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameResolvedID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("NameResolvedID", SqlDbType.Int, null, false, nameResolvedID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into NameResolved.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="resolvedNameString"></param>
		/// <param name="canonicalNameString"></param>
		/// <param name="isPreferred"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameResolved.</returns>
		public NameResolved NameResolvedInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string resolvedNameString,
			string canonicalNameString,
			short isPreferred,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return NameResolvedInsertAuto( sqlConnection, sqlTransaction, "BHL", resolvedNameString, canonicalNameString, isPreferred, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into NameResolved.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="resolvedNameString"></param>
		/// <param name="canonicalNameString"></param>
		/// <param name="isPreferred"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameResolved.</returns>
		public NameResolved NameResolvedInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string resolvedNameString,
			string canonicalNameString,
			short isPreferred,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("NameResolvedID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ResolvedNameString", SqlDbType.NVarChar, 100, false, resolvedNameString),
					CustomSqlHelper.CreateInputParameter("CanonicalNameString", SqlDbType.NVarChar, 100, false, canonicalNameString),
					CustomSqlHelper.CreateInputParameter("IsPreferred", SqlDbType.SmallInt, null, false, isPreferred),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NameResolved> helper = new CustomSqlHelper<NameResolved>())
				{
					CustomGenericList<NameResolved> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameResolved o = list[0];
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
		/// Insert values into NameResolved. Returns an object of type NameResolved.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameResolved.</param>
		/// <returns>Object of type NameResolved.</returns>
		public NameResolved NameResolvedInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameResolved value)
		{
			return NameResolvedInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into NameResolved. Returns an object of type NameResolved.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameResolved.</param>
		/// <returns>Object of type NameResolved.</returns>
		public NameResolved NameResolvedInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameResolved value)
		{
			return NameResolvedInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ResolvedNameString,
				value.CanonicalNameString,
				value.IsPreferred,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from NameResolved by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameResolvedID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameResolvedDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameResolvedID)
		{
			return NameResolvedDeleteAuto( sqlConnection, sqlTransaction, "BHL", nameResolvedID );
		}
		
		/// <summary>
		/// Delete values from NameResolved by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameResolvedID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameResolvedDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameResolvedID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameResolvedID", SqlDbType.Int, null, false, nameResolvedID), 
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
		/// Update values in NameResolved. Returns an object of type NameResolved.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameResolvedID"></param>
		/// <param name="resolvedNameString"></param>
		/// <param name="canonicalNameString"></param>
		/// <param name="isPreferred"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameResolved.</returns>
		public NameResolved NameResolvedUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameResolvedID,
			string resolvedNameString,
			string canonicalNameString,
			short isPreferred,
			int? lastModifiedUserID)
		{
			return NameResolvedUpdateAuto( sqlConnection, sqlTransaction, "BHL", nameResolvedID, resolvedNameString, canonicalNameString, isPreferred, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in NameResolved. Returns an object of type NameResolved.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameResolvedID"></param>
		/// <param name="resolvedNameString"></param>
		/// <param name="canonicalNameString"></param>
		/// <param name="isPreferred"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameResolved.</returns>
		public NameResolved NameResolvedUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameResolvedID,
			string resolvedNameString,
			string canonicalNameString,
			short isPreferred,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameResolvedID", SqlDbType.Int, null, false, nameResolvedID),
					CustomSqlHelper.CreateInputParameter("ResolvedNameString", SqlDbType.NVarChar, 100, false, resolvedNameString),
					CustomSqlHelper.CreateInputParameter("CanonicalNameString", SqlDbType.NVarChar, 100, false, canonicalNameString),
					CustomSqlHelper.CreateInputParameter("IsPreferred", SqlDbType.SmallInt, null, false, isPreferred),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NameResolved> helper = new CustomSqlHelper<NameResolved>())
				{
					CustomGenericList<NameResolved> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameResolved o = list[0];
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
		/// Update values in NameResolved. Returns an object of type NameResolved.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameResolved.</param>
		/// <returns>Object of type NameResolved.</returns>
		public NameResolved NameResolvedUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameResolved value)
		{
			return NameResolvedUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in NameResolved. Returns an object of type NameResolved.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameResolved.</param>
		/// <returns>Object of type NameResolved.</returns>
		public NameResolved NameResolvedUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameResolved value)
		{
			return NameResolvedUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NameResolvedID,
				value.ResolvedNameString,
				value.CanonicalNameString,
				value.IsPreferred,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage NameResolved object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in NameResolved.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameResolved.</param>
		/// <returns>Object of type CustomDataAccessStatus<NameResolved>.</returns>
		public CustomDataAccessStatus<NameResolved> NameResolvedManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameResolved value , int userId )
		{
			return NameResolvedManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage NameResolved object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in NameResolved.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameResolved.</param>
		/// <returns>Object of type CustomDataAccessStatus<NameResolved>.</returns>
		public CustomDataAccessStatus<NameResolved> NameResolvedManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameResolved value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				NameResolved returnValue = NameResolvedInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ResolvedNameString,
						value.CanonicalNameString,
						value.IsPreferred,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<NameResolved>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (NameResolvedDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameResolvedID))
				{
				return new CustomDataAccessStatus<NameResolved>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<NameResolved>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				NameResolved returnValue = NameResolvedUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameResolvedID,
						value.ResolvedNameString,
						value.CanonicalNameString,
						value.IsPreferred,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<NameResolved>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<NameResolved>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

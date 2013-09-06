
// Generated 12/10/2012 3:05:47 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class NameDAL is based upon Name.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class NameDAL
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
	partial class NameDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from Name by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameID"></param>
		/// <returns>Object of type Name.</returns>
		public Name NameSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameID)
		{
			return NameSelectAuto(	sqlConnection, sqlTransaction, "BHL",	nameID );
		}
			
		/// <summary>
		/// Select values from Name by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameID"></param>
		/// <returns>Object of type Name.</returns>
		public Name NameSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameID", SqlDbType.Int, null, false, nameID)))
			{
				using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
				{
					CustomGenericList<Name> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Name o = list[0];
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
		/// Select values from Name by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> NameSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameID)
		{
			return NameSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", nameID );
		}
		
		/// <summary>
		/// Select values from Name by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> NameSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("NameID", SqlDbType.Int, null, false, nameID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into Name.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameSourceID"></param>
		/// <param name="nameString"></param>
		/// <param name="isActive"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="nameResolvedID"></param>
		/// <returns>Object of type Name.</returns>
		public Name NameInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameSourceID,
			string nameString,
			short isActive,
			int? creationUserID,
			int? lastModifiedUserID,
			int? nameResolvedID)
		{
			return NameInsertAuto( sqlConnection, sqlTransaction, "BHL", nameSourceID, nameString, isActive, creationUserID, lastModifiedUserID, nameResolvedID );
		}
		
		/// <summary>
		/// Insert values into Name.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameSourceID"></param>
		/// <param name="nameString"></param>
		/// <param name="isActive"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="nameResolvedID"></param>
		/// <returns>Object of type Name.</returns>
		public Name NameInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameSourceID,
			string nameString,
			short isActive,
			int? creationUserID,
			int? lastModifiedUserID,
			int? nameResolvedID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("NameID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("NameSourceID", SqlDbType.Int, null, false, nameSourceID),
					CustomSqlHelper.CreateInputParameter("NameString", SqlDbType.NVarChar, 100, false, nameString),
					CustomSqlHelper.CreateInputParameter("IsActive", SqlDbType.SmallInt, null, false, isActive),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("NameResolvedID", SqlDbType.Int, null, true, nameResolvedID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
				{
					CustomGenericList<Name> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Name o = list[0];
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
		/// Insert values into Name. Returns an object of type Name.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Name.</param>
		/// <returns>Object of type Name.</returns>
		public Name NameInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Name value)
		{
			return NameInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into Name. Returns an object of type Name.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Name.</param>
		/// <returns>Object of type Name.</returns>
		public Name NameInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Name value)
		{
			return NameInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NameSourceID,
				value.NameString,
				value.IsActive,
				value.CreationUserID,
				value.LastModifiedUserID,
				value.NameResolvedID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Name by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameID)
		{
			return NameDeleteAuto( sqlConnection, sqlTransaction, "BHL", nameID );
		}
		
		/// <summary>
		/// Delete values from Name by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameID", SqlDbType.Int, null, false, nameID), 
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
		/// Update values in Name. Returns an object of type Name.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameID"></param>
		/// <param name="nameSourceID"></param>
		/// <param name="nameString"></param>
		/// <param name="isActive"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="nameResolvedID"></param>
		/// <returns>Object of type Name.</returns>
		public Name NameUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameID,
			int nameSourceID,
			string nameString,
			short isActive,
			int? lastModifiedUserID,
			int? nameResolvedID)
		{
			return NameUpdateAuto( sqlConnection, sqlTransaction, "BHL", nameID, nameSourceID, nameString, isActive, lastModifiedUserID, nameResolvedID);
		}
		
		/// <summary>
		/// Update values in Name. Returns an object of type Name.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameID"></param>
		/// <param name="nameSourceID"></param>
		/// <param name="nameString"></param>
		/// <param name="isActive"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="nameResolvedID"></param>
		/// <returns>Object of type Name.</returns>
		public Name NameUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameID,
			int nameSourceID,
			string nameString,
			short isActive,
			int? lastModifiedUserID,
			int? nameResolvedID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameID", SqlDbType.Int, null, false, nameID),
					CustomSqlHelper.CreateInputParameter("NameSourceID", SqlDbType.Int, null, false, nameSourceID),
					CustomSqlHelper.CreateInputParameter("NameString", SqlDbType.NVarChar, 100, false, nameString),
					CustomSqlHelper.CreateInputParameter("IsActive", SqlDbType.SmallInt, null, false, isActive),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("NameResolvedID", SqlDbType.Int, null, true, nameResolvedID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
				{
					CustomGenericList<Name> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Name o = list[0];
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
		/// Update values in Name. Returns an object of type Name.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Name.</param>
		/// <returns>Object of type Name.</returns>
		public Name NameUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Name value)
		{
			return NameUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in Name. Returns an object of type Name.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Name.</param>
		/// <returns>Object of type Name.</returns>
		public Name NameUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Name value)
		{
			return NameUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NameID,
				value.NameSourceID,
				value.NameString,
				value.IsActive,
				value.LastModifiedUserID,
				value.NameResolvedID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Name object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Name.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Name.</param>
		/// <returns>Object of type CustomDataAccessStatus<Name>.</returns>
		public CustomDataAccessStatus<Name> NameManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Name value , int userId )
		{
			return NameManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage Name object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Name.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Name.</param>
		/// <returns>Object of type CustomDataAccessStatus<Name>.</returns>
		public CustomDataAccessStatus<Name> NameManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Name value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Name returnValue = NameInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameSourceID,
						value.NameString,
						value.IsActive,
						value.CreationUserID,
						value.LastModifiedUserID,
						value.NameResolvedID);
				
				return new CustomDataAccessStatus<Name>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (NameDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameID))
				{
				return new CustomDataAccessStatus<Name>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Name>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				Name returnValue = NameUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameID,
						value.NameSourceID,
						value.NameString,
						value.IsActive,
						value.LastModifiedUserID,
						value.NameResolvedID);
					
				return new CustomDataAccessStatus<Name>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Name>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

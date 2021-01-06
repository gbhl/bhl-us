
// Generated 1/5/2021 3:27:27 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleVariantDAL is based upon dbo.TitleVariant.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleVariantDAL
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
	partial class TitleVariantDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.TitleVariant by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleVariantID"></param>
		/// <returns>Object of type TitleVariant.</returns>
		public TitleVariant TitleVariantSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleVariantID)
		{
			return TitleVariantSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleVariantID );
		}
			
		/// <summary>
		/// Select values from dbo.TitleVariant by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleVariantID"></param>
		/// <returns>Object of type TitleVariant.</returns>
		public TitleVariant TitleVariantSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleVariantID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleVariantSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleVariantID", SqlDbType.Int, null, false, titleVariantID)))
			{
				using (CustomSqlHelper<TitleVariant> helper = new CustomSqlHelper<TitleVariant>())
				{
					List<TitleVariant> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleVariant o = list[0];
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
		/// Select values from dbo.TitleVariant by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleVariantID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleVariantSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleVariantID)
		{
			return TitleVariantSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleVariantID );
		}
		
		/// <summary>
		/// Select values from dbo.TitleVariant by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleVariantID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleVariantSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleVariantID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleVariantSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleVariantID", SqlDbType.Int, null, false, titleVariantID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.TitleVariant.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="titleVariantTypeID"></param>
		/// <param name="title"></param>
		/// <param name="titleRemainder"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleVariant.</returns>
		public TitleVariant TitleVariantInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			int titleVariantTypeID,
			string title,
			string titleRemainder,
			string partNumber,
			string partName,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return TitleVariantInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, titleVariantTypeID, title, titleRemainder, partNumber, partName, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.TitleVariant.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="titleVariantTypeID"></param>
		/// <param name="title"></param>
		/// <param name="titleRemainder"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleVariant.</returns>
		public TitleVariant TitleVariantInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			int titleVariantTypeID,
			string title,
			string titleRemainder,
			string partNumber,
			string partName,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleVariantInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleVariantID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("TitleVariantTypeID", SqlDbType.Int, null, false, titleVariantTypeID),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 1073741823, false, title),
					CustomSqlHelper.CreateInputParameter("TitleRemainder", SqlDbType.NVarChar, 1073741823, false, titleRemainder),
					CustomSqlHelper.CreateInputParameter("PartNumber", SqlDbType.NVarChar, 255, false, partNumber),
					CustomSqlHelper.CreateInputParameter("PartName", SqlDbType.NVarChar, 255, false, partName),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleVariant> helper = new CustomSqlHelper<TitleVariant>())
				{
					List<TitleVariant> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleVariant o = list[0];
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
		/// Insert values into dbo.TitleVariant. Returns an object of type TitleVariant.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleVariant.</param>
		/// <returns>Object of type TitleVariant.</returns>
		public TitleVariant TitleVariantInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleVariant value)
		{
			return TitleVariantInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.TitleVariant. Returns an object of type TitleVariant.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleVariant.</param>
		/// <returns>Object of type TitleVariant.</returns>
		public TitleVariant TitleVariantInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleVariant value)
		{
			return TitleVariantInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.TitleVariantTypeID,
				value.Title,
				value.TitleRemainder,
				value.PartNumber,
				value.PartName,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.TitleVariant by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleVariantID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleVariantDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleVariantID)
		{
			return TitleVariantDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleVariantID );
		}
		
		/// <summary>
		/// Delete values from dbo.TitleVariant by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleVariantID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleVariantDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleVariantID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleVariantDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleVariantID", SqlDbType.Int, null, false, titleVariantID), 
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
		/// Update values in dbo.TitleVariant. Returns an object of type TitleVariant.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleVariantID"></param>
		/// <param name="titleID"></param>
		/// <param name="titleVariantTypeID"></param>
		/// <param name="title"></param>
		/// <param name="titleRemainder"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleVariant.</returns>
		public TitleVariant TitleVariantUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleVariantID,
			int titleID,
			int titleVariantTypeID,
			string title,
			string titleRemainder,
			string partNumber,
			string partName,
			int? lastModifiedUserID)
		{
			return TitleVariantUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleVariantID, titleID, titleVariantTypeID, title, titleRemainder, partNumber, partName, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.TitleVariant. Returns an object of type TitleVariant.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleVariantID"></param>
		/// <param name="titleID"></param>
		/// <param name="titleVariantTypeID"></param>
		/// <param name="title"></param>
		/// <param name="titleRemainder"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleVariant.</returns>
		public TitleVariant TitleVariantUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleVariantID,
			int titleID,
			int titleVariantTypeID,
			string title,
			string titleRemainder,
			string partNumber,
			string partName,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleVariantUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleVariantID", SqlDbType.Int, null, false, titleVariantID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("TitleVariantTypeID", SqlDbType.Int, null, false, titleVariantTypeID),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 1073741823, false, title),
					CustomSqlHelper.CreateInputParameter("TitleRemainder", SqlDbType.NVarChar, 1073741823, false, titleRemainder),
					CustomSqlHelper.CreateInputParameter("PartNumber", SqlDbType.NVarChar, 255, false, partNumber),
					CustomSqlHelper.CreateInputParameter("PartName", SqlDbType.NVarChar, 255, false, partName),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleVariant> helper = new CustomSqlHelper<TitleVariant>())
				{
					List<TitleVariant> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleVariant o = list[0];
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
		/// Update values in dbo.TitleVariant. Returns an object of type TitleVariant.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleVariant.</param>
		/// <returns>Object of type TitleVariant.</returns>
		public TitleVariant TitleVariantUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleVariant value)
		{
			return TitleVariantUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.TitleVariant. Returns an object of type TitleVariant.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleVariant.</param>
		/// <returns>Object of type TitleVariant.</returns>
		public TitleVariant TitleVariantUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleVariant value)
		{
			return TitleVariantUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleVariantID,
				value.TitleID,
				value.TitleVariantTypeID,
				value.Title,
				value.TitleRemainder,
				value.PartNumber,
				value.PartName,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.TitleVariant object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleVariant.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleVariant.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleVariant>.</returns>
		public CustomDataAccessStatus<TitleVariant> TitleVariantManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleVariant value , int userId )
		{
			return TitleVariantManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.TitleVariant object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleVariant.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleVariant.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleVariant>.</returns>
		public CustomDataAccessStatus<TitleVariant> TitleVariantManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleVariant value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TitleVariant returnValue = TitleVariantInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.TitleVariantTypeID,
						value.Title,
						value.TitleRemainder,
						value.PartNumber,
						value.PartName,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TitleVariant>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleVariantDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleVariantID))
				{
				return new CustomDataAccessStatus<TitleVariant>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleVariant>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TitleVariant returnValue = TitleVariantUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleVariantID,
						value.TitleID,
						value.TitleVariantTypeID,
						value.Title,
						value.TitleRemainder,
						value.PartNumber,
						value.PartName,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TitleVariant>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleVariant>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


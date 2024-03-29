
// Generated 1/5/2021 2:13:41 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IADCMetadataDAL is based upon dbo.IADCMetadata.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IADCMetadataDAL
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
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
{
	partial class IADCMetadataDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IADCMetadata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dCMetadataID"></param>
		/// <returns>Object of type IADCMetadata.</returns>
		public IADCMetadata IADCMetadataSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dCMetadataID)
		{
			return IADCMetadataSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	dCMetadataID );
		}
			
		/// <summary>
		/// Select values from dbo.IADCMetadata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dCMetadataID"></param>
		/// <returns>Object of type IADCMetadata.</returns>
		public IADCMetadata IADCMetadataSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dCMetadataID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IADCMetadataSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DCMetadataID", SqlDbType.Int, null, false, dCMetadataID)))
			{
				using (CustomSqlHelper<IADCMetadata> helper = new CustomSqlHelper<IADCMetadata>())
				{
					List<IADCMetadata> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IADCMetadata o = list[0];
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
		/// Select values from dbo.IADCMetadata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dCMetadataID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IADCMetadataSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dCMetadataID)
		{
			return IADCMetadataSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", dCMetadataID );
		}
		
		/// <summary>
		/// Select values from dbo.IADCMetadata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dCMetadataID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IADCMetadataSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dCMetadataID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IADCMetadataSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("DCMetadataID", SqlDbType.Int, null, false, dCMetadataID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IADCMetadata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="dCElementName"></param>
		/// <param name="dCElementValue"></param>
		/// <param name="source"></param>
		/// <returns>Object of type IADCMetadata.</returns>
		public IADCMetadata IADCMetadataInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string dCElementName,
			string dCElementValue,
			string source)
		{
			return IADCMetadataInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, dCElementName, dCElementValue, source );
		}
		
		/// <summary>
		/// Insert values into dbo.IADCMetadata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="dCElementName"></param>
		/// <param name="dCElementValue"></param>
		/// <param name="source"></param>
		/// <returns>Object of type IADCMetadata.</returns>
		public IADCMetadata IADCMetadataInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string dCElementName,
			string dCElementValue,
			string source)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IADCMetadataInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("DCMetadataID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("DCElementName", SqlDbType.NVarChar, 15, false, dCElementName),
					CustomSqlHelper.CreateInputParameter("DCElementValue", SqlDbType.NVarChar, 500, false, dCElementValue),
					CustomSqlHelper.CreateInputParameter("Source", SqlDbType.NVarChar, 50, false, source), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IADCMetadata> helper = new CustomSqlHelper<IADCMetadata>())
				{
					List<IADCMetadata> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IADCMetadata o = list[0];
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
		/// Insert values into dbo.IADCMetadata. Returns an object of type IADCMetadata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IADCMetadata.</param>
		/// <returns>Object of type IADCMetadata.</returns>
		public IADCMetadata IADCMetadataInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IADCMetadata value)
		{
			return IADCMetadataInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IADCMetadata. Returns an object of type IADCMetadata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IADCMetadata.</param>
		/// <returns>Object of type IADCMetadata.</returns>
		public IADCMetadata IADCMetadataInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IADCMetadata value)
		{
			return IADCMetadataInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.DCElementName,
				value.DCElementValue,
				value.Source);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IADCMetadata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dCMetadataID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IADCMetadataDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dCMetadataID)
		{
			return IADCMetadataDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", dCMetadataID );
		}
		
		/// <summary>
		/// Delete values from dbo.IADCMetadata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dCMetadataID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IADCMetadataDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dCMetadataID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IADCMetadataDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DCMetadataID", SqlDbType.Int, null, false, dCMetadataID), 
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
		/// Update values in dbo.IADCMetadata. Returns an object of type IADCMetadata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dCMetadataID"></param>
		/// <param name="itemID"></param>
		/// <param name="dCElementName"></param>
		/// <param name="dCElementValue"></param>
		/// <param name="source"></param>
		/// <returns>Object of type IADCMetadata.</returns>
		public IADCMetadata IADCMetadataUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dCMetadataID,
			int itemID,
			string dCElementName,
			string dCElementValue,
			string source)
		{
			return IADCMetadataUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", dCMetadataID, itemID, dCElementName, dCElementValue, source);
		}
		
		/// <summary>
		/// Update values in dbo.IADCMetadata. Returns an object of type IADCMetadata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dCMetadataID"></param>
		/// <param name="itemID"></param>
		/// <param name="dCElementName"></param>
		/// <param name="dCElementValue"></param>
		/// <param name="source"></param>
		/// <returns>Object of type IADCMetadata.</returns>
		public IADCMetadata IADCMetadataUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dCMetadataID,
			int itemID,
			string dCElementName,
			string dCElementValue,
			string source)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IADCMetadataUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DCMetadataID", SqlDbType.Int, null, false, dCMetadataID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("DCElementName", SqlDbType.NVarChar, 15, false, dCElementName),
					CustomSqlHelper.CreateInputParameter("DCElementValue", SqlDbType.NVarChar, 500, false, dCElementValue),
					CustomSqlHelper.CreateInputParameter("Source", SqlDbType.NVarChar, 50, false, source), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IADCMetadata> helper = new CustomSqlHelper<IADCMetadata>())
				{
					List<IADCMetadata> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IADCMetadata o = list[0];
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
		/// Update values in dbo.IADCMetadata. Returns an object of type IADCMetadata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IADCMetadata.</param>
		/// <returns>Object of type IADCMetadata.</returns>
		public IADCMetadata IADCMetadataUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IADCMetadata value)
		{
			return IADCMetadataUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IADCMetadata. Returns an object of type IADCMetadata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IADCMetadata.</param>
		/// <returns>Object of type IADCMetadata.</returns>
		public IADCMetadata IADCMetadataUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IADCMetadata value)
		{
			return IADCMetadataUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.DCMetadataID,
				value.ItemID,
				value.DCElementName,
				value.DCElementValue,
				value.Source);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IADCMetadata object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IADCMetadata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IADCMetadata.</param>
		/// <returns>Object of type CustomDataAccessStatus<IADCMetadata>.</returns>
		public CustomDataAccessStatus<IADCMetadata> IADCMetadataManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IADCMetadata value  )
		{
			return IADCMetadataManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IADCMetadata object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IADCMetadata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IADCMetadata.</param>
		/// <returns>Object of type CustomDataAccessStatus<IADCMetadata>.</returns>
		public CustomDataAccessStatus<IADCMetadata> IADCMetadataManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IADCMetadata value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IADCMetadata returnValue = IADCMetadataInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.DCElementName,
						value.DCElementValue,
						value.Source);
				
				return new CustomDataAccessStatus<IADCMetadata>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IADCMetadataDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DCMetadataID))
				{
				return new CustomDataAccessStatus<IADCMetadata>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IADCMetadata>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IADCMetadata returnValue = IADCMetadataUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DCMetadataID,
						value.ItemID,
						value.DCElementName,
						value.DCElementValue,
						value.Source);
					
				return new CustomDataAccessStatus<IADCMetadata>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IADCMetadata>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


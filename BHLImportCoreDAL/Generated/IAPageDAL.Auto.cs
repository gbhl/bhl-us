
// Generated 1/5/2021 2:15:07 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAPageDAL is based upon dbo.IAPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAPageDAL
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
	partial class IAPageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IAPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID)
		{
			return IAPageSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	pageID );
		}
			
		/// <summary>
		/// Select values from dbo.IAPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAPageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
			{
				using (CustomSqlHelper<IAPage> helper = new CustomSqlHelper<IAPage>())
				{
					List<IAPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAPage o = list[0];
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
		/// Select values from dbo.IAPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IAPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID)
		{
			return IAPageSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", pageID );
		}
		
		/// <summary>
		/// Select values from dbo.IAPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IAPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAPageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="localFileName"></param>
		/// <param name="sequence"></param>
		/// <param name="externalUrl"></param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string localFileName,
			int? sequence,
			string externalUrl)
		{
			return IAPageInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, localFileName, sequence, externalUrl );
		}
		
		/// <summary>
		/// Insert values into dbo.IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="localFileName"></param>
		/// <param name="sequence"></param>
		/// <param name="externalUrl"></param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string localFileName,
			int? sequence,
			string externalUrl)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAPageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("LocalFileName", SqlDbType.NVarChar, 200, false, localFileName),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, true, sequence),
					CustomSqlHelper.CreateInputParameter("ExternalUrl", SqlDbType.NVarChar, 500, true, externalUrl), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAPage> helper = new CustomSqlHelper<IAPage>())
				{
					List<IAPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAPage o = list[0];
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
		/// Insert values into dbo.IAPage. Returns an object of type IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAPage.</param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAPage value)
		{
			return IAPageInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IAPage. Returns an object of type IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAPage.</param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAPage value)
		{
			return IAPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.LocalFileName,
				value.Sequence,
				value.ExternalUrl);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IAPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID)
		{
			return IAPageDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", pageID );
		}
		
		/// <summary>
		/// Delete values from dbo.IAPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAPageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID), 
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
		/// Update values in dbo.IAPage. Returns an object of type IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="itemID"></param>
		/// <param name="localFileName"></param>
		/// <param name="sequence"></param>
		/// <param name="externalUrl"></param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			int itemID,
			string localFileName,
			int? sequence,
			string externalUrl)
		{
			return IAPageUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", pageID, itemID, localFileName, sequence, externalUrl);
		}
		
		/// <summary>
		/// Update values in dbo.IAPage. Returns an object of type IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <param name="itemID"></param>
		/// <param name="localFileName"></param>
		/// <param name="sequence"></param>
		/// <param name="externalUrl"></param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID,
			int itemID,
			string localFileName,
			int? sequence,
			string externalUrl)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAPageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("LocalFileName", SqlDbType.NVarChar, 200, false, localFileName),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, true, sequence),
					CustomSqlHelper.CreateInputParameter("ExternalUrl", SqlDbType.NVarChar, 500, true, externalUrl), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAPage> helper = new CustomSqlHelper<IAPage>())
				{
					List<IAPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAPage o = list[0];
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
		/// Update values in dbo.IAPage. Returns an object of type IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAPage.</param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAPage value)
		{
			return IAPageUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IAPage. Returns an object of type IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAPage.</param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAPage value)
		{
			return IAPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageID,
				value.ItemID,
				value.LocalFileName,
				value.Sequence,
				value.ExternalUrl);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IAPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAPage>.</returns>
		public CustomDataAccessStatus<IAPage> IAPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAPage value  )
		{
			return IAPageManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IAPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAPage>.</returns>
		public CustomDataAccessStatus<IAPage> IAPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAPage value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAPage returnValue = IAPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.LocalFileName,
						value.Sequence,
						value.ExternalUrl);
				
				return new CustomDataAccessStatus<IAPage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAPageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageID))
				{
				return new CustomDataAccessStatus<IAPage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAPage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAPage returnValue = IAPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageID,
						value.ItemID,
						value.LocalFileName,
						value.Sequence,
						value.ExternalUrl);
					
				return new CustomDataAccessStatus<IAPage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAPage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


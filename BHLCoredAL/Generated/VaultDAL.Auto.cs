
// Generated 11/22/2024 1:27:47 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class VaultDAL is based upon dbo.Vault.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class VaultDAL
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
	partial class VaultDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Vault by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="vaultID"></param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int vaultID)
		{
			return VaultSelectAuto(	sqlConnection, sqlTransaction, "BHL",	vaultID );
		}
			
		/// <summary>
		/// Select values from dbo.Vault by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="vaultID"></param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int vaultID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("VaultSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, false, vaultID)))
			{
				using (CustomSqlHelper<Vault> helper = new CustomSqlHelper<Vault>())
				{
					List<Vault> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Vault o = list[0];
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
		/// Select values from dbo.Vault by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="vaultID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> VaultSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int vaultID)
		{
			return VaultSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", vaultID );
		}
		
		/// <summary>
		/// Select values from dbo.Vault by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="vaultID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> VaultSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int vaultID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("VaultSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, false, vaultID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="vaultID"></param>
		/// <param name="server"></param>
		/// <param name="folderShare"></param>
		/// <param name="webVirtualDirectory"></param>
		/// <param name="oCRFolderShare"></param>
		/// <param name="isCurrent"></param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int vaultID,
			string server,
			string folderShare,
			string webVirtualDirectory,
			string oCRFolderShare,
			byte isCurrent)
		{
			return VaultInsertAuto( sqlConnection, sqlTransaction, "BHL", vaultID, server, folderShare, webVirtualDirectory, oCRFolderShare, isCurrent );
		}
		
		/// <summary>
		/// Insert values into dbo.Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="vaultID"></param>
		/// <param name="server"></param>
		/// <param name="folderShare"></param>
		/// <param name="webVirtualDirectory"></param>
		/// <param name="oCRFolderShare"></param>
		/// <param name="isCurrent"></param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int vaultID,
			string server,
			string folderShare,
			string webVirtualDirectory,
			string oCRFolderShare,
			byte isCurrent)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("VaultInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, false, vaultID),
					CustomSqlHelper.CreateInputParameter("Server", SqlDbType.NVarChar, 30, true, server),
					CustomSqlHelper.CreateInputParameter("FolderShare", SqlDbType.NVarChar, 30, true, folderShare),
					CustomSqlHelper.CreateInputParameter("WebVirtualDirectory", SqlDbType.NVarChar, 30, true, webVirtualDirectory),
					CustomSqlHelper.CreateInputParameter("OCRFolderShare", SqlDbType.NVarChar, 100, true, oCRFolderShare),
					CustomSqlHelper.CreateInputParameter("IsCurrent", SqlDbType.TinyInt, null, false, isCurrent), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Vault> helper = new CustomSqlHelper<Vault>())
				{
					List<Vault> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Vault o = list[0];
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
		/// Insert values into dbo.Vault. Returns an object of type Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Vault.</param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Vault value)
		{
			return VaultInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Vault. Returns an object of type Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Vault.</param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Vault value)
		{
			return VaultInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.VaultID,
				value.Server,
				value.FolderShare,
				value.WebVirtualDirectory,
				value.OCRFolderShare,
				value.IsCurrent);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Vault by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="vaultID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool VaultDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int vaultID)
		{
			return VaultDeleteAuto( sqlConnection, sqlTransaction, "BHL", vaultID );
		}
		
		/// <summary>
		/// Delete values from dbo.Vault by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="vaultID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool VaultDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int vaultID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("VaultDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, false, vaultID), 
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
		/// Update values in dbo.Vault. Returns an object of type Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="vaultID"></param>
		/// <param name="server"></param>
		/// <param name="folderShare"></param>
		/// <param name="webVirtualDirectory"></param>
		/// <param name="oCRFolderShare"></param>
		/// <param name="isCurrent"></param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int vaultID,
			string server,
			string folderShare,
			string webVirtualDirectory,
			string oCRFolderShare,
			byte isCurrent)
		{
			return VaultUpdateAuto( sqlConnection, sqlTransaction, "BHL", vaultID, server, folderShare, webVirtualDirectory, oCRFolderShare, isCurrent);
		}
		
		/// <summary>
		/// Update values in dbo.Vault. Returns an object of type Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="vaultID"></param>
		/// <param name="server"></param>
		/// <param name="folderShare"></param>
		/// <param name="webVirtualDirectory"></param>
		/// <param name="oCRFolderShare"></param>
		/// <param name="isCurrent"></param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int vaultID,
			string server,
			string folderShare,
			string webVirtualDirectory,
			string oCRFolderShare,
			byte isCurrent)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("VaultUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, false, vaultID),
					CustomSqlHelper.CreateInputParameter("Server", SqlDbType.NVarChar, 30, true, server),
					CustomSqlHelper.CreateInputParameter("FolderShare", SqlDbType.NVarChar, 30, true, folderShare),
					CustomSqlHelper.CreateInputParameter("WebVirtualDirectory", SqlDbType.NVarChar, 30, true, webVirtualDirectory),
					CustomSqlHelper.CreateInputParameter("OCRFolderShare", SqlDbType.NVarChar, 100, true, oCRFolderShare),
					CustomSqlHelper.CreateInputParameter("IsCurrent", SqlDbType.TinyInt, null, false, isCurrent), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Vault> helper = new CustomSqlHelper<Vault>())
				{
					List<Vault> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Vault o = list[0];
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
		/// Update values in dbo.Vault. Returns an object of type Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Vault.</param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Vault value)
		{
			return VaultUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Vault. Returns an object of type Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Vault.</param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Vault value)
		{
			return VaultUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.VaultID,
				value.Server,
				value.FolderShare,
				value.WebVirtualDirectory,
				value.OCRFolderShare,
				value.IsCurrent);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Vault object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Vault.</param>
		/// <returns>Object of type CustomDataAccessStatus<Vault>.</returns>
		public CustomDataAccessStatus<Vault> VaultManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Vault value  )
		{
			return VaultManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.Vault object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Vault.</param>
		/// <returns>Object of type CustomDataAccessStatus<Vault>.</returns>
		public CustomDataAccessStatus<Vault> VaultManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Vault value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Vault returnValue = VaultInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.VaultID,
						value.Server,
						value.FolderShare,
						value.WebVirtualDirectory,
						value.OCRFolderShare,
						value.IsCurrent);
				
				return new CustomDataAccessStatus<Vault>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (VaultDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.VaultID))
				{
				return new CustomDataAccessStatus<Vault>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Vault>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Vault returnValue = VaultUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.VaultID,
						value.Server,
						value.FolderShare,
						value.WebVirtualDirectory,
						value.OCRFolderShare,
						value.IsCurrent);
					
				return new CustomDataAccessStatus<Vault>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Vault>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


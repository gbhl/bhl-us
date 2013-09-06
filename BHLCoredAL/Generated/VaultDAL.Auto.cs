
// Generated 1/24/2008 10:03:58 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class VaultDAL is based upon Vault.

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
		/// Select values from Vault by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="vaultID">Unique identifier for each Vault entry.</param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int vaultID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("VaultSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, false, vaultID)))
			{
				using (CustomSqlHelper<Vault> helper = new CustomSqlHelper<Vault>())
				{
					CustomGenericList<Vault> list = helper.ExecuteReader(command);
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
		/// Select values from Vault by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="vaultID">Unique identifier for each Vault entry.</param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> VaultSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int vaultID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
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
		/// Insert values into Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="vaultID">Unique identifier for each Vault entry.</param>
		/// <param name="server">Name of server for this Vault entry.</param>
		/// <param name="folderShare">Name for the folder share for this Vault entry.</param>
		/// <param name="webVirtualDirectory">Name for the Web Virtual Directory for this Vault entry.</param>
		/// <param name="oCRFolderShare"></param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int vaultID,
			string server,
			string folderShare,
			string webVirtualDirectory,
			string oCRFolderShare)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("VaultInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, false, vaultID),
					CustomSqlHelper.CreateInputParameter("Server", SqlDbType.NVarChar, 30, true, server),
					CustomSqlHelper.CreateInputParameter("FolderShare", SqlDbType.NVarChar, 30, true, folderShare),
					CustomSqlHelper.CreateInputParameter("WebVirtualDirectory", SqlDbType.NVarChar, 30, true, webVirtualDirectory),
					CustomSqlHelper.CreateInputParameter("OCRFolderShare", SqlDbType.NVarChar, 100, true, oCRFolderShare), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Vault> helper = new CustomSqlHelper<Vault>())
				{
					CustomGenericList<Vault> list = helper.ExecuteReader(command);
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
		/// Insert values into Vault. Returns an object of type Vault.
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
			return VaultInsertAuto(sqlConnection, sqlTransaction, 
				value.VaultID,
				value.Server,
				value.FolderShare,
				value.WebVirtualDirectory,
				value.OCRFolderShare);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Vault by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="vaultID">Unique identifier for each Vault entry.</param>
		/// <returns>true if successful otherwise false.</returns>
		public bool VaultDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int vaultID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
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
		/// Update values in Vault. Returns an object of type Vault.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="vaultID">Unique identifier for each Vault entry.</param>
		/// <param name="server">Name of server for this Vault entry.</param>
		/// <param name="folderShare">Name for the folder share for this Vault entry.</param>
		/// <param name="webVirtualDirectory">Name for the Web Virtual Directory for this Vault entry.</param>
		/// <param name="oCRFolderShare"></param>
		/// <returns>Object of type Vault.</returns>
		public Vault VaultUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int vaultID,
			string server,
			string folderShare,
			string webVirtualDirectory,
			string oCRFolderShare)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("VaultUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, false, vaultID),
					CustomSqlHelper.CreateInputParameter("Server", SqlDbType.NVarChar, 30, true, server),
					CustomSqlHelper.CreateInputParameter("FolderShare", SqlDbType.NVarChar, 30, true, folderShare),
					CustomSqlHelper.CreateInputParameter("WebVirtualDirectory", SqlDbType.NVarChar, 30, true, webVirtualDirectory),
					CustomSqlHelper.CreateInputParameter("OCRFolderShare", SqlDbType.NVarChar, 100, true, oCRFolderShare), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Vault> helper = new CustomSqlHelper<Vault>())
				{
					CustomGenericList<Vault> list = helper.ExecuteReader(command);
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
		/// Update values in Vault. Returns an object of type Vault.
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
			return VaultUpdateAuto(sqlConnection, sqlTransaction,
				value.VaultID,
				value.Server,
				value.FolderShare,
				value.WebVirtualDirectory,
				value.OCRFolderShare);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Vault object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Vault.
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
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Vault returnValue = VaultInsertAuto(sqlConnection, sqlTransaction, 
					value.VaultID,
						value.Server,
						value.FolderShare,
						value.WebVirtualDirectory,
						value.OCRFolderShare);
				
				return new CustomDataAccessStatus<Vault>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (VaultDeleteAuto(sqlConnection, sqlTransaction, 
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
				
				Vault returnValue = VaultUpdateAuto(sqlConnection, sqlTransaction, 
					value.VaultID,
						value.Server,
						value.FolderShare,
						value.WebVirtualDirectory,
						value.OCRFolderShare);
					
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
// end of source generation

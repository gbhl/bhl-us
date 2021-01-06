
// Generated 1/5/2021 12:28:40 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemDAL is based upon dbo.Item.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.IAAnalysis.DAL
// {
// 		public partial class ItemDAL
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
using MOBOT.IAAnalysis.DataObjects;

#endregion using

namespace MOBOT.IAAnalysis.DAL
{
	partial class ItemDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return ItemSelectAuto(	sqlConnection, sqlTransaction, "IAAnalysis",	itemID );
		}
			
		/// <summary>
		/// Select values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
			{
				using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
				{
					List<Item> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Item o = list[0];
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
		/// Select values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return ItemSelectAutoRaw( sqlConnection, sqlTransaction, "IAAnalysis", itemID );
		}
		
		/// <summary>
		/// Select values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="identifier"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="sponsor"></param>
		/// <param name="contributor"></param>
		/// <param name="scanningCenter"></param>
		/// <param name="collectionLibrary"></param>
		/// <param name="callNumber"></param>
		/// <param name="imageCount"></param>
		/// <param name="curationState"></param>
		/// <param name="possibleCopyrightStatus"></param>
		/// <param name="volume"></param>
		/// <param name="scanDate"></param>
		/// <param name="addedDate"></param>
		/// <param name="publicDate"></param>
		/// <param name="updateDate"></param>
		/// <param name="sponsorDate"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="metaGetStatus"></param>
		/// <param name="marcGetStatus"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string identifier,
			string mARCLeader,
			string sponsor,
			string contributor,
			string scanningCenter,
			string collectionLibrary,
			string callNumber,
			int? imageCount,
			string curationState,
			string possibleCopyrightStatus,
			string volume,
			string scanDate,
			DateTime? addedDate,
			DateTime? publicDate,
			DateTime? updateDate,
			string sponsorDate,
			int itemStatusID,
			string metaGetStatus,
			string marcGetStatus)
		{
			return ItemInsertAuto( sqlConnection, sqlTransaction, "IAAnalysis", identifier, mARCLeader, sponsor, contributor, scanningCenter, collectionLibrary, callNumber, imageCount, curationState, possibleCopyrightStatus, volume, scanDate, addedDate, publicDate, updateDate, sponsorDate, itemStatusID, metaGetStatus, marcGetStatus );
		}
		
		/// <summary>
		/// Insert values into dbo.Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="identifier"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="sponsor"></param>
		/// <param name="contributor"></param>
		/// <param name="scanningCenter"></param>
		/// <param name="collectionLibrary"></param>
		/// <param name="callNumber"></param>
		/// <param name="imageCount"></param>
		/// <param name="curationState"></param>
		/// <param name="possibleCopyrightStatus"></param>
		/// <param name="volume"></param>
		/// <param name="scanDate"></param>
		/// <param name="addedDate"></param>
		/// <param name="publicDate"></param>
		/// <param name="updateDate"></param>
		/// <param name="sponsorDate"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="metaGetStatus"></param>
		/// <param name="marcGetStatus"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string identifier,
			string mARCLeader,
			string sponsor,
			string contributor,
			string scanningCenter,
			string collectionLibrary,
			string callNumber,
			int? imageCount,
			string curationState,
			string possibleCopyrightStatus,
			string volume,
			string scanDate,
			DateTime? addedDate,
			DateTime? publicDate,
			DateTime? updateDate,
			string sponsorDate,
			int itemStatusID,
			string metaGetStatus,
			string marcGetStatus)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("Identifier", SqlDbType.NVarChar, 50, false, identifier),
					CustomSqlHelper.CreateInputParameter("MARCLeader", SqlDbType.NVarChar, 200, false, mARCLeader),
					CustomSqlHelper.CreateInputParameter("Sponsor", SqlDbType.NVarChar, 50, false, sponsor),
					CustomSqlHelper.CreateInputParameter("Contributor", SqlDbType.NVarChar, 100, false, contributor),
					CustomSqlHelper.CreateInputParameter("ScanningCenter", SqlDbType.NVarChar, 50, false, scanningCenter),
					CustomSqlHelper.CreateInputParameter("CollectionLibrary", SqlDbType.NVarChar, 20, false, collectionLibrary),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 50, false, callNumber),
					CustomSqlHelper.CreateInputParameter("ImageCount", SqlDbType.Int, null, true, imageCount),
					CustomSqlHelper.CreateInputParameter("CurationState", SqlDbType.NVarChar, 50, false, curationState),
					CustomSqlHelper.CreateInputParameter("PossibleCopyrightStatus", SqlDbType.NVarChar, 50, false, possibleCopyrightStatus),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 200, false, volume),
					CustomSqlHelper.CreateInputParameter("ScanDate", SqlDbType.NVarChar, 50, false, scanDate),
					CustomSqlHelper.CreateInputParameter("AddedDate", SqlDbType.DateTime, null, true, addedDate),
					CustomSqlHelper.CreateInputParameter("PublicDate", SqlDbType.DateTime, null, true, publicDate),
					CustomSqlHelper.CreateInputParameter("UpdateDate", SqlDbType.DateTime, null, true, updateDate),
					CustomSqlHelper.CreateInputParameter("SponsorDate", SqlDbType.NVarChar, 50, true, sponsorDate),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("MetaGetStatus", SqlDbType.NVarChar, 30, false, metaGetStatus),
					CustomSqlHelper.CreateInputParameter("MarcGetStatus", SqlDbType.NVarChar, 30, false, marcGetStatus), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
				{
					List<Item> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Item o = list[0];
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
		/// Insert values into dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Item value)
		{
			return ItemInsertAuto(sqlConnection, sqlTransaction, "IAAnalysis", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Item value)
		{
			return ItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.Identifier,
				value.MARCLeader,
				value.Sponsor,
				value.Contributor,
				value.ScanningCenter,
				value.CollectionLibrary,
				value.CallNumber,
				value.ImageCount,
				value.CurationState,
				value.PossibleCopyrightStatus,
				value.Volume,
				value.ScanDate,
				value.AddedDate,
				value.PublicDate,
				value.UpdateDate,
				value.SponsorDate,
				value.ItemStatusID,
				value.MetaGetStatus,
				value.MarcGetStatus);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return ItemDeleteAuto( sqlConnection, sqlTransaction, "IAAnalysis", itemID );
		}
		
		/// <summary>
		/// Delete values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID), 
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
		/// Update values in dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="identifier"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="sponsor"></param>
		/// <param name="contributor"></param>
		/// <param name="scanningCenter"></param>
		/// <param name="collectionLibrary"></param>
		/// <param name="callNumber"></param>
		/// <param name="imageCount"></param>
		/// <param name="curationState"></param>
		/// <param name="possibleCopyrightStatus"></param>
		/// <param name="volume"></param>
		/// <param name="scanDate"></param>
		/// <param name="addedDate"></param>
		/// <param name="publicDate"></param>
		/// <param name="updateDate"></param>
		/// <param name="sponsorDate"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="metaGetStatus"></param>
		/// <param name="marcGetStatus"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string identifier,
			string mARCLeader,
			string sponsor,
			string contributor,
			string scanningCenter,
			string collectionLibrary,
			string callNumber,
			int? imageCount,
			string curationState,
			string possibleCopyrightStatus,
			string volume,
			string scanDate,
			DateTime? addedDate,
			DateTime? publicDate,
			DateTime? updateDate,
			string sponsorDate,
			int itemStatusID,
			string metaGetStatus,
			string marcGetStatus)
		{
			return ItemUpdateAuto( sqlConnection, sqlTransaction, "IAAnalysis", itemID, identifier, mARCLeader, sponsor, contributor, scanningCenter, collectionLibrary, callNumber, imageCount, curationState, possibleCopyrightStatus, volume, scanDate, addedDate, publicDate, updateDate, sponsorDate, itemStatusID, metaGetStatus, marcGetStatus);
		}
		
		/// <summary>
		/// Update values in dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="identifier"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="sponsor"></param>
		/// <param name="contributor"></param>
		/// <param name="scanningCenter"></param>
		/// <param name="collectionLibrary"></param>
		/// <param name="callNumber"></param>
		/// <param name="imageCount"></param>
		/// <param name="curationState"></param>
		/// <param name="possibleCopyrightStatus"></param>
		/// <param name="volume"></param>
		/// <param name="scanDate"></param>
		/// <param name="addedDate"></param>
		/// <param name="publicDate"></param>
		/// <param name="updateDate"></param>
		/// <param name="sponsorDate"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="metaGetStatus"></param>
		/// <param name="marcGetStatus"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string identifier,
			string mARCLeader,
			string sponsor,
			string contributor,
			string scanningCenter,
			string collectionLibrary,
			string callNumber,
			int? imageCount,
			string curationState,
			string possibleCopyrightStatus,
			string volume,
			string scanDate,
			DateTime? addedDate,
			DateTime? publicDate,
			DateTime? updateDate,
			string sponsorDate,
			int itemStatusID,
			string metaGetStatus,
			string marcGetStatus)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Identifier", SqlDbType.NVarChar, 50, false, identifier),
					CustomSqlHelper.CreateInputParameter("MARCLeader", SqlDbType.NVarChar, 200, false, mARCLeader),
					CustomSqlHelper.CreateInputParameter("Sponsor", SqlDbType.NVarChar, 50, false, sponsor),
					CustomSqlHelper.CreateInputParameter("Contributor", SqlDbType.NVarChar, 100, false, contributor),
					CustomSqlHelper.CreateInputParameter("ScanningCenter", SqlDbType.NVarChar, 50, false, scanningCenter),
					CustomSqlHelper.CreateInputParameter("CollectionLibrary", SqlDbType.NVarChar, 20, false, collectionLibrary),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 50, false, callNumber),
					CustomSqlHelper.CreateInputParameter("ImageCount", SqlDbType.Int, null, true, imageCount),
					CustomSqlHelper.CreateInputParameter("CurationState", SqlDbType.NVarChar, 50, false, curationState),
					CustomSqlHelper.CreateInputParameter("PossibleCopyrightStatus", SqlDbType.NVarChar, 50, false, possibleCopyrightStatus),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 200, false, volume),
					CustomSqlHelper.CreateInputParameter("ScanDate", SqlDbType.NVarChar, 50, false, scanDate),
					CustomSqlHelper.CreateInputParameter("AddedDate", SqlDbType.DateTime, null, true, addedDate),
					CustomSqlHelper.CreateInputParameter("PublicDate", SqlDbType.DateTime, null, true, publicDate),
					CustomSqlHelper.CreateInputParameter("UpdateDate", SqlDbType.DateTime, null, true, updateDate),
					CustomSqlHelper.CreateInputParameter("SponsorDate", SqlDbType.NVarChar, 50, true, sponsorDate),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("MetaGetStatus", SqlDbType.NVarChar, 30, false, metaGetStatus),
					CustomSqlHelper.CreateInputParameter("MarcGetStatus", SqlDbType.NVarChar, 30, false, marcGetStatus), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
				{
					List<Item> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Item o = list[0];
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
		/// Update values in dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Item value)
		{
			return ItemUpdateAuto(sqlConnection, sqlTransaction, "IAAnalysis", value );
		}
		
		/// <summary>
		/// Update values in dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Item value)
		{
			return ItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.Identifier,
				value.MARCLeader,
				value.Sponsor,
				value.Contributor,
				value.ScanningCenter,
				value.CollectionLibrary,
				value.CallNumber,
				value.ImageCount,
				value.CurationState,
				value.PossibleCopyrightStatus,
				value.Volume,
				value.ScanDate,
				value.AddedDate,
				value.PublicDate,
				value.UpdateDate,
				value.SponsorDate,
				value.ItemStatusID,
				value.MetaGetStatus,
				value.MarcGetStatus);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Item object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type CustomDataAccessStatus<Item>.</returns>
		public CustomDataAccessStatus<Item> ItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Item value  )
		{
			return ItemManageAuto( sqlConnection, sqlTransaction, "IAAnalysis", value  );
		}
		
		/// <summary>
		/// Manage dbo.Item object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type CustomDataAccessStatus<Item>.</returns>
		public CustomDataAccessStatus<Item> ItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Item value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Item returnValue = ItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.Identifier,
						value.MARCLeader,
						value.Sponsor,
						value.Contributor,
						value.ScanningCenter,
						value.CollectionLibrary,
						value.CallNumber,
						value.ImageCount,
						value.CurationState,
						value.PossibleCopyrightStatus,
						value.Volume,
						value.ScanDate,
						value.AddedDate,
						value.PublicDate,
						value.UpdateDate,
						value.SponsorDate,
						value.ItemStatusID,
						value.MetaGetStatus,
						value.MarcGetStatus);
				
				return new CustomDataAccessStatus<Item>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID))
				{
				return new CustomDataAccessStatus<Item>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Item>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Item returnValue = ItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.Identifier,
						value.MARCLeader,
						value.Sponsor,
						value.Contributor,
						value.ScanningCenter,
						value.CollectionLibrary,
						value.CallNumber,
						value.ImageCount,
						value.CurationState,
						value.PossibleCopyrightStatus,
						value.Volume,
						value.ScanDate,
						value.AddedDate,
						value.PublicDate,
						value.UpdateDate,
						value.SponsorDate,
						value.ItemStatusID,
						value.MetaGetStatus,
						value.MarcGetStatus);
					
				return new CustomDataAccessStatus<Item>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Item>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


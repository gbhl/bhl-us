
// Generated 5/9/2016 1:52:26 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemDAL is based upon dbo.Item.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ItemDAL
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
	partial class ItemDAL : IItemDAL
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
			return ItemSelectAuto(	sqlConnection, sqlTransaction, "BHL",	itemID );
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
					CustomGenericList<Item> list = helper.ExecuteReader(command);
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
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return ItemSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", itemID );
		}
		
		/// <summary>
		/// Select values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemSelectAutoRaw(
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
		/// <param name="primaryTitleID"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="itemDescription"></param>
		/// <param name="scannedBy"></param>
		/// <param name="pDFSize"></param>
		/// <param name="vaultID"></param>
		/// <param name="numberOfFiles"></param>
		/// <param name="note"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="itemSourceID"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="fileRootFolder"></param>
		/// <param name="zQuery"></param>
		/// <param name="sponsor"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="redirectItemID"></param>
		/// <param name="externalUrl"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int primaryTitleID,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string institutionCode,
			string languageCode,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			short? numberOfFiles,
			string note,
			int? creationUserID,
			int? lastModifiedUserID,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			int? itemSourceID,
			string year,
			string identifierBib,
			string fileRootFolder,
			string zQuery,
			string sponsor,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			int? thumbnailPageID,
			int? redirectItemID,
			string externalUrl)
		{
			return ItemInsertAuto( sqlConnection, sqlTransaction, "BHL", primaryTitleID, barCode, mARCItemID, callNumber, volume, institutionCode, languageCode, itemDescription, scannedBy, pDFSize, vaultID, numberOfFiles, note, creationUserID, lastModifiedUserID, itemStatusID, scanningUser, scanningDate, paginationCompleteUserID, paginationCompleteDate, paginationStatusID, paginationStatusUserID, paginationStatusDate, lastPageNameLookupDate, itemSourceID, year, identifierBib, fileRootFolder, zQuery, sponsor, licenseUrl, rights, dueDiligence, copyrightStatus, copyrightRegion, copyrightComment, copyrightEvidence, copyrightEvidenceOperator, copyrightEvidenceDate, thumbnailPageID, redirectItemID, externalUrl );
		}
		
		/// <summary>
		/// Insert values into dbo.Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="primaryTitleID"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="itemDescription"></param>
		/// <param name="scannedBy"></param>
		/// <param name="pDFSize"></param>
		/// <param name="vaultID"></param>
		/// <param name="numberOfFiles"></param>
		/// <param name="note"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="itemSourceID"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="fileRootFolder"></param>
		/// <param name="zQuery"></param>
		/// <param name="sponsor"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="redirectItemID"></param>
		/// <param name="externalUrl"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int primaryTitleID,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string institutionCode,
			string languageCode,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			short? numberOfFiles,
			string note,
			int? creationUserID,
			int? lastModifiedUserID,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			int? itemSourceID,
			string year,
			string identifierBib,
			string fileRootFolder,
			string zQuery,
			string sponsor,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			int? thumbnailPageID,
			int? redirectItemID,
			string externalUrl)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("PrimaryTitleID", SqlDbType.Int, null, false, primaryTitleID),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("MARCItemID", SqlDbType.NVarChar, 50, true, mARCItemID),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 100, true, callNumber),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, true, volume),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("ItemDescription", SqlDbType.NText, 1073741823, true, itemDescription),
					CustomSqlHelper.CreateInputParameter("ScannedBy", SqlDbType.Int, null, true, scannedBy),
					CustomSqlHelper.CreateInputParameter("PDFSize", SqlDbType.Int, null, true, pDFSize),
					CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, true, vaultID),
					CustomSqlHelper.CreateInputParameter("NumberOfFiles", SqlDbType.SmallInt, null, true, numberOfFiles),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("ScanningUser", SqlDbType.NVarChar, 100, true, scanningUser),
					CustomSqlHelper.CreateInputParameter("ScanningDate", SqlDbType.DateTime, null, true, scanningDate),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteUserID", SqlDbType.Int, null, true, paginationCompleteUserID),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteDate", SqlDbType.DateTime, null, true, paginationCompleteDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusID", SqlDbType.Int, null, true, paginationStatusID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusUserID", SqlDbType.Int, null, true, paginationStatusUserID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusDate", SqlDbType.DateTime, null, true, paginationStatusDate),
					CustomSqlHelper.CreateInputParameter("LastPageNameLookupDate", SqlDbType.DateTime, null, true, lastPageNameLookupDate),
					CustomSqlHelper.CreateInputParameter("ItemSourceID", SqlDbType.Int, null, true, itemSourceID),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, true, year),
					CustomSqlHelper.CreateInputParameter("IdentifierBib", SqlDbType.NVarChar, 50, true, identifierBib),
					CustomSqlHelper.CreateInputParameter("FileRootFolder", SqlDbType.NVarChar, 250, true, fileRootFolder),
					CustomSqlHelper.CreateInputParameter("ZQuery", SqlDbType.NVarChar, 200, true, zQuery),
					CustomSqlHelper.CreateInputParameter("Sponsor", SqlDbType.NVarChar, 100, true, sponsor),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, null, true, licenseUrl),
					CustomSqlHelper.CreateInputParameter("Rights", SqlDbType.NVarChar, null, true, rights),
					CustomSqlHelper.CreateInputParameter("DueDiligence", SqlDbType.NVarChar, null, true, dueDiligence),
					CustomSqlHelper.CreateInputParameter("CopyrightStatus", SqlDbType.NVarChar, null, true, copyrightStatus),
					CustomSqlHelper.CreateInputParameter("CopyrightRegion", SqlDbType.NVarChar, 50, true, copyrightRegion),
					CustomSqlHelper.CreateInputParameter("CopyrightComment", SqlDbType.NVarChar, null, true, copyrightComment),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidence", SqlDbType.NVarChar, null, true, copyrightEvidence),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceOperator", SqlDbType.NVarChar, 100, true, copyrightEvidenceOperator),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceDate", SqlDbType.NVarChar, 30, true, copyrightEvidenceDate),
					CustomSqlHelper.CreateInputParameter("ThumbnailPageID", SqlDbType.Int, null, true, thumbnailPageID),
					CustomSqlHelper.CreateInputParameter("RedirectItemID", SqlDbType.Int, null, true, redirectItemID),
					CustomSqlHelper.CreateInputParameter("ExternalUrl", SqlDbType.NVarChar, 500, true, externalUrl), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
				{
					CustomGenericList<Item> list = helper.ExecuteReader(command);
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
			return ItemInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
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
				value.PrimaryTitleID,
				value.BarCode,
				value.MARCItemID,
				value.CallNumber,
				value.Volume,
				value.InstitutionCode,
				value.LanguageCode,
				value.ItemDescription,
				value.ScannedBy,
				value.PDFSize,
				value.VaultID,
				value.NumberOfFiles,
				value.Note,
				value.CreationUserID,
				value.LastModifiedUserID,
				value.ItemStatusID,
				value.ScanningUser,
				value.ScanningDate,
				value.PaginationCompleteUserID,
				value.PaginationCompleteDate,
				value.PaginationStatusID,
				value.PaginationStatusUserID,
				value.PaginationStatusDate,
				value.LastPageNameLookupDate,
				value.ItemSourceID,
				value.Year,
				value.IdentifierBib,
				value.FileRootFolder,
				value.ZQuery,
				value.Sponsor,
				value.LicenseUrl,
				value.Rights,
				value.DueDiligence,
				value.CopyrightStatus,
				value.CopyrightRegion,
				value.CopyrightComment,
				value.CopyrightEvidence,
				value.CopyrightEvidenceOperator,
				value.CopyrightEvidenceDate,
				value.ThumbnailPageID,
				value.RedirectItemID,
				value.ExternalUrl);
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
			return ItemDeleteAuto( sqlConnection, sqlTransaction, "BHL", itemID );
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
		/// <param name="primaryTitleID"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="itemDescription"></param>
		/// <param name="scannedBy"></param>
		/// <param name="pDFSize"></param>
		/// <param name="vaultID"></param>
		/// <param name="numberOfFiles"></param>
		/// <param name="note"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="itemSourceID"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="fileRootFolder"></param>
		/// <param name="zQuery"></param>
		/// <param name="sponsor"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="redirectItemID"></param>
		/// <param name="externalUrl"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int primaryTitleID,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string institutionCode,
			string languageCode,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			short? numberOfFiles,
			string note,
			int? lastModifiedUserID,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			int? itemSourceID,
			string year,
			string identifierBib,
			string fileRootFolder,
			string zQuery,
			string sponsor,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			int? thumbnailPageID,
			int? redirectItemID,
			string externalUrl)
		{
			return ItemUpdateAuto( sqlConnection, sqlTransaction, "BHL", itemID, primaryTitleID, barCode, mARCItemID, callNumber, volume, institutionCode, languageCode, itemDescription, scannedBy, pDFSize, vaultID, numberOfFiles, note, lastModifiedUserID, itemStatusID, scanningUser, scanningDate, paginationCompleteUserID, paginationCompleteDate, paginationStatusID, paginationStatusUserID, paginationStatusDate, lastPageNameLookupDate, itemSourceID, year, identifierBib, fileRootFolder, zQuery, sponsor, licenseUrl, rights, dueDiligence, copyrightStatus, copyrightRegion, copyrightComment, copyrightEvidence, copyrightEvidenceOperator, copyrightEvidenceDate, thumbnailPageID, redirectItemID, externalUrl);
		}
		
		/// <summary>
		/// Update values in dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="primaryTitleID"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="itemDescription"></param>
		/// <param name="scannedBy"></param>
		/// <param name="pDFSize"></param>
		/// <param name="vaultID"></param>
		/// <param name="numberOfFiles"></param>
		/// <param name="note"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="itemSourceID"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="fileRootFolder"></param>
		/// <param name="zQuery"></param>
		/// <param name="sponsor"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="redirectItemID"></param>
		/// <param name="externalUrl"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int primaryTitleID,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string institutionCode,
			string languageCode,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			short? numberOfFiles,
			string note,
			int? lastModifiedUserID,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			int? itemSourceID,
			string year,
			string identifierBib,
			string fileRootFolder,
			string zQuery,
			string sponsor,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			int? thumbnailPageID,
			int? redirectItemID,
			string externalUrl)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("PrimaryTitleID", SqlDbType.Int, null, false, primaryTitleID),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("MARCItemID", SqlDbType.NVarChar, 50, true, mARCItemID),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 100, true, callNumber),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, true, volume),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("ItemDescription", SqlDbType.NText, 1073741823, true, itemDescription),
					CustomSqlHelper.CreateInputParameter("ScannedBy", SqlDbType.Int, null, true, scannedBy),
					CustomSqlHelper.CreateInputParameter("PDFSize", SqlDbType.Int, null, true, pDFSize),
					CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, true, vaultID),
					CustomSqlHelper.CreateInputParameter("NumberOfFiles", SqlDbType.SmallInt, null, true, numberOfFiles),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("ScanningUser", SqlDbType.NVarChar, 100, true, scanningUser),
					CustomSqlHelper.CreateInputParameter("ScanningDate", SqlDbType.DateTime, null, true, scanningDate),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteUserID", SqlDbType.Int, null, true, paginationCompleteUserID),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteDate", SqlDbType.DateTime, null, true, paginationCompleteDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusID", SqlDbType.Int, null, true, paginationStatusID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusUserID", SqlDbType.Int, null, true, paginationStatusUserID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusDate", SqlDbType.DateTime, null, true, paginationStatusDate),
					CustomSqlHelper.CreateInputParameter("LastPageNameLookupDate", SqlDbType.DateTime, null, true, lastPageNameLookupDate),
					CustomSqlHelper.CreateInputParameter("ItemSourceID", SqlDbType.Int, null, true, itemSourceID),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, true, year),
					CustomSqlHelper.CreateInputParameter("IdentifierBib", SqlDbType.NVarChar, 50, true, identifierBib),
					CustomSqlHelper.CreateInputParameter("FileRootFolder", SqlDbType.NVarChar, 250, true, fileRootFolder),
					CustomSqlHelper.CreateInputParameter("ZQuery", SqlDbType.NVarChar, 200, true, zQuery),
					CustomSqlHelper.CreateInputParameter("Sponsor", SqlDbType.NVarChar, 100, true, sponsor),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, null, true, licenseUrl),
					CustomSqlHelper.CreateInputParameter("Rights", SqlDbType.NVarChar, null, true, rights),
					CustomSqlHelper.CreateInputParameter("DueDiligence", SqlDbType.NVarChar, null, true, dueDiligence),
					CustomSqlHelper.CreateInputParameter("CopyrightStatus", SqlDbType.NVarChar, null, true, copyrightStatus),
					CustomSqlHelper.CreateInputParameter("CopyrightRegion", SqlDbType.NVarChar, 50, true, copyrightRegion),
					CustomSqlHelper.CreateInputParameter("CopyrightComment", SqlDbType.NVarChar, null, true, copyrightComment),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidence", SqlDbType.NVarChar, null, true, copyrightEvidence),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceOperator", SqlDbType.NVarChar, 100, true, copyrightEvidenceOperator),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceDate", SqlDbType.NVarChar, 30, true, copyrightEvidenceDate),
					CustomSqlHelper.CreateInputParameter("ThumbnailPageID", SqlDbType.Int, null, true, thumbnailPageID),
					CustomSqlHelper.CreateInputParameter("RedirectItemID", SqlDbType.Int, null, true, redirectItemID),
					CustomSqlHelper.CreateInputParameter("ExternalUrl", SqlDbType.NVarChar, 500, true, externalUrl), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
				{
					CustomGenericList<Item> list = helper.ExecuteReader(command);
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
			return ItemUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
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
				value.PrimaryTitleID,
				value.BarCode,
				value.MARCItemID,
				value.CallNumber,
				value.Volume,
				value.InstitutionCode,
				value.LanguageCode,
				value.ItemDescription,
				value.ScannedBy,
				value.PDFSize,
				value.VaultID,
				value.NumberOfFiles,
				value.Note,
				value.LastModifiedUserID,
				value.ItemStatusID,
				value.ScanningUser,
				value.ScanningDate,
				value.PaginationCompleteUserID,
				value.PaginationCompleteDate,
				value.PaginationStatusID,
				value.PaginationStatusUserID,
				value.PaginationStatusDate,
				value.LastPageNameLookupDate,
				value.ItemSourceID,
				value.Year,
				value.IdentifierBib,
				value.FileRootFolder,
				value.ZQuery,
				value.Sponsor,
				value.LicenseUrl,
				value.Rights,
				value.DueDiligence,
				value.CopyrightStatus,
				value.CopyrightRegion,
				value.CopyrightComment,
				value.CopyrightEvidence,
				value.CopyrightEvidenceOperator,
				value.CopyrightEvidenceDate,
				value.ThumbnailPageID,
				value.RedirectItemID,
				value.ExternalUrl);
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
			Item value , int userId )
		{
			return ItemManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
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
			Item value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Item returnValue = ItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PrimaryTitleID,
						value.BarCode,
						value.MARCItemID,
						value.CallNumber,
						value.Volume,
						value.InstitutionCode,
						value.LanguageCode,
						value.ItemDescription,
						value.ScannedBy,
						value.PDFSize,
						value.VaultID,
						value.NumberOfFiles,
						value.Note,
						value.CreationUserID,
						value.LastModifiedUserID,
						value.ItemStatusID,
						value.ScanningUser,
						value.ScanningDate,
						value.PaginationCompleteUserID,
						value.PaginationCompleteDate,
						value.PaginationStatusID,
						value.PaginationStatusUserID,
						value.PaginationStatusDate,
						value.LastPageNameLookupDate,
						value.ItemSourceID,
						value.Year,
						value.IdentifierBib,
						value.FileRootFolder,
						value.ZQuery,
						value.Sponsor,
						value.LicenseUrl,
						value.Rights,
						value.DueDiligence,
						value.CopyrightStatus,
						value.CopyrightRegion,
						value.CopyrightComment,
						value.CopyrightEvidence,
						value.CopyrightEvidenceOperator,
						value.CopyrightEvidenceDate,
						value.ThumbnailPageID,
						value.RedirectItemID,
						value.ExternalUrl);
				
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
				value.LastModifiedUserID = userId;
				Item returnValue = ItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.PrimaryTitleID,
						value.BarCode,
						value.MARCItemID,
						value.CallNumber,
						value.Volume,
						value.InstitutionCode,
						value.LanguageCode,
						value.ItemDescription,
						value.ScannedBy,
						value.PDFSize,
						value.VaultID,
						value.NumberOfFiles,
						value.Note,
						value.LastModifiedUserID,
						value.ItemStatusID,
						value.ScanningUser,
						value.ScanningDate,
						value.PaginationCompleteUserID,
						value.PaginationCompleteDate,
						value.PaginationStatusID,
						value.PaginationStatusUserID,
						value.PaginationStatusDate,
						value.LastPageNameLookupDate,
						value.ItemSourceID,
						value.Year,
						value.IdentifierBib,
						value.FileRootFolder,
						value.ZQuery,
						value.Sponsor,
						value.LicenseUrl,
						value.Rights,
						value.DueDiligence,
						value.CopyrightStatus,
						value.CopyrightRegion,
						value.CopyrightComment,
						value.CopyrightEvidence,
						value.CopyrightEvidenceOperator,
						value.CopyrightEvidenceDate,
						value.ThumbnailPageID,
						value.RedirectItemID,
						value.ExternalUrl);
					
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


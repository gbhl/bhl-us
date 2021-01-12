
// Generated 1/11/2021 2:02:41 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemDAL is based upon dbo.Item.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
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
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
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
			return ItemSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	itemID );
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
			return ItemSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", itemID );
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
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="barCode"></param>
		/// <param name="itemSequence"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="sponsor"></param>
		/// <param name="itemDescription"></param>
		/// <param name="scannedBy"></param>
		/// <param name="pDFSize"></param>
		/// <param name="vaultID"></param>
		/// <param name="numberOfFiles"></param>
		/// <param name="note"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="scanningInstitutionCode"></param>
		/// <param name="rightsHolderCode"></param>
		/// <param name="endYear"></param>
		/// <param name="startVolume"></param>
		/// <param name="endVolume"></param>
		/// <param name="startIssue"></param>
		/// <param name="endIssue"></param>
		/// <param name="startNumber"></param>
		/// <param name="endNumber"></param>
		/// <param name="startSeries"></param>
		/// <param name="endSeries"></param>
		/// <param name="startPart"></param>
		/// <param name="endPart"></param>
		/// <param name="pageProgression"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string importKey,
			int importStatusID,
			int? importSourceID,
			string mARCBibID,
			string barCode,
			short? itemSequence,
			string mARCItemID,
			string callNumber,
			string volume,
			string institutionCode,
			string languageCode,
			string sponsor,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			short? numberOfFiles,
			string note,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate,
			string year,
			string identifierBib,
			string zQuery,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			string scanningInstitutionCode,
			string rightsHolderCode,
			string endYear,
			string startVolume,
			string endVolume,
			string startIssue,
			string endIssue,
			string startNumber,
			string endNumber,
			string startSeries,
			string endSeries,
			string startPart,
			string endPart,
			string pageProgression)
		{
			return ItemInsertAuto( sqlConnection, sqlTransaction, "BHLImport", importKey, importStatusID, importSourceID, mARCBibID, barCode, itemSequence, mARCItemID, callNumber, volume, institutionCode, languageCode, sponsor, itemDescription, scannedBy, pDFSize, vaultID, numberOfFiles, note, itemStatusID, scanningUser, scanningDate, paginationCompleteUserID, paginationCompleteDate, paginationStatusID, paginationStatusUserID, paginationStatusDate, lastPageNameLookupDate, externalCreationDate, externalLastModifiedDate, externalCreationUser, externalLastModifiedUser, productionDate, year, identifierBib, zQuery, licenseUrl, rights, dueDiligence, copyrightStatus, copyrightRegion, copyrightComment, copyrightEvidence, copyrightEvidenceOperator, copyrightEvidenceDate, scanningInstitutionCode, rightsHolderCode, endYear, startVolume, endVolume, startIssue, endIssue, startNumber, endNumber, startSeries, endSeries, startPart, endPart, pageProgression );
		}
		
		/// <summary>
		/// Insert values into dbo.Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="barCode"></param>
		/// <param name="itemSequence"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="sponsor"></param>
		/// <param name="itemDescription"></param>
		/// <param name="scannedBy"></param>
		/// <param name="pDFSize"></param>
		/// <param name="vaultID"></param>
		/// <param name="numberOfFiles"></param>
		/// <param name="note"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="scanningInstitutionCode"></param>
		/// <param name="rightsHolderCode"></param>
		/// <param name="endYear"></param>
		/// <param name="startVolume"></param>
		/// <param name="endVolume"></param>
		/// <param name="startIssue"></param>
		/// <param name="endIssue"></param>
		/// <param name="startNumber"></param>
		/// <param name="endNumber"></param>
		/// <param name="startSeries"></param>
		/// <param name="endSeries"></param>
		/// <param name="startPart"></param>
		/// <param name="endPart"></param>
		/// <param name="pageProgression"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string mARCBibID,
			string barCode,
			short? itemSequence,
			string mARCItemID,
			string callNumber,
			string volume,
			string institutionCode,
			string languageCode,
			string sponsor,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			short? numberOfFiles,
			string note,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate,
			string year,
			string identifierBib,
			string zQuery,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			string scanningInstitutionCode,
			string rightsHolderCode,
			string endYear,
			string startVolume,
			string endVolume,
			string startIssue,
			string endIssue,
			string startNumber,
			string endNumber,
			string startSeries,
			string endSeries,
			string startPart,
			string endPart,
			string pageProgression)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("MARCBibID", SqlDbType.NVarChar, 50, false, mARCBibID),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("ItemSequence", SqlDbType.SmallInt, null, true, itemSequence),
					CustomSqlHelper.CreateInputParameter("MARCItemID", SqlDbType.NVarChar, 50, true, mARCItemID),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 100, true, callNumber),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, true, volume),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("Sponsor", SqlDbType.NVarChar, 100, true, sponsor),
					CustomSqlHelper.CreateInputParameter("ItemDescription", SqlDbType.NText, 1073741823, true, itemDescription),
					CustomSqlHelper.CreateInputParameter("ScannedBy", SqlDbType.Int, null, true, scannedBy),
					CustomSqlHelper.CreateInputParameter("PDFSize", SqlDbType.Int, null, true, pDFSize),
					CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, true, vaultID),
					CustomSqlHelper.CreateInputParameter("NumberOfFiles", SqlDbType.SmallInt, null, true, numberOfFiles),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("ScanningUser", SqlDbType.NVarChar, 100, true, scanningUser),
					CustomSqlHelper.CreateInputParameter("ScanningDate", SqlDbType.DateTime, null, true, scanningDate),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteUserID", SqlDbType.Int, null, true, paginationCompleteUserID),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteDate", SqlDbType.DateTime, null, true, paginationCompleteDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusID", SqlDbType.Int, null, true, paginationStatusID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusUserID", SqlDbType.Int, null, true, paginationStatusUserID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusDate", SqlDbType.DateTime, null, true, paginationStatusDate),
					CustomSqlHelper.CreateInputParameter("LastPageNameLookupDate", SqlDbType.DateTime, null, true, lastPageNameLookupDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationUser", SqlDbType.Int, null, true, externalCreationUser),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedUser", SqlDbType.Int, null, true, externalLastModifiedUser),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, true, year),
					CustomSqlHelper.CreateInputParameter("IdentifierBib", SqlDbType.NVarChar, 50, true, identifierBib),
					CustomSqlHelper.CreateInputParameter("ZQuery", SqlDbType.NVarChar, 200, true, zQuery),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, 1073741823, true, licenseUrl),
					CustomSqlHelper.CreateInputParameter("Rights", SqlDbType.NVarChar, 1073741823, true, rights),
					CustomSqlHelper.CreateInputParameter("DueDiligence", SqlDbType.NVarChar, 1073741823, true, dueDiligence),
					CustomSqlHelper.CreateInputParameter("CopyrightStatus", SqlDbType.NVarChar, 1073741823, true, copyrightStatus),
					CustomSqlHelper.CreateInputParameter("CopyrightRegion", SqlDbType.NVarChar, 50, true, copyrightRegion),
					CustomSqlHelper.CreateInputParameter("CopyrightComment", SqlDbType.NVarChar, 1073741823, true, copyrightComment),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidence", SqlDbType.NVarChar, 1073741823, true, copyrightEvidence),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceOperator", SqlDbType.NVarChar, 100, true, copyrightEvidenceOperator),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceDate", SqlDbType.NVarChar, 30, true, copyrightEvidenceDate),
					CustomSqlHelper.CreateInputParameter("ScanningInstitutionCode", SqlDbType.NVarChar, 10, true, scanningInstitutionCode),
					CustomSqlHelper.CreateInputParameter("RightsHolderCode", SqlDbType.NVarChar, 10, true, rightsHolderCode),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.NVarChar, 20, false, endYear),
					CustomSqlHelper.CreateInputParameter("StartVolume", SqlDbType.NVarChar, 10, false, startVolume),
					CustomSqlHelper.CreateInputParameter("EndVolume", SqlDbType.NVarChar, 10, false, endVolume),
					CustomSqlHelper.CreateInputParameter("StartIssue", SqlDbType.NVarChar, 10, false, startIssue),
					CustomSqlHelper.CreateInputParameter("EndIssue", SqlDbType.NVarChar, 10, false, endIssue),
					CustomSqlHelper.CreateInputParameter("StartNumber", SqlDbType.NVarChar, 10, false, startNumber),
					CustomSqlHelper.CreateInputParameter("EndNumber", SqlDbType.NVarChar, 10, false, endNumber),
					CustomSqlHelper.CreateInputParameter("StartSeries", SqlDbType.NVarChar, 10, false, startSeries),
					CustomSqlHelper.CreateInputParameter("EndSeries", SqlDbType.NVarChar, 10, false, endSeries),
					CustomSqlHelper.CreateInputParameter("StartPart", SqlDbType.NVarChar, 10, false, startPart),
					CustomSqlHelper.CreateInputParameter("EndPart", SqlDbType.NVarChar, 10, false, endPart),
					CustomSqlHelper.CreateInputParameter("PageProgression", SqlDbType.NVarChar, 10, false, pageProgression), 
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
			return ItemInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
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
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.MARCBibID,
				value.BarCode,
				value.ItemSequence,
				value.MARCItemID,
				value.CallNumber,
				value.Volume,
				value.InstitutionCode,
				value.LanguageCode,
				value.Sponsor,
				value.ItemDescription,
				value.ScannedBy,
				value.PDFSize,
				value.VaultID,
				value.NumberOfFiles,
				value.Note,
				value.ItemStatusID,
				value.ScanningUser,
				value.ScanningDate,
				value.PaginationCompleteUserID,
				value.PaginationCompleteDate,
				value.PaginationStatusID,
				value.PaginationStatusUserID,
				value.PaginationStatusDate,
				value.LastPageNameLookupDate,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ExternalCreationUser,
				value.ExternalLastModifiedUser,
				value.ProductionDate,
				value.Year,
				value.IdentifierBib,
				value.ZQuery,
				value.LicenseUrl,
				value.Rights,
				value.DueDiligence,
				value.CopyrightStatus,
				value.CopyrightRegion,
				value.CopyrightComment,
				value.CopyrightEvidence,
				value.CopyrightEvidenceOperator,
				value.CopyrightEvidenceDate,
				value.ScanningInstitutionCode,
				value.RightsHolderCode,
				value.EndYear,
				value.StartVolume,
				value.EndVolume,
				value.StartIssue,
				value.EndIssue,
				value.StartNumber,
				value.EndNumber,
				value.StartSeries,
				value.EndSeries,
				value.StartPart,
				value.EndPart,
				value.PageProgression);
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
			return ItemDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", itemID );
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
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="barCode"></param>
		/// <param name="itemSequence"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="sponsor"></param>
		/// <param name="itemDescription"></param>
		/// <param name="scannedBy"></param>
		/// <param name="pDFSize"></param>
		/// <param name="vaultID"></param>
		/// <param name="numberOfFiles"></param>
		/// <param name="note"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="scanningInstitutionCode"></param>
		/// <param name="rightsHolderCode"></param>
		/// <param name="endYear"></param>
		/// <param name="startVolume"></param>
		/// <param name="endVolume"></param>
		/// <param name="startIssue"></param>
		/// <param name="endIssue"></param>
		/// <param name="startNumber"></param>
		/// <param name="endNumber"></param>
		/// <param name="startSeries"></param>
		/// <param name="endSeries"></param>
		/// <param name="startPart"></param>
		/// <param name="endPart"></param>
		/// <param name="pageProgression"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string mARCBibID,
			string barCode,
			short? itemSequence,
			string mARCItemID,
			string callNumber,
			string volume,
			string institutionCode,
			string languageCode,
			string sponsor,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			short? numberOfFiles,
			string note,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate,
			string year,
			string identifierBib,
			string zQuery,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			string scanningInstitutionCode,
			string rightsHolderCode,
			string endYear,
			string startVolume,
			string endVolume,
			string startIssue,
			string endIssue,
			string startNumber,
			string endNumber,
			string startSeries,
			string endSeries,
			string startPart,
			string endPart,
			string pageProgression)
		{
			return ItemUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, importKey, importStatusID, importSourceID, mARCBibID, barCode, itemSequence, mARCItemID, callNumber, volume, institutionCode, languageCode, sponsor, itemDescription, scannedBy, pDFSize, vaultID, numberOfFiles, note, itemStatusID, scanningUser, scanningDate, paginationCompleteUserID, paginationCompleteDate, paginationStatusID, paginationStatusUserID, paginationStatusDate, lastPageNameLookupDate, externalCreationDate, externalLastModifiedDate, externalCreationUser, externalLastModifiedUser, productionDate, year, identifierBib, zQuery, licenseUrl, rights, dueDiligence, copyrightStatus, copyrightRegion, copyrightComment, copyrightEvidence, copyrightEvidenceOperator, copyrightEvidenceDate, scanningInstitutionCode, rightsHolderCode, endYear, startVolume, endVolume, startIssue, endIssue, startNumber, endNumber, startSeries, endSeries, startPart, endPart, pageProgression);
		}
		
		/// <summary>
		/// Update values in dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="barCode"></param>
		/// <param name="itemSequence"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="sponsor"></param>
		/// <param name="itemDescription"></param>
		/// <param name="scannedBy"></param>
		/// <param name="pDFSize"></param>
		/// <param name="vaultID"></param>
		/// <param name="numberOfFiles"></param>
		/// <param name="note"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="scanningInstitutionCode"></param>
		/// <param name="rightsHolderCode"></param>
		/// <param name="endYear"></param>
		/// <param name="startVolume"></param>
		/// <param name="endVolume"></param>
		/// <param name="startIssue"></param>
		/// <param name="endIssue"></param>
		/// <param name="startNumber"></param>
		/// <param name="endNumber"></param>
		/// <param name="startSeries"></param>
		/// <param name="endSeries"></param>
		/// <param name="startPart"></param>
		/// <param name="endPart"></param>
		/// <param name="pageProgression"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string mARCBibID,
			string barCode,
			short? itemSequence,
			string mARCItemID,
			string callNumber,
			string volume,
			string institutionCode,
			string languageCode,
			string sponsor,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			short? numberOfFiles,
			string note,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate,
			string year,
			string identifierBib,
			string zQuery,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			string scanningInstitutionCode,
			string rightsHolderCode,
			string endYear,
			string startVolume,
			string endVolume,
			string startIssue,
			string endIssue,
			string startNumber,
			string endNumber,
			string startSeries,
			string endSeries,
			string startPart,
			string endPart,
			string pageProgression)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("MARCBibID", SqlDbType.NVarChar, 50, false, mARCBibID),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("ItemSequence", SqlDbType.SmallInt, null, true, itemSequence),
					CustomSqlHelper.CreateInputParameter("MARCItemID", SqlDbType.NVarChar, 50, true, mARCItemID),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 100, true, callNumber),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, true, volume),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("Sponsor", SqlDbType.NVarChar, 100, true, sponsor),
					CustomSqlHelper.CreateInputParameter("ItemDescription", SqlDbType.NText, 1073741823, true, itemDescription),
					CustomSqlHelper.CreateInputParameter("ScannedBy", SqlDbType.Int, null, true, scannedBy),
					CustomSqlHelper.CreateInputParameter("PDFSize", SqlDbType.Int, null, true, pDFSize),
					CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, true, vaultID),
					CustomSqlHelper.CreateInputParameter("NumberOfFiles", SqlDbType.SmallInt, null, true, numberOfFiles),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("ScanningUser", SqlDbType.NVarChar, 100, true, scanningUser),
					CustomSqlHelper.CreateInputParameter("ScanningDate", SqlDbType.DateTime, null, true, scanningDate),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteUserID", SqlDbType.Int, null, true, paginationCompleteUserID),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteDate", SqlDbType.DateTime, null, true, paginationCompleteDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusID", SqlDbType.Int, null, true, paginationStatusID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusUserID", SqlDbType.Int, null, true, paginationStatusUserID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusDate", SqlDbType.DateTime, null, true, paginationStatusDate),
					CustomSqlHelper.CreateInputParameter("LastPageNameLookupDate", SqlDbType.DateTime, null, true, lastPageNameLookupDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationUser", SqlDbType.Int, null, true, externalCreationUser),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedUser", SqlDbType.Int, null, true, externalLastModifiedUser),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, true, year),
					CustomSqlHelper.CreateInputParameter("IdentifierBib", SqlDbType.NVarChar, 50, true, identifierBib),
					CustomSqlHelper.CreateInputParameter("ZQuery", SqlDbType.NVarChar, 200, true, zQuery),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, 1073741823, true, licenseUrl),
					CustomSqlHelper.CreateInputParameter("Rights", SqlDbType.NVarChar, 1073741823, true, rights),
					CustomSqlHelper.CreateInputParameter("DueDiligence", SqlDbType.NVarChar, 1073741823, true, dueDiligence),
					CustomSqlHelper.CreateInputParameter("CopyrightStatus", SqlDbType.NVarChar, 1073741823, true, copyrightStatus),
					CustomSqlHelper.CreateInputParameter("CopyrightRegion", SqlDbType.NVarChar, 50, true, copyrightRegion),
					CustomSqlHelper.CreateInputParameter("CopyrightComment", SqlDbType.NVarChar, 1073741823, true, copyrightComment),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidence", SqlDbType.NVarChar, 1073741823, true, copyrightEvidence),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceOperator", SqlDbType.NVarChar, 100, true, copyrightEvidenceOperator),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceDate", SqlDbType.NVarChar, 30, true, copyrightEvidenceDate),
					CustomSqlHelper.CreateInputParameter("ScanningInstitutionCode", SqlDbType.NVarChar, 10, true, scanningInstitutionCode),
					CustomSqlHelper.CreateInputParameter("RightsHolderCode", SqlDbType.NVarChar, 10, true, rightsHolderCode),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.NVarChar, 20, false, endYear),
					CustomSqlHelper.CreateInputParameter("StartVolume", SqlDbType.NVarChar, 10, false, startVolume),
					CustomSqlHelper.CreateInputParameter("EndVolume", SqlDbType.NVarChar, 10, false, endVolume),
					CustomSqlHelper.CreateInputParameter("StartIssue", SqlDbType.NVarChar, 10, false, startIssue),
					CustomSqlHelper.CreateInputParameter("EndIssue", SqlDbType.NVarChar, 10, false, endIssue),
					CustomSqlHelper.CreateInputParameter("StartNumber", SqlDbType.NVarChar, 10, false, startNumber),
					CustomSqlHelper.CreateInputParameter("EndNumber", SqlDbType.NVarChar, 10, false, endNumber),
					CustomSqlHelper.CreateInputParameter("StartSeries", SqlDbType.NVarChar, 10, false, startSeries),
					CustomSqlHelper.CreateInputParameter("EndSeries", SqlDbType.NVarChar, 10, false, endSeries),
					CustomSqlHelper.CreateInputParameter("StartPart", SqlDbType.NVarChar, 10, false, startPart),
					CustomSqlHelper.CreateInputParameter("EndPart", SqlDbType.NVarChar, 10, false, endPart),
					CustomSqlHelper.CreateInputParameter("PageProgression", SqlDbType.NVarChar, 10, false, pageProgression), 
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
			return ItemUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
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
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.MARCBibID,
				value.BarCode,
				value.ItemSequence,
				value.MARCItemID,
				value.CallNumber,
				value.Volume,
				value.InstitutionCode,
				value.LanguageCode,
				value.Sponsor,
				value.ItemDescription,
				value.ScannedBy,
				value.PDFSize,
				value.VaultID,
				value.NumberOfFiles,
				value.Note,
				value.ItemStatusID,
				value.ScanningUser,
				value.ScanningDate,
				value.PaginationCompleteUserID,
				value.PaginationCompleteDate,
				value.PaginationStatusID,
				value.PaginationStatusUserID,
				value.PaginationStatusDate,
				value.LastPageNameLookupDate,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ExternalCreationUser,
				value.ExternalLastModifiedUser,
				value.ProductionDate,
				value.Year,
				value.IdentifierBib,
				value.ZQuery,
				value.LicenseUrl,
				value.Rights,
				value.DueDiligence,
				value.CopyrightStatus,
				value.CopyrightRegion,
				value.CopyrightComment,
				value.CopyrightEvidence,
				value.CopyrightEvidenceOperator,
				value.CopyrightEvidenceDate,
				value.ScanningInstitutionCode,
				value.RightsHolderCode,
				value.EndYear,
				value.StartVolume,
				value.EndVolume,
				value.StartIssue,
				value.EndIssue,
				value.StartNumber,
				value.EndNumber,
				value.StartSeries,
				value.EndSeries,
				value.StartPart,
				value.EndPart,
				value.PageProgression);
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
			return ItemManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
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
					value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.MARCBibID,
						value.BarCode,
						value.ItemSequence,
						value.MARCItemID,
						value.CallNumber,
						value.Volume,
						value.InstitutionCode,
						value.LanguageCode,
						value.Sponsor,
						value.ItemDescription,
						value.ScannedBy,
						value.PDFSize,
						value.VaultID,
						value.NumberOfFiles,
						value.Note,
						value.ItemStatusID,
						value.ScanningUser,
						value.ScanningDate,
						value.PaginationCompleteUserID,
						value.PaginationCompleteDate,
						value.PaginationStatusID,
						value.PaginationStatusUserID,
						value.PaginationStatusDate,
						value.LastPageNameLookupDate,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ExternalCreationUser,
						value.ExternalLastModifiedUser,
						value.ProductionDate,
						value.Year,
						value.IdentifierBib,
						value.ZQuery,
						value.LicenseUrl,
						value.Rights,
						value.DueDiligence,
						value.CopyrightStatus,
						value.CopyrightRegion,
						value.CopyrightComment,
						value.CopyrightEvidence,
						value.CopyrightEvidenceOperator,
						value.CopyrightEvidenceDate,
						value.ScanningInstitutionCode,
						value.RightsHolderCode,
						value.EndYear,
						value.StartVolume,
						value.EndVolume,
						value.StartIssue,
						value.EndIssue,
						value.StartNumber,
						value.EndNumber,
						value.StartSeries,
						value.EndSeries,
						value.StartPart,
						value.EndPart,
						value.PageProgression);
				
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
						value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.MARCBibID,
						value.BarCode,
						value.ItemSequence,
						value.MARCItemID,
						value.CallNumber,
						value.Volume,
						value.InstitutionCode,
						value.LanguageCode,
						value.Sponsor,
						value.ItemDescription,
						value.ScannedBy,
						value.PDFSize,
						value.VaultID,
						value.NumberOfFiles,
						value.Note,
						value.ItemStatusID,
						value.ScanningUser,
						value.ScanningDate,
						value.PaginationCompleteUserID,
						value.PaginationCompleteDate,
						value.PaginationStatusID,
						value.PaginationStatusUserID,
						value.PaginationStatusDate,
						value.LastPageNameLookupDate,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ExternalCreationUser,
						value.ExternalLastModifiedUser,
						value.ProductionDate,
						value.Year,
						value.IdentifierBib,
						value.ZQuery,
						value.LicenseUrl,
						value.Rights,
						value.DueDiligence,
						value.CopyrightStatus,
						value.CopyrightRegion,
						value.CopyrightComment,
						value.CopyrightEvidence,
						value.CopyrightEvidenceOperator,
						value.CopyrightEvidenceDate,
						value.ScanningInstitutionCode,
						value.RightsHolderCode,
						value.EndYear,
						value.StartVolume,
						value.EndVolume,
						value.StartIssue,
						value.EndIssue,
						value.StartNumber,
						value.EndNumber,
						value.StartSeries,
						value.EndSeries,
						value.StartPart,
						value.EndPart,
						value.PageProgression);
					
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


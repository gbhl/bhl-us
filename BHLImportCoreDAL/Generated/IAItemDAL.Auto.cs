
// Generated 4/12/2017 9:33:46 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAItemDAL is based upon dbo.IAItem.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAItemDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
{
	partial class IAItemDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IAItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>Object of type IAItem.</returns>
		public IAItem IAItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return IAItemSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	itemID );
		}
			
		/// <summary>
		/// Select values from dbo.IAItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>Object of type IAItem.</returns>
		public IAItem IAItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
			{
				using (CustomSqlHelper<IAItem> helper = new CustomSqlHelper<IAItem>())
				{
					CustomGenericList<IAItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItem o = list[0];
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
		/// Select values from dbo.IAItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return IAItemSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", itemID );
		}
		
		/// <summary>
		/// Select values from dbo.IAItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IAItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="iAIdentifierPrefix"></param>
		/// <param name="iAIdentifier"></param>
		/// <param name="sponsor"></param>
		/// <param name="sponsorName"></param>
		/// <param name="scanningCenter"></param>
		/// <param name="callNumber"></param>
		/// <param name="imageCount"></param>
		/// <param name="identifierAccessUrl"></param>
		/// <param name="volume"></param>
		/// <param name="note"></param>
		/// <param name="scanOperator"></param>
		/// <param name="scanDate"></param>
		/// <param name="externalStatus"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="barCode"></param>
		/// <param name="iADateStamp"></param>
		/// <param name="iAAddedDate"></param>
		/// <param name="lastOAIDataHarvestDate"></param>
		/// <param name="lastXMLDataHarvestDate"></param>
		/// <param name="lastProductionDate"></param>
		/// <param name="shortTitle"></param>
		/// <param name="sponsorDate"></param>
		/// <param name="titleID"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="possibleCopyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="localFileFolder"></param>
		/// <param name="noMARCOk"></param>
		/// <param name="scanningInstitution"></param>
		/// <param name="rightsHolder"></param>
		/// <param name="itemDescription"></param>
		/// <returns>Object of type IAItem.</returns>
		public IAItem IAItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID,
			string iAIdentifierPrefix,
			string iAIdentifier,
			string sponsor,
			string sponsorName,
			string scanningCenter,
			string callNumber,
			int? imageCount,
			string identifierAccessUrl,
			string volume,
			string note,
			string scanOperator,
			string scanDate,
			string externalStatus,
			string mARCBibID,
			string barCode,
			DateTime? iADateStamp,
			DateTime? iAAddedDate,
			DateTime? lastOAIDataHarvestDate,
			DateTime? lastXMLDataHarvestDate,
			DateTime? lastProductionDate,
			string shortTitle,
			string sponsorDate,
			string titleID,
			string year,
			string identifierBib,
			string zQuery,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string possibleCopyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			string localFileFolder,
			byte noMARCOk,
			string scanningInstitution,
			string rightsHolder,
			string itemDescription)
		{
			return IAItemInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemStatusID, iAIdentifierPrefix, iAIdentifier, sponsor, sponsorName, scanningCenter, callNumber, imageCount, identifierAccessUrl, volume, note, scanOperator, scanDate, externalStatus, mARCBibID, barCode, iADateStamp, iAAddedDate, lastOAIDataHarvestDate, lastXMLDataHarvestDate, lastProductionDate, shortTitle, sponsorDate, titleID, year, identifierBib, zQuery, licenseUrl, rights, dueDiligence, possibleCopyrightStatus, copyrightRegion, copyrightComment, copyrightEvidence, copyrightEvidenceOperator, copyrightEvidenceDate, localFileFolder, noMARCOk, scanningInstitution, rightsHolder, itemDescription );
		}
		
		/// <summary>
		/// Insert values into dbo.IAItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="iAIdentifierPrefix"></param>
		/// <param name="iAIdentifier"></param>
		/// <param name="sponsor"></param>
		/// <param name="sponsorName"></param>
		/// <param name="scanningCenter"></param>
		/// <param name="callNumber"></param>
		/// <param name="imageCount"></param>
		/// <param name="identifierAccessUrl"></param>
		/// <param name="volume"></param>
		/// <param name="note"></param>
		/// <param name="scanOperator"></param>
		/// <param name="scanDate"></param>
		/// <param name="externalStatus"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="barCode"></param>
		/// <param name="iADateStamp"></param>
		/// <param name="iAAddedDate"></param>
		/// <param name="lastOAIDataHarvestDate"></param>
		/// <param name="lastXMLDataHarvestDate"></param>
		/// <param name="lastProductionDate"></param>
		/// <param name="shortTitle"></param>
		/// <param name="sponsorDate"></param>
		/// <param name="titleID"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="possibleCopyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="localFileFolder"></param>
		/// <param name="noMARCOk"></param>
		/// <param name="scanningInstitution"></param>
		/// <param name="rightsHolder"></param>
		/// <param name="itemDescription"></param>
		/// <returns>Object of type IAItem.</returns>
		public IAItem IAItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemStatusID,
			string iAIdentifierPrefix,
			string iAIdentifier,
			string sponsor,
			string sponsorName,
			string scanningCenter,
			string callNumber,
			int? imageCount,
			string identifierAccessUrl,
			string volume,
			string note,
			string scanOperator,
			string scanDate,
			string externalStatus,
			string mARCBibID,
			string barCode,
			DateTime? iADateStamp,
			DateTime? iAAddedDate,
			DateTime? lastOAIDataHarvestDate,
			DateTime? lastXMLDataHarvestDate,
			DateTime? lastProductionDate,
			string shortTitle,
			string sponsorDate,
			string titleID,
			string year,
			string identifierBib,
			string zQuery,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string possibleCopyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			string localFileFolder,
			byte noMARCOk,
			string scanningInstitution,
			string rightsHolder,
			string itemDescription)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("IAIdentifierPrefix", SqlDbType.NVarChar, 50, false, iAIdentifierPrefix),
					CustomSqlHelper.CreateInputParameter("IAIdentifier", SqlDbType.NVarChar, 50, false, iAIdentifier),
					CustomSqlHelper.CreateInputParameter("Sponsor", SqlDbType.NVarChar, 100, false, sponsor),
					CustomSqlHelper.CreateInputParameter("SponsorName", SqlDbType.NVarChar, 50, true, sponsorName),
					CustomSqlHelper.CreateInputParameter("ScanningCenter", SqlDbType.NVarChar, 50, false, scanningCenter),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 50, false, callNumber),
					CustomSqlHelper.CreateInputParameter("ImageCount", SqlDbType.Int, null, true, imageCount),
					CustomSqlHelper.CreateInputParameter("IdentifierAccessUrl", SqlDbType.NVarChar, 100, true, identifierAccessUrl),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 50, false, volume),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, false, note),
					CustomSqlHelper.CreateInputParameter("ScanOperator", SqlDbType.NVarChar, 100, false, scanOperator),
					CustomSqlHelper.CreateInputParameter("ScanDate", SqlDbType.NVarChar, 50, false, scanDate),
					CustomSqlHelper.CreateInputParameter("ExternalStatus", SqlDbType.NVarChar, 50, false, externalStatus),
					CustomSqlHelper.CreateInputParameter("MARCBibID", SqlDbType.NVarChar, 50, false, mARCBibID),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("IADateStamp", SqlDbType.DateTime, null, true, iADateStamp),
					CustomSqlHelper.CreateInputParameter("IAAddedDate", SqlDbType.DateTime, null, true, iAAddedDate),
					CustomSqlHelper.CreateInputParameter("LastOAIDataHarvestDate", SqlDbType.DateTime, null, true, lastOAIDataHarvestDate),
					CustomSqlHelper.CreateInputParameter("LastXMLDataHarvestDate", SqlDbType.DateTime, null, true, lastXMLDataHarvestDate),
					CustomSqlHelper.CreateInputParameter("LastProductionDate", SqlDbType.DateTime, null, true, lastProductionDate),
					CustomSqlHelper.CreateInputParameter("ShortTitle", SqlDbType.NVarChar, 255, true, shortTitle),
					CustomSqlHelper.CreateInputParameter("SponsorDate", SqlDbType.NVarChar, 50, true, sponsorDate),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.NVarChar, 50, false, titleID),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, false, year),
					CustomSqlHelper.CreateInputParameter("IdentifierBib", SqlDbType.NVarChar, 50, false, identifierBib),
					CustomSqlHelper.CreateInputParameter("ZQuery", SqlDbType.NVarChar, 200, false, zQuery),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, 1073741823, false, licenseUrl),
					CustomSqlHelper.CreateInputParameter("Rights", SqlDbType.NVarChar, 1073741823, false, rights),
					CustomSqlHelper.CreateInputParameter("DueDiligence", SqlDbType.NVarChar, 1073741823, false, dueDiligence),
					CustomSqlHelper.CreateInputParameter("PossibleCopyrightStatus", SqlDbType.NVarChar, 1073741823, false, possibleCopyrightStatus),
					CustomSqlHelper.CreateInputParameter("CopyrightRegion", SqlDbType.NVarChar, 50, false, copyrightRegion),
					CustomSqlHelper.CreateInputParameter("CopyrightComment", SqlDbType.NVarChar, 1073741823, false, copyrightComment),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidence", SqlDbType.NVarChar, 1073741823, false, copyrightEvidence),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceOperator", SqlDbType.NVarChar, 100, false, copyrightEvidenceOperator),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceDate", SqlDbType.NVarChar, 30, false, copyrightEvidenceDate),
					CustomSqlHelper.CreateInputParameter("LocalFileFolder", SqlDbType.NVarChar, 200, false, localFileFolder),
					CustomSqlHelper.CreateInputParameter("NoMARCOk", SqlDbType.TinyInt, null, false, noMARCOk),
					CustomSqlHelper.CreateInputParameter("ScanningInstitution", SqlDbType.NVarChar, 500, false, scanningInstitution),
					CustomSqlHelper.CreateInputParameter("RightsHolder", SqlDbType.NVarChar, 500, false, rightsHolder),
					CustomSqlHelper.CreateInputParameter("ItemDescription", SqlDbType.NVarChar, 1073741823, false, itemDescription), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAItem> helper = new CustomSqlHelper<IAItem>())
				{
					CustomGenericList<IAItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItem o = list[0];
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
		/// Insert values into dbo.IAItem. Returns an object of type IAItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItem.</param>
		/// <returns>Object of type IAItem.</returns>
		public IAItem IAItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItem value)
		{
			return IAItemInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IAItem. Returns an object of type IAItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItem.</param>
		/// <returns>Object of type IAItem.</returns>
		public IAItem IAItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItem value)
		{
			return IAItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemStatusID,
				value.IAIdentifierPrefix,
				value.IAIdentifier,
				value.Sponsor,
				value.SponsorName,
				value.ScanningCenter,
				value.CallNumber,
				value.ImageCount,
				value.IdentifierAccessUrl,
				value.Volume,
				value.Note,
				value.ScanOperator,
				value.ScanDate,
				value.ExternalStatus,
				value.MARCBibID,
				value.BarCode,
				value.IADateStamp,
				value.IAAddedDate,
				value.LastOAIDataHarvestDate,
				value.LastXMLDataHarvestDate,
				value.LastProductionDate,
				value.ShortTitle,
				value.SponsorDate,
				value.TitleID,
				value.Year,
				value.IdentifierBib,
				value.ZQuery,
				value.LicenseUrl,
				value.Rights,
				value.DueDiligence,
				value.PossibleCopyrightStatus,
				value.CopyrightRegion,
				value.CopyrightComment,
				value.CopyrightEvidence,
				value.CopyrightEvidenceOperator,
				value.CopyrightEvidenceDate,
				value.LocalFileFolder,
				value.NoMARCOk,
				value.ScanningInstitution,
				value.RightsHolder,
				value.ItemDescription);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IAItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return IAItemDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", itemID );
		}
		
		/// <summary>
		/// Delete values from dbo.IAItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemDeleteAuto", connection, transaction, 
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
		/// Update values in dbo.IAItem. Returns an object of type IAItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="iAIdentifierPrefix"></param>
		/// <param name="iAIdentifier"></param>
		/// <param name="sponsor"></param>
		/// <param name="sponsorName"></param>
		/// <param name="scanningCenter"></param>
		/// <param name="callNumber"></param>
		/// <param name="imageCount"></param>
		/// <param name="identifierAccessUrl"></param>
		/// <param name="volume"></param>
		/// <param name="note"></param>
		/// <param name="scanOperator"></param>
		/// <param name="scanDate"></param>
		/// <param name="externalStatus"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="barCode"></param>
		/// <param name="iADateStamp"></param>
		/// <param name="iAAddedDate"></param>
		/// <param name="lastOAIDataHarvestDate"></param>
		/// <param name="lastXMLDataHarvestDate"></param>
		/// <param name="lastProductionDate"></param>
		/// <param name="shortTitle"></param>
		/// <param name="sponsorDate"></param>
		/// <param name="titleID"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="possibleCopyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="localFileFolder"></param>
		/// <param name="noMARCOk"></param>
		/// <param name="scanningInstitution"></param>
		/// <param name="rightsHolder"></param>
		/// <param name="itemDescription"></param>
		/// <returns>Object of type IAItem.</returns>
		public IAItem IAItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int itemStatusID,
			string iAIdentifierPrefix,
			string iAIdentifier,
			string sponsor,
			string sponsorName,
			string scanningCenter,
			string callNumber,
			int? imageCount,
			string identifierAccessUrl,
			string volume,
			string note,
			string scanOperator,
			string scanDate,
			string externalStatus,
			string mARCBibID,
			string barCode,
			DateTime? iADateStamp,
			DateTime? iAAddedDate,
			DateTime? lastOAIDataHarvestDate,
			DateTime? lastXMLDataHarvestDate,
			DateTime? lastProductionDate,
			string shortTitle,
			string sponsorDate,
			string titleID,
			string year,
			string identifierBib,
			string zQuery,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string possibleCopyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			string localFileFolder,
			byte noMARCOk,
			string scanningInstitution,
			string rightsHolder,
			string itemDescription)
		{
			return IAItemUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, itemStatusID, iAIdentifierPrefix, iAIdentifier, sponsor, sponsorName, scanningCenter, callNumber, imageCount, identifierAccessUrl, volume, note, scanOperator, scanDate, externalStatus, mARCBibID, barCode, iADateStamp, iAAddedDate, lastOAIDataHarvestDate, lastXMLDataHarvestDate, lastProductionDate, shortTitle, sponsorDate, titleID, year, identifierBib, zQuery, licenseUrl, rights, dueDiligence, possibleCopyrightStatus, copyrightRegion, copyrightComment, copyrightEvidence, copyrightEvidenceOperator, copyrightEvidenceDate, localFileFolder, noMARCOk, scanningInstitution, rightsHolder, itemDescription);
		}
		
		/// <summary>
		/// Update values in dbo.IAItem. Returns an object of type IAItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="iAIdentifierPrefix"></param>
		/// <param name="iAIdentifier"></param>
		/// <param name="sponsor"></param>
		/// <param name="sponsorName"></param>
		/// <param name="scanningCenter"></param>
		/// <param name="callNumber"></param>
		/// <param name="imageCount"></param>
		/// <param name="identifierAccessUrl"></param>
		/// <param name="volume"></param>
		/// <param name="note"></param>
		/// <param name="scanOperator"></param>
		/// <param name="scanDate"></param>
		/// <param name="externalStatus"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="barCode"></param>
		/// <param name="iADateStamp"></param>
		/// <param name="iAAddedDate"></param>
		/// <param name="lastOAIDataHarvestDate"></param>
		/// <param name="lastXMLDataHarvestDate"></param>
		/// <param name="lastProductionDate"></param>
		/// <param name="shortTitle"></param>
		/// <param name="sponsorDate"></param>
		/// <param name="titleID"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="possibleCopyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="localFileFolder"></param>
		/// <param name="noMARCOk"></param>
		/// <param name="scanningInstitution"></param>
		/// <param name="rightsHolder"></param>
		/// <param name="itemDescription"></param>
		/// <returns>Object of type IAItem.</returns>
		public IAItem IAItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int itemStatusID,
			string iAIdentifierPrefix,
			string iAIdentifier,
			string sponsor,
			string sponsorName,
			string scanningCenter,
			string callNumber,
			int? imageCount,
			string identifierAccessUrl,
			string volume,
			string note,
			string scanOperator,
			string scanDate,
			string externalStatus,
			string mARCBibID,
			string barCode,
			DateTime? iADateStamp,
			DateTime? iAAddedDate,
			DateTime? lastOAIDataHarvestDate,
			DateTime? lastXMLDataHarvestDate,
			DateTime? lastProductionDate,
			string shortTitle,
			string sponsorDate,
			string titleID,
			string year,
			string identifierBib,
			string zQuery,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string possibleCopyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			string localFileFolder,
			byte noMARCOk,
			string scanningInstitution,
			string rightsHolder,
			string itemDescription)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("IAIdentifierPrefix", SqlDbType.NVarChar, 50, false, iAIdentifierPrefix),
					CustomSqlHelper.CreateInputParameter("IAIdentifier", SqlDbType.NVarChar, 50, false, iAIdentifier),
					CustomSqlHelper.CreateInputParameter("Sponsor", SqlDbType.NVarChar, 100, false, sponsor),
					CustomSqlHelper.CreateInputParameter("SponsorName", SqlDbType.NVarChar, 50, true, sponsorName),
					CustomSqlHelper.CreateInputParameter("ScanningCenter", SqlDbType.NVarChar, 50, false, scanningCenter),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 50, false, callNumber),
					CustomSqlHelper.CreateInputParameter("ImageCount", SqlDbType.Int, null, true, imageCount),
					CustomSqlHelper.CreateInputParameter("IdentifierAccessUrl", SqlDbType.NVarChar, 100, true, identifierAccessUrl),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 50, false, volume),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, false, note),
					CustomSqlHelper.CreateInputParameter("ScanOperator", SqlDbType.NVarChar, 100, false, scanOperator),
					CustomSqlHelper.CreateInputParameter("ScanDate", SqlDbType.NVarChar, 50, false, scanDate),
					CustomSqlHelper.CreateInputParameter("ExternalStatus", SqlDbType.NVarChar, 50, false, externalStatus),
					CustomSqlHelper.CreateInputParameter("MARCBibID", SqlDbType.NVarChar, 50, false, mARCBibID),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("IADateStamp", SqlDbType.DateTime, null, true, iADateStamp),
					CustomSqlHelper.CreateInputParameter("IAAddedDate", SqlDbType.DateTime, null, true, iAAddedDate),
					CustomSqlHelper.CreateInputParameter("LastOAIDataHarvestDate", SqlDbType.DateTime, null, true, lastOAIDataHarvestDate),
					CustomSqlHelper.CreateInputParameter("LastXMLDataHarvestDate", SqlDbType.DateTime, null, true, lastXMLDataHarvestDate),
					CustomSqlHelper.CreateInputParameter("LastProductionDate", SqlDbType.DateTime, null, true, lastProductionDate),
					CustomSqlHelper.CreateInputParameter("ShortTitle", SqlDbType.NVarChar, 255, true, shortTitle),
					CustomSqlHelper.CreateInputParameter("SponsorDate", SqlDbType.NVarChar, 50, true, sponsorDate),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.NVarChar, 50, false, titleID),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, false, year),
					CustomSqlHelper.CreateInputParameter("IdentifierBib", SqlDbType.NVarChar, 50, false, identifierBib),
					CustomSqlHelper.CreateInputParameter("ZQuery", SqlDbType.NVarChar, 200, false, zQuery),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, 1073741823, false, licenseUrl),
					CustomSqlHelper.CreateInputParameter("Rights", SqlDbType.NVarChar, 1073741823, false, rights),
					CustomSqlHelper.CreateInputParameter("DueDiligence", SqlDbType.NVarChar, 1073741823, false, dueDiligence),
					CustomSqlHelper.CreateInputParameter("PossibleCopyrightStatus", SqlDbType.NVarChar, 1073741823, false, possibleCopyrightStatus),
					CustomSqlHelper.CreateInputParameter("CopyrightRegion", SqlDbType.NVarChar, 50, false, copyrightRegion),
					CustomSqlHelper.CreateInputParameter("CopyrightComment", SqlDbType.NVarChar, 1073741823, false, copyrightComment),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidence", SqlDbType.NVarChar, 1073741823, false, copyrightEvidence),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceOperator", SqlDbType.NVarChar, 100, false, copyrightEvidenceOperator),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidenceDate", SqlDbType.NVarChar, 30, false, copyrightEvidenceDate),
					CustomSqlHelper.CreateInputParameter("LocalFileFolder", SqlDbType.NVarChar, 200, false, localFileFolder),
					CustomSqlHelper.CreateInputParameter("NoMARCOk", SqlDbType.TinyInt, null, false, noMARCOk),
					CustomSqlHelper.CreateInputParameter("ScanningInstitution", SqlDbType.NVarChar, 500, false, scanningInstitution),
					CustomSqlHelper.CreateInputParameter("RightsHolder", SqlDbType.NVarChar, 500, false, rightsHolder),
					CustomSqlHelper.CreateInputParameter("ItemDescription", SqlDbType.NVarChar, 1073741823, false, itemDescription), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAItem> helper = new CustomSqlHelper<IAItem>())
				{
					CustomGenericList<IAItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItem o = list[0];
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
		/// Update values in dbo.IAItem. Returns an object of type IAItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItem.</param>
		/// <returns>Object of type IAItem.</returns>
		public IAItem IAItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItem value)
		{
			return IAItemUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IAItem. Returns an object of type IAItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItem.</param>
		/// <returns>Object of type IAItem.</returns>
		public IAItem IAItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItem value)
		{
			return IAItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.ItemStatusID,
				value.IAIdentifierPrefix,
				value.IAIdentifier,
				value.Sponsor,
				value.SponsorName,
				value.ScanningCenter,
				value.CallNumber,
				value.ImageCount,
				value.IdentifierAccessUrl,
				value.Volume,
				value.Note,
				value.ScanOperator,
				value.ScanDate,
				value.ExternalStatus,
				value.MARCBibID,
				value.BarCode,
				value.IADateStamp,
				value.IAAddedDate,
				value.LastOAIDataHarvestDate,
				value.LastXMLDataHarvestDate,
				value.LastProductionDate,
				value.ShortTitle,
				value.SponsorDate,
				value.TitleID,
				value.Year,
				value.IdentifierBib,
				value.ZQuery,
				value.LicenseUrl,
				value.Rights,
				value.DueDiligence,
				value.PossibleCopyrightStatus,
				value.CopyrightRegion,
				value.CopyrightComment,
				value.CopyrightEvidence,
				value.CopyrightEvidenceOperator,
				value.CopyrightEvidenceDate,
				value.LocalFileFolder,
				value.NoMARCOk,
				value.ScanningInstitution,
				value.RightsHolder,
				value.ItemDescription);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IAItem object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItem.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAItem>.</returns>
		public CustomDataAccessStatus<IAItem> IAItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItem value  )
		{
			return IAItemManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IAItem object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItem.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAItem>.</returns>
		public CustomDataAccessStatus<IAItem> IAItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItem value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAItem returnValue = IAItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemStatusID,
						value.IAIdentifierPrefix,
						value.IAIdentifier,
						value.Sponsor,
						value.SponsorName,
						value.ScanningCenter,
						value.CallNumber,
						value.ImageCount,
						value.IdentifierAccessUrl,
						value.Volume,
						value.Note,
						value.ScanOperator,
						value.ScanDate,
						value.ExternalStatus,
						value.MARCBibID,
						value.BarCode,
						value.IADateStamp,
						value.IAAddedDate,
						value.LastOAIDataHarvestDate,
						value.LastXMLDataHarvestDate,
						value.LastProductionDate,
						value.ShortTitle,
						value.SponsorDate,
						value.TitleID,
						value.Year,
						value.IdentifierBib,
						value.ZQuery,
						value.LicenseUrl,
						value.Rights,
						value.DueDiligence,
						value.PossibleCopyrightStatus,
						value.CopyrightRegion,
						value.CopyrightComment,
						value.CopyrightEvidence,
						value.CopyrightEvidenceOperator,
						value.CopyrightEvidenceDate,
						value.LocalFileFolder,
						value.NoMARCOk,
						value.ScanningInstitution,
						value.RightsHolder,
						value.ItemDescription);
				
				return new CustomDataAccessStatus<IAItem>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAItemDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID))
				{
				return new CustomDataAccessStatus<IAItem>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAItem>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAItem returnValue = IAItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.ItemStatusID,
						value.IAIdentifierPrefix,
						value.IAIdentifier,
						value.Sponsor,
						value.SponsorName,
						value.ScanningCenter,
						value.CallNumber,
						value.ImageCount,
						value.IdentifierAccessUrl,
						value.Volume,
						value.Note,
						value.ScanOperator,
						value.ScanDate,
						value.ExternalStatus,
						value.MARCBibID,
						value.BarCode,
						value.IADateStamp,
						value.IAAddedDate,
						value.LastOAIDataHarvestDate,
						value.LastXMLDataHarvestDate,
						value.LastProductionDate,
						value.ShortTitle,
						value.SponsorDate,
						value.TitleID,
						value.Year,
						value.IdentifierBib,
						value.ZQuery,
						value.LicenseUrl,
						value.Rights,
						value.DueDiligence,
						value.PossibleCopyrightStatus,
						value.CopyrightRegion,
						value.CopyrightComment,
						value.CopyrightEvidence,
						value.CopyrightEvidenceOperator,
						value.CopyrightEvidenceDate,
						value.LocalFileFolder,
						value.NoMARCOk,
						value.ScanningInstitution,
						value.RightsHolder,
						value.ItemDescription);
					
				return new CustomDataAccessStatus<IAItem>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAItem>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


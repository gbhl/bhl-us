
// Generated 10/31/2013 4:01:46 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class OAIRecordDAL is based upon OAIRecord.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class OAIRecordDAL
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
	partial class OAIRecordDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from OAIRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordID"></param>
		/// <returns>Object of type OAIRecord.</returns>
		public OAIRecord OAIRecordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordID)
		{
			return OAIRecordSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	oAIRecordID );
		}
			
		/// <summary>
		/// Select values from OAIRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordID"></param>
		/// <returns>Object of type OAIRecord.</returns>
		public OAIRecord OAIRecordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID)))
			{
				using (CustomSqlHelper<OAIRecord> helper = new CustomSqlHelper<OAIRecord>())
				{
					CustomGenericList<OAIRecord> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecord o = list[0];
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
		/// Select values from OAIRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> OAIRecordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordID)
		{
			return OAIRecordSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", oAIRecordID );
		}
		
		/// <summary>
		/// Select values from OAIRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> OAIRecordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into OAIRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="harvestLogID"></param>
		/// <param name="oAIIdentifier"></param>
		/// <param name="oAIDateStamp"></param>
		/// <param name="oAIStatus"></param>
		/// <param name="recordType"></param>
		/// <param name="title"></param>
		/// <param name="containerTitle"></param>
		/// <param name="contributor"></param>
		/// <param name="date"></param>
		/// <param name="language"></param>
		/// <param name="publisher"></param>
		/// <param name="publicationPlace"></param>
		/// <param name="publicationDate"></param>
		/// <param name="edition"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="startPage"></param>
		/// <param name="endPage"></param>
		/// <param name="issn"></param>
		/// <param name="isbn"></param>
		/// <param name="lccn"></param>
		/// <param name="doi"></param>
		/// <param name="url"></param>
		/// <param name="oAIRecordStatusID"></param>
		/// <param name="productionEntityType"></param>
		/// <param name="productionEntityID"></param>
		/// <returns>Object of type OAIRecord.</returns>
		public OAIRecord OAIRecordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int harvestLogID,
			string oAIIdentifier,
			string oAIDateStamp,
			string oAIStatus,
			string recordType,
			string title,
			string containerTitle,
			string contributor,
			string date,
			string language,
			string publisher,
			string publicationPlace,
			string publicationDate,
			string edition,
			string volume,
			string issue,
			string startPage,
			string endPage,
			string issn,
			string isbn,
			string lccn,
			string doi,
			string url,
			int oAIRecordStatusID,
			string productionEntityType,
			int? productionEntityID)
		{
			return OAIRecordInsertAuto( sqlConnection, sqlTransaction, "BHLImport", harvestLogID, oAIIdentifier, oAIDateStamp, oAIStatus, recordType, title, containerTitle, contributor, date, language, publisher, publicationPlace, publicationDate, edition, volume, issue, startPage, endPage, issn, isbn, lccn, doi, url, oAIRecordStatusID, productionEntityType, productionEntityID );
		}
		
		/// <summary>
		/// Insert values into OAIRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="harvestLogID"></param>
		/// <param name="oAIIdentifier"></param>
		/// <param name="oAIDateStamp"></param>
		/// <param name="oAIStatus"></param>
		/// <param name="recordType"></param>
		/// <param name="title"></param>
		/// <param name="containerTitle"></param>
		/// <param name="contributor"></param>
		/// <param name="date"></param>
		/// <param name="language"></param>
		/// <param name="publisher"></param>
		/// <param name="publicationPlace"></param>
		/// <param name="publicationDate"></param>
		/// <param name="edition"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="startPage"></param>
		/// <param name="endPage"></param>
		/// <param name="issn"></param>
		/// <param name="isbn"></param>
		/// <param name="lccn"></param>
		/// <param name="doi"></param>
		/// <param name="url"></param>
		/// <param name="oAIRecordStatusID"></param>
		/// <param name="productionEntityType"></param>
		/// <param name="productionEntityID"></param>
		/// <returns>Object of type OAIRecord.</returns>
		public OAIRecord OAIRecordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int harvestLogID,
			string oAIIdentifier,
			string oAIDateStamp,
			string oAIStatus,
			string recordType,
			string title,
			string containerTitle,
			string contributor,
			string date,
			string language,
			string publisher,
			string publicationPlace,
			string publicationDate,
			string edition,
			string volume,
			string issue,
			string startPage,
			string endPage,
			string issn,
			string isbn,
			string lccn,
			string doi,
			string url,
			int oAIRecordStatusID,
			string productionEntityType,
			int? productionEntityID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("OAIRecordID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("HarvestLogID", SqlDbType.Int, null, false, harvestLogID),
					CustomSqlHelper.CreateInputParameter("OAIIdentifier", SqlDbType.NVarChar, 100, false, oAIIdentifier),
					CustomSqlHelper.CreateInputParameter("OAIDateStamp", SqlDbType.NVarChar, 30, false, oAIDateStamp),
					CustomSqlHelper.CreateInputParameter("OAIStatus", SqlDbType.NVarChar, 20, false, oAIStatus),
					CustomSqlHelper.CreateInputParameter("RecordType", SqlDbType.NVarChar, 20, false, recordType),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
					CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
					CustomSqlHelper.CreateInputParameter("Contributor", SqlDbType.NVarChar, 200, false, contributor),
					CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
					CustomSqlHelper.CreateInputParameter("Language", SqlDbType.NVarChar, 30, false, language),
					CustomSqlHelper.CreateInputParameter("Publisher", SqlDbType.NVarChar, 250, false, publisher),
					CustomSqlHelper.CreateInputParameter("PublicationPlace", SqlDbType.NVarChar, 150, false, publicationPlace),
					CustomSqlHelper.CreateInputParameter("PublicationDate", SqlDbType.NVarChar, 100, false, publicationDate),
					CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 450, false, edition),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
					CustomSqlHelper.CreateInputParameter("StartPage", SqlDbType.NVarChar, 20, false, startPage),
					CustomSqlHelper.CreateInputParameter("EndPage", SqlDbType.NVarChar, 20, false, endPage),
					CustomSqlHelper.CreateInputParameter("Issn", SqlDbType.NVarChar, 125, false, issn),
					CustomSqlHelper.CreateInputParameter("Isbn", SqlDbType.NVarChar, 125, false, isbn),
					CustomSqlHelper.CreateInputParameter("Lccn", SqlDbType.NVarChar, 125, false, lccn),
					CustomSqlHelper.CreateInputParameter("Doi", SqlDbType.NVarChar, 50, false, doi),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("OAIRecordStatusID", SqlDbType.Int, null, false, oAIRecordStatusID),
					CustomSqlHelper.CreateInputParameter("ProductionEntityType", SqlDbType.NVarChar, 5, true, productionEntityType),
					CustomSqlHelper.CreateInputParameter("ProductionEntityID", SqlDbType.Int, null, true, productionEntityID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecord> helper = new CustomSqlHelper<OAIRecord>())
				{
					CustomGenericList<OAIRecord> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecord o = list[0];
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
		/// Insert values into OAIRecord. Returns an object of type OAIRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecord.</param>
		/// <returns>Object of type OAIRecord.</returns>
		public OAIRecord OAIRecordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecord value)
		{
			return OAIRecordInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into OAIRecord. Returns an object of type OAIRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecord.</param>
		/// <returns>Object of type OAIRecord.</returns>
		public OAIRecord OAIRecordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecord value)
		{
			return OAIRecordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.HarvestLogID,
				value.OAIIdentifier,
				value.OAIDateStamp,
				value.OAIStatus,
				value.RecordType,
				value.Title,
				value.ContainerTitle,
				value.Contributor,
				value.Date,
				value.Language,
				value.Publisher,
				value.PublicationPlace,
				value.PublicationDate,
				value.Edition,
				value.Volume,
				value.Issue,
				value.StartPage,
				value.EndPage,
				value.Issn,
				value.Isbn,
				value.Lccn,
				value.Doi,
				value.Url,
				value.OAIRecordStatusID,
				value.ProductionEntityType,
				value.ProductionEntityID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from OAIRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordID)
		{
			return OAIRecordDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordID );
		}
		
		/// <summary>
		/// Delete values from OAIRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID), 
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
		/// Update values in OAIRecord. Returns an object of type OAIRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="harvestLogID"></param>
		/// <param name="oAIIdentifier"></param>
		/// <param name="oAIDateStamp"></param>
		/// <param name="oAIStatus"></param>
		/// <param name="recordType"></param>
		/// <param name="title"></param>
		/// <param name="containerTitle"></param>
		/// <param name="contributor"></param>
		/// <param name="date"></param>
		/// <param name="language"></param>
		/// <param name="publisher"></param>
		/// <param name="publicationPlace"></param>
		/// <param name="publicationDate"></param>
		/// <param name="edition"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="startPage"></param>
		/// <param name="endPage"></param>
		/// <param name="issn"></param>
		/// <param name="isbn"></param>
		/// <param name="lccn"></param>
		/// <param name="doi"></param>
		/// <param name="url"></param>
		/// <param name="oAIRecordStatusID"></param>
		/// <param name="productionEntityType"></param>
		/// <param name="productionEntityID"></param>
		/// <returns>Object of type OAIRecord.</returns>
		public OAIRecord OAIRecordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordID,
			int harvestLogID,
			string oAIIdentifier,
			string oAIDateStamp,
			string oAIStatus,
			string recordType,
			string title,
			string containerTitle,
			string contributor,
			string date,
			string language,
			string publisher,
			string publicationPlace,
			string publicationDate,
			string edition,
			string volume,
			string issue,
			string startPage,
			string endPage,
			string issn,
			string isbn,
			string lccn,
			string doi,
			string url,
			int oAIRecordStatusID,
			string productionEntityType,
			int? productionEntityID)
		{
			return OAIRecordUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordID, harvestLogID, oAIIdentifier, oAIDateStamp, oAIStatus, recordType, title, containerTitle, contributor, date, language, publisher, publicationPlace, publicationDate, edition, volume, issue, startPage, endPage, issn, isbn, lccn, doi, url, oAIRecordStatusID, productionEntityType, productionEntityID);
		}
		
		/// <summary>
		/// Update values in OAIRecord. Returns an object of type OAIRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="harvestLogID"></param>
		/// <param name="oAIIdentifier"></param>
		/// <param name="oAIDateStamp"></param>
		/// <param name="oAIStatus"></param>
		/// <param name="recordType"></param>
		/// <param name="title"></param>
		/// <param name="containerTitle"></param>
		/// <param name="contributor"></param>
		/// <param name="date"></param>
		/// <param name="language"></param>
		/// <param name="publisher"></param>
		/// <param name="publicationPlace"></param>
		/// <param name="publicationDate"></param>
		/// <param name="edition"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="startPage"></param>
		/// <param name="endPage"></param>
		/// <param name="issn"></param>
		/// <param name="isbn"></param>
		/// <param name="lccn"></param>
		/// <param name="doi"></param>
		/// <param name="url"></param>
		/// <param name="oAIRecordStatusID"></param>
		/// <param name="productionEntityType"></param>
		/// <param name="productionEntityID"></param>
		/// <returns>Object of type OAIRecord.</returns>
		public OAIRecord OAIRecordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordID,
			int harvestLogID,
			string oAIIdentifier,
			string oAIDateStamp,
			string oAIStatus,
			string recordType,
			string title,
			string containerTitle,
			string contributor,
			string date,
			string language,
			string publisher,
			string publicationPlace,
			string publicationDate,
			string edition,
			string volume,
			string issue,
			string startPage,
			string endPage,
			string issn,
			string isbn,
			string lccn,
			string doi,
			string url,
			int oAIRecordStatusID,
			string productionEntityType,
			int? productionEntityID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID),
					CustomSqlHelper.CreateInputParameter("HarvestLogID", SqlDbType.Int, null, false, harvestLogID),
					CustomSqlHelper.CreateInputParameter("OAIIdentifier", SqlDbType.NVarChar, 100, false, oAIIdentifier),
					CustomSqlHelper.CreateInputParameter("OAIDateStamp", SqlDbType.NVarChar, 30, false, oAIDateStamp),
					CustomSqlHelper.CreateInputParameter("OAIStatus", SqlDbType.NVarChar, 20, false, oAIStatus),
					CustomSqlHelper.CreateInputParameter("RecordType", SqlDbType.NVarChar, 20, false, recordType),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
					CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
					CustomSqlHelper.CreateInputParameter("Contributor", SqlDbType.NVarChar, 200, false, contributor),
					CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
					CustomSqlHelper.CreateInputParameter("Language", SqlDbType.NVarChar, 30, false, language),
					CustomSqlHelper.CreateInputParameter("Publisher", SqlDbType.NVarChar, 250, false, publisher),
					CustomSqlHelper.CreateInputParameter("PublicationPlace", SqlDbType.NVarChar, 150, false, publicationPlace),
					CustomSqlHelper.CreateInputParameter("PublicationDate", SqlDbType.NVarChar, 100, false, publicationDate),
					CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 450, false, edition),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
					CustomSqlHelper.CreateInputParameter("StartPage", SqlDbType.NVarChar, 20, false, startPage),
					CustomSqlHelper.CreateInputParameter("EndPage", SqlDbType.NVarChar, 20, false, endPage),
					CustomSqlHelper.CreateInputParameter("Issn", SqlDbType.NVarChar, 125, false, issn),
					CustomSqlHelper.CreateInputParameter("Isbn", SqlDbType.NVarChar, 125, false, isbn),
					CustomSqlHelper.CreateInputParameter("Lccn", SqlDbType.NVarChar, 125, false, lccn),
					CustomSqlHelper.CreateInputParameter("Doi", SqlDbType.NVarChar, 50, false, doi),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("OAIRecordStatusID", SqlDbType.Int, null, false, oAIRecordStatusID),
					CustomSqlHelper.CreateInputParameter("ProductionEntityType", SqlDbType.NVarChar, 5, true, productionEntityType),
					CustomSqlHelper.CreateInputParameter("ProductionEntityID", SqlDbType.Int, null, true, productionEntityID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecord> helper = new CustomSqlHelper<OAIRecord>())
				{
					CustomGenericList<OAIRecord> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecord o = list[0];
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
		/// Update values in OAIRecord. Returns an object of type OAIRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecord.</param>
		/// <returns>Object of type OAIRecord.</returns>
		public OAIRecord OAIRecordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecord value)
		{
			return OAIRecordUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in OAIRecord. Returns an object of type OAIRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecord.</param>
		/// <returns>Object of type OAIRecord.</returns>
		public OAIRecord OAIRecordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecord value)
		{
			return OAIRecordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordID,
				value.HarvestLogID,
				value.OAIIdentifier,
				value.OAIDateStamp,
				value.OAIStatus,
				value.RecordType,
				value.Title,
				value.ContainerTitle,
				value.Contributor,
				value.Date,
				value.Language,
				value.Publisher,
				value.PublicationPlace,
				value.PublicationDate,
				value.Edition,
				value.Volume,
				value.Issue,
				value.StartPage,
				value.EndPage,
				value.Issn,
				value.Isbn,
				value.Lccn,
				value.Doi,
				value.Url,
				value.OAIRecordStatusID,
				value.ProductionEntityType,
				value.ProductionEntityID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage OAIRecord object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in OAIRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecord.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecord>.</returns>
		public CustomDataAccessStatus<OAIRecord> OAIRecordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecord value  )
		{
			return OAIRecordManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage OAIRecord object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in OAIRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecord.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecord>.</returns>
		public CustomDataAccessStatus<OAIRecord> OAIRecordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecord value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				OAIRecord returnValue = OAIRecordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.HarvestLogID,
						value.OAIIdentifier,
						value.OAIDateStamp,
						value.OAIStatus,
						value.RecordType,
						value.Title,
						value.ContainerTitle,
						value.Contributor,
						value.Date,
						value.Language,
						value.Publisher,
						value.PublicationPlace,
						value.PublicationDate,
						value.Edition,
						value.Volume,
						value.Issue,
						value.StartPage,
						value.EndPage,
						value.Issn,
						value.Isbn,
						value.Lccn,
						value.Doi,
						value.Url,
						value.OAIRecordStatusID,
						value.ProductionEntityType,
						value.ProductionEntityID);
				
				return new CustomDataAccessStatus<OAIRecord>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (OAIRecordDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordID))
				{
				return new CustomDataAccessStatus<OAIRecord>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<OAIRecord>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				OAIRecord returnValue = OAIRecordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordID,
						value.HarvestLogID,
						value.OAIIdentifier,
						value.OAIDateStamp,
						value.OAIStatus,
						value.RecordType,
						value.Title,
						value.ContainerTitle,
						value.Contributor,
						value.Date,
						value.Language,
						value.Publisher,
						value.PublicationPlace,
						value.PublicationDate,
						value.Edition,
						value.Volume,
						value.Issue,
						value.StartPage,
						value.EndPage,
						value.Issn,
						value.Isbn,
						value.Lccn,
						value.Doi,
						value.Url,
						value.OAIRecordStatusID,
						value.ProductionEntityType,
						value.ProductionEntityID);
					
				return new CustomDataAccessStatus<OAIRecord>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<OAIRecord>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

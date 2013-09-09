
// Generated 5/19/2009 10:35:29 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleDAL is based upon Title.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class TitleDAL
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
	partial class TitleDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from Title by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <returns>Object of type Title.</returns>
		public Title TitleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID)
		{
			return TitleSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	titleID );
		}
			
		/// <summary>
		/// Select values from Title by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <returns>Object of type Title.</returns>
		public Title TitleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
			{
				using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
				{
					CustomGenericList<Title> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title o = list[0];
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
		/// Select values from Title by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID)
		{
			return TitleSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", titleID );
		}
		
		/// <summary>
		/// Select values from Title by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="fullTitle"></param>
		/// <param name="shortTitle"></param>
		/// <param name="uniformTitle"></param>
		/// <param name="sortTitle"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="callNumber"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="datafield_260_a"></param>
		/// <param name="datafield_260_b"></param>
		/// <param name="datafield_260_c"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="titleDescription"></param>
		/// <param name="tL2Author"></param>
		/// <param name="publishReady"></param>
		/// <param name="rareBooks"></param>
		/// <param name="originalCatalogingSource"></param>
		/// <param name="editionStatement"></param>
		/// <param name="currentPublicationFrequency"></param>
		/// <param name="note"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="productionTitleID"></param>
		/// <returns>Object of type Title.</returns>
		public Title TitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string importKey,
			int importStatusID,
			int? importSourceID,
			string mARCBibID,
			string mARCLeader,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string partNumber,
			string partName,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string institutionCode,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool? publishReady,
			bool? rareBooks,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string note,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate,
			int? productionTitleID)
		{
			return TitleInsertAuto( sqlConnection, sqlTransaction, "BHLImport", importKey, importStatusID, importSourceID, mARCBibID, mARCLeader, fullTitle, shortTitle, uniformTitle, sortTitle, partNumber, partName, callNumber, publicationDetails, startYear, endYear, datafield_260_a, datafield_260_b, datafield_260_c, institutionCode, languageCode, titleDescription, tL2Author, publishReady, rareBooks, originalCatalogingSource, editionStatement, currentPublicationFrequency, note, externalCreationDate, externalLastModifiedDate, externalCreationUser, externalLastModifiedUser, productionDate, productionTitleID );
		}
		
		/// <summary>
		/// Insert values into Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="fullTitle"></param>
		/// <param name="shortTitle"></param>
		/// <param name="uniformTitle"></param>
		/// <param name="sortTitle"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="callNumber"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="datafield_260_a"></param>
		/// <param name="datafield_260_b"></param>
		/// <param name="datafield_260_c"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="titleDescription"></param>
		/// <param name="tL2Author"></param>
		/// <param name="publishReady"></param>
		/// <param name="rareBooks"></param>
		/// <param name="originalCatalogingSource"></param>
		/// <param name="editionStatement"></param>
		/// <param name="currentPublicationFrequency"></param>
		/// <param name="note"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="productionTitleID"></param>
		/// <returns>Object of type Title.</returns>
		public Title TitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string mARCBibID,
			string mARCLeader,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string partNumber,
			string partName,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string institutionCode,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool? publishReady,
			bool? rareBooks,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string note,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate,
			int? productionTitleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("MARCBibID", SqlDbType.NVarChar, 50, false, mARCBibID),
					CustomSqlHelper.CreateInputParameter("MARCLeader", SqlDbType.NVarChar, 24, true, mARCLeader),
					CustomSqlHelper.CreateInputParameter("FullTitle", SqlDbType.NText, 1073741823, true, fullTitle),
					CustomSqlHelper.CreateInputParameter("ShortTitle", SqlDbType.NVarChar, 255, true, shortTitle),
					CustomSqlHelper.CreateInputParameter("UniformTitle", SqlDbType.NVarChar, 255, true, uniformTitle),
					CustomSqlHelper.CreateInputParameter("SortTitle", SqlDbType.NVarChar, 60, true, sortTitle),
					CustomSqlHelper.CreateInputParameter("PartNumber", SqlDbType.NVarChar, 255, true, partNumber),
					CustomSqlHelper.CreateInputParameter("PartName", SqlDbType.NVarChar, 255, true, partName),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 100, true, callNumber),
					CustomSqlHelper.CreateInputParameter("PublicationDetails", SqlDbType.NVarChar, 255, true, publicationDetails),
					CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.SmallInt, null, true, startYear),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.SmallInt, null, true, endYear),
					CustomSqlHelper.CreateInputParameter("Datafield_260_a", SqlDbType.NVarChar, 150, true, datafield_260_a),
					CustomSqlHelper.CreateInputParameter("Datafield_260_b", SqlDbType.NVarChar, 255, true, datafield_260_b),
					CustomSqlHelper.CreateInputParameter("Datafield_260_c", SqlDbType.NVarChar, 100, true, datafield_260_c),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("TitleDescription", SqlDbType.NText, 1073741823, true, titleDescription),
					CustomSqlHelper.CreateInputParameter("TL2Author", SqlDbType.NVarChar, 100, true, tL2Author),
					CustomSqlHelper.CreateInputParameter("PublishReady", SqlDbType.Bit, null, true, publishReady),
					CustomSqlHelper.CreateInputParameter("RareBooks", SqlDbType.Bit, null, true, rareBooks),
					CustomSqlHelper.CreateInputParameter("OriginalCatalogingSource", SqlDbType.NVarChar, 100, true, originalCatalogingSource),
					CustomSqlHelper.CreateInputParameter("EditionStatement", SqlDbType.NVarChar, 450, true, editionStatement),
					CustomSqlHelper.CreateInputParameter("CurrentPublicationFrequency", SqlDbType.NVarChar, 100, true, currentPublicationFrequency),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationUser", SqlDbType.Int, null, true, externalCreationUser),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedUser", SqlDbType.Int, null, true, externalLastModifiedUser),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate),
					CustomSqlHelper.CreateInputParameter("ProductionTitleID", SqlDbType.Int, null, true, productionTitleID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
				{
					CustomGenericList<Title> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title o = list[0];
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
		/// Insert values into Title. Returns an object of type Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title.</param>
		/// <returns>Object of type Title.</returns>
		public Title TitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title value)
		{
			return TitleInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into Title. Returns an object of type Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title.</param>
		/// <returns>Object of type Title.</returns>
		public Title TitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title value)
		{
			return TitleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.MARCBibID,
				value.MARCLeader,
				value.FullTitle,
				value.ShortTitle,
				value.UniformTitle,
				value.SortTitle,
				value.PartNumber,
				value.PartName,
				value.CallNumber,
				value.PublicationDetails,
				value.StartYear,
				value.EndYear,
				value.Datafield_260_a,
				value.Datafield_260_b,
				value.Datafield_260_c,
				value.InstitutionCode,
				value.LanguageCode,
				value.TitleDescription,
				value.TL2Author,
				value.PublishReady,
				value.RareBooks,
				value.OriginalCatalogingSource,
				value.EditionStatement,
				value.CurrentPublicationFrequency,
				value.Note,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ExternalCreationUser,
				value.ExternalLastModifiedUser,
				value.ProductionDate,
				value.ProductionTitleID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Title by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID)
		{
			return TitleDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", titleID );
		}
		
		/// <summary>
		/// Delete values from Title by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID), 
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
		/// Update values in Title. Returns an object of type Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="fullTitle"></param>
		/// <param name="shortTitle"></param>
		/// <param name="uniformTitle"></param>
		/// <param name="sortTitle"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="callNumber"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="datafield_260_a"></param>
		/// <param name="datafield_260_b"></param>
		/// <param name="datafield_260_c"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="titleDescription"></param>
		/// <param name="tL2Author"></param>
		/// <param name="publishReady"></param>
		/// <param name="rareBooks"></param>
		/// <param name="originalCatalogingSource"></param>
		/// <param name="editionStatement"></param>
		/// <param name="currentPublicationFrequency"></param>
		/// <param name="note"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="productionTitleID"></param>
		/// <returns>Object of type Title.</returns>
		public Title TitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string mARCBibID,
			string mARCLeader,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string partNumber,
			string partName,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string institutionCode,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool? publishReady,
			bool? rareBooks,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string note,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate,
			int? productionTitleID)
		{
			return TitleUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", titleID, importKey, importStatusID, importSourceID, mARCBibID, mARCLeader, fullTitle, shortTitle, uniformTitle, sortTitle, partNumber, partName, callNumber, publicationDetails, startYear, endYear, datafield_260_a, datafield_260_b, datafield_260_c, institutionCode, languageCode, titleDescription, tL2Author, publishReady, rareBooks, originalCatalogingSource, editionStatement, currentPublicationFrequency, note, externalCreationDate, externalLastModifiedDate, externalCreationUser, externalLastModifiedUser, productionDate, productionTitleID);
		}
		
		/// <summary>
		/// Update values in Title. Returns an object of type Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="fullTitle"></param>
		/// <param name="shortTitle"></param>
		/// <param name="uniformTitle"></param>
		/// <param name="sortTitle"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="callNumber"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="datafield_260_a"></param>
		/// <param name="datafield_260_b"></param>
		/// <param name="datafield_260_c"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="titleDescription"></param>
		/// <param name="tL2Author"></param>
		/// <param name="publishReady"></param>
		/// <param name="rareBooks"></param>
		/// <param name="originalCatalogingSource"></param>
		/// <param name="editionStatement"></param>
		/// <param name="currentPublicationFrequency"></param>
		/// <param name="note"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="productionTitleID"></param>
		/// <returns>Object of type Title.</returns>
		public Title TitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string mARCBibID,
			string mARCLeader,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string partNumber,
			string partName,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string institutionCode,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool? publishReady,
			bool? rareBooks,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string note,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate,
			int? productionTitleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("MARCBibID", SqlDbType.NVarChar, 50, false, mARCBibID),
					CustomSqlHelper.CreateInputParameter("MARCLeader", SqlDbType.NVarChar, 24, true, mARCLeader),
					CustomSqlHelper.CreateInputParameter("FullTitle", SqlDbType.NText, 1073741823, true, fullTitle),
					CustomSqlHelper.CreateInputParameter("ShortTitle", SqlDbType.NVarChar, 255, true, shortTitle),
					CustomSqlHelper.CreateInputParameter("UniformTitle", SqlDbType.NVarChar, 255, true, uniformTitle),
					CustomSqlHelper.CreateInputParameter("SortTitle", SqlDbType.NVarChar, 60, true, sortTitle),
					CustomSqlHelper.CreateInputParameter("PartNumber", SqlDbType.NVarChar, 255, true, partNumber),
					CustomSqlHelper.CreateInputParameter("PartName", SqlDbType.NVarChar, 255, true, partName),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 100, true, callNumber),
					CustomSqlHelper.CreateInputParameter("PublicationDetails", SqlDbType.NVarChar, 255, true, publicationDetails),
					CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.SmallInt, null, true, startYear),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.SmallInt, null, true, endYear),
					CustomSqlHelper.CreateInputParameter("Datafield_260_a", SqlDbType.NVarChar, 150, true, datafield_260_a),
					CustomSqlHelper.CreateInputParameter("Datafield_260_b", SqlDbType.NVarChar, 255, true, datafield_260_b),
					CustomSqlHelper.CreateInputParameter("Datafield_260_c", SqlDbType.NVarChar, 100, true, datafield_260_c),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("TitleDescription", SqlDbType.NText, 1073741823, true, titleDescription),
					CustomSqlHelper.CreateInputParameter("TL2Author", SqlDbType.NVarChar, 100, true, tL2Author),
					CustomSqlHelper.CreateInputParameter("PublishReady", SqlDbType.Bit, null, true, publishReady),
					CustomSqlHelper.CreateInputParameter("RareBooks", SqlDbType.Bit, null, true, rareBooks),
					CustomSqlHelper.CreateInputParameter("OriginalCatalogingSource", SqlDbType.NVarChar, 100, true, originalCatalogingSource),
					CustomSqlHelper.CreateInputParameter("EditionStatement", SqlDbType.NVarChar, 450, true, editionStatement),
					CustomSqlHelper.CreateInputParameter("CurrentPublicationFrequency", SqlDbType.NVarChar, 100, true, currentPublicationFrequency),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationUser", SqlDbType.Int, null, true, externalCreationUser),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedUser", SqlDbType.Int, null, true, externalLastModifiedUser),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate),
					CustomSqlHelper.CreateInputParameter("ProductionTitleID", SqlDbType.Int, null, true, productionTitleID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
				{
					CustomGenericList<Title> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title o = list[0];
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
		/// Update values in Title. Returns an object of type Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title.</param>
		/// <returns>Object of type Title.</returns>
		public Title TitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title value)
		{
			return TitleUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in Title. Returns an object of type Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title.</param>
		/// <returns>Object of type Title.</returns>
		public Title TitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title value)
		{
			return TitleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.MARCBibID,
				value.MARCLeader,
				value.FullTitle,
				value.ShortTitle,
				value.UniformTitle,
				value.SortTitle,
				value.PartNumber,
				value.PartName,
				value.CallNumber,
				value.PublicationDetails,
				value.StartYear,
				value.EndYear,
				value.Datafield_260_a,
				value.Datafield_260_b,
				value.Datafield_260_c,
				value.InstitutionCode,
				value.LanguageCode,
				value.TitleDescription,
				value.TL2Author,
				value.PublishReady,
				value.RareBooks,
				value.OriginalCatalogingSource,
				value.EditionStatement,
				value.CurrentPublicationFrequency,
				value.Note,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ExternalCreationUser,
				value.ExternalLastModifiedUser,
				value.ProductionDate,
				value.ProductionTitleID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Title object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title.</param>
		/// <returns>Object of type CustomDataAccessStatus<Title>.</returns>
		public CustomDataAccessStatus<Title> TitleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title value  )
		{
			return TitleManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage Title object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title.</param>
		/// <returns>Object of type CustomDataAccessStatus<Title>.</returns>
		public CustomDataAccessStatus<Title> TitleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Title returnValue = TitleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.MARCBibID,
						value.MARCLeader,
						value.FullTitle,
						value.ShortTitle,
						value.UniformTitle,
						value.SortTitle,
						value.PartNumber,
						value.PartName,
						value.CallNumber,
						value.PublicationDetails,
						value.StartYear,
						value.EndYear,
						value.Datafield_260_a,
						value.Datafield_260_b,
						value.Datafield_260_c,
						value.InstitutionCode,
						value.LanguageCode,
						value.TitleDescription,
						value.TL2Author,
						value.PublishReady,
						value.RareBooks,
						value.OriginalCatalogingSource,
						value.EditionStatement,
						value.CurrentPublicationFrequency,
						value.Note,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ExternalCreationUser,
						value.ExternalLastModifiedUser,
						value.ProductionDate,
						value.ProductionTitleID);
				
				return new CustomDataAccessStatus<Title>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID))
				{
				return new CustomDataAccessStatus<Title>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Title>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Title returnValue = TitleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.MARCBibID,
						value.MARCLeader,
						value.FullTitle,
						value.ShortTitle,
						value.UniformTitle,
						value.SortTitle,
						value.PartNumber,
						value.PartName,
						value.CallNumber,
						value.PublicationDetails,
						value.StartYear,
						value.EndYear,
						value.Datafield_260_a,
						value.Datafield_260_b,
						value.Datafield_260_c,
						value.InstitutionCode,
						value.LanguageCode,
						value.TitleDescription,
						value.TL2Author,
						value.PublishReady,
						value.RareBooks,
						value.OriginalCatalogingSource,
						value.EditionStatement,
						value.CurrentPublicationFrequency,
						value.Note,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ExternalCreationUser,
						value.ExternalLastModifiedUser,
						value.ProductionDate,
						value.ProductionTitleID);
					
				return new CustomDataAccessStatus<Title>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Title>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

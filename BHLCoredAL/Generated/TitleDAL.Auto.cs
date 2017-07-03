
// Generated 6/28/2017 2:38:16 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleDAL is based upon dbo.Title.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
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
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	partial class TitleDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Title by primary key(s).
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
			return TitleSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleID );
		}
			
		/// <summary>
		/// Select values from dbo.Title by primary key(s).
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
		/// Select values from dbo.Title by primary key(s).
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
			return TitleSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleID );
		}
		
		/// <summary>
		/// Select values from dbo.Title by primary key(s).
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
		/// Insert values into dbo.Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="mARCBibID"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="tropicosTitleID"></param>
		/// <param name="redirectTitleID"></param>
		/// <param name="fullTitle"></param>
		/// <param name="shortTitle"></param>
		/// <param name="uniformTitle"></param>
		/// <param name="sortTitle"></param>
		/// <param name="callNumber"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="datafield_260_a"></param>
		/// <param name="datafield_260_b"></param>
		/// <param name="datafield_260_c"></param>
		/// <param name="languageCode"></param>
		/// <param name="titleDescription"></param>
		/// <param name="tL2Author"></param>
		/// <param name="publishReady"></param>
		/// <param name="rareBooks"></param>
		/// <param name="note"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="originalCatalogingSource"></param>
		/// <param name="editionStatement"></param>
		/// <param name="currentPublicationFrequency"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="bibliographicLevelID"></param>
		/// <param name="materialTypeID"></param>
		/// <returns>Object of type Title.</returns>
		public Title TitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string mARCBibID,
			string mARCLeader,
			int? tropicosTitleID,
			int? redirectTitleID,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool publishReady,
			bool rareBooks,
			string note,
			int? creationUserID,
			int? lastModifiedUserID,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string partNumber,
			string partName,
			int? bibliographicLevelID,
			int? materialTypeID)
		{
			return TitleInsertAuto( sqlConnection, sqlTransaction, "BHL", mARCBibID, mARCLeader, tropicosTitleID, redirectTitleID, fullTitle, shortTitle, uniformTitle, sortTitle, callNumber, publicationDetails, startYear, endYear, datafield_260_a, datafield_260_b, datafield_260_c, languageCode, titleDescription, tL2Author, publishReady, rareBooks, note, creationUserID, lastModifiedUserID, originalCatalogingSource, editionStatement, currentPublicationFrequency, partNumber, partName, bibliographicLevelID, materialTypeID );
		}
		
		/// <summary>
		/// Insert values into dbo.Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="mARCBibID"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="tropicosTitleID"></param>
		/// <param name="redirectTitleID"></param>
		/// <param name="fullTitle"></param>
		/// <param name="shortTitle"></param>
		/// <param name="uniformTitle"></param>
		/// <param name="sortTitle"></param>
		/// <param name="callNumber"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="datafield_260_a"></param>
		/// <param name="datafield_260_b"></param>
		/// <param name="datafield_260_c"></param>
		/// <param name="languageCode"></param>
		/// <param name="titleDescription"></param>
		/// <param name="tL2Author"></param>
		/// <param name="publishReady"></param>
		/// <param name="rareBooks"></param>
		/// <param name="note"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="originalCatalogingSource"></param>
		/// <param name="editionStatement"></param>
		/// <param name="currentPublicationFrequency"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="bibliographicLevelID"></param>
		/// <param name="materialTypeID"></param>
		/// <returns>Object of type Title.</returns>
		public Title TitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string mARCBibID,
			string mARCLeader,
			int? tropicosTitleID,
			int? redirectTitleID,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool publishReady,
			bool rareBooks,
			string note,
			int? creationUserID,
			int? lastModifiedUserID,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string partNumber,
			string partName,
			int? bibliographicLevelID,
			int? materialTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("MARCBibID", SqlDbType.NVarChar, 50, false, mARCBibID),
					CustomSqlHelper.CreateInputParameter("MARCLeader", SqlDbType.NVarChar, 24, true, mARCLeader),
					CustomSqlHelper.CreateInputParameter("TropicosTitleID", SqlDbType.Int, null, true, tropicosTitleID),
					CustomSqlHelper.CreateInputParameter("RedirectTitleID", SqlDbType.Int, null, true, redirectTitleID),
					CustomSqlHelper.CreateInputParameter("FullTitle", SqlDbType.NVarChar, 2000, false, fullTitle),
					CustomSqlHelper.CreateInputParameter("ShortTitle", SqlDbType.NVarChar, 255, true, shortTitle),
					CustomSqlHelper.CreateInputParameter("UniformTitle", SqlDbType.NVarChar, 255, true, uniformTitle),
					CustomSqlHelper.CreateInputParameter("SortTitle", SqlDbType.NVarChar, 60, true, sortTitle),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 100, true, callNumber),
					CustomSqlHelper.CreateInputParameter("PublicationDetails", SqlDbType.NVarChar, 255, true, publicationDetails),
					CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.SmallInt, null, true, startYear),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.SmallInt, null, true, endYear),
					CustomSqlHelper.CreateInputParameter("Datafield_260_a", SqlDbType.NVarChar, 150, true, datafield_260_a),
					CustomSqlHelper.CreateInputParameter("Datafield_260_b", SqlDbType.NVarChar, 255, true, datafield_260_b),
					CustomSqlHelper.CreateInputParameter("Datafield_260_c", SqlDbType.NVarChar, 100, true, datafield_260_c),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("TitleDescription", SqlDbType.NText, 1073741823, true, titleDescription),
					CustomSqlHelper.CreateInputParameter("TL2Author", SqlDbType.NVarChar, 100, true, tL2Author),
					CustomSqlHelper.CreateInputParameter("PublishReady", SqlDbType.Bit, null, false, publishReady),
					CustomSqlHelper.CreateInputParameter("RareBooks", SqlDbType.Bit, null, false, rareBooks),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("OriginalCatalogingSource", SqlDbType.NVarChar, 100, true, originalCatalogingSource),
					CustomSqlHelper.CreateInputParameter("EditionStatement", SqlDbType.NVarChar, 450, true, editionStatement),
					CustomSqlHelper.CreateInputParameter("CurrentPublicationFrequency", SqlDbType.NVarChar, 100, true, currentPublicationFrequency),
					CustomSqlHelper.CreateInputParameter("PartNumber", SqlDbType.NVarChar, 255, true, partNumber),
					CustomSqlHelper.CreateInputParameter("PartName", SqlDbType.NVarChar, 255, true, partName),
					CustomSqlHelper.CreateInputParameter("BibliographicLevelID", SqlDbType.Int, null, true, bibliographicLevelID),
					CustomSqlHelper.CreateInputParameter("MaterialTypeID", SqlDbType.Int, null, true, materialTypeID), 
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
		/// Insert values into dbo.Title. Returns an object of type Title.
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
			return TitleInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Title. Returns an object of type Title.
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
				value.MARCBibID,
				value.MARCLeader,
				value.TropicosTitleID,
				value.RedirectTitleID,
				value.FullTitle,
				value.ShortTitle,
				value.UniformTitle,
				value.SortTitle,
				value.CallNumber,
				value.PublicationDetails,
				value.StartYear,
				value.EndYear,
				value.Datafield_260_a,
				value.Datafield_260_b,
				value.Datafield_260_c,
				value.LanguageCode,
				value.TitleDescription,
				value.TL2Author,
				value.PublishReady,
				value.RareBooks,
				value.Note,
				value.CreationUserID,
				value.LastModifiedUserID,
				value.OriginalCatalogingSource,
				value.EditionStatement,
				value.CurrentPublicationFrequency,
				value.PartNumber,
				value.PartName,
				value.BibliographicLevelID,
				value.MaterialTypeID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Title by primary key(s).
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
			return TitleDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleID );
		}
		
		/// <summary>
		/// Delete values from dbo.Title by primary key(s).
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
		/// Update values in dbo.Title. Returns an object of type Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="tropicosTitleID"></param>
		/// <param name="redirectTitleID"></param>
		/// <param name="fullTitle"></param>
		/// <param name="shortTitle"></param>
		/// <param name="uniformTitle"></param>
		/// <param name="sortTitle"></param>
		/// <param name="callNumber"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="datafield_260_a"></param>
		/// <param name="datafield_260_b"></param>
		/// <param name="datafield_260_c"></param>
		/// <param name="languageCode"></param>
		/// <param name="titleDescription"></param>
		/// <param name="tL2Author"></param>
		/// <param name="publishReady"></param>
		/// <param name="rareBooks"></param>
		/// <param name="note"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="originalCatalogingSource"></param>
		/// <param name="editionStatement"></param>
		/// <param name="currentPublicationFrequency"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="bibliographicLevelID"></param>
		/// <param name="materialTypeID"></param>
		/// <returns>Object of type Title.</returns>
		public Title TitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			string mARCBibID,
			string mARCLeader,
			int? tropicosTitleID,
			int? redirectTitleID,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool publishReady,
			bool rareBooks,
			string note,
			int? lastModifiedUserID,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string partNumber,
			string partName,
			int? bibliographicLevelID,
			int? materialTypeID)
		{
			return TitleUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleID, mARCBibID, mARCLeader, tropicosTitleID, redirectTitleID, fullTitle, shortTitle, uniformTitle, sortTitle, callNumber, publicationDetails, startYear, endYear, datafield_260_a, datafield_260_b, datafield_260_c, languageCode, titleDescription, tL2Author, publishReady, rareBooks, note, lastModifiedUserID, originalCatalogingSource, editionStatement, currentPublicationFrequency, partNumber, partName, bibliographicLevelID, materialTypeID);
		}
		
		/// <summary>
		/// Update values in dbo.Title. Returns an object of type Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="mARCLeader"></param>
		/// <param name="tropicosTitleID"></param>
		/// <param name="redirectTitleID"></param>
		/// <param name="fullTitle"></param>
		/// <param name="shortTitle"></param>
		/// <param name="uniformTitle"></param>
		/// <param name="sortTitle"></param>
		/// <param name="callNumber"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="datafield_260_a"></param>
		/// <param name="datafield_260_b"></param>
		/// <param name="datafield_260_c"></param>
		/// <param name="languageCode"></param>
		/// <param name="titleDescription"></param>
		/// <param name="tL2Author"></param>
		/// <param name="publishReady"></param>
		/// <param name="rareBooks"></param>
		/// <param name="note"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="originalCatalogingSource"></param>
		/// <param name="editionStatement"></param>
		/// <param name="currentPublicationFrequency"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="bibliographicLevelID"></param>
		/// <param name="materialTypeID"></param>
		/// <returns>Object of type Title.</returns>
		public Title TitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			string mARCBibID,
			string mARCLeader,
			int? tropicosTitleID,
			int? redirectTitleID,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool publishReady,
			bool rareBooks,
			string note,
			int? lastModifiedUserID,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string partNumber,
			string partName,
			int? bibliographicLevelID,
			int? materialTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("MARCBibID", SqlDbType.NVarChar, 50, false, mARCBibID),
					CustomSqlHelper.CreateInputParameter("MARCLeader", SqlDbType.NVarChar, 24, true, mARCLeader),
					CustomSqlHelper.CreateInputParameter("TropicosTitleID", SqlDbType.Int, null, true, tropicosTitleID),
					CustomSqlHelper.CreateInputParameter("RedirectTitleID", SqlDbType.Int, null, true, redirectTitleID),
					CustomSqlHelper.CreateInputParameter("FullTitle", SqlDbType.NVarChar, 2000, false, fullTitle),
					CustomSqlHelper.CreateInputParameter("ShortTitle", SqlDbType.NVarChar, 255, true, shortTitle),
					CustomSqlHelper.CreateInputParameter("UniformTitle", SqlDbType.NVarChar, 255, true, uniformTitle),
					CustomSqlHelper.CreateInputParameter("SortTitle", SqlDbType.NVarChar, 60, true, sortTitle),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 100, true, callNumber),
					CustomSqlHelper.CreateInputParameter("PublicationDetails", SqlDbType.NVarChar, 255, true, publicationDetails),
					CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.SmallInt, null, true, startYear),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.SmallInt, null, true, endYear),
					CustomSqlHelper.CreateInputParameter("Datafield_260_a", SqlDbType.NVarChar, 150, true, datafield_260_a),
					CustomSqlHelper.CreateInputParameter("Datafield_260_b", SqlDbType.NVarChar, 255, true, datafield_260_b),
					CustomSqlHelper.CreateInputParameter("Datafield_260_c", SqlDbType.NVarChar, 100, true, datafield_260_c),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("TitleDescription", SqlDbType.NText, 1073741823, true, titleDescription),
					CustomSqlHelper.CreateInputParameter("TL2Author", SqlDbType.NVarChar, 100, true, tL2Author),
					CustomSqlHelper.CreateInputParameter("PublishReady", SqlDbType.Bit, null, false, publishReady),
					CustomSqlHelper.CreateInputParameter("RareBooks", SqlDbType.Bit, null, false, rareBooks),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("OriginalCatalogingSource", SqlDbType.NVarChar, 100, true, originalCatalogingSource),
					CustomSqlHelper.CreateInputParameter("EditionStatement", SqlDbType.NVarChar, 450, true, editionStatement),
					CustomSqlHelper.CreateInputParameter("CurrentPublicationFrequency", SqlDbType.NVarChar, 100, true, currentPublicationFrequency),
					CustomSqlHelper.CreateInputParameter("PartNumber", SqlDbType.NVarChar, 255, true, partNumber),
					CustomSqlHelper.CreateInputParameter("PartName", SqlDbType.NVarChar, 255, true, partName),
					CustomSqlHelper.CreateInputParameter("BibliographicLevelID", SqlDbType.Int, null, true, bibliographicLevelID),
					CustomSqlHelper.CreateInputParameter("MaterialTypeID", SqlDbType.Int, null, true, materialTypeID), 
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
		/// Update values in dbo.Title. Returns an object of type Title.
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
			return TitleUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Title. Returns an object of type Title.
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
				value.MARCBibID,
				value.MARCLeader,
				value.TropicosTitleID,
				value.RedirectTitleID,
				value.FullTitle,
				value.ShortTitle,
				value.UniformTitle,
				value.SortTitle,
				value.CallNumber,
				value.PublicationDetails,
				value.StartYear,
				value.EndYear,
				value.Datafield_260_a,
				value.Datafield_260_b,
				value.Datafield_260_c,
				value.LanguageCode,
				value.TitleDescription,
				value.TL2Author,
				value.PublishReady,
				value.RareBooks,
				value.Note,
				value.LastModifiedUserID,
				value.OriginalCatalogingSource,
				value.EditionStatement,
				value.CurrentPublicationFrequency,
				value.PartNumber,
				value.PartName,
				value.BibliographicLevelID,
				value.MaterialTypeID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Title object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Title.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title.</param>
		/// <returns>Object of type CustomDataAccessStatus<Title>.</returns>
		public CustomDataAccessStatus<Title> TitleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title value , int userId )
		{
			return TitleManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.Title object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Title.
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
			Title value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Title returnValue = TitleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MARCBibID,
						value.MARCLeader,
						value.TropicosTitleID,
						value.RedirectTitleID,
						value.FullTitle,
						value.ShortTitle,
						value.UniformTitle,
						value.SortTitle,
						value.CallNumber,
						value.PublicationDetails,
						value.StartYear,
						value.EndYear,
						value.Datafield_260_a,
						value.Datafield_260_b,
						value.Datafield_260_c,
						value.LanguageCode,
						value.TitleDescription,
						value.TL2Author,
						value.PublishReady,
						value.RareBooks,
						value.Note,
						value.CreationUserID,
						value.LastModifiedUserID,
						value.OriginalCatalogingSource,
						value.EditionStatement,
						value.CurrentPublicationFrequency,
						value.PartNumber,
						value.PartName,
						value.BibliographicLevelID,
						value.MaterialTypeID);
				
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
				value.LastModifiedUserID = userId;
				Title returnValue = TitleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.MARCBibID,
						value.MARCLeader,
						value.TropicosTitleID,
						value.RedirectTitleID,
						value.FullTitle,
						value.ShortTitle,
						value.UniformTitle,
						value.SortTitle,
						value.CallNumber,
						value.PublicationDetails,
						value.StartYear,
						value.EndYear,
						value.Datafield_260_a,
						value.Datafield_260_b,
						value.Datafield_260_c,
						value.LanguageCode,
						value.TitleDescription,
						value.TL2Author,
						value.PublishReady,
						value.RareBooks,
						value.Note,
						value.LastModifiedUserID,
						value.OriginalCatalogingSource,
						value.EditionStatement,
						value.CurrentPublicationFrequency,
						value.PartNumber,
						value.PartName,
						value.BibliographicLevelID,
						value.MaterialTypeID);
					
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


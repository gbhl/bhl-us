
// Generated 11/21/2016 1:39:33 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class BSSegmentDAL is based upon dbo.BSSegment.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class BSSegmentDAL
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
	partial class BSSegmentDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.BSSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <returns>Object of type BSSegment.</returns>
		public BSSegment BSSegmentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID)
		{
			return BSSegmentSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	segmentID );
		}
			
		/// <summary>
		/// Select values from dbo.BSSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <returns>Object of type BSSegment.</returns>
		public BSSegment BSSegmentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
			{
				using (CustomSqlHelper<BSSegment> helper = new CustomSqlHelper<BSSegment>())
				{
					CustomGenericList<BSSegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSSegment o = list[0];
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
		/// Select values from dbo.BSSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> BSSegmentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID)
		{
			return BSSegmentSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", segmentID );
		}
		
		/// <summary>
		/// Select values from dbo.BSSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> BSSegmentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.BSSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="bioStorReferenceID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="genre"></param>
		/// <param name="title"></param>
		/// <param name="containerTitle"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="year"></param>
		/// <param name="date"></param>
		/// <param name="iSSN"></param>
		/// <param name="dOI"></param>
		/// <param name="oCLC"></param>
		/// <param name="jSTOR"></param>
		/// <param name="startPageNumber"></param>
		/// <param name="endPageNumber"></param>
		/// <param name="startPageID"></param>
		/// <param name="contributorCreationDate"></param>
		/// <param name="contributorLastModifiedDate"></param>
		/// <param name="bHLSegmentID"></param>
		/// <returns>Object of type BSSegment.</returns>
		public BSSegment BSSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string bioStorReferenceID,
			short sequenceOrder,
			string genre,
			string title,
			string containerTitle,
			string publisherName,
			string publisherPlace,
			string volume,
			string series,
			string issue,
			string year,
			string date,
			string iSSN,
			string dOI,
			string oCLC,
			string jSTOR,
			string startPageNumber,
			string endPageNumber,
			int? startPageID,
			DateTime? contributorCreationDate,
			DateTime? contributorLastModifiedDate,
			int? bHLSegmentID)
		{
			return BSSegmentInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, bioStorReferenceID, sequenceOrder, genre, title, containerTitle, publisherName, publisherPlace, volume, series, issue, year, date, iSSN, dOI, oCLC, jSTOR, startPageNumber, endPageNumber, startPageID, contributorCreationDate, contributorLastModifiedDate, bHLSegmentID );
		}
		
		/// <summary>
		/// Insert values into dbo.BSSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="bioStorReferenceID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="genre"></param>
		/// <param name="title"></param>
		/// <param name="containerTitle"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="year"></param>
		/// <param name="date"></param>
		/// <param name="iSSN"></param>
		/// <param name="dOI"></param>
		/// <param name="oCLC"></param>
		/// <param name="jSTOR"></param>
		/// <param name="startPageNumber"></param>
		/// <param name="endPageNumber"></param>
		/// <param name="startPageID"></param>
		/// <param name="contributorCreationDate"></param>
		/// <param name="contributorLastModifiedDate"></param>
		/// <param name="bHLSegmentID"></param>
		/// <returns>Object of type BSSegment.</returns>
		public BSSegment BSSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string bioStorReferenceID,
			short sequenceOrder,
			string genre,
			string title,
			string containerTitle,
			string publisherName,
			string publisherPlace,
			string volume,
			string series,
			string issue,
			string year,
			string date,
			string iSSN,
			string dOI,
			string oCLC,
			string jSTOR,
			string startPageNumber,
			string endPageNumber,
			int? startPageID,
			DateTime? contributorCreationDate,
			DateTime? contributorLastModifiedDate,
			int? bHLSegmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("BioStorReferenceID", SqlDbType.NVarChar, 100, false, bioStorReferenceID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("Genre", SqlDbType.NVarChar, 50, false, genre),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
					CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
					CustomSqlHelper.CreateInputParameter("PublisherName", SqlDbType.NVarChar, 250, false, publisherName),
					CustomSqlHelper.CreateInputParameter("PublisherPlace", SqlDbType.NVarChar, 150, false, publisherPlace),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, false, year),
					CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
					CustomSqlHelper.CreateInputParameter("ISSN", SqlDbType.NVarChar, 125, false, iSSN),
					CustomSqlHelper.CreateInputParameter("DOI", SqlDbType.NVarChar, 50, false, dOI),
					CustomSqlHelper.CreateInputParameter("OCLC", SqlDbType.NVarChar, 125, false, oCLC),
					CustomSqlHelper.CreateInputParameter("JSTOR", SqlDbType.NVarChar, 125, false, jSTOR),
					CustomSqlHelper.CreateInputParameter("StartPageNumber", SqlDbType.NVarChar, 20, false, startPageNumber),
					CustomSqlHelper.CreateInputParameter("EndPageNumber", SqlDbType.NVarChar, 20, false, endPageNumber),
					CustomSqlHelper.CreateInputParameter("StartPageID", SqlDbType.Int, null, true, startPageID),
					CustomSqlHelper.CreateInputParameter("ContributorCreationDate", SqlDbType.DateTime, null, true, contributorCreationDate),
					CustomSqlHelper.CreateInputParameter("ContributorLastModifiedDate", SqlDbType.DateTime, null, true, contributorLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("BHLSegmentID", SqlDbType.Int, null, true, bHLSegmentID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BSSegment> helper = new CustomSqlHelper<BSSegment>())
				{
					CustomGenericList<BSSegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSSegment o = list[0];
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
		/// Insert values into dbo.BSSegment. Returns an object of type BSSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSSegment.</param>
		/// <returns>Object of type BSSegment.</returns>
		public BSSegment BSSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSSegment value)
		{
			return BSSegmentInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.BSSegment. Returns an object of type BSSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSSegment.</param>
		/// <returns>Object of type BSSegment.</returns>
		public BSSegment BSSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSSegment value)
		{
			return BSSegmentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.BioStorReferenceID,
				value.SequenceOrder,
				value.Genre,
				value.Title,
				value.ContainerTitle,
				value.PublisherName,
				value.PublisherPlace,
				value.Volume,
				value.Series,
				value.Issue,
				value.Year,
				value.Date,
				value.ISSN,
				value.DOI,
				value.OCLC,
				value.JSTOR,
				value.StartPageNumber,
				value.EndPageNumber,
				value.StartPageID,
				value.ContributorCreationDate,
				value.ContributorLastModifiedDate,
				value.BHLSegmentID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.BSSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BSSegmentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID)
		{
			return BSSegmentDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", segmentID );
		}
		
		/// <summary>
		/// Delete values from dbo.BSSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BSSegmentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID), 
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
		/// Update values in dbo.BSSegment. Returns an object of type BSSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="itemID"></param>
		/// <param name="bioStorReferenceID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="genre"></param>
		/// <param name="title"></param>
		/// <param name="containerTitle"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="year"></param>
		/// <param name="date"></param>
		/// <param name="iSSN"></param>
		/// <param name="dOI"></param>
		/// <param name="oCLC"></param>
		/// <param name="jSTOR"></param>
		/// <param name="startPageNumber"></param>
		/// <param name="endPageNumber"></param>
		/// <param name="startPageID"></param>
		/// <param name="contributorCreationDate"></param>
		/// <param name="contributorLastModifiedDate"></param>
		/// <param name="bHLSegmentID"></param>
		/// <returns>Object of type BSSegment.</returns>
		public BSSegment BSSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int itemID,
			string bioStorReferenceID,
			short sequenceOrder,
			string genre,
			string title,
			string containerTitle,
			string publisherName,
			string publisherPlace,
			string volume,
			string series,
			string issue,
			string year,
			string date,
			string iSSN,
			string dOI,
			string oCLC,
			string jSTOR,
			string startPageNumber,
			string endPageNumber,
			int? startPageID,
			DateTime? contributorCreationDate,
			DateTime? contributorLastModifiedDate,
			int? bHLSegmentID)
		{
			return BSSegmentUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", segmentID, itemID, bioStorReferenceID, sequenceOrder, genre, title, containerTitle, publisherName, publisherPlace, volume, series, issue, year, date, iSSN, dOI, oCLC, jSTOR, startPageNumber, endPageNumber, startPageID, contributorCreationDate, contributorLastModifiedDate, bHLSegmentID);
		}
		
		/// <summary>
		/// Update values in dbo.BSSegment. Returns an object of type BSSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="itemID"></param>
		/// <param name="bioStorReferenceID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="genre"></param>
		/// <param name="title"></param>
		/// <param name="containerTitle"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="year"></param>
		/// <param name="date"></param>
		/// <param name="iSSN"></param>
		/// <param name="dOI"></param>
		/// <param name="oCLC"></param>
		/// <param name="jSTOR"></param>
		/// <param name="startPageNumber"></param>
		/// <param name="endPageNumber"></param>
		/// <param name="startPageID"></param>
		/// <param name="contributorCreationDate"></param>
		/// <param name="contributorLastModifiedDate"></param>
		/// <param name="bHLSegmentID"></param>
		/// <returns>Object of type BSSegment.</returns>
		public BSSegment BSSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int itemID,
			string bioStorReferenceID,
			short sequenceOrder,
			string genre,
			string title,
			string containerTitle,
			string publisherName,
			string publisherPlace,
			string volume,
			string series,
			string issue,
			string year,
			string date,
			string iSSN,
			string dOI,
			string oCLC,
			string jSTOR,
			string startPageNumber,
			string endPageNumber,
			int? startPageID,
			DateTime? contributorCreationDate,
			DateTime? contributorLastModifiedDate,
			int? bHLSegmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("BioStorReferenceID", SqlDbType.NVarChar, 100, false, bioStorReferenceID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("Genre", SqlDbType.NVarChar, 50, false, genre),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
					CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
					CustomSqlHelper.CreateInputParameter("PublisherName", SqlDbType.NVarChar, 250, false, publisherName),
					CustomSqlHelper.CreateInputParameter("PublisherPlace", SqlDbType.NVarChar, 150, false, publisherPlace),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, false, year),
					CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
					CustomSqlHelper.CreateInputParameter("ISSN", SqlDbType.NVarChar, 125, false, iSSN),
					CustomSqlHelper.CreateInputParameter("DOI", SqlDbType.NVarChar, 50, false, dOI),
					CustomSqlHelper.CreateInputParameter("OCLC", SqlDbType.NVarChar, 125, false, oCLC),
					CustomSqlHelper.CreateInputParameter("JSTOR", SqlDbType.NVarChar, 125, false, jSTOR),
					CustomSqlHelper.CreateInputParameter("StartPageNumber", SqlDbType.NVarChar, 20, false, startPageNumber),
					CustomSqlHelper.CreateInputParameter("EndPageNumber", SqlDbType.NVarChar, 20, false, endPageNumber),
					CustomSqlHelper.CreateInputParameter("StartPageID", SqlDbType.Int, null, true, startPageID),
					CustomSqlHelper.CreateInputParameter("ContributorCreationDate", SqlDbType.DateTime, null, true, contributorCreationDate),
					CustomSqlHelper.CreateInputParameter("ContributorLastModifiedDate", SqlDbType.DateTime, null, true, contributorLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("BHLSegmentID", SqlDbType.Int, null, true, bHLSegmentID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BSSegment> helper = new CustomSqlHelper<BSSegment>())
				{
					CustomGenericList<BSSegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSSegment o = list[0];
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
		/// Update values in dbo.BSSegment. Returns an object of type BSSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSSegment.</param>
		/// <returns>Object of type BSSegment.</returns>
		public BSSegment BSSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSSegment value)
		{
			return BSSegmentUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.BSSegment. Returns an object of type BSSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSSegment.</param>
		/// <returns>Object of type BSSegment.</returns>
		public BSSegment BSSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSSegment value)
		{
			return BSSegmentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.ItemID,
				value.BioStorReferenceID,
				value.SequenceOrder,
				value.Genre,
				value.Title,
				value.ContainerTitle,
				value.PublisherName,
				value.PublisherPlace,
				value.Volume,
				value.Series,
				value.Issue,
				value.Year,
				value.Date,
				value.ISSN,
				value.DOI,
				value.OCLC,
				value.JSTOR,
				value.StartPageNumber,
				value.EndPageNumber,
				value.StartPageID,
				value.ContributorCreationDate,
				value.ContributorLastModifiedDate,
				value.BHLSegmentID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.BSSegment object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.BSSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSSegment.</param>
		/// <returns>Object of type CustomDataAccessStatus<BSSegment>.</returns>
		public CustomDataAccessStatus<BSSegment> BSSegmentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSSegment value  )
		{
			return BSSegmentManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.BSSegment object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.BSSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSSegment.</param>
		/// <returns>Object of type CustomDataAccessStatus<BSSegment>.</returns>
		public CustomDataAccessStatus<BSSegment> BSSegmentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSSegment value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				BSSegment returnValue = BSSegmentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.BioStorReferenceID,
						value.SequenceOrder,
						value.Genre,
						value.Title,
						value.ContainerTitle,
						value.PublisherName,
						value.PublisherPlace,
						value.Volume,
						value.Series,
						value.Issue,
						value.Year,
						value.Date,
						value.ISSN,
						value.DOI,
						value.OCLC,
						value.JSTOR,
						value.StartPageNumber,
						value.EndPageNumber,
						value.StartPageID,
						value.ContributorCreationDate,
						value.ContributorLastModifiedDate,
						value.BHLSegmentID);
				
				return new CustomDataAccessStatus<BSSegment>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (BSSegmentDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID))
				{
				return new CustomDataAccessStatus<BSSegment>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<BSSegment>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				BSSegment returnValue = BSSegmentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.ItemID,
						value.BioStorReferenceID,
						value.SequenceOrder,
						value.Genre,
						value.Title,
						value.ContainerTitle,
						value.PublisherName,
						value.PublisherPlace,
						value.Volume,
						value.Series,
						value.Issue,
						value.Year,
						value.Date,
						value.ISSN,
						value.DOI,
						value.OCLC,
						value.JSTOR,
						value.StartPageNumber,
						value.EndPageNumber,
						value.StartPageID,
						value.ContributorCreationDate,
						value.ContributorLastModifiedDate,
						value.BHLSegmentID);
					
				return new CustomDataAccessStatus<BSSegment>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<BSSegment>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


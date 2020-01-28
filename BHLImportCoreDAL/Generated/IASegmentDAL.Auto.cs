
// Generated 1/24/2020 4:10:26 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IASegmentDAL is based upon dbo.IASegment.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IASegmentDAL
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
	partial class IASegmentDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IASegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <returns>Object of type IASegment.</returns>
		public IASegment IASegmentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID)
		{
			return IASegmentSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	segmentID );
		}
			
		/// <summary>
		/// Select values from dbo.IASegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <returns>Object of type IASegment.</returns>
		public IASegment IASegmentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
			{
				using (CustomSqlHelper<IASegment> helper = new CustomSqlHelper<IASegment>())
				{
					CustomGenericList<IASegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASegment o = list[0];
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
		/// Select values from dbo.IASegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IASegmentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID)
		{
			return IASegmentSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", segmentID );
		}
		
		/// <summary>
		/// Select values from dbo.IASegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IASegmentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IASegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="sequence"></param>
		/// <param name="title"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="series"></param>
		/// <param name="date"></param>
		/// <param name="languageCode"></param>
		/// <param name="bHLSegmentGenreID"></param>
		/// <param name="bHLSegmentGenreName"></param>
		/// <param name="dOI"></param>
		/// <returns>Object of type IASegment.</returns>
		public IASegment IASegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int sequence,
			string title,
			string volume,
			string issue,
			string series,
			string date,
			string languageCode,
			int bHLSegmentGenreID,
			string bHLSegmentGenreName,
			string dOI)
		{
			return IASegmentInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, sequence, title, volume, issue, series, date, languageCode, bHLSegmentGenreID, bHLSegmentGenreName, dOI );
		}
		
		/// <summary>
		/// Insert values into dbo.IASegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="sequence"></param>
		/// <param name="title"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="series"></param>
		/// <param name="date"></param>
		/// <param name="languageCode"></param>
		/// <param name="bHLSegmentGenreID"></param>
		/// <param name="bHLSegmentGenreName"></param>
		/// <param name="dOI"></param>
		/// <returns>Object of type IASegment.</returns>
		public IASegment IASegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int sequence,
			string title,
			string volume,
			string issue,
			string series,
			string date,
			string languageCode,
			int bHLSegmentGenreID,
			string bHLSegmentGenreName,
			string dOI)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, false, sequence),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
					CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
					CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
					CustomSqlHelper.CreateInputParameter("BHLSegmentGenreID", SqlDbType.Int, null, false, bHLSegmentGenreID),
					CustomSqlHelper.CreateInputParameter("BHLSegmentGenreName", SqlDbType.NVarChar, 50, false, bHLSegmentGenreName),
					CustomSqlHelper.CreateInputParameter("DOI", SqlDbType.NVarChar, 50, false, dOI), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IASegment> helper = new CustomSqlHelper<IASegment>())
				{
					CustomGenericList<IASegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASegment o = list[0];
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
		/// Insert values into dbo.IASegment. Returns an object of type IASegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASegment.</param>
		/// <returns>Object of type IASegment.</returns>
		public IASegment IASegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASegment value)
		{
			return IASegmentInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IASegment. Returns an object of type IASegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASegment.</param>
		/// <returns>Object of type IASegment.</returns>
		public IASegment IASegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASegment value)
		{
			return IASegmentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.Sequence,
				value.Title,
				value.Volume,
				value.Issue,
				value.Series,
				value.Date,
				value.LanguageCode,
				value.BHLSegmentGenreID,
				value.BHLSegmentGenreName,
				value.DOI);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IASegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IASegmentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID)
		{
			return IASegmentDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", segmentID );
		}
		
		/// <summary>
		/// Delete values from dbo.IASegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IASegmentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentDeleteAuto", connection, transaction, 
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
		/// Update values in dbo.IASegment. Returns an object of type IASegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="itemID"></param>
		/// <param name="sequence"></param>
		/// <param name="title"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="series"></param>
		/// <param name="date"></param>
		/// <param name="languageCode"></param>
		/// <param name="bHLSegmentGenreID"></param>
		/// <param name="bHLSegmentGenreName"></param>
		/// <param name="dOI"></param>
		/// <returns>Object of type IASegment.</returns>
		public IASegment IASegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int itemID,
			int sequence,
			string title,
			string volume,
			string issue,
			string series,
			string date,
			string languageCode,
			int bHLSegmentGenreID,
			string bHLSegmentGenreName,
			string dOI)
		{
			return IASegmentUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", segmentID, itemID, sequence, title, volume, issue, series, date, languageCode, bHLSegmentGenreID, bHLSegmentGenreName, dOI);
		}
		
		/// <summary>
		/// Update values in dbo.IASegment. Returns an object of type IASegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="itemID"></param>
		/// <param name="sequence"></param>
		/// <param name="title"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="series"></param>
		/// <param name="date"></param>
		/// <param name="languageCode"></param>
		/// <param name="bHLSegmentGenreID"></param>
		/// <param name="bHLSegmentGenreName"></param>
		/// <param name="dOI"></param>
		/// <returns>Object of type IASegment.</returns>
		public IASegment IASegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int itemID,
			int sequence,
			string title,
			string volume,
			string issue,
			string series,
			string date,
			string languageCode,
			int bHLSegmentGenreID,
			string bHLSegmentGenreName,
			string dOI)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, false, sequence),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
					CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
					CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
					CustomSqlHelper.CreateInputParameter("BHLSegmentGenreID", SqlDbType.Int, null, false, bHLSegmentGenreID),
					CustomSqlHelper.CreateInputParameter("BHLSegmentGenreName", SqlDbType.NVarChar, 50, false, bHLSegmentGenreName),
					CustomSqlHelper.CreateInputParameter("DOI", SqlDbType.NVarChar, 50, false, dOI), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IASegment> helper = new CustomSqlHelper<IASegment>())
				{
					CustomGenericList<IASegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASegment o = list[0];
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
		/// Update values in dbo.IASegment. Returns an object of type IASegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASegment.</param>
		/// <returns>Object of type IASegment.</returns>
		public IASegment IASegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASegment value)
		{
			return IASegmentUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IASegment. Returns an object of type IASegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASegment.</param>
		/// <returns>Object of type IASegment.</returns>
		public IASegment IASegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASegment value)
		{
			return IASegmentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.ItemID,
				value.Sequence,
				value.Title,
				value.Volume,
				value.Issue,
				value.Series,
				value.Date,
				value.LanguageCode,
				value.BHLSegmentGenreID,
				value.BHLSegmentGenreName,
				value.DOI);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IASegment object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IASegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASegment.</param>
		/// <returns>Object of type CustomDataAccessStatus<IASegment>.</returns>
		public CustomDataAccessStatus<IASegment> IASegmentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASegment value  )
		{
			return IASegmentManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IASegment object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IASegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASegment.</param>
		/// <returns>Object of type CustomDataAccessStatus<IASegment>.</returns>
		public CustomDataAccessStatus<IASegment> IASegmentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASegment value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IASegment returnValue = IASegmentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.Sequence,
						value.Title,
						value.Volume,
						value.Issue,
						value.Series,
						value.Date,
						value.LanguageCode,
						value.BHLSegmentGenreID,
						value.BHLSegmentGenreName,
						value.DOI);
				
				return new CustomDataAccessStatus<IASegment>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IASegmentDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID))
				{
				return new CustomDataAccessStatus<IASegment>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IASegment>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IASegment returnValue = IASegmentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.ItemID,
						value.Sequence,
						value.Title,
						value.Volume,
						value.Issue,
						value.Series,
						value.Date,
						value.LanguageCode,
						value.BHLSegmentGenreID,
						value.BHLSegmentGenreName,
						value.DOI);
					
				return new CustomDataAccessStatus<IASegment>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IASegment>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


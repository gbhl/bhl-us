
// Generated 11/24/2010 3:52:48 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAScandataDAL is based upon IAScandata.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAScandataDAL
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
	partial class IAScandataDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from IAScandata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataID"></param>
		/// <returns>Object of type IAScandata.</returns>
		public IAScandata IAScandataSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataID)
		{
			return IAScandataSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	scandataID );
		}
			
		/// <summary>
		/// Select values from IAScandata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataID"></param>
		/// <returns>Object of type IAScandata.</returns>
		public IAScandata IAScandataSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID)))
			{
				using (CustomSqlHelper<IAScandata> helper = new CustomSqlHelper<IAScandata>())
				{
					CustomGenericList<IAScandata> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAScandata o = list[0];
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
		/// Select values from IAScandata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAScandataSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataID)
		{
			return IAScandataSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", scandataID );
		}
		
		/// <summary>
		/// Select values from IAScandata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAScandataSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into IAScandata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="sequence"></param>
		/// <param name="pageType"></param>
		/// <param name="pageNumber"></param>
		/// <param name="year"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="issuePrefix"></param>
		/// <returns>Object of type IAScandata.</returns>
		public IAScandata IAScandataInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int sequence,
			string pageType,
			string pageNumber,
			string year,
			string volume,
			string issue,
			string issuePrefix)
		{
			return IAScandataInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, sequence, pageType, pageNumber, year, volume, issue, issuePrefix );
		}
		
		/// <summary>
		/// Insert values into IAScandata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="sequence"></param>
		/// <param name="pageType"></param>
		/// <param name="pageNumber"></param>
		/// <param name="year"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="issuePrefix"></param>
		/// <returns>Object of type IAScandata.</returns>
		public IAScandata IAScandataInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int sequence,
			string pageType,
			string pageNumber,
			string year,
			string volume,
			string issue,
			string issuePrefix)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ScandataID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, false, sequence),
					CustomSqlHelper.CreateInputParameter("PageType", SqlDbType.NVarChar, 50, false, pageType),
					CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, false, pageNumber),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, true, year),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 20, true, volume),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 20, true, issue),
					CustomSqlHelper.CreateInputParameter("IssuePrefix", SqlDbType.NVarChar, 20, true, issuePrefix), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAScandata> helper = new CustomSqlHelper<IAScandata>())
				{
					CustomGenericList<IAScandata> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAScandata o = list[0];
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
		/// Insert values into IAScandata. Returns an object of type IAScandata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAScandata.</param>
		/// <returns>Object of type IAScandata.</returns>
		public IAScandata IAScandataInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAScandata value)
		{
			return IAScandataInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into IAScandata. Returns an object of type IAScandata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAScandata.</param>
		/// <returns>Object of type IAScandata.</returns>
		public IAScandata IAScandataInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAScandata value)
		{
			return IAScandataInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.Sequence,
				value.PageType,
				value.PageNumber,
				value.Year,
				value.Volume,
				value.Issue,
				value.IssuePrefix);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from IAScandata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAScandataDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataID)
		{
			return IAScandataDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", scandataID );
		}
		
		/// <summary>
		/// Delete values from IAScandata by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAScandataDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID), 
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
		/// Update values in IAScandata. Returns an object of type IAScandata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataID"></param>
		/// <param name="itemID"></param>
		/// <param name="sequence"></param>
		/// <param name="pageType"></param>
		/// <param name="pageNumber"></param>
		/// <param name="year"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="issuePrefix"></param>
		/// <returns>Object of type IAScandata.</returns>
		public IAScandata IAScandataUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataID,
			int itemID,
			int sequence,
			string pageType,
			string pageNumber,
			string year,
			string volume,
			string issue,
			string issuePrefix)
		{
			return IAScandataUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", scandataID, itemID, sequence, pageType, pageNumber, year, volume, issue, issuePrefix);
		}
		
		/// <summary>
		/// Update values in IAScandata. Returns an object of type IAScandata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataID"></param>
		/// <param name="itemID"></param>
		/// <param name="sequence"></param>
		/// <param name="pageType"></param>
		/// <param name="pageNumber"></param>
		/// <param name="year"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="issuePrefix"></param>
		/// <returns>Object of type IAScandata.</returns>
		public IAScandata IAScandataUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataID,
			int itemID,
			int sequence,
			string pageType,
			string pageNumber,
			string year,
			string volume,
			string issue,
			string issuePrefix)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, false, sequence),
					CustomSqlHelper.CreateInputParameter("PageType", SqlDbType.NVarChar, 50, false, pageType),
					CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, false, pageNumber),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, true, year),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 20, true, volume),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 20, true, issue),
					CustomSqlHelper.CreateInputParameter("IssuePrefix", SqlDbType.NVarChar, 20, true, issuePrefix), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAScandata> helper = new CustomSqlHelper<IAScandata>())
				{
					CustomGenericList<IAScandata> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAScandata o = list[0];
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
		/// Update values in IAScandata. Returns an object of type IAScandata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAScandata.</param>
		/// <returns>Object of type IAScandata.</returns>
		public IAScandata IAScandataUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAScandata value)
		{
			return IAScandataUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in IAScandata. Returns an object of type IAScandata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAScandata.</param>
		/// <returns>Object of type IAScandata.</returns>
		public IAScandata IAScandataUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAScandata value)
		{
			return IAScandataUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ScandataID,
				value.ItemID,
				value.Sequence,
				value.PageType,
				value.PageNumber,
				value.Year,
				value.Volume,
				value.Issue,
				value.IssuePrefix);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage IAScandata object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAScandata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAScandata.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAScandata>.</returns>
		public CustomDataAccessStatus<IAScandata> IAScandataManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAScandata value  )
		{
			return IAScandataManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage IAScandata object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAScandata.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAScandata.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAScandata>.</returns>
		public CustomDataAccessStatus<IAScandata> IAScandataManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAScandata value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAScandata returnValue = IAScandataInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.Sequence,
						value.PageType,
						value.PageNumber,
						value.Year,
						value.Volume,
						value.Issue,
						value.IssuePrefix);
				
				return new CustomDataAccessStatus<IAScandata>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAScandataDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ScandataID))
				{
				return new CustomDataAccessStatus<IAScandata>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAScandata>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAScandata returnValue = IAScandataUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ScandataID,
						value.ItemID,
						value.Sequence,
						value.PageType,
						value.PageNumber,
						value.Year,
						value.Volume,
						value.Issue,
						value.IssuePrefix);
					
				return new CustomDataAccessStatus<IAScandata>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAScandata>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

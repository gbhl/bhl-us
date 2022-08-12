
// Generated 1/5/2021 3:27:00 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentGenreDAL is based upon dbo.SegmentGenre.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentGenreDAL
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
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	partial class SegmentGenreDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.SegmentGenre by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentGenreID"></param>
		/// <returns>Object of type SegmentGenre.</returns>
		public SegmentGenre SegmentGenreSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentGenreID)
		{
			return SegmentGenreSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentGenreID );
		}
			
		/// <summary>
		/// Select values from dbo.SegmentGenre by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentGenreID"></param>
		/// <returns>Object of type SegmentGenre.</returns>
		public SegmentGenre SegmentGenreSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentGenreID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentGenreSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentGenreID", SqlDbType.Int, null, false, segmentGenreID)))
			{
				using (CustomSqlHelper<SegmentGenre> helper = new CustomSqlHelper<SegmentGenre>())
				{
					List<SegmentGenre> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentGenre o = list[0];
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
		/// Select values from dbo.SegmentGenre by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentGenreID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentGenreSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentGenreID)
		{
			return SegmentGenreSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentGenreID );
		}
		
		/// <summary>
		/// Select values from dbo.SegmentGenre by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentGenreID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentGenreSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentGenreID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentGenreSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentGenreID", SqlDbType.Int, null, false, segmentGenreID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.SegmentGenre.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="genreName"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentGenre.</returns>
		public SegmentGenre SegmentGenreInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string genreName,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return SegmentGenreInsertAuto( sqlConnection, sqlTransaction, "BHL", genreName, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentGenre.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="genreName"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentGenre.</returns>
		public SegmentGenre SegmentGenreInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string genreName,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentGenreInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentGenreID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("GenreName", SqlDbType.NVarChar, 50, false, genreName),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentGenre> helper = new CustomSqlHelper<SegmentGenre>())
				{
					List<SegmentGenre> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentGenre o = list[0];
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
		/// Insert values into dbo.SegmentGenre. Returns an object of type SegmentGenre.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentGenre.</param>
		/// <returns>Object of type SegmentGenre.</returns>
		public SegmentGenre SegmentGenreInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentGenre value)
		{
			return SegmentGenreInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentGenre. Returns an object of type SegmentGenre.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentGenre.</param>
		/// <returns>Object of type SegmentGenre.</returns>
		public SegmentGenre SegmentGenreInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentGenre value)
		{
			return SegmentGenreInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.GenreName,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.SegmentGenre by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentGenreID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentGenreDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentGenreID)
		{
			return SegmentGenreDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentGenreID );
		}
		
		/// <summary>
		/// Delete values from dbo.SegmentGenre by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentGenreID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentGenreDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentGenreID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentGenreDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentGenreID", SqlDbType.Int, null, false, segmentGenreID), 
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
		/// Update values in dbo.SegmentGenre. Returns an object of type SegmentGenre.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentGenreID"></param>
		/// <param name="genreName"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentGenre.</returns>
		public SegmentGenre SegmentGenreUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentGenreID,
			string genreName,
			int? lastModifiedUserID)
		{
			return SegmentGenreUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentGenreID, genreName, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.SegmentGenre. Returns an object of type SegmentGenre.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentGenreID"></param>
		/// <param name="genreName"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentGenre.</returns>
		public SegmentGenre SegmentGenreUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentGenreID,
			string genreName,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentGenreUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentGenreID", SqlDbType.Int, null, false, segmentGenreID),
					CustomSqlHelper.CreateInputParameter("GenreName", SqlDbType.NVarChar, 50, false, genreName),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentGenre> helper = new CustomSqlHelper<SegmentGenre>())
				{
					List<SegmentGenre> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentGenre o = list[0];
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
		/// Update values in dbo.SegmentGenre. Returns an object of type SegmentGenre.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentGenre.</param>
		/// <returns>Object of type SegmentGenre.</returns>
		public SegmentGenre SegmentGenreUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentGenre value)
		{
			return SegmentGenreUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.SegmentGenre. Returns an object of type SegmentGenre.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentGenre.</param>
		/// <returns>Object of type SegmentGenre.</returns>
		public SegmentGenre SegmentGenreUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentGenre value)
		{
			return SegmentGenreUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentGenreID,
				value.GenreName,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.SegmentGenre object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentGenre.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentGenre.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentGenre>.</returns>
		public CustomDataAccessStatus<SegmentGenre> SegmentGenreManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentGenre value , int userId )
		{
			return SegmentGenreManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.SegmentGenre object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentGenre.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentGenre.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentGenre>.</returns>
		public CustomDataAccessStatus<SegmentGenre> SegmentGenreManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentGenre value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				SegmentGenre returnValue = SegmentGenreInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.GenreName,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<SegmentGenre>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentGenreDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentGenreID))
				{
				return new CustomDataAccessStatus<SegmentGenre>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<SegmentGenre>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				SegmentGenre returnValue = SegmentGenreUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentGenreID,
						value.GenreName,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<SegmentGenre>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<SegmentGenre>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}



// Generated 1/24/2020 4:10:38 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IASegmentAuthorDAL is based upon dbo.IASegmentAuthor.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IASegmentAuthorDAL
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
	partial class IASegmentAuthorDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IASegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>Object of type IASegmentAuthor.</returns>
		public IASegmentAuthor IASegmentAuthorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID)
		{
			return IASegmentAuthorSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	segmentAuthorID );
		}
			
		/// <summary>
		/// Select values from dbo.IASegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>Object of type IASegmentAuthor.</returns>
		public IASegmentAuthor IASegmentAuthorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentAuthorSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentAuthorID", SqlDbType.Int, null, false, segmentAuthorID)))
			{
				using (CustomSqlHelper<IASegmentAuthor> helper = new CustomSqlHelper<IASegmentAuthor>())
				{
					CustomGenericList<IASegmentAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASegmentAuthor o = list[0];
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
		/// Select values from dbo.IASegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IASegmentAuthorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID)
		{
			return IASegmentAuthorSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", segmentAuthorID );
		}
		
		/// <summary>
		/// Select values from dbo.IASegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IASegmentAuthorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentAuthorSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentAuthorID", SqlDbType.Int, null, false, segmentAuthorID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IASegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="sequence"></param>
		/// <param name="bHLAuthorID"></param>
		/// <param name="fullName"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="bHLIdentifierID"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type IASegmentAuthor.</returns>
		public IASegmentAuthor IASegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int sequence,
			int? bHLAuthorID,
			string fullName,
			string lastName,
			string firstName,
			string startDate,
			string endDate,
			int? bHLIdentifierID,
			string identifierValue)
		{
			return IASegmentAuthorInsertAuto( sqlConnection, sqlTransaction, "BHLImport", segmentID, sequence, bHLAuthorID, fullName, lastName, firstName, startDate, endDate, bHLIdentifierID, identifierValue );
		}
		
		/// <summary>
		/// Insert values into dbo.IASegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="sequence"></param>
		/// <param name="bHLAuthorID"></param>
		/// <param name="fullName"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="bHLIdentifierID"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type IASegmentAuthor.</returns>
		public IASegmentAuthor IASegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int sequence,
			int? bHLAuthorID,
			string fullName,
			string lastName,
			string firstName,
			string startDate,
			string endDate,
			int? bHLIdentifierID,
			string identifierValue)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentAuthorInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentAuthorID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, false, sequence),
					CustomSqlHelper.CreateInputParameter("BHLAuthorID", SqlDbType.Int, null, true, bHLAuthorID),
					CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 300, false, fullName),
					CustomSqlHelper.CreateInputParameter("LastName", SqlDbType.NVarChar, 150, false, lastName),
					CustomSqlHelper.CreateInputParameter("FirstName", SqlDbType.NVarChar, 150, false, firstName),
					CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.NVarChar, 25, false, startDate),
					CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.NVarChar, 25, false, endDate),
					CustomSqlHelper.CreateInputParameter("BHLIdentifierID", SqlDbType.Int, null, true, bHLIdentifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IASegmentAuthor> helper = new CustomSqlHelper<IASegmentAuthor>())
				{
					CustomGenericList<IASegmentAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASegmentAuthor o = list[0];
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
		/// Insert values into dbo.IASegmentAuthor. Returns an object of type IASegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASegmentAuthor.</param>
		/// <returns>Object of type IASegmentAuthor.</returns>
		public IASegmentAuthor IASegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASegmentAuthor value)
		{
			return IASegmentAuthorInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IASegmentAuthor. Returns an object of type IASegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASegmentAuthor.</param>
		/// <returns>Object of type IASegmentAuthor.</returns>
		public IASegmentAuthor IASegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASegmentAuthor value)
		{
			return IASegmentAuthorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.Sequence,
				value.BHLAuthorID,
				value.FullName,
				value.LastName,
				value.FirstName,
				value.StartDate,
				value.EndDate,
				value.BHLIdentifierID,
				value.IdentifierValue);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IASegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IASegmentAuthorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID)
		{
			return IASegmentAuthorDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", segmentAuthorID );
		}
		
		/// <summary>
		/// Delete values from dbo.IASegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IASegmentAuthorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentAuthorDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentAuthorID", SqlDbType.Int, null, false, segmentAuthorID), 
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
		/// Update values in dbo.IASegmentAuthor. Returns an object of type IASegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <param name="segmentID"></param>
		/// <param name="sequence"></param>
		/// <param name="bHLAuthorID"></param>
		/// <param name="fullName"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="bHLIdentifierID"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type IASegmentAuthor.</returns>
		public IASegmentAuthor IASegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID,
			int segmentID,
			int sequence,
			int? bHLAuthorID,
			string fullName,
			string lastName,
			string firstName,
			string startDate,
			string endDate,
			int? bHLIdentifierID,
			string identifierValue)
		{
			return IASegmentAuthorUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", segmentAuthorID, segmentID, sequence, bHLAuthorID, fullName, lastName, firstName, startDate, endDate, bHLIdentifierID, identifierValue);
		}
		
		/// <summary>
		/// Update values in dbo.IASegmentAuthor. Returns an object of type IASegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <param name="segmentID"></param>
		/// <param name="sequence"></param>
		/// <param name="bHLAuthorID"></param>
		/// <param name="fullName"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="bHLIdentifierID"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type IASegmentAuthor.</returns>
		public IASegmentAuthor IASegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID,
			int segmentID,
			int sequence,
			int? bHLAuthorID,
			string fullName,
			string lastName,
			string firstName,
			string startDate,
			string endDate,
			int? bHLIdentifierID,
			string identifierValue)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentAuthorUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentAuthorID", SqlDbType.Int, null, false, segmentAuthorID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, false, sequence),
					CustomSqlHelper.CreateInputParameter("BHLAuthorID", SqlDbType.Int, null, true, bHLAuthorID),
					CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 300, false, fullName),
					CustomSqlHelper.CreateInputParameter("LastName", SqlDbType.NVarChar, 150, false, lastName),
					CustomSqlHelper.CreateInputParameter("FirstName", SqlDbType.NVarChar, 150, false, firstName),
					CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.NVarChar, 25, false, startDate),
					CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.NVarChar, 25, false, endDate),
					CustomSqlHelper.CreateInputParameter("BHLIdentifierID", SqlDbType.Int, null, true, bHLIdentifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IASegmentAuthor> helper = new CustomSqlHelper<IASegmentAuthor>())
				{
					CustomGenericList<IASegmentAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASegmentAuthor o = list[0];
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
		/// Update values in dbo.IASegmentAuthor. Returns an object of type IASegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASegmentAuthor.</param>
		/// <returns>Object of type IASegmentAuthor.</returns>
		public IASegmentAuthor IASegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASegmentAuthor value)
		{
			return IASegmentAuthorUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IASegmentAuthor. Returns an object of type IASegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASegmentAuthor.</param>
		/// <returns>Object of type IASegmentAuthor.</returns>
		public IASegmentAuthor IASegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASegmentAuthor value)
		{
			return IASegmentAuthorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentAuthorID,
				value.SegmentID,
				value.Sequence,
				value.BHLAuthorID,
				value.FullName,
				value.LastName,
				value.FirstName,
				value.StartDate,
				value.EndDate,
				value.BHLIdentifierID,
				value.IdentifierValue);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IASegmentAuthor object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IASegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASegmentAuthor.</param>
		/// <returns>Object of type CustomDataAccessStatus<IASegmentAuthor>.</returns>
		public CustomDataAccessStatus<IASegmentAuthor> IASegmentAuthorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASegmentAuthor value  )
		{
			return IASegmentAuthorManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IASegmentAuthor object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IASegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASegmentAuthor.</param>
		/// <returns>Object of type CustomDataAccessStatus<IASegmentAuthor>.</returns>
		public CustomDataAccessStatus<IASegmentAuthor> IASegmentAuthorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASegmentAuthor value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IASegmentAuthor returnValue = IASegmentAuthorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.Sequence,
						value.BHLAuthorID,
						value.FullName,
						value.LastName,
						value.FirstName,
						value.StartDate,
						value.EndDate,
						value.BHLIdentifierID,
						value.IdentifierValue);
				
				return new CustomDataAccessStatus<IASegmentAuthor>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IASegmentAuthorDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentAuthorID))
				{
				return new CustomDataAccessStatus<IASegmentAuthor>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IASegmentAuthor>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IASegmentAuthor returnValue = IASegmentAuthorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentAuthorID,
						value.SegmentID,
						value.Sequence,
						value.BHLAuthorID,
						value.FullName,
						value.LastName,
						value.FirstName,
						value.StartDate,
						value.EndDate,
						value.BHLIdentifierID,
						value.IdentifierValue);
					
				return new CustomDataAccessStatus<IASegmentAuthor>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IASegmentAuthor>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


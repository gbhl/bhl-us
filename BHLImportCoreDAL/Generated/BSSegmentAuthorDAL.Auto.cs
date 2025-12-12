
// Generated 12/12/2025 1:03:44 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class BSSegmentAuthorDAL is based upon dbo.BSSegmentAuthor.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class BSSegmentAuthorDAL
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
	partial class BSSegmentAuthorDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.BSSegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>Object of type BSSegmentAuthor.</returns>
		public BSSegmentAuthor BSSegmentAuthorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID)
		{
			return BSSegmentAuthorSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	segmentAuthorID );
		}
			
		/// <summary>
		/// Select values from dbo.BSSegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>Object of type BSSegmentAuthor.</returns>
		public BSSegmentAuthor BSSegmentAuthorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentAuthorSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentAuthorID", SqlDbType.Int, null, false, segmentAuthorID)))
			{
				using (CustomSqlHelper<BSSegmentAuthor> helper = new CustomSqlHelper<BSSegmentAuthor>())
				{
					List<BSSegmentAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSSegmentAuthor o = list[0];
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
		/// Select values from dbo.BSSegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> BSSegmentAuthorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID)
		{
			return BSSegmentAuthorSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", segmentAuthorID );
		}
		
		/// <summary>
		/// Select values from dbo.BSSegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> BSSegmentAuthorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentAuthorSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentAuthorID", SqlDbType.Int, null, false, segmentAuthorID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.BSSegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importSourceID"></param>
		/// <param name="segmentID"></param>
		/// <param name="bioStorID"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="vIAFIdentifier"></param>
		/// <param name="bHLAuthorID"></param>
		/// <returns>Object of type BSSegmentAuthor.</returns>
		public BSSegmentAuthor BSSegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importSourceID,
			int segmentID,
			string bioStorID,
			string lastName,
			string firstName,
			int sequenceOrder,
			string vIAFIdentifier,
			int? bHLAuthorID)
		{
			return BSSegmentAuthorInsertAuto( sqlConnection, sqlTransaction, "BHLImport", importSourceID, segmentID, bioStorID, lastName, firstName, sequenceOrder, vIAFIdentifier, bHLAuthorID );
		}
		
		/// <summary>
		/// Insert values into dbo.BSSegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importSourceID"></param>
		/// <param name="segmentID"></param>
		/// <param name="bioStorID"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="vIAFIdentifier"></param>
		/// <param name="bHLAuthorID"></param>
		/// <returns>Object of type BSSegmentAuthor.</returns>
		public BSSegmentAuthor BSSegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importSourceID,
			int segmentID,
			string bioStorID,
			string lastName,
			string firstName,
			int sequenceOrder,
			string vIAFIdentifier,
			int? bHLAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentAuthorInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentAuthorID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("BioStorID", SqlDbType.NVarChar, 100, false, bioStorID),
					CustomSqlHelper.CreateInputParameter("LastName", SqlDbType.NVarChar, 150, false, lastName),
					CustomSqlHelper.CreateInputParameter("FirstName", SqlDbType.NVarChar, 150, false, firstName),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("VIAFIdentifier", SqlDbType.NVarChar, 20, false, vIAFIdentifier),
					CustomSqlHelper.CreateInputParameter("BHLAuthorID", SqlDbType.Int, null, true, bHLAuthorID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BSSegmentAuthor> helper = new CustomSqlHelper<BSSegmentAuthor>())
				{
					List<BSSegmentAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSSegmentAuthor o = list[0];
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
		/// Insert values into dbo.BSSegmentAuthor. Returns an object of type BSSegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSSegmentAuthor.</param>
		/// <returns>Object of type BSSegmentAuthor.</returns>
		public BSSegmentAuthor BSSegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSSegmentAuthor value)
		{
			return BSSegmentAuthorInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.BSSegmentAuthor. Returns an object of type BSSegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSSegmentAuthor.</param>
		/// <returns>Object of type BSSegmentAuthor.</returns>
		public BSSegmentAuthor BSSegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSSegmentAuthor value)
		{
			return BSSegmentAuthorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportSourceID,
				value.SegmentID,
				value.BioStorID,
				value.LastName,
				value.FirstName,
				value.SequenceOrder,
				value.VIAFIdentifier,
				value.BHLAuthorID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.BSSegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BSSegmentAuthorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID)
		{
			return BSSegmentAuthorDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", segmentAuthorID );
		}
		
		/// <summary>
		/// Delete values from dbo.BSSegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BSSegmentAuthorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentAuthorDeleteAuto", connection, transaction, 
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
		/// Update values in dbo.BSSegmentAuthor. Returns an object of type BSSegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="segmentID"></param>
		/// <param name="bioStorID"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="vIAFIdentifier"></param>
		/// <param name="bHLAuthorID"></param>
		/// <returns>Object of type BSSegmentAuthor.</returns>
		public BSSegmentAuthor BSSegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID,
			int importSourceID,
			int segmentID,
			string bioStorID,
			string lastName,
			string firstName,
			int sequenceOrder,
			string vIAFIdentifier,
			int? bHLAuthorID)
		{
			return BSSegmentAuthorUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", segmentAuthorID, importSourceID, segmentID, bioStorID, lastName, firstName, sequenceOrder, vIAFIdentifier, bHLAuthorID);
		}
		
		/// <summary>
		/// Update values in dbo.BSSegmentAuthor. Returns an object of type BSSegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="segmentID"></param>
		/// <param name="bioStorID"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="vIAFIdentifier"></param>
		/// <param name="bHLAuthorID"></param>
		/// <returns>Object of type BSSegmentAuthor.</returns>
		public BSSegmentAuthor BSSegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID,
			int importSourceID,
			int segmentID,
			string bioStorID,
			string lastName,
			string firstName,
			int sequenceOrder,
			string vIAFIdentifier,
			int? bHLAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentAuthorUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentAuthorID", SqlDbType.Int, null, false, segmentAuthorID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("BioStorID", SqlDbType.NVarChar, 100, false, bioStorID),
					CustomSqlHelper.CreateInputParameter("LastName", SqlDbType.NVarChar, 150, false, lastName),
					CustomSqlHelper.CreateInputParameter("FirstName", SqlDbType.NVarChar, 150, false, firstName),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("VIAFIdentifier", SqlDbType.NVarChar, 20, false, vIAFIdentifier),
					CustomSqlHelper.CreateInputParameter("BHLAuthorID", SqlDbType.Int, null, true, bHLAuthorID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BSSegmentAuthor> helper = new CustomSqlHelper<BSSegmentAuthor>())
				{
					List<BSSegmentAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSSegmentAuthor o = list[0];
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
		/// Update values in dbo.BSSegmentAuthor. Returns an object of type BSSegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSSegmentAuthor.</param>
		/// <returns>Object of type BSSegmentAuthor.</returns>
		public BSSegmentAuthor BSSegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSSegmentAuthor value)
		{
			return BSSegmentAuthorUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.BSSegmentAuthor. Returns an object of type BSSegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSSegmentAuthor.</param>
		/// <returns>Object of type BSSegmentAuthor.</returns>
		public BSSegmentAuthor BSSegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSSegmentAuthor value)
		{
			return BSSegmentAuthorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentAuthorID,
				value.ImportSourceID,
				value.SegmentID,
				value.BioStorID,
				value.LastName,
				value.FirstName,
				value.SequenceOrder,
				value.VIAFIdentifier,
				value.BHLAuthorID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.BSSegmentAuthor object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.BSSegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSSegmentAuthor.</param>
		/// <returns>Object of type CustomDataAccessStatus<BSSegmentAuthor>.</returns>
		public CustomDataAccessStatus<BSSegmentAuthor> BSSegmentAuthorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSSegmentAuthor value  )
		{
			return BSSegmentAuthorManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.BSSegmentAuthor object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.BSSegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSSegmentAuthor.</param>
		/// <returns>Object of type CustomDataAccessStatus<BSSegmentAuthor>.</returns>
		public CustomDataAccessStatus<BSSegmentAuthor> BSSegmentAuthorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSSegmentAuthor value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				BSSegmentAuthor returnValue = BSSegmentAuthorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportSourceID,
						value.SegmentID,
						value.BioStorID,
						value.LastName,
						value.FirstName,
						value.SequenceOrder,
						value.VIAFIdentifier,
						value.BHLAuthorID);
				
				return new CustomDataAccessStatus<BSSegmentAuthor>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (BSSegmentAuthorDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentAuthorID))
				{
				return new CustomDataAccessStatus<BSSegmentAuthor>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<BSSegmentAuthor>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				BSSegmentAuthor returnValue = BSSegmentAuthorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentAuthorID,
						value.ImportSourceID,
						value.SegmentID,
						value.BioStorID,
						value.LastName,
						value.FirstName,
						value.SequenceOrder,
						value.VIAFIdentifier,
						value.BHLAuthorID);
					
				return new CustomDataAccessStatus<BSSegmentAuthor>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<BSSegmentAuthor>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


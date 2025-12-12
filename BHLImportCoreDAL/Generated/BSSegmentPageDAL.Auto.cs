
// Generated 12/12/2025 1:03:53 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class BSSegmentPageDAL is based upon dbo.BSSegmentPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class BSSegmentPageDAL
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
	partial class BSSegmentPageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.BSSegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>Object of type BSSegmentPage.</returns>
		public BSSegmentPage BSSegmentPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID)
		{
			return BSSegmentPageSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	segmentPageID );
		}
			
		/// <summary>
		/// Select values from dbo.BSSegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>Object of type BSSegmentPage.</returns>
		public BSSegmentPage BSSegmentPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentPageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentPageID", SqlDbType.Int, null, false, segmentPageID)))
			{
				using (CustomSqlHelper<BSSegmentPage> helper = new CustomSqlHelper<BSSegmentPage>())
				{
					List<BSSegmentPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSSegmentPage o = list[0];
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
		/// Select values from dbo.BSSegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> BSSegmentPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID)
		{
			return BSSegmentPageSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", segmentPageID );
		}
		
		/// <summary>
		/// Select values from dbo.BSSegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> BSSegmentPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentPageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentPageID", SqlDbType.Int, null, false, segmentPageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.BSSegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="bHLPageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <returns>Object of type BSSegmentPage.</returns>
		public BSSegmentPage BSSegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int bHLPageID,
			short sequenceOrder)
		{
			return BSSegmentPageInsertAuto( sqlConnection, sqlTransaction, "BHLImport", segmentID, bHLPageID, sequenceOrder );
		}
		
		/// <summary>
		/// Insert values into dbo.BSSegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="bHLPageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <returns>Object of type BSSegmentPage.</returns>
		public BSSegmentPage BSSegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int bHLPageID,
			short sequenceOrder)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentPageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentPageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("BHLPageID", SqlDbType.Int, null, false, bHLPageID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BSSegmentPage> helper = new CustomSqlHelper<BSSegmentPage>())
				{
					List<BSSegmentPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSSegmentPage o = list[0];
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
		/// Insert values into dbo.BSSegmentPage. Returns an object of type BSSegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSSegmentPage.</param>
		/// <returns>Object of type BSSegmentPage.</returns>
		public BSSegmentPage BSSegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSSegmentPage value)
		{
			return BSSegmentPageInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.BSSegmentPage. Returns an object of type BSSegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSSegmentPage.</param>
		/// <returns>Object of type BSSegmentPage.</returns>
		public BSSegmentPage BSSegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSSegmentPage value)
		{
			return BSSegmentPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.BHLPageID,
				value.SequenceOrder);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.BSSegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BSSegmentPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID)
		{
			return BSSegmentPageDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", segmentPageID );
		}
		
		/// <summary>
		/// Delete values from dbo.BSSegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BSSegmentPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentPageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentPageID", SqlDbType.Int, null, false, segmentPageID), 
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
		/// Update values in dbo.BSSegmentPage. Returns an object of type BSSegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <param name="segmentID"></param>
		/// <param name="bHLPageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <returns>Object of type BSSegmentPage.</returns>
		public BSSegmentPage BSSegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID,
			int segmentID,
			int bHLPageID,
			short sequenceOrder)
		{
			return BSSegmentPageUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", segmentPageID, segmentID, bHLPageID, sequenceOrder);
		}
		
		/// <summary>
		/// Update values in dbo.BSSegmentPage. Returns an object of type BSSegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <param name="segmentID"></param>
		/// <param name="bHLPageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <returns>Object of type BSSegmentPage.</returns>
		public BSSegmentPage BSSegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID,
			int segmentID,
			int bHLPageID,
			short sequenceOrder)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentPageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentPageID", SqlDbType.Int, null, false, segmentPageID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("BHLPageID", SqlDbType.Int, null, false, bHLPageID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BSSegmentPage> helper = new CustomSqlHelper<BSSegmentPage>())
				{
					List<BSSegmentPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSSegmentPage o = list[0];
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
		/// Update values in dbo.BSSegmentPage. Returns an object of type BSSegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSSegmentPage.</param>
		/// <returns>Object of type BSSegmentPage.</returns>
		public BSSegmentPage BSSegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSSegmentPage value)
		{
			return BSSegmentPageUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.BSSegmentPage. Returns an object of type BSSegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSSegmentPage.</param>
		/// <returns>Object of type BSSegmentPage.</returns>
		public BSSegmentPage BSSegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSSegmentPage value)
		{
			return BSSegmentPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentPageID,
				value.SegmentID,
				value.BHLPageID,
				value.SequenceOrder);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.BSSegmentPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.BSSegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSSegmentPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<BSSegmentPage>.</returns>
		public CustomDataAccessStatus<BSSegmentPage> BSSegmentPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSSegmentPage value  )
		{
			return BSSegmentPageManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.BSSegmentPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.BSSegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSSegmentPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<BSSegmentPage>.</returns>
		public CustomDataAccessStatus<BSSegmentPage> BSSegmentPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSSegmentPage value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				BSSegmentPage returnValue = BSSegmentPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.BHLPageID,
						value.SequenceOrder);
				
				return new CustomDataAccessStatus<BSSegmentPage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (BSSegmentPageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentPageID))
				{
				return new CustomDataAccessStatus<BSSegmentPage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<BSSegmentPage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				BSSegmentPage returnValue = BSSegmentPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentPageID,
						value.SegmentID,
						value.BHLPageID,
						value.SequenceOrder);
					
				return new CustomDataAccessStatus<BSSegmentPage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<BSSegmentPage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


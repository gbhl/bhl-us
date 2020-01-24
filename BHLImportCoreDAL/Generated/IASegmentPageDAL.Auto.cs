
// Generated 1/24/2020 4:10:31 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IASegmentPageDAL is based upon dbo.IASegmentPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IASegmentPageDAL
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
	partial class IASegmentPageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IASegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>Object of type IASegmentPage.</returns>
		public IASegmentPage IASegmentPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID)
		{
			return IASegmentPageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentPageID );
		}
			
		/// <summary>
		/// Select values from dbo.IASegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>Object of type IASegmentPage.</returns>
		public IASegmentPage IASegmentPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentPageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentPageID", SqlDbType.Int, null, false, segmentPageID)))
			{
				using (CustomSqlHelper<IASegmentPage> helper = new CustomSqlHelper<IASegmentPage>())
				{
					CustomGenericList<IASegmentPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASegmentPage o = list[0];
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
		/// Select values from dbo.IASegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IASegmentPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID)
		{
			return IASegmentPageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentPageID );
		}
		
		/// <summary>
		/// Select values from dbo.IASegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IASegmentPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentPageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentPageID", SqlDbType.Int, null, false, segmentPageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IASegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="pageSequence"></param>
		/// <returns>Object of type IASegmentPage.</returns>
		public IASegmentPage IASegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int pageSequence)
		{
			return IASegmentPageInsertAuto( sqlConnection, sqlTransaction, "BHL", segmentID, pageSequence );
		}
		
		/// <summary>
		/// Insert values into dbo.IASegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="pageSequence"></param>
		/// <returns>Object of type IASegmentPage.</returns>
		public IASegmentPage IASegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int pageSequence)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentPageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentPageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("PageSequence", SqlDbType.Int, null, false, pageSequence), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IASegmentPage> helper = new CustomSqlHelper<IASegmentPage>())
				{
					CustomGenericList<IASegmentPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASegmentPage o = list[0];
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
		/// Insert values into dbo.IASegmentPage. Returns an object of type IASegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASegmentPage.</param>
		/// <returns>Object of type IASegmentPage.</returns>
		public IASegmentPage IASegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASegmentPage value)
		{
			return IASegmentPageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IASegmentPage. Returns an object of type IASegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASegmentPage.</param>
		/// <returns>Object of type IASegmentPage.</returns>
		public IASegmentPage IASegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASegmentPage value)
		{
			return IASegmentPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.PageSequence);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IASegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IASegmentPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID)
		{
			return IASegmentPageDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentPageID );
		}
		
		/// <summary>
		/// Delete values from dbo.IASegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IASegmentPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentPageDeleteAuto", connection, transaction, 
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
		/// Update values in dbo.IASegmentPage. Returns an object of type IASegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <param name="segmentID"></param>
		/// <param name="pageSequence"></param>
		/// <returns>Object of type IASegmentPage.</returns>
		public IASegmentPage IASegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID,
			int segmentID,
			int pageSequence)
		{
			return IASegmentPageUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentPageID, segmentID, pageSequence);
		}
		
		/// <summary>
		/// Update values in dbo.IASegmentPage. Returns an object of type IASegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <param name="segmentID"></param>
		/// <param name="pageSequence"></param>
		/// <returns>Object of type IASegmentPage.</returns>
		public IASegmentPage IASegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID,
			int segmentID,
			int pageSequence)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentPageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentPageID", SqlDbType.Int, null, false, segmentPageID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("PageSequence", SqlDbType.Int, null, false, pageSequence), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IASegmentPage> helper = new CustomSqlHelper<IASegmentPage>())
				{
					CustomGenericList<IASegmentPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASegmentPage o = list[0];
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
		/// Update values in dbo.IASegmentPage. Returns an object of type IASegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASegmentPage.</param>
		/// <returns>Object of type IASegmentPage.</returns>
		public IASegmentPage IASegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASegmentPage value)
		{
			return IASegmentPageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.IASegmentPage. Returns an object of type IASegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASegmentPage.</param>
		/// <returns>Object of type IASegmentPage.</returns>
		public IASegmentPage IASegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASegmentPage value)
		{
			return IASegmentPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentPageID,
				value.SegmentID,
				value.PageSequence);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IASegmentPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IASegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASegmentPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<IASegmentPage>.</returns>
		public CustomDataAccessStatus<IASegmentPage> IASegmentPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASegmentPage value  )
		{
			return IASegmentPageManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.IASegmentPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IASegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASegmentPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<IASegmentPage>.</returns>
		public CustomDataAccessStatus<IASegmentPage> IASegmentPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASegmentPage value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IASegmentPage returnValue = IASegmentPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.PageSequence);
				
				return new CustomDataAccessStatus<IASegmentPage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IASegmentPageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentPageID))
				{
				return new CustomDataAccessStatus<IASegmentPage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IASegmentPage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IASegmentPage returnValue = IASegmentPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentPageID,
						value.SegmentID,
						value.PageSequence);
					
				return new CustomDataAccessStatus<IASegmentPage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IASegmentPage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


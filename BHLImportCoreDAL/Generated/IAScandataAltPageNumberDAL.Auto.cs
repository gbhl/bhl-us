
// Generated 11/23/2010 11:26:17 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAScandataAltPageNumberDAL is based upon IAScandataAltPageNumber.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAScandataAltPageNumberDAL
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
	partial class IAScandataAltPageNumberDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from IAScandataAltPageNumber by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataAltPageNumberID"></param>
		/// <returns>Object of type IAScandataAltPageNumber.</returns>
		public IAScandataAltPageNumber IAScandataAltPageNumberSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataAltPageNumberID)
		{
			return IAScandataAltPageNumberSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	scandataAltPageNumberID );
		}
			
		/// <summary>
		/// Select values from IAScandataAltPageNumber by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataAltPageNumberID"></param>
		/// <returns>Object of type IAScandataAltPageNumber.</returns>
		public IAScandataAltPageNumber IAScandataAltPageNumberSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataAltPageNumberID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageNumberSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScandataAltPageNumberID", SqlDbType.Int, null, false, scandataAltPageNumberID)))
			{
				using (CustomSqlHelper<IAScandataAltPageNumber> helper = new CustomSqlHelper<IAScandataAltPageNumber>())
				{
					CustomGenericList<IAScandataAltPageNumber> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAScandataAltPageNumber o = list[0];
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
		/// Select values from IAScandataAltPageNumber by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataAltPageNumberID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAScandataAltPageNumberSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataAltPageNumberID)
		{
			return IAScandataAltPageNumberSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", scandataAltPageNumberID );
		}
		
		/// <summary>
		/// Select values from IAScandataAltPageNumber by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataAltPageNumberID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAScandataAltPageNumberSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataAltPageNumberID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageNumberSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ScandataAltPageNumberID", SqlDbType.Int, null, false, scandataAltPageNumberID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into IAScandataAltPageNumber.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataID"></param>
		/// <param name="sequence"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <returns>Object of type IAScandataAltPageNumber.</returns>
		public IAScandataAltPageNumber IAScandataAltPageNumberInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataID,
			int sequence,
			string pagePrefix,
			string pageNumber,
			bool implied)
		{
			return IAScandataAltPageNumberInsertAuto( sqlConnection, sqlTransaction, "BHLImport", scandataID, sequence, pagePrefix, pageNumber, implied );
		}
		
		/// <summary>
		/// Insert values into IAScandataAltPageNumber.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataID"></param>
		/// <param name="sequence"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <returns>Object of type IAScandataAltPageNumber.</returns>
		public IAScandataAltPageNumber IAScandataAltPageNumberInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataID,
			int sequence,
			string pagePrefix,
			string pageNumber,
			bool implied)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageNumberInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ScandataAltPageNumberID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, false, sequence),
					CustomSqlHelper.CreateInputParameter("PagePrefix", SqlDbType.NVarChar, 40, false, pagePrefix),
					CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, false, pageNumber),
					CustomSqlHelper.CreateInputParameter("Implied", SqlDbType.Bit, null, false, implied), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAScandataAltPageNumber> helper = new CustomSqlHelper<IAScandataAltPageNumber>())
				{
					CustomGenericList<IAScandataAltPageNumber> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAScandataAltPageNumber o = list[0];
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
		/// Insert values into IAScandataAltPageNumber. Returns an object of type IAScandataAltPageNumber.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAScandataAltPageNumber.</param>
		/// <returns>Object of type IAScandataAltPageNumber.</returns>
		public IAScandataAltPageNumber IAScandataAltPageNumberInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAScandataAltPageNumber value)
		{
			return IAScandataAltPageNumberInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into IAScandataAltPageNumber. Returns an object of type IAScandataAltPageNumber.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAScandataAltPageNumber.</param>
		/// <returns>Object of type IAScandataAltPageNumber.</returns>
		public IAScandataAltPageNumber IAScandataAltPageNumberInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAScandataAltPageNumber value)
		{
			return IAScandataAltPageNumberInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ScandataID,
				value.Sequence,
				value.PagePrefix,
				value.PageNumber,
				value.Implied);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from IAScandataAltPageNumber by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataAltPageNumberID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAScandataAltPageNumberDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataAltPageNumberID)
		{
			return IAScandataAltPageNumberDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", scandataAltPageNumberID );
		}
		
		/// <summary>
		/// Delete values from IAScandataAltPageNumber by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataAltPageNumberID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAScandataAltPageNumberDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataAltPageNumberID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageNumberDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScandataAltPageNumberID", SqlDbType.Int, null, false, scandataAltPageNumberID), 
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
		/// Update values in IAScandataAltPageNumber. Returns an object of type IAScandataAltPageNumber.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataAltPageNumberID"></param>
		/// <param name="scandataID"></param>
		/// <param name="sequence"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <returns>Object of type IAScandataAltPageNumber.</returns>
		public IAScandataAltPageNumber IAScandataAltPageNumberUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataAltPageNumberID,
			int scandataID,
			int sequence,
			string pagePrefix,
			string pageNumber,
			bool implied)
		{
			return IAScandataAltPageNumberUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", scandataAltPageNumberID, scandataID, sequence, pagePrefix, pageNumber, implied);
		}
		
		/// <summary>
		/// Update values in IAScandataAltPageNumber. Returns an object of type IAScandataAltPageNumber.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataAltPageNumberID"></param>
		/// <param name="scandataID"></param>
		/// <param name="sequence"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <returns>Object of type IAScandataAltPageNumber.</returns>
		public IAScandataAltPageNumber IAScandataAltPageNumberUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataAltPageNumberID,
			int scandataID,
			int sequence,
			string pagePrefix,
			string pageNumber,
			bool implied)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageNumberUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScandataAltPageNumberID", SqlDbType.Int, null, false, scandataAltPageNumberID),
					CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, false, sequence),
					CustomSqlHelper.CreateInputParameter("PagePrefix", SqlDbType.NVarChar, 40, false, pagePrefix),
					CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, false, pageNumber),
					CustomSqlHelper.CreateInputParameter("Implied", SqlDbType.Bit, null, false, implied), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAScandataAltPageNumber> helper = new CustomSqlHelper<IAScandataAltPageNumber>())
				{
					CustomGenericList<IAScandataAltPageNumber> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAScandataAltPageNumber o = list[0];
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
		/// Update values in IAScandataAltPageNumber. Returns an object of type IAScandataAltPageNumber.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAScandataAltPageNumber.</param>
		/// <returns>Object of type IAScandataAltPageNumber.</returns>
		public IAScandataAltPageNumber IAScandataAltPageNumberUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAScandataAltPageNumber value)
		{
			return IAScandataAltPageNumberUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in IAScandataAltPageNumber. Returns an object of type IAScandataAltPageNumber.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAScandataAltPageNumber.</param>
		/// <returns>Object of type IAScandataAltPageNumber.</returns>
		public IAScandataAltPageNumber IAScandataAltPageNumberUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAScandataAltPageNumber value)
		{
			return IAScandataAltPageNumberUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ScandataAltPageNumberID,
				value.ScandataID,
				value.Sequence,
				value.PagePrefix,
				value.PageNumber,
				value.Implied);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage IAScandataAltPageNumber object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAScandataAltPageNumber.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAScandataAltPageNumber.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAScandataAltPageNumber>.</returns>
		public CustomDataAccessStatus<IAScandataAltPageNumber> IAScandataAltPageNumberManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAScandataAltPageNumber value  )
		{
			return IAScandataAltPageNumberManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage IAScandataAltPageNumber object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAScandataAltPageNumber.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAScandataAltPageNumber.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAScandataAltPageNumber>.</returns>
		public CustomDataAccessStatus<IAScandataAltPageNumber> IAScandataAltPageNumberManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAScandataAltPageNumber value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAScandataAltPageNumber returnValue = IAScandataAltPageNumberInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ScandataID,
						value.Sequence,
						value.PagePrefix,
						value.PageNumber,
						value.Implied);
				
				return new CustomDataAccessStatus<IAScandataAltPageNumber>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAScandataAltPageNumberDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ScandataAltPageNumberID))
				{
				return new CustomDataAccessStatus<IAScandataAltPageNumber>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAScandataAltPageNumber>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAScandataAltPageNumber returnValue = IAScandataAltPageNumberUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ScandataAltPageNumberID,
						value.ScandataID,
						value.Sequence,
						value.PagePrefix,
						value.PageNumber,
						value.Implied);
					
				return new CustomDataAccessStatus<IAScandataAltPageNumber>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAScandataAltPageNumber>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

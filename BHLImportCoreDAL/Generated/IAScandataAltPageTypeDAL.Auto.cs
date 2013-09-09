
// Generated 11/23/2010 11:26:17 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAScandataAltPageTypeDAL is based upon IAScandataAltPageType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAScandataAltPageTypeDAL
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
	partial class IAScandataAltPageTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from IAScandataAltPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataAltPageTypeID"></param>
		/// <returns>Object of type IAScandataAltPageType.</returns>
		public IAScandataAltPageType IAScandataAltPageTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataAltPageTypeID)
		{
			return IAScandataAltPageTypeSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	scandataAltPageTypeID );
		}
			
		/// <summary>
		/// Select values from IAScandataAltPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataAltPageTypeID"></param>
		/// <returns>Object of type IAScandataAltPageType.</returns>
		public IAScandataAltPageType IAScandataAltPageTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataAltPageTypeID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScandataAltPageTypeID", SqlDbType.Int, null, false, scandataAltPageTypeID)))
			{
				using (CustomSqlHelper<IAScandataAltPageType> helper = new CustomSqlHelper<IAScandataAltPageType>())
				{
					CustomGenericList<IAScandataAltPageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAScandataAltPageType o = list[0];
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
		/// Select values from IAScandataAltPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataAltPageTypeID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAScandataAltPageTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataAltPageTypeID)
		{
			return IAScandataAltPageTypeSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", scandataAltPageTypeID );
		}
		
		/// <summary>
		/// Select values from IAScandataAltPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataAltPageTypeID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAScandataAltPageTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataAltPageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ScandataAltPageTypeID", SqlDbType.Int, null, false, scandataAltPageTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into IAScandataAltPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataID"></param>
		/// <param name="pageType"></param>
		/// <returns>Object of type IAScandataAltPageType.</returns>
		public IAScandataAltPageType IAScandataAltPageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataID,
			string pageType)
		{
			return IAScandataAltPageTypeInsertAuto( sqlConnection, sqlTransaction, "BHLImport", scandataID, pageType );
		}
		
		/// <summary>
		/// Insert values into IAScandataAltPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataID"></param>
		/// <param name="pageType"></param>
		/// <returns>Object of type IAScandataAltPageType.</returns>
		public IAScandataAltPageType IAScandataAltPageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataID,
			string pageType)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ScandataAltPageTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID),
					CustomSqlHelper.CreateInputParameter("PageType", SqlDbType.NVarChar, 50, false, pageType), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAScandataAltPageType> helper = new CustomSqlHelper<IAScandataAltPageType>())
				{
					CustomGenericList<IAScandataAltPageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAScandataAltPageType o = list[0];
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
		/// Insert values into IAScandataAltPageType. Returns an object of type IAScandataAltPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAScandataAltPageType.</param>
		/// <returns>Object of type IAScandataAltPageType.</returns>
		public IAScandataAltPageType IAScandataAltPageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAScandataAltPageType value)
		{
			return IAScandataAltPageTypeInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into IAScandataAltPageType. Returns an object of type IAScandataAltPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAScandataAltPageType.</param>
		/// <returns>Object of type IAScandataAltPageType.</returns>
		public IAScandataAltPageType IAScandataAltPageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAScandataAltPageType value)
		{
			return IAScandataAltPageTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ScandataID,
				value.PageType);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from IAScandataAltPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataAltPageTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAScandataAltPageTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataAltPageTypeID)
		{
			return IAScandataAltPageTypeDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", scandataAltPageTypeID );
		}
		
		/// <summary>
		/// Delete values from IAScandataAltPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataAltPageTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAScandataAltPageTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataAltPageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScandataAltPageTypeID", SqlDbType.Int, null, false, scandataAltPageTypeID), 
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
		/// Update values in IAScandataAltPageType. Returns an object of type IAScandataAltPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scandataAltPageTypeID"></param>
		/// <param name="scandataID"></param>
		/// <param name="pageType"></param>
		/// <returns>Object of type IAScandataAltPageType.</returns>
		public IAScandataAltPageType IAScandataAltPageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scandataAltPageTypeID,
			int scandataID,
			string pageType)
		{
			return IAScandataAltPageTypeUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", scandataAltPageTypeID, scandataID, pageType);
		}
		
		/// <summary>
		/// Update values in IAScandataAltPageType. Returns an object of type IAScandataAltPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scandataAltPageTypeID"></param>
		/// <param name="scandataID"></param>
		/// <param name="pageType"></param>
		/// <returns>Object of type IAScandataAltPageType.</returns>
		public IAScandataAltPageType IAScandataAltPageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scandataAltPageTypeID,
			int scandataID,
			string pageType)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScandataAltPageTypeID", SqlDbType.Int, null, false, scandataAltPageTypeID),
					CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID),
					CustomSqlHelper.CreateInputParameter("PageType", SqlDbType.NVarChar, 50, false, pageType), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAScandataAltPageType> helper = new CustomSqlHelper<IAScandataAltPageType>())
				{
					CustomGenericList<IAScandataAltPageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAScandataAltPageType o = list[0];
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
		/// Update values in IAScandataAltPageType. Returns an object of type IAScandataAltPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAScandataAltPageType.</param>
		/// <returns>Object of type IAScandataAltPageType.</returns>
		public IAScandataAltPageType IAScandataAltPageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAScandataAltPageType value)
		{
			return IAScandataAltPageTypeUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in IAScandataAltPageType. Returns an object of type IAScandataAltPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAScandataAltPageType.</param>
		/// <returns>Object of type IAScandataAltPageType.</returns>
		public IAScandataAltPageType IAScandataAltPageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAScandataAltPageType value)
		{
			return IAScandataAltPageTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ScandataAltPageTypeID,
				value.ScandataID,
				value.PageType);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage IAScandataAltPageType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAScandataAltPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAScandataAltPageType.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAScandataAltPageType>.</returns>
		public CustomDataAccessStatus<IAScandataAltPageType> IAScandataAltPageTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAScandataAltPageType value  )
		{
			return IAScandataAltPageTypeManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage IAScandataAltPageType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAScandataAltPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAScandataAltPageType.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAScandataAltPageType>.</returns>
		public CustomDataAccessStatus<IAScandataAltPageType> IAScandataAltPageTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAScandataAltPageType value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAScandataAltPageType returnValue = IAScandataAltPageTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ScandataID,
						value.PageType);
				
				return new CustomDataAccessStatus<IAScandataAltPageType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAScandataAltPageTypeDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ScandataAltPageTypeID))
				{
				return new CustomDataAccessStatus<IAScandataAltPageType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAScandataAltPageType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAScandataAltPageType returnValue = IAScandataAltPageTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ScandataAltPageTypeID,
						value.ScandataID,
						value.PageType);
					
				return new CustomDataAccessStatus<IAScandataAltPageType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAScandataAltPageType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation


// Generated 10/16/2024 5:39:53 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ServiceDAL is based upon servlog.Service.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ServiceDAL
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
	partial class ServiceDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from servlog.Service by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="serviceID"></param>
		/// <returns>Object of type Service.</returns>
		public Service ServiceSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int serviceID)
		{
			return ServiceSelectAuto(	sqlConnection, sqlTransaction, "BHL",	serviceID );
		}
			
		/// <summary>
		/// Select values from servlog.Service by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="serviceID"></param>
		/// <returns>Object of type Service.</returns>
		public Service ServiceSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int serviceID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ServiceSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ServiceID", SqlDbType.Int, null, false, serviceID)))
			{
				using (CustomSqlHelper<Service> helper = new CustomSqlHelper<Service>())
				{
					List<Service> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Service o = list[0];
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
		/// Select values from servlog.Service by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="serviceID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ServiceSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int serviceID)
		{
			return ServiceSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", serviceID );
		}
		
		/// <summary>
		/// Select values from servlog.Service by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="serviceID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ServiceSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int serviceID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ServiceSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ServiceID", SqlDbType.Int, null, false, serviceID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into servlog.Service.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="name"></param>
		/// <param name="param"></param>
		/// <param name="frequencyID"></param>
		/// <param name="disabled"></param>
		/// <param name="display"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Service.</returns>
		public Service ServiceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string name,
			string param,
			int? frequencyID,
			byte disabled,
			byte display,
			int creationUserID,
			int lastModifiedUserID)
		{
			return ServiceInsertAuto( sqlConnection, sqlTransaction, "BHL", name, param, frequencyID, disabled, display, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into servlog.Service.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="name"></param>
		/// <param name="param"></param>
		/// <param name="frequencyID"></param>
		/// <param name="disabled"></param>
		/// <param name="display"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Service.</returns>
		public Service ServiceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string name,
			string param,
			int? frequencyID,
			byte disabled,
			byte display,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ServiceInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ServiceID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("Name", SqlDbType.NVarChar, 200, false, name),
					CustomSqlHelper.CreateInputParameter("Param", SqlDbType.NVarChar, 30, false, param),
					CustomSqlHelper.CreateInputParameter("FrequencyID", SqlDbType.Int, null, true, frequencyID),
					CustomSqlHelper.CreateInputParameter("Disabled", SqlDbType.TinyInt, null, false, disabled),
					CustomSqlHelper.CreateInputParameter("Display", SqlDbType.TinyInt, null, false, display),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Service> helper = new CustomSqlHelper<Service>())
				{
					List<Service> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Service o = list[0];
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
		/// Insert values into servlog.Service. Returns an object of type Service.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Service.</param>
		/// <returns>Object of type Service.</returns>
		public Service ServiceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Service value)
		{
			return ServiceInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into servlog.Service. Returns an object of type Service.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Service.</param>
		/// <returns>Object of type Service.</returns>
		public Service ServiceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Service value)
		{
			return ServiceInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.Name,
				value.Param,
				value.FrequencyID,
				value.Disabled,
				value.Display,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from servlog.Service by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="serviceID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ServiceDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int serviceID)
		{
			return ServiceDeleteAuto( sqlConnection, sqlTransaction, "BHL", serviceID );
		}
		
		/// <summary>
		/// Delete values from servlog.Service by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="serviceID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ServiceDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int serviceID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ServiceDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ServiceID", SqlDbType.Int, null, false, serviceID), 
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
		/// Update values in servlog.Service. Returns an object of type Service.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="serviceID"></param>
		/// <param name="name"></param>
		/// <param name="param"></param>
		/// <param name="frequencyID"></param>
		/// <param name="disabled"></param>
		/// <param name="display"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Service.</returns>
		public Service ServiceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int serviceID,
			string name,
			string param,
			int? frequencyID,
			byte disabled,
			byte display,
			int lastModifiedUserID)
		{
			return ServiceUpdateAuto( sqlConnection, sqlTransaction, "BHL", serviceID, name, param, frequencyID, disabled, display, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in servlog.Service. Returns an object of type Service.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="serviceID"></param>
		/// <param name="name"></param>
		/// <param name="param"></param>
		/// <param name="frequencyID"></param>
		/// <param name="disabled"></param>
		/// <param name="display"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Service.</returns>
		public Service ServiceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int serviceID,
			string name,
			string param,
			int? frequencyID,
			byte disabled,
			byte display,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ServiceUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ServiceID", SqlDbType.Int, null, false, serviceID),
					CustomSqlHelper.CreateInputParameter("Name", SqlDbType.NVarChar, 200, false, name),
					CustomSqlHelper.CreateInputParameter("Param", SqlDbType.NVarChar, 30, false, param),
					CustomSqlHelper.CreateInputParameter("FrequencyID", SqlDbType.Int, null, true, frequencyID),
					CustomSqlHelper.CreateInputParameter("Disabled", SqlDbType.TinyInt, null, false, disabled),
					CustomSqlHelper.CreateInputParameter("Display", SqlDbType.TinyInt, null, false, display),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Service> helper = new CustomSqlHelper<Service>())
				{
					List<Service> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Service o = list[0];
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
		/// Update values in servlog.Service. Returns an object of type Service.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Service.</param>
		/// <returns>Object of type Service.</returns>
		public Service ServiceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Service value)
		{
			return ServiceUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in servlog.Service. Returns an object of type Service.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Service.</param>
		/// <returns>Object of type Service.</returns>
		public Service ServiceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Service value)
		{
			return ServiceUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ServiceID,
				value.Name,
				value.Param,
				value.FrequencyID,
				value.Disabled,
				value.Display,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage servlog.Service object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in servlog.Service.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Service.</param>
		/// <returns>Object of type CustomDataAccessStatus<Service>.</returns>
		public CustomDataAccessStatus<Service> ServiceManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Service value , int userId )
		{
			return ServiceManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage servlog.Service object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in servlog.Service.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Service.</param>
		/// <returns>Object of type CustomDataAccessStatus<Service>.</returns>
		public CustomDataAccessStatus<Service> ServiceManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Service value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Service returnValue = ServiceInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.Name,
						value.Param,
						value.FrequencyID,
						value.Disabled,
						value.Display,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<Service>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ServiceDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ServiceID))
				{
				return new CustomDataAccessStatus<Service>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Service>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				Service returnValue = ServiceUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ServiceID,
						value.Name,
						value.Param,
						value.FrequencyID,
						value.Disabled,
						value.Display,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<Service>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Service>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


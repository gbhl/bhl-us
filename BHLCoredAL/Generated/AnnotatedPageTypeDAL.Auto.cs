
// Generated 12/20/2010 4:06:17 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotatedPageTypeDAL is based upon AnnotatedPageType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotatedPageTypeDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	partial class AnnotatedPageTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotatedPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageTypeID"></param>
		/// <returns>Object of type AnnotatedPageType.</returns>
		public AnnotatedPageType AnnotatedPageTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageTypeID)
		{
			return AnnotatedPageTypeSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotatedPageTypeID );
		}
			
		/// <summary>
		/// Select values from AnnotatedPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageTypeID"></param>
		/// <returns>Object of type AnnotatedPageType.</returns>
		public AnnotatedPageType AnnotatedPageTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageTypeID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageTypeID", SqlDbType.Int, null, false, annotatedPageTypeID)))
			{
				using (CustomSqlHelper<AnnotatedPageType> helper = new CustomSqlHelper<AnnotatedPageType>())
				{
					CustomGenericList<AnnotatedPageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedPageType o = list[0];
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
		/// Select values from AnnotatedPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageTypeID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AnnotatedPageTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageTypeID)
		{
			return AnnotatedPageTypeSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotatedPageTypeID );
		}
		
		/// <summary>
		/// Select values from AnnotatedPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageTypeID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AnnotatedPageTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotatedPageTypeID", SqlDbType.Int, null, false, annotatedPageTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotatedPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageTypeName"></param>
		/// <param name="annotatedPageTypeDescription"></param>
		/// <returns>Object of type AnnotatedPageType.</returns>
		public AnnotatedPageType AnnotatedPageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string annotatedPageTypeName,
			string annotatedPageTypeDescription)
		{
			return AnnotatedPageTypeInsertAuto( sqlConnection, sqlTransaction, "BHL", annotatedPageTypeName, annotatedPageTypeDescription );
		}
		
		/// <summary>
		/// Insert values into AnnotatedPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageTypeName"></param>
		/// <param name="annotatedPageTypeDescription"></param>
		/// <returns>Object of type AnnotatedPageType.</returns>
		public AnnotatedPageType AnnotatedPageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string annotatedPageTypeName,
			string annotatedPageTypeDescription)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AnnotatedPageTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AnnotatedPageTypeName", SqlDbType.NVarChar, 30, false, annotatedPageTypeName),
					CustomSqlHelper.CreateInputParameter("AnnotatedPageTypeDescription", SqlDbType.NVarChar, 500, false, annotatedPageTypeDescription), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotatedPageType> helper = new CustomSqlHelper<AnnotatedPageType>())
				{
					CustomGenericList<AnnotatedPageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedPageType o = list[0];
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
		/// Insert values into AnnotatedPageType. Returns an object of type AnnotatedPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedPageType.</param>
		/// <returns>Object of type AnnotatedPageType.</returns>
		public AnnotatedPageType AnnotatedPageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedPageType value)
		{
			return AnnotatedPageTypeInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotatedPageType. Returns an object of type AnnotatedPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedPageType.</param>
		/// <returns>Object of type AnnotatedPageType.</returns>
		public AnnotatedPageType AnnotatedPageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedPageType value)
		{
			return AnnotatedPageTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotatedPageTypeName,
				value.AnnotatedPageTypeDescription);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotatedPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotatedPageTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageTypeID)
		{
			return AnnotatedPageTypeDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotatedPageTypeID );
		}
		
		/// <summary>
		/// Delete values from AnnotatedPageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotatedPageTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageTypeID", SqlDbType.Int, null, false, annotatedPageTypeID), 
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
		/// Update values in AnnotatedPageType. Returns an object of type AnnotatedPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageTypeID"></param>
		/// <param name="annotatedPageTypeName"></param>
		/// <param name="annotatedPageTypeDescription"></param>
		/// <returns>Object of type AnnotatedPageType.</returns>
		public AnnotatedPageType AnnotatedPageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageTypeID,
			string annotatedPageTypeName,
			string annotatedPageTypeDescription)
		{
			return AnnotatedPageTypeUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotatedPageTypeID, annotatedPageTypeName, annotatedPageTypeDescription);
		}
		
		/// <summary>
		/// Update values in AnnotatedPageType. Returns an object of type AnnotatedPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageTypeID"></param>
		/// <param name="annotatedPageTypeName"></param>
		/// <param name="annotatedPageTypeDescription"></param>
		/// <returns>Object of type AnnotatedPageType.</returns>
		public AnnotatedPageType AnnotatedPageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageTypeID,
			string annotatedPageTypeName,
			string annotatedPageTypeDescription)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageTypeID", SqlDbType.Int, null, false, annotatedPageTypeID),
					CustomSqlHelper.CreateInputParameter("AnnotatedPageTypeName", SqlDbType.NVarChar, 30, false, annotatedPageTypeName),
					CustomSqlHelper.CreateInputParameter("AnnotatedPageTypeDescription", SqlDbType.NVarChar, 500, false, annotatedPageTypeDescription), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotatedPageType> helper = new CustomSqlHelper<AnnotatedPageType>())
				{
					CustomGenericList<AnnotatedPageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedPageType o = list[0];
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
		/// Update values in AnnotatedPageType. Returns an object of type AnnotatedPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedPageType.</param>
		/// <returns>Object of type AnnotatedPageType.</returns>
		public AnnotatedPageType AnnotatedPageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedPageType value)
		{
			return AnnotatedPageTypeUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotatedPageType. Returns an object of type AnnotatedPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedPageType.</param>
		/// <returns>Object of type AnnotatedPageType.</returns>
		public AnnotatedPageType AnnotatedPageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedPageType value)
		{
			return AnnotatedPageTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotatedPageTypeID,
				value.AnnotatedPageTypeName,
				value.AnnotatedPageTypeDescription);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotatedPageType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotatedPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedPageType.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotatedPageType>.</returns>
		public CustomDataAccessStatus<AnnotatedPageType> AnnotatedPageTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedPageType value  )
		{
			return AnnotatedPageTypeManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotatedPageType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotatedPageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedPageType.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotatedPageType>.</returns>
		public CustomDataAccessStatus<AnnotatedPageType> AnnotatedPageTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedPageType value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotatedPageType returnValue = AnnotatedPageTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedPageTypeName,
						value.AnnotatedPageTypeDescription);
				
				return new CustomDataAccessStatus<AnnotatedPageType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotatedPageTypeDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedPageTypeID))
				{
				return new CustomDataAccessStatus<AnnotatedPageType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotatedPageType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotatedPageType returnValue = AnnotatedPageTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedPageTypeID,
						value.AnnotatedPageTypeName,
						value.AnnotatedPageTypeDescription);
					
				return new CustomDataAccessStatus<AnnotatedPageType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotatedPageType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

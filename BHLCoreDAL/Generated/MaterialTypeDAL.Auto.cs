
// Generated 1/5/2021 3:26:12 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class MaterialTypeDAL is based upon dbo.MaterialType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class MaterialTypeDAL
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
	partial class MaterialTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.MaterialType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="materialTypeID"></param>
		/// <returns>Object of type MaterialType.</returns>
		public MaterialType MaterialTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int materialTypeID)
		{
			return MaterialTypeSelectAuto(	sqlConnection, sqlTransaction, "BHL",	materialTypeID );
		}
			
		/// <summary>
		/// Select values from dbo.MaterialType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="materialTypeID"></param>
		/// <returns>Object of type MaterialType.</returns>
		public MaterialType MaterialTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int materialTypeID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MaterialTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MaterialTypeID", SqlDbType.Int, null, false, materialTypeID)))
			{
				using (CustomSqlHelper<MaterialType> helper = new CustomSqlHelper<MaterialType>())
				{
					List<MaterialType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MaterialType o = list[0];
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
		/// Select values from dbo.MaterialType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="materialTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> MaterialTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int materialTypeID)
		{
			return MaterialTypeSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", materialTypeID );
		}
		
		/// <summary>
		/// Select values from dbo.MaterialType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="materialTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> MaterialTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int materialTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MaterialTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("MaterialTypeID", SqlDbType.Int, null, false, materialTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.MaterialType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="materialTypeName"></param>
		/// <param name="materialTypeLabel"></param>
		/// <param name="mARCCode"></param>
		/// <returns>Object of type MaterialType.</returns>
		public MaterialType MaterialTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string materialTypeName,
			string materialTypeLabel,
			string mARCCode)
		{
			return MaterialTypeInsertAuto( sqlConnection, sqlTransaction, "BHL", materialTypeName, materialTypeLabel, mARCCode );
		}
		
		/// <summary>
		/// Insert values into dbo.MaterialType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="materialTypeName"></param>
		/// <param name="materialTypeLabel"></param>
		/// <param name="mARCCode"></param>
		/// <returns>Object of type MaterialType.</returns>
		public MaterialType MaterialTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string materialTypeName,
			string materialTypeLabel,
			string mARCCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MaterialTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MaterialTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("MaterialTypeName", SqlDbType.NVarChar, 60, false, materialTypeName),
					CustomSqlHelper.CreateInputParameter("MaterialTypeLabel", SqlDbType.NVarChar, 60, false, materialTypeLabel),
					CustomSqlHelper.CreateInputParameter("MARCCode", SqlDbType.NChar, 1, false, mARCCode), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MaterialType> helper = new CustomSqlHelper<MaterialType>())
				{
					List<MaterialType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MaterialType o = list[0];
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
		/// Insert values into dbo.MaterialType. Returns an object of type MaterialType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MaterialType.</param>
		/// <returns>Object of type MaterialType.</returns>
		public MaterialType MaterialTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MaterialType value)
		{
			return MaterialTypeInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.MaterialType. Returns an object of type MaterialType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MaterialType.</param>
		/// <returns>Object of type MaterialType.</returns>
		public MaterialType MaterialTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MaterialType value)
		{
			return MaterialTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MaterialTypeName,
				value.MaterialTypeLabel,
				value.MARCCode);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.MaterialType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="materialTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MaterialTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int materialTypeID)
		{
			return MaterialTypeDeleteAuto( sqlConnection, sqlTransaction, "BHL", materialTypeID );
		}
		
		/// <summary>
		/// Delete values from dbo.MaterialType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="materialTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MaterialTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int materialTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MaterialTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MaterialTypeID", SqlDbType.Int, null, false, materialTypeID), 
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
		/// Update values in dbo.MaterialType. Returns an object of type MaterialType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="materialTypeID"></param>
		/// <param name="materialTypeName"></param>
		/// <param name="materialTypeLabel"></param>
		/// <param name="mARCCode"></param>
		/// <returns>Object of type MaterialType.</returns>
		public MaterialType MaterialTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int materialTypeID,
			string materialTypeName,
			string materialTypeLabel,
			string mARCCode)
		{
			return MaterialTypeUpdateAuto( sqlConnection, sqlTransaction, "BHL", materialTypeID, materialTypeName, materialTypeLabel, mARCCode);
		}
		
		/// <summary>
		/// Update values in dbo.MaterialType. Returns an object of type MaterialType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="materialTypeID"></param>
		/// <param name="materialTypeName"></param>
		/// <param name="materialTypeLabel"></param>
		/// <param name="mARCCode"></param>
		/// <returns>Object of type MaterialType.</returns>
		public MaterialType MaterialTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int materialTypeID,
			string materialTypeName,
			string materialTypeLabel,
			string mARCCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MaterialTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MaterialTypeID", SqlDbType.Int, null, false, materialTypeID),
					CustomSqlHelper.CreateInputParameter("MaterialTypeName", SqlDbType.NVarChar, 60, false, materialTypeName),
					CustomSqlHelper.CreateInputParameter("MaterialTypeLabel", SqlDbType.NVarChar, 60, false, materialTypeLabel),
					CustomSqlHelper.CreateInputParameter("MARCCode", SqlDbType.NChar, 1, false, mARCCode), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MaterialType> helper = new CustomSqlHelper<MaterialType>())
				{
					List<MaterialType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MaterialType o = list[0];
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
		/// Update values in dbo.MaterialType. Returns an object of type MaterialType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MaterialType.</param>
		/// <returns>Object of type MaterialType.</returns>
		public MaterialType MaterialTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MaterialType value)
		{
			return MaterialTypeUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.MaterialType. Returns an object of type MaterialType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MaterialType.</param>
		/// <returns>Object of type MaterialType.</returns>
		public MaterialType MaterialTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MaterialType value)
		{
			return MaterialTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MaterialTypeID,
				value.MaterialTypeName,
				value.MaterialTypeLabel,
				value.MARCCode);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.MaterialType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MaterialType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MaterialType.</param>
		/// <returns>Object of type CustomDataAccessStatus<MaterialType>.</returns>
		public CustomDataAccessStatus<MaterialType> MaterialTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MaterialType value  )
		{
			return MaterialTypeManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.MaterialType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MaterialType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MaterialType.</param>
		/// <returns>Object of type CustomDataAccessStatus<MaterialType>.</returns>
		public CustomDataAccessStatus<MaterialType> MaterialTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MaterialType value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				MaterialType returnValue = MaterialTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MaterialTypeName,
						value.MaterialTypeLabel,
						value.MARCCode);
				
				return new CustomDataAccessStatus<MaterialType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (MaterialTypeDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MaterialTypeID))
				{
				return new CustomDataAccessStatus<MaterialType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<MaterialType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				MaterialType returnValue = MaterialTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MaterialTypeID,
						value.MaterialTypeName,
						value.MaterialTypeLabel,
						value.MARCCode);
					
				return new CustomDataAccessStatus<MaterialType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<MaterialType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}



// Generated 1/5/2021 3:27:09 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleAssociationTypeDAL is based upon dbo.TitleAssociationType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleAssociationTypeDAL
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
	partial class TitleAssociationTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.TitleAssociationType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociationTypeID"></param>
		/// <returns>Object of type TitleAssociationType.</returns>
		public TitleAssociationType TitleAssociationTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociationTypeID)
		{
			return TitleAssociationTypeSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleAssociationTypeID );
		}
			
		/// <summary>
		/// Select values from dbo.TitleAssociationType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociationTypeID"></param>
		/// <returns>Object of type TitleAssociationType.</returns>
		public TitleAssociationType TitleAssociationTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociationTypeID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociationTypeID", SqlDbType.Int, null, false, titleAssociationTypeID)))
			{
				using (CustomSqlHelper<TitleAssociationType> helper = new CustomSqlHelper<TitleAssociationType>())
				{
					List<TitleAssociationType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociationType o = list[0];
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
		/// Select values from dbo.TitleAssociationType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociationTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleAssociationTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociationTypeID)
		{
			return TitleAssociationTypeSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleAssociationTypeID );
		}
		
		/// <summary>
		/// Select values from dbo.TitleAssociationType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociationTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleAssociationTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociationTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleAssociationTypeID", SqlDbType.Int, null, false, titleAssociationTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.TitleAssociationType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociationName"></param>
		/// <param name="mARCTag"></param>
		/// <param name="mARCIndicator2"></param>
		/// <param name="titleAssociationLabel"></param>
		/// <returns>Object of type TitleAssociationType.</returns>
		public TitleAssociationType TitleAssociationTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string titleAssociationName,
			string mARCTag,
			string mARCIndicator2,
			string titleAssociationLabel)
		{
			return TitleAssociationTypeInsertAuto( sqlConnection, sqlTransaction, "BHL", titleAssociationName, mARCTag, mARCIndicator2, titleAssociationLabel );
		}
		
		/// <summary>
		/// Insert values into dbo.TitleAssociationType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociationName"></param>
		/// <param name="mARCTag"></param>
		/// <param name="mARCIndicator2"></param>
		/// <param name="titleAssociationLabel"></param>
		/// <returns>Object of type TitleAssociationType.</returns>
		public TitleAssociationType TitleAssociationTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string titleAssociationName,
			string mARCTag,
			string mARCIndicator2,
			string titleAssociationLabel)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleAssociationTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleAssociationName", SqlDbType.NVarChar, 60, false, titleAssociationName),
					CustomSqlHelper.CreateInputParameter("MARCTag", SqlDbType.NVarChar, 20, false, mARCTag),
					CustomSqlHelper.CreateInputParameter("MARCIndicator2", SqlDbType.NChar, 1, false, mARCIndicator2),
					CustomSqlHelper.CreateInputParameter("TitleAssociationLabel", SqlDbType.NVarChar, 30, false, titleAssociationLabel), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleAssociationType> helper = new CustomSqlHelper<TitleAssociationType>())
				{
					List<TitleAssociationType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociationType o = list[0];
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
		/// Insert values into dbo.TitleAssociationType. Returns an object of type TitleAssociationType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociationType.</param>
		/// <returns>Object of type TitleAssociationType.</returns>
		public TitleAssociationType TitleAssociationTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociationType value)
		{
			return TitleAssociationTypeInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.TitleAssociationType. Returns an object of type TitleAssociationType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociationType.</param>
		/// <returns>Object of type TitleAssociationType.</returns>
		public TitleAssociationType TitleAssociationTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociationType value)
		{
			return TitleAssociationTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleAssociationName,
				value.MARCTag,
				value.MARCIndicator2,
				value.TitleAssociationLabel);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.TitleAssociationType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociationTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleAssociationTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociationTypeID)
		{
			return TitleAssociationTypeDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleAssociationTypeID );
		}
		
		/// <summary>
		/// Delete values from dbo.TitleAssociationType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociationTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleAssociationTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociationTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociationTypeID", SqlDbType.Int, null, false, titleAssociationTypeID), 
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
		/// Update values in dbo.TitleAssociationType. Returns an object of type TitleAssociationType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociationTypeID"></param>
		/// <param name="titleAssociationName"></param>
		/// <param name="mARCTag"></param>
		/// <param name="mARCIndicator2"></param>
		/// <param name="titleAssociationLabel"></param>
		/// <returns>Object of type TitleAssociationType.</returns>
		public TitleAssociationType TitleAssociationTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociationTypeID,
			string titleAssociationName,
			string mARCTag,
			string mARCIndicator2,
			string titleAssociationLabel)
		{
			return TitleAssociationTypeUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleAssociationTypeID, titleAssociationName, mARCTag, mARCIndicator2, titleAssociationLabel);
		}
		
		/// <summary>
		/// Update values in dbo.TitleAssociationType. Returns an object of type TitleAssociationType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociationTypeID"></param>
		/// <param name="titleAssociationName"></param>
		/// <param name="mARCTag"></param>
		/// <param name="mARCIndicator2"></param>
		/// <param name="titleAssociationLabel"></param>
		/// <returns>Object of type TitleAssociationType.</returns>
		public TitleAssociationType TitleAssociationTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociationTypeID,
			string titleAssociationName,
			string mARCTag,
			string mARCIndicator2,
			string titleAssociationLabel)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociationTypeID", SqlDbType.Int, null, false, titleAssociationTypeID),
					CustomSqlHelper.CreateInputParameter("TitleAssociationName", SqlDbType.NVarChar, 60, false, titleAssociationName),
					CustomSqlHelper.CreateInputParameter("MARCTag", SqlDbType.NVarChar, 20, false, mARCTag),
					CustomSqlHelper.CreateInputParameter("MARCIndicator2", SqlDbType.NChar, 1, false, mARCIndicator2),
					CustomSqlHelper.CreateInputParameter("TitleAssociationLabel", SqlDbType.NVarChar, 30, false, titleAssociationLabel), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleAssociationType> helper = new CustomSqlHelper<TitleAssociationType>())
				{
					List<TitleAssociationType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociationType o = list[0];
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
		/// Update values in dbo.TitleAssociationType. Returns an object of type TitleAssociationType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociationType.</param>
		/// <returns>Object of type TitleAssociationType.</returns>
		public TitleAssociationType TitleAssociationTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociationType value)
		{
			return TitleAssociationTypeUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.TitleAssociationType. Returns an object of type TitleAssociationType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociationType.</param>
		/// <returns>Object of type TitleAssociationType.</returns>
		public TitleAssociationType TitleAssociationTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociationType value)
		{
			return TitleAssociationTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleAssociationTypeID,
				value.TitleAssociationName,
				value.MARCTag,
				value.MARCIndicator2,
				value.TitleAssociationLabel);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.TitleAssociationType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleAssociationType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociationType.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleAssociationType>.</returns>
		public CustomDataAccessStatus<TitleAssociationType> TitleAssociationTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociationType value  )
		{
			return TitleAssociationTypeManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.TitleAssociationType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleAssociationType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociationType.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleAssociationType>.</returns>
		public CustomDataAccessStatus<TitleAssociationType> TitleAssociationTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociationType value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				TitleAssociationType returnValue = TitleAssociationTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAssociationName,
						value.MARCTag,
						value.MARCIndicator2,
						value.TitleAssociationLabel);
				
				return new CustomDataAccessStatus<TitleAssociationType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleAssociationTypeDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAssociationTypeID))
				{
				return new CustomDataAccessStatus<TitleAssociationType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleAssociationType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				TitleAssociationType returnValue = TitleAssociationTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAssociationTypeID,
						value.TitleAssociationName,
						value.MARCTag,
						value.MARCIndicator2,
						value.TitleAssociationLabel);
					
				return new CustomDataAccessStatus<TitleAssociationType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleAssociationType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


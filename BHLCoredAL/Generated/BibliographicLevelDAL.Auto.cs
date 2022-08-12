
// Generated 1/5/2021 3:25:02 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class BibliographicLevelDAL is based upon dbo.BibliographicLevel.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class BibliographicLevelDAL
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
	partial class BibliographicLevelDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.BibliographicLevel by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bibliographicLevelID"></param>
		/// <returns>Object of type BibliographicLevel.</returns>
		public BibliographicLevel BibliographicLevelSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bibliographicLevelID)
		{
			return BibliographicLevelSelectAuto(	sqlConnection, sqlTransaction, "BHL",	bibliographicLevelID );
		}
			
		/// <summary>
		/// Select values from dbo.BibliographicLevel by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bibliographicLevelID"></param>
		/// <returns>Object of type BibliographicLevel.</returns>
		public BibliographicLevel BibliographicLevelSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bibliographicLevelID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BibliographicLevelSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BibliographicLevelID", SqlDbType.Int, null, false, bibliographicLevelID)))
			{
				using (CustomSqlHelper<BibliographicLevel> helper = new CustomSqlHelper<BibliographicLevel>())
				{
					List<BibliographicLevel> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BibliographicLevel o = list[0];
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
		/// Select values from dbo.BibliographicLevel by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bibliographicLevelID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> BibliographicLevelSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bibliographicLevelID)
		{
			return BibliographicLevelSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", bibliographicLevelID );
		}
		
		/// <summary>
		/// Select values from dbo.BibliographicLevel by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bibliographicLevelID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> BibliographicLevelSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bibliographicLevelID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BibliographicLevelSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("BibliographicLevelID", SqlDbType.Int, null, false, bibliographicLevelID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.BibliographicLevel.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bibliographicLevelName"></param>
		/// <param name="bibliographicLevelLabel"></param>
		/// <param name="mARCCode"></param>
		/// <returns>Object of type BibliographicLevel.</returns>
		public BibliographicLevel BibliographicLevelInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string bibliographicLevelName,
			string bibliographicLevelLabel,
			string mARCCode)
		{
			return BibliographicLevelInsertAuto( sqlConnection, sqlTransaction, "BHL", bibliographicLevelName, bibliographicLevelLabel, mARCCode );
		}
		
		/// <summary>
		/// Insert values into dbo.BibliographicLevel.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bibliographicLevelName"></param>
		/// <param name="bibliographicLevelLabel"></param>
		/// <param name="mARCCode"></param>
		/// <returns>Object of type BibliographicLevel.</returns>
		public BibliographicLevel BibliographicLevelInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string bibliographicLevelName,
			string bibliographicLevelLabel,
			string mARCCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BibliographicLevelInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("BibliographicLevelID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("BibliographicLevelName", SqlDbType.NVarChar, 50, false, bibliographicLevelName),
					CustomSqlHelper.CreateInputParameter("BibliographicLevelLabel", SqlDbType.NVarChar, 50, false, bibliographicLevelLabel),
					CustomSqlHelper.CreateInputParameter("MARCCode", SqlDbType.NChar, 1, false, mARCCode), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BibliographicLevel> helper = new CustomSqlHelper<BibliographicLevel>())
				{
					List<BibliographicLevel> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BibliographicLevel o = list[0];
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
		/// Insert values into dbo.BibliographicLevel. Returns an object of type BibliographicLevel.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BibliographicLevel.</param>
		/// <returns>Object of type BibliographicLevel.</returns>
		public BibliographicLevel BibliographicLevelInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BibliographicLevel value)
		{
			return BibliographicLevelInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.BibliographicLevel. Returns an object of type BibliographicLevel.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BibliographicLevel.</param>
		/// <returns>Object of type BibliographicLevel.</returns>
		public BibliographicLevel BibliographicLevelInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BibliographicLevel value)
		{
			return BibliographicLevelInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.BibliographicLevelName,
				value.BibliographicLevelLabel,
				value.MARCCode);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.BibliographicLevel by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bibliographicLevelID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BibliographicLevelDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bibliographicLevelID)
		{
			return BibliographicLevelDeleteAuto( sqlConnection, sqlTransaction, "BHL", bibliographicLevelID );
		}
		
		/// <summary>
		/// Delete values from dbo.BibliographicLevel by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bibliographicLevelID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BibliographicLevelDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bibliographicLevelID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BibliographicLevelDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BibliographicLevelID", SqlDbType.Int, null, false, bibliographicLevelID), 
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
		/// Update values in dbo.BibliographicLevel. Returns an object of type BibliographicLevel.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bibliographicLevelID"></param>
		/// <param name="bibliographicLevelName"></param>
		/// <param name="bibliographicLevelLabel"></param>
		/// <param name="mARCCode"></param>
		/// <returns>Object of type BibliographicLevel.</returns>
		public BibliographicLevel BibliographicLevelUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bibliographicLevelID,
			string bibliographicLevelName,
			string bibliographicLevelLabel,
			string mARCCode)
		{
			return BibliographicLevelUpdateAuto( sqlConnection, sqlTransaction, "BHL", bibliographicLevelID, bibliographicLevelName, bibliographicLevelLabel, mARCCode);
		}
		
		/// <summary>
		/// Update values in dbo.BibliographicLevel. Returns an object of type BibliographicLevel.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bibliographicLevelID"></param>
		/// <param name="bibliographicLevelName"></param>
		/// <param name="bibliographicLevelLabel"></param>
		/// <param name="mARCCode"></param>
		/// <returns>Object of type BibliographicLevel.</returns>
		public BibliographicLevel BibliographicLevelUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bibliographicLevelID,
			string bibliographicLevelName,
			string bibliographicLevelLabel,
			string mARCCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BibliographicLevelUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BibliographicLevelID", SqlDbType.Int, null, false, bibliographicLevelID),
					CustomSqlHelper.CreateInputParameter("BibliographicLevelName", SqlDbType.NVarChar, 50, false, bibliographicLevelName),
					CustomSqlHelper.CreateInputParameter("BibliographicLevelLabel", SqlDbType.NVarChar, 50, false, bibliographicLevelLabel),
					CustomSqlHelper.CreateInputParameter("MARCCode", SqlDbType.NChar, 1, false, mARCCode), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BibliographicLevel> helper = new CustomSqlHelper<BibliographicLevel>())
				{
					List<BibliographicLevel> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BibliographicLevel o = list[0];
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
		/// Update values in dbo.BibliographicLevel. Returns an object of type BibliographicLevel.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BibliographicLevel.</param>
		/// <returns>Object of type BibliographicLevel.</returns>
		public BibliographicLevel BibliographicLevelUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BibliographicLevel value)
		{
			return BibliographicLevelUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.BibliographicLevel. Returns an object of type BibliographicLevel.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BibliographicLevel.</param>
		/// <returns>Object of type BibliographicLevel.</returns>
		public BibliographicLevel BibliographicLevelUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BibliographicLevel value)
		{
			return BibliographicLevelUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.BibliographicLevelID,
				value.BibliographicLevelName,
				value.BibliographicLevelLabel,
				value.MARCCode);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.BibliographicLevel object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.BibliographicLevel.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BibliographicLevel.</param>
		/// <returns>Object of type CustomDataAccessStatus<BibliographicLevel>.</returns>
		public CustomDataAccessStatus<BibliographicLevel> BibliographicLevelManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BibliographicLevel value  )
		{
			return BibliographicLevelManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.BibliographicLevel object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.BibliographicLevel.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BibliographicLevel.</param>
		/// <returns>Object of type CustomDataAccessStatus<BibliographicLevel>.</returns>
		public CustomDataAccessStatus<BibliographicLevel> BibliographicLevelManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BibliographicLevel value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				BibliographicLevel returnValue = BibliographicLevelInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BibliographicLevelName,
						value.BibliographicLevelLabel,
						value.MARCCode);
				
				return new CustomDataAccessStatus<BibliographicLevel>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (BibliographicLevelDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BibliographicLevelID))
				{
				return new CustomDataAccessStatus<BibliographicLevel>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<BibliographicLevel>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				BibliographicLevel returnValue = BibliographicLevelUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BibliographicLevelID,
						value.BibliographicLevelName,
						value.BibliographicLevelLabel,
						value.MARCCode);
					
				return new CustomDataAccessStatus<BibliographicLevel>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<BibliographicLevel>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


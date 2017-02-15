
// Generated 2/15/2017 3:14:49 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class MarcControlDAL is based upon dbo.MarcControl.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class MarcControlDAL
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
	partial class MarcControlDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.MarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcControlID"></param>
		/// <returns>Object of type MarcControl.</returns>
		public MarcControl MarcControlSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcControlID)
		{
			return MarcControlSelectAuto(	sqlConnection, sqlTransaction, "BHL",	marcControlID );
		}
			
		/// <summary>
		/// Select values from dbo.MarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcControlID"></param>
		/// <returns>Object of type MarcControl.</returns>
		public MarcControl MarcControlSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcControlID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcControlSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcControlID", SqlDbType.Int, null, false, marcControlID)))
			{
				using (CustomSqlHelper<MarcControl> helper = new CustomSqlHelper<MarcControl>())
				{
					CustomGenericList<MarcControl> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcControl o = list[0];
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
		/// Select values from dbo.MarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcControlID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> MarcControlSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcControlID)
		{
			return MarcControlSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", marcControlID );
		}
		
		/// <summary>
		/// Select values from dbo.MarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcControlID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> MarcControlSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcControlID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcControlSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("MarcControlID", SqlDbType.Int, null, false, marcControlID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.MarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="value"></param>
		/// <returns>Object of type MarcControl.</returns>
		public MarcControl MarcControlInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID,
			string tag,
			string value)
		{
			return MarcControlInsertAuto( sqlConnection, sqlTransaction, "BHL", marcID, tag, value );
		}
		
		/// <summary>
		/// Insert values into dbo.MarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="value"></param>
		/// <returns>Object of type MarcControl.</returns>
		public MarcControl MarcControlInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcID,
			string tag,
			string value)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcControlInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MarcControlID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID),
					CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NChar, 3, false, tag),
					CustomSqlHelper.CreateInputParameter("Value", SqlDbType.NVarChar, 2000, false, value), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MarcControl> helper = new CustomSqlHelper<MarcControl>())
				{
					CustomGenericList<MarcControl> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcControl o = list[0];
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
		/// Insert values into dbo.MarcControl. Returns an object of type MarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcControl.</param>
		/// <returns>Object of type MarcControl.</returns>
		public MarcControl MarcControlInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcControl value)
		{
			return MarcControlInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.MarcControl. Returns an object of type MarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcControl.</param>
		/// <returns>Object of type MarcControl.</returns>
		public MarcControl MarcControlInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcControl value)
		{
			return MarcControlInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcID,
				value.Tag,
				value.Value);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.MarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcControlID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MarcControlDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcControlID)
		{
			return MarcControlDeleteAuto( sqlConnection, sqlTransaction, "BHL", marcControlID );
		}
		
		/// <summary>
		/// Delete values from dbo.MarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcControlID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MarcControlDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcControlID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcControlDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcControlID", SqlDbType.Int, null, false, marcControlID), 
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
		/// Update values in dbo.MarcControl. Returns an object of type MarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcControlID"></param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="value"></param>
		/// <returns>Object of type MarcControl.</returns>
		public MarcControl MarcControlUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcControlID,
			int marcID,
			string tag,
			string value)
		{
			return MarcControlUpdateAuto( sqlConnection, sqlTransaction, "BHL", marcControlID, marcID, tag, value);
		}
		
		/// <summary>
		/// Update values in dbo.MarcControl. Returns an object of type MarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcControlID"></param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="value"></param>
		/// <returns>Object of type MarcControl.</returns>
		public MarcControl MarcControlUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcControlID,
			int marcID,
			string tag,
			string value)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcControlUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcControlID", SqlDbType.Int, null, false, marcControlID),
					CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID),
					CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NChar, 3, false, tag),
					CustomSqlHelper.CreateInputParameter("Value", SqlDbType.NVarChar, 2000, false, value), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MarcControl> helper = new CustomSqlHelper<MarcControl>())
				{
					CustomGenericList<MarcControl> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcControl o = list[0];
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
		/// Update values in dbo.MarcControl. Returns an object of type MarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcControl.</param>
		/// <returns>Object of type MarcControl.</returns>
		public MarcControl MarcControlUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcControl value)
		{
			return MarcControlUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.MarcControl. Returns an object of type MarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcControl.</param>
		/// <returns>Object of type MarcControl.</returns>
		public MarcControl MarcControlUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcControl value)
		{
			return MarcControlUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcControlID,
				value.MarcID,
				value.Tag,
				value.Value);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.MarcControl object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcControl.</param>
		/// <returns>Object of type CustomDataAccessStatus<MarcControl>.</returns>
		public CustomDataAccessStatus<MarcControl> MarcControlManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcControl value  )
		{
			return MarcControlManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.MarcControl object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcControl.</param>
		/// <returns>Object of type CustomDataAccessStatus<MarcControl>.</returns>
		public CustomDataAccessStatus<MarcControl> MarcControlManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcControl value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				MarcControl returnValue = MarcControlInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcID,
						value.Tag,
						value.Value);
				
				return new CustomDataAccessStatus<MarcControl>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (MarcControlDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcControlID))
				{
				return new CustomDataAccessStatus<MarcControl>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<MarcControl>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				MarcControl returnValue = MarcControlUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcControlID,
						value.MarcID,
						value.Tag,
						value.Value);
					
				return new CustomDataAccessStatus<MarcControl>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<MarcControl>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


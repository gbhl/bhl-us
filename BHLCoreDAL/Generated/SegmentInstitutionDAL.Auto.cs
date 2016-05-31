
// Generated 6/2/2016 9:31:45 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentInstitutionDAL is based upon dbo.SegmentInstitution.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentInstitutionDAL
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
	partial class SegmentInstitutionDAL : ISegmentInstitutionDAL
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.SegmentInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentInstitutionID"></param>
		/// <returns>Object of type SegmentInstitution.</returns>
		public SegmentInstitution SegmentInstitutionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentInstitutionID)
		{
			return SegmentInstitutionSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentInstitutionID );
		}
			
		/// <summary>
		/// Select values from dbo.SegmentInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentInstitutionID"></param>
		/// <returns>Object of type SegmentInstitution.</returns>
		public SegmentInstitution SegmentInstitutionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentInstitutionID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentInstitutionSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentInstitutionID", SqlDbType.Int, null, false, segmentInstitutionID)))
			{
				using (CustomSqlHelper<SegmentInstitution> helper = new CustomSqlHelper<SegmentInstitution>())
				{
					CustomGenericList<SegmentInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentInstitution o = list[0];
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
		/// Select values from dbo.SegmentInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentInstitutionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentInstitutionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentInstitutionID)
		{
			return SegmentInstitutionSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentInstitutionID );
		}
		
		/// <summary>
		/// Select values from dbo.SegmentInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentInstitutionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentInstitutionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentInstitutionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentInstitutionSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentInstitutionID", SqlDbType.Int, null, false, segmentInstitutionID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.SegmentInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentInstitution.</returns>
		public SegmentInstitution SegmentInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			string institutionCode,
			int institutionRoleID,
			int creationUserID,
			int lastModifiedUserID)
		{
			return SegmentInstitutionInsertAuto( sqlConnection, sqlTransaction, "BHL", segmentID, institutionCode, institutionRoleID, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentInstitution.</returns>
		public SegmentInstitution SegmentInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			string institutionCode,
			int institutionRoleID,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentInstitutionInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentInstitutionID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("InstitutionRoleID", SqlDbType.Int, null, false, institutionRoleID),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentInstitution> helper = new CustomSqlHelper<SegmentInstitution>())
				{
					CustomGenericList<SegmentInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentInstitution o = list[0];
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
		/// Insert values into dbo.SegmentInstitution. Returns an object of type SegmentInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentInstitution.</param>
		/// <returns>Object of type SegmentInstitution.</returns>
		public SegmentInstitution SegmentInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentInstitution value)
		{
			return SegmentInstitutionInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentInstitution. Returns an object of type SegmentInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentInstitution.</param>
		/// <returns>Object of type SegmentInstitution.</returns>
		public SegmentInstitution SegmentInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentInstitution value)
		{
			return SegmentInstitutionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.InstitutionCode,
				value.InstitutionRoleID,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.SegmentInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentInstitutionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentInstitutionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentInstitutionID)
		{
			return SegmentInstitutionDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentInstitutionID );
		}
		
		/// <summary>
		/// Delete values from dbo.SegmentInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentInstitutionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentInstitutionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentInstitutionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentInstitutionDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentInstitutionID", SqlDbType.Int, null, false, segmentInstitutionID), 
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
		/// Update values in dbo.SegmentInstitution. Returns an object of type SegmentInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentInstitutionID"></param>
		/// <param name="segmentID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentInstitution.</returns>
		public SegmentInstitution SegmentInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentInstitutionID,
			int segmentID,
			string institutionCode,
			int institutionRoleID,
			int lastModifiedUserID)
		{
			return SegmentInstitutionUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentInstitutionID, segmentID, institutionCode, institutionRoleID, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.SegmentInstitution. Returns an object of type SegmentInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentInstitutionID"></param>
		/// <param name="segmentID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentInstitution.</returns>
		public SegmentInstitution SegmentInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentInstitutionID,
			int segmentID,
			string institutionCode,
			int institutionRoleID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentInstitutionUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentInstitutionID", SqlDbType.Int, null, false, segmentInstitutionID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("InstitutionRoleID", SqlDbType.Int, null, false, institutionRoleID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentInstitution> helper = new CustomSqlHelper<SegmentInstitution>())
				{
					CustomGenericList<SegmentInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentInstitution o = list[0];
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
		/// Update values in dbo.SegmentInstitution. Returns an object of type SegmentInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentInstitution.</param>
		/// <returns>Object of type SegmentInstitution.</returns>
		public SegmentInstitution SegmentInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentInstitution value)
		{
			return SegmentInstitutionUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.SegmentInstitution. Returns an object of type SegmentInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentInstitution.</param>
		/// <returns>Object of type SegmentInstitution.</returns>
		public SegmentInstitution SegmentInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentInstitution value)
		{
			return SegmentInstitutionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentInstitutionID,
				value.SegmentID,
				value.InstitutionCode,
				value.InstitutionRoleID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.SegmentInstitution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentInstitution.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentInstitution>.</returns>
		public CustomDataAccessStatus<SegmentInstitution> SegmentInstitutionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentInstitution value , int userId )
		{
			return SegmentInstitutionManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.SegmentInstitution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentInstitution.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentInstitution>.</returns>
		public CustomDataAccessStatus<SegmentInstitution> SegmentInstitutionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentInstitution value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				SegmentInstitution returnValue = SegmentInstitutionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.InstitutionCode,
						value.InstitutionRoleID,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<SegmentInstitution>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentInstitutionDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentInstitutionID))
				{
				return new CustomDataAccessStatus<SegmentInstitution>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<SegmentInstitution>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				SegmentInstitution returnValue = SegmentInstitutionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentInstitutionID,
						value.SegmentID,
						value.InstitutionCode,
						value.InstitutionRoleID,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<SegmentInstitution>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<SegmentInstitution>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


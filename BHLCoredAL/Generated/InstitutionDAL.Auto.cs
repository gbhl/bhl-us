
// Generated 6/2/2016 9:32:10 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class InstitutionDAL is based upon dbo.Institution.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class InstitutionDAL
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
	partial class InstitutionDAL : IInstitutionDAL
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionCode"></param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionCode)
		{
			return InstitutionSelectAuto(	sqlConnection, sqlTransaction, "BHL",	institutionCode );
		}
			
		/// <summary>
		/// Select values from dbo.Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionCode"></param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string institutionCode )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode)))
			{
				using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
				{
					CustomGenericList<Institution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Institution o = list[0];
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
		/// Select values from dbo.Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionCode"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> InstitutionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionCode)
		{
			return InstitutionSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", institutionCode );
		}
		
		/// <summary>
		/// Select values from dbo.Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionCode"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> InstitutionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string institutionCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionName"></param>
		/// <param name="note"></param>
		/// <param name="institutionUrl"></param>
		/// <param name="bHLMemberLibrary"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return InstitutionInsertAuto( sqlConnection, sqlTransaction, "BHL", institutionCode, institutionName, note, institutionUrl, bHLMemberLibrary, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionName"></param>
		/// <param name="note"></param>
		/// <param name="institutionUrl"></param>
		/// <param name="bHLMemberLibrary"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("InstitutionName", SqlDbType.NVarChar, 255, false, institutionName),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("InstitutionUrl", SqlDbType.NVarChar, 255, true, institutionUrl),
					CustomSqlHelper.CreateInputParameter("BHLMemberLibrary", SqlDbType.Bit, null, false, bHLMemberLibrary),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
				{
					CustomGenericList<Institution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Institution o = list[0];
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
		/// Insert values into dbo.Institution. Returns an object of type Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Institution.</param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Institution value)
		{
			return InstitutionInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Institution. Returns an object of type Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Institution.</param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Institution value)
		{
			return InstitutionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.InstitutionCode,
				value.InstitutionName,
				value.Note,
				value.InstitutionUrl,
				value.BHLMemberLibrary,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionCode"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool InstitutionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionCode)
		{
			return InstitutionDeleteAuto( sqlConnection, sqlTransaction, "BHL", institutionCode );
		}
		
		/// <summary>
		/// Delete values from dbo.Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionCode"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool InstitutionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string institutionCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode), 
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
		/// Update values in dbo.Institution. Returns an object of type Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionName"></param>
		/// <param name="note"></param>
		/// <param name="institutionUrl"></param>
		/// <param name="bHLMemberLibrary"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary,
			int? lastModifiedUserID)
		{
			return InstitutionUpdateAuto( sqlConnection, sqlTransaction, "BHL", institutionCode, institutionName, note, institutionUrl, bHLMemberLibrary, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.Institution. Returns an object of type Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionName"></param>
		/// <param name="note"></param>
		/// <param name="institutionUrl"></param>
		/// <param name="bHLMemberLibrary"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("InstitutionName", SqlDbType.NVarChar, 255, false, institutionName),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("InstitutionUrl", SqlDbType.NVarChar, 255, true, institutionUrl),
					CustomSqlHelper.CreateInputParameter("BHLMemberLibrary", SqlDbType.Bit, null, false, bHLMemberLibrary),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
				{
					CustomGenericList<Institution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Institution o = list[0];
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
		/// Update values in dbo.Institution. Returns an object of type Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Institution.</param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Institution value)
		{
			return InstitutionUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Institution. Returns an object of type Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Institution.</param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Institution value)
		{
			return InstitutionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.InstitutionCode,
				value.InstitutionName,
				value.Note,
				value.InstitutionUrl,
				value.BHLMemberLibrary,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Institution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Institution.</param>
		/// <returns>Object of type CustomDataAccessStatus<Institution>.</returns>
		public CustomDataAccessStatus<Institution> InstitutionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Institution value , int userId )
		{
			return InstitutionManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.Institution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Institution.</param>
		/// <returns>Object of type CustomDataAccessStatus<Institution>.</returns>
		public CustomDataAccessStatus<Institution> InstitutionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Institution value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Institution returnValue = InstitutionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionCode,
						value.InstitutionName,
						value.Note,
						value.InstitutionUrl,
						value.BHLMemberLibrary,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<Institution>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (InstitutionDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionCode))
				{
				return new CustomDataAccessStatus<Institution>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Institution>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				Institution returnValue = InstitutionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionCode,
						value.InstitutionName,
						value.Note,
						value.InstitutionUrl,
						value.BHLMemberLibrary,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<Institution>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Institution>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


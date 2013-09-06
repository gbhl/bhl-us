
// Generated 10/15/2009 4:11:33 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class InstitutionDAL is based upon Institution.

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
	partial class InstitutionDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionCode">Code for Institution providing assistance.</param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionCode)
		{
			return InstitutionSelectAuto(	sqlConnection, sqlTransaction, "BHL",	institutionCode );
		}
			
		/// <summary>
		/// Select values from Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionCode">Code for Institution providing assistance.</param>
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
		/// Select values from Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionCode">Code for Institution providing assistance.</param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> InstitutionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionCode)
		{
			return InstitutionSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", institutionCode );
		}
		
		/// <summary>
		/// Select values from Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionCode">Code for Institution providing assistance.</param>
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
		/// Insert values into Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionCode">Code for Institution providing assistance.</param>
		/// <param name="institutionName">Name for the Institution.</param>
		/// <param name="note">Notes about this Institution.</param>
		/// <param name="institutionUrl"></param>
		/// <param name="bHLMemberLibrary"></param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary)
		{
			return InstitutionInsertAuto( sqlConnection, sqlTransaction, "BHL", institutionCode, institutionName, note, institutionUrl, bHLMemberLibrary );
		}
		
		/// <summary>
		/// Insert values into Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionCode">Code for Institution providing assistance.</param>
		/// <param name="institutionName">Name for the Institution.</param>
		/// <param name="note">Notes about this Institution.</param>
		/// <param name="institutionUrl"></param>
		/// <param name="bHLMemberLibrary"></param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("InstitutionName", SqlDbType.NVarChar, 255, false, institutionName),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("InstitutionUrl", SqlDbType.NVarChar, 255, true, institutionUrl),
					CustomSqlHelper.CreateInputParameter("BHLMemberLibrary", SqlDbType.Bit, null, false, bHLMemberLibrary), 
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
		/// Insert values into Institution. Returns an object of type Institution.
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
		/// Insert values into Institution. Returns an object of type Institution.
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
				value.BHLMemberLibrary);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionCode">Code for Institution providing assistance.</param>
		/// <returns>true if successful otherwise false.</returns>
		public bool InstitutionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionCode)
		{
			return InstitutionDeleteAuto( sqlConnection, sqlTransaction, "BHL", institutionCode );
		}
		
		/// <summary>
		/// Delete values from Institution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionCode">Code for Institution providing assistance.</param>
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
		/// Update values in Institution. Returns an object of type Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionCode">Code for Institution providing assistance.</param>
		/// <param name="institutionName">Name for the Institution.</param>
		/// <param name="note">Notes about this Institution.</param>
		/// <param name="institutionUrl"></param>
		/// <param name="bHLMemberLibrary"></param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary)
		{
			return InstitutionUpdateAuto( sqlConnection, sqlTransaction, "BHL", institutionCode, institutionName, note, institutionUrl, bHLMemberLibrary);
		}
		
		/// <summary>
		/// Update values in Institution. Returns an object of type Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionCode">Code for Institution providing assistance.</param>
		/// <param name="institutionName">Name for the Institution.</param>
		/// <param name="note">Notes about this Institution.</param>
		/// <param name="institutionUrl"></param>
		/// <param name="bHLMemberLibrary"></param>
		/// <returns>Object of type Institution.</returns>
		public Institution InstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("InstitutionName", SqlDbType.NVarChar, 255, false, institutionName),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("InstitutionUrl", SqlDbType.NVarChar, 255, true, institutionUrl),
					CustomSqlHelper.CreateInputParameter("BHLMemberLibrary", SqlDbType.Bit, null, false, bHLMemberLibrary), 
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
		/// Update values in Institution. Returns an object of type Institution.
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
		/// Update values in Institution. Returns an object of type Institution.
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
				value.BHLMemberLibrary);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Institution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Institution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Institution.</param>
		/// <returns>Object of type CustomDataAccessStatus<Institution>.</returns>
		public CustomDataAccessStatus<Institution> InstitutionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Institution value  )
		{
			return InstitutionManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage Institution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Institution.
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
			Institution value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Institution returnValue = InstitutionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionCode,
						value.InstitutionName,
						value.Note,
						value.InstitutionUrl,
						value.BHLMemberLibrary);
				
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
				
				Institution returnValue = InstitutionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionCode,
						value.InstitutionName,
						value.Note,
						value.InstitutionUrl,
						value.BHLMemberLibrary);
					
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
// end of source generation

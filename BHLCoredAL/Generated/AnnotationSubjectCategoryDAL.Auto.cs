
// Generated 5/12/2010 3:45:46 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotationSubjectCategoryDAL is based upon AnnotationSubjectCategory.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotationSubjectCategoryDAL
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
	partial class AnnotationSubjectCategoryDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotationSubjectCategory by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <returns>Object of type AnnotationSubjectCategory.</returns>
		public AnnotationSubjectCategory AnnotationSubjectCategorySelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationSubjectCategoryID)
		{
			return AnnotationSubjectCategorySelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotationSubjectCategoryID );
		}
			
		/// <summary>
		/// Select values from AnnotationSubjectCategory by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <returns>Object of type AnnotationSubjectCategory.</returns>
		public AnnotationSubjectCategory AnnotationSubjectCategorySelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationSubjectCategoryID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectCategorySelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationSubjectCategoryID", SqlDbType.Int, null, false, annotationSubjectCategoryID)))
			{
				using (CustomSqlHelper<AnnotationSubjectCategory> helper = new CustomSqlHelper<AnnotationSubjectCategory>())
				{
					CustomGenericList<AnnotationSubjectCategory> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationSubjectCategory o = list[0];
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
		/// Select values from AnnotationSubjectCategory by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AnnotationSubjectCategorySelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationSubjectCategoryID)
		{
			return AnnotationSubjectCategorySelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotationSubjectCategoryID );
		}
		
		/// <summary>
		/// Select values from AnnotationSubjectCategory by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AnnotationSubjectCategorySelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationSubjectCategoryID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectCategorySelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotationSubjectCategoryID", SqlDbType.Int, null, false, annotationSubjectCategoryID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotationSubjectCategory.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationSourceID"></param>
		/// <param name="subjectCategoryCode"></param>
		/// <param name="subjectCategoryName"></param>
		/// <returns>Object of type AnnotationSubjectCategory.</returns>
		public AnnotationSubjectCategory AnnotationSubjectCategoryInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationSourceID,
			string subjectCategoryCode,
			string subjectCategoryName)
		{
			return AnnotationSubjectCategoryInsertAuto( sqlConnection, sqlTransaction, "BHL", annotationSourceID, subjectCategoryCode, subjectCategoryName );
		}
		
		/// <summary>
		/// Insert values into AnnotationSubjectCategory.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationSourceID"></param>
		/// <param name="subjectCategoryCode"></param>
		/// <param name="subjectCategoryName"></param>
		/// <returns>Object of type AnnotationSubjectCategory.</returns>
		public AnnotationSubjectCategory AnnotationSubjectCategoryInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationSourceID,
			string subjectCategoryCode,
			string subjectCategoryName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectCategoryInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AnnotationSubjectCategoryID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID),
					CustomSqlHelper.CreateInputParameter("SubjectCategoryCode", SqlDbType.NVarChar, 20, false, subjectCategoryCode),
					CustomSqlHelper.CreateInputParameter("SubjectCategoryName", SqlDbType.NVarChar, 50, false, subjectCategoryName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationSubjectCategory> helper = new CustomSqlHelper<AnnotationSubjectCategory>())
				{
					CustomGenericList<AnnotationSubjectCategory> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationSubjectCategory o = list[0];
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
		/// Insert values into AnnotationSubjectCategory. Returns an object of type AnnotationSubjectCategory.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationSubjectCategory.</param>
		/// <returns>Object of type AnnotationSubjectCategory.</returns>
		public AnnotationSubjectCategory AnnotationSubjectCategoryInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationSubjectCategory value)
		{
			return AnnotationSubjectCategoryInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotationSubjectCategory. Returns an object of type AnnotationSubjectCategory.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationSubjectCategory.</param>
		/// <returns>Object of type AnnotationSubjectCategory.</returns>
		public AnnotationSubjectCategory AnnotationSubjectCategoryInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationSubjectCategory value)
		{
			return AnnotationSubjectCategoryInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationSourceID,
				value.SubjectCategoryCode,
				value.SubjectCategoryName);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotationSubjectCategory by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationSubjectCategoryDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationSubjectCategoryID)
		{
			return AnnotationSubjectCategoryDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotationSubjectCategoryID );
		}
		
		/// <summary>
		/// Delete values from AnnotationSubjectCategory by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationSubjectCategoryDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationSubjectCategoryID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectCategoryDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationSubjectCategoryID", SqlDbType.Int, null, false, annotationSubjectCategoryID), 
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
		/// Update values in AnnotationSubjectCategory. Returns an object of type AnnotationSubjectCategory.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="subjectCategoryCode"></param>
		/// <param name="subjectCategoryName"></param>
		/// <returns>Object of type AnnotationSubjectCategory.</returns>
		public AnnotationSubjectCategory AnnotationSubjectCategoryUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationSubjectCategoryID,
			int annotationSourceID,
			string subjectCategoryCode,
			string subjectCategoryName)
		{
			return AnnotationSubjectCategoryUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotationSubjectCategoryID, annotationSourceID, subjectCategoryCode, subjectCategoryName);
		}
		
		/// <summary>
		/// Update values in AnnotationSubjectCategory. Returns an object of type AnnotationSubjectCategory.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="subjectCategoryCode"></param>
		/// <param name="subjectCategoryName"></param>
		/// <returns>Object of type AnnotationSubjectCategory.</returns>
		public AnnotationSubjectCategory AnnotationSubjectCategoryUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationSubjectCategoryID,
			int annotationSourceID,
			string subjectCategoryCode,
			string subjectCategoryName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectCategoryUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationSubjectCategoryID", SqlDbType.Int, null, false, annotationSubjectCategoryID),
					CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID),
					CustomSqlHelper.CreateInputParameter("SubjectCategoryCode", SqlDbType.NVarChar, 20, false, subjectCategoryCode),
					CustomSqlHelper.CreateInputParameter("SubjectCategoryName", SqlDbType.NVarChar, 50, false, subjectCategoryName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationSubjectCategory> helper = new CustomSqlHelper<AnnotationSubjectCategory>())
				{
					CustomGenericList<AnnotationSubjectCategory> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationSubjectCategory o = list[0];
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
		/// Update values in AnnotationSubjectCategory. Returns an object of type AnnotationSubjectCategory.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationSubjectCategory.</param>
		/// <returns>Object of type AnnotationSubjectCategory.</returns>
		public AnnotationSubjectCategory AnnotationSubjectCategoryUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationSubjectCategory value)
		{
			return AnnotationSubjectCategoryUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotationSubjectCategory. Returns an object of type AnnotationSubjectCategory.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationSubjectCategory.</param>
		/// <returns>Object of type AnnotationSubjectCategory.</returns>
		public AnnotationSubjectCategory AnnotationSubjectCategoryUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationSubjectCategory value)
		{
			return AnnotationSubjectCategoryUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationSubjectCategoryID,
				value.AnnotationSourceID,
				value.SubjectCategoryCode,
				value.SubjectCategoryName);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotationSubjectCategory object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationSubjectCategory.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationSubjectCategory.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationSubjectCategory>.</returns>
		public CustomDataAccessStatus<AnnotationSubjectCategory> AnnotationSubjectCategoryManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationSubjectCategory value  )
		{
			return AnnotationSubjectCategoryManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotationSubjectCategory object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationSubjectCategory.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationSubjectCategory.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationSubjectCategory>.</returns>
		public CustomDataAccessStatus<AnnotationSubjectCategory> AnnotationSubjectCategoryManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationSubjectCategory value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotationSubjectCategory returnValue = AnnotationSubjectCategoryInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationSourceID,
						value.SubjectCategoryCode,
						value.SubjectCategoryName);
				
				return new CustomDataAccessStatus<AnnotationSubjectCategory>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotationSubjectCategoryDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationSubjectCategoryID))
				{
				return new CustomDataAccessStatus<AnnotationSubjectCategory>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotationSubjectCategory>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotationSubjectCategory returnValue = AnnotationSubjectCategoryUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationSubjectCategoryID,
						value.AnnotationSourceID,
						value.SubjectCategoryCode,
						value.SubjectCategoryName);
					
				return new CustomDataAccessStatus<AnnotationSubjectCategory>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotationSubjectCategory>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

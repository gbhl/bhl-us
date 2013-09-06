
// Generated 5/11/2010 4:39:26 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotationSubjectDAL is based upon AnnotationSubject.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotationSubjectDAL
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
	partial class AnnotationSubjectDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotationSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationSubjectID"></param>
		/// <returns>Object of type AnnotationSubject.</returns>
		public AnnotationSubject AnnotationSubjectSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationSubjectID)
		{
			return AnnotationSubjectSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotationSubjectID );
		}
			
		/// <summary>
		/// Select values from AnnotationSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationSubjectID"></param>
		/// <returns>Object of type AnnotationSubject.</returns>
		public AnnotationSubject AnnotationSubjectSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationSubjectID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationSubjectID", SqlDbType.Int, null, false, annotationSubjectID)))
			{
				using (CustomSqlHelper<AnnotationSubject> helper = new CustomSqlHelper<AnnotationSubject>())
				{
					CustomGenericList<AnnotationSubject> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationSubject o = list[0];
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
		/// Select values from AnnotationSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationSubjectID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AnnotationSubjectSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationSubjectID)
		{
			return AnnotationSubjectSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotationSubjectID );
		}
		
		/// <summary>
		/// Select values from AnnotationSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationSubjectID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AnnotationSubjectSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationSubjectID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotationSubjectID", SqlDbType.Int, null, false, annotationSubjectID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotationSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <param name="subjectText"></param>
		/// <returns>Object of type AnnotationSubject.</returns>
		public AnnotationSubject AnnotationSubjectInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			int? annotationSubjectCategoryID,
			int annotationKeywordTargetID,
			string subjectText)
		{
			return AnnotationSubjectInsertAuto( sqlConnection, sqlTransaction, "BHL", annotationID, annotationSubjectCategoryID, annotationKeywordTargetID, subjectText );
		}
		
		/// <summary>
		/// Insert values into AnnotationSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <param name="subjectText"></param>
		/// <returns>Object of type AnnotationSubject.</returns>
		public AnnotationSubject AnnotationSubjectInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			int? annotationSubjectCategoryID,
			int annotationKeywordTargetID,
			string subjectText)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AnnotationSubjectID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("AnnotationSubjectCategoryID", SqlDbType.Int, null, true, annotationSubjectCategoryID),
					CustomSqlHelper.CreateInputParameter("AnnotationKeywordTargetID", SqlDbType.Int, null, false, annotationKeywordTargetID),
					CustomSqlHelper.CreateInputParameter("SubjectText", SqlDbType.NVarChar, 150, false, subjectText), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationSubject> helper = new CustomSqlHelper<AnnotationSubject>())
				{
					CustomGenericList<AnnotationSubject> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationSubject o = list[0];
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
		/// Insert values into AnnotationSubject. Returns an object of type AnnotationSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationSubject.</param>
		/// <returns>Object of type AnnotationSubject.</returns>
		public AnnotationSubject AnnotationSubjectInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationSubject value)
		{
			return AnnotationSubjectInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotationSubject. Returns an object of type AnnotationSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationSubject.</param>
		/// <returns>Object of type AnnotationSubject.</returns>
		public AnnotationSubject AnnotationSubjectInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationSubject value)
		{
			return AnnotationSubjectInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationID,
				value.AnnotationSubjectCategoryID,
				value.AnnotationKeywordTargetID,
				value.SubjectText);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotationSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationSubjectID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationSubjectDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationSubjectID)
		{
			return AnnotationSubjectDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotationSubjectID );
		}
		
		/// <summary>
		/// Delete values from AnnotationSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationSubjectID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationSubjectDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationSubjectID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationSubjectID", SqlDbType.Int, null, false, annotationSubjectID), 
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
		/// Update values in AnnotationSubject. Returns an object of type AnnotationSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationSubjectID"></param>
		/// <param name="annotationID"></param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <param name="subjectText"></param>
		/// <returns>Object of type AnnotationSubject.</returns>
		public AnnotationSubject AnnotationSubjectUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationSubjectID,
			int annotationID,
			int? annotationSubjectCategoryID,
			int annotationKeywordTargetID,
			string subjectText)
		{
			return AnnotationSubjectUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotationSubjectID, annotationID, annotationSubjectCategoryID, annotationKeywordTargetID, subjectText);
		}
		
		/// <summary>
		/// Update values in AnnotationSubject. Returns an object of type AnnotationSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationSubjectID"></param>
		/// <param name="annotationID"></param>
		/// <param name="annotationSubjectCategoryID"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <param name="subjectText"></param>
		/// <returns>Object of type AnnotationSubject.</returns>
		public AnnotationSubject AnnotationSubjectUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationSubjectID,
			int annotationID,
			int? annotationSubjectCategoryID,
			int annotationKeywordTargetID,
			string subjectText)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationSubjectID", SqlDbType.Int, null, false, annotationSubjectID),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("AnnotationSubjectCategoryID", SqlDbType.Int, null, true, annotationSubjectCategoryID),
					CustomSqlHelper.CreateInputParameter("AnnotationKeywordTargetID", SqlDbType.Int, null, false, annotationKeywordTargetID),
					CustomSqlHelper.CreateInputParameter("SubjectText", SqlDbType.NVarChar, 150, false, subjectText), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationSubject> helper = new CustomSqlHelper<AnnotationSubject>())
				{
					CustomGenericList<AnnotationSubject> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationSubject o = list[0];
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
		/// Update values in AnnotationSubject. Returns an object of type AnnotationSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationSubject.</param>
		/// <returns>Object of type AnnotationSubject.</returns>
		public AnnotationSubject AnnotationSubjectUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationSubject value)
		{
			return AnnotationSubjectUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotationSubject. Returns an object of type AnnotationSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationSubject.</param>
		/// <returns>Object of type AnnotationSubject.</returns>
		public AnnotationSubject AnnotationSubjectUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationSubject value)
		{
			return AnnotationSubjectUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationSubjectID,
				value.AnnotationID,
				value.AnnotationSubjectCategoryID,
				value.AnnotationKeywordTargetID,
				value.SubjectText);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotationSubject object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationSubject.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationSubject>.</returns>
		public CustomDataAccessStatus<AnnotationSubject> AnnotationSubjectManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationSubject value  )
		{
			return AnnotationSubjectManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotationSubject object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationSubject.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationSubject>.</returns>
		public CustomDataAccessStatus<AnnotationSubject> AnnotationSubjectManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationSubject value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotationSubject returnValue = AnnotationSubjectInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID,
						value.AnnotationSubjectCategoryID,
						value.AnnotationKeywordTargetID,
						value.SubjectText);
				
				return new CustomDataAccessStatus<AnnotationSubject>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotationSubjectDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationSubjectID))
				{
				return new CustomDataAccessStatus<AnnotationSubject>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotationSubject>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotationSubject returnValue = AnnotationSubjectUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationSubjectID,
						value.AnnotationID,
						value.AnnotationSubjectCategoryID,
						value.AnnotationKeywordTargetID,
						value.SubjectText);
					
				return new CustomDataAccessStatus<AnnotationSubject>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotationSubject>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

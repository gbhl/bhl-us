
// Generated 1/7/2011 3:13:11 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotationConceptDAL is based upon AnnotationConcept.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotationConceptDAL
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
	partial class AnnotationConceptDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationConceptCode"></param>
		/// <returns>Object of type AnnotationConcept.</returns>
		public AnnotationConcept AnnotationConceptSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string annotationConceptCode)
		{
			return AnnotationConceptSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotationConceptCode );
		}
			
		/// <summary>
		/// Select values from AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationConceptCode"></param>
		/// <returns>Object of type AnnotationConcept.</returns>
		public AnnotationConcept AnnotationConceptSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string annotationConceptCode )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationConceptSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode)))
			{
				using (CustomSqlHelper<AnnotationConcept> helper = new CustomSqlHelper<AnnotationConcept>())
				{
					List<AnnotationConcept> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationConcept o = list[0];
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
		/// Select values from AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationConceptCode"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationConceptSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string annotationConceptCode)
		{
			return AnnotationConceptSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotationConceptCode );
		}
		
		/// <summary>
		/// Select values from AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationConceptCode"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationConceptSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string annotationConceptCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationConceptSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="conceptText"></param>
		/// <param name="parentConceptCode"></param>
		/// <returns>Object of type AnnotationConcept.</returns>
		public AnnotationConcept AnnotationConceptInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string annotationConceptCode,
			int annotationSourceID,
			string conceptText,
			string parentConceptCode)
		{
			return AnnotationConceptInsertAuto( sqlConnection, sqlTransaction, "BHL", annotationConceptCode, annotationSourceID, conceptText, parentConceptCode );
		}
		
		/// <summary>
		/// Insert values into AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="conceptText"></param>
		/// <param name="parentConceptCode"></param>
		/// <returns>Object of type AnnotationConcept.</returns>
		public AnnotationConcept AnnotationConceptInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string annotationConceptCode,
			int annotationSourceID,
			string conceptText,
			string parentConceptCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationConceptInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode),
					CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID),
					CustomSqlHelper.CreateInputParameter("ConceptText", SqlDbType.NVarChar, 100, false, conceptText),
					CustomSqlHelper.CreateInputParameter("ParentConceptCode", SqlDbType.NVarChar, 20, true, parentConceptCode), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationConcept> helper = new CustomSqlHelper<AnnotationConcept>())
				{
					List<AnnotationConcept> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationConcept o = list[0];
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
		/// Insert values into AnnotationConcept. Returns an object of type AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationConcept.</param>
		/// <returns>Object of type AnnotationConcept.</returns>
		public AnnotationConcept AnnotationConceptInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationConcept value)
		{
			return AnnotationConceptInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotationConcept. Returns an object of type AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationConcept.</param>
		/// <returns>Object of type AnnotationConcept.</returns>
		public AnnotationConcept AnnotationConceptInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationConcept value)
		{
			return AnnotationConceptInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationConceptCode,
				value.AnnotationSourceID,
				value.ConceptText,
				value.ParentConceptCode);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationConceptCode"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationConceptDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string annotationConceptCode)
		{
			return AnnotationConceptDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotationConceptCode );
		}
		
		/// <summary>
		/// Delete values from AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationConceptCode"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationConceptDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string annotationConceptCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationConceptDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode), 
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
		/// Update values in AnnotationConcept. Returns an object of type AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="conceptText"></param>
		/// <param name="parentConceptCode"></param>
		/// <returns>Object of type AnnotationConcept.</returns>
		public AnnotationConcept AnnotationConceptUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string annotationConceptCode,
			int annotationSourceID,
			string conceptText,
			string parentConceptCode)
		{
			return AnnotationConceptUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotationConceptCode, annotationSourceID, conceptText, parentConceptCode);
		}
		
		/// <summary>
		/// Update values in AnnotationConcept. Returns an object of type AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="conceptText"></param>
		/// <param name="parentConceptCode"></param>
		/// <returns>Object of type AnnotationConcept.</returns>
		public AnnotationConcept AnnotationConceptUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string annotationConceptCode,
			int annotationSourceID,
			string conceptText,
			string parentConceptCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationConceptUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode),
					CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID),
					CustomSqlHelper.CreateInputParameter("ConceptText", SqlDbType.NVarChar, 100, false, conceptText),
					CustomSqlHelper.CreateInputParameter("ParentConceptCode", SqlDbType.NVarChar, 20, true, parentConceptCode), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationConcept> helper = new CustomSqlHelper<AnnotationConcept>())
				{
					List<AnnotationConcept> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationConcept o = list[0];
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
		/// Update values in AnnotationConcept. Returns an object of type AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationConcept.</param>
		/// <returns>Object of type AnnotationConcept.</returns>
		public AnnotationConcept AnnotationConceptUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationConcept value)
		{
			return AnnotationConceptUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotationConcept. Returns an object of type AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationConcept.</param>
		/// <returns>Object of type AnnotationConcept.</returns>
		public AnnotationConcept AnnotationConceptUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationConcept value)
		{
			return AnnotationConceptUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationConceptCode,
				value.AnnotationSourceID,
				value.ConceptText,
				value.ParentConceptCode);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotationConcept object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationConcept.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationConcept>.</returns>
		public CustomDataAccessStatus<AnnotationConcept> AnnotationConceptManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationConcept value  )
		{
			return AnnotationConceptManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotationConcept object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationConcept.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationConcept>.</returns>
		public CustomDataAccessStatus<AnnotationConcept> AnnotationConceptManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationConcept value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotationConcept returnValue = AnnotationConceptInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationConceptCode,
						value.AnnotationSourceID,
						value.ConceptText,
						value.ParentConceptCode);
				
				return new CustomDataAccessStatus<AnnotationConcept>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotationConceptDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationConceptCode))
				{
				return new CustomDataAccessStatus<AnnotationConcept>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotationConcept>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotationConcept returnValue = AnnotationConceptUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationConceptCode,
						value.AnnotationSourceID,
						value.ConceptText,
						value.ParentConceptCode);
					
				return new CustomDataAccessStatus<AnnotationConcept>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotationConcept>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

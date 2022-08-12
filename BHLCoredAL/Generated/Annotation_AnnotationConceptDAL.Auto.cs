
// Generated 5/12/2010 1:57:10 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class Annotation_AnnotationConceptDAL is based upon Annotation_AnnotationConcept.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class Annotation_AnnotationConceptDAL
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
	partial class Annotation_AnnotationConceptDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from Annotation_AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <returns>Object of type Annotation_AnnotationConcept.</returns>
		public Annotation_AnnotationConcept Annotation_AnnotationConceptSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string annotationConceptCode,
			int annotationKeywordTargetID)
		{
			return Annotation_AnnotationConceptSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotationID, annotationConceptCode, annotationKeywordTargetID );
		}
			
		/// <summary>
		/// Select values from Annotation_AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <returns>Object of type Annotation_AnnotationConcept.</returns>
		public Annotation_AnnotationConcept Annotation_AnnotationConceptSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string annotationConceptCode,
			int annotationKeywordTargetID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.Annotation_AnnotationConceptSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode),
					CustomSqlHelper.CreateInputParameter("AnnotationKeywordTargetID", SqlDbType.Int, null, false, annotationKeywordTargetID)))
			{
				using (CustomSqlHelper<Annotation_AnnotationConcept> helper = new CustomSqlHelper<Annotation_AnnotationConcept>())
				{
					List<Annotation_AnnotationConcept> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Annotation_AnnotationConcept o = list[0];
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
		/// Select values from Annotation_AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> Annotation_AnnotationConceptSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string annotationConceptCode,
			int annotationKeywordTargetID)
		{
			return Annotation_AnnotationConceptSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotationID, annotationConceptCode, annotationKeywordTargetID );
		}
		
		/// <summary>
		/// Select values from Annotation_AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> Annotation_AnnotationConceptSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string annotationConceptCode,
			int annotationKeywordTargetID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.Annotation_AnnotationConceptSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode),
					CustomSqlHelper.CreateInputParameter("AnnotationKeywordTargetID", SqlDbType.Int, null, false, annotationKeywordTargetID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into Annotation_AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <returns>Object of type Annotation_AnnotationConcept.</returns>
		public Annotation_AnnotationConcept Annotation_AnnotationConceptInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string annotationConceptCode,
			int annotationKeywordTargetID)
		{
			return Annotation_AnnotationConceptInsertAuto( sqlConnection, sqlTransaction, "BHL", annotationID, annotationConceptCode, annotationKeywordTargetID );
		}
		
		/// <summary>
		/// Insert values into Annotation_AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <returns>Object of type Annotation_AnnotationConcept.</returns>
		public Annotation_AnnotationConcept Annotation_AnnotationConceptInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string annotationConceptCode,
			int annotationKeywordTargetID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.Annotation_AnnotationConceptInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode),
					CustomSqlHelper.CreateInputParameter("AnnotationKeywordTargetID", SqlDbType.Int, null, false, annotationKeywordTargetID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Annotation_AnnotationConcept> helper = new CustomSqlHelper<Annotation_AnnotationConcept>())
				{
					List<Annotation_AnnotationConcept> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Annotation_AnnotationConcept o = list[0];
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
		/// Insert values into Annotation_AnnotationConcept. Returns an object of type Annotation_AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Annotation_AnnotationConcept.</param>
		/// <returns>Object of type Annotation_AnnotationConcept.</returns>
		public Annotation_AnnotationConcept Annotation_AnnotationConceptInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Annotation_AnnotationConcept value)
		{
			return Annotation_AnnotationConceptInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into Annotation_AnnotationConcept. Returns an object of type Annotation_AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Annotation_AnnotationConcept.</param>
		/// <returns>Object of type Annotation_AnnotationConcept.</returns>
		public Annotation_AnnotationConcept Annotation_AnnotationConceptInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Annotation_AnnotationConcept value)
		{
			return Annotation_AnnotationConceptInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationID,
				value.AnnotationConceptCode,
				value.AnnotationKeywordTargetID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Annotation_AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool Annotation_AnnotationConceptDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string annotationConceptCode,
			int annotationKeywordTargetID)
		{
			return Annotation_AnnotationConceptDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotationID, annotationConceptCode, annotationKeywordTargetID );
		}
		
		/// <summary>
		/// Delete values from Annotation_AnnotationConcept by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool Annotation_AnnotationConceptDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string annotationConceptCode,
			int annotationKeywordTargetID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.Annotation_AnnotationConceptDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode),
					CustomSqlHelper.CreateInputParameter("AnnotationKeywordTargetID", SqlDbType.Int, null, false, annotationKeywordTargetID), 
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
		/// Update values in Annotation_AnnotationConcept. Returns an object of type Annotation_AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <returns>Object of type Annotation_AnnotationConcept.</returns>
		public Annotation_AnnotationConcept Annotation_AnnotationConceptUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string annotationConceptCode,
			int annotationKeywordTargetID)
		{
			return Annotation_AnnotationConceptUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotationID, annotationConceptCode, annotationKeywordTargetID);
		}
		
		/// <summary>
		/// Update values in Annotation_AnnotationConcept. Returns an object of type Annotation_AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationConceptCode"></param>
		/// <param name="annotationKeywordTargetID"></param>
		/// <returns>Object of type Annotation_AnnotationConcept.</returns>
		public Annotation_AnnotationConcept Annotation_AnnotationConceptUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string annotationConceptCode,
			int annotationKeywordTargetID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.Annotation_AnnotationConceptUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode),
					CustomSqlHelper.CreateInputParameter("AnnotationKeywordTargetID", SqlDbType.Int, null, false, annotationKeywordTargetID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Annotation_AnnotationConcept> helper = new CustomSqlHelper<Annotation_AnnotationConcept>())
				{
					List<Annotation_AnnotationConcept> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Annotation_AnnotationConcept o = list[0];
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
		/// Update values in Annotation_AnnotationConcept. Returns an object of type Annotation_AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Annotation_AnnotationConcept.</param>
		/// <returns>Object of type Annotation_AnnotationConcept.</returns>
		public Annotation_AnnotationConcept Annotation_AnnotationConceptUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Annotation_AnnotationConcept value)
		{
			return Annotation_AnnotationConceptUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in Annotation_AnnotationConcept. Returns an object of type Annotation_AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Annotation_AnnotationConcept.</param>
		/// <returns>Object of type Annotation_AnnotationConcept.</returns>
		public Annotation_AnnotationConcept Annotation_AnnotationConceptUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Annotation_AnnotationConcept value)
		{
			return Annotation_AnnotationConceptUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationID,
				value.AnnotationConceptCode,
				value.AnnotationKeywordTargetID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Annotation_AnnotationConcept object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Annotation_AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Annotation_AnnotationConcept.</param>
		/// <returns>Object of type CustomDataAccessStatus<Annotation_AnnotationConcept>.</returns>
		public CustomDataAccessStatus<Annotation_AnnotationConcept> Annotation_AnnotationConceptManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Annotation_AnnotationConcept value  )
		{
			return Annotation_AnnotationConceptManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage Annotation_AnnotationConcept object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Annotation_AnnotationConcept.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Annotation_AnnotationConcept.</param>
		/// <returns>Object of type CustomDataAccessStatus<Annotation_AnnotationConcept>.</returns>
		public CustomDataAccessStatus<Annotation_AnnotationConcept> Annotation_AnnotationConceptManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Annotation_AnnotationConcept value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Annotation_AnnotationConcept returnValue = Annotation_AnnotationConceptInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID,
						value.AnnotationConceptCode,
						value.AnnotationKeywordTargetID);
				
				return new CustomDataAccessStatus<Annotation_AnnotationConcept>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (Annotation_AnnotationConceptDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID,
						value.AnnotationConceptCode,
						value.AnnotationKeywordTargetID))
				{
				return new CustomDataAccessStatus<Annotation_AnnotationConcept>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Annotation_AnnotationConcept>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Annotation_AnnotationConcept returnValue = Annotation_AnnotationConceptUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID,
						value.AnnotationConceptCode,
						value.AnnotationKeywordTargetID);
					
				return new CustomDataAccessStatus<Annotation_AnnotationConcept>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Annotation_AnnotationConcept>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation


// Generated 6/15/2010 1:29:40 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotationRelationDAL is based upon AnnotationRelation.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotationRelationDAL
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
	partial class AnnotationRelationDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotationRelation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="relatedExternalIdentifier"></param>
		/// <returns>Object of type AnnotationRelation.</returns>
		public AnnotationRelation AnnotationRelationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string relatedExternalIdentifier)
		{
			return AnnotationRelationSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotationID, relatedExternalIdentifier );
		}
			
		/// <summary>
		/// Select values from AnnotationRelation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="relatedExternalIdentifier"></param>
		/// <returns>Object of type AnnotationRelation.</returns>
		public AnnotationRelation AnnotationRelationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string relatedExternalIdentifier )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationRelationSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("RelatedExternalIdentifier", SqlDbType.NVarChar, 50, false, relatedExternalIdentifier)))
			{
				using (CustomSqlHelper<AnnotationRelation> helper = new CustomSqlHelper<AnnotationRelation>())
				{
					List<AnnotationRelation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationRelation o = list[0];
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
		/// Select values from AnnotationRelation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="relatedExternalIdentifier"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationRelationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string relatedExternalIdentifier)
		{
			return AnnotationRelationSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotationID, relatedExternalIdentifier );
		}
		
		/// <summary>
		/// Select values from AnnotationRelation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="relatedExternalIdentifier"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationRelationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string relatedExternalIdentifier)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationRelationSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("RelatedExternalIdentifier", SqlDbType.NVarChar, 50, false, relatedExternalIdentifier)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotationRelation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="relatedExternalIdentifier"></param>
		/// <param name="note"></param>
		/// <returns>Object of type AnnotationRelation.</returns>
		public AnnotationRelation AnnotationRelationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string relatedExternalIdentifier,
			string note)
		{
			return AnnotationRelationInsertAuto( sqlConnection, sqlTransaction, "BHL", annotationID, relatedExternalIdentifier, note );
		}
		
		/// <summary>
		/// Insert values into AnnotationRelation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="relatedExternalIdentifier"></param>
		/// <param name="note"></param>
		/// <returns>Object of type AnnotationRelation.</returns>
		public AnnotationRelation AnnotationRelationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string relatedExternalIdentifier,
			string note)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationRelationInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("RelatedExternalIdentifier", SqlDbType.NVarChar, 50, false, relatedExternalIdentifier),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 1073741823, false, note), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationRelation> helper = new CustomSqlHelper<AnnotationRelation>())
				{
					List<AnnotationRelation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationRelation o = list[0];
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
		/// Insert values into AnnotationRelation. Returns an object of type AnnotationRelation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationRelation.</param>
		/// <returns>Object of type AnnotationRelation.</returns>
		public AnnotationRelation AnnotationRelationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationRelation value)
		{
			return AnnotationRelationInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotationRelation. Returns an object of type AnnotationRelation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationRelation.</param>
		/// <returns>Object of type AnnotationRelation.</returns>
		public AnnotationRelation AnnotationRelationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationRelation value)
		{
			return AnnotationRelationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationID,
				value.RelatedExternalIdentifier,
				value.Note);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotationRelation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="relatedExternalIdentifier"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationRelationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string relatedExternalIdentifier)
		{
			return AnnotationRelationDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotationID, relatedExternalIdentifier );
		}
		
		/// <summary>
		/// Delete values from AnnotationRelation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="relatedExternalIdentifier"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationRelationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string relatedExternalIdentifier)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationRelationDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("RelatedExternalIdentifier", SqlDbType.NVarChar, 50, false, relatedExternalIdentifier), 
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
		/// Update values in AnnotationRelation. Returns an object of type AnnotationRelation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="relatedExternalIdentifier"></param>
		/// <param name="note"></param>
		/// <returns>Object of type AnnotationRelation.</returns>
		public AnnotationRelation AnnotationRelationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string relatedExternalIdentifier,
			string note)
		{
			return AnnotationRelationUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotationID, relatedExternalIdentifier, note);
		}
		
		/// <summary>
		/// Update values in AnnotationRelation. Returns an object of type AnnotationRelation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="relatedExternalIdentifier"></param>
		/// <param name="note"></param>
		/// <returns>Object of type AnnotationRelation.</returns>
		public AnnotationRelation AnnotationRelationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string relatedExternalIdentifier,
			string note)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationRelationUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("RelatedExternalIdentifier", SqlDbType.NVarChar, 50, false, relatedExternalIdentifier),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 1073741823, false, note), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationRelation> helper = new CustomSqlHelper<AnnotationRelation>())
				{
					List<AnnotationRelation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationRelation o = list[0];
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
		/// Update values in AnnotationRelation. Returns an object of type AnnotationRelation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationRelation.</param>
		/// <returns>Object of type AnnotationRelation.</returns>
		public AnnotationRelation AnnotationRelationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationRelation value)
		{
			return AnnotationRelationUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotationRelation. Returns an object of type AnnotationRelation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationRelation.</param>
		/// <returns>Object of type AnnotationRelation.</returns>
		public AnnotationRelation AnnotationRelationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationRelation value)
		{
			return AnnotationRelationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationID,
				value.RelatedExternalIdentifier,
				value.Note);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotationRelation object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationRelation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationRelation.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationRelation>.</returns>
		public CustomDataAccessStatus<AnnotationRelation> AnnotationRelationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationRelation value  )
		{
			return AnnotationRelationManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotationRelation object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationRelation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationRelation.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationRelation>.</returns>
		public CustomDataAccessStatus<AnnotationRelation> AnnotationRelationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationRelation value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotationRelation returnValue = AnnotationRelationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID,
						value.RelatedExternalIdentifier,
						value.Note);
				
				return new CustomDataAccessStatus<AnnotationRelation>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotationRelationDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID,
						value.RelatedExternalIdentifier))
				{
				return new CustomDataAccessStatus<AnnotationRelation>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotationRelation>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotationRelation returnValue = AnnotationRelationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID,
						value.RelatedExternalIdentifier,
						value.Note);
					
				return new CustomDataAccessStatus<AnnotationRelation>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotationRelation>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

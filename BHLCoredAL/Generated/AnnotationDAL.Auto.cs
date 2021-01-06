
// Generated 12/15/2010 3:05:49 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotationDAL is based upon Annotation.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotationDAL
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
	partial class AnnotationDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from Annotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <returns>Object of type Annotation.</returns>
		public Annotation AnnotationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID)
		{
			return AnnotationSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotationID );
		}
			
		/// <summary>
		/// Select values from Annotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <returns>Object of type Annotation.</returns>
		public Annotation AnnotationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID)))
			{
				using (CustomSqlHelper<Annotation> helper = new CustomSqlHelper<Annotation>())
				{
					List<Annotation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Annotation o = list[0];
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
		/// Select values from Annotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID)
		{
			return AnnotationSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotationID );
		}
		
		/// <summary>
		/// Select values from Annotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into Annotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationSourceID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="sequenceNumber"></param>
		/// <param name="annotationTextDescription"></param>
		/// <param name="annotationText"></param>
		/// <param name="annotationTextClean"></param>
		/// <param name="annotationTextDisplay"></param>
		/// <param name="annotationTextCorrected"></param>
		/// <param name="comment"></param>
		/// <returns>Object of type Annotation.</returns>
		public Annotation AnnotationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationSourceID,
			string externalIdentifier,
			int sequenceNumber,
			string annotationTextDescription,
			string annotationText,
			string annotationTextClean,
			string annotationTextDisplay,
			string annotationTextCorrected,
			string comment)
		{
			return AnnotationInsertAuto( sqlConnection, sqlTransaction, "BHL", annotationSourceID, externalIdentifier, sequenceNumber, annotationTextDescription, annotationText, annotationTextClean, annotationTextDisplay, annotationTextCorrected, comment );
		}
		
		/// <summary>
		/// Insert values into Annotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationSourceID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="sequenceNumber"></param>
		/// <param name="annotationTextDescription"></param>
		/// <param name="annotationText"></param>
		/// <param name="annotationTextClean"></param>
		/// <param name="annotationTextDisplay"></param>
		/// <param name="annotationTextCorrected"></param>
		/// <param name="comment"></param>
		/// <returns>Object of type Annotation.</returns>
		public Annotation AnnotationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationSourceID,
			string externalIdentifier,
			int sequenceNumber,
			string annotationTextDescription,
			string annotationText,
			string annotationTextClean,
			string annotationTextDisplay,
			string annotationTextCorrected,
			string comment)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AnnotationID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID),
					CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
					CustomSqlHelper.CreateInputParameter("SequenceNumber", SqlDbType.Int, null, false, sequenceNumber),
					CustomSqlHelper.CreateInputParameter("AnnotationTextDescription", SqlDbType.NVarChar, 1073741823, false, annotationTextDescription),
					CustomSqlHelper.CreateInputParameter("AnnotationText", SqlDbType.NVarChar, 1073741823, false, annotationText),
					CustomSqlHelper.CreateInputParameter("AnnotationTextClean", SqlDbType.NVarChar, 1073741823, false, annotationTextClean),
					CustomSqlHelper.CreateInputParameter("AnnotationTextDisplay", SqlDbType.NVarChar, 1073741823, false, annotationTextDisplay),
					CustomSqlHelper.CreateInputParameter("AnnotationTextCorrected", SqlDbType.NVarChar, 1073741823, false, annotationTextCorrected),
					CustomSqlHelper.CreateInputParameter("Comment", SqlDbType.NVarChar, 1073741823, false, comment), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Annotation> helper = new CustomSqlHelper<Annotation>())
				{
					List<Annotation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Annotation o = list[0];
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
		/// Insert values into Annotation. Returns an object of type Annotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Annotation.</param>
		/// <returns>Object of type Annotation.</returns>
		public Annotation AnnotationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Annotation value)
		{
			return AnnotationInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into Annotation. Returns an object of type Annotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Annotation.</param>
		/// <returns>Object of type Annotation.</returns>
		public Annotation AnnotationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Annotation value)
		{
			return AnnotationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationSourceID,
				value.ExternalIdentifier,
				value.SequenceNumber,
				value.AnnotationTextDescription,
				value.AnnotationText,
				value.AnnotationTextClean,
				value.AnnotationTextDisplay,
				value.AnnotationTextCorrected,
				value.Comment);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Annotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID)
		{
			return AnnotationDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotationID );
		}
		
		/// <summary>
		/// Delete values from Annotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID), 
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
		/// Update values in Annotation. Returns an object of type Annotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="sequenceNumber"></param>
		/// <param name="annotationTextDescription"></param>
		/// <param name="annotationText"></param>
		/// <param name="annotationTextClean"></param>
		/// <param name="annotationTextDisplay"></param>
		/// <param name="annotationTextCorrected"></param>
		/// <param name="comment"></param>
		/// <returns>Object of type Annotation.</returns>
		public Annotation AnnotationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			int annotationSourceID,
			string externalIdentifier,
			int sequenceNumber,
			string annotationTextDescription,
			string annotationText,
			string annotationTextClean,
			string annotationTextDisplay,
			string annotationTextCorrected,
			string comment)
		{
			return AnnotationUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotationID, annotationSourceID, externalIdentifier, sequenceNumber, annotationTextDescription, annotationText, annotationTextClean, annotationTextDisplay, annotationTextCorrected, comment);
		}
		
		/// <summary>
		/// Update values in Annotation. Returns an object of type Annotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="sequenceNumber"></param>
		/// <param name="annotationTextDescription"></param>
		/// <param name="annotationText"></param>
		/// <param name="annotationTextClean"></param>
		/// <param name="annotationTextDisplay"></param>
		/// <param name="annotationTextCorrected"></param>
		/// <param name="comment"></param>
		/// <returns>Object of type Annotation.</returns>
		public Annotation AnnotationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			int annotationSourceID,
			string externalIdentifier,
			int sequenceNumber,
			string annotationTextDescription,
			string annotationText,
			string annotationTextClean,
			string annotationTextDisplay,
			string annotationTextCorrected,
			string comment)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID),
					CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
					CustomSqlHelper.CreateInputParameter("SequenceNumber", SqlDbType.Int, null, false, sequenceNumber),
					CustomSqlHelper.CreateInputParameter("AnnotationTextDescription", SqlDbType.NVarChar, 1073741823, false, annotationTextDescription),
					CustomSqlHelper.CreateInputParameter("AnnotationText", SqlDbType.NVarChar, 1073741823, false, annotationText),
					CustomSqlHelper.CreateInputParameter("AnnotationTextClean", SqlDbType.NVarChar, 1073741823, false, annotationTextClean),
					CustomSqlHelper.CreateInputParameter("AnnotationTextDisplay", SqlDbType.NVarChar, 1073741823, false, annotationTextDisplay),
					CustomSqlHelper.CreateInputParameter("AnnotationTextCorrected", SqlDbType.NVarChar, 1073741823, false, annotationTextCorrected),
					CustomSqlHelper.CreateInputParameter("Comment", SqlDbType.NVarChar, 1073741823, false, comment), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Annotation> helper = new CustomSqlHelper<Annotation>())
				{
					List<Annotation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Annotation o = list[0];
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
		/// Update values in Annotation. Returns an object of type Annotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Annotation.</param>
		/// <returns>Object of type Annotation.</returns>
		public Annotation AnnotationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Annotation value)
		{
			return AnnotationUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in Annotation. Returns an object of type Annotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Annotation.</param>
		/// <returns>Object of type Annotation.</returns>
		public Annotation AnnotationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Annotation value)
		{
			return AnnotationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationID,
				value.AnnotationSourceID,
				value.ExternalIdentifier,
				value.SequenceNumber,
				value.AnnotationTextDescription,
				value.AnnotationText,
				value.AnnotationTextClean,
				value.AnnotationTextDisplay,
				value.AnnotationTextCorrected,
				value.Comment);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Annotation object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Annotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Annotation.</param>
		/// <returns>Object of type CustomDataAccessStatus<Annotation>.</returns>
		public CustomDataAccessStatus<Annotation> AnnotationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Annotation value  )
		{
			return AnnotationManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage Annotation object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Annotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Annotation.</param>
		/// <returns>Object of type CustomDataAccessStatus<Annotation>.</returns>
		public CustomDataAccessStatus<Annotation> AnnotationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Annotation value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Annotation returnValue = AnnotationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationSourceID,
						value.ExternalIdentifier,
						value.SequenceNumber,
						value.AnnotationTextDescription,
						value.AnnotationText,
						value.AnnotationTextClean,
						value.AnnotationTextDisplay,
						value.AnnotationTextCorrected,
						value.Comment);
				
				return new CustomDataAccessStatus<Annotation>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotationDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID))
				{
				return new CustomDataAccessStatus<Annotation>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Annotation>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Annotation returnValue = AnnotationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID,
						value.AnnotationSourceID,
						value.ExternalIdentifier,
						value.SequenceNumber,
						value.AnnotationTextDescription,
						value.AnnotationText,
						value.AnnotationTextClean,
						value.AnnotationTextDisplay,
						value.AnnotationTextCorrected,
						value.Comment);
					
				return new CustomDataAccessStatus<Annotation>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Annotation>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

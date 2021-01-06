
// Generated 12/15/2010 3:05:49 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotationNoteDAL is based upon AnnotationNote.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotationNoteDAL
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
	partial class AnnotationNoteDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotationNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationNoteID"></param>
		/// <returns>Object of type AnnotationNote.</returns>
		public AnnotationNote AnnotationNoteSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationNoteID)
		{
			return AnnotationNoteSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotationNoteID );
		}
			
		/// <summary>
		/// Select values from AnnotationNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationNoteID"></param>
		/// <returns>Object of type AnnotationNote.</returns>
		public AnnotationNote AnnotationNoteSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationNoteID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationNoteSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationNoteID", SqlDbType.Int, null, false, annotationNoteID)))
			{
				using (CustomSqlHelper<AnnotationNote> helper = new CustomSqlHelper<AnnotationNote>())
				{
					List<AnnotationNote> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationNote o = list[0];
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
		/// Select values from AnnotationNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationNoteID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationNoteSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationNoteID)
		{
			return AnnotationNoteSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotationNoteID );
		}
		
		/// <summary>
		/// Select values from AnnotationNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationNoteID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationNoteSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationNoteID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationNoteSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotationNoteID", SqlDbType.Int, null, false, annotationNoteID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotationNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="noteText"></param>
		/// <param name="noteTextClean"></param>
		/// <param name="noteTextDisplay"></param>
		/// <param name="isAlternate"></param>
		/// <returns>Object of type AnnotationNote.</returns>
		public AnnotationNote AnnotationNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string noteText,
			string noteTextClean,
			string noteTextDisplay,
			byte isAlternate)
		{
			return AnnotationNoteInsertAuto( sqlConnection, sqlTransaction, "BHL", annotationID, noteText, noteTextClean, noteTextDisplay, isAlternate );
		}
		
		/// <summary>
		/// Insert values into AnnotationNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="noteText"></param>
		/// <param name="noteTextClean"></param>
		/// <param name="noteTextDisplay"></param>
		/// <param name="isAlternate"></param>
		/// <returns>Object of type AnnotationNote.</returns>
		public AnnotationNote AnnotationNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string noteText,
			string noteTextClean,
			string noteTextDisplay,
			byte isAlternate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationNoteInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AnnotationNoteID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("NoteText", SqlDbType.NVarChar, 1073741823, false, noteText),
					CustomSqlHelper.CreateInputParameter("NoteTextClean", SqlDbType.NVarChar, 1073741823, false, noteTextClean),
					CustomSqlHelper.CreateInputParameter("NoteTextDisplay", SqlDbType.NVarChar, 1073741823, false, noteTextDisplay),
					CustomSqlHelper.CreateInputParameter("IsAlternate", SqlDbType.TinyInt, null, false, isAlternate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationNote> helper = new CustomSqlHelper<AnnotationNote>())
				{
					List<AnnotationNote> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationNote o = list[0];
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
		/// Insert values into AnnotationNote. Returns an object of type AnnotationNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationNote.</param>
		/// <returns>Object of type AnnotationNote.</returns>
		public AnnotationNote AnnotationNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationNote value)
		{
			return AnnotationNoteInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotationNote. Returns an object of type AnnotationNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationNote.</param>
		/// <returns>Object of type AnnotationNote.</returns>
		public AnnotationNote AnnotationNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationNote value)
		{
			return AnnotationNoteInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationID,
				value.NoteText,
				value.NoteTextClean,
				value.NoteTextDisplay,
				value.IsAlternate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotationNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationNoteID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationNoteDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationNoteID)
		{
			return AnnotationNoteDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotationNoteID );
		}
		
		/// <summary>
		/// Delete values from AnnotationNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationNoteID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationNoteDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationNoteID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationNoteDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationNoteID", SqlDbType.Int, null, false, annotationNoteID), 
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
		/// Update values in AnnotationNote. Returns an object of type AnnotationNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationNoteID"></param>
		/// <param name="annotationID"></param>
		/// <param name="noteText"></param>
		/// <param name="noteTextClean"></param>
		/// <param name="noteTextDisplay"></param>
		/// <param name="isAlternate"></param>
		/// <returns>Object of type AnnotationNote.</returns>
		public AnnotationNote AnnotationNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationNoteID,
			int annotationID,
			string noteText,
			string noteTextClean,
			string noteTextDisplay,
			byte isAlternate)
		{
			return AnnotationNoteUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotationNoteID, annotationID, noteText, noteTextClean, noteTextDisplay, isAlternate);
		}
		
		/// <summary>
		/// Update values in AnnotationNote. Returns an object of type AnnotationNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationNoteID"></param>
		/// <param name="annotationID"></param>
		/// <param name="noteText"></param>
		/// <param name="noteTextClean"></param>
		/// <param name="noteTextDisplay"></param>
		/// <param name="isAlternate"></param>
		/// <returns>Object of type AnnotationNote.</returns>
		public AnnotationNote AnnotationNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationNoteID,
			int annotationID,
			string noteText,
			string noteTextClean,
			string noteTextDisplay,
			byte isAlternate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationNoteUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationNoteID", SqlDbType.Int, null, false, annotationNoteID),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("NoteText", SqlDbType.NVarChar, 1073741823, false, noteText),
					CustomSqlHelper.CreateInputParameter("NoteTextClean", SqlDbType.NVarChar, 1073741823, false, noteTextClean),
					CustomSqlHelper.CreateInputParameter("NoteTextDisplay", SqlDbType.NVarChar, 1073741823, false, noteTextDisplay),
					CustomSqlHelper.CreateInputParameter("IsAlternate", SqlDbType.TinyInt, null, false, isAlternate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationNote> helper = new CustomSqlHelper<AnnotationNote>())
				{
					List<AnnotationNote> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationNote o = list[0];
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
		/// Update values in AnnotationNote. Returns an object of type AnnotationNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationNote.</param>
		/// <returns>Object of type AnnotationNote.</returns>
		public AnnotationNote AnnotationNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationNote value)
		{
			return AnnotationNoteUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotationNote. Returns an object of type AnnotationNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationNote.</param>
		/// <returns>Object of type AnnotationNote.</returns>
		public AnnotationNote AnnotationNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationNote value)
		{
			return AnnotationNoteUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationNoteID,
				value.AnnotationID,
				value.NoteText,
				value.NoteTextClean,
				value.NoteTextDisplay,
				value.IsAlternate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotationNote object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationNote.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationNote>.</returns>
		public CustomDataAccessStatus<AnnotationNote> AnnotationNoteManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationNote value  )
		{
			return AnnotationNoteManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotationNote object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationNote.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationNote>.</returns>
		public CustomDataAccessStatus<AnnotationNote> AnnotationNoteManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationNote value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotationNote returnValue = AnnotationNoteInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID,
						value.NoteText,
						value.NoteTextClean,
						value.NoteTextDisplay,
						value.IsAlternate);
				
				return new CustomDataAccessStatus<AnnotationNote>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotationNoteDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationNoteID))
				{
				return new CustomDataAccessStatus<AnnotationNote>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotationNote>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotationNote returnValue = AnnotationNoteUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationNoteID,
						value.AnnotationID,
						value.NoteText,
						value.NoteTextClean,
						value.NoteTextDisplay,
						value.IsAlternate);
					
				return new CustomDataAccessStatus<AnnotationNote>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotationNote>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

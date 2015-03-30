
// Generated 2/27/2015 2:20:32 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleNoteDAL is based upon TitleNote.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleNoteDAL
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
	partial class TitleNoteDAL : ITitleNoteDAL
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from TitleNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleNoteID"></param>
		/// <returns>Object of type TitleNote.</returns>
		public TitleNote TitleNoteSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleNoteID)
		{
			return TitleNoteSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleNoteID );
		}
			
		/// <summary>
		/// Select values from TitleNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleNoteID"></param>
		/// <returns>Object of type TitleNote.</returns>
		public TitleNote TitleNoteSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleNoteID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleNoteSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleNoteID", SqlDbType.Int, null, false, titleNoteID)))
			{
				using (CustomSqlHelper<TitleNote> helper = new CustomSqlHelper<TitleNote>())
				{
					CustomGenericList<TitleNote> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleNote o = list[0];
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
		/// Select values from TitleNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleNoteID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleNoteSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleNoteID)
		{
			return TitleNoteSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleNoteID );
		}
		
		/// <summary>
		/// Select values from TitleNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleNoteID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleNoteSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleNoteID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleNoteSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleNoteID", SqlDbType.Int, null, false, titleNoteID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into TitleNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="noteTypeID"></param>
		/// <param name="noteText"></param>
		/// <param name="noteSequence"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type TitleNote.</returns>
		public TitleNote TitleNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			int? noteTypeID,
			string noteText,
			short? noteSequence,
			int? creationUserID)
		{
			return TitleNoteInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, noteTypeID, noteText, noteSequence, creationUserID );
		}
		
		/// <summary>
		/// Insert values into TitleNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="noteTypeID"></param>
		/// <param name="noteText"></param>
		/// <param name="noteSequence"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type TitleNote.</returns>
		public TitleNote TitleNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			int? noteTypeID,
			string noteText,
			short? noteSequence,
			int? creationUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleNoteInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleNoteID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("NoteTypeID", SqlDbType.Int, null, true, noteTypeID),
					CustomSqlHelper.CreateInputParameter("NoteText", SqlDbType.NVarChar, 1073741823, false, noteText),
					CustomSqlHelper.CreateInputParameter("NoteSequence", SqlDbType.SmallInt, null, true, noteSequence),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleNote> helper = new CustomSqlHelper<TitleNote>())
				{
					CustomGenericList<TitleNote> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleNote o = list[0];
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
		/// Insert values into TitleNote. Returns an object of type TitleNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleNote.</param>
		/// <returns>Object of type TitleNote.</returns>
		public TitleNote TitleNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleNote value)
		{
			return TitleNoteInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into TitleNote. Returns an object of type TitleNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleNote.</param>
		/// <returns>Object of type TitleNote.</returns>
		public TitleNote TitleNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleNote value)
		{
			return TitleNoteInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.NoteTypeID,
				value.NoteText,
				value.NoteSequence,
				value.CreationUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from TitleNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleNoteID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleNoteDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleNoteID)
		{
			return TitleNoteDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleNoteID );
		}
		
		/// <summary>
		/// Delete values from TitleNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleNoteID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleNoteDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleNoteID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleNoteDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleNoteID", SqlDbType.Int, null, false, titleNoteID), 
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
		/// Update values in TitleNote. Returns an object of type TitleNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleNoteID"></param>
		/// <param name="titleID"></param>
		/// <param name="noteTypeID"></param>
		/// <param name="noteText"></param>
		/// <param name="noteSequence"></param>
		/// <returns>Object of type TitleNote.</returns>
		public TitleNote TitleNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleNoteID,
			int titleID,
			int? noteTypeID,
			string noteText,
			short? noteSequence)
		{
			return TitleNoteUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleNoteID, titleID, noteTypeID, noteText, noteSequence);
		}
		
		/// <summary>
		/// Update values in TitleNote. Returns an object of type TitleNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleNoteID"></param>
		/// <param name="titleID"></param>
		/// <param name="noteTypeID"></param>
		/// <param name="noteText"></param>
		/// <param name="noteSequence"></param>
		/// <returns>Object of type TitleNote.</returns>
		public TitleNote TitleNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleNoteID,
			int titleID,
			int? noteTypeID,
			string noteText,
			short? noteSequence)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleNoteUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleNoteID", SqlDbType.Int, null, false, titleNoteID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("NoteTypeID", SqlDbType.Int, null, true, noteTypeID),
					CustomSqlHelper.CreateInputParameter("NoteText", SqlDbType.NVarChar, 1073741823, false, noteText),
					CustomSqlHelper.CreateInputParameter("NoteSequence", SqlDbType.SmallInt, null, true, noteSequence), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleNote> helper = new CustomSqlHelper<TitleNote>())
				{
					CustomGenericList<TitleNote> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleNote o = list[0];
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
		/// Update values in TitleNote. Returns an object of type TitleNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleNote.</param>
		/// <returns>Object of type TitleNote.</returns>
		public TitleNote TitleNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleNote value)
		{
			return TitleNoteUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in TitleNote. Returns an object of type TitleNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleNote.</param>
		/// <returns>Object of type TitleNote.</returns>
		public TitleNote TitleNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleNote value)
		{
			return TitleNoteUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleNoteID,
				value.TitleID,
				value.NoteTypeID,
				value.NoteText,
				value.NoteSequence);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage TitleNote object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleNote.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleNote>.</returns>
		public CustomDataAccessStatus<TitleNote> TitleNoteManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleNote value , int userId )
		{
			return TitleNoteManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage TitleNote object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleNote.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleNote>.</returns>
		public CustomDataAccessStatus<TitleNote> TitleNoteManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleNote value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				
				TitleNote returnValue = TitleNoteInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.NoteTypeID,
						value.NoteText,
						value.NoteSequence,
						value.CreationUserID);
				
				return new CustomDataAccessStatus<TitleNote>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleNoteDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleNoteID))
				{
				return new CustomDataAccessStatus<TitleNote>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleNote>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				TitleNote returnValue = TitleNoteUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleNoteID,
						value.TitleID,
						value.NoteTypeID,
						value.NoteText,
						value.NoteSequence);
					
				return new CustomDataAccessStatus<TitleNote>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleNote>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

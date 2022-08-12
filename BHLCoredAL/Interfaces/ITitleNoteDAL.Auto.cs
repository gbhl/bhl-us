	
// Generated 2/27/2015 2:20:32 PM
// Do not modify the contents of this code file.
// Interface ITitleNoteDAL based upon TitleNote.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface ITitleNoteDAL
	{
		TitleNote TitleNoteSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleNoteID);

		List<CustomDataRow> TitleNoteSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleNoteID);

		TitleNote TitleNoteInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleID,
			int? noteTypeID,
			string noteText,
			short? noteSequence,
			int? creationUserID);

		TitleNote TitleNoteInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, TitleNote value);

		bool TitleNoteDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleNoteID);

		TitleNote TitleNoteUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleNoteID,
			int titleID,
			int? noteTypeID,
			string noteText,
			short? noteSequence);

		TitleNote TitleNoteUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, TitleNote value);

		
	}
}
// end of source generation

	
// Generated 2/27/2015 2:20:32 PM
// Do not modify the contents of this code file.
// Interface INoteTypeDAL based upon NoteType.

#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface INoteTypeDAL
	{
		NoteType NoteTypeSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int noteTypeID);

		CustomGenericList<CustomDataRow> NoteTypeSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int noteTypeID);

		NoteType NoteTypeInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			string noteTypeName,
			string noteTypeDisplay,
			string marcDataFieldTag,
			string marcIndicator1,
			int? creationUserID,
			int? lastModifiedUserID);

		NoteType NoteTypeInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, NoteType value);

		bool NoteTypeDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int noteTypeID);

		NoteType NoteTypeUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int noteTypeID,
			string noteTypeName,
			string noteTypeDisplay,
			string marcDataFieldTag,
			string marcIndicator1,
			int? lastModifiedUserID);

		NoteType NoteTypeUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, NoteType value);

		
	}
}
// end of source generation

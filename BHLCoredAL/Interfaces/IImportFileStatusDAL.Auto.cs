	
// Generated 1/10/2014 11:05:49 AM
// Do not modify the contents of this code file.
// Interface IImportFileStatusDAL based upon ImportFileStatus.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IImportFileStatusDAL
	{
		ImportFileStatus ImportFileStatusSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileStatusID);

		List<CustomDataRow> ImportFileStatusSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileStatusID);

		ImportFileStatus ImportFileStatusInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID);

		ImportFileStatus ImportFileStatusInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportFileStatus value);

		bool ImportFileStatusDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileStatusID);

		ImportFileStatus ImportFileStatusUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID);

		ImportFileStatus ImportFileStatusUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportFileStatus value);
	}
}
// end of source generation

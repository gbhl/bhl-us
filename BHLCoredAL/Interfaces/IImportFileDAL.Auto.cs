	
// Generated 1/10/2014 11:05:49 AM
// Do not modify the contents of this code file.
// Interface IImportFileDAL based upon ImportFile.

#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IImportFileDAL
	{
		ImportFile ImportFileSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileID);

		CustomGenericList<CustomDataRow> ImportFileSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileID);

		ImportFile ImportFileInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileStatusID,
			string importFileName,
			string contributorCode,
			int creationUserID,
			int lastModifiedUserID);

		ImportFile ImportFileInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportFile value);

		bool ImportFileDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileID);

        void ImportFileDeleteByImportFileID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int importFileID);

		ImportFile ImportFileUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileID,
			int importFileStatusID,
			string importFileName,
			string contributorCode,
			int lastModifiedUserID);

		ImportFile ImportFileUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportFile value);
	}
}
// end of source generation

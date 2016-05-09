
// Generated 5/9/2016 1:52:44 PM
// Do not modify the contents of this code file.
// Interface IMarcDAL based upon dbo.Marc.

#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IMarcDAL
	{
		Marc MarcSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int marcID);

		Marc MarcSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int marcID);

		CustomGenericList<CustomDataRow> MarcSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int marcID);

		CustomGenericList<CustomDataRow> MarcSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int marcID);

		Marc MarcInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int marcImportStatusID,
			int marcImportBatchID,
			string marcFileLocation,
			string institutionCode,
			string leader,
			int? titleID);

		Marc MarcInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int marcImportStatusID,
			int marcImportBatchID,
			string marcFileLocation,
			string institutionCode,
			string leader,
			int? titleID);

		Marc MarcInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Marc value);

		Marc MarcInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Marc value);

		bool MarcDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int marcID);

		bool MarcDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int marcID);

		Marc MarcUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int marcID,
			int marcImportStatusID,
			int marcImportBatchID,
			string marcFileLocation,
			string institutionCode,
			string leader,
			int? titleID);

		Marc MarcUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int marcID,
			int marcImportStatusID,
			int marcImportBatchID,
			string marcFileLocation,
			string institutionCode,
			string leader,
			int? titleID);

		Marc MarcUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Marc value);

		Marc MarcUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Marc value);

		CustomDataAccessStatus<Marc> MarcManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Marc value);

		CustomDataAccessStatus<Marc> MarcManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Marc value);


	}
}


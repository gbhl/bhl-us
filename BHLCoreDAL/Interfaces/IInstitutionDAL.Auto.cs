
// Generated 6/2/2016 9:32:10 AM
// Do not modify the contents of this code file.
// Interface IInstitutionDAL based upon dbo.Institution.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IInstitutionDAL
	{
		Institution InstitutionSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			string institutionCode);

		Institution InstitutionSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			string institutionCode);

		List<CustomDataRow> InstitutionSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			string institutionCode);

		List<CustomDataRow> InstitutionSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			string institutionCode);

		Institution InstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary,
			int? creationUserID,
			int? lastModifiedUserID);

		Institution InstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary,
			int? creationUserID,
			int? lastModifiedUserID);

		Institution InstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Institution value);

		Institution InstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Institution value);

		bool InstitutionDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			string institutionCode);

		bool InstitutionDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			string institutionCode);

		Institution InstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary,
			int? lastModifiedUserID);

		Institution InstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			string institutionCode,
			string institutionName,
			string note,
			string institutionUrl,
			bool bHLMemberLibrary,
			int? lastModifiedUserID);

		Institution InstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Institution value);

		Institution InstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Institution value);

		CustomDataAccessStatus<Institution> InstitutionManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Institution value, int userId);

		CustomDataAccessStatus<Institution> InstitutionManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Institution value, int userId);


	}
}


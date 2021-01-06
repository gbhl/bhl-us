
// Generated 6/2/2016 9:31:15 AM
// Do not modify the contents of this code file.
// Interface IInstitutionRoleDAL based upon dbo.InstitutionRole.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IInstitutionRoleDAL
	{
		InstitutionRole InstitutionRoleSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int institutionRoleID);

		InstitutionRole InstitutionRoleSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int institutionRoleID);

		List<CustomDataRow> InstitutionRoleSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int institutionRoleID);

		List<CustomDataRow> InstitutionRoleSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int institutionRoleID);

		InstitutionRole InstitutionRoleInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			string institutionRoleName,
			string institutionRoleLabel,
			int creationUserID,
			int lastModifiedUserID);

		InstitutionRole InstitutionRoleInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			string institutionRoleName,
			string institutionRoleLabel,
			int creationUserID,
			int lastModifiedUserID);

		InstitutionRole InstitutionRoleInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, InstitutionRole value);

		InstitutionRole InstitutionRoleInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, InstitutionRole value);

		bool InstitutionRoleDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int institutionRoleID);

		bool InstitutionRoleDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int institutionRoleID);

		InstitutionRole InstitutionRoleUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int institutionRoleID,
			string institutionRoleName,
			string institutionRoleLabel,
			int lastModifiedUserID);

		InstitutionRole InstitutionRoleUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int institutionRoleID,
			string institutionRoleName,
			string institutionRoleLabel,
			int lastModifiedUserID);

		InstitutionRole InstitutionRoleUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, InstitutionRole value);

		InstitutionRole InstitutionRoleUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, InstitutionRole value);

		CustomDataAccessStatus<InstitutionRole> InstitutionRoleManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, InstitutionRole value, int userId);

		CustomDataAccessStatus<InstitutionRole> InstitutionRoleManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, InstitutionRole value, int userId);


	}
}



// Generated 6/2/2016 9:31:45 AM
// Do not modify the contents of this code file.
// Interface ISegmentInstitutionDAL based upon dbo.SegmentInstitution.

#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface ISegmentInstitutionDAL
	{
		SegmentInstitution SegmentInstitutionSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int segmentInstitutionID);

		SegmentInstitution SegmentInstitutionSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int segmentInstitutionID);

		CustomGenericList<CustomDataRow> SegmentInstitutionSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int segmentInstitutionID);

		CustomGenericList<CustomDataRow> SegmentInstitutionSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int segmentInstitutionID);

		SegmentInstitution SegmentInstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int segmentID,
			string institutionCode,
			int institutionRoleID,
			int creationUserID,
			int lastModifiedUserID);

		SegmentInstitution SegmentInstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int segmentID,
			string institutionCode,
			int institutionRoleID,
			int creationUserID,
			int lastModifiedUserID);

		SegmentInstitution SegmentInstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SegmentInstitution value);

		SegmentInstitution SegmentInstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, SegmentInstitution value);

		bool SegmentInstitutionDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int segmentInstitutionID);

		bool SegmentInstitutionDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int segmentInstitutionID);

		SegmentInstitution SegmentInstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int segmentInstitutionID,
			int segmentID,
			string institutionCode,
			int institutionRoleID,
			int lastModifiedUserID);

		SegmentInstitution SegmentInstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int segmentInstitutionID,
			int segmentID,
			string institutionCode,
			int institutionRoleID,
			int lastModifiedUserID);

		SegmentInstitution SegmentInstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SegmentInstitution value);

		SegmentInstitution SegmentInstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, SegmentInstitution value);

		CustomDataAccessStatus<SegmentInstitution> SegmentInstitutionManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SegmentInstitution value, int userId);

		CustomDataAccessStatus<SegmentInstitution> SegmentInstitutionManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, SegmentInstitution value, int userId);


	}
}


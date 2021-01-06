
// Generated 6/2/2016 9:31:31 AM
// Do not modify the contents of this code file.
// Interface IItemInstitutionDAL based upon dbo.ItemInstitution.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IItemInstitutionDAL
	{
		ItemInstitution ItemInstitutionSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemInstitutionID);

		ItemInstitution ItemInstitutionSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemInstitutionID);

		List<CustomDataRow> ItemInstitutionSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemInstitutionID);

		List<CustomDataRow> ItemInstitutionSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemInstitutionID);

		ItemInstitution ItemInstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemID,
			string institutionCode,
			int institutionRoleID,
			int creationUserID,
			int lastModifiedUserID);

		ItemInstitution ItemInstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemID,
			string institutionCode,
			int institutionRoleID,
			int creationUserID,
			int lastModifiedUserID);

		ItemInstitution ItemInstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ItemInstitution value);

		ItemInstitution ItemInstitutionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ItemInstitution value);

		bool ItemInstitutionDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemInstitutionID);

		bool ItemInstitutionDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemInstitutionID);

		ItemInstitution ItemInstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemInstitutionID,
			int itemID,
			string institutionCode,
			int institutionRoleID,
			int lastModifiedUserID);

		ItemInstitution ItemInstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemInstitutionID,
			int itemID,
			string institutionCode,
			int institutionRoleID,
			int lastModifiedUserID);

		ItemInstitution ItemInstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ItemInstitution value);

		ItemInstitution ItemInstitutionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ItemInstitution value);

		CustomDataAccessStatus<ItemInstitution> ItemInstitutionManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ItemInstitution value, int userId);

		CustomDataAccessStatus<ItemInstitution> ItemInstitutionManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ItemInstitution value, int userId);


	}
}


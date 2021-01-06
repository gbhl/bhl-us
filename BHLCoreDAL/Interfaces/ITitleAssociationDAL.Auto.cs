
// Generated 5/9/2016 1:54:17 PM
// Do not modify the contents of this code file.
// Interface ITitleAssociationDAL based upon dbo.TitleAssociation.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface ITitleAssociationDAL
	{
		TitleAssociation TitleAssociationSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleAssociationID);

		TitleAssociation TitleAssociationSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleAssociationID);

		List<CustomDataRow> TitleAssociationSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleAssociationID);

		List<CustomDataRow> TitleAssociationSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleAssociationID);

		TitleAssociation TitleAssociationInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleID,
			int titleAssociationTypeID,
			string title,
			string section,
			string volume,
			bool active,
			int? associatedTitleID,
			string heading,
			string publication,
			string relationship,
			int? creationUserID,
			int? lastModifiedUserID);

		TitleAssociation TitleAssociationInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleID,
			int titleAssociationTypeID,
			string title,
			string section,
			string volume,
			bool active,
			int? associatedTitleID,
			string heading,
			string publication,
			string relationship,
			int? creationUserID,
			int? lastModifiedUserID);

		TitleAssociation TitleAssociationInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, TitleAssociation value);

		TitleAssociation TitleAssociationInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, TitleAssociation value);

		bool TitleAssociationDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleAssociationID);

		bool TitleAssociationDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleAssociationID);

		TitleAssociation TitleAssociationUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleAssociationID,
			int titleID,
			int titleAssociationTypeID,
			string title,
			string section,
			string volume,
			bool active,
			int? associatedTitleID,
			string heading,
			string publication,
			string relationship,
			int? lastModifiedUserID);

		TitleAssociation TitleAssociationUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleAssociationID,
			int titleID,
			int titleAssociationTypeID,
			string title,
			string section,
			string volume,
			bool active,
			int? associatedTitleID,
			string heading,
			string publication,
			string relationship,
			int? lastModifiedUserID);

		TitleAssociation TitleAssociationUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, TitleAssociation value);

		TitleAssociation TitleAssociationUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, TitleAssociation value);

		CustomDataAccessStatus<TitleAssociation> TitleAssociationManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, TitleAssociation value, int userId);

		CustomDataAccessStatus<TitleAssociation> TitleAssociationManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, TitleAssociation value, int userId);


	}
}


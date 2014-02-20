
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class ImportFileDAL
	{
        public void ImportFileDeleteByImportFileID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int importFileID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileDeleteByImportFileID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ImportFileID", SqlDbType.Int, null, false, importFileID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void ImportFilePublishToProduction(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int importFileID,
            int userID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFilePublishToProduction", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ImportFileID", SqlDbType.Int, null, false, importFileID),
                CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void ImportFileRejectFile(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int importFileID, int userID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileRejectFile", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ImportFileID", SqlDbType.Int, null, false, importFileID),
                CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public CustomGenericList<ImportFile> ImportFileSelectDetails(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string institutionCode,
            int fileStatusID, int numberOfDays)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileSelectDetails", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ContributorCode", SqlDbType.NVarChar, 10, false, institutionCode),
                CustomSqlHelper.CreateInputParameter("FileStatusID", SqlDbType.Int, null, false, fileStatusID),
                CustomSqlHelper.CreateInputParameter("NumDays", SqlDbType.Int, null, false, numberOfDays)))
            {
                using (CustomSqlHelper<ImportFile> helper = new CustomSqlHelper<ImportFile>())
                {
                    CustomGenericList<ImportFile> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public ImportFile ImportFileSelectByFileName(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            string importFileName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileSelectByFileName", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ImportFileName", SqlDbType.NVarChar, 200, false, importFileName)))
            {
                using (CustomSqlHelper<ImportFile> helper = new CustomSqlHelper<ImportFile>())
                {
                    CustomGenericList<ImportFile> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

	}
}

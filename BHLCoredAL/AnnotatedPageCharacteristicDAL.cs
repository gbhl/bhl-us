
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class AnnotatedPageCharacteristicDAL
	{
        public bool AnnotatedPageCharacteristicDeleteByPageID(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, int annotatedPageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageCharacteristicDeleteByPageID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, false, annotatedPageID),
                CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
            {
                int returnCode = CustomSqlHelper.ExecuteNonQuery(command, "ReturnCode");

                if (returnCode == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public AnnotatedPageCharacteristic AnnotatedPageCharacteristicSelectByPageID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string connectionKeyName,
            int PageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageCharacteristicSelectByPageID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, PageID)))
            {
                using (CustomSqlHelper<AnnotatedPageCharacteristic> helper = new CustomSqlHelper<AnnotatedPageCharacteristic>())
                {
                    List<AnnotatedPageCharacteristic> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        AnnotatedPageCharacteristic o = list[0];
                        list = null;
                        return o;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
	}
}

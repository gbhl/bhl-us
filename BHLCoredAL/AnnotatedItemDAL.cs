
#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class AnnotatedItemDAL
	{
        public bool AnnotatedItemCheckForSurrogate(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int annotatedItemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;


            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedItemCheckForSurrogate",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, annotatedItemID)))
            {
                using (CustomSqlHelper<bool> helper = new CustomSqlHelper<bool>())
                {
                    List<bool> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return false;
                }
            }
        }
    }
}

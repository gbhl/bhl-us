
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class DocumentTypeDAL
	{
        public List<DocumentType> DocumentTypeSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("DocumentTypeSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<DocumentType> helper = new CustomSqlHelper<DocumentType>())
                {
                    List<DocumentType> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}


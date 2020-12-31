#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class NameIdentifierDAL
	{
        public List<NameIdentifier> NameIdentifierSelectForResolvedName(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string resolvedName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameIdentifierSelectForResolvedName",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("ResolvedNameString", SqlDbType.NVarChar, 100, false, resolvedName)))
            {
                using (CustomSqlHelper<NameIdentifier> helper = new CustomSqlHelper<NameIdentifier>())
                {
                    List<NameIdentifier> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

#region Using

using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class NameIdentifierDAL
	{
        public CustomGenericList<NameIdentifier> NameIdentifierSelectForResolvedName(
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
                    CustomGenericList<NameIdentifier> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

using CustomDataAccess;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHLImport.DAL
{
    public partial class IABHLCreatorDAL
	{
        /// <summary>
        /// Delete the IABHLCreator and IABHLCreatorIdentifier records for the specified Item ID
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="itemID">Identifier of the IA Item</param>
        public void IABHLCreatorDeleteByItem(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IABHLCreatorDeleteByItem", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
                if (transaction == null) CustomSqlHelper.CloseConnection(connection);
            }
        }
    }
}

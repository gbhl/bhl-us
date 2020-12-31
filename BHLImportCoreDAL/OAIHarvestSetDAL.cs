using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHLImport.DAL
{
    public class OAIHarvestSetDAL
    {
        /// <summary>
        /// Get the list of OAI sets to be harvested
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of objects of type vwOAIHarvestSet.</returns>
        public List<vwOAIHarvestSet> OAIHarvestSetSelectAll(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int onlyActive)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIHarvestSetSelectAll", connection, transaction,
                CustomSqlHelper.CreateInputParameter("OnlyActive", SqlDbType.SmallInt, null, false, onlyActive)))
            {
                using (CustomSqlHelper<vwOAIHarvestSet> helper = new CustomSqlHelper<vwOAIHarvestSet>())
                {
                    List<vwOAIHarvestSet> list = helper.ExecuteReader(command);
                    return (list.Count > 0 ? list : null);
                }
            }
        }
    }
}

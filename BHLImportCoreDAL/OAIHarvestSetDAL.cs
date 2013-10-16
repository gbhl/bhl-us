using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MOBOT.BHLImport.DataObjects;
using CustomDataAccess;
using System.Data;

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
        public CustomGenericList<vwOAIHarvestSet> OAIHarvestSetSelectAll(
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
                    CustomGenericList<vwOAIHarvestSet> list = helper.ExecuteReader(command);
                    return (list.Count > 0 ? list : null);
                }
            }
        }
    }
}

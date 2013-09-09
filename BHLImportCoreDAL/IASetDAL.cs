
#region Using

using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class IASetDAL
	{
        /// <summary>
        /// Select Sets that are marked for download.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>Object of type Set.</returns>
        public CustomGenericList<IASet> IASetSelectForDownload(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IASetSelectForDownload", connection, transaction))
            {
                using (CustomSqlHelper<IASet> helper = new CustomSqlHelper<IASet>())
                {
                    CustomGenericList<IASet> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Select the set with the specified set specification.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="setSpecification">Set specification for which to search</param>
        /// <returns>The Set that matches the search criteria</returns>
        public IASet IASetSelectBySetSpecification(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string setSpecification)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IASetSelectBySetSpecification", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SetSpecification", SqlDbType.NVarChar, 200, false, setSpecification)))
            {
                using (CustomSqlHelper<IASet> helper = new CustomSqlHelper<IASet>())
                {
                    CustomGenericList<IASet> list = helper.ExecuteReader(command);

                    if (list.Count > 0)
                    {
                        return list[0];
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

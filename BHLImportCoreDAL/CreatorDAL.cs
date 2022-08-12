
#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class CreatorDAL
	{
        /// <summary>
        /// Select the new creator with the creator name and importSourceID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="creatorName"></param>
        /// <param name="importSourceID"></param>
        /// <returns>Object of type Creator.</returns>
        public Creator CreatorSelectNewByCreatorNameAndSource(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string marcCreatorA,
            string marcCreatorB,
            string marcCreatorC,
            string marcCreatorD,
            int importSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CreatorSelectNewByCreatorNameAndSource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MARCCreator_a", SqlDbType.NVarChar, 450, true, marcCreatorA),
                CustomSqlHelper.CreateInputParameter("MARCCreator_b", SqlDbType.NVarChar, 450, true, marcCreatorB),
                CustomSqlHelper.CreateInputParameter("MARCCreator_c", SqlDbType.NVarChar, 450, true, marcCreatorC),
                CustomSqlHelper.CreateInputParameter("MARCCreator_d", SqlDbType.NVarChar, 450, true, marcCreatorD),
                CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID)))
            {
                using (CustomSqlHelper<Creator> helper = new CustomSqlHelper<Creator>())
                {
                    List<Creator> list = helper.ExecuteReader(command);

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

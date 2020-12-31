using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class BibliographicLevelDAL
	{
        public List<BibliographicLevel> BibliographicLevelSelectAll(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("BibliographicLevelSelectAll", 
                connection, transaction))
            {
                using (CustomSqlHelper<BibliographicLevel> helper = new CustomSqlHelper<BibliographicLevel>())
                {
                    List<BibliographicLevel> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

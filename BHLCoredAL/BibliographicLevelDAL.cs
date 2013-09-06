using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class BibliographicLevelDAL
	{
        public CustomGenericList<BibliographicLevel> BibliographicLevelSelectAll(SqlConnection sqlConnection, 
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
                    CustomGenericList<BibliographicLevel> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

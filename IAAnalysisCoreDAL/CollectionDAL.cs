
#region Using

using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.IAAnalysis.DataObjects;

#endregion Using

namespace MOBOT.IAAnalysis.DAL
{
	public partial class CollectionDAL
	{
        public Collection CollectionSelectByCollectionName(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string collectionName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("IAAnalysis"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectByCollectionName", connection, transaction,
                CustomSqlHelper.CreateInputParameter("CollectionName", SqlDbType.NVarChar, 200, false, collectionName)))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    CustomGenericList<Collection> list = helper.ExecuteReader(command);

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

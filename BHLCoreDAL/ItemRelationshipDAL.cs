using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class ItemRelationshipDAL
	{
		public List<ItemRelationship> ItemRelationshipSelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemRelationshipSelectByItemID", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID)))
			{
				using (CustomSqlHelper<ItemRelationship> helper = new CustomSqlHelper<ItemRelationship>())
				{
					List<ItemRelationship> list = helper.ExecuteReader(command);
					return list;					
				}
			}
		}

	}
}


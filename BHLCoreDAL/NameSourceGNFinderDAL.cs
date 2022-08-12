
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class NameSourceGNFinderDAL
	{
		public List<NameSourceGNFinder> NameSourceGNFinderSelectAll(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceGNFinderSelectAll", connection, transaction))
			{
				using (CustomSqlHelper<NameSourceGNFinder> helper = new CustomSqlHelper<NameSourceGNFinder>())
				{
					List<NameSourceGNFinder> list = helper.ExecuteReader(command);
					return (list);
				}
			}
		}
	}
}


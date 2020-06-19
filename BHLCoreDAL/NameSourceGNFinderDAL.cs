
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class NameSourceGNFinderDAL
	{
		public CustomGenericList<NameSourceGNFinder> NameSourceGNFinderSelectAll(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceGNFinderSelectAll", connection, transaction))
			{
				using (CustomSqlHelper<NameSourceGNFinder> helper = new CustomSqlHelper<NameSourceGNFinder>())
				{
					CustomGenericList<NameSourceGNFinder> list = helper.ExecuteReader(command);
					return (list);
				}
			}
		}
	}
}


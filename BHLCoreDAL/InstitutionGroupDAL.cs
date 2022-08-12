using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
	public partial class InstitutionGroupDAL
	{
		public List<InstitutionGroup> InstitutionGroupSelectAll(
			SqlConnection sqlConnection,
			SqlTransaction sqlTransaction)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupSelectAll", connection, transaction))
			{
				using (CustomSqlHelper<InstitutionGroup> helper = new CustomSqlHelper<InstitutionGroup>())
				{
					List<InstitutionGroup> list = helper.ExecuteReader(command);
					return (list);
				}
			}
		}

		public List<InstitutionGroupInstitution> InstitutionGroupSelectInstitutions(
			SqlConnection sqlConnection,
			SqlTransaction sqlTransaction,
			int institutionGroupId)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupSelectInstitutions", connection, transaction,
					CustomSqlHelper.CreateInputParameter("InstitutionGroupID", SqlDbType.Int, null, false, institutionGroupId)))
			{
				using (CustomSqlHelper<InstitutionGroupInstitution> helper = new CustomSqlHelper<InstitutionGroupInstitution>())
				{
					List<InstitutionGroupInstitution> list = helper.ExecuteReader(command);
					return (list);
				}
			}
		}

		// Delete an insstitiongroup and all associations with institutions
		public void InstitutionGroupDelete(
			SqlConnection sqlConnection,
			SqlTransaction sqlTransaction,
			int institutionGroupId)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupDelete", connection, transaction,
					CustomSqlHelper.CreateInputParameter("InstitutionGroupID", SqlDbType.Int, null, false, institutionGroupId)))
			{
				CustomSqlHelper.ExecuteNonQuery(command);
			}
		}
	}
}


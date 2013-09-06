using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class InstitutionDAL
	{
		public CustomGenericList<Institution> InstitutionSelectAll(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;
			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "InstitutionSelectAll", connection, transaction ) )
			{
				using ( CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>() )
				{
					CustomGenericList<Institution> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		public Institution InstitutionSelectByItemID(
				SqlConnection sqlConnection,
				SqlTransaction sqlTransaction,
				int itemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "InstitutionSelectByItemID", connection, transaction,
					CustomSqlHelper.CreateInputParameter( "ItemID", SqlDbType.Int, null, false, itemID ) ) )
			{
				using ( CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>() )
				{
					CustomGenericList<Institution> list = helper.ExecuteReader( command );
					if ( list.Count > 0 )
					{
						Institution o = list[ 0 ];
						list = null;
						return o;
					}
					else
					{
						return null;
					}
				}
			}
		}

        public CustomGenericList<Institution> InstitutionSelectWithPublishedItems(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                bool onlyMemberLibraries)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectWithPublishedItems", connection, transaction,
                CustomSqlHelper.CreateInputParameter("OnlyMemberLibraries", SqlDbType.Bit, null, false, onlyMemberLibraries)))
            {
                using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
                {
                    CustomGenericList<Institution> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<Institution> InstitutionSelectDOIStats(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int sortBy)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectDOIStats", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.Int, null, false, sortBy)))
            {
                using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
                {
                    CustomGenericList<Institution> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Institution institution)
		{
			SqlConnection connection = sqlConnection;
			SqlTransaction transaction = sqlTransaction;

			if ( connection == null )
			{
				connection =
					CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ) );
			}

			bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator( transaction );

			try
			{
				transaction = CustomSqlHelper.BeginTransaction( connection, transaction, isTransactionCoordinator );

				new InstitutionDAL().InstitutionManageAuto( connection, transaction, institution );

				CustomSqlHelper.CommitTransaction( transaction, isTransactionCoordinator );
			}
			catch ( Exception ex )
			{
				CustomSqlHelper.RollbackTransaction( transaction, isTransactionCoordinator );

				throw;
			}
			finally
			{
				CustomSqlHelper.CloseConnection( connection, isTransactionCoordinator );
			}
		}
	}
}

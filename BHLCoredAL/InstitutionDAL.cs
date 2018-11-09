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

		public CustomGenericList<Institution> InstitutionSelectByItemID(
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
						return list;
					}
					else
					{
						return null;
					}
				}
			}
		}

        public CustomGenericList<Institution> InstitutionSelectByTitleID(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
                {
                    CustomGenericList<Institution> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Institution> InstitutionSelectWithPublishedItems(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                bool onlyMemberLibraries,
                string institutionRoleName = null)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectWithPublishedItems", connection, transaction,
                CustomSqlHelper.CreateInputParameter("OnlyMemberLibraries", SqlDbType.Bit, null, false, onlyMemberLibraries),
                CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, true, institutionRoleName)))
            {
                using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
                {
                    CustomGenericList<Institution> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<Institution> InstitutionSelectWithPublishedSegments(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                bool onlyMemberLibraries,
                string institutionRoleName = null)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectWithPublishedSegments", connection, transaction,
                CustomSqlHelper.CreateInputParameter("OnlyMemberLibraries", SqlDbType.Bit, null, false, onlyMemberLibraries),
                CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, true, institutionRoleName)))
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
            int sortBy,
            int bhlOnly)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectDOIStats", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.Int, null, false, sortBy),
                CustomSqlHelper.CreateInputParameter("BHLOnly", SqlDbType.Int, null, false, bhlOnly)))
            {
                using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
                {
                    CustomGenericList<Institution> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<Institution> InstitutionSelectBySegmentIDAndRole(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int segmentID,
            string institutionRoleName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectBySegmentIDAndRole", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName)))
            {
                using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
                {
                    CustomGenericList<Institution> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<Institution> InstitutionSelectByItemIDAndRole(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID,
            string institutionRoleName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectByItemIDAndRole", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName)))
            {
                using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
                {
                    CustomGenericList<Institution> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<Institution> InstitutionSelectByTitleIDAndRole(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int titleID,
            string institutionRoleName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectByTitleIDAndRole", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
                CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName)))
            {
                using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
                {
                    CustomGenericList<Institution> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Institution institution, int userID)
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

				new InstitutionDAL().InstitutionManageAuto( connection, transaction, institution, userID );

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

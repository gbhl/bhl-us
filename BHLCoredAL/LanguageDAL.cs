using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class LanguageDAL
	{
		public List<Language> SelectAll( SqlConnection sqlConnection, SqlTransaction sqlTransaction )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( 
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "LanguageSelectAll", connection, transaction ) )
			{
				using ( CustomSqlHelper<Language> helper = new CustomSqlHelper<Language>() )
				{
					List<Language> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

        public List<Language> LanguageSelectWithPublishedItems(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("LanguageSelectWithPublishedItems", connection, transaction))
            {
                using (CustomSqlHelper<Language> helper = new CustomSqlHelper<Language>())
                {
                    List<Language> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Language language)
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

				new LanguageDAL().LanguageManageAuto( connection, transaction, language );

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

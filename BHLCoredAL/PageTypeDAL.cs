using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class PageTypeDAL
	{
		public List<PageType> PageTypeSelectAll( SqlConnection sqlConnection, SqlTransaction sqlTransaction )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( 
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "PageTypeSelectAll", connection, transaction ) )
			{
				using ( CustomSqlHelper<PageType> helper = new CustomSqlHelper<PageType>() )
				{
					List<PageType> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		public void Save( SqlConnection sqlConnection, SqlTransaction sqlTransaction, PageType pageType, int userId )
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

				new PageTypeDAL().PageTypeManageAuto( connection, transaction, pageType, userId );

				CustomSqlHelper.CommitTransaction( transaction, isTransactionCoordinator );
			}
			catch (Exception)
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

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class ItemStatusDAL
	{
		public List<ItemStatus> SelectAll( SqlConnection sqlConnection, SqlTransaction sqlTransaction )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( 
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;
			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "ItemStatusSelectAll", connection, transaction ) )
			{
				using ( CustomSqlHelper<ItemStatus> helper = new CustomSqlHelper<ItemStatus>() )
				{
					List<ItemStatus> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		public void Save( SqlConnection sqlConnection, SqlTransaction sqlTransaction, ItemStatus itemStatus )
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

				new ItemStatusDAL().ItemStatusManageAuto( connection, transaction, itemStatus );

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

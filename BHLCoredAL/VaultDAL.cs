using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class VaultDAL
	{
		public CustomGenericList<Vault> SelectAll( SqlConnection sqlConnection, SqlTransaction sqlTransaction )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "VaultSelectAll", connection, transaction ) )
			{
				using ( CustomSqlHelper<Vault> helper = new CustomSqlHelper<Vault>() )
				{
					CustomGenericList<Vault> list = helper.ExecuteReader( command );
					return ( list );
				}
			}
		}

		public void Save( SqlConnection sqlConnection, SqlTransaction sqlTransaction, Vault vault )
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

				if ( vault.VaultID == 0 )
				{
					using ( SqlCommand command = CustomSqlHelper.CreateCommand( "VaultSelectMaxID", connection, transaction ) )
					{
						using ( CustomSqlHelper<int> helper = new CustomSqlHelper<int>() )
						{
							CustomGenericList<int> list = helper.ExecuteReader( command );
							vault.VaultID = list[ 0 ] + 1;
						}
					}
				}

				new VaultDAL().VaultManageAuto( connection, transaction, vault );

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

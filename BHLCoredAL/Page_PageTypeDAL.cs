using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class Page_PageTypeDAL
	{
        public void Page_PageTypeSave(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Page_PageType value,
			int userId )
		{
			Page_PageType ppt = this.Page_PageTypeSelectAuto( sqlConnection, sqlTransaction, value.PageID, value.PageTypeID );
			if ( ppt != null )
			{
				ppt.Verified = true;
				ppt.LastModifiedUserID = userId;
				this.Page_PageTypeUpdateAuto( sqlConnection, sqlTransaction, ppt );
			}
			else
			{
				value.LastModifiedUserID = userId;
				value.CreationUserID = userId;
				this.Page_PageTypeInsertAuto( sqlConnection, sqlTransaction, value );
			}
		}

		public bool Page_PageTypeDeleteAllForPage( SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection( 
				CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ), sqlConnection );
			SqlTransaction transaction = sqlTransaction;

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "Page_PageTypeDeleteAllForPage", connection, transaction,
				CustomSqlHelper.CreateInputParameter( "PageID", SqlDbType.Int, null, false, pageID ),
				CustomSqlHelper.CreateReturnValueParameter( "ReturnCode", SqlDbType.Int, null, false ) ) )
			{
				int returnCode = CustomSqlHelper.ExecuteNonQuery( command, "ReturnCode" );

				if ( returnCode == 0 )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
	}
}

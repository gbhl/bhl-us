
// Generated 1/15/2008 11:27:51 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAPageDAL is based upon IAPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAPageDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
{
	partial class IAPageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from IAPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAPageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
			{
				using (CustomSqlHelper<IAPage> helper = new CustomSqlHelper<IAPage>())
				{
					CustomGenericList<IAPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAPage o = list[0];
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
		
		/// <summary>
		/// Select values from IAPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAPageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="localFileName"></param>
		/// <param name="sequence"></param>
		/// <param name="externalUrl"></param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string localFileName,
			int? sequence,
			string externalUrl)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAPageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("LocalFileName", SqlDbType.NVarChar, 50, false, localFileName),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, true, sequence),
					CustomSqlHelper.CreateInputParameter("ExternalUrl", SqlDbType.NVarChar, 500, true, externalUrl), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAPage> helper = new CustomSqlHelper<IAPage>())
				{
					CustomGenericList<IAPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAPage o = list[0];
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

		/// <summary>
		/// Insert values into IAPage. Returns an object of type IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAPage.</param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAPage value)
		{
			return IAPageInsertAuto(sqlConnection, sqlTransaction, 
				value.ItemID,
				value.LocalFileName,
				value.Sequence,
				value.ExternalUrl);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from IAPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAPageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				int returnCode = CustomSqlHelper.ExecuteNonQuery(command, "ReturnCode");
				
				if (transaction == null)
				{
					CustomSqlHelper.CloseConnection(connection);
				}
				
				if (returnCode == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		
		#endregion ===== DELETE =====

 		#region ===== UPDATE =====

		/// <summary>
		/// Update values in IAPage. Returns an object of type IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="itemID"></param>
		/// <param name="localFileName"></param>
		/// <param name="sequence"></param>
		/// <param name="externalUrl"></param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			int itemID,
			string localFileName,
			int? sequence,
			string externalUrl)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAPageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("LocalFileName", SqlDbType.NVarChar, 50, false, localFileName),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, true, sequence),
					CustomSqlHelper.CreateInputParameter("ExternalUrl", SqlDbType.NVarChar, 500, true, externalUrl), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAPage> helper = new CustomSqlHelper<IAPage>())
				{
					CustomGenericList<IAPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAPage o = list[0];
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
		
		/// <summary>
		/// Update values in IAPage. Returns an object of type IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAPage.</param>
		/// <returns>Object of type IAPage.</returns>
		public IAPage IAPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAPage value)
		{
			return IAPageUpdateAuto(sqlConnection, sqlTransaction,
				value.PageID,
				value.ItemID,
				value.LocalFileName,
				value.Sequence,
				value.ExternalUrl);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage IAPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAPage>.</returns>
		public CustomDataAccessStatus<IAPage> IAPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAPage value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAPage returnValue = IAPageInsertAuto(sqlConnection, sqlTransaction, 
					value.ItemID,
						value.LocalFileName,
						value.Sequence,
						value.ExternalUrl);
				
				return new CustomDataAccessStatus<IAPage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAPageDeleteAuto(sqlConnection, sqlTransaction, 
					value.PageID))
				{
				return new CustomDataAccessStatus<IAPage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAPage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAPage returnValue = IAPageUpdateAuto(sqlConnection, sqlTransaction, 
					value.PageID,
						value.ItemID,
						value.LocalFileName,
						value.Sequence,
						value.ExternalUrl);
					
				return new CustomDataAccessStatus<IAPage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAPage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

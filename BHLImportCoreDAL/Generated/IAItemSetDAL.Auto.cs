
// Generated 1/15/2008 11:27:51 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAItemSetDAL is based upon IAItemSet.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAItemSetDAL
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
	partial class IAItemSetDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from IAItemSet by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="setID"></param>
		/// <returns>Object of type IAItemSet.</returns>
		public IAItemSet IAItemSetSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int setID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSetSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("SetID", SqlDbType.Int, null, false, setID)))
			{
				using (CustomSqlHelper<IAItemSet> helper = new CustomSqlHelper<IAItemSet>())
				{
					CustomGenericList<IAItemSet> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemSet o = list[0];
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
		/// Select values from IAItemSet by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="setID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAItemSetSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int setID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSetSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("SetID", SqlDbType.Int, null, false, setID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into IAItemSet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="setID"></param>
		/// <returns>Object of type IAItemSet.</returns>
		public IAItemSet IAItemSetInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int setID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSetInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("SetID", SqlDbType.Int, null, false, setID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAItemSet> helper = new CustomSqlHelper<IAItemSet>())
				{
					CustomGenericList<IAItemSet> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemSet o = list[0];
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
		/// Insert values into IAItemSet. Returns an object of type IAItemSet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemSet.</param>
		/// <returns>Object of type IAItemSet.</returns>
		public IAItemSet IAItemSetInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemSet value)
		{
			return IAItemSetInsertAuto(sqlConnection, sqlTransaction, 
				value.ItemID,
				value.SetID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from IAItemSet by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="setID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAItemSetDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int setID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSetDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("SetID", SqlDbType.Int, null, false, setID), 
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
		/// Update values in IAItemSet. Returns an object of type IAItemSet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="setID"></param>
		/// <returns>Object of type IAItemSet.</returns>
		public IAItemSet IAItemSetUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int setID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSetUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("SetID", SqlDbType.Int, null, false, setID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAItemSet> helper = new CustomSqlHelper<IAItemSet>())
				{
					CustomGenericList<IAItemSet> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemSet o = list[0];
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
		/// Update values in IAItemSet. Returns an object of type IAItemSet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemSet.</param>
		/// <returns>Object of type IAItemSet.</returns>
		public IAItemSet IAItemSetUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemSet value)
		{
			return IAItemSetUpdateAuto(sqlConnection, sqlTransaction,
				value.ItemID,
				value.SetID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage IAItemSet object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAItemSet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemSet.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAItemSet>.</returns>
		public CustomDataAccessStatus<IAItemSet> IAItemSetManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemSet value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAItemSet returnValue = IAItemSetInsertAuto(sqlConnection, sqlTransaction, 
					value.ItemID,
						value.SetID);
				
				return new CustomDataAccessStatus<IAItemSet>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAItemSetDeleteAuto(sqlConnection, sqlTransaction, 
					value.ItemID,
						value.SetID))
				{
				return new CustomDataAccessStatus<IAItemSet>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAItemSet>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAItemSet returnValue = IAItemSetUpdateAuto(sqlConnection, sqlTransaction, 
					value.ItemID,
						value.SetID);
					
				return new CustomDataAccessStatus<IAItemSet>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAItemSet>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

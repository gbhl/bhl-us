
// Generated 1/15/2008 11:27:51 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAMarcDAL is based upon IAMarc.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAMarcDAL
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
	partial class IAMarcDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from IAMarc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID)))
			{
				using (CustomSqlHelper<IAMarc> helper = new CustomSqlHelper<IAMarc>())
				{
					CustomGenericList<IAMarc> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarc o = list[0];
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
		/// Select values from IAMarc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAMarcSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="leader"></param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string leader)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MarcID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Leader", SqlDbType.VarChar, 200, false, leader), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAMarc> helper = new CustomSqlHelper<IAMarc>())
				{
					CustomGenericList<IAMarc> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarc o = list[0];
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
		/// Insert values into IAMarc. Returns an object of type IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarc.</param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarc value)
		{
			return IAMarcInsertAuto(sqlConnection, sqlTransaction, 
				value.ItemID,
				value.Leader);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from IAMarc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAMarcDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID), 
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
		/// Update values in IAMarc. Returns an object of type IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <param name="itemID"></param>
		/// <param name="leader"></param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID,
			int itemID,
			string leader)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Leader", SqlDbType.VarChar, 200, false, leader), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAMarc> helper = new CustomSqlHelper<IAMarc>())
				{
					CustomGenericList<IAMarc> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarc o = list[0];
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
		/// Update values in IAMarc. Returns an object of type IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarc.</param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarc value)
		{
			return IAMarcUpdateAuto(sqlConnection, sqlTransaction,
				value.MarcID,
				value.ItemID,
				value.Leader);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage IAMarc object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarc.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAMarc>.</returns>
		public CustomDataAccessStatus<IAMarc> IAMarcManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarc value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAMarc returnValue = IAMarcInsertAuto(sqlConnection, sqlTransaction, 
					value.ItemID,
						value.Leader);
				
				return new CustomDataAccessStatus<IAMarc>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAMarcDeleteAuto(sqlConnection, sqlTransaction, 
					value.MarcID))
				{
				return new CustomDataAccessStatus<IAMarc>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAMarc>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAMarc returnValue = IAMarcUpdateAuto(sqlConnection, sqlTransaction, 
					value.MarcID,
						value.ItemID,
						value.Leader);
					
				return new CustomDataAccessStatus<IAMarc>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAMarc>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

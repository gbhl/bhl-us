
// Generated 1/17/2008 3:54:35 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class BotanicusHarvestLogDAL is based upon BotanicusHarvestLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class BotanicusHarvestLogDAL
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
	partial class BotanicusHarvestLogDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from BotanicusHarvestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="botanicusHarvestLogID"></param>
		/// <returns>Object of type BotanicusHarvestLog.</returns>
		public BotanicusHarvestLog BotanicusHarvestLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int botanicusHarvestLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BotanicusHarvestLogSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BotanicusHarvestLogID", SqlDbType.Int, null, false, botanicusHarvestLogID)))
			{
				using (CustomSqlHelper<BotanicusHarvestLog> helper = new CustomSqlHelper<BotanicusHarvestLog>())
				{
					CustomGenericList<BotanicusHarvestLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BotanicusHarvestLog o = list[0];
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
		/// Select values from BotanicusHarvestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="botanicusHarvestLogID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> BotanicusHarvestLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int botanicusHarvestLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BotanicusHarvestLogSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("BotanicusHarvestLogID", SqlDbType.Int, null, false, botanicusHarvestLogID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into BotanicusHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="harvestStartDate"></param>
		/// <param name="harvestEndDate"></param>
		/// <param name="automaticHarvest"></param>
		/// <param name="successfulHarvest"></param>
		/// <param name="title"></param>
		/// <param name="titleTag"></param>
		/// <param name="titleCreator"></param>
		/// <param name="creator"></param>
		/// <param name="item"></param>
		/// <param name="page"></param>
		/// <param name="indicatedPage"></param>
		/// <param name="pagePageType"></param>
		/// <param name="pageName"></param>
		/// <returns>Object of type BotanicusHarvestLog.</returns>
		public BotanicusHarvestLog BotanicusHarvestLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			DateTime harvestStartDate,
			DateTime harvestEndDate,
			bool automaticHarvest,
			bool successfulHarvest,
			int title,
			int titleTag,
			int titleCreator,
			int creator,
			int item,
			int page,
			int indicatedPage,
			int pagePageType,
			int pageName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BotanicusHarvestLogInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("BotanicusHarvestLogID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("HarvestStartDate", SqlDbType.DateTime, null, false, harvestStartDate),
					CustomSqlHelper.CreateInputParameter("HarvestEndDate", SqlDbType.DateTime, null, false, harvestEndDate),
					CustomSqlHelper.CreateInputParameter("AutomaticHarvest", SqlDbType.Bit, null, false, automaticHarvest),
					CustomSqlHelper.CreateInputParameter("SuccessfulHarvest", SqlDbType.Bit, null, false, successfulHarvest),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.Int, null, false, title),
					CustomSqlHelper.CreateInputParameter("TitleTag", SqlDbType.Int, null, false, titleTag),
					CustomSqlHelper.CreateInputParameter("TitleCreator", SqlDbType.Int, null, false, titleCreator),
					CustomSqlHelper.CreateInputParameter("Creator", SqlDbType.Int, null, false, creator),
					CustomSqlHelper.CreateInputParameter("Item", SqlDbType.Int, null, false, item),
					CustomSqlHelper.CreateInputParameter("Page", SqlDbType.Int, null, false, page),
					CustomSqlHelper.CreateInputParameter("IndicatedPage", SqlDbType.Int, null, false, indicatedPage),
					CustomSqlHelper.CreateInputParameter("PagePageType", SqlDbType.Int, null, false, pagePageType),
					CustomSqlHelper.CreateInputParameter("PageName", SqlDbType.Int, null, false, pageName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BotanicusHarvestLog> helper = new CustomSqlHelper<BotanicusHarvestLog>())
				{
					CustomGenericList<BotanicusHarvestLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BotanicusHarvestLog o = list[0];
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
		/// Insert values into BotanicusHarvestLog. Returns an object of type BotanicusHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BotanicusHarvestLog.</param>
		/// <returns>Object of type BotanicusHarvestLog.</returns>
		public BotanicusHarvestLog BotanicusHarvestLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BotanicusHarvestLog value)
		{
			return BotanicusHarvestLogInsertAuto(sqlConnection, sqlTransaction, 
				value.HarvestStartDate,
				value.HarvestEndDate,
				value.AutomaticHarvest,
				value.SuccessfulHarvest,
				value.Title,
				value.TitleTag,
				value.TitleCreator,
				value.Creator,
				value.Item,
				value.Page,
				value.IndicatedPage,
				value.PagePageType,
				value.PageName);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from BotanicusHarvestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="botanicusHarvestLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BotanicusHarvestLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int botanicusHarvestLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BotanicusHarvestLogDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BotanicusHarvestLogID", SqlDbType.Int, null, false, botanicusHarvestLogID), 
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
		/// Update values in BotanicusHarvestLog. Returns an object of type BotanicusHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="botanicusHarvestLogID"></param>
		/// <param name="harvestStartDate"></param>
		/// <param name="harvestEndDate"></param>
		/// <param name="automaticHarvest"></param>
		/// <param name="successfulHarvest"></param>
		/// <param name="title"></param>
		/// <param name="titleTag"></param>
		/// <param name="titleCreator"></param>
		/// <param name="creator"></param>
		/// <param name="item"></param>
		/// <param name="page"></param>
		/// <param name="indicatedPage"></param>
		/// <param name="pagePageType"></param>
		/// <param name="pageName"></param>
		/// <returns>Object of type BotanicusHarvestLog.</returns>
		public BotanicusHarvestLog BotanicusHarvestLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int botanicusHarvestLogID,
			DateTime harvestStartDate,
			DateTime harvestEndDate,
			bool automaticHarvest,
			bool successfulHarvest,
			int title,
			int titleTag,
			int titleCreator,
			int creator,
			int item,
			int page,
			int indicatedPage,
			int pagePageType,
			int pageName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BotanicusHarvestLogUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BotanicusHarvestLogID", SqlDbType.Int, null, false, botanicusHarvestLogID),
					CustomSqlHelper.CreateInputParameter("HarvestStartDate", SqlDbType.DateTime, null, false, harvestStartDate),
					CustomSqlHelper.CreateInputParameter("HarvestEndDate", SqlDbType.DateTime, null, false, harvestEndDate),
					CustomSqlHelper.CreateInputParameter("AutomaticHarvest", SqlDbType.Bit, null, false, automaticHarvest),
					CustomSqlHelper.CreateInputParameter("SuccessfulHarvest", SqlDbType.Bit, null, false, successfulHarvest),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.Int, null, false, title),
					CustomSqlHelper.CreateInputParameter("TitleTag", SqlDbType.Int, null, false, titleTag),
					CustomSqlHelper.CreateInputParameter("TitleCreator", SqlDbType.Int, null, false, titleCreator),
					CustomSqlHelper.CreateInputParameter("Creator", SqlDbType.Int, null, false, creator),
					CustomSqlHelper.CreateInputParameter("Item", SqlDbType.Int, null, false, item),
					CustomSqlHelper.CreateInputParameter("Page", SqlDbType.Int, null, false, page),
					CustomSqlHelper.CreateInputParameter("IndicatedPage", SqlDbType.Int, null, false, indicatedPage),
					CustomSqlHelper.CreateInputParameter("PagePageType", SqlDbType.Int, null, false, pagePageType),
					CustomSqlHelper.CreateInputParameter("PageName", SqlDbType.Int, null, false, pageName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BotanicusHarvestLog> helper = new CustomSqlHelper<BotanicusHarvestLog>())
				{
					CustomGenericList<BotanicusHarvestLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BotanicusHarvestLog o = list[0];
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
		/// Update values in BotanicusHarvestLog. Returns an object of type BotanicusHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BotanicusHarvestLog.</param>
		/// <returns>Object of type BotanicusHarvestLog.</returns>
		public BotanicusHarvestLog BotanicusHarvestLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BotanicusHarvestLog value)
		{
			return BotanicusHarvestLogUpdateAuto(sqlConnection, sqlTransaction,
				value.BotanicusHarvestLogID,
				value.HarvestStartDate,
				value.HarvestEndDate,
				value.AutomaticHarvest,
				value.SuccessfulHarvest,
				value.Title,
				value.TitleTag,
				value.TitleCreator,
				value.Creator,
				value.Item,
				value.Page,
				value.IndicatedPage,
				value.PagePageType,
				value.PageName);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage BotanicusHarvestLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in BotanicusHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BotanicusHarvestLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<BotanicusHarvestLog>.</returns>
		public CustomDataAccessStatus<BotanicusHarvestLog> BotanicusHarvestLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BotanicusHarvestLog value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				BotanicusHarvestLog returnValue = BotanicusHarvestLogInsertAuto(sqlConnection, sqlTransaction, 
					value.HarvestStartDate,
						value.HarvestEndDate,
						value.AutomaticHarvest,
						value.SuccessfulHarvest,
						value.Title,
						value.TitleTag,
						value.TitleCreator,
						value.Creator,
						value.Item,
						value.Page,
						value.IndicatedPage,
						value.PagePageType,
						value.PageName);
				
				return new CustomDataAccessStatus<BotanicusHarvestLog>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (BotanicusHarvestLogDeleteAuto(sqlConnection, sqlTransaction, 
					value.BotanicusHarvestLogID))
				{
				return new CustomDataAccessStatus<BotanicusHarvestLog>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<BotanicusHarvestLog>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				BotanicusHarvestLog returnValue = BotanicusHarvestLogUpdateAuto(sqlConnection, sqlTransaction, 
					value.BotanicusHarvestLogID,
						value.HarvestStartDate,
						value.HarvestEndDate,
						value.AutomaticHarvest,
						value.SuccessfulHarvest,
						value.Title,
						value.TitleTag,
						value.TitleCreator,
						value.Creator,
						value.Item,
						value.Page,
						value.IndicatedPage,
						value.PagePageType,
						value.PageName);
					
				return new CustomDataAccessStatus<BotanicusHarvestLog>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<BotanicusHarvestLog>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

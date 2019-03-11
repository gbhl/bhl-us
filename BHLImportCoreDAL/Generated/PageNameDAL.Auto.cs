
// Generated 1/16/2008 1:54:48 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PageNameDAL is based upon PageName.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class PageNameDAL
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
	partial class PageNameDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from PageName by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageNameID"></param>
		/// <returns>Object of type PageName.</returns>
		public PageName PageNameSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageNameID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageNameSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageNameID", SqlDbType.Int, null, false, pageNameID)))
			{
				using (CustomSqlHelper<PageName> helper = new CustomSqlHelper<PageName>())
				{
					CustomGenericList<PageName> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageName o = list[0];
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
		/// Select values from PageName by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageNameID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> PageNameSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageNameID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageNameSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PageNameID", SqlDbType.Int, null, false, pageNameID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into PageName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="barCode"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="source"></param>
		/// <param name="nameFound"></param>
		/// <param name="nameConfirmed"></param>
		/// <param name="nameBankID"></param>
		/// <param name="active"></param>
		/// <param name="externalCreateDate"></param>
		/// <param name="externalLastUpdateDate"></param>
		/// <param name="isCommonName"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type PageName.</returns>
		public PageName PageNameInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importStatusID,
			int? importSourceID,
			string barCode,
			string fileNamePrefix,
			int? sequenceOrder,
			string source,
			string nameFound,
			string nameConfirmed,
			int? nameBankID,
			bool? active,
			DateTime? externalCreateDate,
			DateTime? externalLastUpdateDate,
			bool? isCommonName,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageNameInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PageNameID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("FileNamePrefix", SqlDbType.NVarChar, 200, false, fileNamePrefix),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, true, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("Source", SqlDbType.NVarChar, 50, true, source),
					CustomSqlHelper.CreateInputParameter("NameFound", SqlDbType.NVarChar, 100, true, nameFound),
					CustomSqlHelper.CreateInputParameter("NameConfirmed", SqlDbType.NVarChar, 100, true, nameConfirmed),
					CustomSqlHelper.CreateInputParameter("NameBankID", SqlDbType.Int, null, true, nameBankID),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.Bit, null, true, active),
					CustomSqlHelper.CreateInputParameter("ExternalCreateDate", SqlDbType.DateTime, null, true, externalCreateDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastUpdateDate", SqlDbType.DateTime, null, true, externalLastUpdateDate),
					CustomSqlHelper.CreateInputParameter("IsCommonName", SqlDbType.Bit, null, true, isCommonName),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageName> helper = new CustomSqlHelper<PageName>())
				{
					CustomGenericList<PageName> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageName o = list[0];
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
		/// Insert values into PageName. Returns an object of type PageName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageName.</param>
		/// <returns>Object of type PageName.</returns>
		public PageName PageNameInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageName value)
		{
			return PageNameInsertAuto(sqlConnection, sqlTransaction, 
				value.ImportStatusID,
				value.ImportSourceID,
				value.BarCode,
				value.FileNamePrefix,
				value.SequenceOrder,
				value.Source,
				value.NameFound,
				value.NameConfirmed,
				value.NameBankID,
				value.Active,
				value.ExternalCreateDate,
				value.ExternalLastUpdateDate,
				value.IsCommonName,
				value.ProductionDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from PageName by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageNameID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageNameDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageNameID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageNameDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageNameID", SqlDbType.Int, null, false, pageNameID), 
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
		/// Update values in PageName. Returns an object of type PageName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageNameID"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="barCode"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="source"></param>
		/// <param name="nameFound"></param>
		/// <param name="nameConfirmed"></param>
		/// <param name="nameBankID"></param>
		/// <param name="active"></param>
		/// <param name="externalCreateDate"></param>
		/// <param name="externalLastUpdateDate"></param>
		/// <param name="isCommonName"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type PageName.</returns>
		public PageName PageNameUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageNameID,
			int importStatusID,
			int? importSourceID,
			string barCode,
			string fileNamePrefix,
			int? sequenceOrder,
			string source,
			string nameFound,
			string nameConfirmed,
			int? nameBankID,
			bool? active,
			DateTime? externalCreateDate,
			DateTime? externalLastUpdateDate,
			bool? isCommonName,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageNameUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageNameID", SqlDbType.Int, null, false, pageNameID),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("FileNamePrefix", SqlDbType.NVarChar, 200, false, fileNamePrefix),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, true, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("Source", SqlDbType.NVarChar, 50, true, source),
					CustomSqlHelper.CreateInputParameter("NameFound", SqlDbType.NVarChar, 100, true, nameFound),
					CustomSqlHelper.CreateInputParameter("NameConfirmed", SqlDbType.NVarChar, 100, true, nameConfirmed),
					CustomSqlHelper.CreateInputParameter("NameBankID", SqlDbType.Int, null, true, nameBankID),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.Bit, null, true, active),
					CustomSqlHelper.CreateInputParameter("ExternalCreateDate", SqlDbType.DateTime, null, true, externalCreateDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastUpdateDate", SqlDbType.DateTime, null, true, externalLastUpdateDate),
					CustomSqlHelper.CreateInputParameter("IsCommonName", SqlDbType.Bit, null, true, isCommonName),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageName> helper = new CustomSqlHelper<PageName>())
				{
					CustomGenericList<PageName> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageName o = list[0];
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
		/// Update values in PageName. Returns an object of type PageName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageName.</param>
		/// <returns>Object of type PageName.</returns>
		public PageName PageNameUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageName value)
		{
			return PageNameUpdateAuto(sqlConnection, sqlTransaction,
				value.PageNameID,
				value.ImportStatusID,
				value.ImportSourceID,
				value.BarCode,
				value.FileNamePrefix,
				value.SequenceOrder,
				value.Source,
				value.NameFound,
				value.NameConfirmed,
				value.NameBankID,
				value.Active,
				value.ExternalCreateDate,
				value.ExternalLastUpdateDate,
				value.IsCommonName,
				value.ProductionDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage PageName object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in PageName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageName.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageName>.</returns>
		public CustomDataAccessStatus<PageName> PageNameManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageName value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				PageName returnValue = PageNameInsertAuto(sqlConnection, sqlTransaction, 
					value.ImportStatusID,
						value.ImportSourceID,
						value.BarCode,
						value.FileNamePrefix,
						value.SequenceOrder,
						value.Source,
						value.NameFound,
						value.NameConfirmed,
						value.NameBankID,
						value.Active,
						value.ExternalCreateDate,
						value.ExternalLastUpdateDate,
						value.IsCommonName,
						value.ProductionDate);
				
				return new CustomDataAccessStatus<PageName>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PageNameDeleteAuto(sqlConnection, sqlTransaction, 
					value.PageNameID))
				{
				return new CustomDataAccessStatus<PageName>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<PageName>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				PageName returnValue = PageNameUpdateAuto(sqlConnection, sqlTransaction, 
					value.PageNameID,
						value.ImportStatusID,
						value.ImportSourceID,
						value.BarCode,
						value.FileNamePrefix,
						value.SequenceOrder,
						value.Source,
						value.NameFound,
						value.NameConfirmed,
						value.NameBankID,
						value.Active,
						value.ExternalCreateDate,
						value.ExternalLastUpdateDate,
						value.IsCommonName,
						value.ProductionDate);
					
				return new CustomDataAccessStatus<PageName>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<PageName>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

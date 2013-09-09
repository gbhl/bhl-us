
// Generated 1/16/2008 1:54:48 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class Page_PageTypeDAL is based upon Page_PageType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class Page_PageTypeDAL
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
	partial class Page_PageTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from Page_PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pagePageTypeID"></param>
		/// <returns>Object of type Page_PageType.</returns>
		public Page_PageType Page_PageTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pagePageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Page_PageTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PagePageTypeID", SqlDbType.Int, null, false, pagePageTypeID)))
			{
				using (CustomSqlHelper<Page_PageType> helper = new CustomSqlHelper<Page_PageType>())
				{
					CustomGenericList<Page_PageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Page_PageType o = list[0];
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
		/// Select values from Page_PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pagePageTypeID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> Page_PageTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pagePageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Page_PageTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PagePageTypeID", SqlDbType.Int, null, false, pagePageTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into Page_PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="barCode"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="pageTypeID"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="verified"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type Page_PageType.</returns>
		public Page_PageType Page_PageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string barCode,
			string fileNamePrefix,
			int? sequenceOrder,
			int pageTypeID,
			int importStatusID,
			int? importSourceID,
			bool verified,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Page_PageTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PagePageTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("FileNamePrefix", SqlDbType.NVarChar, 50, false, fileNamePrefix),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, true, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("PageTypeID", SqlDbType.Int, null, false, pageTypeID),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("Verified", SqlDbType.Bit, null, false, verified),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationUser", SqlDbType.Int, null, true, externalCreationUser),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedUser", SqlDbType.Int, null, true, externalLastModifiedUser),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Page_PageType> helper = new CustomSqlHelper<Page_PageType>())
				{
					CustomGenericList<Page_PageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Page_PageType o = list[0];
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
		/// Insert values into Page_PageType. Returns an object of type Page_PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Page_PageType.</param>
		/// <returns>Object of type Page_PageType.</returns>
		public Page_PageType Page_PageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Page_PageType value)
		{
			return Page_PageTypeInsertAuto(sqlConnection, sqlTransaction, 
				value.BarCode,
				value.FileNamePrefix,
				value.SequenceOrder,
				value.PageTypeID,
				value.ImportStatusID,
				value.ImportSourceID,
				value.Verified,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ExternalCreationUser,
				value.ExternalLastModifiedUser,
				value.ProductionDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Page_PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pagePageTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool Page_PageTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pagePageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Page_PageTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PagePageTypeID", SqlDbType.Int, null, false, pagePageTypeID), 
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
		/// Update values in Page_PageType. Returns an object of type Page_PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pagePageTypeID"></param>
		/// <param name="barCode"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="pageTypeID"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="verified"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type Page_PageType.</returns>
		public Page_PageType Page_PageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pagePageTypeID,
			string barCode,
			string fileNamePrefix,
			int? sequenceOrder,
			int pageTypeID,
			int importStatusID,
			int? importSourceID,
			bool verified,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Page_PageTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PagePageTypeID", SqlDbType.Int, null, false, pagePageTypeID),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("FileNamePrefix", SqlDbType.NVarChar, 50, false, fileNamePrefix),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, true, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("PageTypeID", SqlDbType.Int, null, false, pageTypeID),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("Verified", SqlDbType.Bit, null, false, verified),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationUser", SqlDbType.Int, null, true, externalCreationUser),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedUser", SqlDbType.Int, null, true, externalLastModifiedUser),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Page_PageType> helper = new CustomSqlHelper<Page_PageType>())
				{
					CustomGenericList<Page_PageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Page_PageType o = list[0];
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
		/// Update values in Page_PageType. Returns an object of type Page_PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Page_PageType.</param>
		/// <returns>Object of type Page_PageType.</returns>
		public Page_PageType Page_PageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Page_PageType value)
		{
			return Page_PageTypeUpdateAuto(sqlConnection, sqlTransaction,
				value.PagePageTypeID,
				value.BarCode,
				value.FileNamePrefix,
				value.SequenceOrder,
				value.PageTypeID,
				value.ImportStatusID,
				value.ImportSourceID,
				value.Verified,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ExternalCreationUser,
				value.ExternalLastModifiedUser,
				value.ProductionDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Page_PageType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Page_PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Page_PageType.</param>
		/// <returns>Object of type CustomDataAccessStatus<Page_PageType>.</returns>
		public CustomDataAccessStatus<Page_PageType> Page_PageTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Page_PageType value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Page_PageType returnValue = Page_PageTypeInsertAuto(sqlConnection, sqlTransaction, 
					value.BarCode,
						value.FileNamePrefix,
						value.SequenceOrder,
						value.PageTypeID,
						value.ImportStatusID,
						value.ImportSourceID,
						value.Verified,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ExternalCreationUser,
						value.ExternalLastModifiedUser,
						value.ProductionDate);
				
				return new CustomDataAccessStatus<Page_PageType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (Page_PageTypeDeleteAuto(sqlConnection, sqlTransaction, 
					value.PagePageTypeID))
				{
				return new CustomDataAccessStatus<Page_PageType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Page_PageType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Page_PageType returnValue = Page_PageTypeUpdateAuto(sqlConnection, sqlTransaction, 
					value.PagePageTypeID,
						value.BarCode,
						value.FileNamePrefix,
						value.SequenceOrder,
						value.PageTypeID,
						value.ImportStatusID,
						value.ImportSourceID,
						value.Verified,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ExternalCreationUser,
						value.ExternalLastModifiedUser,
						value.ProductionDate);
					
				return new CustomDataAccessStatus<Page_PageType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Page_PageType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

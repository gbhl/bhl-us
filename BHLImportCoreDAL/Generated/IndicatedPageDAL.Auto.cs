
// Generated 1/5/2021 2:16:27 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IndicatedPageDAL is based upon dbo.IndicatedPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IndicatedPageDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
{
	partial class IndicatedPageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="indicatedPageID"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int indicatedPageID)
		{
			return IndicatedPageSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	indicatedPageID );
		}
			
		/// <summary>
		/// Select values from dbo.IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="indicatedPageID"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int indicatedPageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("IndicatedPageID", SqlDbType.Int, null, false, indicatedPageID)))
			{
				using (CustomSqlHelper<IndicatedPage> helper = new CustomSqlHelper<IndicatedPage>())
				{
					List<IndicatedPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IndicatedPage o = list[0];
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
		/// Select values from dbo.IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="indicatedPageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IndicatedPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int indicatedPageID)
		{
			return IndicatedPageSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", indicatedPageID );
		}
		
		/// <summary>
		/// Select values from dbo.IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="indicatedPageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IndicatedPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int indicatedPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("IndicatedPageID", SqlDbType.Int, null, false, indicatedPageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="barCode"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="sequence"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string barCode,
			string fileNamePrefix,
			int? sequenceOrder,
			short? sequence,
			int importStatusID,
			int? importSourceID,
			string pagePrefix,
			string pageNumber,
			bool implied,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate)
		{
			return IndicatedPageInsertAuto( sqlConnection, sqlTransaction, "BHLImport", barCode, fileNamePrefix, sequenceOrder, sequence, importStatusID, importSourceID, pagePrefix, pageNumber, implied, externalCreationDate, externalLastModifiedDate, externalCreationUser, externalLastModifiedUser, productionDate );
		}
		
		/// <summary>
		/// Insert values into dbo.IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="barCode"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="sequence"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string barCode,
			string fileNamePrefix,
			int? sequenceOrder,
			short? sequence,
			int importStatusID,
			int? importSourceID,
			string pagePrefix,
			string pageNumber,
			bool implied,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("IndicatedPageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("FileNamePrefix", SqlDbType.NVarChar, 200, false, fileNamePrefix),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, true, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.SmallInt, null, true, sequence),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("PagePrefix", SqlDbType.NVarChar, 40, true, pagePrefix),
					CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, true, pageNumber),
					CustomSqlHelper.CreateInputParameter("Implied", SqlDbType.Bit, null, false, implied),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationUser", SqlDbType.Int, null, true, externalCreationUser),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedUser", SqlDbType.Int, null, true, externalLastModifiedUser),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IndicatedPage> helper = new CustomSqlHelper<IndicatedPage>())
				{
					List<IndicatedPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IndicatedPage o = list[0];
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
		/// Insert values into dbo.IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IndicatedPage value)
		{
			return IndicatedPageInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IndicatedPage value)
		{
			return IndicatedPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.BarCode,
				value.FileNamePrefix,
				value.SequenceOrder,
				value.Sequence,
				value.ImportStatusID,
				value.ImportSourceID,
				value.PagePrefix,
				value.PageNumber,
				value.Implied,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ExternalCreationUser,
				value.ExternalLastModifiedUser,
				value.ProductionDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="indicatedPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IndicatedPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int indicatedPageID)
		{
			return IndicatedPageDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", indicatedPageID );
		}
		
		/// <summary>
		/// Delete values from dbo.IndicatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="indicatedPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IndicatedPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int indicatedPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("IndicatedPageID", SqlDbType.Int, null, false, indicatedPageID), 
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
		/// Update values in dbo.IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="indicatedPageID"></param>
		/// <param name="barCode"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="sequence"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int indicatedPageID,
			string barCode,
			string fileNamePrefix,
			int? sequenceOrder,
			short? sequence,
			int importStatusID,
			int? importSourceID,
			string pagePrefix,
			string pageNumber,
			bool implied,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate)
		{
			return IndicatedPageUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", indicatedPageID, barCode, fileNamePrefix, sequenceOrder, sequence, importStatusID, importSourceID, pagePrefix, pageNumber, implied, externalCreationDate, externalLastModifiedDate, externalCreationUser, externalLastModifiedUser, productionDate);
		}
		
		/// <summary>
		/// Update values in dbo.IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="indicatedPageID"></param>
		/// <param name="barCode"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="sequence"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int indicatedPageID,
			string barCode,
			string fileNamePrefix,
			int? sequenceOrder,
			short? sequence,
			int importStatusID,
			int? importSourceID,
			string pagePrefix,
			string pageNumber,
			bool implied,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("IndicatedPageID", SqlDbType.Int, null, false, indicatedPageID),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
					CustomSqlHelper.CreateInputParameter("FileNamePrefix", SqlDbType.NVarChar, 200, false, fileNamePrefix),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, true, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.SmallInt, null, true, sequence),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("PagePrefix", SqlDbType.NVarChar, 40, true, pagePrefix),
					CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, true, pageNumber),
					CustomSqlHelper.CreateInputParameter("Implied", SqlDbType.Bit, null, false, implied),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationUser", SqlDbType.Int, null, true, externalCreationUser),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedUser", SqlDbType.Int, null, true, externalLastModifiedUser),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IndicatedPage> helper = new CustomSqlHelper<IndicatedPage>())
				{
					List<IndicatedPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IndicatedPage o = list[0];
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
		/// Update values in dbo.IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IndicatedPage value)
		{
			return IndicatedPageUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IndicatedPage. Returns an object of type IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type IndicatedPage.</returns>
		public IndicatedPage IndicatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IndicatedPage value)
		{
			return IndicatedPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.IndicatedPageID,
				value.BarCode,
				value.FileNamePrefix,
				value.SequenceOrder,
				value.Sequence,
				value.ImportStatusID,
				value.ImportSourceID,
				value.PagePrefix,
				value.PageNumber,
				value.Implied,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ExternalCreationUser,
				value.ExternalLastModifiedUser,
				value.ProductionDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IndicatedPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<IndicatedPage>.</returns>
		public CustomDataAccessStatus<IndicatedPage> IndicatedPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IndicatedPage value  )
		{
			return IndicatedPageManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IndicatedPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IndicatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IndicatedPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<IndicatedPage>.</returns>
		public CustomDataAccessStatus<IndicatedPage> IndicatedPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IndicatedPage value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IndicatedPage returnValue = IndicatedPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BarCode,
						value.FileNamePrefix,
						value.SequenceOrder,
						value.Sequence,
						value.ImportStatusID,
						value.ImportSourceID,
						value.PagePrefix,
						value.PageNumber,
						value.Implied,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ExternalCreationUser,
						value.ExternalLastModifiedUser,
						value.ProductionDate);
				
				return new CustomDataAccessStatus<IndicatedPage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IndicatedPageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.IndicatedPageID))
				{
				return new CustomDataAccessStatus<IndicatedPage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IndicatedPage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IndicatedPage returnValue = IndicatedPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.IndicatedPageID,
						value.BarCode,
						value.FileNamePrefix,
						value.SequenceOrder,
						value.Sequence,
						value.ImportStatusID,
						value.ImportSourceID,
						value.PagePrefix,
						value.PageNumber,
						value.Implied,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ExternalCreationUser,
						value.ExternalLastModifiedUser,
						value.ProductionDate);
					
				return new CustomDataAccessStatus<IndicatedPage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IndicatedPage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}



// Generated 7/14/2010 1:25:28 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotatedPageDAL is based upon AnnotatedPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotatedPageDAL
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
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	partial class AnnotatedPageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageID"></param>
		/// <returns>Object of type AnnotatedPage.</returns>
		public AnnotatedPage AnnotatedPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageID)
		{
			return AnnotatedPageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotatedPageID );
		}
			
		/// <summary>
		/// Select values from AnnotatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageID"></param>
		/// <returns>Object of type AnnotatedPage.</returns>
		public AnnotatedPage AnnotatedPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, false, annotatedPageID)))
			{
				using (CustomSqlHelper<AnnotatedPage> helper = new CustomSqlHelper<AnnotatedPage>())
				{
					List<AnnotatedPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedPage o = list[0];
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
		/// Select values from AnnotatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotatedPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageID)
		{
			return AnnotatedPageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotatedPageID );
		}
		
		/// <summary>
		/// Select values from AnnotatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotatedPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, false, annotatedPageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedItemID"></param>
		/// <param name="pageID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="annotatedPageTypeID"></param>
		/// <param name="pageNumber"></param>
		/// <returns>Object of type AnnotatedPage.</returns>
		public AnnotatedPage AnnotatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedItemID,
			int? pageID,
			string externalIdentifier,
			int annotatedPageTypeID,
			string pageNumber)
		{
			return AnnotatedPageInsertAuto( sqlConnection, sqlTransaction, "BHL", annotatedItemID, pageID, externalIdentifier, annotatedPageTypeID, pageNumber );
		}
		
		/// <summary>
		/// Insert values into AnnotatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedItemID"></param>
		/// <param name="pageID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="annotatedPageTypeID"></param>
		/// <param name="pageNumber"></param>
		/// <returns>Object of type AnnotatedPage.</returns>
		public AnnotatedPage AnnotatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedItemID,
			int? pageID,
			string externalIdentifier,
			int annotatedPageTypeID,
			string pageNumber)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AnnotatedPageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AnnotatedItemID", SqlDbType.Int, null, false, annotatedItemID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, true, pageID),
					CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
					CustomSqlHelper.CreateInputParameter("AnnotatedPageTypeID", SqlDbType.Int, null, false, annotatedPageTypeID),
					CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, false, pageNumber), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotatedPage> helper = new CustomSqlHelper<AnnotatedPage>())
				{
					List<AnnotatedPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedPage o = list[0];
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
		/// Insert values into AnnotatedPage. Returns an object of type AnnotatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedPage.</param>
		/// <returns>Object of type AnnotatedPage.</returns>
		public AnnotatedPage AnnotatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedPage value)
		{
			return AnnotatedPageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotatedPage. Returns an object of type AnnotatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedPage.</param>
		/// <returns>Object of type AnnotatedPage.</returns>
		public AnnotatedPage AnnotatedPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedPage value)
		{
			return AnnotatedPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotatedItemID,
				value.PageID,
				value.ExternalIdentifier,
				value.AnnotatedPageTypeID,
				value.PageNumber);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotatedPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageID)
		{
			return AnnotatedPageDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotatedPageID );
		}
		
		/// <summary>
		/// Delete values from AnnotatedPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotatedPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, false, annotatedPageID), 
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
		/// Update values in AnnotatedPage. Returns an object of type AnnotatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotatedItemID"></param>
		/// <param name="pageID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="annotatedPageTypeID"></param>
		/// <param name="pageNumber"></param>
		/// <returns>Object of type AnnotatedPage.</returns>
		public AnnotatedPage AnnotatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageID,
			int annotatedItemID,
			int? pageID,
			string externalIdentifier,
			int annotatedPageTypeID,
			string pageNumber)
		{
			return AnnotatedPageUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotatedPageID, annotatedItemID, pageID, externalIdentifier, annotatedPageTypeID, pageNumber);
		}
		
		/// <summary>
		/// Update values in AnnotatedPage. Returns an object of type AnnotatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotatedItemID"></param>
		/// <param name="pageID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="annotatedPageTypeID"></param>
		/// <param name="pageNumber"></param>
		/// <returns>Object of type AnnotatedPage.</returns>
		public AnnotatedPage AnnotatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageID,
			int annotatedItemID,
			int? pageID,
			string externalIdentifier,
			int annotatedPageTypeID,
			string pageNumber)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, false, annotatedPageID),
					CustomSqlHelper.CreateInputParameter("AnnotatedItemID", SqlDbType.Int, null, false, annotatedItemID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, true, pageID),
					CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
					CustomSqlHelper.CreateInputParameter("AnnotatedPageTypeID", SqlDbType.Int, null, false, annotatedPageTypeID),
					CustomSqlHelper.CreateInputParameter("PageNumber", SqlDbType.NVarChar, 20, false, pageNumber), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotatedPage> helper = new CustomSqlHelper<AnnotatedPage>())
				{
					List<AnnotatedPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedPage o = list[0];
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
		/// Update values in AnnotatedPage. Returns an object of type AnnotatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedPage.</param>
		/// <returns>Object of type AnnotatedPage.</returns>
		public AnnotatedPage AnnotatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedPage value)
		{
			return AnnotatedPageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotatedPage. Returns an object of type AnnotatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedPage.</param>
		/// <returns>Object of type AnnotatedPage.</returns>
		public AnnotatedPage AnnotatedPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedPage value)
		{
			return AnnotatedPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotatedPageID,
				value.AnnotatedItemID,
				value.PageID,
				value.ExternalIdentifier,
				value.AnnotatedPageTypeID,
				value.PageNumber);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotatedPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotatedPage>.</returns>
		public CustomDataAccessStatus<AnnotatedPage> AnnotatedPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedPage value  )
		{
			return AnnotatedPageManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotatedPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotatedPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotatedPage>.</returns>
		public CustomDataAccessStatus<AnnotatedPage> AnnotatedPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedPage value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotatedPage returnValue = AnnotatedPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedItemID,
						value.PageID,
						value.ExternalIdentifier,
						value.AnnotatedPageTypeID,
						value.PageNumber);
				
				return new CustomDataAccessStatus<AnnotatedPage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotatedPageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedPageID))
				{
				return new CustomDataAccessStatus<AnnotatedPage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotatedPage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotatedPage returnValue = AnnotatedPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedPageID,
						value.AnnotatedItemID,
						value.PageID,
						value.ExternalIdentifier,
						value.AnnotatedPageTypeID,
						value.PageNumber);
					
				return new CustomDataAccessStatus<AnnotatedPage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotatedPage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

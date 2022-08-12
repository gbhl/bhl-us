
// Generated 1/5/2021 3:26:47 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PDFPageDAL is based upon dbo.PDFPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class PDFPageDAL
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
	partial class PDFPageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.PDFPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfPageID"></param>
		/// <returns>Object of type PDFPage.</returns>
		public PDFPage PDFPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfPageID)
		{
			return PDFPageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	pdfPageID );
		}
			
		/// <summary>
		/// Select values from dbo.PDFPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfPageID"></param>
		/// <returns>Object of type PDFPage.</returns>
		public PDFPage PDFPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfPageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFPageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PdfPageID", SqlDbType.Int, null, false, pdfPageID)))
			{
				using (CustomSqlHelper<PDFPage> helper = new CustomSqlHelper<PDFPage>())
				{
					List<PDFPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PDFPage o = list[0];
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
		/// Select values from dbo.PDFPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfPageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PDFPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfPageID)
		{
			return PDFPageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", pdfPageID );
		}
		
		/// <summary>
		/// Select values from dbo.PDFPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfPageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PDFPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFPageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PdfPageID", SqlDbType.Int, null, false, pdfPageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.PDFPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfID"></param>
		/// <param name="pageID"></param>
		/// <returns>Object of type PDFPage.</returns>
		public PDFPage PDFPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfID,
			int pageID)
		{
			return PDFPageInsertAuto( sqlConnection, sqlTransaction, "BHL", pdfID, pageID );
		}
		
		/// <summary>
		/// Insert values into dbo.PDFPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfID"></param>
		/// <param name="pageID"></param>
		/// <returns>Object of type PDFPage.</returns>
		public PDFPage PDFPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfID,
			int pageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFPageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PdfPageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, pdfID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PDFPage> helper = new CustomSqlHelper<PDFPage>())
				{
					List<PDFPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PDFPage o = list[0];
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
		/// Insert values into dbo.PDFPage. Returns an object of type PDFPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PDFPage.</param>
		/// <returns>Object of type PDFPage.</returns>
		public PDFPage PDFPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PDFPage value)
		{
			return PDFPageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.PDFPage. Returns an object of type PDFPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PDFPage.</param>
		/// <returns>Object of type PDFPage.</returns>
		public PDFPage PDFPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PDFPage value)
		{
			return PDFPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PdfID,
				value.PageID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.PDFPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PDFPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfPageID)
		{
			return PDFPageDeleteAuto( sqlConnection, sqlTransaction, "BHL", pdfPageID );
		}
		
		/// <summary>
		/// Delete values from dbo.PDFPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PDFPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFPageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PdfPageID", SqlDbType.Int, null, false, pdfPageID), 
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
		/// Update values in dbo.PDFPage. Returns an object of type PDFPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfPageID"></param>
		/// <param name="pdfID"></param>
		/// <param name="pageID"></param>
		/// <returns>Object of type PDFPage.</returns>
		public PDFPage PDFPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfPageID,
			int pdfID,
			int pageID)
		{
			return PDFPageUpdateAuto( sqlConnection, sqlTransaction, "BHL", pdfPageID, pdfID, pageID);
		}
		
		/// <summary>
		/// Update values in dbo.PDFPage. Returns an object of type PDFPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfPageID"></param>
		/// <param name="pdfID"></param>
		/// <param name="pageID"></param>
		/// <returns>Object of type PDFPage.</returns>
		public PDFPage PDFPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfPageID,
			int pdfID,
			int pageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFPageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PdfPageID", SqlDbType.Int, null, false, pdfPageID),
					CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, pdfID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PDFPage> helper = new CustomSqlHelper<PDFPage>())
				{
					List<PDFPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PDFPage o = list[0];
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
		/// Update values in dbo.PDFPage. Returns an object of type PDFPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PDFPage.</param>
		/// <returns>Object of type PDFPage.</returns>
		public PDFPage PDFPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PDFPage value)
		{
			return PDFPageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.PDFPage. Returns an object of type PDFPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PDFPage.</param>
		/// <returns>Object of type PDFPage.</returns>
		public PDFPage PDFPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PDFPage value)
		{
			return PDFPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PdfPageID,
				value.PdfID,
				value.PageID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.PDFPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PDFPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PDFPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<PDFPage>.</returns>
		public CustomDataAccessStatus<PDFPage> PDFPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PDFPage value  )
		{
			return PDFPageManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.PDFPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PDFPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PDFPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<PDFPage>.</returns>
		public CustomDataAccessStatus<PDFPage> PDFPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PDFPage value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				PDFPage returnValue = PDFPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PdfID,
						value.PageID);
				
				return new CustomDataAccessStatus<PDFPage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PDFPageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PdfPageID))
				{
				return new CustomDataAccessStatus<PDFPage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<PDFPage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				PDFPage returnValue = PDFPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PdfPageID,
						value.PdfID,
						value.PageID);
					
				return new CustomDataAccessStatus<PDFPage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<PDFPage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}



// Generated 1/5/2021 3:26:49 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PDFStatusDAL is based upon dbo.PDFStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class PDFStatusDAL
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
	partial class PDFStatusDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.PDFStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfStatusID"></param>
		/// <returns>Object of type PDFStatus.</returns>
		public PDFStatus PDFStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfStatusID)
		{
			return PDFStatusSelectAuto(	sqlConnection, sqlTransaction, "BHL",	pdfStatusID );
		}
			
		/// <summary>
		/// Select values from dbo.PDFStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfStatusID"></param>
		/// <returns>Object of type PDFStatus.</returns>
		public PDFStatus PDFStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfStatusID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFStatusSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PdfStatusID", SqlDbType.Int, null, false, pdfStatusID)))
			{
				using (CustomSqlHelper<PDFStatus> helper = new CustomSqlHelper<PDFStatus>())
				{
					List<PDFStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PDFStatus o = list[0];
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
		/// Select values from dbo.PDFStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PDFStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfStatusID)
		{
			return PDFStatusSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", pdfStatusID );
		}
		
		/// <summary>
		/// Select values from dbo.PDFStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PDFStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFStatusSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PdfStatusID", SqlDbType.Int, null, false, pdfStatusID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.PDFStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfStatusID"></param>
		/// <param name="pdfStatusName"></param>
		/// <returns>Object of type PDFStatus.</returns>
		public PDFStatus PDFStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfStatusID,
			string pdfStatusName)
		{
			return PDFStatusInsertAuto( sqlConnection, sqlTransaction, "BHL", pdfStatusID, pdfStatusName );
		}
		
		/// <summary>
		/// Insert values into dbo.PDFStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfStatusID"></param>
		/// <param name="pdfStatusName"></param>
		/// <returns>Object of type PDFStatus.</returns>
		public PDFStatus PDFStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfStatusID,
			string pdfStatusName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFStatusInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PdfStatusID", SqlDbType.Int, null, false, pdfStatusID),
					CustomSqlHelper.CreateInputParameter("PdfStatusName", SqlDbType.NChar, 10, false, pdfStatusName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PDFStatus> helper = new CustomSqlHelper<PDFStatus>())
				{
					List<PDFStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PDFStatus o = list[0];
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
		/// Insert values into dbo.PDFStatus. Returns an object of type PDFStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PDFStatus.</param>
		/// <returns>Object of type PDFStatus.</returns>
		public PDFStatus PDFStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PDFStatus value)
		{
			return PDFStatusInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.PDFStatus. Returns an object of type PDFStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PDFStatus.</param>
		/// <returns>Object of type PDFStatus.</returns>
		public PDFStatus PDFStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PDFStatus value)
		{
			return PDFStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PdfStatusID,
				value.PdfStatusName);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.PDFStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PDFStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfStatusID)
		{
			return PDFStatusDeleteAuto( sqlConnection, sqlTransaction, "BHL", pdfStatusID );
		}
		
		/// <summary>
		/// Delete values from dbo.PDFStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PDFStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFStatusDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PdfStatusID", SqlDbType.Int, null, false, pdfStatusID), 
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
		/// Update values in dbo.PDFStatus. Returns an object of type PDFStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfStatusID"></param>
		/// <param name="pdfStatusName"></param>
		/// <returns>Object of type PDFStatus.</returns>
		public PDFStatus PDFStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfStatusID,
			string pdfStatusName)
		{
			return PDFStatusUpdateAuto( sqlConnection, sqlTransaction, "BHL", pdfStatusID, pdfStatusName);
		}
		
		/// <summary>
		/// Update values in dbo.PDFStatus. Returns an object of type PDFStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfStatusID"></param>
		/// <param name="pdfStatusName"></param>
		/// <returns>Object of type PDFStatus.</returns>
		public PDFStatus PDFStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfStatusID,
			string pdfStatusName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFStatusUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PdfStatusID", SqlDbType.Int, null, false, pdfStatusID),
					CustomSqlHelper.CreateInputParameter("PdfStatusName", SqlDbType.NChar, 10, false, pdfStatusName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PDFStatus> helper = new CustomSqlHelper<PDFStatus>())
				{
					List<PDFStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PDFStatus o = list[0];
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
		/// Update values in dbo.PDFStatus. Returns an object of type PDFStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PDFStatus.</param>
		/// <returns>Object of type PDFStatus.</returns>
		public PDFStatus PDFStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PDFStatus value)
		{
			return PDFStatusUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.PDFStatus. Returns an object of type PDFStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PDFStatus.</param>
		/// <returns>Object of type PDFStatus.</returns>
		public PDFStatus PDFStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PDFStatus value)
		{
			return PDFStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PdfStatusID,
				value.PdfStatusName);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.PDFStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PDFStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PDFStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<PDFStatus>.</returns>
		public CustomDataAccessStatus<PDFStatus> PDFStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PDFStatus value  )
		{
			return PDFStatusManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.PDFStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PDFStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PDFStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<PDFStatus>.</returns>
		public CustomDataAccessStatus<PDFStatus> PDFStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PDFStatus value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				PDFStatus returnValue = PDFStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PdfStatusID,
						value.PdfStatusName);
				
				return new CustomDataAccessStatus<PDFStatus>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PDFStatusDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PdfStatusID))
				{
				return new CustomDataAccessStatus<PDFStatus>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<PDFStatus>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				PDFStatus returnValue = PDFStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PdfStatusID,
						value.PdfStatusName);
					
				return new CustomDataAccessStatus<PDFStatus>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<PDFStatus>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


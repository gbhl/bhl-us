
// Generated 5/11/2010 1:52:21 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PageAnnotationDAL is based upon PageAnnotation.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class PageAnnotationDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	partial class PageAnnotationDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from PageAnnotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotationID"></param>
		/// <returns>Object of type PageAnnotation.</returns>
		public PageAnnotation PageAnnotationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageID,
			int annotationID)
		{
			return PageAnnotationSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotatedPageID, annotationID );
		}
			
		/// <summary>
		/// Select values from PageAnnotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotationID"></param>
		/// <returns>Object of type PageAnnotation.</returns>
		public PageAnnotation PageAnnotationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageID,
			int annotationID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.PageAnnotationSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, false, annotatedPageID),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID)))
			{
				using (CustomSqlHelper<PageAnnotation> helper = new CustomSqlHelper<PageAnnotation>())
				{
					CustomGenericList<PageAnnotation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageAnnotation o = list[0];
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
		/// Select values from PageAnnotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotationID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> PageAnnotationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageID,
			int annotationID)
		{
			return PageAnnotationSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotatedPageID, annotationID );
		}
		
		/// <summary>
		/// Select values from PageAnnotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotationID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> PageAnnotationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageID,
			int annotationID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.PageAnnotationSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, false, annotatedPageID),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into PageAnnotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotationID"></param>
		/// <param name="pageColumn"></param>
		/// <returns>Object of type PageAnnotation.</returns>
		public PageAnnotation PageAnnotationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageID,
			int annotationID,
			string pageColumn)
		{
			return PageAnnotationInsertAuto( sqlConnection, sqlTransaction, "BHL", annotatedPageID, annotationID, pageColumn );
		}
		
		/// <summary>
		/// Insert values into PageAnnotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotationID"></param>
		/// <param name="pageColumn"></param>
		/// <returns>Object of type PageAnnotation.</returns>
		public PageAnnotation PageAnnotationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageID,
			int annotationID,
			string pageColumn)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.PageAnnotationInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, false, annotatedPageID),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("PageColumn", SqlDbType.NVarChar, 20, false, pageColumn), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageAnnotation> helper = new CustomSqlHelper<PageAnnotation>())
				{
					CustomGenericList<PageAnnotation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageAnnotation o = list[0];
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
		/// Insert values into PageAnnotation. Returns an object of type PageAnnotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageAnnotation.</param>
		/// <returns>Object of type PageAnnotation.</returns>
		public PageAnnotation PageAnnotationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageAnnotation value)
		{
			return PageAnnotationInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into PageAnnotation. Returns an object of type PageAnnotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageAnnotation.</param>
		/// <returns>Object of type PageAnnotation.</returns>
		public PageAnnotation PageAnnotationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageAnnotation value)
		{
			return PageAnnotationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotatedPageID,
				value.AnnotationID,
				value.PageColumn);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from PageAnnotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotationID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageAnnotationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageID,
			int annotationID)
		{
			return PageAnnotationDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotatedPageID, annotationID );
		}
		
		/// <summary>
		/// Delete values from PageAnnotation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotationID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageAnnotationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageID,
			int annotationID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.PageAnnotationDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, false, annotatedPageID),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID), 
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
		/// Update values in PageAnnotation. Returns an object of type PageAnnotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotationID"></param>
		/// <param name="pageColumn"></param>
		/// <returns>Object of type PageAnnotation.</returns>
		public PageAnnotation PageAnnotationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageID,
			int annotationID,
			string pageColumn)
		{
			return PageAnnotationUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotatedPageID, annotationID, pageColumn);
		}
		
		/// <summary>
		/// Update values in PageAnnotation. Returns an object of type PageAnnotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotationID"></param>
		/// <param name="pageColumn"></param>
		/// <returns>Object of type PageAnnotation.</returns>
		public PageAnnotation PageAnnotationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageID,
			int annotationID,
			string pageColumn)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.PageAnnotationUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, false, annotatedPageID),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("PageColumn", SqlDbType.NVarChar, 20, false, pageColumn), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageAnnotation> helper = new CustomSqlHelper<PageAnnotation>())
				{
					CustomGenericList<PageAnnotation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageAnnotation o = list[0];
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
		/// Update values in PageAnnotation. Returns an object of type PageAnnotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageAnnotation.</param>
		/// <returns>Object of type PageAnnotation.</returns>
		public PageAnnotation PageAnnotationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageAnnotation value)
		{
			return PageAnnotationUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in PageAnnotation. Returns an object of type PageAnnotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageAnnotation.</param>
		/// <returns>Object of type PageAnnotation.</returns>
		public PageAnnotation PageAnnotationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageAnnotation value)
		{
			return PageAnnotationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotatedPageID,
				value.AnnotationID,
				value.PageColumn);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage PageAnnotation object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in PageAnnotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageAnnotation.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageAnnotation>.</returns>
		public CustomDataAccessStatus<PageAnnotation> PageAnnotationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageAnnotation value  )
		{
			return PageAnnotationManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage PageAnnotation object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in PageAnnotation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageAnnotation.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageAnnotation>.</returns>
		public CustomDataAccessStatus<PageAnnotation> PageAnnotationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageAnnotation value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				PageAnnotation returnValue = PageAnnotationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedPageID,
						value.AnnotationID,
						value.PageColumn);
				
				return new CustomDataAccessStatus<PageAnnotation>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PageAnnotationDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedPageID,
						value.AnnotationID))
				{
				return new CustomDataAccessStatus<PageAnnotation>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<PageAnnotation>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				PageAnnotation returnValue = PageAnnotationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedPageID,
						value.AnnotationID,
						value.PageColumn);
					
				return new CustomDataAccessStatus<PageAnnotation>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<PageAnnotation>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

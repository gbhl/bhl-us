
// Generated 1/5/2021 3:26:45 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PDFDAL is based upon dbo.PDF.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class PDFDAL
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
	partial class PDFDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.PDF by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfID"></param>
		/// <returns>Object of type PDF.</returns>
		public PDF PDFSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfID)
		{
			return PDFSelectAuto(	sqlConnection, sqlTransaction, "BHL",	pdfID );
		}
			
		/// <summary>
		/// Select values from dbo.PDF by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfID"></param>
		/// <returns>Object of type PDF.</returns>
		public PDF PDFSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, pdfID)))
			{
				using (CustomSqlHelper<PDF> helper = new CustomSqlHelper<PDF>())
				{
					List<PDF> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PDF o = list[0];
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
		/// Select values from dbo.PDF by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PDFSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfID)
		{
			return PDFSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", pdfID );
		}
		
		/// <summary>
		/// Select values from dbo.PDF by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PDFSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, pdfID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.PDF.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="fileLocation"></param>
		/// <param name="emailAddress"></param>
		/// <param name="shareWithEmailAddresses"></param>
		/// <param name="articleTitle"></param>
		/// <param name="articleCreators"></param>
		/// <param name="articleTags"></param>
		/// <param name="imagesOnly"></param>
		/// <param name="fileUrl"></param>
		/// <param name="fileGenerationDate"></param>
		/// <param name="fileDeletionDate"></param>
		/// <param name="pdfStatusID"></param>
		/// <param name="numberImagesMissing"></param>
		/// <param name="numberOcrMissing"></param>
		/// <param name="comment"></param>
		/// <returns>Object of type PDF.</returns>
		public PDF PDFInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string fileLocation,
			string emailAddress,
			string shareWithEmailAddresses,
			string articleTitle,
			string articleCreators,
			string articleTags,
			bool imagesOnly,
			string fileUrl,
			DateTime? fileGenerationDate,
			DateTime? fileDeletionDate,
			int pdfStatusID,
			int numberImagesMissing,
			int numberOcrMissing,
			string comment)
		{
			return PDFInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, fileLocation, emailAddress, shareWithEmailAddresses, articleTitle, articleCreators, articleTags, imagesOnly, fileUrl, fileGenerationDate, fileDeletionDate, pdfStatusID, numberImagesMissing, numberOcrMissing, comment );
		}
		
		/// <summary>
		/// Insert values into dbo.PDF.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="fileLocation"></param>
		/// <param name="emailAddress"></param>
		/// <param name="shareWithEmailAddresses"></param>
		/// <param name="articleTitle"></param>
		/// <param name="articleCreators"></param>
		/// <param name="articleTags"></param>
		/// <param name="imagesOnly"></param>
		/// <param name="fileUrl"></param>
		/// <param name="fileGenerationDate"></param>
		/// <param name="fileDeletionDate"></param>
		/// <param name="pdfStatusID"></param>
		/// <param name="numberImagesMissing"></param>
		/// <param name="numberOcrMissing"></param>
		/// <param name="comment"></param>
		/// <returns>Object of type PDF.</returns>
		public PDF PDFInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string fileLocation,
			string emailAddress,
			string shareWithEmailAddresses,
			string articleTitle,
			string articleCreators,
			string articleTags,
			bool imagesOnly,
			string fileUrl,
			DateTime? fileGenerationDate,
			DateTime? fileDeletionDate,
			int pdfStatusID,
			int numberImagesMissing,
			int numberOcrMissing,
			string comment)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PdfID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("FileLocation", SqlDbType.NVarChar, 200, false, fileLocation),
					CustomSqlHelper.CreateInputParameter("EmailAddress", SqlDbType.NVarChar, 200, false, emailAddress),
					CustomSqlHelper.CreateInputParameter("ShareWithEmailAddresses", SqlDbType.NVarChar, 1073741823, false, shareWithEmailAddresses),
					CustomSqlHelper.CreateInputParameter("ArticleTitle", SqlDbType.NVarChar, 1073741823, false, articleTitle),
					CustomSqlHelper.CreateInputParameter("ArticleCreators", SqlDbType.NVarChar, 1073741823, false, articleCreators),
					CustomSqlHelper.CreateInputParameter("ArticleTags", SqlDbType.NVarChar, 1073741823, false, articleTags),
					CustomSqlHelper.CreateInputParameter("ImagesOnly", SqlDbType.Bit, null, false, imagesOnly),
					CustomSqlHelper.CreateInputParameter("FileUrl", SqlDbType.NVarChar, 200, false, fileUrl),
					CustomSqlHelper.CreateInputParameter("FileGenerationDate", SqlDbType.DateTime, null, true, fileGenerationDate),
					CustomSqlHelper.CreateInputParameter("FileDeletionDate", SqlDbType.DateTime, null, true, fileDeletionDate),
					CustomSqlHelper.CreateInputParameter("PdfStatusID", SqlDbType.Int, null, false, pdfStatusID),
					CustomSqlHelper.CreateInputParameter("NumberImagesMissing", SqlDbType.Int, null, false, numberImagesMissing),
					CustomSqlHelper.CreateInputParameter("NumberOcrMissing", SqlDbType.Int, null, false, numberOcrMissing),
					CustomSqlHelper.CreateInputParameter("Comment", SqlDbType.NVarChar, 1073741823, false, comment), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PDF> helper = new CustomSqlHelper<PDF>())
				{
					List<PDF> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PDF o = list[0];
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
		/// Insert values into dbo.PDF. Returns an object of type PDF.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PDF.</param>
		/// <returns>Object of type PDF.</returns>
		public PDF PDFInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PDF value)
		{
			return PDFInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.PDF. Returns an object of type PDF.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PDF.</param>
		/// <returns>Object of type PDF.</returns>
		public PDF PDFInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PDF value)
		{
			return PDFInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.FileLocation,
				value.EmailAddress,
				value.ShareWithEmailAddresses,
				value.ArticleTitle,
				value.ArticleCreators,
				value.ArticleTags,
				value.ImagesOnly,
				value.FileUrl,
				value.FileGenerationDate,
				value.FileDeletionDate,
				value.PdfStatusID,
				value.NumberImagesMissing,
				value.NumberOcrMissing,
				value.Comment);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.PDF by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PDFDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfID)
		{
			return PDFDeleteAuto( sqlConnection, sqlTransaction, "BHL", pdfID );
		}
		
		/// <summary>
		/// Delete values from dbo.PDF by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PDFDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, pdfID), 
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
		/// Update values in dbo.PDF. Returns an object of type PDF.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pdfID"></param>
		/// <param name="itemID"></param>
		/// <param name="fileLocation"></param>
		/// <param name="emailAddress"></param>
		/// <param name="shareWithEmailAddresses"></param>
		/// <param name="articleTitle"></param>
		/// <param name="articleCreators"></param>
		/// <param name="articleTags"></param>
		/// <param name="imagesOnly"></param>
		/// <param name="fileUrl"></param>
		/// <param name="fileGenerationDate"></param>
		/// <param name="fileDeletionDate"></param>
		/// <param name="pdfStatusID"></param>
		/// <param name="numberImagesMissing"></param>
		/// <param name="numberOcrMissing"></param>
		/// <param name="comment"></param>
		/// <returns>Object of type PDF.</returns>
		public PDF PDFUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pdfID,
			int itemID,
			string fileLocation,
			string emailAddress,
			string shareWithEmailAddresses,
			string articleTitle,
			string articleCreators,
			string articleTags,
			bool imagesOnly,
			string fileUrl,
			DateTime? fileGenerationDate,
			DateTime? fileDeletionDate,
			int pdfStatusID,
			int numberImagesMissing,
			int numberOcrMissing,
			string comment)
		{
			return PDFUpdateAuto( sqlConnection, sqlTransaction, "BHL", pdfID, itemID, fileLocation, emailAddress, shareWithEmailAddresses, articleTitle, articleCreators, articleTags, imagesOnly, fileUrl, fileGenerationDate, fileDeletionDate, pdfStatusID, numberImagesMissing, numberOcrMissing, comment);
		}
		
		/// <summary>
		/// Update values in dbo.PDF. Returns an object of type PDF.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pdfID"></param>
		/// <param name="itemID"></param>
		/// <param name="fileLocation"></param>
		/// <param name="emailAddress"></param>
		/// <param name="shareWithEmailAddresses"></param>
		/// <param name="articleTitle"></param>
		/// <param name="articleCreators"></param>
		/// <param name="articleTags"></param>
		/// <param name="imagesOnly"></param>
		/// <param name="fileUrl"></param>
		/// <param name="fileGenerationDate"></param>
		/// <param name="fileDeletionDate"></param>
		/// <param name="pdfStatusID"></param>
		/// <param name="numberImagesMissing"></param>
		/// <param name="numberOcrMissing"></param>
		/// <param name="comment"></param>
		/// <returns>Object of type PDF.</returns>
		public PDF PDFUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pdfID,
			int itemID,
			string fileLocation,
			string emailAddress,
			string shareWithEmailAddresses,
			string articleTitle,
			string articleCreators,
			string articleTags,
			bool imagesOnly,
			string fileUrl,
			DateTime? fileGenerationDate,
			DateTime? fileDeletionDate,
			int pdfStatusID,
			int numberImagesMissing,
			int numberOcrMissing,
			string comment)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, pdfID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("FileLocation", SqlDbType.NVarChar, 200, false, fileLocation),
					CustomSqlHelper.CreateInputParameter("EmailAddress", SqlDbType.NVarChar, 200, false, emailAddress),
					CustomSqlHelper.CreateInputParameter("ShareWithEmailAddresses", SqlDbType.NVarChar, 1073741823, false, shareWithEmailAddresses),
					CustomSqlHelper.CreateInputParameter("ArticleTitle", SqlDbType.NVarChar, 1073741823, false, articleTitle),
					CustomSqlHelper.CreateInputParameter("ArticleCreators", SqlDbType.NVarChar, 1073741823, false, articleCreators),
					CustomSqlHelper.CreateInputParameter("ArticleTags", SqlDbType.NVarChar, 1073741823, false, articleTags),
					CustomSqlHelper.CreateInputParameter("ImagesOnly", SqlDbType.Bit, null, false, imagesOnly),
					CustomSqlHelper.CreateInputParameter("FileUrl", SqlDbType.NVarChar, 200, false, fileUrl),
					CustomSqlHelper.CreateInputParameter("FileGenerationDate", SqlDbType.DateTime, null, true, fileGenerationDate),
					CustomSqlHelper.CreateInputParameter("FileDeletionDate", SqlDbType.DateTime, null, true, fileDeletionDate),
					CustomSqlHelper.CreateInputParameter("PdfStatusID", SqlDbType.Int, null, false, pdfStatusID),
					CustomSqlHelper.CreateInputParameter("NumberImagesMissing", SqlDbType.Int, null, false, numberImagesMissing),
					CustomSqlHelper.CreateInputParameter("NumberOcrMissing", SqlDbType.Int, null, false, numberOcrMissing),
					CustomSqlHelper.CreateInputParameter("Comment", SqlDbType.NVarChar, 1073741823, false, comment), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PDF> helper = new CustomSqlHelper<PDF>())
				{
					List<PDF> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PDF o = list[0];
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
		/// Update values in dbo.PDF. Returns an object of type PDF.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PDF.</param>
		/// <returns>Object of type PDF.</returns>
		public PDF PDFUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PDF value)
		{
			return PDFUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.PDF. Returns an object of type PDF.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PDF.</param>
		/// <returns>Object of type PDF.</returns>
		public PDF PDFUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PDF value)
		{
			return PDFUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PdfID,
				value.ItemID,
				value.FileLocation,
				value.EmailAddress,
				value.ShareWithEmailAddresses,
				value.ArticleTitle,
				value.ArticleCreators,
				value.ArticleTags,
				value.ImagesOnly,
				value.FileUrl,
				value.FileGenerationDate,
				value.FileDeletionDate,
				value.PdfStatusID,
				value.NumberImagesMissing,
				value.NumberOcrMissing,
				value.Comment);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.PDF object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PDF.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PDF.</param>
		/// <returns>Object of type CustomDataAccessStatus<PDF>.</returns>
		public CustomDataAccessStatus<PDF> PDFManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PDF value  )
		{
			return PDFManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.PDF object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PDF.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PDF.</param>
		/// <returns>Object of type CustomDataAccessStatus<PDF>.</returns>
		public CustomDataAccessStatus<PDF> PDFManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PDF value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				PDF returnValue = PDFInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.FileLocation,
						value.EmailAddress,
						value.ShareWithEmailAddresses,
						value.ArticleTitle,
						value.ArticleCreators,
						value.ArticleTags,
						value.ImagesOnly,
						value.FileUrl,
						value.FileGenerationDate,
						value.FileDeletionDate,
						value.PdfStatusID,
						value.NumberImagesMissing,
						value.NumberOcrMissing,
						value.Comment);
				
				return new CustomDataAccessStatus<PDF>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PDFDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PdfID))
				{
				return new CustomDataAccessStatus<PDF>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<PDF>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				PDF returnValue = PDFUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PdfID,
						value.ItemID,
						value.FileLocation,
						value.EmailAddress,
						value.ShareWithEmailAddresses,
						value.ArticleTitle,
						value.ArticleCreators,
						value.ArticleTags,
						value.ImagesOnly,
						value.FileUrl,
						value.FileGenerationDate,
						value.FileDeletionDate,
						value.PdfStatusID,
						value.NumberImagesMissing,
						value.NumberOcrMissing,
						value.Comment);
					
				return new CustomDataAccessStatus<PDF>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<PDF>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}


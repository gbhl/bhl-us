
// Generated 7/14/2010 1:25:28 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotatedTitleDAL is based upon AnnotatedTitle.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotatedTitleDAL
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
	partial class AnnotatedTitleDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedTitleID"></param>
		/// <returns>Object of type AnnotatedTitle.</returns>
		public AnnotatedTitle AnnotatedTitleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedTitleID)
		{
			return AnnotatedTitleSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotatedTitleID );
		}
			
		/// <summary>
		/// Select values from AnnotatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedTitleID"></param>
		/// <returns>Object of type AnnotatedTitle.</returns>
		public AnnotatedTitle AnnotatedTitleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedTitleID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedTitleSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedTitleID", SqlDbType.Int, null, false, annotatedTitleID)))
			{
				using (CustomSqlHelper<AnnotatedTitle> helper = new CustomSqlHelper<AnnotatedTitle>())
				{
					CustomGenericList<AnnotatedTitle> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedTitle o = list[0];
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
		/// Select values from AnnotatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedTitleID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AnnotatedTitleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedTitleID)
		{
			return AnnotatedTitleSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotatedTitleID );
		}
		
		/// <summary>
		/// Select values from AnnotatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedTitleID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AnnotatedTitleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedTitleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedTitleSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotatedTitleID", SqlDbType.Int, null, false, annotatedTitleID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationSourceID"></param>
		/// <param name="titleID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="author"></param>
		/// <param name="title"></param>
		/// <param name="edition"></param>
		/// <param name="volume"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="date"></param>
		/// <param name="location"></param>
		/// <param name="isBeagleEra"></param>
		/// <param name="inscription"></param>
		/// <returns>Object of type AnnotatedTitle.</returns>
		public AnnotatedTitle AnnotatedTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationSourceID,
			int? titleID,
			string externalIdentifier,
			string author,
			string title,
			string edition,
			string volume,
			string publicationDetails,
			string date,
			string location,
			string isBeagleEra,
			string inscription)
		{
			return AnnotatedTitleInsertAuto( sqlConnection, sqlTransaction, "BHL", annotationSourceID, titleID, externalIdentifier, author, title, edition, volume, publicationDetails, date, location, isBeagleEra, inscription );
		}
		
		/// <summary>
		/// Insert values into AnnotatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationSourceID"></param>
		/// <param name="titleID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="author"></param>
		/// <param name="title"></param>
		/// <param name="edition"></param>
		/// <param name="volume"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="date"></param>
		/// <param name="location"></param>
		/// <param name="isBeagleEra"></param>
		/// <param name="inscription"></param>
		/// <returns>Object of type AnnotatedTitle.</returns>
		public AnnotatedTitle AnnotatedTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationSourceID,
			int? titleID,
			string externalIdentifier,
			string author,
			string title,
			string edition,
			string volume,
			string publicationDetails,
			string date,
			string location,
			string isBeagleEra,
			string inscription)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedTitleInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AnnotatedTitleID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, true, titleID),
					CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
					CustomSqlHelper.CreateInputParameter("Author", SqlDbType.NVarChar, 100, false, author),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 200, false, title),
					CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 50, false, edition),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 50, false, volume),
					CustomSqlHelper.CreateInputParameter("PublicationDetails", SqlDbType.NVarChar, 100, false, publicationDetails),
					CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
					CustomSqlHelper.CreateInputParameter("Location", SqlDbType.NVarChar, 50, false, location),
					CustomSqlHelper.CreateInputParameter("IsBeagleEra", SqlDbType.NVarChar, 200, false, isBeagleEra),
					CustomSqlHelper.CreateInputParameter("Inscription", SqlDbType.NVarChar, 200, false, inscription), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotatedTitle> helper = new CustomSqlHelper<AnnotatedTitle>())
				{
					CustomGenericList<AnnotatedTitle> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedTitle o = list[0];
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
		/// Insert values into AnnotatedTitle. Returns an object of type AnnotatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedTitle.</param>
		/// <returns>Object of type AnnotatedTitle.</returns>
		public AnnotatedTitle AnnotatedTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedTitle value)
		{
			return AnnotatedTitleInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotatedTitle. Returns an object of type AnnotatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedTitle.</param>
		/// <returns>Object of type AnnotatedTitle.</returns>
		public AnnotatedTitle AnnotatedTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedTitle value)
		{
			return AnnotatedTitleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationSourceID,
				value.TitleID,
				value.ExternalIdentifier,
				value.Author,
				value.Title,
				value.Edition,
				value.Volume,
				value.PublicationDetails,
				value.Date,
				value.Location,
				value.IsBeagleEra,
				value.Inscription);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedTitleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotatedTitleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedTitleID)
		{
			return AnnotatedTitleDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotatedTitleID );
		}
		
		/// <summary>
		/// Delete values from AnnotatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedTitleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotatedTitleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedTitleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedTitleDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedTitleID", SqlDbType.Int, null, false, annotatedTitleID), 
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
		/// Update values in AnnotatedTitle. Returns an object of type AnnotatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedTitleID"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="titleID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="author"></param>
		/// <param name="title"></param>
		/// <param name="edition"></param>
		/// <param name="volume"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="date"></param>
		/// <param name="location"></param>
		/// <param name="isBeagleEra"></param>
		/// <param name="inscription"></param>
		/// <returns>Object of type AnnotatedTitle.</returns>
		public AnnotatedTitle AnnotatedTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedTitleID,
			int annotationSourceID,
			int? titleID,
			string externalIdentifier,
			string author,
			string title,
			string edition,
			string volume,
			string publicationDetails,
			string date,
			string location,
			string isBeagleEra,
			string inscription)
		{
			return AnnotatedTitleUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotatedTitleID, annotationSourceID, titleID, externalIdentifier, author, title, edition, volume, publicationDetails, date, location, isBeagleEra, inscription);
		}
		
		/// <summary>
		/// Update values in AnnotatedTitle. Returns an object of type AnnotatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedTitleID"></param>
		/// <param name="annotationSourceID"></param>
		/// <param name="titleID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="author"></param>
		/// <param name="title"></param>
		/// <param name="edition"></param>
		/// <param name="volume"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="date"></param>
		/// <param name="location"></param>
		/// <param name="isBeagleEra"></param>
		/// <param name="inscription"></param>
		/// <returns>Object of type AnnotatedTitle.</returns>
		public AnnotatedTitle AnnotatedTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedTitleID,
			int annotationSourceID,
			int? titleID,
			string externalIdentifier,
			string author,
			string title,
			string edition,
			string volume,
			string publicationDetails,
			string date,
			string location,
			string isBeagleEra,
			string inscription)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedTitleUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedTitleID", SqlDbType.Int, null, false, annotatedTitleID),
					CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, true, titleID),
					CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
					CustomSqlHelper.CreateInputParameter("Author", SqlDbType.NVarChar, 100, false, author),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 200, false, title),
					CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 50, false, edition),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 50, false, volume),
					CustomSqlHelper.CreateInputParameter("PublicationDetails", SqlDbType.NVarChar, 100, false, publicationDetails),
					CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
					CustomSqlHelper.CreateInputParameter("Location", SqlDbType.NVarChar, 50, false, location),
					CustomSqlHelper.CreateInputParameter("IsBeagleEra", SqlDbType.NVarChar, 200, false, isBeagleEra),
					CustomSqlHelper.CreateInputParameter("Inscription", SqlDbType.NVarChar, 200, false, inscription), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotatedTitle> helper = new CustomSqlHelper<AnnotatedTitle>())
				{
					CustomGenericList<AnnotatedTitle> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedTitle o = list[0];
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
		/// Update values in AnnotatedTitle. Returns an object of type AnnotatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedTitle.</param>
		/// <returns>Object of type AnnotatedTitle.</returns>
		public AnnotatedTitle AnnotatedTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedTitle value)
		{
			return AnnotatedTitleUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotatedTitle. Returns an object of type AnnotatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedTitle.</param>
		/// <returns>Object of type AnnotatedTitle.</returns>
		public AnnotatedTitle AnnotatedTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedTitle value)
		{
			return AnnotatedTitleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotatedTitleID,
				value.AnnotationSourceID,
				value.TitleID,
				value.ExternalIdentifier,
				value.Author,
				value.Title,
				value.Edition,
				value.Volume,
				value.PublicationDetails,
				value.Date,
				value.Location,
				value.IsBeagleEra,
				value.Inscription);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotatedTitle object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedTitle.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotatedTitle>.</returns>
		public CustomDataAccessStatus<AnnotatedTitle> AnnotatedTitleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedTitle value  )
		{
			return AnnotatedTitleManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotatedTitle object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedTitle.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotatedTitle>.</returns>
		public CustomDataAccessStatus<AnnotatedTitle> AnnotatedTitleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedTitle value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotatedTitle returnValue = AnnotatedTitleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationSourceID,
						value.TitleID,
						value.ExternalIdentifier,
						value.Author,
						value.Title,
						value.Edition,
						value.Volume,
						value.PublicationDetails,
						value.Date,
						value.Location,
						value.IsBeagleEra,
						value.Inscription);
				
				return new CustomDataAccessStatus<AnnotatedTitle>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotatedTitleDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedTitleID))
				{
				return new CustomDataAccessStatus<AnnotatedTitle>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotatedTitle>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotatedTitle returnValue = AnnotatedTitleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedTitleID,
						value.AnnotationSourceID,
						value.TitleID,
						value.ExternalIdentifier,
						value.Author,
						value.Title,
						value.Edition,
						value.Volume,
						value.PublicationDetails,
						value.Date,
						value.Location,
						value.IsBeagleEra,
						value.Inscription);
					
				return new CustomDataAccessStatus<AnnotatedTitle>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotatedTitle>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

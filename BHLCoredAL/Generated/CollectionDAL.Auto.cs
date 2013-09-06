
// Generated 4/2/2012 3:02:06 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class CollectionDAL is based upon Collection.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class CollectionDAL
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
	partial class CollectionDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int collectionID)
		{
			return CollectionSelectAuto(	sqlConnection, sqlTransaction, "BHL",	collectionID );
		}
			
		/// <summary>
		/// Select values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int collectionID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
			{
				using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
				{
					CustomGenericList<Collection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Collection o = list[0];
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
		/// Select values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="collectionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> CollectionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int collectionID)
		{
			return CollectionSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", collectionID );
		}
		
		/// <summary>
		/// Select values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="collectionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> CollectionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int collectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="collectionName"></param>
		/// <param name="collectionDescription"></param>
		/// <param name="collectionURL"></param>
		/// <param name="imageURL"></param>
		/// <param name="htmlContent"></param>
		/// <param name="canContainTitles"></param>
		/// <param name="canContainItems"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="collectionTarget"></param>
		/// <param name="iTunesImageURL"></param>
		/// <param name="iTunesURL"></param>
		/// <param name="featured"></param>
		/// <param name="active"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string collectionName,
			string collectionDescription,
			string collectionURL,
			string imageURL,
			string htmlContent,
			short canContainTitles,
			short canContainItems,
			string institutionCode,
			string languageCode,
			string collectionTarget,
			string iTunesImageURL,
			string iTunesURL,
			short featured,
			short active)
		{
			return CollectionInsertAuto( sqlConnection, sqlTransaction, "BHL", collectionName, collectionDescription, collectionURL, imageURL, htmlContent, canContainTitles, canContainItems, institutionCode, languageCode, collectionTarget, iTunesImageURL, iTunesURL, featured, active );
		}
		
		/// <summary>
		/// Insert values into Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="collectionName"></param>
		/// <param name="collectionDescription"></param>
		/// <param name="collectionURL"></param>
		/// <param name="imageURL"></param>
		/// <param name="htmlContent"></param>
		/// <param name="canContainTitles"></param>
		/// <param name="canContainItems"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="collectionTarget"></param>
		/// <param name="iTunesImageURL"></param>
		/// <param name="iTunesURL"></param>
		/// <param name="featured"></param>
		/// <param name="active"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string collectionName,
			string collectionDescription,
			string collectionURL,
			string imageURL,
			string htmlContent,
			short canContainTitles,
			short canContainItems,
			string institutionCode,
			string languageCode,
			string collectionTarget,
			string iTunesImageURL,
			string iTunesURL,
			short featured,
			short active)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("CollectionID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("CollectionName", SqlDbType.NVarChar, 50, false, collectionName),
					CustomSqlHelper.CreateInputParameter("CollectionDescription", SqlDbType.NVarChar, 4000, false, collectionDescription),
					CustomSqlHelper.CreateInputParameter("CollectionURL", SqlDbType.NVarChar, 50, false, collectionURL),
					CustomSqlHelper.CreateInputParameter("ImageURL", SqlDbType.NVarChar, 100, false, imageURL),
					CustomSqlHelper.CreateInputParameter("HtmlContent", SqlDbType.NVarChar, 1073741823, false, htmlContent),
					CustomSqlHelper.CreateInputParameter("CanContainTitles", SqlDbType.SmallInt, null, false, canContainTitles),
					CustomSqlHelper.CreateInputParameter("CanContainItems", SqlDbType.SmallInt, null, false, canContainItems),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("CollectionTarget", SqlDbType.NVarChar, 30, false, collectionTarget),
					CustomSqlHelper.CreateInputParameter("ITunesImageURL", SqlDbType.NVarChar, 100, false, iTunesImageURL),
					CustomSqlHelper.CreateInputParameter("ITunesURL", SqlDbType.NVarChar, 100, false, iTunesURL),
					CustomSqlHelper.CreateInputParameter("Featured", SqlDbType.SmallInt, null, false, featured),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.SmallInt, null, false, active), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
				{
					CustomGenericList<Collection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Collection o = list[0];
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
		/// Insert values into Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Collection value)
		{
			return CollectionInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Collection value)
		{
			return CollectionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.CollectionName,
				value.CollectionDescription,
				value.CollectionURL,
				value.ImageURL,
				value.HtmlContent,
				value.CanContainTitles,
				value.CanContainItems,
				value.InstitutionCode,
				value.LanguageCode,
				value.CollectionTarget,
				value.ITunesImageURL,
				value.ITunesURL,
				value.Featured,
				value.Active);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="collectionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool CollectionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int collectionID)
		{
			return CollectionDeleteAuto( sqlConnection, sqlTransaction, "BHL", collectionID );
		}
		
		/// <summary>
		/// Delete values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="collectionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool CollectionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int collectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID), 
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
		/// Update values in Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="collectionID"></param>
		/// <param name="collectionName"></param>
		/// <param name="collectionDescription"></param>
		/// <param name="collectionURL"></param>
		/// <param name="imageURL"></param>
		/// <param name="htmlContent"></param>
		/// <param name="canContainTitles"></param>
		/// <param name="canContainItems"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="collectionTarget"></param>
		/// <param name="iTunesImageURL"></param>
		/// <param name="iTunesURL"></param>
		/// <param name="featured"></param>
		/// <param name="active"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int collectionID,
			string collectionName,
			string collectionDescription,
			string collectionURL,
			string imageURL,
			string htmlContent,
			short canContainTitles,
			short canContainItems,
			string institutionCode,
			string languageCode,
			string collectionTarget,
			string iTunesImageURL,
			string iTunesURL,
			short featured,
			short active)
		{
			return CollectionUpdateAuto( sqlConnection, sqlTransaction, "BHL", collectionID, collectionName, collectionDescription, collectionURL, imageURL, htmlContent, canContainTitles, canContainItems, institutionCode, languageCode, collectionTarget, iTunesImageURL, iTunesURL, featured, active);
		}
		
		/// <summary>
		/// Update values in Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="collectionID"></param>
		/// <param name="collectionName"></param>
		/// <param name="collectionDescription"></param>
		/// <param name="collectionURL"></param>
		/// <param name="imageURL"></param>
		/// <param name="htmlContent"></param>
		/// <param name="canContainTitles"></param>
		/// <param name="canContainItems"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="collectionTarget"></param>
		/// <param name="iTunesImageURL"></param>
		/// <param name="iTunesURL"></param>
		/// <param name="featured"></param>
		/// <param name="active"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int collectionID,
			string collectionName,
			string collectionDescription,
			string collectionURL,
			string imageURL,
			string htmlContent,
			short canContainTitles,
			short canContainItems,
			string institutionCode,
			string languageCode,
			string collectionTarget,
			string iTunesImageURL,
			string iTunesURL,
			short featured,
			short active)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID),
					CustomSqlHelper.CreateInputParameter("CollectionName", SqlDbType.NVarChar, 50, false, collectionName),
					CustomSqlHelper.CreateInputParameter("CollectionDescription", SqlDbType.NVarChar, 4000, false, collectionDescription),
					CustomSqlHelper.CreateInputParameter("CollectionURL", SqlDbType.NVarChar, 50, false, collectionURL),
					CustomSqlHelper.CreateInputParameter("ImageURL", SqlDbType.NVarChar, 100, false, imageURL),
					CustomSqlHelper.CreateInputParameter("HtmlContent", SqlDbType.NVarChar, 1073741823, false, htmlContent),
					CustomSqlHelper.CreateInputParameter("CanContainTitles", SqlDbType.SmallInt, null, false, canContainTitles),
					CustomSqlHelper.CreateInputParameter("CanContainItems", SqlDbType.SmallInt, null, false, canContainItems),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("CollectionTarget", SqlDbType.NVarChar, 30, false, collectionTarget),
					CustomSqlHelper.CreateInputParameter("ITunesImageURL", SqlDbType.NVarChar, 100, false, iTunesImageURL),
					CustomSqlHelper.CreateInputParameter("ITunesURL", SqlDbType.NVarChar, 100, false, iTunesURL),
					CustomSqlHelper.CreateInputParameter("Featured", SqlDbType.SmallInt, null, false, featured),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.SmallInt, null, false, active), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
				{
					CustomGenericList<Collection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Collection o = list[0];
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
		/// Update values in Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Collection value)
		{
			return CollectionUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Collection value)
		{
			return CollectionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.CollectionID,
				value.CollectionName,
				value.CollectionDescription,
				value.CollectionURL,
				value.ImageURL,
				value.HtmlContent,
				value.CanContainTitles,
				value.CanContainItems,
				value.InstitutionCode,
				value.LanguageCode,
				value.CollectionTarget,
				value.ITunesImageURL,
				value.ITunesURL,
				value.Featured,
				value.Active);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Collection object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type CustomDataAccessStatus<Collection>.</returns>
		public CustomDataAccessStatus<Collection> CollectionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Collection value  )
		{
			return CollectionManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage Collection object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type CustomDataAccessStatus<Collection>.</returns>
		public CustomDataAccessStatus<Collection> CollectionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Collection value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Collection returnValue = CollectionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.CollectionName,
						value.CollectionDescription,
						value.CollectionURL,
						value.ImageURL,
						value.HtmlContent,
						value.CanContainTitles,
						value.CanContainItems,
						value.InstitutionCode,
						value.LanguageCode,
						value.CollectionTarget,
						value.ITunesImageURL,
						value.ITunesURL,
						value.Featured,
						value.Active);
				
				return new CustomDataAccessStatus<Collection>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (CollectionDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.CollectionID))
				{
				return new CustomDataAccessStatus<Collection>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Collection>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Collection returnValue = CollectionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.CollectionID,
						value.CollectionName,
						value.CollectionDescription,
						value.CollectionURL,
						value.ImageURL,
						value.HtmlContent,
						value.CanContainTitles,
						value.CanContainItems,
						value.InstitutionCode,
						value.LanguageCode,
						value.CollectionTarget,
						value.ITunesImageURL,
						value.ITunesURL,
						value.Featured,
						value.Active);
					
				return new CustomDataAccessStatus<Collection>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Collection>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

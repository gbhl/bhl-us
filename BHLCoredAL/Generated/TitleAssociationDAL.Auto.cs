
// Generated 2/4/2011 2:43:35 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleAssociationDAL is based upon TitleAssociation.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleAssociationDAL
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
	partial class TitleAssociationDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from TitleAssociation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociationID"></param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociationID)
		{
			return TitleAssociationSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleAssociationID );
		}
			
		/// <summary>
		/// Select values from TitleAssociation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociationID"></param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociationID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociationID", SqlDbType.Int, null, false, titleAssociationID)))
			{
				using (CustomSqlHelper<TitleAssociation> helper = new CustomSqlHelper<TitleAssociation>())
				{
					CustomGenericList<TitleAssociation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociation o = list[0];
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
		/// Select values from TitleAssociation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociationID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleAssociationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociationID)
		{
			return TitleAssociationSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleAssociationID );
		}
		
		/// <summary>
		/// Select values from TitleAssociation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociationID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleAssociationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociationID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleAssociationID", SqlDbType.Int, null, false, titleAssociationID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="titleAssociationTypeID"></param>
		/// <param name="title"></param>
		/// <param name="section"></param>
		/// <param name="volume"></param>
		/// <param name="active"></param>
		/// <param name="associatedTitleID"></param>
		/// <param name="heading"></param>
		/// <param name="publication"></param>
		/// <param name="relationship"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			int titleAssociationTypeID,
			string title,
			string section,
			string volume,
			bool active,
			int? associatedTitleID,
			string heading,
			string publication,
			string relationship,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return TitleAssociationInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, titleAssociationTypeID, title, section, volume, active, associatedTitleID, heading, publication, relationship, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="titleAssociationTypeID"></param>
		/// <param name="title"></param>
		/// <param name="section"></param>
		/// <param name="volume"></param>
		/// <param name="active"></param>
		/// <param name="associatedTitleID"></param>
		/// <param name="heading"></param>
		/// <param name="publication"></param>
		/// <param name="relationship"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			int titleAssociationTypeID,
			string title,
			string section,
			string volume,
			bool active,
			int? associatedTitleID,
			string heading,
			string publication,
			string relationship,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleAssociationID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("TitleAssociationTypeID", SqlDbType.Int, null, false, titleAssociationTypeID),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 500, false, title),
					CustomSqlHelper.CreateInputParameter("Section", SqlDbType.NVarChar, 500, false, section),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 500, false, volume),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.Bit, null, false, active),
					CustomSqlHelper.CreateInputParameter("AssociatedTitleID", SqlDbType.Int, null, true, associatedTitleID),
					CustomSqlHelper.CreateInputParameter("Heading", SqlDbType.NVarChar, 500, false, heading),
					CustomSqlHelper.CreateInputParameter("Publication", SqlDbType.NVarChar, 500, false, publication),
					CustomSqlHelper.CreateInputParameter("Relationship", SqlDbType.NVarChar, 500, false, relationship),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleAssociation> helper = new CustomSqlHelper<TitleAssociation>())
				{
					CustomGenericList<TitleAssociation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociation o = list[0];
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
		/// Insert values into TitleAssociation. Returns an object of type TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociation.</param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociation value)
		{
			return TitleAssociationInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into TitleAssociation. Returns an object of type TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociation.</param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociation value)
		{
			return TitleAssociationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.TitleAssociationTypeID,
				value.Title,
				value.Section,
				value.Volume,
				value.Active,
				value.AssociatedTitleID,
				value.Heading,
				value.Publication,
				value.Relationship,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from TitleAssociation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociationID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleAssociationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociationID)
		{
			return TitleAssociationDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleAssociationID );
		}
		
		/// <summary>
		/// Delete values from TitleAssociation by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociationID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleAssociationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociationID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociationID", SqlDbType.Int, null, false, titleAssociationID), 
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
		/// Update values in TitleAssociation. Returns an object of type TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociationID"></param>
		/// <param name="titleID"></param>
		/// <param name="titleAssociationTypeID"></param>
		/// <param name="title"></param>
		/// <param name="section"></param>
		/// <param name="volume"></param>
		/// <param name="active"></param>
		/// <param name="associatedTitleID"></param>
		/// <param name="heading"></param>
		/// <param name="publication"></param>
		/// <param name="relationship"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociationID,
			int titleID,
			int titleAssociationTypeID,
			string title,
			string section,
			string volume,
			bool active,
			int? associatedTitleID,
			string heading,
			string publication,
			string relationship,
			int? lastModifiedUserID)
		{
			return TitleAssociationUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleAssociationID, titleID, titleAssociationTypeID, title, section, volume, active, associatedTitleID, heading, publication, relationship, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in TitleAssociation. Returns an object of type TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociationID"></param>
		/// <param name="titleID"></param>
		/// <param name="titleAssociationTypeID"></param>
		/// <param name="title"></param>
		/// <param name="section"></param>
		/// <param name="volume"></param>
		/// <param name="active"></param>
		/// <param name="associatedTitleID"></param>
		/// <param name="heading"></param>
		/// <param name="publication"></param>
		/// <param name="relationship"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociationID,
			int titleID,
			int titleAssociationTypeID,
			string title,
			string section,
			string volume,
			bool active,
			int? associatedTitleID,
			string heading,
			string publication,
			string relationship,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociationID", SqlDbType.Int, null, false, titleAssociationID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("TitleAssociationTypeID", SqlDbType.Int, null, false, titleAssociationTypeID),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 500, false, title),
					CustomSqlHelper.CreateInputParameter("Section", SqlDbType.NVarChar, 500, false, section),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 500, false, volume),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.Bit, null, false, active),
					CustomSqlHelper.CreateInputParameter("AssociatedTitleID", SqlDbType.Int, null, true, associatedTitleID),
					CustomSqlHelper.CreateInputParameter("Heading", SqlDbType.NVarChar, 500, false, heading),
					CustomSqlHelper.CreateInputParameter("Publication", SqlDbType.NVarChar, 500, false, publication),
					CustomSqlHelper.CreateInputParameter("Relationship", SqlDbType.NVarChar, 500, false, relationship),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleAssociation> helper = new CustomSqlHelper<TitleAssociation>())
				{
					CustomGenericList<TitleAssociation> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociation o = list[0];
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
		/// Update values in TitleAssociation. Returns an object of type TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociation.</param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociation value)
		{
			return TitleAssociationUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in TitleAssociation. Returns an object of type TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociation.</param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociation value)
		{
			return TitleAssociationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleAssociationID,
				value.TitleID,
				value.TitleAssociationTypeID,
				value.Title,
				value.Section,
				value.Volume,
				value.Active,
				value.AssociatedTitleID,
				value.Heading,
				value.Publication,
				value.Relationship,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage TitleAssociation object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociation.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleAssociation>.</returns>
		public CustomDataAccessStatus<TitleAssociation> TitleAssociationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociation value , int userId )
		{
			return TitleAssociationManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage TitleAssociation object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociation.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleAssociation>.</returns>
		public CustomDataAccessStatus<TitleAssociation> TitleAssociationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociation value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TitleAssociation returnValue = TitleAssociationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.TitleAssociationTypeID,
						value.Title,
						value.Section,
						value.Volume,
						value.Active,
						value.AssociatedTitleID,
						value.Heading,
						value.Publication,
						value.Relationship,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TitleAssociation>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleAssociationDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAssociationID))
				{
				return new CustomDataAccessStatus<TitleAssociation>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleAssociation>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TitleAssociation returnValue = TitleAssociationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAssociationID,
						value.TitleID,
						value.TitleAssociationTypeID,
						value.Title,
						value.Section,
						value.Volume,
						value.Active,
						value.AssociatedTitleID,
						value.Heading,
						value.Publication,
						value.Relationship,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TitleAssociation>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleAssociation>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

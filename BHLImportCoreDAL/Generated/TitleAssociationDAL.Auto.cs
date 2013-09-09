
// Generated 9/4/2008 2:16:32 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleAssociationDAL is based upon TitleAssociation.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
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
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
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
			return TitleAssociationSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	titleAssociationID );
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
			return TitleAssociationSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", titleAssociationID );
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
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCTag"></param>
		/// <param name="mARCIndicator2"></param>
		/// <param name="title"></param>
		/// <param name="section"></param>
		/// <param name="volume"></param>
		/// <param name="heading"></param>
		/// <param name="publication"></param>
		/// <param name="relationship"></param>
		/// <param name="active"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string importKey,
			int importStatusID,
			int importSourceID,
			string mARCTag,
			string mARCIndicator2,
			string title,
			string section,
			string volume,
			string heading,
			string publication,
			string relationship,
			bool active,
			DateTime? productionDate)
		{
			return TitleAssociationInsertAuto( sqlConnection, sqlTransaction, "BHLImport", importKey, importStatusID, importSourceID, mARCTag, mARCIndicator2, title, section, volume, heading, publication, relationship, active, productionDate );
		}
		
		/// <summary>
		/// Insert values into TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCTag"></param>
		/// <param name="mARCIndicator2"></param>
		/// <param name="title"></param>
		/// <param name="section"></param>
		/// <param name="volume"></param>
		/// <param name="heading"></param>
		/// <param name="publication"></param>
		/// <param name="relationship"></param>
		/// <param name="active"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string importKey,
			int importStatusID,
			int importSourceID,
			string mARCTag,
			string mARCIndicator2,
			string title,
			string section,
			string volume,
			string heading,
			string publication,
			string relationship,
			bool active,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleAssociationID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID),
					CustomSqlHelper.CreateInputParameter("MARCTag", SqlDbType.NVarChar, 20, false, mARCTag),
					CustomSqlHelper.CreateInputParameter("MARCIndicator2", SqlDbType.NChar, 1, false, mARCIndicator2),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 500, false, title),
					CustomSqlHelper.CreateInputParameter("Section", SqlDbType.NVarChar, 500, false, section),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 500, false, volume),
					CustomSqlHelper.CreateInputParameter("Heading", SqlDbType.NVarChar, 500, false, heading),
					CustomSqlHelper.CreateInputParameter("Publication", SqlDbType.NVarChar, 500, false, publication),
					CustomSqlHelper.CreateInputParameter("Relationship", SqlDbType.NVarChar, 500, false, relationship),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.Bit, null, false, active),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
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
			return TitleAssociationInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
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
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.MARCTag,
				value.MARCIndicator2,
				value.Title,
				value.Section,
				value.Volume,
				value.Heading,
				value.Publication,
				value.Relationship,
				value.Active,
				value.ProductionDate);
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
			return TitleAssociationDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", titleAssociationID );
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
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCTag"></param>
		/// <param name="mARCIndicator2"></param>
		/// <param name="title"></param>
		/// <param name="section"></param>
		/// <param name="volume"></param>
		/// <param name="heading"></param>
		/// <param name="publication"></param>
		/// <param name="relationship"></param>
		/// <param name="active"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociationID,
			string importKey,
			int importStatusID,
			int importSourceID,
			string mARCTag,
			string mARCIndicator2,
			string title,
			string section,
			string volume,
			string heading,
			string publication,
			string relationship,
			bool active,
			DateTime? productionDate)
		{
			return TitleAssociationUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", titleAssociationID, importKey, importStatusID, importSourceID, mARCTag, mARCIndicator2, title, section, volume, heading, publication, relationship, active, productionDate);
		}
		
		/// <summary>
		/// Update values in TitleAssociation. Returns an object of type TitleAssociation.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociationID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCTag"></param>
		/// <param name="mARCIndicator2"></param>
		/// <param name="title"></param>
		/// <param name="section"></param>
		/// <param name="volume"></param>
		/// <param name="heading"></param>
		/// <param name="publication"></param>
		/// <param name="relationship"></param>
		/// <param name="active"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleAssociation.</returns>
		public TitleAssociation TitleAssociationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociationID,
			string importKey,
			int importStatusID,
			int importSourceID,
			string mARCTag,
			string mARCIndicator2,
			string title,
			string section,
			string volume,
			string heading,
			string publication,
			string relationship,
			bool active,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociationID", SqlDbType.Int, null, false, titleAssociationID),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID),
					CustomSqlHelper.CreateInputParameter("MARCTag", SqlDbType.NVarChar, 20, false, mARCTag),
					CustomSqlHelper.CreateInputParameter("MARCIndicator2", SqlDbType.NChar, 1, false, mARCIndicator2),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 500, false, title),
					CustomSqlHelper.CreateInputParameter("Section", SqlDbType.NVarChar, 500, false, section),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 500, false, volume),
					CustomSqlHelper.CreateInputParameter("Heading", SqlDbType.NVarChar, 500, false, heading),
					CustomSqlHelper.CreateInputParameter("Publication", SqlDbType.NVarChar, 500, false, publication),
					CustomSqlHelper.CreateInputParameter("Relationship", SqlDbType.NVarChar, 500, false, relationship),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.Bit, null, false, active),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
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
			return TitleAssociationUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
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
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.MARCTag,
				value.MARCIndicator2,
				value.Title,
				value.Section,
				value.Volume,
				value.Heading,
				value.Publication,
				value.Relationship,
				value.Active,
				value.ProductionDate);
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
			TitleAssociation value  )
		{
			return TitleAssociationManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
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
			TitleAssociation value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				TitleAssociation returnValue = TitleAssociationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.MARCTag,
						value.MARCIndicator2,
						value.Title,
						value.Section,
						value.Volume,
						value.Heading,
						value.Publication,
						value.Relationship,
						value.Active,
						value.ProductionDate);
				
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
				
				TitleAssociation returnValue = TitleAssociationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAssociationID,
						value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.MARCTag,
						value.MARCIndicator2,
						value.Title,
						value.Section,
						value.Volume,
						value.Heading,
						value.Publication,
						value.Relationship,
						value.Active,
						value.ProductionDate);
					
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


// Generated 9/4/2008 2:16:32 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleAssociation_TitleIdentifierDAL is based upon TitleAssociation_TitleIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class TitleAssociation_TitleIdentifierDAL
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
	partial class TitleAssociation_TitleIdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociation_TitleIdentifierID)
		{
			return TitleAssociation_TitleIdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	titleAssociation_TitleIdentifierID );
		}
			
		/// <summary>
		/// Select values from TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociation_TitleIdentifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociation_TitleIdentifierID", SqlDbType.Int, null, false, titleAssociation_TitleIdentifierID)))
			{
				using (CustomSqlHelper<TitleAssociation_TitleIdentifier> helper = new CustomSqlHelper<TitleAssociation_TitleIdentifier>())
				{
					CustomGenericList<TitleAssociation_TitleIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociation_TitleIdentifier o = list[0];
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
		/// Select values from TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleAssociation_TitleIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociation_TitleIdentifierID)
		{
			return TitleAssociation_TitleIdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", titleAssociation_TitleIdentifierID );
		}
		
		/// <summary>
		/// Select values from TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleAssociation_TitleIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociation_TitleIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleAssociation_TitleIdentifierID", SqlDbType.Int, null, false, titleAssociation_TitleIdentifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into TitleAssociation_TitleIdentifier.
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
		/// <param name="identifierName"></param>
		/// <param name="identifierValue"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierInsertAuto(
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
			string identifierName,
			string identifierValue,
			DateTime? productionDate)
		{
			return TitleAssociation_TitleIdentifierInsertAuto( sqlConnection, sqlTransaction, "BHLImport", importKey, importStatusID, importSourceID, mARCTag, mARCIndicator2, title, section, volume, heading, publication, relationship, identifierName, identifierValue, productionDate );
		}
		
		/// <summary>
		/// Insert values into TitleAssociation_TitleIdentifier.
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
		/// <param name="identifierName"></param>
		/// <param name="identifierValue"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierInsertAuto(
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
			string identifierName,
			string identifierValue,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleAssociation_TitleIdentifierID", SqlDbType.Int, null, false),
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
					CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleAssociation_TitleIdentifier> helper = new CustomSqlHelper<TitleAssociation_TitleIdentifier>())
				{
					CustomGenericList<TitleAssociation_TitleIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociation_TitleIdentifier o = list[0];
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
		/// Insert values into TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociation_TitleIdentifier value)
		{
			return TitleAssociation_TitleIdentifierInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociation_TitleIdentifier value)
		{
			return TitleAssociation_TitleIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
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
				value.IdentifierName,
				value.IdentifierValue,
				value.ProductionDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleAssociation_TitleIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociation_TitleIdentifierID)
		{
			return TitleAssociation_TitleIdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", titleAssociation_TitleIdentifierID );
		}
		
		/// <summary>
		/// Delete values from TitleAssociation_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleAssociation_TitleIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociation_TitleIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociation_TitleIdentifierID", SqlDbType.Int, null, false, titleAssociation_TitleIdentifierID), 
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
		/// Update values in TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
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
		/// <param name="identifierName"></param>
		/// <param name="identifierValue"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAssociation_TitleIdentifierID,
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
			string identifierName,
			string identifierValue,
			DateTime? productionDate)
		{
			return TitleAssociation_TitleIdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", titleAssociation_TitleIdentifierID, importKey, importStatusID, importSourceID, mARCTag, mARCIndicator2, title, section, volume, heading, publication, relationship, identifierName, identifierValue, productionDate);
		}
		
		/// <summary>
		/// Update values in TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAssociation_TitleIdentifierID"></param>
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
		/// <param name="identifierName"></param>
		/// <param name="identifierValue"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAssociation_TitleIdentifierID,
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
			string identifierName,
			string identifierValue,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAssociation_TitleIdentifierID", SqlDbType.Int, null, false, titleAssociation_TitleIdentifierID),
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
					CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleAssociation_TitleIdentifier> helper = new CustomSqlHelper<TitleAssociation_TitleIdentifier>())
				{
					CustomGenericList<TitleAssociation_TitleIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAssociation_TitleIdentifier o = list[0];
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
		/// Update values in TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociation_TitleIdentifier value)
		{
			return TitleAssociation_TitleIdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in TitleAssociation_TitleIdentifier. Returns an object of type TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type TitleAssociation_TitleIdentifier.</returns>
		public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociation_TitleIdentifier value)
		{
			return TitleAssociation_TitleIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleAssociation_TitleIdentifierID,
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
				value.IdentifierName,
				value.IdentifierValue,
				value.ProductionDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage TitleAssociation_TitleIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleAssociation_TitleIdentifier>.</returns>
		public CustomDataAccessStatus<TitleAssociation_TitleIdentifier> TitleAssociation_TitleIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAssociation_TitleIdentifier value  )
		{
			return TitleAssociation_TitleIdentifierManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage TitleAssociation_TitleIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleAssociation_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAssociation_TitleIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleAssociation_TitleIdentifier>.</returns>
		public CustomDataAccessStatus<TitleAssociation_TitleIdentifier> TitleAssociation_TitleIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAssociation_TitleIdentifier value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				TitleAssociation_TitleIdentifier returnValue = TitleAssociation_TitleIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
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
						value.IdentifierName,
						value.IdentifierValue,
						value.ProductionDate);
				
				return new CustomDataAccessStatus<TitleAssociation_TitleIdentifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleAssociation_TitleIdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAssociation_TitleIdentifierID))
				{
				return new CustomDataAccessStatus<TitleAssociation_TitleIdentifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleAssociation_TitleIdentifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				TitleAssociation_TitleIdentifier returnValue = TitleAssociation_TitleIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAssociation_TitleIdentifierID,
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
						value.IdentifierName,
						value.IdentifierValue,
						value.ProductionDate);
					
				return new CustomDataAccessStatus<TitleAssociation_TitleIdentifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleAssociation_TitleIdentifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

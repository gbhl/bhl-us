
// Generated 9/4/2008 2:16:32 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleTagDAL is based upon TitleTag.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class TitleTagDAL
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
	partial class TitleTagDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from TitleTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleTagID"></param>
		/// <returns>Object of type TitleTag.</returns>
		public TitleTag TitleTagSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleTagID)
		{
			return TitleTagSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	titleTagID );
		}
			
		/// <summary>
		/// Select values from TitleTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleTagID"></param>
		/// <returns>Object of type TitleTag.</returns>
		public TitleTag TitleTagSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleTagID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleTagSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleTagID", SqlDbType.Int, null, false, titleTagID)))
			{
				using (CustomSqlHelper<TitleTag> helper = new CustomSqlHelper<TitleTag>())
				{
					CustomGenericList<TitleTag> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleTag o = list[0];
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
		/// Select values from TitleTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleTagID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleTagSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleTagID)
		{
			return TitleTagSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", titleTagID );
		}
		
		/// <summary>
		/// Select values from TitleTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleTagID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleTagSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleTagID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleTagSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleTagID", SqlDbType.Int, null, false, titleTagID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into TitleTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="tagText"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcSubFieldCode"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleTag.</returns>
		public TitleTag TitleTagInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string tagText,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string marcDataFieldTag,
			string marcSubFieldCode,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			DateTime? productionDate)
		{
			return TitleTagInsertAuto( sqlConnection, sqlTransaction, "BHLImport", tagText, importKey, importStatusID, importSourceID, marcDataFieldTag, marcSubFieldCode, externalCreationDate, externalLastModifiedDate, productionDate );
		}
		
		/// <summary>
		/// Insert values into TitleTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="tagText"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcSubFieldCode"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleTag.</returns>
		public TitleTag TitleTagInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string tagText,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string marcDataFieldTag,
			string marcSubFieldCode,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleTagInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleTagID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TagText", SqlDbType.NVarChar, 50, false, tagText),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("MarcDataFieldTag", SqlDbType.NVarChar, 50, true, marcDataFieldTag),
					CustomSqlHelper.CreateInputParameter("MarcSubFieldCode", SqlDbType.NVarChar, 50, true, marcSubFieldCode),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleTag> helper = new CustomSqlHelper<TitleTag>())
				{
					CustomGenericList<TitleTag> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleTag o = list[0];
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
		/// Insert values into TitleTag. Returns an object of type TitleTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleTag.</param>
		/// <returns>Object of type TitleTag.</returns>
		public TitleTag TitleTagInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleTag value)
		{
			return TitleTagInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into TitleTag. Returns an object of type TitleTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleTag.</param>
		/// <returns>Object of type TitleTag.</returns>
		public TitleTag TitleTagInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleTag value)
		{
			return TitleTagInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TagText,
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.MarcDataFieldTag,
				value.MarcSubFieldCode,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ProductionDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from TitleTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleTagID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleTagDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleTagID)
		{
			return TitleTagDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", titleTagID );
		}
		
		/// <summary>
		/// Delete values from TitleTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleTagID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleTagDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleTagID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleTagDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleTagID", SqlDbType.Int, null, false, titleTagID), 
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
		/// Update values in TitleTag. Returns an object of type TitleTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleTagID"></param>
		/// <param name="tagText"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcSubFieldCode"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleTag.</returns>
		public TitleTag TitleTagUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleTagID,
			string tagText,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string marcDataFieldTag,
			string marcSubFieldCode,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			DateTime? productionDate)
		{
			return TitleTagUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", titleTagID, tagText, importKey, importStatusID, importSourceID, marcDataFieldTag, marcSubFieldCode, externalCreationDate, externalLastModifiedDate, productionDate);
		}
		
		/// <summary>
		/// Update values in TitleTag. Returns an object of type TitleTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleTagID"></param>
		/// <param name="tagText"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcSubFieldCode"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type TitleTag.</returns>
		public TitleTag TitleTagUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleTagID,
			string tagText,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string marcDataFieldTag,
			string marcSubFieldCode,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleTagUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleTagID", SqlDbType.Int, null, false, titleTagID),
					CustomSqlHelper.CreateInputParameter("TagText", SqlDbType.NVarChar, 50, false, tagText),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("MarcDataFieldTag", SqlDbType.NVarChar, 50, true, marcDataFieldTag),
					CustomSqlHelper.CreateInputParameter("MarcSubFieldCode", SqlDbType.NVarChar, 50, true, marcSubFieldCode),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleTag> helper = new CustomSqlHelper<TitleTag>())
				{
					CustomGenericList<TitleTag> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleTag o = list[0];
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
		/// Update values in TitleTag. Returns an object of type TitleTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleTag.</param>
		/// <returns>Object of type TitleTag.</returns>
		public TitleTag TitleTagUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleTag value)
		{
			return TitleTagUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in TitleTag. Returns an object of type TitleTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleTag.</param>
		/// <returns>Object of type TitleTag.</returns>
		public TitleTag TitleTagUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleTag value)
		{
			return TitleTagUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleTagID,
				value.TagText,
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.MarcDataFieldTag,
				value.MarcSubFieldCode,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ProductionDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage TitleTag object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleTag.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleTag>.</returns>
		public CustomDataAccessStatus<TitleTag> TitleTagManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleTag value  )
		{
			return TitleTagManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage TitleTag object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleTag.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleTag>.</returns>
		public CustomDataAccessStatus<TitleTag> TitleTagManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleTag value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				TitleTag returnValue = TitleTagInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TagText,
						value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.MarcDataFieldTag,
						value.MarcSubFieldCode,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ProductionDate);
				
				return new CustomDataAccessStatus<TitleTag>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleTagDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleTagID))
				{
				return new CustomDataAccessStatus<TitleTag>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleTag>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				TitleTag returnValue = TitleTagUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleTagID,
						value.TagText,
						value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.MarcDataFieldTag,
						value.MarcSubFieldCode,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ProductionDate);
					
				return new CustomDataAccessStatus<TitleTag>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleTag>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

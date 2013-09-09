
// Generated 9/4/2008 2:16:32 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class Title_CreatorDAL is based upon Title_Creator.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class Title_CreatorDAL
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
	partial class Title_CreatorDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from Title_Creator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleCreatorID"></param>
		/// <returns>Object of type Title_Creator.</returns>
		public Title_Creator Title_CreatorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleCreatorID)
		{
			return Title_CreatorSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	titleCreatorID );
		}
			
		/// <summary>
		/// Select values from Title_Creator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleCreatorID"></param>
		/// <returns>Object of type Title_Creator.</returns>
		public Title_Creator Title_CreatorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleCreatorID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_CreatorSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleCreatorID", SqlDbType.Int, null, false, titleCreatorID)))
			{
				using (CustomSqlHelper<Title_Creator> helper = new CustomSqlHelper<Title_Creator>())
				{
					CustomGenericList<Title_Creator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title_Creator o = list[0];
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
		/// Select values from Title_Creator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleCreatorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> Title_CreatorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleCreatorID)
		{
			return Title_CreatorSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", titleCreatorID );
		}
		
		/// <summary>
		/// Select values from Title_Creator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleCreatorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> Title_CreatorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleCreatorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_CreatorSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleCreatorID", SqlDbType.Int, null, false, titleCreatorID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into Title_Creator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="creatorName"></param>
		/// <param name="mARCCreator_a"></param>
		/// <param name="mARCCreator_b"></param>
		/// <param name="mARCCreator_c"></param>
		/// <param name="mARCCreator_d"></param>
		/// <param name="creatorRoleTypeID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type Title_Creator.</returns>
		public Title_Creator Title_CreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string creatorName,
			string mARCCreator_a,
			string mARCCreator_b,
			string mARCCreator_c,
			string mARCCreator_d,
			int creatorRoleTypeID,
			string importKey,
			int importStatusID,
			int? importSourceID,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate)
		{
			return Title_CreatorInsertAuto( sqlConnection, sqlTransaction, "BHLImport", creatorName, mARCCreator_a, mARCCreator_b, mARCCreator_c, mARCCreator_d, creatorRoleTypeID, importKey, importStatusID, importSourceID, externalCreationDate, externalLastModifiedDate, externalCreationUser, externalLastModifiedUser, productionDate );
		}
		
		/// <summary>
		/// Insert values into Title_Creator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="creatorName"></param>
		/// <param name="mARCCreator_a"></param>
		/// <param name="mARCCreator_b"></param>
		/// <param name="mARCCreator_c"></param>
		/// <param name="mARCCreator_d"></param>
		/// <param name="creatorRoleTypeID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type Title_Creator.</returns>
		public Title_Creator Title_CreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string creatorName,
			string mARCCreator_a,
			string mARCCreator_b,
			string mARCCreator_c,
			string mARCCreator_d,
			int creatorRoleTypeID,
			string importKey,
			int importStatusID,
			int? importSourceID,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_CreatorInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleCreatorID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("CreatorName", SqlDbType.NVarChar, 255, false, creatorName),
					CustomSqlHelper.CreateInputParameter("MARCCreator_a", SqlDbType.NVarChar, 450, true, mARCCreator_a),
					CustomSqlHelper.CreateInputParameter("MARCCreator_b", SqlDbType.NVarChar, 450, true, mARCCreator_b),
					CustomSqlHelper.CreateInputParameter("MARCCreator_c", SqlDbType.NVarChar, 450, true, mARCCreator_c),
					CustomSqlHelper.CreateInputParameter("MARCCreator_d", SqlDbType.NVarChar, 450, true, mARCCreator_d),
					CustomSqlHelper.CreateInputParameter("CreatorRoleTypeID", SqlDbType.Int, null, false, creatorRoleTypeID),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationUser", SqlDbType.Int, null, true, externalCreationUser),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedUser", SqlDbType.Int, null, true, externalLastModifiedUser),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Title_Creator> helper = new CustomSqlHelper<Title_Creator>())
				{
					CustomGenericList<Title_Creator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title_Creator o = list[0];
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
		/// Insert values into Title_Creator. Returns an object of type Title_Creator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title_Creator.</param>
		/// <returns>Object of type Title_Creator.</returns>
		public Title_Creator Title_CreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title_Creator value)
		{
			return Title_CreatorInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into Title_Creator. Returns an object of type Title_Creator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title_Creator.</param>
		/// <returns>Object of type Title_Creator.</returns>
		public Title_Creator Title_CreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title_Creator value)
		{
			return Title_CreatorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.CreatorName,
				value.MARCCreator_a,
				value.MARCCreator_b,
				value.MARCCreator_c,
				value.MARCCreator_d,
				value.CreatorRoleTypeID,
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ExternalCreationUser,
				value.ExternalLastModifiedUser,
				value.ProductionDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Title_Creator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleCreatorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool Title_CreatorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleCreatorID)
		{
			return Title_CreatorDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", titleCreatorID );
		}
		
		/// <summary>
		/// Delete values from Title_Creator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleCreatorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool Title_CreatorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleCreatorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_CreatorDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleCreatorID", SqlDbType.Int, null, false, titleCreatorID), 
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
		/// Update values in Title_Creator. Returns an object of type Title_Creator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleCreatorID"></param>
		/// <param name="creatorName"></param>
		/// <param name="mARCCreator_a"></param>
		/// <param name="mARCCreator_b"></param>
		/// <param name="mARCCreator_c"></param>
		/// <param name="mARCCreator_d"></param>
		/// <param name="creatorRoleTypeID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type Title_Creator.</returns>
		public Title_Creator Title_CreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleCreatorID,
			string creatorName,
			string mARCCreator_a,
			string mARCCreator_b,
			string mARCCreator_c,
			string mARCCreator_d,
			int creatorRoleTypeID,
			string importKey,
			int importStatusID,
			int? importSourceID,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate)
		{
			return Title_CreatorUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", titleCreatorID, creatorName, mARCCreator_a, mARCCreator_b, mARCCreator_c, mARCCreator_d, creatorRoleTypeID, importKey, importStatusID, importSourceID, externalCreationDate, externalLastModifiedDate, externalCreationUser, externalLastModifiedUser, productionDate);
		}
		
		/// <summary>
		/// Update values in Title_Creator. Returns an object of type Title_Creator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleCreatorID"></param>
		/// <param name="creatorName"></param>
		/// <param name="mARCCreator_a"></param>
		/// <param name="mARCCreator_b"></param>
		/// <param name="mARCCreator_c"></param>
		/// <param name="mARCCreator_d"></param>
		/// <param name="creatorRoleTypeID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type Title_Creator.</returns>
		public Title_Creator Title_CreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleCreatorID,
			string creatorName,
			string mARCCreator_a,
			string mARCCreator_b,
			string mARCCreator_c,
			string mARCCreator_d,
			int creatorRoleTypeID,
			string importKey,
			int importStatusID,
			int? importSourceID,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			int? externalCreationUser,
			int? externalLastModifiedUser,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_CreatorUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleCreatorID", SqlDbType.Int, null, false, titleCreatorID),
					CustomSqlHelper.CreateInputParameter("CreatorName", SqlDbType.NVarChar, 255, false, creatorName),
					CustomSqlHelper.CreateInputParameter("MARCCreator_a", SqlDbType.NVarChar, 450, true, mARCCreator_a),
					CustomSqlHelper.CreateInputParameter("MARCCreator_b", SqlDbType.NVarChar, 450, true, mARCCreator_b),
					CustomSqlHelper.CreateInputParameter("MARCCreator_c", SqlDbType.NVarChar, 450, true, mARCCreator_c),
					CustomSqlHelper.CreateInputParameter("MARCCreator_d", SqlDbType.NVarChar, 450, true, mARCCreator_d),
					CustomSqlHelper.CreateInputParameter("CreatorRoleTypeID", SqlDbType.Int, null, false, creatorRoleTypeID),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ExternalCreationUser", SqlDbType.Int, null, true, externalCreationUser),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedUser", SqlDbType.Int, null, true, externalLastModifiedUser),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Title_Creator> helper = new CustomSqlHelper<Title_Creator>())
				{
					CustomGenericList<Title_Creator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title_Creator o = list[0];
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
		/// Update values in Title_Creator. Returns an object of type Title_Creator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title_Creator.</param>
		/// <returns>Object of type Title_Creator.</returns>
		public Title_Creator Title_CreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title_Creator value)
		{
			return Title_CreatorUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in Title_Creator. Returns an object of type Title_Creator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title_Creator.</param>
		/// <returns>Object of type Title_Creator.</returns>
		public Title_Creator Title_CreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title_Creator value)
		{
			return Title_CreatorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleCreatorID,
				value.CreatorName,
				value.MARCCreator_a,
				value.MARCCreator_b,
				value.MARCCreator_c,
				value.MARCCreator_d,
				value.CreatorRoleTypeID,
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ExternalCreationUser,
				value.ExternalLastModifiedUser,
				value.ProductionDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Title_Creator object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Title_Creator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title_Creator.</param>
		/// <returns>Object of type CustomDataAccessStatus<Title_Creator>.</returns>
		public CustomDataAccessStatus<Title_Creator> Title_CreatorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title_Creator value  )
		{
			return Title_CreatorManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage Title_Creator object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Title_Creator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title_Creator.</param>
		/// <returns>Object of type CustomDataAccessStatus<Title_Creator>.</returns>
		public CustomDataAccessStatus<Title_Creator> Title_CreatorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title_Creator value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Title_Creator returnValue = Title_CreatorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.CreatorName,
						value.MARCCreator_a,
						value.MARCCreator_b,
						value.MARCCreator_c,
						value.MARCCreator_d,
						value.CreatorRoleTypeID,
						value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ExternalCreationUser,
						value.ExternalLastModifiedUser,
						value.ProductionDate);
				
				return new CustomDataAccessStatus<Title_Creator>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (Title_CreatorDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleCreatorID))
				{
				return new CustomDataAccessStatus<Title_Creator>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Title_Creator>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Title_Creator returnValue = Title_CreatorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleCreatorID,
						value.CreatorName,
						value.MARCCreator_a,
						value.MARCCreator_b,
						value.MARCCreator_c,
						value.MARCCreator_d,
						value.CreatorRoleTypeID,
						value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ExternalCreationUser,
						value.ExternalLastModifiedUser,
						value.ProductionDate);
					
				return new CustomDataAccessStatus<Title_Creator>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Title_Creator>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

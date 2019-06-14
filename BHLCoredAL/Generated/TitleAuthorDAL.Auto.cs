
// Generated 6/14/2019 5:14:33 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleAuthorDAL is based upon dbo.TitleAuthor.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleAuthorDAL
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
	partial class TitleAuthorDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.TitleAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAuthorID"></param>
		/// <returns>Object of type TitleAuthor.</returns>
		public TitleAuthor TitleAuthorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAuthorID)
		{
			return TitleAuthorSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleAuthorID );
		}
			
		/// <summary>
		/// Select values from dbo.TitleAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAuthorID"></param>
		/// <returns>Object of type TitleAuthor.</returns>
		public TitleAuthor TitleAuthorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAuthorID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAuthorSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAuthorID", SqlDbType.Int, null, false, titleAuthorID)))
			{
				using (CustomSqlHelper<TitleAuthor> helper = new CustomSqlHelper<TitleAuthor>())
				{
					CustomGenericList<TitleAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAuthor o = list[0];
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
		/// Select values from dbo.TitleAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAuthorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleAuthorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAuthorID)
		{
			return TitleAuthorSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleAuthorID );
		}
		
		/// <summary>
		/// Select values from dbo.TitleAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAuthorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleAuthorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAuthorSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleAuthorID", SqlDbType.Int, null, false, titleAuthorID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.TitleAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="authorID"></param>
		/// <param name="authorRoleID"></param>
		/// <param name="relationship"></param>
		/// <param name="titleOfWork"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="sequenceOrder"></param>
		/// <returns>Object of type TitleAuthor.</returns>
		public TitleAuthor TitleAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			int authorID,
			int? authorRoleID,
			string relationship,
			string titleOfWork,
			int? creationUserID,
			int? lastModifiedUserID,
			short sequenceOrder)
		{
			return TitleAuthorInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, authorID, authorRoleID, relationship, titleOfWork, creationUserID, lastModifiedUserID, sequenceOrder );
		}
		
		/// <summary>
		/// Insert values into dbo.TitleAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="authorID"></param>
		/// <param name="authorRoleID"></param>
		/// <param name="relationship"></param>
		/// <param name="titleOfWork"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="sequenceOrder"></param>
		/// <returns>Object of type TitleAuthor.</returns>
		public TitleAuthor TitleAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			int authorID,
			int? authorRoleID,
			string relationship,
			string titleOfWork,
			int? creationUserID,
			int? lastModifiedUserID,
			short sequenceOrder)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAuthorInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleAuthorID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID),
					CustomSqlHelper.CreateInputParameter("AuthorRoleID", SqlDbType.Int, null, true, authorRoleID),
					CustomSqlHelper.CreateInputParameter("Relationship", SqlDbType.NVarChar, 150, false, relationship),
					CustomSqlHelper.CreateInputParameter("TitleOfWork", SqlDbType.NVarChar, 500, false, titleOfWork),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleAuthor> helper = new CustomSqlHelper<TitleAuthor>())
				{
					CustomGenericList<TitleAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAuthor o = list[0];
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
		/// Insert values into dbo.TitleAuthor. Returns an object of type TitleAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAuthor.</param>
		/// <returns>Object of type TitleAuthor.</returns>
		public TitleAuthor TitleAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAuthor value)
		{
			return TitleAuthorInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.TitleAuthor. Returns an object of type TitleAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAuthor.</param>
		/// <returns>Object of type TitleAuthor.</returns>
		public TitleAuthor TitleAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAuthor value)
		{
			return TitleAuthorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.AuthorID,
				value.AuthorRoleID,
				value.Relationship,
				value.TitleOfWork,
				value.CreationUserID,
				value.LastModifiedUserID,
				value.SequenceOrder);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.TitleAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAuthorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleAuthorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAuthorID)
		{
			return TitleAuthorDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleAuthorID );
		}
		
		/// <summary>
		/// Delete values from dbo.TitleAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAuthorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleAuthorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAuthorDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAuthorID", SqlDbType.Int, null, false, titleAuthorID), 
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
		/// Update values in dbo.TitleAuthor. Returns an object of type TitleAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleAuthorID"></param>
		/// <param name="titleID"></param>
		/// <param name="authorID"></param>
		/// <param name="authorRoleID"></param>
		/// <param name="relationship"></param>
		/// <param name="titleOfWork"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="sequenceOrder"></param>
		/// <returns>Object of type TitleAuthor.</returns>
		public TitleAuthor TitleAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleAuthorID,
			int titleID,
			int authorID,
			int? authorRoleID,
			string relationship,
			string titleOfWork,
			int? lastModifiedUserID,
			short sequenceOrder)
		{
			return TitleAuthorUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleAuthorID, titleID, authorID, authorRoleID, relationship, titleOfWork, lastModifiedUserID, sequenceOrder);
		}
		
		/// <summary>
		/// Update values in dbo.TitleAuthor. Returns an object of type TitleAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleAuthorID"></param>
		/// <param name="titleID"></param>
		/// <param name="authorID"></param>
		/// <param name="authorRoleID"></param>
		/// <param name="relationship"></param>
		/// <param name="titleOfWork"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="sequenceOrder"></param>
		/// <returns>Object of type TitleAuthor.</returns>
		public TitleAuthor TitleAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleAuthorID,
			int titleID,
			int authorID,
			int? authorRoleID,
			string relationship,
			string titleOfWork,
			int? lastModifiedUserID,
			short sequenceOrder)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAuthorUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleAuthorID", SqlDbType.Int, null, false, titleAuthorID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID),
					CustomSqlHelper.CreateInputParameter("AuthorRoleID", SqlDbType.Int, null, true, authorRoleID),
					CustomSqlHelper.CreateInputParameter("Relationship", SqlDbType.NVarChar, 150, false, relationship),
					CustomSqlHelper.CreateInputParameter("TitleOfWork", SqlDbType.NVarChar, 500, false, titleOfWork),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleAuthor> helper = new CustomSqlHelper<TitleAuthor>())
				{
					CustomGenericList<TitleAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleAuthor o = list[0];
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
		/// Update values in dbo.TitleAuthor. Returns an object of type TitleAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAuthor.</param>
		/// <returns>Object of type TitleAuthor.</returns>
		public TitleAuthor TitleAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAuthor value)
		{
			return TitleAuthorUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.TitleAuthor. Returns an object of type TitleAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAuthor.</param>
		/// <returns>Object of type TitleAuthor.</returns>
		public TitleAuthor TitleAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAuthor value)
		{
			return TitleAuthorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleAuthorID,
				value.TitleID,
				value.AuthorID,
				value.AuthorRoleID,
				value.Relationship,
				value.TitleOfWork,
				value.LastModifiedUserID,
				value.SequenceOrder);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.TitleAuthor object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleAuthor.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleAuthor>.</returns>
		public CustomDataAccessStatus<TitleAuthor> TitleAuthorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleAuthor value , int userId )
		{
			return TitleAuthorManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.TitleAuthor object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleAuthor.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleAuthor>.</returns>
		public CustomDataAccessStatus<TitleAuthor> TitleAuthorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleAuthor value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TitleAuthor returnValue = TitleAuthorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.AuthorID,
						value.AuthorRoleID,
						value.Relationship,
						value.TitleOfWork,
						value.CreationUserID,
						value.LastModifiedUserID,
						value.SequenceOrder);
				
				return new CustomDataAccessStatus<TitleAuthor>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleAuthorDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAuthorID))
				{
				return new CustomDataAccessStatus<TitleAuthor>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleAuthor>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TitleAuthor returnValue = TitleAuthorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleAuthorID,
						value.TitleID,
						value.AuthorID,
						value.AuthorRoleID,
						value.Relationship,
						value.TitleOfWork,
						value.LastModifiedUserID,
						value.SequenceOrder);
					
				return new CustomDataAccessStatus<TitleAuthor>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleAuthor>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}



// Generated 6/25/2010 5:09:34 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotationPolygonDAL is based upon AnnotationPolygon.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotationPolygonDAL
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
	partial class AnnotationPolygonDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotationPolygon by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationPolygonID"></param>
		/// <returns>Object of type AnnotationPolygon.</returns>
		public AnnotationPolygon AnnotationPolygonSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationPolygonID)
		{
			return AnnotationPolygonSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotationPolygonID );
		}
			
		/// <summary>
		/// Select values from AnnotationPolygon by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationPolygonID"></param>
		/// <returns>Object of type AnnotationPolygon.</returns>
		public AnnotationPolygon AnnotationPolygonSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationPolygonID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationPolygonSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationPolygonID", SqlDbType.Int, null, false, annotationPolygonID)))
			{
				using (CustomSqlHelper<AnnotationPolygon> helper = new CustomSqlHelper<AnnotationPolygon>())
				{
					List<AnnotationPolygon> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationPolygon o = list[0];
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
		/// Select values from AnnotationPolygon by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationPolygonID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationPolygonSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationPolygonID)
		{
			return AnnotationPolygonSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotationPolygonID );
		}
		
		/// <summary>
		/// Select values from AnnotationPolygon by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationPolygonID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationPolygonSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationPolygonID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationPolygonSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotationPolygonID", SqlDbType.Int, null, false, annotationPolygonID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotationPolygon.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="polygonX1"></param>
		/// <param name="polygonY1"></param>
		/// <param name="polygonX2"></param>
		/// <param name="polygonY2"></param>
		/// <returns>Object of type AnnotationPolygon.</returns>
		public AnnotationPolygon AnnotationPolygonInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			int? polygonX1,
			int? polygonY1,
			int? polygonX2,
			int? polygonY2)
		{
			return AnnotationPolygonInsertAuto( sqlConnection, sqlTransaction, "BHL", annotationID, polygonX1, polygonY1, polygonX2, polygonY2 );
		}
		
		/// <summary>
		/// Insert values into AnnotationPolygon.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="polygonX1"></param>
		/// <param name="polygonY1"></param>
		/// <param name="polygonX2"></param>
		/// <param name="polygonY2"></param>
		/// <returns>Object of type AnnotationPolygon.</returns>
		public AnnotationPolygon AnnotationPolygonInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			int? polygonX1,
			int? polygonY1,
			int? polygonX2,
			int? polygonY2)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationPolygonInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AnnotationPolygonID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("PolygonX1", SqlDbType.Int, null, true, polygonX1),
					CustomSqlHelper.CreateInputParameter("PolygonY1", SqlDbType.Int, null, true, polygonY1),
					CustomSqlHelper.CreateInputParameter("PolygonX2", SqlDbType.Int, null, true, polygonX2),
					CustomSqlHelper.CreateInputParameter("PolygonY2", SqlDbType.Int, null, true, polygonY2), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationPolygon> helper = new CustomSqlHelper<AnnotationPolygon>())
				{
					List<AnnotationPolygon> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationPolygon o = list[0];
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
		/// Insert values into AnnotationPolygon. Returns an object of type AnnotationPolygon.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationPolygon.</param>
		/// <returns>Object of type AnnotationPolygon.</returns>
		public AnnotationPolygon AnnotationPolygonInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationPolygon value)
		{
			return AnnotationPolygonInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotationPolygon. Returns an object of type AnnotationPolygon.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationPolygon.</param>
		/// <returns>Object of type AnnotationPolygon.</returns>
		public AnnotationPolygon AnnotationPolygonInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationPolygon value)
		{
			return AnnotationPolygonInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationID,
				value.PolygonX1,
				value.PolygonY1,
				value.PolygonX2,
				value.PolygonY2);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotationPolygon by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationPolygonID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationPolygonDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationPolygonID)
		{
			return AnnotationPolygonDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotationPolygonID );
		}
		
		/// <summary>
		/// Delete values from AnnotationPolygon by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationPolygonID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationPolygonDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationPolygonID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationPolygonDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationPolygonID", SqlDbType.Int, null, false, annotationPolygonID), 
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
		/// Update values in AnnotationPolygon. Returns an object of type AnnotationPolygon.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationPolygonID"></param>
		/// <param name="annotationID"></param>
		/// <param name="polygonX1"></param>
		/// <param name="polygonY1"></param>
		/// <param name="polygonX2"></param>
		/// <param name="polygonY2"></param>
		/// <returns>Object of type AnnotationPolygon.</returns>
		public AnnotationPolygon AnnotationPolygonUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationPolygonID,
			int annotationID,
			int? polygonX1,
			int? polygonY1,
			int? polygonX2,
			int? polygonY2)
		{
			return AnnotationPolygonUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotationPolygonID, annotationID, polygonX1, polygonY1, polygonX2, polygonY2);
		}
		
		/// <summary>
		/// Update values in AnnotationPolygon. Returns an object of type AnnotationPolygon.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationPolygonID"></param>
		/// <param name="annotationID"></param>
		/// <param name="polygonX1"></param>
		/// <param name="polygonY1"></param>
		/// <param name="polygonX2"></param>
		/// <param name="polygonY2"></param>
		/// <returns>Object of type AnnotationPolygon.</returns>
		public AnnotationPolygon AnnotationPolygonUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationPolygonID,
			int annotationID,
			int? polygonX1,
			int? polygonY1,
			int? polygonX2,
			int? polygonY2)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationPolygonUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationPolygonID", SqlDbType.Int, null, false, annotationPolygonID),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("PolygonX1", SqlDbType.Int, null, true, polygonX1),
					CustomSqlHelper.CreateInputParameter("PolygonY1", SqlDbType.Int, null, true, polygonY1),
					CustomSqlHelper.CreateInputParameter("PolygonX2", SqlDbType.Int, null, true, polygonX2),
					CustomSqlHelper.CreateInputParameter("PolygonY2", SqlDbType.Int, null, true, polygonY2), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationPolygon> helper = new CustomSqlHelper<AnnotationPolygon>())
				{
					List<AnnotationPolygon> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationPolygon o = list[0];
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
		/// Update values in AnnotationPolygon. Returns an object of type AnnotationPolygon.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationPolygon.</param>
		/// <returns>Object of type AnnotationPolygon.</returns>
		public AnnotationPolygon AnnotationPolygonUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationPolygon value)
		{
			return AnnotationPolygonUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotationPolygon. Returns an object of type AnnotationPolygon.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationPolygon.</param>
		/// <returns>Object of type AnnotationPolygon.</returns>
		public AnnotationPolygon AnnotationPolygonUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationPolygon value)
		{
			return AnnotationPolygonUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationPolygonID,
				value.AnnotationID,
				value.PolygonX1,
				value.PolygonY1,
				value.PolygonX2,
				value.PolygonY2);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotationPolygon object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationPolygon.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationPolygon.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationPolygon>.</returns>
		public CustomDataAccessStatus<AnnotationPolygon> AnnotationPolygonManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationPolygon value  )
		{
			return AnnotationPolygonManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotationPolygon object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationPolygon.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationPolygon.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationPolygon>.</returns>
		public CustomDataAccessStatus<AnnotationPolygon> AnnotationPolygonManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationPolygon value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotationPolygon returnValue = AnnotationPolygonInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID,
						value.PolygonX1,
						value.PolygonY1,
						value.PolygonX2,
						value.PolygonY2);
				
				return new CustomDataAccessStatus<AnnotationPolygon>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotationPolygonDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationPolygonID))
				{
				return new CustomDataAccessStatus<AnnotationPolygon>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotationPolygon>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotationPolygon returnValue = AnnotationPolygonUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationPolygonID,
						value.AnnotationID,
						value.PolygonX1,
						value.PolygonY1,
						value.PolygonX2,
						value.PolygonY2);
					
				return new CustomDataAccessStatus<AnnotationPolygon>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotationPolygon>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

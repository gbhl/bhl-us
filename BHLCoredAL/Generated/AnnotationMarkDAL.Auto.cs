
// Generated 5/5/2010 11:11:49 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotationMarkDAL is based upon AnnotationMark.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotationMarkDAL
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
	partial class AnnotationMarkDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotationMark by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationMarkID"></param>
		/// <returns>Object of type AnnotationMark.</returns>
		public AnnotationMark AnnotationMarkSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationMarkID)
		{
			return AnnotationMarkSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotationMarkID );
		}
			
		/// <summary>
		/// Select values from AnnotationMark by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationMarkID"></param>
		/// <returns>Object of type AnnotationMark.</returns>
		public AnnotationMark AnnotationMarkSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationMarkID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationMarkSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationMarkID", SqlDbType.Int, null, false, annotationMarkID)))
			{
				using (CustomSqlHelper<AnnotationMark> helper = new CustomSqlHelper<AnnotationMark>())
				{
					List<AnnotationMark> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationMark o = list[0];
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
		/// Select values from AnnotationMark by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationMarkID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationMarkSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationMarkID)
		{
			return AnnotationMarkSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotationMarkID );
		}
		
		/// <summary>
		/// Select values from AnnotationMark by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationMarkID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotationMarkSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationMarkID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationMarkSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotationMarkID", SqlDbType.Int, null, false, annotationMarkID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotationMark.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="sequenceNumber"></param>
		/// <param name="position"></param>
		/// <param name="annotationMarkTypeID"></param>
		/// <param name="content"></param>
		/// <param name="correctedContent"></param>
		/// <param name="comment"></param>
		/// <param name="polygonX1"></param>
		/// <param name="polygonY1"></param>
		/// <param name="polygonX2"></param>
		/// <param name="polygonY2"></param>
		/// <returns>Object of type AnnotationMark.</returns>
		public AnnotationMark AnnotationMarkInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationID,
			string externalIdentifier,
			int sequenceNumber,
			string position,
			int? annotationMarkTypeID,
			string content,
			string correctedContent,
			string comment,
			int? polygonX1,
			int? polygonY1,
			int? polygonX2,
			int? polygonY2)
		{
			return AnnotationMarkInsertAuto( sqlConnection, sqlTransaction, "BHL", annotationID, externalIdentifier, sequenceNumber, position, annotationMarkTypeID, content, correctedContent, comment, polygonX1, polygonY1, polygonX2, polygonY2 );
		}
		
		/// <summary>
		/// Insert values into AnnotationMark.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="sequenceNumber"></param>
		/// <param name="position"></param>
		/// <param name="annotationMarkTypeID"></param>
		/// <param name="content"></param>
		/// <param name="correctedContent"></param>
		/// <param name="comment"></param>
		/// <param name="polygonX1"></param>
		/// <param name="polygonY1"></param>
		/// <param name="polygonX2"></param>
		/// <param name="polygonY2"></param>
		/// <returns>Object of type AnnotationMark.</returns>
		public AnnotationMark AnnotationMarkInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationID,
			string externalIdentifier,
			int sequenceNumber,
			string position,
			int? annotationMarkTypeID,
			string content,
			string correctedContent,
			string comment,
			int? polygonX1,
			int? polygonY1,
			int? polygonX2,
			int? polygonY2)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationMarkInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AnnotationMarkID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
					CustomSqlHelper.CreateInputParameter("SequenceNumber", SqlDbType.Int, null, false, sequenceNumber),
					CustomSqlHelper.CreateInputParameter("Position", SqlDbType.NVarChar, 50, false, position),
					CustomSqlHelper.CreateInputParameter("AnnotationMarkTypeID", SqlDbType.Int, null, true, annotationMarkTypeID),
					CustomSqlHelper.CreateInputParameter("Content", SqlDbType.NVarChar, 1073741823, false, content),
					CustomSqlHelper.CreateInputParameter("CorrectedContent", SqlDbType.NVarChar, 1073741823, false, correctedContent),
					CustomSqlHelper.CreateInputParameter("Comment", SqlDbType.NVarChar, 1073741823, false, comment),
					CustomSqlHelper.CreateInputParameter("PolygonX1", SqlDbType.Int, null, true, polygonX1),
					CustomSqlHelper.CreateInputParameter("PolygonY1", SqlDbType.Int, null, true, polygonY1),
					CustomSqlHelper.CreateInputParameter("PolygonX2", SqlDbType.Int, null, true, polygonX2),
					CustomSqlHelper.CreateInputParameter("PolygonY2", SqlDbType.Int, null, true, polygonY2), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationMark> helper = new CustomSqlHelper<AnnotationMark>())
				{
					List<AnnotationMark> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationMark o = list[0];
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
		/// Insert values into AnnotationMark. Returns an object of type AnnotationMark.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationMark.</param>
		/// <returns>Object of type AnnotationMark.</returns>
		public AnnotationMark AnnotationMarkInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationMark value)
		{
			return AnnotationMarkInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotationMark. Returns an object of type AnnotationMark.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationMark.</param>
		/// <returns>Object of type AnnotationMark.</returns>
		public AnnotationMark AnnotationMarkInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationMark value)
		{
			return AnnotationMarkInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationID,
				value.ExternalIdentifier,
				value.SequenceNumber,
				value.Position,
				value.AnnotationMarkTypeID,
				value.Content,
				value.CorrectedContent,
				value.Comment,
				value.PolygonX1,
				value.PolygonY1,
				value.PolygonX2,
				value.PolygonY2);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotationMark by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationMarkID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationMarkDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationMarkID)
		{
			return AnnotationMarkDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotationMarkID );
		}
		
		/// <summary>
		/// Delete values from AnnotationMark by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationMarkID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotationMarkDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationMarkID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationMarkDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationMarkID", SqlDbType.Int, null, false, annotationMarkID), 
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
		/// Update values in AnnotationMark. Returns an object of type AnnotationMark.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotationMarkID"></param>
		/// <param name="annotationID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="sequenceNumber"></param>
		/// <param name="position"></param>
		/// <param name="annotationMarkTypeID"></param>
		/// <param name="content"></param>
		/// <param name="correctedContent"></param>
		/// <param name="comment"></param>
		/// <param name="polygonX1"></param>
		/// <param name="polygonY1"></param>
		/// <param name="polygonX2"></param>
		/// <param name="polygonY2"></param>
		/// <returns>Object of type AnnotationMark.</returns>
		public AnnotationMark AnnotationMarkUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotationMarkID,
			int annotationID,
			string externalIdentifier,
			int sequenceNumber,
			string position,
			int? annotationMarkTypeID,
			string content,
			string correctedContent,
			string comment,
			int? polygonX1,
			int? polygonY1,
			int? polygonX2,
			int? polygonY2)
		{
			return AnnotationMarkUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotationMarkID, annotationID, externalIdentifier, sequenceNumber, position, annotationMarkTypeID, content, correctedContent, comment, polygonX1, polygonY1, polygonX2, polygonY2);
		}
		
		/// <summary>
		/// Update values in AnnotationMark. Returns an object of type AnnotationMark.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotationMarkID"></param>
		/// <param name="annotationID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="sequenceNumber"></param>
		/// <param name="position"></param>
		/// <param name="annotationMarkTypeID"></param>
		/// <param name="content"></param>
		/// <param name="correctedContent"></param>
		/// <param name="comment"></param>
		/// <param name="polygonX1"></param>
		/// <param name="polygonY1"></param>
		/// <param name="polygonX2"></param>
		/// <param name="polygonY2"></param>
		/// <returns>Object of type AnnotationMark.</returns>
		public AnnotationMark AnnotationMarkUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotationMarkID,
			int annotationID,
			string externalIdentifier,
			int sequenceNumber,
			string position,
			int? annotationMarkTypeID,
			string content,
			string correctedContent,
			string comment,
			int? polygonX1,
			int? polygonY1,
			int? polygonX2,
			int? polygonY2)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationMarkUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotationMarkID", SqlDbType.Int, null, false, annotationMarkID),
					CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
					CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
					CustomSqlHelper.CreateInputParameter("SequenceNumber", SqlDbType.Int, null, false, sequenceNumber),
					CustomSqlHelper.CreateInputParameter("Position", SqlDbType.NVarChar, 50, false, position),
					CustomSqlHelper.CreateInputParameter("AnnotationMarkTypeID", SqlDbType.Int, null, true, annotationMarkTypeID),
					CustomSqlHelper.CreateInputParameter("Content", SqlDbType.NVarChar, 1073741823, false, content),
					CustomSqlHelper.CreateInputParameter("CorrectedContent", SqlDbType.NVarChar, 1073741823, false, correctedContent),
					CustomSqlHelper.CreateInputParameter("Comment", SqlDbType.NVarChar, 1073741823, false, comment),
					CustomSqlHelper.CreateInputParameter("PolygonX1", SqlDbType.Int, null, true, polygonX1),
					CustomSqlHelper.CreateInputParameter("PolygonY1", SqlDbType.Int, null, true, polygonY1),
					CustomSqlHelper.CreateInputParameter("PolygonX2", SqlDbType.Int, null, true, polygonX2),
					CustomSqlHelper.CreateInputParameter("PolygonY2", SqlDbType.Int, null, true, polygonY2), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotationMark> helper = new CustomSqlHelper<AnnotationMark>())
				{
					List<AnnotationMark> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotationMark o = list[0];
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
		/// Update values in AnnotationMark. Returns an object of type AnnotationMark.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationMark.</param>
		/// <returns>Object of type AnnotationMark.</returns>
		public AnnotationMark AnnotationMarkUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationMark value)
		{
			return AnnotationMarkUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotationMark. Returns an object of type AnnotationMark.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationMark.</param>
		/// <returns>Object of type AnnotationMark.</returns>
		public AnnotationMark AnnotationMarkUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationMark value)
		{
			return AnnotationMarkUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotationMarkID,
				value.AnnotationID,
				value.ExternalIdentifier,
				value.SequenceNumber,
				value.Position,
				value.AnnotationMarkTypeID,
				value.Content,
				value.CorrectedContent,
				value.Comment,
				value.PolygonX1,
				value.PolygonY1,
				value.PolygonX2,
				value.PolygonY2);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotationMark object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationMark.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotationMark.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationMark>.</returns>
		public CustomDataAccessStatus<AnnotationMark> AnnotationMarkManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotationMark value  )
		{
			return AnnotationMarkManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotationMark object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotationMark.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotationMark.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotationMark>.</returns>
		public CustomDataAccessStatus<AnnotationMark> AnnotationMarkManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotationMark value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotationMark returnValue = AnnotationMarkInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationID,
						value.ExternalIdentifier,
						value.SequenceNumber,
						value.Position,
						value.AnnotationMarkTypeID,
						value.Content,
						value.CorrectedContent,
						value.Comment,
						value.PolygonX1,
						value.PolygonY1,
						value.PolygonX2,
						value.PolygonY2);
				
				return new CustomDataAccessStatus<AnnotationMark>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotationMarkDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationMarkID))
				{
				return new CustomDataAccessStatus<AnnotationMark>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotationMark>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotationMark returnValue = AnnotationMarkUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotationMarkID,
						value.AnnotationID,
						value.ExternalIdentifier,
						value.SequenceNumber,
						value.Position,
						value.AnnotationMarkTypeID,
						value.Content,
						value.CorrectedContent,
						value.Comment,
						value.PolygonX1,
						value.PolygonY1,
						value.PolygonX2,
						value.PolygonY2);
					
				return new CustomDataAccessStatus<AnnotationMark>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotationMark>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation

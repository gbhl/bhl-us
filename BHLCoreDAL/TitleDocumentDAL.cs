
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class TitleDocumentDAL
	{
        public List<TitleDocument> TitleDocumentSelectByTitleID(SqlConnection sqlConnection,
         SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleDocumentSelectByTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<TitleDocument> helper = new CustomSqlHelper<TitleDocument>())
                {
                    List<TitleDocument> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        public List<TitleDocument> TitleDocumentSelectByBookID(SqlConnection sqlConnection,
         SqlTransaction sqlTransaction, int bookID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleDocumentSelectByBookID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("BookID", SqlDbType.Int, null, false, bookID)))
            {
                using (CustomSqlHelper<TitleDocument> helper = new CustomSqlHelper<TitleDocument>())
                {
                    List<TitleDocument> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        public List<TitleDocument> TitleDocumentSelectBySegmentID(SqlConnection sqlConnection,
         SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleDocumentSelectBySegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<TitleDocument> helper = new CustomSqlHelper<TitleDocument>())
                {
                    List<TitleDocument> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }
    }
}


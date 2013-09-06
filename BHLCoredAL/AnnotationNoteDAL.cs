
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class AnnotationNoteDAL
	{
        public bool AnnotationNoteDeleteByAnnotationID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int annotationID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationNoteDeleteByAnnotationID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID),
                CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
            {
                int returnCode = CustomSqlHelper.ExecuteNonQuery(command, "ReturnCode");

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

        public CustomGenericList<AnnotationNote> AnnotationNoteSelectByAnnotationID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int annotationID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationNoteSelectByAnnotationID",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID)))
            {
                using (CustomSqlHelper<AnnotationNote> helper = new CustomSqlHelper<AnnotationNote>())
                {
                    CustomGenericList<AnnotationNote> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}


#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class Annotation_AnnotationConceptDAL
    {
        public CustomGenericList<CustomDataRow> Annotation_AnnotationConceptSelectByAnnotationID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int annotationID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.Annotation_AnnotationConceptSelectByAnnotationID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID)))
            {
                return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
            }
        }

        public bool Annotation_AnnotationConceptDeleteByAnnotationID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int annotationID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.Annotation_AnnotationConceptDeleteByAnnotationID", connection, transaction,
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
    }
}

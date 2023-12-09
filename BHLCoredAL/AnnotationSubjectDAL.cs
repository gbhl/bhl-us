
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class AnnotationSubjectDAL
    {
        public List<CustomDataRow> AnnotationSubjectSelectByAnnotationID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int annotationID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectSelectByAnnotationID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID)))
            {
                return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
            }
        }

        public List<AnnotationSubject> AnnotationSubjectSelectBySubjectText(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string subjectText,
            int annotationSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectSelectBySubjectText", 
                    connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SubjectText", SqlDbType.NVarChar, 150, false, subjectText),
                    CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID)))
            {
                using (CustomSqlHelper<AnnotationSubject> helper = new CustomSqlHelper<AnnotationSubject>())
                {
                    List<AnnotationSubject> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public AnnotationSubject AnnotationSubjectSelect(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int annotationSubjectID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectSelect", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AnnotationSubjectID", SqlDbType.Int, null, false, annotationSubjectID)))
            {
                using (CustomSqlHelper<AnnotationSubject> helper = new CustomSqlHelper<AnnotationSubject>())
                {
                    List<AnnotationSubject> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

    }
}

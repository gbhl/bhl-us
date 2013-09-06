
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class AnnotationSubjectDAL
    {
        public CustomGenericList<CustomDataRow> AnnotationSubjectSelectByAnnotationID(
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

        public CustomGenericList<AnnotationSubject> AnnotationSubjectSelectBySubjectText(
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
                    CustomGenericList<AnnotationSubject> list = helper.ExecuteReader(command);
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

        public bool AnnotationSubjectDeleteByAnnotationID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int annotationID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectDeleteByAnnotationID", connection, transaction,
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

        public AnnotationSubject AnnotationSubjectInsertUnique(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, AnnotationSubject subject)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectInsertUnique", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, subject.AnnotationID),
                    CustomSqlHelper.CreateInputParameter("AnnotationSubjectCategoryID", SqlDbType.Int, null, true, subject.AnnotationSubjectCategoryID),
                    CustomSqlHelper.CreateInputParameter("AnnotationKeywordTargetID", SqlDbType.Int, null, false, subject.AnnotationKeywordTargetID),
                    CustomSqlHelper.CreateInputParameter("SubjectText", SqlDbType.NVarChar, 150, false, subject.SubjectText),
                    CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
            {
                using (CustomSqlHelper<AnnotationSubject> helper = new CustomSqlHelper<AnnotationSubject>())
                {
                    CustomGenericList<AnnotationSubject> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        AnnotationSubject o = list[0];
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
    }
}

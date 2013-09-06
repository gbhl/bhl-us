
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class AnnotationDAL
	{
        public CustomGenericList<Annotation> AnnotationsSelectByItemID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSelectByItemID",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<Annotation> helper = new CustomSqlHelper<Annotation>())
                {
                    CustomGenericList<Annotation> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list;
                    else
                        return null;
                }
            }
        }

        public Annotation AnnotationSelectByExternalIdentifer(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            String externalIdentifier,
            int annotationSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSelectByExternalIdentifer",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
                CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID)))
            {
                using (CustomSqlHelper<Annotation> helper = new CustomSqlHelper<Annotation>())
                {
                    CustomGenericList<Annotation> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }


        public CustomGenericList<CustomDataRow> AnnotationRelationSelectByAnnotationID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int annotationID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationRelationSelectByAnnotationID",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("AnnotationID", SqlDbType.Int, null, false, annotationID)))
            {
                using (CustomSqlHelper<CustomDataRow> helper = new CustomSqlHelper<CustomDataRow>())
                {
                    CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                    return list;
                }
            }
        }

        public CustomGenericList<SearchBookResult> SearchPageForConcept(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string annotationConceptCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.SearchPageForConcept",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    CustomGenericList<SearchBookResult> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<SearchBookResult> SearchPageForSubject(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int annotationSubjectCategoryID,
            int subjectID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.SearchPageForSubject",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("AnnotationSubjectCategoryID", SqlDbType.Int, null, false, annotationSubjectCategoryID),
                CustomSqlHelper.CreateInputParameter("AnnotationSubjectID", SqlDbType.NVarChar, 50, false, subjectID)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    CustomGenericList<SearchBookResult> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

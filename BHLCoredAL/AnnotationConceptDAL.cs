
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class AnnotationConceptDAL
	{
        public CustomGenericList<AnnotationConcept> AnnotationConceptSelectAll(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int annotationSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationConceptSelectAll", 
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID)))
            {
                using (CustomSqlHelper<AnnotationConcept> helper = new CustomSqlHelper<AnnotationConcept>())
                {
                    CustomGenericList<AnnotationConcept> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list;
                    else
                        return null;
                }
            }
        }

        public CustomGenericList<AnnotationConcept> AnnotationConceptSelectByConceptText(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string conceptText,
            int annotationSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationConceptSelectByConceptText",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("ConceptText", SqlDbType.NVarChar, 100, false, conceptText),
                CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID)))
            {
                using (CustomSqlHelper<AnnotationConcept> helper = new CustomSqlHelper<AnnotationConcept>())
                {
                    CustomGenericList<AnnotationConcept> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public AnnotationConcept AnnotationConceptSelectByCode(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string annotationConceptCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationConceptSelectByCode",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("AnnotationConceptCode", SqlDbType.NVarChar, 20, false, annotationConceptCode)))
            {
                using (CustomSqlHelper<AnnotationConcept> helper = new CustomSqlHelper<AnnotationConcept>())
                {
                    CustomGenericList<AnnotationConcept> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }
    }
}

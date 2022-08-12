
#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class AnnotationSubjectCategoryDAL
	{
        public AnnotationSubjectCategory AnnotationSubjectCategorySelectByCode(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            String categoryCode,
            int annotationSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotationSubjectCategorySelectByCode", 
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("SubjectCategoryCode", SqlDbType.NVarChar, 20, false, categoryCode),
                CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID)))
            {
                using (CustomSqlHelper<AnnotationSubjectCategory> helper = new CustomSqlHelper<AnnotationSubjectCategory>())
                {
                    List<AnnotationSubjectCategory> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

	}
}

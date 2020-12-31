
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
	public partial class AnnotatedTitleDAL
	{
        public AnnotatedTitle AnnotatedTitleSelectByExternalIdentifer(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            String externalIdentifier,
            int annotationSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedTitleSelectByExternalIdentifer",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
                CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, false, annotationSourceID)))
            {
                using (CustomSqlHelper<AnnotatedTitle> helper = new CustomSqlHelper<AnnotatedTitle>())
                {
                    List<AnnotatedTitle> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }
    }
}

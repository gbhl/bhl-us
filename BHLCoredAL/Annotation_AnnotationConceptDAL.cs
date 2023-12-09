
#region Using

using CustomDataAccess;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class Annotation_AnnotationConceptDAL
    {
        public List<CustomDataRow> Annotation_AnnotationConceptSelectByAnnotationID(
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
    }
}


#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class TitleAssociation_TitleIdentifierDAL
	{
        public List<TitleAssociation_TitleIdentifier> TitleAssociation_TitleIdentifierSelectByTitleAssociationID(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int titleAssociationID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierSelectByTitleAssociationID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleAssociationID", SqlDbType.Int, null, false, titleAssociationID)))
            {
                using (CustomSqlHelper<TitleAssociation_TitleIdentifier> helper = new CustomSqlHelper<TitleAssociation_TitleIdentifier>())
                {
                    List<TitleAssociation_TitleIdentifier> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

    }
}

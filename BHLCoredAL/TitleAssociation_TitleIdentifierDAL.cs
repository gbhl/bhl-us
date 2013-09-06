
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class TitleAssociation_TitleIdentifierDAL
	{
        public CustomGenericList<TitleAssociation_TitleIdentifier> TitleAssociation_TitleIdentifierSelectByTitleAssociationID(
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
                    CustomGenericList<TitleAssociation_TitleIdentifier> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

    }
}

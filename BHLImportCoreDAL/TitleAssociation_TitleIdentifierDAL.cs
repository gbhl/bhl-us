
#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class TitleAssociation_TitleIdentifierDAL
	{
        public TitleAssociation_TitleIdentifier TitleAssociation_TitleIdentifierSelectByKey(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string marcBibID,
            string marcTag,
            string marcIndicator2,
            string title,
            string section,
            string volume,
            string heading,
            string publication,
            string relationship,
            string identifierName,
            string identifierValue
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociation_TitleIdentifierSelectByKey", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, marcBibID),
                CustomSqlHelper.CreateInputParameter("MARCTag", SqlDbType.NVarChar, 20, false, marcTag),
                CustomSqlHelper.CreateInputParameter("MARCIndicator2", SqlDbType.NVarChar, 1, false, marcIndicator2),
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 500, false, title),
                CustomSqlHelper.CreateInputParameter("Section", SqlDbType.NVarChar, 500, false, section),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 500, false, volume),
                CustomSqlHelper.CreateInputParameter("Heading", SqlDbType.NVarChar, 500, false, heading),
                CustomSqlHelper.CreateInputParameter("Publication", SqlDbType.NVarChar, 500, false, publication),
                CustomSqlHelper.CreateInputParameter("Relationship", SqlDbType.NVarChar, 500, false, relationship),
                CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
                CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue)))
            {
                using (CustomSqlHelper<TitleAssociation_TitleIdentifier> helper = new CustomSqlHelper<TitleAssociation_TitleIdentifier>())
                {
                    List<TitleAssociation_TitleIdentifier> list = helper.ExecuteReader(command);

                    if (list.Count > 0)
                    {
                        return list[0];
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

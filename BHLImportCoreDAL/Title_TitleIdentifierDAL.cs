
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class Title_TitleIdentifierDAL
    {
        public Title_TitleIdentifier Title_TitleIdentifierSelectByKeyNameAndValue(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string marcBibID,
            string identifierName,
            string identifierValue
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_TitleIdentifierSelectByKeyNameAndValue", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, marcBibID),
                CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
                CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 50, true, identifierValue)))
            {
                using (CustomSqlHelper<Title_TitleIdentifier> helper = new CustomSqlHelper<Title_TitleIdentifier>())
                {
                    CustomGenericList<Title_TitleIdentifier> list = helper.ExecuteReader(command);

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

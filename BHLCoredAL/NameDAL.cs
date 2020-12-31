
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class NameDAL
	{
        /// <summary>
        /// Return all active names that match partically or exactly the specified string.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Name> NameSelectByNameString(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string name)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSelectByNameString", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("NameString", SqlDbType.NVarChar, 100, false, name)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        /// <summary>
        /// Return the name that exactly matches the specified string.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Name NameSelectByNameStringExact(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string name)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSelectByNameStringExact", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("NameString", SqlDbType.NVarChar, 100, false, name)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    List<Name> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public Name NameSelectByNameID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int nameId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSelectByNameID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("NameID", SqlDbType.Int, null, false, nameId)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    List<Name> names =  helper.ExecuteReader(command);
                    if (names.Count > 0)
                        return names[0];
                    else
                        return null;
                }
            }
        }

        public List<CustomDataRow> NameMetadataSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameMetadataSelectByItemID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
            {
                List<CustomDataRow> names = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                return names;
            }
        }

        public Name NameInsertComplete(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int nameSourceId, string nameString, short isActive,
            string resolvedNameString, string canonicalNameString, short isPreferred,
            string namebankID, string eolID, int userId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameInsertComplete", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("NameSourceID", SqlDbType.Int, null, false, nameSourceId),
                    CustomSqlHelper.CreateInputParameter("NameString", SqlDbType.NVarChar, 100, false, nameString),
                    CustomSqlHelper.CreateInputParameter("IsActive", SqlDbType.SmallInt, null, false, isActive),
                    CustomSqlHelper.CreateInputParameter("ResolvedNameString", SqlDbType.NVarChar, 100, false, resolvedNameString),
                    CustomSqlHelper.CreateInputParameter("CanonicalNameString", SqlDbType.NVarChar, 100, false, canonicalNameString),
                    CustomSqlHelper.CreateInputParameter("IsPreferred", SqlDbType.SmallInt, null, false, isPreferred),
                    CustomSqlHelper.CreateInputParameter("NameBankID", SqlDbType.NVarChar, 100, false, namebankID),
                    CustomSqlHelper.CreateInputParameter("EOLID", SqlDbType.NVarChar, 100, false, eolID),
                    CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, userId),
                    CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, userId)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    List<Name> names = helper.ExecuteReader(command);
                    if (names.Count > 0)
                        return names[0];
                    else
                        return null;
                }
            }
        }
    }
}

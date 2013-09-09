
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class Title_CreatorDAL
	{
        /// <summary>
        /// Select the new title_creator with the specified marcBibID, creator name, 
        /// role type, and importSourceID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="marcBibID"></param>
        /// <param name="creatorName"></param>
        /// <param name="creatorRoleTypeID"></param>
        /// <param name="importSourceID"></param>
        /// <returns>Object of type Title_Creator.</returns>
        public Title_Creator Title_CreatorSelectNewByKeyCreatorAndSource(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string marcBibID,
            string creatorName,
            string marcCreatorA,
            string marcCreatorB,
            string marcCreatorC,
            string marcCreatorD,
            int creatorRoleTypeID,
            int importSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_CreatorSelectNewByKeyCreatorAndSource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, marcBibID),
                CustomSqlHelper.CreateInputParameter("CreatorName", SqlDbType.NVarChar, 255, false, creatorName),
                CustomSqlHelper.CreateInputParameter("MARCCreator_a", SqlDbType.NVarChar, 450, true, marcCreatorA),
                CustomSqlHelper.CreateInputParameter("MARCCreator_b", SqlDbType.NVarChar, 450, true, marcCreatorB),
                CustomSqlHelper.CreateInputParameter("MARCCreator_c", SqlDbType.NVarChar, 450, true, marcCreatorC),
                CustomSqlHelper.CreateInputParameter("MARCCreator_d", SqlDbType.NVarChar, 450, true, marcCreatorD),
                CustomSqlHelper.CreateInputParameter("CreatorRoleTypeID", SqlDbType.Int, null, false, creatorRoleTypeID),
                CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID)))
            {
                using (CustomSqlHelper<Title_Creator> helper = new CustomSqlHelper<Title_Creator>())
                {
                    CustomGenericList<Title_Creator> list = helper.ExecuteReader(command);

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

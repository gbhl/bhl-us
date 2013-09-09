
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class TitleDAL
	{
        /// <summary>
        /// Select the new title with the specified marcBibID and importSourceID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="marcBibID"></param>
        /// <param name="importSourceID"></param>
        /// <returns>Object of type Title.</returns>
        public Title TitleSelectNewByKeyAndSource(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string marcBibID,
            int importSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectNewByKeyAndSource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, marcBibID),
                CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    CustomGenericList<Title> list = helper.ExecuteReader(command);

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

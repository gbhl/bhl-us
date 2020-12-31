
#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class TitleTagDAL
	{
        /// <summary>
        /// Select the new title tag with the specified marcBibID, tag text, and importSourceID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="marcBibID"></param>
        /// <param name="tagText"></param>
        /// <param name="importSourceID"></param>
        /// <returns>Object of type Title.</returns>
        public TitleTag TitleTagSelectNewByKeyTagTextAndSource(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string marcBibID,
            string tagText,
            int importSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleTagSelectNewByKeyTagTextAndSource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, marcBibID),
                CustomSqlHelper.CreateInputParameter("TagText", SqlDbType.NVarChar, 50, false, tagText),
                CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID)))
            {
                using (CustomSqlHelper<TitleTag> helper = new CustomSqlHelper<TitleTag>())
                {
                    List<TitleTag> list = helper.ExecuteReader(command);

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

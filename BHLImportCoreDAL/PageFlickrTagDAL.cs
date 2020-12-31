
#region Using

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class PageFlickrTagDAL
	{
        public List<PageFlickrTag> PageFlickrTagSelectForPageID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrTagSelectForPageID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<PageFlickrTag> helper = new CustomSqlHelper<PageFlickrTag>())
                {
                    List<PageFlickrTag> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

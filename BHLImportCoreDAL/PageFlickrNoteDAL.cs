
#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class PageFlickrNoteDAL
	{
        public CustomGenericList<PageFlickrNote> PageFlickrNoteSelectForPageID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrNoteSelectForPageID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<PageFlickrNote> helper = new CustomSqlHelper<PageFlickrNote>())
                {
                    CustomGenericList<PageFlickrNote> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

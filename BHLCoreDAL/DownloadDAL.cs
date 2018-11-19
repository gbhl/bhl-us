using CustomDataAccess;
using System;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public class DownloadDAL
    {
        public CustomGenericList<Tuple<string, string, string>> LinkSelectToExternalContent(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("LinkSelectToExternalContent", connection, transaction))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);

                CustomGenericList<Tuple<string, string, string>> links = new CustomGenericList<Tuple<string, string, string>>();
                foreach(CustomDataRow row in list)
                {
                    Tuple<string, string, string> link = new Tuple<string, string, string>(
                        row["Entity"].Value.ToString(),
                        row["Id"].Value.ToString() + "|" + row["Title"].Value.ToString(),
                        row["Url"].Value.ToString());
                    links.Add(link);
                }

                return links;
            }
        }
    }
}

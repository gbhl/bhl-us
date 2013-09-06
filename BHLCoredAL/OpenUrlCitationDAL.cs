using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
    public class OpenUrlCitationDAL
    {
        public CustomGenericList<OpenUrlCitation> OpenUrlCitationSelectByPageID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OpenUrlCitationSelectByPageID", connection, transaction,
    				CustomSqlHelper.CreateInputParameter( "PageID", SqlDbType.Int, null, true, pageID) ))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                return this.GetOpenUrlCitationList(list);
            }
        }

        public CustomGenericList<OpenUrlCitation> OpenUrlCitationSelectBySegmentID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OpenUrlCitationSelectBySegmentID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, true, segmentID)))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                return this.GetOpenUrlCitationList(list);
            }
        }

        public CustomGenericList<OpenUrlCitation> OpenUrlCitationSelectByDOI(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string doi)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OpenUrlCitationSelectByDOI", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, doi)))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                return this.GetOpenUrlCitationList(list);
            }
        }

        public CustomGenericList<OpenUrlCitation> OpenUrlCitationSelectByCitationDetails(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int titleID,
            int itemID,
            string title,
            string articleTitle,
            string authorLast,
            string authorFirst,
            string volume,
            string issue,
            string year,
            string startPage)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OpenUrlCitationSelectByCitationDetail", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, true, titleID),
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
                    CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, true, title),
                    CustomSqlHelper.CreateInputParameter("ArticleTitle", SqlDbType.NVarChar, 2000, true, articleTitle),
                    CustomSqlHelper.CreateInputParameter("AuthorLast", SqlDbType.NVarChar, 150, true, authorLast),
                    CustomSqlHelper.CreateInputParameter("AuthorFirst", SqlDbType.NVarChar, 100, true, authorFirst),
                    CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, true, volume),
                    CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 20, true, issue),
                    CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, true, year),
                    CustomSqlHelper.CreateInputParameter("StartPage", SqlDbType.NVarChar, 20, true, startPage)))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                return this.GetOpenUrlCitationList(list);
            }
        }

        public CustomGenericList<OpenUrlCitation> OpenUrlCitationSelectByCitationDetailsFT(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int titleID,
            int itemID,
            string title,
            string articleTitle,
            string authorLast,
            string authorFirst,
            string volume,
            string issue,
            string year,
            string startPage)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OpenUrlCitationSelectByCitationDetailFT", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, true, titleID),
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
                    CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, true, title),
                    CustomSqlHelper.CreateInputParameter("ArticleTitle", SqlDbType.NVarChar, 2000, true, articleTitle),
                    CustomSqlHelper.CreateInputParameter("AuthorLast", SqlDbType.NVarChar, 150, true, authorLast),
                    CustomSqlHelper.CreateInputParameter("AuthorFirst", SqlDbType.NVarChar, 100, true, authorFirst),
                    CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, true, volume),
                    CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 20, true, issue),
                    CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, true, year),
                    CustomSqlHelper.CreateInputParameter("StartPage", SqlDbType.NVarChar, 20, true, startPage)))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                return this.GetOpenUrlCitationList(list);
            }
        }

        /// <summary>
        /// Reads the specified list of generic data rows into a list of OpenUrlCitations
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private CustomGenericList<OpenUrlCitation> GetOpenUrlCitationList(CustomGenericList<CustomDataRow> list)
        {
            CustomGenericList<OpenUrlCitation> citations = new CustomGenericList<OpenUrlCitation>();
            foreach (CustomDataRow row in list)
            {
                OpenUrlCitation citation = new OpenUrlCitation();
                citation.PageID = Utility.ZeroIfNull(row["PageID"].Value);
                citation.ItemID = Utility.ZeroIfNull(row["ItemID"].Value);
                citation.TitleID = Utility.ZeroIfNull(row["TitleID"].Value);
                citation.PartID = Utility.ZeroIfNull(row["SegmentID"].Value);
                citation.FullTitle = row["FullTitle"].Value.ToString();
                citation.ArticleTitle = row["SegmentTitle"].Value.ToString();
                citation.JournalTitle = row["ContainerTitle"].Value.ToString();
                citation.PublisherPlace = row["PublisherPlace"].Value.ToString();
                citation.PublisherName = row["PublisherName"].Value.ToString();
                citation.Date = row["Date"].Value.ToString();
                citation.LanguageName = row["LanguageName"].Value.ToString();
                citation.Volume = row["Volume"].Value.ToString();
                citation.EditionStatement = row["EditionStatement"].Value.ToString();
                citation.CurrentPublicationFrequency = row["CurrentPublicationFrequency"].Value.ToString();
                citation.Genre = row["Genre"].Value.ToString();
                citation.Authors = row["Authors"].Value.ToString();
                citation.Subjects = row["Subjects"].Value.ToString();
                citation.StartPage = row["StartPage"].Value.ToString();
                citation.EndPage = row["EndPage"].Value.ToString();
                citation.Pages = row["Pages"].Value.ToString();
                citation.Issn = row["ISSN"].Value.ToString();
                citation.Isbn = row["ISBN"].Value.ToString();
                citation.Lccn = row["LCCN"].Value.ToString();
                citation.Oclc = row["OCLC"].Value.ToString();
                citation.Abbreviation = row["Abbreviation"].Value.ToString();
                citations.Add(citation);
            }

            return citations;
        }
    }
}

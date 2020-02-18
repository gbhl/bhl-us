using CustomDataAccess;
using MOBOT.BHL.API.BHLApiDataObjects3;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.API.BHLApiDAL
{
    public class Api3DAL : IApi3DAL
    {

        #region Page methods

        public List<Name> NamePageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageSelectByPageID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public Page PageSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiPageSelectByPageID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
                {
                    List<Page> list = helper.ExecuteReader(command);
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

        public List<Page> PageSelectByPageIDList(SqlConnection sqlConnection, SqlTransaction sqlTransaction, DataTable pageIDs)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.ApiPageSelectByPageIDList", connection, transaction))
            {
                SqlParameter idListParam = command.Parameters.AddWithValue("@IDList", pageIDs);
                idListParam.SqlDbType = SqlDbType.Structured;
                idListParam.TypeName = "dbo.IDListInt";

                using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<PageNumber> IndicatedPageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageSelectByPageID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<PageNumber> helper = new CustomSqlHelper<PageNumber>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<PageType> PageTypeSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeSelectByPageID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<PageType> helper = new CustomSqlHelper<PageType>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Page methods

        #region Item methods

        public List<Item> ItemSelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiItemSelectByItemID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Item> ItemSelectByBarcode(SqlConnection sqlConnection, SqlTransaction sqlTransaction, String barcode)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null) connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiItemSelectByBarcode", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Barcode", SqlDbType.NVarChar, 40, false, barcode)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<PageDetail> PageSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiPageSelectByItemID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<PageDetail> helper = new CustomSqlHelper<PageDetail>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Item methods

        #region Title methods
        public List<Title> TitleSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectAuto", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    List<Title> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Item> ItemSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiItemSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleId", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<TitleVariant> TitleVariantSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleVariantSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<TitleVariant> helper = new CustomSqlHelper<TitleVariant>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Identifier> TitleIdentifierSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleIdentifierSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Identifier> helper = new CustomSqlHelper<Identifier>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<TitleNote> TitleNoteSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleNoteSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<TitleNote> helper = new CustomSqlHelper<TitleNote>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Subject> SubjectSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleKeywordSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Subject> helper = new CustomSqlHelper<Subject>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Title> TitleSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string identifierName, string identifierValue)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectByIdentifier", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
                    CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 145, false, identifierValue)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Title> TitleSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectByDOI", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, doi)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Title methods

        #region Segment methods

        public List<Part> SegmentSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectByItemID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Part> SegmentSelectForSegmentID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int segmentId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectForSegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentId)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Identifier> SegmentIdentifierSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentIdentifierSelectBySegmentID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<Identifier> helper = new CustomSqlHelper<Identifier>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Subject> SubjectSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentKeywordSelectBySegmentID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<Subject> helper = new CustomSqlHelper<Subject>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Part> SegmentSelectRelated(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectRelated", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Name> NameSegmentSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiNameSegmentSelectBySegmentID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Part> SegmentSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
           string identifierName, string identifierValue)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectByIdentifier", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
                    CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Part> SegmentSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectByDOI", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, doi)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<PageDetail> PageSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiPageSelectBySegmentID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<PageDetail> helper = new CustomSqlHelper<PageDetail>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Segment methods

        #region Subject methods

        public List<Subject> KeywordSelectByKeyword(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string subject
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("KeywordSelectByKeyword", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, subject)))
            {
                using (CustomSqlHelper<Subject> helper = new CustomSqlHelper<Subject>())
                {
                    List<Subject> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Subject> TitleKeywordSelectLikeTag(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string subject
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleKeywordSelectLikeTag", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NVarChar, 50, false, subject),
                    CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, ""),
                    CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, 1000000)))
            {
                using (CustomSqlHelper<Subject> helper = new CustomSqlHelper<Subject>())
                {
                    List<Subject> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Full-text search for the specified title keyword.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="tag"></param>
        /// <param name="languageCode"></param>
        /// <param name="returnCount"></param>
        /// <returns></returns>
        public List<Subject> SearchTitleKeyword(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string tag)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchTitleKeyword", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 300, false, tag),
                    CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, ""),
                    CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, 1000000)))
            {
                using (CustomSqlHelper<Subject> helper = new CustomSqlHelper<Subject>())
                {
                    List<Subject> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Title> TitleSelectByKeyword(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string subject
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectByKeyword", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, subject)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    List<Title> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Part> SegmentSelectByKeyword(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string subject
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectByKeyword", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, subject)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    List<Part> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        #endregion Subject methods

        #region Author methods

        public List<Author> AuthorSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiAuthorSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Author> AuthorSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiAuthorSelectBySegmentID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Author> AuthorSelectByAuthorID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int authorID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiAuthorSelectWithNameByAuthorID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Author> AuthorSelectByIdentifier(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string identifierName, string identifierValue)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiAuthorSelectWithNameByIdentifier", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
                    CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Identifier> AuthorIdentifierSelectByAuthorID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int authorID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.AuthorIdentifierSelectByAuthorID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID)))
            {
                using (CustomSqlHelper<Identifier> helper = new CustomSqlHelper<Identifier>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        /// <summary>
        /// Select all Creators starting with a certain letter (or letters).
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of objects of type Creator.</returns>
        public List<Author> AuthorSelectNameStartsWith(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string name)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiAuthorSelectNameStartsWith", connection, transaction,
                CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 255, false, name)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    List<Author> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Execute a full-text search of creators.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of objects of type Creator.</returns>
        public List<Author> SearchAuthor(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string name)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSearchAuthor", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AuthorName", SqlDbType.NVarChar, 4000, false, name)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    List<Author> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Author> AuthorSelectForList(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            List<int> authorIds)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = connection.CreateCommand())
            {
                // Set up table-valued stored procedure parameter
                DataTable idTable = new DataTable();
                idTable.Columns.Add("ID", typeof(int));
                foreach (int authorId in authorIds) idTable.Rows.Add(authorId);

                command.CommandText = "ApiAuthorSelectForList";
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = command.Parameters.AddWithValue("@IDs", idTable);
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = "dbo.SearchIDTable";

                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    List<Author> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Title> TitleSelectByAuthor(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int authorID
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiTitleSelectByAuthor", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID)))
            {
                using (CustomSqlHelper<Title> helper = new CustomSqlHelper<Title>())
                {
                    List<Title> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Part> SegmentSelectByAuthor(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int authorID
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiSegmentSelectByAuthor", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    List<Part> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        #endregion Author methods

        #region Institution methods

        public List<Institution> InstitutionSelectAll(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<Institution> helper = new CustomSqlHelper<Institution>())
                {
                    List<Institution> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }


        public List<Contributor> InstitutionSelectBySegmentIDAndRole(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int segmentID,
            string institutionRoleName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectBySegmentIDAndRole", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName)))
            {
                using (CustomSqlHelper<Contributor> helper = new CustomSqlHelper<Contributor>())
                {
                    List<Contributor> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<Contributor> InstitutionSelectByItemIDAndRole(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID,
            string institutionRoleName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionSelectByItemIDAndRole", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName)))
            {
                using (CustomSqlHelper<Contributor> helper = new CustomSqlHelper<Contributor>())
                {
                    List<Contributor> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        #endregion Institution methods

        #region Language methods

        public string GetLanguageName(string languageCode)
        {
            Language selected = null;
            List<Language> languages = this.LanguageSelectWithPublishedItems(null, null);
            foreach (Language language in languages)
            {
                if (language.LanguageCode == languageCode)
                {
                    selected = language;
                    break;
                }
            }

            return (selected == null ? string.Empty : selected.LanguageName);
        }

        public List<Language> LanguageSelectWithPublishedItems(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("LanguageSelectWithPublishedItems", connection, transaction))
            {
                using (CustomSqlHelper<Language> helper = new CustomSqlHelper<Language>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Language methods

        #region Collection methods

        public string GetCollectionName(string collectionID)
        {
            Collection selected = null;
            List<Collection> collections = this.CollectionSelectActive(null, null);
            foreach(Collection collection in collections)
            {
                if (collection.CollectionID == collectionID)
                {
                    selected = collection;
                    break;
                }
            }

            return (selected == null ? string.Empty : selected.CollectionName);
        }

        public List<Collection> CollectionSelectActive(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectActive", connection, transaction))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Collection> CollectionSelectByTitleID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectByTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Collection> CollectionSelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectByItemID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Collection methods

        #region Name methods

        public List<Name> NameResolvedSelectByNameLike(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string name)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiNameResolvedSelectByNameLike", connection, transaction,
                CustomSqlHelper.CreateInputParameter("NameConfirmed", SqlDbType.NVarChar, 100, false, name),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, 1000000)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<PageDetail> PageSelectByResolvedName(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string nameConfirmed)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null) connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiPageSelectByResolvedName", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ResolvedNameString", SqlDbType.NVarChar, 100, false, nameConfirmed)))
            {
                using (CustomSqlHelper<PageDetail> helper = new CustomSqlHelper<PageDetail>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<PageDetail> PageSelectByNameIdentifier(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string identifierName, string identifierValue)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiPageSelectByNameIdentifier", connection, transaction,
                CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
                CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue)))
            {
                using (CustomSqlHelper<PageDetail> helper = new CustomSqlHelper<PageDetail>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<Identifier> NameIdentifierSelectByNameResolvedID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int nameResolvedID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiNameIdentifierSelectByNameResolvedID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("NameResolvedID", SqlDbType.Int, null, false, nameResolvedID)))
            {
                using (CustomSqlHelper<Identifier> helper = new CustomSqlHelper<Identifier>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        #endregion Name methods

        #region Search methods

        public List<SearchBookResult> SearchBook(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string authorLastName, string volume, string edition, int? year, string subject, string languageCode,
            int? collectionID, int returnCount)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchBook", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
                CustomSqlHelper.CreateInputParameter("AuthorLastName", SqlDbType.NVarChar, 255, false, authorLastName),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
                CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 450, false, edition),
                CustomSqlHelper.CreateInputParameter("Year", SqlDbType.SmallInt, null, true, year),
                CustomSqlHelper.CreateInputParameter("Subject", SqlDbType.NVarChar, 50, false, subject),
                CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, true, collectionID),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Call the database procedure that implements the multiple-field full-text book search.
        /// </summary>
        public List<SearchBookResult> SearchBookFullText(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string authorLastName, string volume, string edition, int? year, string subject, string languageCode,
            int? collectionID, int returnCount)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchBookFT", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
                CustomSqlHelper.CreateInputParameter("AuthorLastName", SqlDbType.NVarChar, 255, false, authorLastName),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
                CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 450, false, edition),
                CustomSqlHelper.CreateInputParameter("Year", SqlDbType.SmallInt, null, true, year),
                CustomSqlHelper.CreateInputParameter("Subject", SqlDbType.NVarChar, 50, false, subject),
                CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, true, collectionID),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<Part> SearchSegment(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string containerTitle, string author, string date, string volume, string series, string issue,
            int returnCount, string sortBy)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchSegment", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
                CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
                CustomSqlHelper.CreateInputParameter("Author", SqlDbType.NVarChar, 2000, false, author),
                CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
                CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
                CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, true, issue),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount),
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.NVarChar, 50, true, sortBy),
                CustomSqlHelper.CreateInputParameter("IncludeNoContent", SqlDbType.SmallInt, null, false, 1)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    List<Part> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Call the database procedure that implements the multiple-field full-text book search.
        /// </summary>
        public List<Part> SearchSegmentFullText(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string containerTitle, string author, string date, string volume, string series, string issue,
            int returnCount, string sortBy)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchSegmentAdvancedFT", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
                CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
                CustomSqlHelper.CreateInputParameter("Author", SqlDbType.NVarChar, 2000, false, author),
                CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
                CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
                CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, true, issue),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount),
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.NVarChar, 50, true, sortBy),
                CustomSqlHelper.CreateInputParameter("IncludeNoContent", SqlDbType.SmallInt, null, true, 1)))
            {
                using (CustomSqlHelper<Part> helper = new CustomSqlHelper<Part>())
                {
                    List<Part> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        #endregion Search methods

        #region Validation methods

        public ApiKey ApiKeySelectByKey(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            Guid keyValue)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiKeySelectByKey", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ApiKeyValue", SqlDbType.UniqueIdentifier, null, false, keyValue)))
            {
                using (CustomSqlHelper<ApiKey> helper = new CustomSqlHelper<ApiKey>())
                {
                    List<ApiKey> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        #endregion Validation methods

    }
}

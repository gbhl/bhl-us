using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
    public class SearchDAL
    {
        /// <summary>
        /// Call the database procedure that implements the book search.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="maxIdentifiers"></param>
        /// <param name="startId"></param>
        /// <param name="fromDate"></param>
        /// <param name="untilDate"></param>
        /// <returns></returns>
        public List<SearchBookResult> SearchBook(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string authorLastName, string volume, string edition, int? year, string subject, string languageCode, 
            int? collectionID, int returnCount, string searchSort)
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
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount),
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.NVarChar, 50, false, searchSort)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<SearchAnnotationResult> SearchAnnotation(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string annotationText, string title, string authorLastName, string volume, string edition, int? year, int? collectionID, 
            int? annotationSourceID, int returnCount)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchAnnotation", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AnnotationText", SqlDbType.NVarChar, 200, false, annotationText),
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
                CustomSqlHelper.CreateInputParameter("AuthorLastName", SqlDbType.NVarChar, 255, false, authorLastName),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
                CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 450, false, edition),
                CustomSqlHelper.CreateInputParameter("Year", SqlDbType.SmallInt, null, true, year),
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, true, collectionID),
                CustomSqlHelper.CreateInputParameter("AnnotationSourceID", SqlDbType.Int, null, true, annotationSourceID),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount)))
            {
                using (CustomSqlHelper<SearchAnnotationResult> helper = new CustomSqlHelper<SearchAnnotationResult>())
                {
                    List<SearchAnnotationResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select all values from Title like a particular string.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of SearchBookResults.</returns>
        public Tuple<int, List<SearchBookResult>> TitleSelectByNameLike(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string name, int pageNum, int numPages, string sort)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByNameLike", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("Name", SqlDbType.VarChar, 1000, false, name),
                            CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
                            CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numPages),
                            CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sort),
                            CustomSqlHelper.CreateOutputParameter("TotalTitles", SqlDbType.Int, null, false)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    // Get the page of titles
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    // Get the total number of titles
                    int totalTitles = (int)command.Parameters[4].Value;

                    return new Tuple<int, List<SearchBookResult>>(totalTitles, list);

                }
            }
        }

        /// <summary>
        /// Select all values from Title NOT like a particular string.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of SearchBookResults.</returns>
        public List<SearchBookResult> TitleSelectByNameNotLike(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        string name)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByNameNotLike", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("Name", SqlDbType.VarChar, 1000, false, name)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select all values from Title for a particular Author.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of SearchBookResults.</returns>
        public List<SearchBookResult> TitleSelectByAuthor(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        int authorId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByAuthor", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("AuthorId", SqlDbType.Int, null, false, authorId)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public Tuple<int, List<SearchBookResult>> TitleSelectByAuthorPaged(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
                        int authorId, int pageNum, int numPages, string sort)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.TitleSelectByAuthorPaged", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("AuthorId", SqlDbType.Int, null, false, authorId),
                            CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
                            CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numPages),
                            CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sort),
                            CustomSqlHelper.CreateOutputParameter("TotalTitles", SqlDbType.Int, null, false)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    // Get the page of titles for the author
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    // Get the total number of titles for the author from the output parameter
                    int totalTitles = (int)command.Parameters[4].Value;

                    return new Tuple<int, List<SearchBookResult>>(totalTitles, list);
                }
            }
        }

        public Tuple<int, List<SearchBookResult>> TitleSelectByInstitutionAndStartsWith(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
                        string institutionCode, string startsWith, int pageNum, int numPages, string sort)        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByInstitutionAndStartsWith", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                    CustomSqlHelper.CreateInputParameter("StartsWith", SqlDbType.NVarChar, 1000, false, startsWith),
                    CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
                    CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numPages),
                    CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sort),
                    CustomSqlHelper.CreateOutputParameter("TotalTitles", SqlDbType.Int, null, false)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    // Get the page of titles
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    // Get the total number of titles for the author from the output parameter
                    int totalTitles = (int)command.Parameters[5].Value;

                    return new Tuple<int, List<SearchBookResult>>(totalTitles, list);
                }
            }
        }

        public Tuple<int, List<SearchBookResult>> TitleSelectByInstitutionAndStartsWithout(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
                        string institutionCode, string startsWith, int pageNum, int numPages, string sort)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByInstitutionAndStartsWithout", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                    CustomSqlHelper.CreateInputParameter("StartsWith", SqlDbType.NVarChar, 1000, false, startsWith),
                    CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
                    CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numPages),
                    CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sort),
                    CustomSqlHelper.CreateOutputParameter("TotalTitles", SqlDbType.Int, null, false)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    // Get the page of titles
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    // Get the total number of titles for the author from the output parameter
                    int totalTitles = (int)command.Parameters[5].Value;

                    return new Tuple<int, List<SearchBookResult>>(totalTitles, list);
                }
            }
        }

        /// <summary>
        /// Select all values from Title that are related to the specified language.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="languageCode">ID of the language for which to retrieve titles</param>
        /// <returns>List of objects of type SearchBookResult.</returns>
        public List<SearchBookResult> TitleSelectByLanguage(
                        SqlConnection sqlConnection,
                        SqlTransaction sqlTransaction,
                        String languageCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByLanguage", connection, transaction,
                     CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<SearchBookResult> TitleSelectByKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string keyword)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByKeyword", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, keyword)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public Tuple<int, List<SearchBookResult>> TitleSelectByKeywordPaged(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string keyword, int pageNum, int numPages, string sort)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.TitleSelectByKeywordPaged", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, keyword),
                            CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
                            CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numPages),
                            CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sort),
                            CustomSqlHelper.CreateOutputParameter("TotalTitles", SqlDbType.Int, null, false)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    // Get the page of titles for the keyword
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    // Get the total number of titles for the keyword from the output parameter
                    int totalTitles = (int)command.Parameters[4].Value;

                    return new Tuple<int, List<SearchBookResult>>(totalTitles, list);
                }
            }
        }

        /// <summary>
        /// Select all values from Title published between the specified dates.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of SearchBookResults.</returns>
        public Tuple<int, List<SearchBookResult>> TitleSelectByDateRange(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
                        int startYear, int endYear, int pageNum, int numPages, string sort)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByDateRange", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.Int, null, false, startYear),
                            CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.Int, null, false, endYear),
                            CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
                            CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numPages),
                            CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sort),
                            CustomSqlHelper.CreateOutputParameter("TotalTitles", SqlDbType.Int, null, false)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    // Get the page of titles
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    // Get the total number of titles
                    int totalTitles = (int)command.Parameters[5].Value;

                    return new Tuple<int, List<SearchBookResult>>(totalTitles, list);
                }
            }
        }

        /// <summary>
        /// Select all values from Title associated with the specified collection
        /// and starting with the specified letter
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="collectionID"></param>
        /// <param name="startString"></param>
        /// <returns>List of SearchBookResults</returns>
        public Tuple<int, List<SearchBookResult>> TitleSelectByCollectionAndStartsWith(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int collectionID, string startString, int pageNum, int numPages, string sort)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByCollectionAndStartsWith",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID),
                CustomSqlHelper.CreateInputParameter("StartsWith", SqlDbType.VarChar, 1000, false, startString),
                CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
                CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numPages),
                CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sort),
                CustomSqlHelper.CreateOutputParameter("TotalTitles", SqlDbType.Int, null, false)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    // Get the page of titles
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    // Get the total number of titles
                    int totalTitles = (int)command.Parameters[5].Value;

                    return new Tuple<int, List<SearchBookResult>>(totalTitles, list);
                }
            }
        }

        /// <summary>
        /// Select all values from Title associated with the specified collection
        /// and not starting with the specified letter
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="collectionID"></param>
        /// <param name="startString"></param>
        /// <returns>List of SearchBookResult</returns>
        public List<SearchBookResult> TitleSelectByCollectionAndStartsWithout(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int collectionID,
            string startString)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectByCollectionAndStartsWithout",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID),
                CustomSqlHelper.CreateInputParameter("StartsWith", SqlDbType.VarChar, 1000, false, startString)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select all values from Item associated with the specified collectn
        /// and starting with the specified letter
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="collectionID"></param>
        /// <param name="startString"></param>
        /// <returns>List of SearchBookResult</returns>
        public Tuple<int, List<SearchBookResult>> ItemSelectByCollectionAndStartsWith(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int collectionID, string startString, int pageNum, int numPages, string sort)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectByCollectionAndStartsWith",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID),
                CustomSqlHelper.CreateInputParameter("StartsWith", SqlDbType.VarChar, 255, false, startString),
                CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
                CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numPages),
                CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sort),
                CustomSqlHelper.CreateOutputParameter("TotalItems", SqlDbType.Int, null, false)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    // Get the page of items
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    // Get the total number of items
                    int totalItems = (int)command.Parameters[5].Value;

                    return new Tuple<int, List<SearchBookResult>>(totalItems, list);
                }
            }
        }

        /// <summary>
        /// Search all values from Item associated with the specified collection
        /// and not starting with the specified letter
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="collectionID"></param>
        /// <param name="startString"></param>
        /// <returns>List of SesarchBookresult</returns>
        public List<SearchBookResult> ItemSelectByCollectionAndStartsWithout(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int collectionID,
            string startString)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectByCollectionAndStartsWithout",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID),
                CustomSqlHelper.CreateInputParameter("StartsWith", SqlDbType.VarChar, 255, false, startString)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Full-text search for the specified keyword.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="tag"></param>
        /// <param name="languageCode"></param>
        /// <param name="returnCount"></param>
        /// <returns></returns>
        public List<TitleKeyword> SearchTitleKeyword(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string keyword,
            string languageCode,
            int returnCount)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchTitleKeyword", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 300, false, keyword),
                    CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
                    CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount)))
            {
                using (CustomSqlHelper<TitleKeyword> helper = new CustomSqlHelper<TitleKeyword>())
                {
                    List<TitleKeyword> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Full-text search for the specified AuthorName.  Only returns active authors associated with at least one book.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="creatorName"></param>
        /// <param name="returnCount"></param>
        /// <returns></returns>
        public List<Author> SearchAuthor(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string authorName,
            int returnCount)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchAuthor", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AuthorName", SqlDbType.NVarChar, 300, false, authorName),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    List<Author> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Full-text search for the specified AuthorName.  Returns all authors.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="creatorName"></param>
        /// <returns></returns>
        public List<Author> SearchAuthorComplete(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string authorName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchAuthorComplete", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AuthorName", SqlDbType.NVarChar, 300, false, authorName)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    List<Author> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Full-text search for the specified AuthorName.  Returns all authors.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="creatorName"></param>
        /// <returns></returns>
        public List<Segment> SearchSegmentComplete(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string title)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchSegmentComplete", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    List<Segment> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Segment> SearchSegment(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string title,
            string containerTitle,
            string authorLastName,
            string date,
            string volume,
            string series,
            string issue,
            int returnCount,
            string searchSort)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchSegment", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
                CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
                CustomSqlHelper.CreateInputParameter("Author", SqlDbType.NVarChar, 2000, false, authorLastName),
                CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
                CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
                CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount),
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.NVarChar, 50, false, searchSort),
                CustomSqlHelper.CreateInputParameter("IncludeNoContent", SqlDbType.SmallInt, null, false, 0)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    List<Segment> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Segment> SearchSegmentFullText(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string searchText,
            int returnCount,
            string searchSort)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchSegmentFT", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SearchText", SqlDbType.NVarChar, 2000, false, searchText),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount),
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.NVarChar, 50, false, searchSort)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    List<Segment> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Segment> SearchSegmentAdvancedFullText(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string title,
            string containerTitle,
            string authorLastName,
            string date,
            string volume,
            string series,
            string issue,
            int returnCount,
            string searchSort)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchSegmentAdvancedFT", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
                CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
                CustomSqlHelper.CreateInputParameter("Author", SqlDbType.NVarChar, 2000, false, authorLastName),
                CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
                CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
                CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
                CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount),
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.NVarChar, 50, false, searchSort),
                CustomSqlHelper.CreateInputParameter("IncludeNoContent", SqlDbType.SmallInt, null, true, 0)))
            {
                using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
                {
                    List<Segment> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Call the database procedure that implements the multiple-field full-text book search.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="maxIdentifiers"></param>
        /// <param name="startId"></param>
        /// <param name="fromDate"></param>
        /// <param name="untilDate"></param>
        /// <returns></returns>
        public List<SearchBookResult> SearchBookFullText(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string authorLastName, string volume, string edition, int? year, string subject, string languageCode, 
            int? collectionID, int returnCount, string searchSort)
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
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount),
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.NVarChar, 50, false, searchSort)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Call the database procedure that implements the single-field full-text book search.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="maxIdentifiers"></param>
        /// <param name="startId"></param>
        /// <param name="fromDate"></param>
        /// <param name="untilDate"></param>
        /// <returns></returns>
        public List<SearchBookResult> SearchBookGlobalFullText(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, string searchText, int returnCount, string searchSort)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchBookGlobalFT", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SearchText", SqlDbType.NVarChar, 2000, false, searchText),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount),
                CustomSqlHelper.CreateInputParameter("SortBy", SqlDbType.NVarChar, 50, false, searchSort)))
            {
                using (CustomSqlHelper<SearchBookResult> helper = new CustomSqlHelper<SearchBookResult>())
                {
                    List<SearchBookResult> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}

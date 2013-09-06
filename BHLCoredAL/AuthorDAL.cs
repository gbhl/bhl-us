using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class AuthorDAL
	{
        public Author AuthorSelectExtended(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int authorId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            Author author = new AuthorDAL().AuthorSelectAuto(connection, transaction, authorId);

            if (author != null)
            {
                author.AuthorNames = new AuthorNameDAL().AuthorNameSelectByAuthorID(connection, transaction, authorId);
                author.AuthorIdentifiers = new AuthorIdentifierDAL().AuthorIdentifierSelectByAuthorID(connection, transaction, authorId);
            }

            return author;
        }

        /// <summary>
        /// Select all Authors associated with title contributed by the specified institution.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>Object of type Author.</returns>
        public CustomGenericList<Author> AuthorSelectByInstitution(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string institutionCode,
            string languageCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorSelectByInstitution", connection, transaction,
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    CustomGenericList<Author> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Select all Authors starting with a certain letter.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>Object of type Author.</returns>
        public CustomGenericList<Author> AuthorSelectByNameLike(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string fullName,
            string languageCode,
            int returnCount)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorSelectByNameLike", connection, transaction,
                CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 255, false, fullName),
                CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    CustomGenericList<Author> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Select all Authors that start with a certain letter and are associated with
        /// a title contributed by the specified institution.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>Object of type Author.</returns>
        public CustomGenericList<Author> AuthorSelectByNameLikeAndInstitution(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string fullName,
            string institutionCode,
            string languageCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorSelectByNameLikeAndInstitution", connection, transaction,
                CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 255, false, fullName),
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    CustomGenericList<Author> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Author> AuthorSelectByTitleId(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
          int titleId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorSelectByTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleId)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    CustomGenericList<Author> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public Author AuthorSelectWithNameByAuthorId(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorSelectWithNameByAuthorId", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorId)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    CustomGenericList<Author> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        /// <summary>
        /// Returns a list of authors that have suspected character encoding problems.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="institutionCode">Institution for which to return authors</param>
        /// <param name="maxAge">Age in days of authors to consider (i.e. authors new in the last 30 days)</param>
        /// <returns></returns>
        public CustomGenericList<AuthorSuspectCharacter> AuthorSelectWithSuspectCharacters(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                String institutionCode,
                int maxAge)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorSelectWithSuspectCharacters", connection, transaction,
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                CustomSqlHelper.CreateInputParameter("MaxAge", SqlDbType.Int, null, false, maxAge)))
            {
                using (CustomSqlHelper<AuthorSuspectCharacter> helper = new CustomSqlHelper<AuthorSuspectCharacter>())
                {
                    CustomGenericList<AuthorSuspectCharacter> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public int Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Author author, int userId)
        {
            int authorID = author.AuthorID;
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection =
                  CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator(transaction);

            try
            {
                transaction = CustomSqlHelper.BeginTransaction(connection, transaction, isTransactionCoordinator);

                CustomDataAccessStatus<Author> updatedAuthor =
                    new AuthorDAL().AuthorManageAuto(connection, transaction, author, userId);

                authorID = updatedAuthor.ReturnObject.AuthorID;

                if (author.AuthorNames.Count > 0)
                {
                    AuthorNameDAL authorNameDAL = new AuthorNameDAL();
                    foreach (AuthorName authorName in author.AuthorNames)
                    {
                        if (authorName.AuthorID == 0) authorName.AuthorID = updatedAuthor.ReturnObject.AuthorID;
                        authorNameDAL.AuthorNameManageAuto(connection, transaction, authorName, userId);
                    }
                }

                if (author.AuthorIdentifiers.Count > 0)
                {
                    AuthorIdentifierDAL authorIdentifierDAL = new AuthorIdentifierDAL();
                    foreach (AuthorIdentifier authorIdentifier in author.AuthorIdentifiers)
                    {
                        if (authorIdentifier.AuthorID == 0) authorIdentifier.AuthorID = updatedAuthor.ReturnObject.AuthorID;
                        authorIdentifierDAL.AuthorIdentifierManageAuto(connection, transaction, authorIdentifier, userId);
                    }
                }

                // If the author record being updated has been redirected to another author record, 'move' all of the 
                // associated TitleAuthor and SegmentAuthor records to the author being redirected to
                if (author.IsActive == 0 && author.RedirectAuthorID != null)
                {
                    new TitleAuthorDAL().TitleAuthorUpdateAuthorID(sqlConnection, sqlTransaction, 
                        author.AuthorID, (int)author.RedirectAuthorID, userId);

                    new SegmentAuthorDAL().SegmentAuthorUpdateAuthorID(sqlConnection, sqlTransaction,
                        author.AuthorID, (int)author.RedirectAuthorID, userId);
                }

                CustomSqlHelper.CommitTransaction(transaction, isTransactionCoordinator);
            }
            catch (Exception ex)
            {
                CustomSqlHelper.RollbackTransaction(transaction, isTransactionCoordinator);

                throw;
            }
            finally
            {
                CustomSqlHelper.CloseConnection(connection, isTransactionCoordinator);
            }

            return authorID;
        }
    }
}

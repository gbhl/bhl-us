using System;
using System.Collections.Generic;
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
        /// Select all Authors starting with a certain letter.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>Object of type Author.</returns>
        public List<Author> AuthorSelectByNameLike(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string fullName,
            int returnCount)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorSelectByNameLike", connection, transaction,
                CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 255, false, fullName),
                CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    List<Author> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Author> AuthorSelectByTitleId(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
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
                    List<Author> list = helper.ExecuteReader(command);
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
                    List<Author> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public List<Author> AuthorSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int identifierID, string identifierValue)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorSelectByIdentifier", connection, transaction,
                CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
                CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    List<Author> list = helper.ExecuteReader(command);
                    return list;
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
        public List<AuthorSuspectCharacter> AuthorSelectWithSuspectCharacters(
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
                    List<AuthorSuspectCharacter> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select all authors that match the specified criteria.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="fullName"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Object of type Author.</returns>
        public List<Author> AuthorResolve(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string fullName, string lastName, string firstName, string startDate, string endDate, int? authorID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.AuthorResolve", connection, transaction,
                CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 300, false, fullName),
                CustomSqlHelper.CreateInputParameter("LastName", SqlDbType.NVarChar, 150, false, lastName),
                CustomSqlHelper.CreateInputParameter("FirstName", SqlDbType.NVarChar, 150, false, firstName),
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.NVarChar, 25, false, startDate),
                CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.NVarChar, 25, false, endDate),
                CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID)))
            {
                using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
                {
                    List<Author> list = helper.ExecuteReader(command);
                    return list;
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
                // associated TitleAuthor, ItemAuthor, and AuthorIdentifier records to the author being redirected to.  
                // Also, copy all AuthorNames to the author being redirected to.  For AuthorIdentifier and AuthorName
                // records, only move/copy them if they do not already exist on the target author.
                if (author.IsActive == 0 && author.RedirectAuthorID != null)
                {
                    new TitleAuthorDAL().TitleAuthorUpdateAuthorID(sqlConnection, sqlTransaction, 
                        author.AuthorID, (int)author.RedirectAuthorID, userId);

                    new ItemAuthorDAL().ItemAuthorUpdateAuthorID(sqlConnection, sqlTransaction,
                        author.AuthorID, (int)author.RedirectAuthorID, userId);

                    new AuthorIdentifierDAL().AuthorIdentifierUpdateAuthorID(sqlConnection, sqlTransaction,
                        author.AuthorID, (int)author.RedirectAuthorID, userId);

                    new AuthorNameDAL().AuthorNameInsertFromAuthorID(sqlConnection, sqlTransaction,
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

using System;
using System.Collections.Generic;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public Author AuthorSelectExtended(int authorId)
        {
            return new AuthorDAL().AuthorSelectExtended(null, null, authorId);
        }

        /// <summary>
        /// Search for active authors that match the specified criteria.
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="languageCode"></param>
        /// <param name="returnCount"></param>
        /// <returns></returns>
        public CustomGenericList<Author> AuthorSelectByNameLike(string fullName, int returnCount)
        {
            return new AuthorDAL().AuthorSelectByNameLike(null, null, fullName, returnCount);
        }

        public CustomGenericList<Author> AuthorSelectByTitleId(int titleId)
        {
            return new AuthorDAL().AuthorSelectByTitleId(null, null, titleId);
        }

        public Author AuthorSelectWithNameByAuthorId(int authorId)
        {
            return new AuthorDAL().AuthorSelectWithNameByAuthorId(null, null, authorId);
        }

        public CustomGenericList<Author> AuthorSelectByIdentifier(int identifierID, string identifierValue)
        {
            return new AuthorDAL().AuthorSelectByIdentifier(null, null, identifierID, identifierValue);
        }

        public Author AuthorSelectAuto(int authorID)
        {
            return new AuthorDAL().AuthorSelectAuto(null, null, authorID);
        }

        public CustomGenericList<AuthorSuspectCharacter> AuthorSelectWithSuspectCharacters(
            string institutionCode, int maxAge)
        {
            return new AuthorDAL().AuthorSelectWithSuspectCharacters(null, null, institutionCode, maxAge);
        }

        public CustomGenericList<Author> AuthorResolve(string fullName, string lastName, string firstName,
            string startDate, string endDate, int? authorID)
        {
            return new AuthorDAL().AuthorResolve(null, null, fullName, lastName, firstName, startDate, endDate, authorID);
        }

        public int SaveAuthor(Author author, int userId, string userDescription = null)
        {
            // Not a new author
            if (author.AuthorID != 0)
            {
                // Get existing author record
                Author existingAuthor = AuthorSelectAuto(author.AuthorID);

                // Is author being deactivated and redirected?
                if ((author.IsActive == 0 && existingAuthor.IsActive == 1) &&
                    (author.RedirectAuthorID != null) &&
                    (author.RedirectAuthorID != existingAuthor.RedirectAuthorID))
                {
                    author.Note += string.Format("{0}Replaced by {1}. {2} by {3}",
                        Environment.NewLine,
                        author.RedirectAuthorID.ToString(),
                        DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"),
                        string.IsNullOrWhiteSpace(userDescription) ? "unknown" : userDescription);

                    Author targetAuthor = AuthorSelectAuto((int)author.RedirectAuthorID);
                    targetAuthor.AuthorNames = new List<AuthorName>();
                    targetAuthor.AuthorIdentifiers = new List<AuthorIdentifier>();
                    targetAuthor.Note += string.Format("{0}This author replaces {1}. {2} by {3}",
                        Environment.NewLine,
                        author.AuthorID.ToString(),
                        DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"),
                        string.IsNullOrWhiteSpace(userDescription) ? "unknown" : userDescription);

                    new AuthorDAL().Save(null, null, targetAuthor, userId);
                }
            }

            return new AuthorDAL().Save(null, null, author, userId);
        }
    }
}

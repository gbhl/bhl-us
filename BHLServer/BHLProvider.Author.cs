using System;
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

        public CustomGenericList<Author> AuthorSelectByInstitution(string institutionCode, string languageCode)
        {
            return new AuthorDAL().AuthorSelectByInstitution(null, null, institutionCode, languageCode);
        }

        /// <summary>
        /// Search for active authors that match the specified criteria.
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="languageCode"></param>
        /// <param name="returnCount"></param>
        /// <returns></returns>
        public CustomGenericList<Author> AuthorSelectByNameLike(string fullName, string languageCode, int returnCount)
        {
            return new AuthorDAL().AuthorSelectByNameLike(null, null, fullName, languageCode, returnCount);
        }

        public CustomGenericList<Author> AuthorSelectByNameLikeAndInstitution(string fullName,
            string institutionCode, string languageCode)
        {
            return new AuthorDAL().AuthorSelectByNameLikeAndInstitution(null, null, fullName, institutionCode, languageCode);
        }

        public CustomGenericList<Author> AuthorSelectByTitleId(int titleId)
        {
            return new AuthorDAL().AuthorSelectByTitleId(null, null, titleId);
        }

        public Author AuthorSelectWithNameByAuthorId(int authorId)
        {
            return new AuthorDAL().AuthorSelectWithNameByAuthorId(null, null, authorId);
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

        public int SaveAuthor(Author author, int userId)
        {
            return new AuthorDAL().Save(null, null, author, userId);
        }
    }
}

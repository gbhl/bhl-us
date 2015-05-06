using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<SearchBookResult> SearchBook(string title, string authorLastName, string volume, string edition,
            int? year, string subject, string languageCode, int? collectionID, int returnCount, string searchSort)
        {
            return new SearchDAL().SearchBook(null, null, title, authorLastName, volume, edition, year, subject,
                languageCode, collectionID, returnCount, searchSort);
        }

        public CustomGenericList<SearchAnnotationResult> SearchAnnotation(string annotationText, string title, string authorLastName,
            string volume, string edition, int? year, int? collectionID, int? annotationSourceID, int returnCount)
        {
            return new SearchDAL().SearchAnnotation(null, null, annotationText, title, authorLastName, volume, edition,
                year, collectionID, annotationSourceID, returnCount);
        }

        /// <summary>
        /// Select all values from Title like a particular string.
        /// </summary>
        /// <returns>List of SearchBookResults.</returns>
        public CustomGenericList<SearchBookResult> TitleSelectByNameLike(string name, string institutionCode, string languageCode)
        {
            return (new SearchDAL().TitleSelectByNameLike(null, null, name, institutionCode, languageCode));
        }

        /// <summary>
        /// Select all values from Title NOT like a particular string.
        /// </summary>
        /// <returns>List of SearchBookResults.</returns>
        public CustomGenericList<SearchBookResult> TitleSelectByNameNotLike(string name)
        {
            return (new SearchDAL().TitleSelectByNameNotLike(null, null, name));
        }

        /// <summary>
        /// Select Titles for a particular AuthorID.
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns>List of SearchBookResults.</returns>
        public CustomGenericList<SearchBookResult> TitleSelectByAuthor(int authorId)
        {
            return (new SearchDAL().TitleSelectByAuthor(null, null, authorId));
        }

        /// <summary>
        /// Select Titles for a particular InstitutionCode.
        /// </summary>
        /// <param name="institutionCode"></param>
        /// <returns>List of SearchBookResults.</returns>
        public CustomGenericList<SearchBookResult> TitleSelectByInstitution(string institutionCode)
        {
            return (new SearchDAL().TitleSelectByInstitution(null, null, institutionCode));
        }

        public CustomGenericList<SearchBookResult> TitleSelectByInstitutionAndStartsWith(string institutionCode, string startsWith)
        {
            return new SearchDAL().TitleSelectByInstitutionAndStartsWith(null, null, institutionCode, startsWith);
        }

        public CustomGenericList<SearchBookResult> TitleSelectByInstitutionAndStartsWithout(string institutionCode, string startsWith)
        {
            return new SearchDAL().TitleSelectByInstitutionAndStartsWithout(null, null, institutionCode, startsWith);
        }

        public CustomGenericList<SearchBookResult> TitleSelectByKeywordInstitutionAndLanguage(string keyword, string institutionCode, string languageCode)
        {
            return new SearchDAL().TitleSelectByKeywordInstitutionAndLanguage(null, null, keyword, institutionCode, languageCode);
        }

        /// <summary>
        /// Select Titles for a particular LanguageCode.
        /// </summary>
        /// <param name="languageCode"></param>
        /// <returns>List of SearchBookResults.</returns>
        public CustomGenericList<SearchBookResult> TitleSelectByLanguage(string languageCode)
        {
            return (new SearchDAL().TitleSelectByLanguage(null, null, languageCode));
        }

        /// <summary>
        /// Select Titles for a particular date range
        /// </summary>
        /// <returns>List of SearchBookResults.</returns>
        public CustomGenericList<SearchBookResult> TitleSelectByDateRangeAndInstitution(int startDate, int endDate, String institutionCode, String languageCode)
        {
            return (new SearchDAL().TitleSelectByDateRangeAndInstitution(null, null, startDate, endDate, institutionCode, languageCode));
        }

        /// <summary>
        /// Select Titles for a particular collection
        /// </summary>
        /// <param name="collectionID"></param>
        /// <param name="startString"></param>
        /// <returns>List of SearchBookResults</returns>
        public CustomGenericList<SearchBookResult> TitleSelectByCollectionAndStartsWith(int collectionID, string startString)
        {
            return new SearchDAL().TitleSelectByCollectionAndStartsWith(null, null, collectionID, startString);
        }

        /// <summary>
        /// Select Items for a particular collection
        /// </summary>
        /// <param name="collectionID"></param>
        /// <param name="startsWith"></param>
        /// <returns>List of SearchBookResult</returns>
        public CustomGenericList<SearchBookResult> ItemSelectByCollectionAndStartsWith(int collectionID, string startsWith)
        {
            return new SearchDAL().ItemSelectByCollectionAndStartsWith(null, null, collectionID, startsWith);
        }

        public CustomGenericList<TitleKeyword> SearchTitleKeyword(string keyword, string languageCode, int returnCount)
		{
            return new SearchDAL().SearchTitleKeyword(null, null, keyword, languageCode, returnCount);
		}

        /// <summary>
        /// Search for active authors that match the specified criteria.
        /// </summary>
        /// <param name="authorName"></param>
        /// <param name="languageCode"></param>
        /// <param name="returnCount"></param>
        /// <returns></returns>
        public CustomGenericList<Author> SearchAuthor(string authorName, string languageCode, int returnCount)
        {
            return new SearchDAL().SearchAuthor(null, null, authorName, languageCode, returnCount);
        }

        /// <summary>
        /// Search all authors (whether active or not).
        /// </summary>
        /// <param name="authorName"></param>
        /// <returns></returns>
        public CustomGenericList<Author> SearchAuthorComplete(string authorName)
        {
            return new SearchDAL().SearchAuthorComplete(null, null, authorName);
        }

        /// <summary>
        /// Search all segments (whether active or not).
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public CustomGenericList<Segment> SearchSegmentComplete(string title)
        {
            return new SearchDAL().SearchSegmentComplete(null, null, title);
        }

        public CustomGenericList<Segment> SearchSegment(string title, string containerTitle,
            string authorLastName, string date, string volume, string series, string issue, int returnCount, string searchSort)
        {
            return new SearchDAL().SearchSegment(null, null, title, containerTitle, authorLastName, date,
                volume, series, issue, returnCount, searchSort);
        }

        public CustomGenericList<Segment> SearchSegmentFullText(string searchText, int returnCount, string searchSort)
        {
            return new SearchDAL().SearchSegmentFullText(null, null, searchText, returnCount, searchSort);
        }

        public CustomGenericList<Segment> SearchSegmentAdvancedFullText(string title, string containerTitle, 
            string authorLastName, string date, string volume, string series, string issue, int returnCount, string searchSort)
        {
            return new SearchDAL().SearchSegmentAdvancedFullText(null, null, title, containerTitle, authorLastName, date, 
                volume, series, issue, returnCount, searchSort);
        }

        public CustomGenericList<SearchBookResult> SearchBookFullText(string title, string authorLastName, string volume, string edition,
            int? year, string subject, string languageCode, int? collectionID, int returnCount, string searchSort)
        {
            return new SearchDAL().SearchBookFullText(null, null, title, authorLastName, volume, edition, year, subject,
                languageCode, collectionID, returnCount, searchSort);
        }

        public CustomGenericList<SearchBookResult> SearchBookFullText(string searchText, int returnCount, string searchSort)
        {
            return new SearchDAL().SearchBookGlobalFullText(null, null, searchText, returnCount, searchSort);
        }

        /// <summary>
        /// Log the OpenUrl request
        /// </summary>
        /// <param name="detail"></param>
        private void LogRequest(string ipAddress, string detail)
        {
            // Log the request.  
            // First argument "3" corresponds to "BHL OpenUrl".  
            // Fourth argument "231" corresponds to "Citation Finder"
            BHL.Web.Utilities.RequestLog requestLog = new BHL.Web.Utilities.RequestLog();
            requestLog.SaveRequestLog(3, ipAddress, null, 231, detail);
        }
    }
}

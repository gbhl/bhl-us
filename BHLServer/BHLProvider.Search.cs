using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<SearchBookResult> SearchBook(string title, string authorLastName, string volume, string edition,
            int? year, string subject, string languageCode, int? collectionID, int returnCount, string searchSort)
        {
            return new SearchDAL().SearchBook(null, null, title, authorLastName, volume, edition, year, subject,
                languageCode, collectionID, returnCount, searchSort);
        }

        public List<SearchAnnotationResult> SearchAnnotation(string annotationText, string title, string authorLastName,
            string volume, string edition, int? year, int? collectionID, int? annotationSourceID, int returnCount)
        {
            return new SearchDAL().SearchAnnotation(null, null, annotationText, title, authorLastName, volume, edition,
                year, collectionID, annotationSourceID, returnCount);
        }

        /// <summary>
        /// Select all values from Title like a particular string.
        /// </summary>
        /// <returns>List of SearchBookResults.</returns>
        public Tuple<int, List<SearchBookResult>> TitleSelectByNameLike(string name, int pageNum, int numPages, string sort)
        {
            return (new SearchDAL().TitleSelectByNameLike(null, null, name, pageNum, numPages, sort));
        }

        /// <summary>
        /// Select all values from Title NOT like a particular string.
        /// </summary>
        /// <returns>List of SearchBookResults.</returns>
        public List<SearchBookResult> TitleSelectByNameNotLike(string name)
        {
            return (new SearchDAL().TitleSelectByNameNotLike(null, null, name));
        }

        /// <summary>
        /// Select Titles for a particular AuthorID.
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns>List of SearchBookResults.</returns>
        public List<SearchBookResult> TitleSelectByAuthor(int authorId)
        {
            return (new SearchDAL().TitleSelectByAuthor(null, null, authorId));
        }

        public Tuple<int, List<SearchBookResult>> TitleSelectByAuthorPaged(int authorId, int pageNum, int numPages, string sort)
        {
            return (new SearchDAL().TitleSelectByAuthorPaged(null, null, authorId, pageNum, numPages, sort));
        }

        public Tuple<int, List<SearchBookResult>> TitleSelectByInstitutionAndStartsWith(string institutionCode, string startsWith, int pageNum, int numPages, string sort)
        {
            return new SearchDAL().TitleSelectByInstitutionAndStartsWith(null, null, institutionCode, startsWith, pageNum, numPages, sort);
        }

        public Tuple<int, List<SearchBookResult>> TitleSelectByInstitutionAndStartsWithout(string institutionCode, string startsWith, int pageNum, int numPages, string sort)
        {
            return new SearchDAL().TitleSelectByInstitutionAndStartsWithout(null, null, institutionCode, startsWith, pageNum, numPages, sort);
        }

        public List<SearchBookResult> TitleSelectByKeyword(string keyword)
        {
            return new SearchDAL().TitleSelectByKeyword(null, null, keyword);
        }

        public Tuple<int, List<SearchBookResult>> TitleSelectByKeywordPaged(string keyword, int pageNum, int numPages, string sort)
        {
            return new SearchDAL().TitleSelectByKeywordPaged(null, null, keyword, pageNum, numPages, sort);
        }

        /// <summary>
        /// Select Titles for a particular LanguageCode.
        /// </summary>
        /// <param name="languageCode"></param>
        /// <returns>List of SearchBookResults.</returns>
        public List<SearchBookResult> TitleSelectByLanguage(string languageCode)
        {
            return (new SearchDAL().TitleSelectByLanguage(null, null, languageCode));
        }

        /// <summary>
        /// Select Titles for a particular date range
        /// </summary>
        /// <returns>List of SearchBookResults.</returns>
        public Tuple<int, List<SearchBookResult>> TitleSelectByDateRange(int startDate, int endDate, int pageNum, int numPages, string sort)
        {
            return (new SearchDAL().TitleSelectByDateRange(null, null, startDate, endDate, pageNum, numPages, sort));
        }

        /// <summary>
        /// Select Titles for a particular collection
        /// </summary>
        /// <param name="collectionID"></param>
        /// <param name="startString"></param>
        /// <returns>List of SearchBookResults</returns>
        public Tuple<int, List<SearchBookResult>> TitleSelectByCollectionAndStartsWith(int collectionID, string startString, int pageNum, int numPages, string sort)
        {
            return new SearchDAL().TitleSelectByCollectionAndStartsWith(null, null, collectionID, startString, pageNum, numPages, sort);
        }

        /// <summary>
        /// Select Titles for a particular collection
        /// </summary>
        /// <param name="collectionID"></param>
        /// <param name="startString"></param>
        /// <returns>List of SearchBookResult</returns>
        public List<SearchBookResult> TitleSelectByCollectionAndStartsWithout(int collectionID, string startString)
        {
            return new SearchDAL().TitleSelectByCollectionAndStartsWithout(null, null, collectionID, startString);
        }

        /// <summary>
        /// Select Items for a particular collection
        /// </summary>
        /// <param name="collectionID"></param>
        /// <param name="startsWith"></param>
        /// <returns>List of SearchBookResult</returns>
        public Tuple<int, List<SearchBookResult>> ItemSelectByCollectionAndStartsWith(int collectionID, string startsWith, int pageNum, int numPages, string sort)
        {
            return new SearchDAL().ItemSelectByCollectionAndStartsWith(null, null, collectionID, startsWith, pageNum, numPages, sort);
        }

        /// <summary>
        /// Select Items for a particular collection
        /// </summary>
        /// <param name="collectionID"></param>
        /// <param name="startsWith"></param>
        /// <returns></returns>
        public List<SearchBookResult> ItemSelectByCollectionAndStartsWithout(int collectionID, string startsWith)
        {
            return new SearchDAL().ItemSelectByCollectionAndStartsWithout(null, null, collectionID, startsWith);
        }

        public List<TitleKeyword> SearchTitleKeyword(string keyword, string languageCode, int returnCount)
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
        public List<Author> SearchAuthor(string authorName, int returnCount)
        {
            return new SearchDAL().SearchAuthor(null, null, authorName, returnCount);
        }

        /// <summary>
        /// Search all authors (whether active or not).
        /// </summary>
        /// <param name="authorName"></param>
        /// <returns></returns>
        public List<Author> SearchAuthorComplete(string authorName)
        {
            return new SearchDAL().SearchAuthorComplete(null, null, authorName);
        }

        /// <summary>
        /// Search all segments (whether active or not).
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<Segment> SearchSegmentComplete(string title)
        {
            return new SearchDAL().SearchSegmentComplete(null, null, title);
        }

        public List<Segment> SearchSegment(string title, string containerTitle,
            string authorLastName, string date, string volume, string series, string issue, int returnCount, string searchSort)
        {
            return new SearchDAL().SearchSegment(null, null, title, containerTitle, authorLastName, date,
                volume, series, issue, returnCount, searchSort);
        }

        public List<Segment> SearchSegmentFullText(string searchText, int returnCount, string searchSort)
        {
            return new SearchDAL().SearchSegmentFullText(null, null, searchText, returnCount, searchSort);
        }

        public List<Segment> SearchSegmentAdvancedFullText(string title, string containerTitle, 
            string authorLastName, string date, string volume, string series, string issue, int returnCount, string searchSort)
        {
            return new SearchDAL().SearchSegmentAdvancedFullText(null, null, title, containerTitle, authorLastName, date, 
                volume, series, issue, returnCount, searchSort);
        }

        public List<SearchBookResult> SearchBookFullText(string title, string authorLastName, string volume, string edition,
            int? year, string subject, string languageCode, int? collectionID, int returnCount, string searchSort)
        {
            return new SearchDAL().SearchBookFullText(null, null, title, authorLastName, volume, edition, year, subject,
                languageCode, collectionID, returnCount, searchSort);
        }

        public List<SearchBookResult> SearchBookFullText(string searchText, int returnCount, string searchSort)
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
            MOBOT.BHL.Utility.RequestLog requestLog = new MOBOT.BHL.Utility.RequestLog();
            requestLog.SaveRequestLog(3, ipAddress, null, 231, detail);
        }
    }
}

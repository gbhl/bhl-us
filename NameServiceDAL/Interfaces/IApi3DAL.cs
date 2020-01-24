using MOBOT.BHL.API.BHLApiDataObjects3;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.API.BHLApiDAL
{
    public interface IApi3DAL
    {
        List<Name> NamePageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        Page PageSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        List<PageNumber> IndicatedPageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        List<PageType> PageTypeSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        List<Item> ItemSelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID);
        List<Item> ItemSelectByBarcode(SqlConnection sqlConnection, SqlTransaction sqlTransaction, String barcode);
        List<PageDetail> PageSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID);
        List<Part> SegmentSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID);
        List<Author> AuthorSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<Author> AuthorSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        List<Title> TitleSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int titleID);
        List<Item> ItemSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<TitleVariant> TitleVariantSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<Identifier> TitleIdentifierSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<TitleNote> TitleNoteSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<Subject> SubjectSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<Title> TitleSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string identifierName, string identifierValue);
        List<Title> TitleSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi);
        List<Part> SegmentSelectForSegmentID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int segmentId);
        List<Identifier> SegmentIdentifierSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        List<Subject> SubjectSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        List<Part> SegmentSelectRelated(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        List<Name> NameSegmentSelectBySegmentID(SqlConnection sqlConnection,
        SqlTransaction sqlTransaction, int segmentID);
        List<Part> SegmentSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string identifierName, string identifierValue);
        List<Part> SegmentSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi);
        List<PageDetail> PageSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        List<Collection> CollectionSelectActive(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        ApiKey ApiKeySelectByKey(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Guid keyValue);
        List<Author> AuthorSelectNameStartsWith(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string name);
        List<Author> SearchAuthor(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string name);
        List<Subject> TitleKeywordSelectLikeTag(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        List<Subject> SearchTitleKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string tag);
        List<Name> NameResolvedSelectByNameLike(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string name);
        List<PageDetail> PageSelectByResolvedName(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string nameConfirmed);
        List<PageDetail> PageSelectByNameIdentifier(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string identifierName, string identifierValue);
        List<Title> TitleSelectByAuthor(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorID);
        List<Part> SegmentSelectByAuthor(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorID);
        List<Title> TitleSelectByKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        List<Part> SegmentSelectByKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        List<Collection> CollectionSelectByTitleID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int titleID);
        List<Collection> CollectionSelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int itemID);
        List<SearchBookResult> SearchBook(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string authorLastName, string volume, string edition, int? year, string subject, string languageCode,
            int? collectionID, int returnCount);
        List<SearchBookResult> SearchBookFullText(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string authorLastName, string volume, string edition, int? year, string subject, string languageCode,
            int? collectionID, int returnCount);
        List<Part> SearchSegment(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string containerTitle, string author, string date, string volume, string series, string issue,
            int returnCount, string sortBy);
        List<Part> SearchSegmentFullText(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string containerTitle, string author, string date, string volume, string series, string issue,
            int returnCount, string sortBy);
    }
}

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.API.BHLApiDataObjects3;

namespace MOBOT.BHL.API.BHLApiDAL
{
    public interface IApi3DAL
    {
        CustomGenericList<Name> NamePageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        Page PageSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        CustomGenericList<PageNumber> IndicatedPageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        CustomGenericList<PageType> PageTypeSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        Item ItemSelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID);
        Item ItemSelectByBarcode(SqlConnection sqlConnection, SqlTransaction sqlTransaction, String barcode);
        CustomGenericList<PageDetail> PageSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID);
        CustomGenericList<Part> SegmentSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID);
        CustomGenericList<Author> AuthorSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<Author> AuthorSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        Title TitleSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<Item> ItemSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<TitleVariant> TitleVariantSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<Identifier> TitleIdentifierSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<TitleNote> TitleNoteSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<Subject> SubjectSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<Title> TitleSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string identifierName, string identifierValue);
        CustomGenericList<Title> TitleSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi);
        Part SegmentSelectForSegmentID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int segmentId);
        CustomGenericList<Identifier> SegmentIdentifierSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        CustomGenericList<Subject> SubjectSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        CustomGenericList<Part> SegmentSelectRelated(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        CustomGenericList<Name> NameSegmentSelectBySegmentID(SqlConnection sqlConnection,
        SqlTransaction sqlTransaction, int segmentID);
        CustomGenericList<Part> SegmentSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string identifierName, string identifierValue);
        CustomGenericList<Part> SegmentSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi);
        CustomGenericList<PageDetail> PageSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        CustomGenericList<Collection> CollectionSelectActive(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        ApiKey ApiKeySelectByKey(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Guid keyValue);
        CustomGenericList<Title> TitleSelectSearchSimple(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
        string title);
        CustomGenericList<Title> SearchTitleSimple(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
                string title);
        CustomGenericList<Author> AuthorSelectNameStartsWith(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string name);
        CustomGenericList<Author> SearchAuthor(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string name);
        CustomGenericList<Subject> TitleKeywordSelectLikeTag(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        CustomGenericList<Subject> SearchTitleKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string tag);
        CustomGenericList<Name> NameResolvedSelectByNameLike(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string name);
        CustomGenericList<PageDetail> PageSelectByNameConfirmed(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string nameConfirmed);
        CustomGenericList<PageDetail> PageSelectByNameIdentifier(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string identifierName, string identifierValue);
        CustomGenericList<Title> TitleSelectByAuthor(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorID);
        CustomGenericList<Part> SegmentSelectByAuthor(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorID);
        CustomGenericList<Title> TitleSelectByKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        CustomGenericList<Part> SegmentSelectByKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        CustomGenericList<Collection> CollectionSelectByTitleID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int titleID);
        CustomGenericList<Collection> CollectionSelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int itemID);
        CustomGenericList<SearchBookResult> SearchBook(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string authorLastName, string volume, string edition, int? year, string subject, string languageCode,
            int? collectionID, int returnCount);
        CustomGenericList<SearchBookResult> SearchBookFullText(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string authorLastName, string volume, string edition, int? year, string subject, string languageCode,
            int? collectionID, int returnCount);
        CustomGenericList<Part> SearchSegment(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string containerTitle, string author, string date, string volume, string series, string issue,
            int returnCount, string sortBy);
        CustomGenericList<Part> SearchSegmentFullText(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string title, string containerTitle, string author, string date, string volume, string series, string issue,
            int returnCount, string sortBy);
    }
}

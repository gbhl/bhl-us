
#region using

using MOBOT.BHL.API.BHLApiDataObjects2;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion using

namespace MOBOT.BHL.API.BHLApiDAL
{
    public interface IApi2DAL
	{
        List<Name> NamePageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        Page PageSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        List<PageNumber> IndicatedPageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        List<PageType> PageTypeSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        Item ItemSelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID);
        Item ItemSelectByBarcode(SqlConnection sqlConnection, SqlTransaction sqlTransaction, String barcode);
        List<PageDetail> PageSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID);
        List<Item> ItemSelectUnpublished(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        List<Part> SegmentSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID);
        Title TitleSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int titleID);
        List<Item> ItemSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<Creator> AuthorSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<TitleVariant> TitleVariantSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<TitleIdentifier> TitleIdentifierSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<TitleNote> TitleNoteSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<Subject> SubjectSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        List<Title> TitleSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string identifierName, string identifierValue);
        List<Title> TitleSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi);
        List<Title> TitleSelectSearchSimple(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
                string title);
        List<Title> SearchTitleSimple(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
                string title);
        List<Title> TitleSelectUnpublished(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        Part SegmentSelectForSegmentID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int segmentId);
        List<Name> NameSegmentSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        List<Creator> AuthorSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        List<PartIdentifier> SegmentIdentifierSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        List<Subject> SubjectSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        List<PageDetail> PageSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        List<Part> SegmentSelectRelated(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        List<Part> SegmentSelectUnpublished(SqlConnection sqlConnection, SqlTransaction sqlTransaction );
        List<Part> SegmentSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string identifierName, string identifierValue);
        List<Part> SegmentSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi);
        List<Subject> TitleKeywordSelectLikeTag(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        List<Subject> SearchTitleKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string tag);
        List<Title> TitleSelectByKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        List<Part> SegmentSelectByKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        List<Creator> AuthorSelectNameStartsWith(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string name);
        List<Creator> SearchAuthor(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string name);
        List<Title> TitleSelectByAuthor(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorID);
        List<Part> SegmentSelectByAuthor( SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorID);
        int NameResolvedCountUnique(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        int NameResolvedCountUniqueBetweenDates(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            DateTime startDate, DateTime endDate);
        List<Name> NameResolvedListActive(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int startRow, int batchSize);
        List<Name> NameResolvedListActiveBetweenDates(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int startRow, int batchSize, DateTime startDate, DateTime endDate);
        List<Name> NameResolvedSelectByNameLike(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string name);
        List<PageDetail> PageSelectByNameBankID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string nameBankID);
        List<PageDetail> PageSelectByNameConfirmed(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string nameConfirmed);
        List<Language> LanguageSelectWithPublishedItems(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        List<Collection> CollectionSelectActive(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
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
        ApiKey ApiKeySelectByKey(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Guid keyValue);

    }
}


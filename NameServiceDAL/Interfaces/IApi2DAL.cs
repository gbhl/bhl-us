
#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.API.BHLApiDataObjects2;

#endregion using

namespace MOBOT.BHL.API.BHLApiDAL
{
	public interface IApi2DAL
	{
        CustomGenericList<Name> NamePageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        Page PageSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        CustomGenericList<PageNumber> IndicatedPageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        CustomGenericList<PageType> PageTypeSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID);
        Item ItemSelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID);
        Item ItemSelectByBarcode(SqlConnection sqlConnection, SqlTransaction sqlTransaction, String barcode);
        CustomGenericList<PageDetail> PageSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID);
        CustomGenericList<Item> ItemSelectUnpublished(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        CustomGenericList<Part> SegmentSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID);
        Title TitleSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<Item> ItemSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<Creator> AuthorSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<TitleVariant> TitleVariantSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<TitleIdentifier> TitleIdentifierSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<TitleNote> TitleNoteSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<Subject> SubjectSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID);
        CustomGenericList<Title> TitleSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string identifierName, string identifierValue);
        CustomGenericList<Title> TitleSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi);
        CustomGenericList<Title> TitleSelectSearchSimple(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
                string title);
        CustomGenericList<Title> SearchTitleSimple(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
                string title);
        CustomGenericList<Title> TitleSelectUnpublished(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        Part SegmentSelectForSegmentID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int segmentId);
        CustomGenericList<Name> NameSegmentSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        CustomGenericList<Creator> AuthorSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        CustomGenericList<PartIdentifier> SegmentIdentifierSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        CustomGenericList<Subject> SubjectSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        CustomGenericList<PageDetail> PageSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        CustomGenericList<Part> SegmentSelectRelated(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID);
        CustomGenericList<Part> SegmentSelectUnpublished(SqlConnection sqlConnection, SqlTransaction sqlTransaction );
        CustomGenericList<Part> SegmentSelectByIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string identifierName, string identifierValue);
        CustomGenericList<Part> SegmentSelectByDOI(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string doi);
        CustomGenericList<Subject> TitleKeywordSelectLikeTag(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        CustomGenericList<Subject> SearchTitleKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string tag);
        CustomGenericList<Title> TitleSelectByKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        CustomGenericList<Part> SegmentSelectByKeyword(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string subject);
        CustomGenericList<Creator> AuthorSelectNameStartsWith(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string name);
        CustomGenericList<Creator> SearchAuthor(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string name);
        CustomGenericList<Title> TitleSelectByAuthor(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorID);
        CustomGenericList<Part> SegmentSelectByAuthor( SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorID);
        int NameResolvedCountUnique(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        int NameResolvedCountUniqueBetweenDates(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            DateTime startDate, DateTime endDate);
        CustomGenericList<Name> NameResolvedListActive(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int startRow, int batchSize);
        CustomGenericList<Name> NameResolvedListActiveBetweenDates(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int startRow, int batchSize, DateTime startDate, DateTime endDate);
        CustomGenericList<Name> NameResolvedSelectByNameLike(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string name);
        CustomGenericList<PageDetail> PageSelectByNameBankID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string nameBankID);
        CustomGenericList<PageDetail> PageSelectByNameConfirmed(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string nameConfirmed);
        CustomGenericList<Language> LanguageSelectWithPublishedItems(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        CustomGenericList<Collection> CollectionSelectActive(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
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
        ApiKey ApiKeySelectByKey(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Guid keyValue);

    }
}


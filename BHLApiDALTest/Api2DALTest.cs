using MOBOT.BHL.API.BHLApiDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.API.BHLApiDataObjects2;
using CustomDataAccess;

namespace BHLApiDALTest
{
    /// <summary>
    ///This is a test class for Api2DALTest and is intended
    ///to contain all Api2DALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Api2DALTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ApiKeySelectByKey
        ///</summary>
        [TestMethod()]
        public void ApiKeySelectByKeyTest()
        {
            // Make sure a record is retrieved
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            Guid keyValue = new Guid("00000000-0000-0000-0000-000000000000");
            ApiKey actual;
            actual = target.ApiKeySelectByKey(sqlConnection, sqlTransaction, keyValue);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for AuthorSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void AuthorSelectByTitleIDTest()
        {
            // Make sure a record is retrieved
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 1000;
            CustomGenericList<Creator> actual;
            actual = target.AuthorSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for AuthorSelectNameStartsWith
        ///</summary>
        [TestMethod()]
        public void AuthorSelectNameStartsWithTest()
        {
            // Make sure that records are returned
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "Q";
            CustomGenericList<Creator> actual;
            actual = target.AuthorSelectNameStartsWith(sqlConnection, sqlTransaction, name);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for CollectionSelectActive
        ///</summary>
        [TestMethod()]
        public void CollectionSelectActiveTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            CustomGenericList<Collection> actual;
            actual = target.CollectionSelectActive(sqlConnection, sqlTransaction);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for CollectionSelectByItemID
        ///</summary>
        [TestMethod()]
        public void CollectionSelectByItemIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 200;
            CustomGenericList<Collection> actual;
            actual = target.CollectionSelectByItemID(sqlConnection, sqlTransaction, itemID);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for CollectionSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void CollectionSelectByTitleIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 200;
            CustomGenericList<Collection> actual;
            actual = target.CollectionSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for IndicatedPageSelectByPageID
        ///</summary>
        [TestMethod()]
        public void IndicatedPageSelectByPageIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 100000;
            CustomGenericList<PageNumber> actual;
            actual = target.IndicatedPageSelectByPageID(sqlConnection, sqlTransaction, pageID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for ItemSelectByBarcode
        ///</summary>
        [TestMethod()]
        public void ItemSelectByBarcodeTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string barcode = "mobot31753002307509";
            CustomGenericList<Item> actual;
            actual = target.ItemSelectByBarcode(sqlConnection, sqlTransaction, barcode);
            Assert.IsTrue(actual.Count == 1);
        }

        /// <summary>
        ///A test for ItemSelectByItemID
        ///</summary>
        [TestMethod()]
        public void ItemSelectByItemIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 1000;
            Item actual;
            actual = target.ItemSelectByItemID(sqlConnection, sqlTransaction, itemID);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for ItemSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void ItemSelectByTitleIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 1000;
            CustomGenericList<Item> actual;
            actual = target.ItemSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for ItemSelectUnpublished
        ///</summary>
        [TestMethod()]
        public void ItemSelectUnpublishedTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            CustomGenericList<Item> actual;
            actual = target.ItemSelectUnpublished(sqlConnection, sqlTransaction);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for LanguageSelectWithPublishedItems
        ///</summary>
        [TestMethod()]
        public void LanguageSelectWithPublishedItemsTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            CustomGenericList<Language> actual;
            actual = target.LanguageSelectWithPublishedItems(sqlConnection, sqlTransaction);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for PageNameCountUnique
        ///</summary>
        [TestMethod()]
        public void ResolvedNameCountUniqueTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int actual;
            actual = target.NameResolvedCountUnique(sqlConnection, sqlTransaction);
            Assert.IsTrue(actual > 0);
        }

        /// <summary>
        ///A test for PageNameCountUniqueBetweenDates
        ///</summary>
        [TestMethod()]
        public void ResolvedNameCountUniqueBetweenDatesTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            DateTime startDate = new DateTime(2009, 11, 1);
            DateTime endDate = new DateTime(2009, 12, 1);
            int actual;
            actual = target.NameResolvedCountUniqueBetweenDates(sqlConnection, sqlTransaction, startDate, endDate);
            Assert.IsTrue(actual == 0);
        }

        /// <summary>
        ///A test for PageNameListActive
        ///</summary>
        [TestMethod()]
        public void NameResolvedListActiveTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int startRow = 1;
            int batchSize = 10;
            CustomGenericList<Name> actual;
            actual = target.NameResolvedListActive(sqlConnection, sqlTransaction, startRow, batchSize);
            Assert.IsTrue(actual.Count == 10);
        }

        /// <summary>
        ///A test for PageNameListActiveBetweenDates
        ///</summary>
        [TestMethod()]
        public void NameResolvedListActiveBetweenDatesTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int startRow = 1;
            int batchSize = 10;
            DateTime startDate = new DateTime(2009, 11, 1);
            DateTime endDate = new DateTime(2009, 12, 1);
            CustomGenericList<Name> actual;
            actual = target.NameResolvedListActiveBetweenDates(sqlConnection, sqlTransaction, startRow, batchSize, startDate, endDate);
            Assert.IsTrue(actual.Count == 0);
        }

        /// <summary>
        ///A test for ResolvedNameSelectByNameLike
        ///</summary>
        [TestMethod()]
        public void NameResolvedSelectByNameLikeTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "poa annua";
            CustomGenericList<Name> actual;
            actual = target.NameResolvedSelectByNameLike(sqlConnection, sqlTransaction, name);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for PageNameSelectByPageID
        ///</summary>
        [TestMethod()]
        public void NamePageSelectByPageIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 7000;
            CustomGenericList<Name> actual;
            actual = target.NamePageSelectByPageID(sqlConnection, sqlTransaction, pageID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for PageSelectAuto
        ///</summary>
        [TestMethod()]
        public void PageSelectAutoTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 100000;
            Page actual;
            actual = target.PageSelectAuto(sqlConnection, sqlTransaction, pageID);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for PageSelectByItemID
        ///</summary>
        [TestMethod()]
        public void PageSelectByItemIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 1000;
            CustomGenericList<PageDetail> actual;
            actual = target.PageSelectByItemID(sqlConnection, sqlTransaction, itemID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for PageSelectByNameBankID
        ///</summary>
        [TestMethod()]
        public void PageSelectByNameBankIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string nameBankID = "2661223";   // poa annua
            CustomGenericList<PageDetail> actual;
            actual = target.PageSelectByNameBankID(sqlConnection, sqlTransaction, nameBankID);
            Assert.IsTrue(actual.Count >  0);
        }

        /// <summary>
        ///A test for PageSelectByNameConfirmed
        ///</summary>
        [TestMethod()]
        public void PageSelectByNameConfirmedTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string nameConfirmed = "poa annua";
            CustomGenericList<PageDetail> actual;
            actual = target.PageSelectByNameConfirmed(sqlConnection, sqlTransaction, nameConfirmed);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for PageTypeSelectByPageID
        ///</summary>
        [TestMethod()]
        public void PageTypeSelectByPageIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 100000;
            CustomGenericList<PageType> actual;
            actual = target.PageTypeSelectByPageID(sqlConnection, sqlTransaction, pageID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchAuthor
        ///</summary>
        [TestMethod()]
        public void SearchAuthorTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "darwin";
            CustomGenericList<Creator> actual;
            actual = target.SearchAuthor(sqlConnection, sqlTransaction, name);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchBook
        ///</summary>
        [TestMethod()]
        public void SearchBookTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "annals of the missouri";
            string authorLastName = string.Empty;
            string volume = string.Empty;
            string edition = string.Empty;
            Nullable<int> year = new Nullable<int>();
            string subject = string.Empty;
            string languageCode = string.Empty;
            Nullable<int> collectionID = new Nullable<int>();
            int returnCount = 10;
            CustomGenericList<SearchBookResult> actual;
            actual = target.SearchBook(sqlConnection, sqlTransaction, title, authorLastName, volume, edition, year, subject, languageCode, collectionID, returnCount);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchBookFullText
        ///</summary>
        [TestMethod()]
        public void SearchBookFullTextTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "annals missouri";
            string authorLastName = string.Empty;
            string volume = string.Empty;
            string edition = string.Empty;
            Nullable<int> year = new Nullable<int>();
            string subject = string.Empty;
            string languageCode = string.Empty;
            Nullable<int> collectionID = new Nullable<int>();
            int returnCount = 10;
            CustomGenericList<SearchBookResult> actual;
            actual = target.SearchBookFullText(sqlConnection, sqlTransaction, title, authorLastName, volume, edition, year, subject, languageCode, collectionID, returnCount);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchTitleKeyword
        ///</summary>
        [TestMethod()]
        public void SearchTitleKeywordTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string tag = "birds";
            CustomGenericList<Subject> actual;
            actual = target.SearchTitleKeyword(sqlConnection, sqlTransaction, tag);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchTitleSimple
        ///</summary>
        [TestMethod()]
        public void SearchTitleSimpleTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "annals of the missouri";
            CustomGenericList<Title> actual;
            actual = target.SearchTitleSimple(sqlConnection, sqlTransaction, title);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SubjectSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void SubjectSelectByTitleIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 1000;
            CustomGenericList<Subject> actual;
            actual = target.SubjectSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleIdentifierSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void TitleIdentifierSelectByTitleIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 1000;
            CustomGenericList<TitleIdentifier> actual;
            actual = target.TitleIdentifierSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleKeywordSelectLikeTag
        ///</summary>
        [TestMethod()]
        public void TitleKeywordSelectLikeTagTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string subject = "bird";
            CustomGenericList<Subject> actual;
            actual = target.TitleKeywordSelectLikeTag(sqlConnection, sqlTransaction, subject);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleSelectAuto
        ///</summary>
        [TestMethod()]
        public void TitleSelectAutoTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 1000;
            Title actual;
            actual = target.TitleSelectAuto(sqlConnection, sqlTransaction, titleID);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for TitleSelectByAuthor
        ///</summary>
        [TestMethod()]
        public void TitleSelectByAuthorTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int authorID = 93; // charles darwin
            CustomGenericList<Title> actual;
            actual = target.TitleSelectByAuthor(sqlConnection, sqlTransaction, authorID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleSelectByDOI
        ///</summary>
        [TestMethod()]
        public void TitleSelectByDOITest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string doi = "10.5962/bhl.title.2";
            CustomGenericList<Title> actual;
            actual = target.TitleSelectByDOI(sqlConnection, sqlTransaction, doi);
            Assert.IsTrue(actual.Count == 1);
        }

        /// <summary>
        ///A test for TitleSelectByIdentifier
        ///</summary>
        [TestMethod()]
        public void TitleSelectByIdentifierTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string identifierName = "oclc";
            string identifierValue = "1521478";
            CustomGenericList<Title> actual;
            actual = target.TitleSelectByIdentifier(sqlConnection, sqlTransaction, identifierName, identifierValue);
            Assert.IsTrue(actual.Count == 1);
        }

        /// <summary>
        ///A test for TitleSelectByKeyword
        ///</summary>
        [TestMethod()]
        public void TitleSelectByKeywordTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string subject = "birds";
            CustomGenericList<Title> actual;
            actual = target.TitleSelectByKeyword(sqlConnection, sqlTransaction, subject);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleSelectSearchSimple
        ///</summary>
        [TestMethod()]
        public void TitleSelectSearchSimpleTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "annals of the missouri";
            CustomGenericList<Title> actual;
            actual = target.TitleSelectSearchSimple(sqlConnection, sqlTransaction, title);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleSelectUnpublished
        ///</summary>
        [TestMethod()]
        public void TitleSelectUnpublishedTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            CustomGenericList<Title> actual;
            actual = target.TitleSelectUnpublished(sqlConnection, sqlTransaction);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleVariantSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void TitleVariantSelectByTitleIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 4220;
            CustomGenericList<TitleVariant> actual;
            actual = target.TitleVariantSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectByItemID
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByItemIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 22498;
            CustomGenericList<Part> actual = target.SegmentSelectByItemID(sqlConnection, sqlTransaction, itemID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectByAuthor
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByAuthorTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int authorID = 39105;
            CustomGenericList<Part> actual = target.SegmentSelectByAuthor(sqlConnection, sqlTransaction, authorID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectByDOI
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByDOITest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string doi = "10.4039/Ent38406-12";
            CustomGenericList<Part> actual = target.SegmentSelectByDOI(sqlConnection, sqlTransaction, doi);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SegmentSelectByIdentifier
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByIdentifierTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string identifierName = "ISSN";
            string identifierValue = "0006-9698";
            CustomGenericList<Part> actual = target.SegmentSelectByIdentifier(sqlConnection, sqlTransaction, identifierName, identifierValue);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectByKeyword
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByKeywordTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string subject = "Shells";
            CustomGenericList<Part> actual = target.SegmentSelectByKeyword(sqlConnection, sqlTransaction, subject);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectUnpublished
        ///</summary>
        [TestMethod()]
        public void SegmentSelectUnpublishedTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            CustomGenericList<Part> actual = target.SegmentSelectUnpublished(sqlConnection, sqlTransaction);
            Assert.IsTrue(actual.Count >= 0);
        }

        /// <summary>
        ///A test for SearchSegment
        ///</summary>
        [TestMethod()]
        public void SearchSegmentTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "Darwin";
            string containerTitle = string.Empty;
            string author = string.Empty;
            string date = string.Empty;
            string volume = string.Empty;
            string series = string.Empty;
            string issue = string.Empty;
            int returnCount = 1;
            string sortBy = "Title";
            CustomGenericList<Part> actual = target.SearchSegment(sqlConnection, sqlTransaction, title, containerTitle, author, date, volume, series, issue, returnCount, sortBy);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SearchSegmentFullText
        ///</summary>
        [TestMethod()]
        public void SearchSegmentFullTextTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "Darwin";
            string containerTitle = string.Empty;
            string author = string.Empty;
            string date = string.Empty;
            string volume = string.Empty;
            string series = string.Empty;
            string issue = string.Empty;
            int returnCount = 1;
            string sortBy = "Title";
            CustomGenericList<Part> actual = target.SearchSegmentFullText(sqlConnection, sqlTransaction, title, containerTitle, author, date, volume, series, issue, returnCount, sortBy);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for AuthorSelectBySegmentID
        ///</summary>
        [TestMethod()]
        public void AuthorSelectBySegmentIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 2000;
            CustomGenericList<Creator> actual = target.AuthorSelectBySegmentID(sqlConnection, sqlTransaction, segmentID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentIdentifierSelectBySegmentID
        ///</summary>
        [TestMethod()]
        public void SegmentIdentifierSelectBySegmentIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 970;
            CustomGenericList<PartIdentifier> actual = target.SegmentIdentifierSelectBySegmentID(sqlConnection, sqlTransaction, segmentID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SubjectSelectBySegmentID
        ///</summary>
        [TestMethod()]
        public void SubjectSelectBySegmentIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 41797;
            CustomGenericList<Subject> actual = target.SubjectSelectBySegmentID(sqlConnection, sqlTransaction, segmentID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for PageSelectBySegmentID
        ///</summary>
        [TestMethod()]
        public void PageSelectBySegmentIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 1000;
            CustomGenericList<PageDetail> actual = target.PageSelectBySegmentID(sqlConnection, sqlTransaction, segmentID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectRelated
        ///</summary>
        [TestMethod()]
        public void SegmentSelectRelatedTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 10409;
            CustomGenericList<Part> actual = target.SegmentSelectRelated(sqlConnection, sqlTransaction, segmentID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for NameSegmentSelectBySegmentID
        ///</summary>
        [TestMethod()]
        public void NameSegmentSelectBySegmentIDTest()
        {
            Api2DAL target = new Api2DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 41797;
            CustomGenericList<Name> actual = target.NameSegmentSelectBySegmentID(sqlConnection, sqlTransaction, segmentID);
            Assert.IsTrue(actual.Count > 0);
        }
    }
}

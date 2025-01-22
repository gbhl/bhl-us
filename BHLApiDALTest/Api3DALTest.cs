using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOBOT.BHL.API.BHLApiDAL;
using MOBOT.BHL.API.BHLApiDataObjects3;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BHLApiDALTest
{
    /// <summary>
    ///This is a test class for Api3DAL and is intended
    ///to contain all Api3DAL Unit Tests
    ///</summary>
    [TestClass()]
    public class Api3DALTest
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
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            Guid keyValue = new Guid("12345678-1234-1234-1234-123456789012");
            ApiKey actual;
            actual = target.ApiKeySelectByKey(sqlConnection, sqlTransaction, keyValue);
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void AuthorIdentifierSelectByAuthorIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int authorID = 44420;
            List<Identifier> actual = target.AuthorIdentifierSelectByAuthorID(sqlConnection, sqlTransaction, authorID);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod()]
        public void AuthorSelectByAuthorIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int authorID = 44420;
            List<Author> actual = target.AuthorSelectByAuthorID(sqlConnection, sqlTransaction, authorID);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod()]
        public void AuthorSelectByIdentifierTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string identifierName = "BioStor Author ID";
            string identifierValue = "1916";
            List<Author> actual = target.AuthorSelectByIdentifier(sqlConnection, sqlTransaction, identifierName, identifierValue);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for AuthorSelectBySegmentID
        ///</summary>
        [TestMethod()]
        public void AuthorSelectBySegmentIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 2341;
            List<Author> actual = target.AuthorSelectBySegmentID(sqlConnection, sqlTransaction, segmentID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for AuthorSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void AuthorSelectByTitleIDTest()
        {
            // Make sure a record is retrieved
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 3926;
            List<Author> actual;
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
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "Z";
            List<Author> actual;
            actual = target.AuthorSelectNameStartsWith(sqlConnection, sqlTransaction, name);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for CollectionSelectActive
        ///</summary>
        [TestMethod()]
        public void CollectionSelectActiveTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            List<Collection> actual;
            actual = target.CollectionSelectActive(sqlConnection, sqlTransaction);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for CollectionSelectByItemID
        ///</summary>
        [TestMethod()]
        public void CollectionSelectByItemIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 200;
            List<Collection> actual;
            actual = target.CollectionSelectByItemID(sqlConnection, sqlTransaction, itemID);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for CollectionSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void CollectionSelectByTitleIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 200;
            List<Collection> actual;
            actual = target.CollectionSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for IndicatedPageSelectByPageID
        ///</summary>
        [TestMethod()]
        public void IndicatedPageSelectByPageIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 3003000;
            List<PageNumber> actual;
            actual = target.IndicatedPageSelectByPageID(sqlConnection, sqlTransaction, pageID);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void InstitutionSelectAll()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            List<Institution> actual = target.InstitutionSelectAll(sqlConnection, sqlTransaction);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for ItemSelectByBarcode
        ///</summary>
        [TestMethod()]
        public void ItemSelectByBarcodeTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string barcode = "journalofmicrosc04post";
            List<Item> actual;
            actual = target.ItemSelectByBarcode(sqlConnection, sqlTransaction, barcode);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for ItemSelectByItemID
        ///</summary>
        [TestMethod()]
        public void ItemSelectByItemIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 22010;
            List<Item> actual;
            actual = target.ItemSelectByItemID(sqlConnection, sqlTransaction, itemID);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for ItemSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void ItemSelectByTitleIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 2187;
            List<Item> actual;
            actual = target.ItemSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for LanguageSelectWithPublishedItems
        ///</summary>
        [TestMethod()]
        public void LanguageSelectWithPublishedItemsTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            List<Language> actual;
            actual = target.LanguageSelectWithPublishedItems(sqlConnection, sqlTransaction);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for PageNameSelectByPageID
        ///</summary>
        [TestMethod()]
        public void NamePageSelectByPageIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 3116320;
            List<Name> actual;
            actual = target.NamePageSelectByPageID(sqlConnection, sqlTransaction, pageID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for ResolvedNameSelectByNameLike
        ///</summary>
        [TestMethod()]
        public void NameResolvedSelectByNameLikeTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "mollusca";
            List<Name> actual;
            actual = target.NameResolvedSelectByNameLike(sqlConnection, sqlTransaction, name);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for NameSegmentSelectBySegmentID
        ///</summary>
        [TestMethod()]
        public void NameSegmentSelectBySegmentIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 41797;
            List<Name> actual = target.NameSegmentSelectBySegmentID(sqlConnection, sqlTransaction, segmentID);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for PageSelectAuto
        ///</summary>
        [TestMethod()]
        public void PageSelectAutoTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 3003000;
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
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 22010;
            List<PageDetail> actual;
            actual = target.PageSelectByItemID(sqlConnection, sqlTransaction, itemID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for PageSelectBySegmentID
        ///</summary>
        [TestMethod()]
        public void PageSelectBySegmentIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 2341;
            List<PageDetail> actual = target.PageSelectBySegmentID(sqlConnection, sqlTransaction, segmentID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for PageTypeSelectByPageID
        ///</summary>
        [TestMethod()]
        public void PageTypeSelectByPageIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 3003000;
            List<PageType> actual;
            actual = target.PageTypeSelectByPageID(sqlConnection, sqlTransaction, pageID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchAuthor
        ///</summary>
        [TestMethod()]
        public void SearchAuthorTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "huxley";
            List<Author> actual;
            actual = target.SearchAuthor(sqlConnection, sqlTransaction, name);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchBook
        ///</summary>
        [TestMethod()]
        public void SearchBookTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "Proceedings of the National Shellfisheries";
            string authorLastName = string.Empty;
            string volume = string.Empty;
            string edition = string.Empty;
            Nullable<int> year = new Nullable<int>();
            string subject = string.Empty;
            string languageCode = string.Empty;
            Nullable<int> collectionID = new Nullable<int>();
            int returnCount = 10;
            List<SearchBookResult> actual;
            actual = target.SearchBook(sqlConnection, sqlTransaction, title, authorLastName, volume, edition, year, subject, languageCode, collectionID, returnCount);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchBookFullText
        ///</summary>
        [TestMethod()]
        public void SearchBookFullTextTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "Shellfisheries";
            string authorLastName = string.Empty;
            string volume = string.Empty;
            string edition = string.Empty;
            Nullable<int> year = new Nullable<int>();
            string subject = string.Empty;
            string languageCode = string.Empty;
            Nullable<int> collectionID = new Nullable<int>();
            int returnCount = 10;
            List<SearchBookResult> actual;
            actual = target.SearchBookFullText(sqlConnection, sqlTransaction, title, authorLastName, volume, edition, year, subject, languageCode, collectionID, returnCount);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchTitleKeyword
        ///</summary>
        [TestMethod()]
        public void SearchTitleKeywordTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string tag = "birds";
            List<Subject> actual;
            actual = target.SearchTitleKeyword(sqlConnection, sqlTransaction, tag);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SubjectSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void SubjectSelectByTitleIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 2187;
            List<Subject> actual;
            actual = target.SubjectSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleIdentifierSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void TitleIdentifierSelectByTitleIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 3926;
            List<Identifier> actual;
            actual = target.TitleIdentifierSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleKeywordSelectLikeTag
        ///</summary>
        [TestMethod()]
        public void TitleKeywordSelectLikeTagTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string subject = "bird";
            List<Subject> actual;
            actual = target.TitleKeywordSelectLikeTag(sqlConnection, sqlTransaction, subject);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleSelectAuto
        ///</summary>
        [TestMethod()]
        public void TitleSelectAutoTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 2187;
            List<Title> actual;
            actual = target.TitleSelectAuto(sqlConnection, sqlTransaction, titleID);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for TitleSelectByAuthor
        ///</summary>
        [TestMethod()]
        public void TitleSelectByAuthorTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int authorID = 1810;
            List<Title> actual;
            actual = target.TitleSelectByAuthor(sqlConnection, sqlTransaction, authorID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleSelectByDOI
        ///</summary>
        [TestMethod()]
        public void TitleSelectByDOITest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string doi = "10.5962/bhl.title.3938";
            List<Title> actual;
            actual = target.TitleSelectByDOI(sqlConnection, sqlTransaction, doi);
            Assert.IsTrue(actual.Count == 1);
        }

        /// <summary>
        ///A test for TitleSelectByIdentifier
        ///</summary>
        [TestMethod()]
        public void TitleSelectByIdentifierTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string identifierName = "oclc";
            string identifierValue = "9680810";
            List<Title> actual;
            actual = target.TitleSelectByIdentifier(sqlConnection, sqlTransaction, identifierName, identifierValue);
            Assert.IsTrue(actual.Count == 1);
        }

        /// <summary>
        ///A test for TitleSelectByKeyword
        ///</summary>
        [TestMethod()]
        public void TitleSelectByKeywordTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string subject = "birds";
            List<Title> actual;
            actual = target.TitleSelectByKeyword(sqlConnection, sqlTransaction, subject);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleVariantSelectByTitleID
        ///</summary>
        [TestMethod()]
        public void TitleVariantSelectByTitleIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 4007;
            List<TitleVariant> actual;
            actual = target.TitleVariantSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentIdentifierSelectBySegmentID
        ///</summary>
        [TestMethod()]
        public void SegmentIdentifierSelectBySegmentIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 6450;
            List<Identifier> actual = target.SegmentIdentifierSelectBySegmentID(sqlConnection, sqlTransaction, segmentID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectByItemID
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByItemIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 22418;
            List<Part> actual = target.SegmentSelectByItemID(sqlConnection, sqlTransaction, itemID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectByAuthor
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByAuthorTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int authorID = 45632;
            List<Part> actual = target.SegmentSelectByAuthor(sqlConnection, sqlTransaction, authorID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectByDOI
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByDOITest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string doi = "10.4039/Ent13220-11";
            List<Part> actual = target.SegmentSelectByDOI(sqlConnection, sqlTransaction, doi);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SegmentSelectByIdentifier
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByIdentifierTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string identifierName = "BioStor";
            string identifierValue = "127216";
            List<Part> actual = target.SegmentSelectByIdentifier(sqlConnection, sqlTransaction, identifierName, identifierValue);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectByKeyword
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByKeywordTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string subject = "Shells";
            List<Part> actual = target.SegmentSelectByKeyword(sqlConnection, sqlTransaction, subject);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for SegmentSelectRelated
        ///</summary>
        [TestMethod()]
        public void SegmentSelectRelatedTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 25777;
            List<Part> actual = target.SegmentSelectRelated(sqlConnection, sqlTransaction, segmentID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchSegment
        ///</summary>
        [TestMethod()]
        public void SearchSegmentTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "appendix";
            string containerTitle = string.Empty;
            string author = string.Empty;
            string date = string.Empty;
            string volume = string.Empty;
            string series = string.Empty;
            string issue = string.Empty;
            int returnCount = 1;
            string sortBy = "Title";
            List<Part> actual = target.SearchSegment(sqlConnection, sqlTransaction, title, containerTitle, author, date, volume, series, issue, returnCount, sortBy);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SearchSegmentFullText
        ///</summary>
        [TestMethod()]
        public void SearchSegmentFullTextTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "gopher";
            string containerTitle = string.Empty;
            string author = string.Empty;
            string date = string.Empty;
            string volume = string.Empty;
            string series = string.Empty;
            string issue = string.Empty;
            int returnCount = 1;
            string sortBy = "Title";
            List<Part> actual = target.SearchSegmentFullText(sqlConnection, sqlTransaction, title, containerTitle, author, date, volume, series, issue, returnCount, sortBy);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SubjectSelectBySegmentID
        ///</summary>
        [TestMethod()]
        public void SubjectSelectBySegmentIDTest()
        {
            Api3DAL target = new Api3DAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 41797;
            List<Subject> actual = target.SubjectSelectBySegmentID(sqlConnection, sqlTransaction, segmentID);
            Assert.IsNotNull(actual);
        }
    }
}

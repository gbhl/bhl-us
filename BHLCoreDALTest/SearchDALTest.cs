using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BHLCoreDALTest
{


    /// <summary>
    ///This is a test class for SearchDALTest and is intended
    ///to contain all SearchDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SearchDALTest
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
        ///A test for TitleSelectByKeywordInstitutionAndLanguage
        ///</summary>
        [TestMethod()]
        public void TitleSelectByKeywordTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string keyword = "birds";
            List<SearchBookResult> actual = target.TitleSelectByKeyword(sqlConnection, sqlTransaction, keyword);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleSelectByAuthor
        ///</summary>
        [TestMethod()]
        public void TitleSelectByAuthorTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int authorId = 178;
            List<SearchBookResult> actual = target.TitleSelectByAuthor(sqlConnection, sqlTransaction, authorId);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchTitleKeyword
        ///</summary>
        [TestMethod()]
        public void SearchTitleKeywordTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string keyword = "birds";
            string languageCode = string.Empty;
            int returnCount = 1;
            List<TitleKeyword> actual = target.SearchTitleKeyword(sqlConnection, sqlTransaction, keyword, languageCode, returnCount);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SearchBookGlobalFullText
        ///</summary>
        [TestMethod()]
        public void SearchBookGlobalFullTextTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string searchText = "d";
            int returnCount = 1;
            string searchSort = string.Empty;
            List<SearchBookResult> actual = target.SearchBookGlobalFullText(sqlConnection, sqlTransaction, searchText, returnCount, searchSort);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SearchBookFullText
        ///</summary>
        [TestMethod()]
        public void SearchBookFullTextTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "o";
            string authorLastName = string.Empty;
            string volume = string.Empty;
            string edition = string.Empty;
            Nullable<int> year = new Nullable<int>();
            string subject = string.Empty;
            string languageCode = string.Empty;
            Nullable<int> collectionID = new Nullable<int>();
            int returnCount = 1;
            string searchSort = string.Empty;
            List<SearchBookResult> actual = target.SearchBookFullText(sqlConnection, sqlTransaction, title, authorLastName, volume, edition, year, subject, languageCode, collectionID, returnCount, searchSort);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SearchBook
        ///</summary>
        [TestMethod()]
        public void SearchBookTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "o";
            string authorLastName = string.Empty;
            string volume = string.Empty;
            string edition = string.Empty;
            Nullable<int> year = new Nullable<int>();
            string subject = string.Empty;
            string languageCode = string.Empty;
            Nullable<int> collectionID = new Nullable<int>();
            int returnCount = 1;
            string searchSort = string.Empty;
            List<SearchBookResult> actual = target.SearchBook(sqlConnection, sqlTransaction, title, authorLastName, volume, edition, year, subject, languageCode, collectionID, returnCount, searchSort);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SearchAuthorComplete
        ///</summary>
        [TestMethod()]
        public void SearchAuthorCompleteTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string authorName = "thomas henry huxley";
            List<Author> actual = target.SearchAuthorComplete(sqlConnection, sqlTransaction, authorName);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchAuthor
        ///</summary>
        [TestMethod()]
        public void SearchAuthorTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string authorName = "huxley";
            int returnCount = 1;
            List<Author> actual = target.SearchAuthor(sqlConnection, sqlTransaction, authorName, returnCount);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SearchAnnotation
        ///</summary>
        [TestMethod()]
        public void SearchAnnotationTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string annotationText = "d";
            string title = string.Empty;
            string authorLastName = string.Empty;
            string volume = string.Empty;
            string edition = string.Empty;
            Nullable<int> year = new Nullable<int>();
            Nullable<int> collectionID = new Nullable<int>();
            Nullable<int> annotationSourceID = new Nullable<int>();
            int returnCount = 1;
            List<SearchAnnotationResult> actual = target.SearchAnnotation(sqlConnection, sqlTransaction, annotationText, title, authorLastName, volume, edition, year, collectionID, annotationSourceID, returnCount);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SearchSegmentComplete
        ///</summary>
        [TestMethod()]
        public void SearchSegmentCompleteTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "description";
            List<Segment> actual = target.SearchSegmentComplete(sqlConnection, sqlTransaction, title);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SearchSegment
        ///</summary>
        [TestMethod()]
        public void SearchSegmentTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "S";
            string containerTitle = string.Empty;
            string authorLastName = string.Empty;
            string date = string.Empty;
            string volume = string.Empty;
            string series = string.Empty;
            string issue = string.Empty;
            int returnCount = 1;
            string searchSort = "Title";
            List<Segment> actual = target.SearchSegment(sqlConnection, sqlTransaction, title, containerTitle, authorLastName, date, volume, series, issue, returnCount, searchSort);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SearchSegmentAdvancedFullText
        ///</summary>
        [TestMethod()]
        public void SearchSegmentAdvancedFullTextTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "gopher";
            string containerTitle = string.Empty;
            string authorLastName = string.Empty;
            string date = string.Empty;
            string volume = string.Empty;
            string series = string.Empty;
            string issue = string.Empty;
            int returnCount = 1;
            string searchSort = "Title";
            List<Segment> actual = target.SearchSegmentAdvancedFullText(sqlConnection, sqlTransaction, title, containerTitle, authorLastName, date, volume, series, issue, returnCount, searchSort);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for SearchSegmentFullText
        ///</summary>
        [TestMethod()]
        public void SearchSegmentFullTextTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string searchText = "gopher";
            int returnCount = 1;
            string searchSort = "Title";
            List<Segment> actual = target.SearchSegmentFullText(sqlConnection, sqlTransaction, searchText, returnCount, searchSort);
            Assert.AreEqual(actual.Count, 1);
        }

        [TestMethod]
        public void ItemSelectByCollectionAndStartsWithTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int collectionID = 12;
            string startsWith = "D";
            int pageNum = 1;
            int numPages = 10;
            string sort = "title";
            Tuple<int, List<SearchBookResult>> actual = target.ItemSelectByCollectionAndStartsWith(sqlConnection, sqlTransaction, collectionID, startsWith, pageNum, numPages, sort);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void TitleSelectByCollectionAndStartsWithTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int collectionID = 12;
            string startsWith = "D";
            int pageNum = 1;
            int numPages = 10;
            string sort = "title";
            Tuple<int, List<SearchBookResult>> actual = target.TitleSelectByCollectionAndStartsWith(sqlConnection, sqlTransaction, collectionID, startsWith, pageNum, numPages, sort);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void TitleSelectByDateRangeTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int startYear = 1900;
            int endYear = 2000;
            int pageNum = 1;
            int numPages = 10;
            string sort = "title";
            Tuple<int, List<SearchBookResult>> actual = target.TitleSelectByDateRange(sqlConnection, sqlTransaction, startYear, endYear, pageNum, numPages, sort);
            Assert.IsTrue(actual.Item2.Count > 0);
        }

        [TestMethod]
        public void TitleSelectByInstitutionAndStartsWithTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string institutionCode = "MBLWHOI";
            string startsWith = "C";
            int pageNum = 1;
            int numPages = 10;
            string sort = "title";
            Tuple<int, List<SearchBookResult>> actual = target.TitleSelectByInstitutionAndStartsWith(sqlConnection, sqlTransaction, institutionCode, startsWith, pageNum, numPages, sort);
            Assert.IsTrue(actual.Item2.Count > 0);
        }

        [TestMethod]
        public void TitleSelectByInstitutionAndStartsWithoutTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string institutionCode = "MBLWHOI";
            string startsWith = "X";
            int pageNum = 1;
            int numPages = 10;
            string sort = "title";
            Tuple<int, List<SearchBookResult>> actual = target.TitleSelectByInstitutionAndStartsWithout(sqlConnection, sqlTransaction, institutionCode, startsWith, pageNum, numPages, sort);
            Assert.IsTrue(actual.Item2.Count > 0);
        }

        [TestMethod]
        public void TitleSelectByNameLikeTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string startsWith = "C";
            int pageNum = 1;
            int numPages = 10;
            string sort = "title";
            Tuple<int, List<SearchBookResult>> actual = target.TitleSelectByNameLike(sqlConnection, sqlTransaction, startsWith, pageNum, numPages, sort);
            Assert.IsTrue(actual.Item2.Count > 0);
        }

        [TestMethod]
        public void TitleSelectByNameNotLikeTest()
        {
            SearchDAL target = new SearchDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string startsWith = "C";
            List<SearchBookResult> actual = target.TitleSelectByNameNotLike(sqlConnection, sqlTransaction, startsWith);
            Assert.IsTrue(actual.Count > 0);
        }

    }
}

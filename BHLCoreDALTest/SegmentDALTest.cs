using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BHLCoreDALTest
{


    /// <summary>
    ///This is a test class for SegmentDALTest and is intended
    ///to contain all SegmentDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SegmentDALTest
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
        ///A test for SegmentSimpleSelectByAuthor
        ///</summary>
        [TestMethod()]
        public void SegmentSimpleSelectByAuthorTest()
        {
            SegmentDAL target = new SegmentDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int authorId = 45632;
            List<Segment> actual = target.SegmentSimpleSelectByAuthor(sqlConnection, sqlTransaction, authorId);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectBibTexForSegmentID
        ///</summary>
        [TestMethod()]
        public void SegmentSelectBibTexForSegmentIDTest()
        {
            SegmentDAL target = new SegmentDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentId = 2341;
            short includeNoContent = 0;
            List<TitleBibTeX> actual = target.SegmentSelectBibTexForSegmentID(sqlConnection, sqlTransaction, segmentId, includeNoContent);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectForSegmentID
        ///</summary>
        [TestMethod()]
        public void SegmentSelectForSegmentIDTest()
        {
            SegmentDAL target = new SegmentDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentId = 2341;
            Segment actual = target.SegmentSelectForSegmentID(sqlConnection, sqlTransaction, segmentId);
            Assert.AreEqual(actual.SegmentID, 2341);
        }

        /// <summary>
        ///A test for SegmentSelectExtended
        ///</summary>
        [TestMethod()]
        public void SegmentSelectExtendedTest()
        {
            SegmentDAL target = new SegmentDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentId = 2341;
            Segment actual = target.SegmentSelectExtended(sqlConnection, sqlTransaction, segmentId);
            Assert.AreEqual(actual.SegmentID, 2341);
            Assert.IsTrue(actual.AuthorList.Count > 0);
            Assert.IsTrue(actual.IdentifierList.Count > 0);
            Assert.IsTrue(actual.KeywordList.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectByItemID
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByItemIDTest()
        {
            SegmentDAL target = new SegmentDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemId = 282;
            Segment actual = target.SegmentSelectByItemID(sqlConnection, sqlTransaction, itemId);
            Assert.AreEqual(actual.ItemID, 282);
        }

        /// <summary>
        ///A test for SegmentSelectPublished
        ///</summary>
        [TestMethod()]
        public void SegmentSelectPublishedTest()
        {
            SegmentDAL target = new SegmentDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            List<Segment> actual = target.SegmentSelectPublished(sqlConnection, sqlTransaction);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectRelated
        ///</summary>
        [TestMethod()]
        public void SegmentSelectRelatedTest()
        {
            SegmentDAL target = new SegmentDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentId = 25777;
            List<Segment> actual = target.SegmentSelectRelated(sqlConnection, sqlTransaction, segmentId);
            Assert.IsTrue(actual.Count > 0);
        }


        /// <summary>
        ///A test for SegmentSelectByDateRange
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByDateRangeTest()
        {
            SegmentDAL target = new SegmentDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string startDate = "1900";
            string endDate = "1923";
            int pageNum = 1;
            int numPages = 10;
            string sort = "title";
            Tuple<int, List<Segment>> actual = target.SegmentSelectByDateRange(sqlConnection, sqlTransaction, startDate, endDate, pageNum, numPages, sort);
            Assert.IsTrue(actual.Item2.Count > 0);
        }

        /// <summary>
        ///A test for SegmentSelectByTitleLike
        ///</summary>
        [TestMethod()]
        public void SegmentSelectByTitleLikeTest()
        {
            SegmentDAL target = new SegmentDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string title = "q";
            int pageNum = 1;
            int numPages = 10;
            string sort = "title";
            Tuple<int, List<Segment>> actual = target.SegmentSelectByTitleLike(sqlConnection, sqlTransaction, title, pageNum, numPages, sort);
            Assert.IsTrue(actual.Item2.Count > 0);
        }

        [TestMethod()]
        public void SegmentSelectByInstitutionAndStartsWithTest()
        {
            SegmentDAL target = new SegmentDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string institutionCode = "BSTOR";
            string startsWith = "N";
            int pageNum = 1;
            int numPages = 10;
            string sort = "title";
            Tuple<int, List<Segment>> actual = target.SegmentSelectByInstitutionAndStartsWith(sqlConnection, sqlTransaction, institutionCode, startsWith, pageNum, numPages, sort);
            Assert.IsTrue(actual.Item2.Count > 0);
        }

        [TestMethod()]
        public void SegmentSelectWithoutSubmittedDOITest()
        {
            SegmentDAL target = new SegmentDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int numberToReturn = 10;
            List<DOI> actual = target.SegmentSelectWithoutSubmittedDOI(sqlConnection, sqlTransaction, numberToReturn);
            Assert.IsTrue(actual.Count > 0);
            Assert.IsTrue(actual.Count <= 10);
        }
    }
}

using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace BHLCoreDALTest
{
    /// <summary>
    ///This is a test class for PageDALTest and is intended
    ///to contain all PageDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PageDALTest
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
        ///A test for PageSelectWithoutPageNames
        ///</summary>
        [TestMethod()]
        public void PageSelectWithoutPageNamesTest()
        {
            PageDAL target = new PageDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            CustomGenericList<Page> actual;
            actual = target.PageSelectWithoutPageNames(sqlConnection, sqlTransaction);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for PageSelectWithoutPageNamesForItem
        ///</summary>
        [TestMethod()]
        public void PageSelectWithoutPageNamesForItemTest()
        {
            PageDAL target = new PageDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 1000;
            CustomGenericList<Page> actual;
            actual = target.PageSelectWithoutPageNamesForItem(sqlConnection, sqlTransaction, itemID);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for PageSelectWithExpiredPageNamesByItemID
        ///</summary>
        [TestMethod()]
        public void PageSelectWithExpiredPageNamesByItemIDTest()
        {
            PageDAL target = new PageDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 22004;
            int maxAge = 0;
            CustomGenericList<Page> actual;
            actual = target.PageSelectWithExpiredPageNamesByItemID(sqlConnection, sqlTransaction, itemID, maxAge);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod()]
        public void PageSelectOcrPathForPageIDTest()
        {
            PageDAL target = new PageDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 3299787;
            Page actual;
            actual = target.PageSelectOcrPathForPageID(sqlConnection, sqlTransaction, pageID);
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void PageSelectExternalUrlForPageID()
        {
            PageDAL target = new PageDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 3299787;
            Page actual;
            actual = target.PageSelectExternalUrlForPageID(sqlConnection, sqlTransaction, pageID);
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void PageSelectByItemAndPageNumberTest1()
        {
            PageDAL target = new PageDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            // Matching record
            int itemID = 22024;
            string volume = "12";
            string pageNumber = "382";
            CustomGenericList<Page> actual = new CustomGenericList<Page>();
            actual = target.PageSelectByItemAndPageNumber(sqlConnection, sqlTransaction, itemID, volume, pageNumber);
            Assert.IsTrue(actual.Count == 1);
        }

        [TestMethod()]
        public void PageSelectByItemAndPageNumberTest2()
        {
            PageDAL target = new PageDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            // Matching record
            int itemID = 22024;
            string volume = "";
            string pageNumber = "382";
            CustomGenericList<Page> actual = new CustomGenericList<Page>();
            actual = target.PageSelectByItemAndPageNumber(sqlConnection, sqlTransaction, itemID, volume, pageNumber);
            Assert.IsTrue(actual.Count == 1);
        }

        [TestMethod()]
        public void PageSelectByItemAndPageNumberTest3()
        {
            PageDAL target = new PageDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            // No matching record
            int itemID = 22024;
            string volume = "13";
            string pageNumber = "382";
            CustomGenericList<Page> actual = new CustomGenericList<Page>();
            actual = target.PageSelectByItemAndPageNumber(sqlConnection, sqlTransaction, itemID, volume, pageNumber);
            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod()]
        public void PageSelectByItemAndPageNumberTest4()
        {
            PageDAL target = new PageDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            // No matching record
            int itemID = 22024;
            string volume = "12";
            string pageNumber = "450";
            CustomGenericList<Page> actual = new CustomGenericList<Page>();
            actual = target.PageSelectByItemAndPageNumber(sqlConnection, sqlTransaction, itemID, volume, pageNumber);
            Assert.IsTrue(actual.Count == 0);
        }
    }
}

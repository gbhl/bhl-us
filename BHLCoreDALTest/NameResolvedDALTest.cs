using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace BHLCoreDALTest
{
    
    
    /// <summary>
    ///This is a test class for NameResolvedDALTest and is intended
    ///to contain all NameResolvedDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NameResolvedDALTest
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
        ///A test for NameResolvedSelectByNameLike
        ///</summary>
        [TestMethod()]
        public void NameResolvedSelectByNameLikeTest()
        {
            NameResolvedDAL target = new NameResolvedDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "strix varia";
            int returnCount = 1;
            CustomGenericList<NameResolved> actual = target.NameResolvedSelectByNameLike(sqlConnection, sqlTransaction, name, returnCount);
            Assert.AreEqual(actual.Count, 1);
        }

        /// <summary>
        ///A test for NameResolvedSearchForPages
        ///</summary>
        [TestMethod()]
        public void NameResolvedSearchForPagesTest()
        {
            NameResolvedDAL target = new NameResolvedDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "strix varia";
            int numberOfRows = 10;
            int pageNumber = 1;
            string sortColumn = "ShortTitle";
            string sortDirection = "ASC";
            CustomGenericList<CustomDataRow> actual = target.NameResolvedSearchForPages(sqlConnection, sqlTransaction, name, numberOfRows, pageNumber, sortColumn, sortDirection);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for NameResolvedSearchForPagesDownload
        ///</summary>
        [TestMethod()]
        public void NameResolvedSearchForPagesDownloadTest()
        {
            NameResolvedDAL target = new NameResolvedDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "strix varia";
            CustomGenericList<CustomDataRow> actual = target.NameResolvedSearchForPagesDownload(sqlConnection, sqlTransaction, name);
            Assert.IsTrue(actual.Count > 0);
        }
    }
}

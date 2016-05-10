using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;

namespace BHLCoreDALTest
{
    /// <summary>
    ///This is a test class for ItemCOinSDALTest and is intended
    ///to contain all ItemCOinSDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ItemCOinSDALTest
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
        ///A test for ItemCOinSSelectByItemId
        ///</summary>
        [TestMethod()]
        public void ItemCOinSSelectByItemIdTest()
        {
            ItemCOinSDAL target = new ItemCOinSDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemId = 22004;
            ItemCOinSView actual;
            actual = target.ItemCOinSSelectByItemId(sqlConnection, sqlTransaction, itemId);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for ItemCOinSSelectByTitleId
        ///</summary>
        [TestMethod()]
        public void ItemCOinSSelectByTitleIdTest()
        {
            ItemCOinSDAL target = new ItemCOinSDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleId = 3926;
            ItemCOinSView actual;
            actual = target.ItemCOinSSelectByTitleId(sqlConnection, sqlTransaction, titleId);
            Assert.IsNotNull(actual);
        }
    }
}

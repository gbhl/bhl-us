using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace BHLCoreDALTest
{
    /// <summary>
    ///This is a test class for ItemDALTest and is intended
    ///to contain all ItemDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ItemDALTest
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
        ///A test for ItemSelectWithExpiredPageNames
        ///</summary>
        [TestMethod()]
        public void ItemSelectWithExpiredPageNamesTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int maxAge = 0;
            CustomGenericList<Item> actual;
            actual = target.ItemSelectWithExpiredPageNames(sqlConnection, sqlTransaction, maxAge);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for ItemSelectWithoutPageNames
        ///</summary>
        [TestMethod()]
        public void ItemSelectWithoutPageNamesTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            CustomGenericList<Item> actual;
            actual = target.ItemSelectWithoutPageNames(sqlConnection, sqlTransaction);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for ItemSelectByInstitution
        ///</summary>
        [TestMethod()]
        public void ItemSelectByInstitutionTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string institutionCode = "MO";
            int returnCount = 1;
            string sortBy = "Date";
            CustomGenericList<Item> actual = target.ItemSelectByInstitution(sqlConnection, sqlTransaction, institutionCode, returnCount, sortBy);
            Assert.AreEqual(actual.Count, 1);
        }
    }
}

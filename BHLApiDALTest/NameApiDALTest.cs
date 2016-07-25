using MOBOT.BHL.API.BHLApiDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.API.BHLApiDataObjects;
using CustomDataAccess;

namespace BHLApiDALTest
{
    /// <summary>
    ///This is a test class for NameApiDALTest and is intended
    ///to contain all NameApiDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NameApiDALTest
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
        ///A test for PageNameCountUniqueConfirmed
        ///</summary>
        [TestMethod()]
        public void NameCountUniqueConfirmedTest()
        {
            NameApiDAL target = new NameApiDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int actual;
            actual = target.NameCountUniqueConfirmed(sqlConnection, sqlTransaction);
            Assert.IsTrue(actual > 0);
        }

        /// <summary>
        ///A test for PageNameCountUniqueConfirmedBetweenDates
        ///</summary>
        [TestMethod()]
        public void NameCountUniqueConfirmedBetweenDatesTest()
        {
            NameApiDAL target = new NameApiDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            DateTime startDate = new DateTime(2008, 1, 1);
            DateTime endDate = new DateTime(2008, 2, 1);
            int actual;
            actual = target.NameCountUniqueConfirmedBetweenDates(sqlConnection, sqlTransaction, startDate, endDate);
            Assert.IsTrue(actual == 0);
        }

        /// <summary>
        ///A test for PageNameListActive
        ///</summary>
        [TestMethod()]
        public void NameListActiveTest()
        {
            NameApiDAL target = new NameApiDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int startRow = 1;
            int batchSize = 10;
            CustomGenericList<Name> actual;
            actual = target.NameListActive(sqlConnection, sqlTransaction, startRow, batchSize);
            Assert.IsTrue(actual.Count == 10);
        }

        /// <summary>
        ///A test for PageNameListActiveBetweenDates
        ///</summary>
        [TestMethod()]
        public void NameListActiveBetweenDatesTest()
        {
            NameApiDAL target = new NameApiDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int startRow = 1;
            int batchSize = 10;
            DateTime startDate = new DateTime(2008, 1, 24);
            DateTime endDate = new DateTime(2008, 1, 26);
            CustomGenericList<Name> actual;
            actual = target.NameListActiveBetweenDates(sqlConnection, sqlTransaction, startRow, batchSize, startDate, endDate);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for PageSelectByNameBankID
        ///</summary>
        [TestMethod()]
        public void PageSelectByNameBankIDTest()
        {
            NameApiDAL target = new NameApiDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string nameBankID = "2661223"; // poa annua
            CustomGenericList<PageDetail> actual;
            actual = target.PageSelectByNameBankID(sqlConnection, sqlTransaction, nameBankID);
            Assert.IsNotNull(actual);
        }
    }
}

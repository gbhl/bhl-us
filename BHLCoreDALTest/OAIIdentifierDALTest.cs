using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace BHLCoreDALTest
{
    /// <summary>
    ///This is a test class for OAIIdentifierDALTest and is intended
    ///to contain all OAIIdentifierDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OAIIdentifierDALTest
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
        ///A test for OAIIdentifierSelectAll
        ///</summary>
        [TestMethod()]
        public void OAIIdentifierSelectAllTest()
        {
            // Simple test to make sure data is returned from the database
            OAIIdentifierDAL target = new OAIIdentifierDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int maxIdentifiers = 10;
            int startId = 1;
            string set = string.Empty;
            Nullable<DateTime> fromDate = new Nullable<DateTime>();
            Nullable<DateTime> untilDate = new Nullable<DateTime>();
            CustomGenericList<OAIIdentifier> actual;
            actual = target.OAIIdentifierSelectAll(sqlConnection, sqlTransaction, maxIdentifiers, startId, set, fromDate, untilDate);
            Assert.IsTrue(actual.Count == 10);
        }

        /// <summary>
        ///A test for OAIIdentifierSelectItems
        ///</summary>
        [TestMethod()]
        public void OAIIdentifierSelectItemsTest()
        {
            OAIIdentifierDAL target = new OAIIdentifierDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int maxIdentifiers = 10;
            int startId = 1;
            Nullable<DateTime> fromDate = new Nullable<DateTime>();
            Nullable<DateTime> untilDate = new Nullable<DateTime>();
            CustomGenericList<OAIIdentifier> actual;
            actual = target.OAIIdentifierSelectItems(sqlConnection, sqlTransaction, maxIdentifiers, startId, fromDate, untilDate);
            Assert.IsTrue(actual.Count == 10);
        }

        /// <summary>
        ///A test for OAIIdentifierSelectPDFs
        ///</summary>
        [TestMethod()]
        public void OAIIdentifierSelectPDFsTest()
        {
            OAIIdentifierDAL target = new OAIIdentifierDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int maxIdentifiers = 10;
            int startId = 1;
            Nullable<DateTime> fromDate = new Nullable<DateTime>();
            Nullable<DateTime> untilDate = new Nullable<DateTime>();
            CustomGenericList<OAIIdentifier> actual;
            actual = target.OAIIdentifierSelectPDFs(sqlConnection, sqlTransaction, maxIdentifiers, startId, fromDate, untilDate);
            Assert.IsTrue(actual.Count == 10);
        }

        /// <summary>
        ///A test for OAIIdentifierSelectTitles
        ///</summary>
        [TestMethod()]
        public void OAIIdentifierSelectTitlesTest()
        {
            OAIIdentifierDAL target = new OAIIdentifierDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int maxIdentifiers = 10;
            int startId = 1;
            Nullable<DateTime> fromDate = new Nullable<DateTime>();
            Nullable<DateTime> untilDate = new Nullable<DateTime>();
            CustomGenericList<OAIIdentifier> actual;
            actual = target.OAIIdentifierSelectTitles(sqlConnection, sqlTransaction, maxIdentifiers, startId, fromDate, untilDate);
            Assert.IsTrue(actual.Count == 10);
        }

        /// <summary>
        ///A test for OAIIdentifierSelectSegments
        ///</summary>
        [TestMethod()]
        public void OAIIdentifierSelectSegmentsTest()
        {
            OAIIdentifierDAL target = new OAIIdentifierDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int maxIdentifiers = 10;
            int startId = 1;
            Nullable<DateTime> fromDate = new Nullable<DateTime>();
            Nullable<DateTime> untilDate = new Nullable<DateTime>();
            CustomGenericList<OAIIdentifier> actual = target.OAIIdentifierSelectSegments(sqlConnection, sqlTransaction, maxIdentifiers, startId, fromDate, untilDate);
            Assert.IsTrue(actual.Count == 10);
        }
    }
}

using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Data.SqlClient;

namespace BHLCoreDALTest
{
    /// <summary>
    ///This is a test class for OpenUrlCitationDALTest and is intended
    ///to contain all OpenUrlCitationDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OpenUrlCitationDALTest
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
        ///A test for OpenUrlCitationSelectByCitationDetails
        ///</summary>
        [TestMethod()]
        public void OpenUrlCitationSelectByCitationDetailsTest()
        {
            // Simple test to ensure that data is returned from the database
            OpenUrlCitationDAL target = new OpenUrlCitationDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 1000;
            int itemID = 0;
            string doi = string.Empty;
            string title = string.Empty;
            string articleTitle = string.Empty;
            string authorLast = string.Empty;
            string authorFirst = string.Empty;
            string volume = string.Empty;
            string issue = string.Empty;
            string year = string.Empty;
            string startPage = string.Empty;
            CustomGenericList<OpenUrlCitation> actual;
            actual = target.OpenUrlCitationSelectByCitationDetails(sqlConnection, sqlTransaction, titleID, itemID, doi, title, articleTitle, authorLast, authorFirst, volume, issue, year, startPage);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for OpenUrlCitationSelectByCitationDetailsFT
        ///</summary>
        [TestMethod()]
        public void OpenUrlCitationSelectByCitationDetailsFTTest()
        {
            // Simple test to ensure that data is returned from the database
            OpenUrlCitationDAL target = new OpenUrlCitationDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 1000;
            int itemID = 0;
            string doi = string.Empty;
            string title = string.Empty;
            string articleTitle = string.Empty;
            string authorLast = string.Empty;
            string authorFirst = string.Empty;
            string volume = string.Empty;
            string issue = string.Empty;
            string year = string.Empty;
            string startPage = string.Empty;
            CustomGenericList<OpenUrlCitation> actual;
            actual = target.OpenUrlCitationSelectByCitationDetailsFT(sqlConnection, sqlTransaction, titleID, itemID, doi, title, articleTitle, authorLast, authorFirst, volume, issue, year, startPage);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for OpenUrlCitationSelectSegmentByDOI
        ///</summary>
        [TestMethod()]
        public void OpenUrlCitationSelectSegmentByDOITest()
        {
            OpenUrlCitationDAL target = new OpenUrlCitationDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string doi = "10.4039/Ent42105-4";
            CustomGenericList<OpenUrlCitation> actual;
            actual = target.OpenUrlCitationSelectByDOI(sqlConnection, sqlTransaction, doi);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for OpenUrlCitationSelectTitleByDOI
        ///</summary>
        [TestMethod()]
        public void OpenUrlCitationSelectTitleByDOITest()
        {
            OpenUrlCitationDAL target = new OpenUrlCitationDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string doi = "10.5962/bhl.title.5";
            CustomGenericList<OpenUrlCitation> actual;
            actual = target.OpenUrlCitationSelectByDOI(sqlConnection, sqlTransaction, doi);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for OpenUrlCitationSelectTitleByDOI
        ///</summary>
        [TestMethod()]
        public void OpenUrlCitationSelectItemByDOITest()
        {
            OpenUrlCitationDAL target = new OpenUrlCitationDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string doi = "TESTITEMDOI";
            CustomGenericList<OpenUrlCitation> actual;
            actual = target.OpenUrlCitationSelectByDOI(sqlConnection, sqlTransaction, doi);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for OpenUrlCitationSelectByPageID
        ///</summary>
        [TestMethod()]
        public void OpenUrlCitationSelectByPageIDTest()
        {
            OpenUrlCitationDAL target = new OpenUrlCitationDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 100000;
            CustomGenericList<OpenUrlCitation> actual;
            actual = target.OpenUrlCitationSelectByPageID(sqlConnection, sqlTransaction, pageID);
            Assert.IsTrue(actual.Count == 1);
        }
    }
}
